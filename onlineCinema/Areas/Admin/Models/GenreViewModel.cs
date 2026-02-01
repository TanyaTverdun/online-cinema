using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models;

public class GenreViewModel
{
    public int GenreId { get; set; }

    [Required(ErrorMessage = "Назва жанру обов'язкова")]
    [Display(Name = "Назва жанру")]
    public string GenreName { get; set; } = string.Empty;
}