using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces;

public interface ILanguageService
{
    Task<IEnumerable<LanguageDto>> GetAllAsync();
    Task<LanguageDto?> GetByIdAsync(int id);
    Task CreateAsync(LanguageDto dto);
    Task UpdateAsync(LanguageDto dto);
    Task DeleteAsync(int id);
}