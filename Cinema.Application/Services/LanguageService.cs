using onlineCinema.Application.DTOs;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services;

public class LanguageService : ILanguageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly LanguageMapping _mapper;

    public LanguageService(IUnitOfWork unitOfWork, LanguageMapping mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LanguageDto>> GetAllAsync()
    {
        var entities = await _unitOfWork.Language.GetAllAsync();
        return entities.Select(e => _mapper.MapToDto(e));
    }

    public async Task<LanguageDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Language.GetByIdAsync(id);
        return entity != null ? _mapper.MapToDto(entity) : null;
    }

    public async Task CreateAsync(LanguageDto dto)
    {
        var entity = _mapper.MapToEntity(dto);
        await _unitOfWork.Language.AddAsync(entity);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(LanguageDto dto)
    {
        var entity = await _unitOfWork.Language.GetByIdAsync(dto.LanguageId);
        if (entity != null)
        {
            _mapper.UpdateEntityFromDto(dto, entity);
            _unitOfWork.Language.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Language.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.Language.Remove(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}