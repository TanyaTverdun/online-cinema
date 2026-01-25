using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookingMapper _mapper;
        private readonly SnackMapper _snackMapper;

        public BookingService(IUnitOfWork unitOfWork, BookingMapper mapper, SnackMapper snackMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _snackMapper = snackMapper;
        }

        public async Task<SessionSeatMapDto> GetSessionSeatMapAsync(int sessionId)
        {
            var session = await _unitOfWork.Session.GetByIdWithMovieAndHallAsync(sessionId);

            if (session == null)
            {
                throw new KeyNotFoundException($"Сеанс з ID {sessionId} не знайдено.");
            }

            if (session.ShowingDateTime < DateTime.Now)
            {
                throw new InvalidOperationException("Цей сеанс уже відбувся або розпочався.");
            }

            // Чи є в залі взагалі місця
            var allSeats = await _unitOfWork.Seat.GetSeatsByHallIdAsync(session.HallId);
            if (!allSeats.Any())
            {
                throw new Exception("У вибраному залі відсутня конфігурація місць.");
            }

            // для зайнятих місць
            var bookedTickets = await _unitOfWork.Ticket.GetBookedTicketsBySessionIdAsync(sessionId);

            // HashSet з ID зайнятих місць.
            // щоб пошук .Contains() працював миттєво (O(1)), 
            // а не перебирав список для кожного місця.
            var bookedSeatIds = bookedTickets.Select(t => t.SeatId).ToHashSet();

            var seatDtos = allSeats.Select(seat =>
                _mapper.MapToSeatDto(seat, session.BasePrice, bookedSeatIds)
            ).ToList();

            var sessionMapDto = _mapper.MapToSessionSeatMapDto(session, seatDtos);

            return sessionMapDto;
        }

        public async Task<int> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            var session = await _unitOfWork.Session.GetByIdWithMovieAndHallAsync(bookingDto.SessionId);
            if (session == null)
            {
                throw new KeyNotFoundException("Сеанс не знайдено.");
            }

            var existingTickets = await _unitOfWork.Ticket.GetBookedTicketsBySessionIdAsync(bookingDto.SessionId);
            if (bookingDto.SeatIds.Intersect(existingTickets.Select(t => t.SeatId)).Any())
            {
                throw new InvalidOperationException("Деякі місця вже зайняті.");
            }

            var booking = _mapper.MapCreateBookingDtoToEntity(bookingDto);

            var allHallSeats = await _unitOfWork.Seat.GetSeatsByHallIdAsync(session.HallId);
            var selectedSeats = allHallSeats.Where(s => bookingDto.SeatIds.Contains(s.SeatId));

            booking.Tickets = selectedSeats.Select(seat =>
                _mapper.MapToTicket(seat, session.SessionId, session.BasePrice)
            ).ToList();

            await _unitOfWork.Booking.AddAsync(booking);
            await _unitOfWork.SaveAsync();

            return booking.BookingId;
        }

        public async Task<decimal> GetTicketsPriceTotalAsync(int bookingId)
        {
            var booking = await _unitOfWork.Booking.GetByIdWithDetailsAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException();
            }

            return booking.Tickets.Sum(t => t.Price);
        }

        public async Task AddSnacksToBookingAsync(int bookingId, List<SelectedSnackDto> selectedSnacks)
        {
            var booking = await _unitOfWork.Booking.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Бронювання не знайдено.");
            }

            booking.SnackBookings = _snackMapper.MapSelectedSnackDtoToEntityList(selectedSnacks, bookingId);

            await _unitOfWork.Booking.UpdateWithDetailsAsync(booking);
            await _unitOfWork.SaveAsync();
        }
    }
}
