using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DirectorMapping _mapper;

        public DirectorService(IUnitOfWork unitOfWork, DirectorMapping mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorFormDto>> GetAllAsync()
        {
            var directors = await _unitOfWork.Director.GetAllAsync();
            return directors.Select(d => _mapper.ToDto(d));
        }

        public async Task<DirectorFormDto?> GetByIdAsync(int id)
        {
            var director = await _unitOfWork.Director.GetByIdAsync(id);

            var dto = _mapper.ToDto(director);

            return dto;
        }

        public async Task AddAsync(DirectorFormDto dto)
        {
            var director = _mapper.ToEntity(dto);
            await _unitOfWork.Director.AddAsync(director);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(DirectorFormDto dto)
        {
            var director = await _unitOfWork.Director.GetByIdAsync(dto.DirectorId);
            if (director != null)
            {
                _mapper.UpdateEntityFromDto(dto, director);
                _unitOfWork.Director.Update(director);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var director = await _unitOfWork.Director.GetByIdAsync(id);
            if (director != null)
            {
                _unitOfWork.Director.Remove(director);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
