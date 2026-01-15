using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace onlineCinema.Models
{
    public enum TicketStatus
    {
        Reserved = 0,
        Paid = 1,
        Cancelled = 2
    }

    [Index(nameof(SessionId), nameof(RowNumber), nameof(SeatNumber), IsUnique = true)]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        // Seat info.
        [Required(ErrorMessage = "Please specify the row number")]
        [Display(Name = "Row number")]
        public int RowNumber { get; set; }

        [Required(ErrorMessage = "Please specify the seat number")]
        [Display(Name = "Seat number")]
        public int SeatNumber { get; set; }

        // Purchase info.
        [Display(Name = "Purchase date")]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Price paid")]
        public decimal PricePaid { get; set; }

        // Ticket status.
        [Required]
        [Display(Name = "Ticket status")]
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Reserved;

        // Session.
        [Required]
        [Display(Name = "Session")]
        public int SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session? Session { get; set; }

        // ASP.NET Identity User.
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser? User { get; set; }
    }
}
