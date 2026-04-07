using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class DanceClassMapDto
    {
        public int ClassId { get; set; }
        public DateTime StartDateTime { get; set; }

        public string PerformanceTitle { get; set; } = string.Empty;
        public string PerformanceCover { get; set; } = string.Empty;

      
        public int HallId { get; set; }
        public int HallNumber { get; set; }
        public double AreaSize { get; set; }
        public int MaxPeople { get; set; }

        
        public List<InventoryDto> AvailableInventory { get; set; } = new List<InventoryDto>();

    
        public decimal BasePrice { get; set; }
    }

    public class InventoryDto
    {
        public int ItemId { get; set; }
        public string Category { get; set; } = string.Empty; 
        public int IdentifierNumber { get; set; }
        public bool IsOccupied { get; set; } 
    }
}
