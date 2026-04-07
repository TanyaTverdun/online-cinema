using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class CreateBookingDto
    {
        public int DanceClassId { get; set; }
        public List<int> AttendanceLogIds { get; set; } = new List<int>();

        public string UserId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        // Дата народження (важливо для перевірки вікової категорії групи)
        public DateTime? UserDateOfBirth { get; set; }
    }
}
