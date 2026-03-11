namespace onlineCinema.Application.DTOs.Ticket
{
    public class TicketInfoDto
    {
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public byte RowNumber { get; set; }
        public byte SeatNumber { get; set; }
        public string SeatType { get; set; } = string.Empty;
    }
}

