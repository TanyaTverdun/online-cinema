using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services;

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
        var entities = await _unitOfWork.Genre.GetAllAsync();
        return entities.Select(e => _mapper.MapToDto(e));
    }

    public async Task<GenreDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Genre.GetByIdAsync(id);
        return entity != null ? _mapper.MapToDto(entity) : null;
    }

    public async Task CreateAsync(GenreDto dto)
    {
        var entity = _mapper.MapToEntity(dto);
        await _unitOfWork.Genre.AddAsync(entity);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(GenreDto dto)
    {
        var entity = await _unitOfWork.Genre.GetByIdAsync(dto.GenreId);
        if (entity != null)
        {
            _mapper.UpdateEntityFromDto(dto, entity);
            _unitOfWork.Genre.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }

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
            var dtos = _mapper.ToDtoList(genres);
            return dtos;
        }

        public async Task<GenreFormDto?> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            var dto = genre == null ? null : _mapper.ToDto(genre);
            return dto;
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