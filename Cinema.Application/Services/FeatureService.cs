using onlineCinema.Application.DTOs;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FeatureMapping _mapper;

        public FeatureService(IUnitOfWork unitOfWork, FeatureMapping mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeatureDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Feature.GetAllAsync();
            return _mapper.MapToDtoList(entities);
        }

        public async Task<FeatureDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Feature.GetByIdAsync(id);
            return entity != null ? _mapper.MapToDto(entity) : null;
        }

        public async Task CreateAsync(FeatureDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            await _unitOfWork.Feature.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(FeatureDto dto)
        {
            var entity = await _unitOfWork.Feature.GetByIdAsync(dto.FeatureId);
            if (entity != null)
            {
                _mapper.UpdateEntityFromDto(dto, entity);
                _unitOfWork.Feature.Update(entity);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Feature.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.Feature.Remove(entity);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}