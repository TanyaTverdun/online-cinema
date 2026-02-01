using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models;

public class SnackViewModel
{
    public int SnackId { get; set; }

    [Display(Name = "Назва снеку")]
    public string SnackName { get; set; } = string.Empty;

    [Display(Name = "Ціна (грн)")]
    public decimal Price { get; set; }
}