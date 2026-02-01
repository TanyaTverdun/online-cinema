using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models;

public class CastMemberViewModel
{
    public int CastId { get; set; }

    [Display(Name = "Ім'я")]
    public string CastFirstName { get; set; } = string.Empty;

    [Display(Name = "Прізвище")]
    public string CastLastName { get; set; } = string.Empty;

    [Display(Name = "По батькові")]
    public string CastMiddleName { get; set; } = string.Empty;
}