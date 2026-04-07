using Riok.Mapperly.Abstractions;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using onlineCinema.ViewModels;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class UserMapping
    {
        [MapperIgnoreSource(nameof(DanceMember.Bookings))]
        [MapperIgnoreSource(nameof(DanceMember.UserName))]
        [MapperIgnoreSource(nameof(DanceMember.NormalizedUserName))]
        [MapperIgnoreSource(nameof(DanceMember.NormalizedEmail))]
        [MapperIgnoreSource(nameof(DanceMember.EmailConfirmed))]
        [MapperIgnoreSource(nameof(DanceMember.PasswordHash))]
        [MapperIgnoreSource(nameof(DanceMember.SecurityStamp))]
        [MapperIgnoreSource(nameof(DanceMember.ConcurrencyStamp))]
        [MapperIgnoreSource(nameof(DanceMember.PhoneNumberConfirmed))]
        [MapperIgnoreSource(nameof(DanceMember.TwoFactorEnabled))]
        [MapperIgnoreSource(nameof(DanceMember.LockoutEnd))]
        [MapperIgnoreSource(nameof(DanceMember.LockoutEnabled))]
        [MapperIgnoreSource(nameof(DanceMember.AccessFailedCount))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.FullName))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.BookingHistory))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.ReturnUrl))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.NewPassword))]
        [MapperIgnoreTarget(nameof(ProfileViewModel.ConfirmPassword))]
        public partial ProfileViewModel ToProfileViewModelBase(
            DanceMember user);

        public ProfileViewModel ToProfileViewModel(
            DanceMember user,
            PagedResultDto<BookingHistoryDto> bookings,
            string? returnUrl = null)
        {
            var viewModel = ToProfileViewModelBase(user);

            viewModel.BookingHistory = 
                new PagedResultDto<BookingHistoryItemViewModel>
            {
                Items = 
                bookings.Items
                .Select(ToBookingHistoryItemViewModel)
                .ToList(),
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

        public BookingHistoryItemViewModel ToBookingHistoryItemViewModel(
            BookingHistoryDto dto)
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

        [MapProperty(nameof(AttendanceLogInfoDto.TicketId),
            nameof(TicketInfoViewModel.TicketId))]
        [MapProperty(nameof(AttendanceLogInfoDto.Price),
            nameof(TicketInfoViewModel.Price))]
        [MapProperty(nameof(AttendanceLogInfoDto.RowNumber), 
            nameof(TicketInfoViewModel.RowNumber))]
        [MapProperty(nameof(AttendanceLogInfoDto.SeatNumber),
            nameof(TicketInfoViewModel.SeatNumber))]
        [MapProperty(nameof(AttendanceLogInfoDto.SeatType), 
            nameof(TicketInfoViewModel.SeatType))]
        public partial TicketInfoViewModel ToTicketInfoViewModel(
            AttendanceLogInfoDto dto);

        private string? MapMoviePoster(byte[]? posterBytes)
        {
            if (posterBytes == null || posterBytes.Length == 0)
            {
                return null;
            }

            return Convert.ToBase64String(posterBytes);
        }

        [MapProperty(nameof(RegisterViewModel.Email),
            nameof(DanceMember.UserName))]
        [MapProperty(nameof(RegisterViewModel.Email),
            nameof(DanceMember.Email))]
        [MapperIgnoreSource(nameof(RegisterViewModel.Password))]
        [MapperIgnoreSource(nameof(RegisterViewModel.ConfirmPassword))]
        [MapperIgnoreSource(nameof(RegisterViewModel.ReturnUrl))]
        [MapperIgnoreTarget(nameof(DanceMember.Bookings))]
        [MapperIgnoreTarget(nameof(DanceMember.Id))]
        [MapperIgnoreTarget(nameof(DanceMember.NormalizedUserName))]
        [MapperIgnoreTarget(nameof(DanceMember.NormalizedEmail))]
        [MapperIgnoreTarget(nameof(DanceMember.EmailConfirmed))]
        [MapperIgnoreTarget(nameof(DanceMember.PasswordHash))]
        [MapperIgnoreTarget(nameof(DanceMember.SecurityStamp))]
        [MapperIgnoreTarget(nameof(DanceMember.ConcurrencyStamp))]
        [MapperIgnoreTarget(nameof(DanceMember.PhoneNumberConfirmed))]
        [MapperIgnoreTarget(nameof(DanceMember.TwoFactorEnabled))]
        [MapperIgnoreTarget(nameof(DanceMember.LockoutEnd))]
        [MapperIgnoreTarget(nameof(DanceMember.LockoutEnabled))]
        [MapperIgnoreTarget(nameof(DanceMember.AccessFailedCount))]
        private partial DanceMember ToApplicationUserBase(
            RegisterViewModel model);

        public DanceMember ToApplicationUser(RegisterViewModel model)
        {
            var user = ToApplicationUserBase(model);
            user.EmailConfirmed = true;
            user.MiddleName ??= string.Empty;
            return user;
        }

        [MapProperty(nameof(ProfileViewModel.PhoneNumber),
            nameof(DanceMember.PhoneNumber))]
        [MapProperty(nameof(ProfileViewModel.FirstName),
            nameof(DanceMember.FirstName))]
        [MapProperty(nameof(ProfileViewModel.LastName),
            nameof(DanceMember.LastName))]
        [MapProperty(nameof(ProfileViewModel.MiddleName), 
            nameof(DanceMember.MiddleName))]
        [MapProperty(nameof(ProfileViewModel.DateOfBirth),
            nameof(DanceMember.DateOfBirth))]
        [MapperIgnoreSource(nameof(ProfileViewModel.Id))]
        [MapperIgnoreSource(nameof(ProfileViewModel.Email))]
        [MapperIgnoreSource(nameof(ProfileViewModel.FullName))]
        [MapperIgnoreSource(nameof(ProfileViewModel.NewPassword))]
        [MapperIgnoreSource(nameof(ProfileViewModel.ConfirmPassword))]
        [MapperIgnoreTarget(nameof(DanceMember.Id))]
        [MapperIgnoreTarget(nameof(DanceMember.Email))]
        [MapperIgnoreTarget(nameof(DanceMember.Bookings))]
        [MapperIgnoreTarget(nameof(DanceMember.UserName))]
        [MapperIgnoreTarget(nameof(DanceMember.NormalizedUserName))]
        [MapperIgnoreTarget(nameof(DanceMember.NormalizedEmail))]
        [MapperIgnoreTarget(nameof(DanceMember.EmailConfirmed))]
        [MapperIgnoreTarget(nameof(DanceMember.PasswordHash))]
        [MapperIgnoreTarget(nameof(DanceMember.SecurityStamp))]
        [MapperIgnoreTarget(nameof(DanceMember.ConcurrencyStamp))]
        [MapperIgnoreTarget(nameof(DanceMember.PhoneNumberConfirmed))]
        [MapperIgnoreTarget(nameof(DanceMember.TwoFactorEnabled))]
        [MapperIgnoreTarget(nameof(DanceMember.LockoutEnd))]
        [MapperIgnoreTarget(nameof(DanceMember.LockoutEnabled))]
        [MapperIgnoreTarget(nameof(DanceMember.AccessFailedCount))]
        private partial void UpdateApplicationUserBase(
            ProfileViewModel model,
            DanceMember user);

        public void UpdateApplicationUser(
            ProfileViewModel model,
            DanceMember user)
        {
            UpdateApplicationUserBase(model, user);

            user.MiddleName = string.IsNullOrWhiteSpace(user.MiddleName)
                ? null 
                : user.MiddleName.Trim();
        }
    }
}