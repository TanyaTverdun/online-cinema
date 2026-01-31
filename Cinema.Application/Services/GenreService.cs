using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenreMapping _mapper;

        public GenreService(IUnitOfWork unitOfWork, GenreMapping mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreFormDto>> GetAllAsync()
        {
            var genres = await _unitOfWork.Genre.GetAllAsync();
            return genres.Select(g => _mapper.ToDto(g)!);
        }

        public async Task<GenreFormDto?> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            return _mapper.ToDto(genre);
        }

        public async Task AddAsync(GenreFormDto dto)
        {
            var genre = _mapper.ToEntity(dto);
            await _unitOfWork.Genre.AddAsync(genre);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(GenreFormDto dto)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(dto.GenreId);
            if (genre != null)
            {
                _mapper.UpdateEntityFromDto(dto, genre);
                _unitOfWork.Genre.Update(genre);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            if (genre != null)
            {
                _unitOfWork.Genre.Remove(genre);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
