using System.ComponentModel.DataAnnotations;

namespace onlineCinema.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ім'я є обов'язковим")]
        [Display(Name = "Ім'я")]
        [StringLength(50, ErrorMessage = "Ім'я не може перевищувати 50 символів")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Прізвище є обов'язковим")]
        [Display(Name = "Прізвище")]
        [StringLength(50, ErrorMessage = "Прізвище не може перевищувати 50 символів")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "По батькові")]
        [StringLength(50, ErrorMessage = "По батькові не може перевищувати 50 символів")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Email є обов'язковим")]
        [EmailAddress(ErrorMessage = "Невірний формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Номер телефону є обов'язковим")]
        [Phone(ErrorMessage = "Невірний формат телефону")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Повне ім'я")]
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

        public List<BookingHistoryItemViewModel> BookingHistory { get; set; } = new();
    }
}

