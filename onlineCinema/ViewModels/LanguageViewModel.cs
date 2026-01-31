using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Models.ViewModels;

public class LanguageViewModel
{
    public int LanguageId { get; set; }

    [Required(ErrorMessage = "Назва мови обов'язкова")]
    [Display(Name = "Мова")]
    public string LanguageName { get; set; } = string.Empty;
}