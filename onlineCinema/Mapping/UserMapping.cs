using Riok.Mapperly.Abstractions;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using onlineCinema.ViewModels;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class UserMapping
    {
        [MapperIgnoreSource(nameof(ApplicationUser.Bookings))]
        [MapperIgnoreSource(nameof(ApplicationUser.UserName))]
        [MapperIgnoreSource(nameof(ApplicationUser.NormalizedUserName))]
        [MapperIgnoreSource(nameof(ApplicationUser.NormalizedEmail))]
        [MapperIgnoreSource(nameof(ApplicationUser.EmailConfirmed))]
        [MapperIgnoreSource(nameof(ApplicationUser.PasswordHash))]
        [MapperIgnoreSource(nameof(ApplicationUser.SecurityStamp))]
        [MapperIgnoreSource(nameof(ApplicationUser.ConcurrencyStamp))]
        [MapperIgnoreSource(nameof(ApplicationUser.PhoneNumberConfirmed))]
        [MapperIgnoreSource(nameof(ApplicationUser.TwoFactorEnabled))]
        [MapperIgnoreSource(nameof(ApplicationUser.LockoutEnd))]
        [MapperIgnoreSource(nameof(ApplicationUser.LockoutEnabled))]
        [MapperIgnoreSource(nameof(ApplicationUser.AccessFailedCount))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.FullName))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.BookingHistory))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.ReturnUrl))]
        public partial ProfileViewModel ToProfileViewModelBase(ApplicationUser user);

        public ProfileViewModel ToProfileViewModel(
            ApplicationUser user, 
            IEnumerable<BookingHistoryDto> bookings, 
            string? returnUrl = null)
        {
            var viewModel = ToProfileViewModelBase(user);
            viewModel.BookingHistory = bookings.Select(ToBookingHistoryItemViewModel).ToList();
            viewModel.ReturnUrl = returnUrl;
            return viewModel;
        }

        public BookingHistoryItemViewModel ToBookingHistoryItemViewModel(BookingHistoryDto dto)
        {
            return new BookingHistoryItemViewModel
            {
                BookingId = dto.BookingId,
                CreatedDateTime = dto.CreatedDateTime,
                TotalAmount = dto.TotalAmount,
                MovieTitle = dto.MovieTitle,
                SessionDateTime = dto.SessionDateTime,
                MoviePoster = MapMoviePoster(dto.MoviePoster),
                PaymentStatus = dto.PaymentStatus,
                HallName = dto.HallName,
                Tickets = dto.Tickets.Select(ToTicketInfoViewModel).ToList()
            };
        }

        [MapProperty(nameof(TicketInfoDto.TicketId), nameof(TicketInfoViewModel.TicketId))]
        [MapProperty(nameof(TicketInfoDto.Price), nameof(TicketInfoViewModel.Price))]
        [MapProperty(nameof(TicketInfoDto.RowNumber), nameof(TicketInfoViewModel.RowNumber))]
        [MapProperty(nameof(TicketInfoDto.SeatNumber), nameof(TicketInfoViewModel.SeatNumber))]
        [MapProperty(nameof(TicketInfoDto.SeatType), nameof(TicketInfoViewModel.SeatType))]
        public partial TicketInfoViewModel ToTicketInfoViewModel(TicketInfoDto dto);

        private string? MapMoviePoster(byte[]? posterBytes)
        {
            // Convert byte[] to base64 string if needed, or return null
            // For now, since we're returning null from BookingMapper, this will be null
            if (posterBytes == null || posterBytes.Length == 0)
            {
                return null;
            }
            
            // Convert byte[] to base64 string
            return Convert.ToBase64String(posterBytes);
        }

        [MapProperty(nameof(RegisterViewModel.Email), nameof(ApplicationUser.UserName))]
        [MapProperty(nameof(RegisterViewModel.Email), nameof(ApplicationUser.Email))]
        [MapperIgnoreSource(nameof(RegisterViewModel.Password))]
        [MapperIgnoreSource(nameof(RegisterViewModel.ConfirmPassword))]
        [MapperIgnoreSource(nameof(RegisterViewModel.ReturnUrl))]
        [MapperIgnoreTarget(nameof(ApplicationUser.Bookings))]
        [MapperIgnoreTarget(nameof(ApplicationUser.Id))]
        [MapperIgnoreTarget(nameof(ApplicationUser.NormalizedUserName))]
        [MapperIgnoreTarget(nameof(ApplicationUser.NormalizedEmail))]
        [MapperIgnoreTarget(nameof(ApplicationUser.EmailConfirmed))]
        [MapperIgnoreTarget(nameof(ApplicationUser.PasswordHash))]
        [MapperIgnoreTarget(nameof(ApplicationUser.SecurityStamp))]
        [MapperIgnoreTarget(nameof(ApplicationUser.ConcurrencyStamp))]
        [MapperIgnoreTarget(nameof(ApplicationUser.PhoneNumberConfirmed))]
        [MapperIgnoreTarget(nameof(ApplicationUser.TwoFactorEnabled))]
        [MapperIgnoreTarget(nameof(ApplicationUser.LockoutEnd))]
        [MapperIgnoreTarget(nameof(ApplicationUser.LockoutEnabled))]
        [MapperIgnoreTarget(nameof(ApplicationUser.AccessFailedCount))]
        public partial ApplicationUser ToApplicationUser(RegisterViewModel model);

        [MapProperty(nameof(ProfileViewModel.PhoneNumber), nameof(ApplicationUser.PhoneNumber))]
        [MapProperty(nameof(ProfileViewModel.FirstName), nameof(ApplicationUser.FirstName))]
        [MapProperty(nameof(ProfileViewModel.LastName), nameof(ApplicationUser.LastName))]
        [MapProperty(nameof(ProfileViewModel.MiddleName), nameof(ApplicationUser.MiddleName))]
        [MapProperty(nameof(ProfileViewModel.DateOfBirth), nameof(ApplicationUser.DateOfBirth))]
        [MapperIgnoreSource(nameof(ProfileViewModel.Id))]
        [MapperIgnoreSource(nameof(ProfileViewModel.Email))]
        [MapperIgnoreSource(nameof(ProfileViewModel.FullName))]
        [MapperIgnoreTarget(nameof(ApplicationUser.Id))]
        [MapperIgnoreTarget(nameof(ApplicationUser.Email))]
        [MapperIgnoreTarget(nameof(ApplicationUser.Bookings))]
        [MapperIgnoreTarget(nameof(ApplicationUser.UserName))]
        [MapperIgnoreTarget(nameof(ApplicationUser.NormalizedUserName))]
        [MapperIgnoreTarget(nameof(ApplicationUser.NormalizedEmail))]
        [MapperIgnoreTarget(nameof(ApplicationUser.EmailConfirmed))]
        [MapperIgnoreTarget(nameof(ApplicationUser.PasswordHash))]
        [MapperIgnoreTarget(nameof(ApplicationUser.SecurityStamp))]
        [MapperIgnoreTarget(nameof(ApplicationUser.ConcurrencyStamp))]
        [MapperIgnoreTarget(nameof(ApplicationUser.PhoneNumberConfirmed))]
        [MapperIgnoreTarget(nameof(ApplicationUser.TwoFactorEnabled))]
        [MapperIgnoreTarget(nameof(ApplicationUser.LockoutEnd))]
        [MapperIgnoreTarget(nameof(ApplicationUser.LockoutEnabled))]
        [MapperIgnoreTarget(nameof(ApplicationUser.AccessFailedCount))]
        public partial void UpdateApplicationUser(ProfileViewModel model, ApplicationUser user);
    }
}

