using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class CreateBookingDto
    {
        public int SessionId { get; set; }
        public List<int> SeatIds { get; set; } = new List<int>();
        public string UserId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }
}
