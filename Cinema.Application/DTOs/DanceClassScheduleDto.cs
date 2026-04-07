using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class DanceClassScheduleDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string PerformanceTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
