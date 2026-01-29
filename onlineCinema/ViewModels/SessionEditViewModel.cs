using Microsoft.AspNetCore.Mvc.Rendering;

namespace onlineCinema.ViewModels
{
    public class SessionEditViewModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public decimal BasePrice { get; set; }
   
    }
}
