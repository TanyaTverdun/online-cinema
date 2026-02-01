using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using onlineCinema.Domain.Extensions;

namespace onlineCinema.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookingMapper _mapper;
        private readonly SnackMapper _snackMapper;
        private readonly PaymentMapper _paymentMapper;
        private readonly BookingMapper _bookingMapper;

        private const int BookingLockMinutes = 15;

        public BookingService(IUnitOfWork unitOfWork, BookingMapper mapper, SnackMapper snackMapper, PaymentMapper paymentMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _snackMapper = snackMapper;
            _paymentMapper = paymentMapper;
        }

        public async Task<SessionSeatMapDto> GetSessionSeatMapAsync(int sessionId)
        {
            var session = await this._unitOfWork.Session.GetByIdWithMovieAndHallAsync(sessionId);

            if (session == null)
            {
                throw new KeyNotFoundException($"Сеанс з ID {sessionId} не знайдено.");
            }

            if (session.ShowingDateTime < DateTime.Now)
            {
                throw new InvalidOperationException("Цей сеанс уже відбувся або розпочався.");
            }

            // Чи є в залі взагалі місця
            var allSeats = await this._unitOfWork.Seat.GetSeatsByHallIdAsync(session.HallId);
            if (!allSeats.Any())
            {
                throw new Exception("У вибраному залі відсутня конфігурація місць.");
            }

            // для зайнятих місць
            var activeTickets = await this._unitOfWork.Ticket.GetAllAsync(t =>
                t.SessionId == sessionId &&
                    (t.LockUntil > DateTime.Now || (t.Booking.Payment != null && t.Booking.Payment.Status == PaymentStatus.Completed)),
                includeProperties: "Booking,Booking.Payment"
             );

            // HashSet з ID зайнятих місць.
            // щоб пошук .Contains() працював миттєво (O(1)), 
            // а не перебирав список для кожного місця.
            var bookedSeatIds = activeTickets.Select(t => t.SeatId).ToHashSet();

            var seatDtos = allSeats.Select(seat =>
                this._mapper.MapToSeatDto(seat, session.BasePrice, bookedSeatIds)
            ).ToList();

            var sessionMapDto = this._mapper.MapToSessionSeatMapDto(session, seatDtos);

            return sessionMapDto;
        }

        public async Task<int> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            var session = await this._unitOfWork.Session.GetByIdWithMovieAndHallAsync(bookingDto.SessionId);
            if (session == null)
            {
                throw new KeyNotFoundException("Сеанс не знайдено.");
            }

            if (session.Movie.AgeRating != AgeRating.Age0)
            {
                if (!bookingDto.UserDateOfBirth.HasValue)
                {
                    throw new InvalidOperationException("Для бронювання цього фільму необхідно вказати дату народження в профілі.");
                }

                int requiredAge = (int)session.Movie.AgeRating;
                int userAge = bookingDto.UserDateOfBirth.Value.CalculateAge();

                if (userAge < requiredAge)
                {
                    throw new InvalidOperationException($"Ваш вік ({userAge}) менше допустимого для цього фільму ({requiredAge}+).");
                }
            }

            var activeTickets = await this._unitOfWork.Ticket.GetAllAsync(t => t.SessionId == bookingDto.SessionId);
            var busySeatIds = activeTickets
                .Where(t => t.LockUntil > DateTime.Now || (t.Booking?.Payment?.Status == PaymentStatus.Completed))
                .Select(t => t.SeatId);

            if (bookingDto.SeatIds.Intersect(busySeatIds).Any())
            {
                throw new InvalidOperationException("Вибачте, ці місця щойно були заброньовані кимось іншим.");
            }

            var booking = this._mapper.MapCreateBookingDtoToEntity(bookingDto);

            var lockExpiration = DateTime.Now.AddMinutes(BookingLockMinutes);

            var allHallSeats = await this._unitOfWork.Seat.GetSeatsByHallIdAsync(session.HallId);
            var selectedSeats = allHallSeats.Where(s => bookingDto.SeatIds.Contains(s.SeatId));

            booking.Tickets = selectedSeats.Select(seat =>
                this._mapper.MapToTicket(seat, session.SessionId, session.BasePrice, lockExpiration)
            ).ToList();

            await this._unitOfWork.Booking.AddAsync(booking);
            await this._unitOfWork.SaveAsync();

            return booking.BookingId;
        }

        public async Task<decimal> GetTicketsPriceTotalAsync(int bookingId)
        {
            var booking = await this._unitOfWork.Booking.GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            return booking.Tickets.Sum(t => t.Price);
        }

        public async Task AddSnacksToBookingAsync(int bookingId, List<SelectedSnackDto> selectedSnacks)
        {
            var booking = await this._unitOfWork.Booking.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Бронювання не знайдено.");
            }

            booking.SnackBookings = this._snackMapper.MapSelectedSnackDtoToEntityList(selectedSnacks, bookingId);

            await this._unitOfWork.Booking.UpdateWithDetailsAsync(booking);
            await this._unitOfWork.SaveAsync();
        }

        public async Task<DateTime> GetBookingLockUntilAsync(int bookingId)
        {
            var booking = await this._unitOfWork.Booking.GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            return booking.Tickets.FirstOrDefault()?.LockUntil ?? DateTime.Now;
        }

        public async Task CompletePaymentAsync(int bookingId)
        {
            var booking = await this._unitOfWork.Booking.GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            decimal ticketsTotal = booking.Tickets.Sum(t => t.Price);
            decimal snacksTotal = booking.SnackBookings.Sum(sb => sb.Quantity * sb.Snack.Price);
            decimal totalAmount = ticketsTotal + snacksTotal;

            var payment = this._paymentMapper.CreateCompletedPayment(bookingId, totalAmount);

            await this._unitOfWork.Payment.AddAsync(payment);
            await this._unitOfWork.SaveAsync();

            booking.PaymentId = payment.PaymentId;
            await this._unitOfWork.Booking.UpdateWithDetailsAsync(booking);
            await this._unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string userId)
        {
            var bookings = await _unitOfWork.Booking.GetUserBookingsWithDetailsAsync(userId);

            if (bookings == null)
            {
                throw new KeyNotFoundException($"Bookings not found for user with ID: {userId}");
            }

            return bookings.Select(booking => _bookingMapper.ToBookingHistoryDto(booking));
        }
    }
}
