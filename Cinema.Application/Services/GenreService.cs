using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;

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

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            var genres = await _unitOfWork.Genre.GetAllAsync();

            var dtos = genres.Select(g => new GenreDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName
            });

            return dtos;
        }

        public async Task<GenreDto?> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            if (genre == null) return null;

            return new GenreDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
        }

        public async Task CreateAsync(GenreFormDto dto)
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