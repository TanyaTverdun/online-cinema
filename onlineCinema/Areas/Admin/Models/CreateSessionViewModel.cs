using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models
{
    public class CreateSessionViewModel
    {
        [Required]
        public bool GenerateForWeek { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int HallId { get; set; }

        [Required]
        [Display(Name = "Showing Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime ShowingDateTime { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal BasePrice { get; set; }

        public List<SelectListItem> Movies { get; set; } = new();
        public List<SelectListItem> Halls { get; set; } = new();
    }
}