using System.ComponentModel.DataAnnotations;

namespace onlineCinema.ViewModels
{
    public class SessionCreateViewModel
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int HallId { get; set; }
        [Required]
        public DateTime ShowingDateTime { get; set; }
        [Required]
        [Range(1, 10000)]
        public decimal BasePrice { get; set; }
    }
}
