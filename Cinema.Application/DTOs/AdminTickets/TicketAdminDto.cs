using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.AdminTickets
{
    public class TicketAdminDto
    {
        public int TicketId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime ShowingDateTime { get; set; }
        public int HallNumber { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
