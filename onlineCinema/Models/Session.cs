using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Models
{
	public class Session
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please indicate the start time of the session")]
		[Display(Name = "Start time")]
		public DateTime StartTime { get; set; }
		[Required(ErrorMessage = "Please indicate the price of a standard ticket")]
		[Column(TypeName = "decimal(18,2)")]
		[Display(Name = "Ticket price")]
		public decimal TicketPrice { get; set; }

		// Зв'язок із залом
		[Required(ErrorMessage = "Please specify the hall")]
		[Display(Name = "Hall")]
		public int HallId { get; set; }
		[ForeignKey("HallId")]
		public Hall? Hall { get; set; }

		// Зв'язок з фільмом
		[Required(ErrorMessage = "Please specify the movie")]
		[Display(Name = "Movie")]
		public int MovieId { get; set; }
		[ForeignKey("MovieId")]
		public Movie? Movie { get; set; }

		public ICollection<Ticket>? Tickets { get; set; }
	}
}
