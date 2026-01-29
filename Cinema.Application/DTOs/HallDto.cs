using onlineCinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class HallDto
    {
        public int Id { get; set; }
        public int HallNumber { get; set; }
        public int RowCount { get; set; }
        public int SeatInRowCount { get; set; }

        public int TotalSeats => RowCount * SeatInRowCount;
        public List<string> FeatureNames { get; set; } = new();
        public List<int> FeatureIds { get; set; } = new();

        public List<SessionSeatMapDto> Sessions { get; set; } = new();
    }
}
