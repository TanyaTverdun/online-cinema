using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.AdminTickets
{
    public class PaymentAdminDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime MovieSessionDateTime { get; set; }
        public PaymentStatus Status { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
        public int TicketCount { get; set; }

        public List<string> TicketsInfo { get; set; } = new();
        public List<string> SnacksInfo { get; set; } = new();
    }
}
