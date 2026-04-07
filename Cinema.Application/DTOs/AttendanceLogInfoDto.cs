namespace onlineCinema.Application.DTOs
{
    public class AttendanceLogInfoDto
    {
        public int AttendanceId { get; set; } // Було TicketId
        public int ClassId { get; set; }      // ID заняття
        public int? ItemId { get; set; }      // ID костюма/інвентаря (якщо є)
        public decimal ActualPrice { get; set; } // Твоє поле actual_price з БД

        // Додаткові поля для зручності виводу
        public string PerformanceTitle { get; set; } = string.Empty;
        public DateTime StartDatetime { get; set; }
    }
}

