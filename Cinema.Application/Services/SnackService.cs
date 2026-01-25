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
    public class SnackService : ISnackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SnackMapper _mapper;

        public SnackService(IUnitOfWork unitOfWork, SnackMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SnackDto>> GetAllSnacksAsync()
        {
            var snacks = await _unitOfWork.Snack.GetAllAsync();

            return _mapper.MapSnacksToDtos(snacks);
        }
    }
}
