using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace onlineCinema.ViewModels
{
    public class SessionCreateViewModel
    {
        public int? MovieId { get; set; }
        public int? HallId { get; set; }
        public DateTime? ShowingDateTime { get; set; }
        public decimal? BasePrice { get; set; }
        public bool GenerateForWeek { get; set; }
        public IEnumerable<SelectListItem> AvailableMovies { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> AvailableHalls { get; set; } = new List<SelectListItem>();
    }
}
