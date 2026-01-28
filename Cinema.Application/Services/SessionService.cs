using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SessionMapping _mapper;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new SessionMapping();
        }

        public async Task<IEnumerable<SessionDto>> GetScheduleAsync()
        {
             var sessions = await _unitOfWork.Sessions.GetAllWithDetailsAsync();
            if (sessions == null || !sessions.Any())
            {
                return Enumerable.Empty<SessionDto>();
            }
            return sessions.Select(s => _mapper.SessionToDto(s));
        }
    }
}
