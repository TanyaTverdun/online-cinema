using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class PaginatedListDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedListDto(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }
    }
}