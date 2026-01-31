using onlineCinema.Application.DTOs;
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

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Genre.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.Genre.Remove(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}