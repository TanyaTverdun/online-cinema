using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace onlineCinema.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }
        public new string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
