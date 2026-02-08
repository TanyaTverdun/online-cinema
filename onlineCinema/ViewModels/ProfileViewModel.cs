using onlineCinema.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace onlineCinema.ViewModels
{
    public class ProfileViewModel
    {
        public string? Id { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
        public List<BookingHistoryItemViewModel> BookingHistory { get; set; } = new();
        public string? ReturnUrl { get; set; }
        public PaginatedListDto<BookingHistoryItemViewModel>? PaginatedBookingHistory { get; set; }
    }
}

