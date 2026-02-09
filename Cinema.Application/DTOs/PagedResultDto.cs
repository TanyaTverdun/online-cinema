using System;
using System.Collections.Generic;


namespace onlineCinema.Application.DTOs
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public int? LastId { get; set; }
        public int? FirstId { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}