using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Configurations
{
    public class StatisticsSettings
    {
        public int SnacksCount { get; set; } = 5;
        public int MoviesCount { get; set; } = 5;
        public int OccupancyCount { get; set; } = 5;
        public int AdminPageSize { get; set; }
    }
}
