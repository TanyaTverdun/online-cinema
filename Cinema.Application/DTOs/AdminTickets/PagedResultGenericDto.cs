using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.AdminTickets
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public bool HasNextPage { get; set; }
        public int? LastId => Items.Count > 0
            ? (int?)typeof(T).GetProperty("TicketId")?.GetValue(Items.Last())
            : null;
    }
}
