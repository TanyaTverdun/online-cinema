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
        [MapperIgnoreTarget(nameof(ProfileViewModel.NewPassword))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.ConfirmPassword))]
        public partial ProfileViewModel ToProfileViewModelBase(ApplicationUser user);

        public ProfileViewModel ToProfileViewModel(
            ApplicationUser user,
            PagedResultDto<BookingHistoryDto> bookings,
            string? returnUrl = null)
        {
            var viewModel = ToProfileViewModelBase(user);

            viewModel.BookingHistory = new PagedResultDto<BookingHistoryItemViewModel>
            {
                Items = bookings.Items.Select(ToBookingHistoryItemViewModel).ToList(),
                TotalCount = bookings.TotalCount,
                PageSize = bookings.PageSize,

                HasNextPage = bookings.HasNextPage,
                HasPreviousPage = bookings.HasPreviousPage,
                LastId = bookings.LastId,
                FirstId = bookings.FirstId
            };

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
                Tickets = dto.Tickets.Select(ToTicketInfoViewModel).ToList(),

                Snacks = dto.Snacks.Select(s => new SnackInfoViewModel
                {
                    Name = s.Name,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    TotalPrice = s.TotalPrice
                }).ToList(),

                CanRefund = dto.CanRefund
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
            if (posterBytes == null || posterBytes.Length == 0)
            {
                return null;
            }

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
        private partial ApplicationUser ToApplicationUserBase(RegisterViewModel model);

        public ApplicationUser ToApplicationUser(RegisterViewModel model)
        {
            var user = ToApplicationUserBase(model);
            user.EmailConfirmed = true;
            user.MiddleName ??= string.Empty;
            return user;
        }

        [MapProperty(nameof(ProfileViewModel.PhoneNumber), nameof(ApplicationUser.PhoneNumber))]
        [MapProperty(nameof(ProfileViewModel.FirstName), nameof(ApplicationUser.FirstName))]
        [MapProperty(nameof(ProfileViewModel.LastName), nameof(ApplicationUser.LastName))]
        [MapProperty(nameof(ProfileViewModel.MiddleName), nameof(ApplicationUser.MiddleName))]
        [MapProperty(nameof(ProfileViewModel.DateOfBirth), nameof(ApplicationUser.DateOfBirth))]
        [MapperIgnoreSource(nameof(ProfileViewModel.Id))]
        [MapperIgnoreSource(nameof(ProfileViewModel.Email))]
        [MapperIgnoreSource(nameof(ProfileViewModel.FullName))]
        [MapperIgnoreSource(nameof(ProfileViewModel.NewPassword))]
        [MapperIgnoreSource(nameof(ProfileViewModel.ConfirmPassword))]
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
        private partial void UpdateApplicationUserBase(ProfileViewModel model, ApplicationUser user);

        public void UpdateApplicationUser(ProfileViewModel model, ApplicationUser user)
        {
            UpdateApplicationUserBase(model, user);

            user.MiddleName = string.IsNullOrWhiteSpace(user.MiddleName) ? null : user.MiddleName.Trim();
        }
    }
}