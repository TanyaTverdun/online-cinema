using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models;

public class FeatureViewModel
{
    public int FeatureId { get; set; }

    [Display(Name = "Назва характеристики")]
    public string FeatureName { get; set; } = string.Empty;

    [Display(Name = "Опис")]
    public string FeatureDescription { get; set; } = string.Empty;
}