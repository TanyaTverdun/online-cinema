using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.DTOs;

namespace onlineCinema.Areas.Admin.Models
{
    public class SessionViewModel
    {
        public int? Id { get; set; }
        public int? MovieId { get; set; }
        public int? HallId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public DateTime? ShowingDateTime { get; set; }
        public decimal? BasePrice { get; set; }
        public bool GenerateForWeek { get; set; }
        public IEnumerable<MovieDto> MoviesList { get; set; } = new List<MovieDto>();
        public IEnumerable<HallDto> HallsList { get; set; } = new List<HallDto>();
    }
}
