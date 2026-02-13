using Microsoft.Extensions.Options;
using onlineCinema.Application.Configurations;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Enums;
using onlineCinema.Domain.Extensions;

namespace onlineCinema.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SnackMapper _snackMapper;
        private readonly PaymentMapper _paymentMapper;
        private readonly BookingMapper _bookingMapper;
        private readonly BookingSettings _settings;

        public BookingService(
            IUnitOfWork unitOfWork, 
            BookingMapper mapper, 
            SnackMapper snackMapper, 
            PaymentMapper paymentMapper,
            IOptions<BookingSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _bookingMapper = mapper;
            _snackMapper = snackMapper;
            _paymentMapper = paymentMapper;
            _settings = settings.Value;
        }

        public async Task<SessionSeatMapDto> GetSessionSeatMapAsync(int sessionId)
        {
            var session = await this._unitOfWork.Session
                .GetByIdWithMovieAndHallAsync(sessionId);

            if (session == null)
            {
                throw new KeyNotFoundException($"Сеанс " +
                    $"з ID {sessionId} не знайдено.");
            }

            if (session.ShowingDateTime < DateTime.Now)
            {
                throw new InvalidOperationException("Цей сеанс " +
                    "уже відбувся або розпочався.");
            }

            var allSeats = await this._unitOfWork.Seat
                .GetSeatsByHallIdAsync(session.HallId);

            var activeTickets = await this._unitOfWork.Ticket.GetAllAsync(t =>
                t.SessionId == sessionId &&
                    (t.LockUntil > DateTime.Now 
                        || (t.Booking.Payment != null 
                            && t.Booking.Payment.Status == PaymentStatus.Completed)),
                includeProperties: "Booking,Booking.Payment"
             );

            var bookedSeatIds = activeTickets.Select(t => t.SeatId).ToHashSet();

            var seatDtos = allSeats.Select(seat =>
                this._bookingMapper
                .MapToSeatDto(seat, session.BasePrice, bookedSeatIds)
            ).ToList();

            var sessionMapDto = this._bookingMapper
                .MapToSessionSeatMapDto(session, seatDtos);

            return sessionMapDto;
        }

        public async Task<int> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            var session = await this._unitOfWork.Session
                .GetByIdWithMovieAndHallAsync(bookingDto.SessionId);
            if (session == null)
            {
                throw new KeyNotFoundException("Сеанс не знайдено.");
            }

            if (session.Movie.AgeRating != AgeRating.Age0)
            {
                if (!bookingDto.UserDateOfBirth.HasValue)
                {
                    throw new InvalidOperationException("Для бронювання " +
                        "цього фільму необхідно вказати дату народження в профілі.");
                }

                int requiredAge = (int)session.Movie.AgeRating;
                int userAge = bookingDto.UserDateOfBirth.Value.CalculateAge();

                if (userAge < requiredAge)
                {
                    throw new InvalidOperationException($"Ваш вік " +
                        $"({userAge}) менше допустимого для цього фільму ({requiredAge}+).");
                }
            }

            var activeTickets = await this._unitOfWork.Ticket
                .GetAllAsync(t => t.SessionId == bookingDto.SessionId);
            var busySeatIds = activeTickets
                .Where(t => t.LockUntil > DateTime.Now 
                    || (t.Booking?.Payment?.Status == PaymentStatus.Completed))
                .Select(t => t.SeatId);

            if (bookingDto.SeatIds.Intersect(busySeatIds).Any())
            {
                throw new InvalidOperationException("Вибачте, " +
                    "ці місця щойно були заброньовані кимось іншим.");
            }

            var booking = this._bookingMapper
                .MapCreateBookingDtoToEntity(bookingDto);

            var lockExpiration = DateTime.Now
                .AddSeconds(_settings.BookingLockSeconds);

            var allHallSeats = await this._unitOfWork.Seat
                .GetSeatsByHallIdAsync(session.HallId);

            var selectedSeats = allHallSeats
                .Where(s => bookingDto.SeatIds.Contains(s.SeatId));

            booking.Tickets = selectedSeats.Select(seat =>
                this._bookingMapper
                .MapToTicket(seat, session.SessionId, session.BasePrice, lockExpiration)
            ).ToList();

            await this._unitOfWork.Booking.AddAsync(booking);
            await this._unitOfWork.SaveAsync();

            return booking.BookingId;
        }

        public async Task<decimal> GetTicketsPriceTotalAsync(int bookingId)
        {
            var booking = await this._unitOfWork.Booking
                .GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            return booking.Tickets.Sum(t => t.Price);
        }

        public async Task AddSnacksToBookingAsync(
            int bookingId, 
            List<SelectedSnackDto> selectedSnacks)
        {
            var booking = await this._unitOfWork.Booking.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Бронювання не знайдено.");
            }

            booking.SnackBookings = this._snackMapper
                .MapSelectedSnackDtoToEntityList(selectedSnacks, bookingId);

            await this._unitOfWork.Booking.UpdateWithDetailsAsync(booking);
            await this._unitOfWork.SaveAsync();
        }

        public async Task<DateTime> GetBookingLockUntilAsync(int bookingId)
        {
            var booking = await this._unitOfWork.Booking
                .GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            return booking.Tickets.FirstOrDefault()?.LockUntil ?? DateTime.Now;
        }

        public async Task CompletePaymentAsync(int bookingId)
        {
            var booking = await this._unitOfWork.Booking
                .GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            decimal ticketsTotal = booking.Tickets
                .Sum(t => t.Price);
            decimal snacksTotal = booking.SnackBookings
                .Sum(sb => sb.Quantity * sb.Snack.Price);
            decimal totalAmount = ticketsTotal + snacksTotal;

            var payment = this._paymentMapper
                .CreateCompletedPayment(bookingId, totalAmount);

            await this._unitOfWork.Payment.AddAsync(payment);
            await this._unitOfWork.SaveAsync();

            booking.PaymentId = payment.PaymentId;
            await this._unitOfWork.Booking.UpdateWithDetailsAsync(booking);
            await this._unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string userId)
        {
            var bookings = await _unitOfWork.Booking
                .GetUserBookingsWithDetailsAsync(userId);

            if (bookings == null)
            {
                throw new KeyNotFoundException($"Бронювання " +
                    $"для користувача з ID: {userId} не знайдено.");
            }

            return bookings.Select(booking => _bookingMapper.ToBookingHistoryDto(booking));
        }

        public async Task CancelBookingAsync(int bookingId, string userId)
        {
            var booking = await _unitOfWork.Booking
                .GetByIdWithDetailsAsync(bookingId);

            if (booking == null)
            {
                throw new KeyNotFoundException("Бронювання не знайдено.");
            }

            if (booking.ApplicationUserId != userId)
            {
                throw new UnauthorizedAccessException("Ви не можете скасувати чуже бронювання.");
            }

            if (booking.Payment != null && booking.Payment.Status == PaymentStatus.Refunded)
            {
                throw new InvalidOperationException("Бронювання вже скасовано.");
            }

            bool isPaid = booking.Payment != null 
                && booking.Payment.Status == PaymentStatus.Completed;

            var session = booking.Tickets.FirstOrDefault()?.Session;
            if (session != null)
            {
                if (session.ShowingDateTime < DateTime.Now)
                {
                    throw new InvalidOperationException("Сеанс вже розпочався або минув.");
                }

                if ((session.ShowingDateTime - DateTime.Now).TotalMinutes < 60)
                {
                    throw new InvalidOperationException("Повернення можливе " +
                        "не пізніше ніж за 1 годину до початку сеансу.");
                }
            }

            // Логіка повернення
            if (booking.Payment == null)
            {
                var dummyPayment = _paymentMapper.CreateCompletedPayment(bookingId, 0);
                dummyPayment.Status = PaymentStatus.Refunded;
                await _unitOfWork.Payment.AddAsync(dummyPayment);
                booking.Payment = dummyPayment;
            }
            else
            {
                booking.Payment.Status = PaymentStatus.Refunded;

                _unitOfWork.Payment.Update(booking.Payment);

                await _unitOfWork.SaveAsync();
            }

            // Скасування блокування з місць!
            // Час блокування -1, щоб карта місць не бачила ці квитки як активні
            foreach (var ticket in booking.Tickets)
            {
                ticket.LockUntil = DateTime.Now.AddMinutes(-1);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<PagedResultDto<BookingHistoryDto>> GetBookingHistorySeekAsync(
            string userId, 
            int? lastId, 
            int? firstId)
        {
            int pageSize = 5;

            var (bookings, totalCount, hasNext, hasPrevious) =
                await _unitOfWork.Booking
                .GetUserBookingsSeekAsync(userId, lastId, firstId, pageSize);

            var dtos = bookings
                .Select(booking => _bookingMapper
                    .ToBookingHistoryDto(booking))
                .ToList();

            return _bookingMapper
                .MapToPagedResult(dtos, totalCount, pageSize, hasNext, hasPrevious);
        }
    }
}
