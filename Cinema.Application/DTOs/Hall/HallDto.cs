using onlineCinema.Application.DTOs.Session;

namespace onlineCinema.Application.DTOs.Hall
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
        public List<string> FeatureDescriptions { get; set; } = new();
        
        public List<SessionSeatMapDto> Sessions { get; set; } = new();

        public int VipRowCount { get; set; }
        public float VipCoefficient { get; set; }
    }
}
