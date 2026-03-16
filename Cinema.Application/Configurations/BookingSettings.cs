namespace onlineCinema.Application.Configurations
{
    public class BookingSettings
    {
        public int BookingLockMinutes { get; set; }
        public int BookingLockSeconds { get; set; }
        public int MinMinutesBeforeSessionForRefund { get; set; } = 60;
        public int HistoryPageSize { get; set; } = 5;
    }
}
