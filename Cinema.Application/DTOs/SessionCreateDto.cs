using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
        public class SessionCreateDto
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public decimal BasePrice { get; set; }
        public bool GenerateForWeek { get; set; }
    }
}
