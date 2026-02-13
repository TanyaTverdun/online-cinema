using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class DailyScheduleDto
    {
        public DateTime Date { get; set; }
        public List<SessionScheduleDto> Sessions { get; set; } = new();
    }
}
