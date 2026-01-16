using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace onlineCinema.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public override string? Email { get; set; }

        // UserName
        [Required]
        [Display(Name = "Username")]
        public override string? UserName { get; set; }

        // Navigation property
        // One user -> many tickets
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
