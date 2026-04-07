using System;

namespace onlineCinema.Application.DTOs
{
    public class DanceClassDto
    {
        public int Id { get; set; }
        public int PerformanceId { get; set; }
        public string PerformanceTitle { get; set; } = string.Empty;
        public int DanceHallId { get; set; }
        public string HallName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
    }
}