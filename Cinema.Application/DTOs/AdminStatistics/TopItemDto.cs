using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.AdminStatistics
{
    public class TopItemDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int Count { get; set; }
    }
}
