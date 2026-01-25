using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Services
{
    public class HallService : IHallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HallMapper _mapper;

        public HallService(IUnitOfWork unitOfWork, HallMapper hallMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = hallMapper;
        }

        public async Task<HallDto?> CreateHallAsync(HallDto hallDto, List<int> selectedFeatureIds)
        {
            if (hallDto.RowCount > 255 || hallDto.SeatInRowCount > 255)
            {
                throw new ArgumentException("Кількість рядів та місць у ряді не може перевищувати 255.");
            }

            var hallEntity = _mapper.MapToEntity(hallDto);
            hallEntity.CinemaId = 3; // Temporary hardcoded cinema ID

            await _unitOfWork.Hall.AddAsync(hallEntity);
            await _unitOfWork.SaveAsync();

            if(selectedFeatureIds != null && selectedFeatureIds.Count > 0)
            {
                await _unitOfWork.Hall.UpdateWithFeaturesAsync(hallEntity, selectedFeatureIds);
            }

            return await GetHallByIdAsync(hallEntity.HallId);
        }

        public async Task<bool> DeleteHallAsync(int id)
        {
            await _unitOfWork.Hall.DeleteAsync(id);

            return true;
        }

        public async Task<HallDto?> EditHallAsync(HallDto hallDto, List<int> selectedFeatureIds)
        {
            var exists = await _unitOfWork.Hall.GetByIdWithStatsAsync(hallDto.Id);

            if(exists == null)
            {
                throw new KeyNotFoundException($"Зал з id {hallDto.Id} не знайдено.");
            }

            var hallEntity = _mapper.MapToEntity(hallDto);
            hallEntity.HallId = hallDto.Id;

            await _unitOfWork.Hall.UpdateWithFeaturesAsync(hallEntity, selectedFeatureIds);

            return await GetHallByIdAsync(hallDto.Id);
        }

        public async Task<IEnumerable<HallDto>> GetAllHallsAsync()
        {
            return await _unitOfWork.Hall.GetAllWithStatsAsync();
        }

        public async Task<HallDto?> GetHallByIdAsync(int id)
        {
            var hallDto = await _unitOfWork.Hall.GetByIdWithStatsAsync(id);

            if(hallDto == null)
            {
                throw new KeyNotFoundException($"Зал з id {id} не знайдено.");
            }

            return hallDto;
        }

        public async Task<IEnumerable<Feature>> GetAllFeaturesAsync()
        {
            return await _unitOfWork.Feature.GetAllAsync();
        }
    }
}
