namespace onlineCinema.Application.Configurations
{
    public class HallSettings
    {
        public int MaxRowCount { get; set; } = 255;
        public int MaxSeatInRowCount { get; set; } = 255;
        public int FutureSessionDays { get; set; } = 3;
    }
}
