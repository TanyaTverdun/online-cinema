using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Models.ViewModels;

public class CastMemberViewModel
{
    public int CastId { get; set; }

    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Ім'я обов'язкове")]
    public string CastFirstName { get; set; } = string.Empty;

    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Прізвище обов'язкове")]
    public string CastLastName { get; set; } = string.Empty;

    [Display(Name = "По батькові")]
    public string CastMiddleName { get; set; } = string.Empty;
}