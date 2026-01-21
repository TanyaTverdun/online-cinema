using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace onlineCinema.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName {  get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
