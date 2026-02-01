using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Movie;

namespace onlineCinema.Application.Services.Interfaces;

public interface IFeatureService
{
    Task<IEnumerable<FeatureDto>> GetAllAsync();
    Task<FeatureDto?> GetByIdAsync(int id);
    Task CreateAsync(FeatureDto dto);
    Task UpdateAsync(FeatureDto dto);
    Task DeleteAsync(int id);
}