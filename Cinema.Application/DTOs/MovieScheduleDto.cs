using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class MovieScheduleDto
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int Runtime { get; set; }
        public string PosterUrl { get; set; }
        public List<DailyScheduleDto> Schedule { get; set; } = new();
    }
}
