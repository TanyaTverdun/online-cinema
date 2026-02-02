using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
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
        private readonly SeatMapper _seatMapper;

        public HallService(IUnitOfWork unitOfWork, HallMapper hallMapper, SeatMapper seatMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = hallMapper;
            _seatMapper = seatMapper;
        }

        public async Task<HallDto?> CreateHallAsync(HallDto hallDto)
        {
            if (hallDto.RowCount > 255 || hallDto.SeatInRowCount > 255)
            {
                throw new ArgumentException("Кількість рядів та місць у ряді не може перевищувати 255.");
            }

            var hallEntity = _mapper.MapToEntity(hallDto);

            hallEntity.CinemaId = 1; // hardcoded cinema ID FROM INITIALIZER

            await _unitOfWork.Hall.AddAsync(hallEntity);
            await _unitOfWork.SaveAsync();

            await GenerateSeatsForHall(hallEntity, hallDto);

            if (hallDto.FeatureIds != null && hallDto.FeatureIds.Any())
            {
                await _unitOfWork.Hall.UpdateWithFeaturesAsync(hallEntity, hallDto.FeatureIds);
            }

            await _unitOfWork.SaveAsync();

            return await GetHallByIdAsync(hallEntity.HallId);
        }

        public async Task<bool> DeleteHallAsync(int id)
        {
            await _unitOfWork.Hall.DeleteAsync(id);

            return true;
        }

        public async Task<HallDto?> EditHallAsync(HallDto hallDto)
        {
            var exists = await _unitOfWork.Hall.GetByIdWithStatsAsync(hallDto.Id);

            if(exists == null)
            {
                throw new KeyNotFoundException($"Зал з id {hallDto.Id} не знайдено.");
            }

            var hallEntity = _mapper.MapToEntity(hallDto);
            hallEntity.HallId = hallDto.Id;

            await _unitOfWork.Hall.UpdateWithFeaturesAsync(hallEntity, hallDto.FeatureIds);

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

        public async Task<HallDto?> GetHallDetailsAsync(int id)
        {
            var dto = await _unitOfWork.Hall.GetHallWithFutureSessionsAsync(id);

            if (dto == null) {
                throw new KeyNotFoundException($"Зал з id {id} не знайдено."); 
            }

            return dto;
        }

        // seats

        private async Task GenerateSeatsForHall(Hall hall, HallDto dto)
        {
            int startVipRow = hall.RowCount - dto.VipRowCount;

            for (byte row = 1; row <= hall.RowCount; row++)
            {
                bool isVipRow = row > startVipRow;

                SeatType type = isVipRow ? SeatType.VIP : SeatType.Standard;
                float coef = isVipRow ? dto.VipCoefficient : 1.0f;

                for (byte number = 1; number <= hall.SeatInRowCount; number++)
                {
                    var seat = _seatMapper.CreateSeatEntity(hall.HallId, row, number, type, coef);

                    await _unitOfWork.Seat.AddAsync(seat);
                }
            }
        }
    }
}
