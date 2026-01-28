using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Interfaces;
using onlineCinema.ViewModels;
using onlineCinema.Application.Services.Interfaces;

public class SessionController : Controller
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Index()
    {
        var sessionDtos = await _sessionService.GetScheduleAsync();
        var viewModel = sessionDtos.Select(dto => new SessionScheduleViewModel
        {
            SessionId = dto.SessionId,
            MovieTitle = dto.MovieTitle ?? "Назва відсутня",
            MoviePoster = dto.MoviePosterImage ?? "/images/default-poster.jpg",
            StartTime = dto.ShowingDatetime.ToString("HH:mm"),
            StartDate = dto.ShowingDatetime.ToString("dd MMMM"),
            HallName = $"Зал №{dto.HallName}",
            Price = $"{dto.BasePrice:F2} грн"
        }).ToList();

        return View(viewModel);
    }
}