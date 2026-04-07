using System;
using System.Collections.Generic;

namespace onlineCinema.Application.DTOs;

public class PerformanceScheduleDto // Було MovieScheduleDto
{
    public int PerformanceId { get; set; } // Було MovieId
    public string PerformanceTitle { get; set; } = string.Empty; // Було MovieTitle
    public int DurationMinutes { get; set; } // Було Runtime (тривалість виступу/заняття)
    public string ImageUrl { get; set; } = string.Empty; // Було PosterUrl
    public List<DailyScheduleDto> Schedule { get; set; } = new();
}