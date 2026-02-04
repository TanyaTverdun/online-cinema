using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly AdminStatisticsMapper _mapper;

        public DashboardController(IStatisticsService statisticsService, AdminStatisticsMapper mapper)
        {
            _statisticsService = statisticsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var statsDto = await _statisticsService.GetAdminStatisticsAsync();
            var viewModel = _mapper.MapToViewModel(statsDto);

            return View(viewModel);
        }
    }
}
