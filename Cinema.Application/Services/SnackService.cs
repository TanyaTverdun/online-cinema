using onlineCinema.Application.DTOs;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;

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

        public async Task<IEnumerable<SnackDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Snack.GetAllAsync();
            return _mapper.MapSnacksToDtos(entities);
        }

        public async Task<SnackDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Snack.GetByIdAsync(id);
            return entity != null ? _mapper.MapSnackToDto(entity) : null;
        }

        public async Task CreateAsync(SnackDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            await _unitOfWork.Snack.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(SnackDto dto)
        {
            var entity = await _unitOfWork.Snack.GetByIdAsync(dto.SnackId);
            if (entity != null)
            {
                _mapper.UpdateEntityFromDto(dto, entity);
                _unitOfWork.Snack.Update(entity);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Snack.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.Snack.Remove(entity);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}