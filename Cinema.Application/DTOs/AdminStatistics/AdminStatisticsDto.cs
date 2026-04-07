using System.Collections.Generic;

namespace onlineCinema.Application.DTOs.AdminStatistics;

public class AdminStatisticsDto // Залишаємо назву класу, вона універсальна
{
    // Заповнюваність виступів/занять
    public List<OccupancyDto> PerformanceOccupancy { get; set; } = new();

    // Найпопулярніші та найменш популярні виступи (курси)
    public List<TopItemDto> MostPopularPerformances { get; set; } = new();
    public List<TopItemDto> LeastPopularPerformances { get; set; } = new();

    // Статистика по товарах (мерчу) студії
    public List<TopItemDto> MostPopularMerch { get; set; } = new();
    public List<TopItemDto> LeastPopularMerch { get; set; } = new();
}