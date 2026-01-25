using Riok.Mapperly.Abstractions;
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
        public partial ProfileViewModel ToProfileViewModel(ApplicationUser user);

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

