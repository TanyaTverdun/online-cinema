using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using onlineCinema.Application.Configurations;
using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AdminTicketMapping _ticketMapper;
        private readonly StatisticsSettings _settings;

        public TicketService(IUnitOfWork unitOfWork, 
            AdminTicketMapping ticketMapper, 
            IOptions<StatisticsSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _ticketMapper = ticketMapper;
            _settings = settings.Value;
        }

        public async Task<PagedResult<TicketAdminDto>> GetTicketsForAdminAsync(
                int? lastId,
                string? email,
                string? movie,
                DateTime? date)
        {
            var pageSize = _settings.AdminTicketsPageSize;
            var (entities, totalCount) = await _unitOfWork.Ticket
                .GetTicketsSeekAsync(lastId, pageSize, email, movie, date);

            return _ticketMapper.MapToPagedResult(entities, totalCount, pageSize);
        }
    }
}
