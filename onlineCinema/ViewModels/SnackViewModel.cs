using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Models.ViewModels;

public class SnackViewModel
{
    public int SnackId { get; set; }

    [Required(ErrorMessage = "Назва обов'язкова")]
    [Display(Name = "Назва снеку")]
    public string SnackName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ціна обов'язкова")]
    [Range(0.01, 10000, ErrorMessage = "Ціна має бути більше 0")]
    [Display(Name = "Ціна (грн)")]
    public decimal Price { get; set; }
}