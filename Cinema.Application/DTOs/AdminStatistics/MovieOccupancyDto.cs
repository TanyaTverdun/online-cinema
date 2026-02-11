using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.AdminStatistics
{
    public class MovieOccupancyDto
    {
        public string MovieTitle { get; set; } = string.Empty;
        public double OccupancyPercentage { get; set; }
    }
}
