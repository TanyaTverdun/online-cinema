namespace onlineCinema.Application.DTOs
{
    public class BookingHistoryDto
    {
        public int BookingId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string PerformanceTitle { get; set; } = string.Empty;
        public DateTime ClassDateTime { get; set; }
        public byte[]? PerformanceImage { get; set; } // Виправив назву з Imager на Image
        public string HallName { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;

        // Зверни увагу: прибрав 's' у назвах типів, щоб вони збігалися з файлами
        public List<AttendanceLogInfoDto> AttendanceLogs { get; set; } = new();
        public bool CanRefund { get; set; }
        public List<MerchInfoDto> MerchOrders { get; set; } = new();
    }
}

