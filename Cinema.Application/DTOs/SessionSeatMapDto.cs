using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class SessionSeatMapDto
    {
        public int SessionId { get; set; }
        public string MovieTitle { get; set; }
        public int HallNumber { get; set; }
        public DateTime ShowingDate { get; set; }
        public decimal BasePrice { get; set; }
        public List<SeatDto> Seats { get; set; } = new();
    }
}
