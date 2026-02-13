using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces;

public interface ISnackService
{
    Task<IEnumerable<SnackDto>> GetAllAsync();
    Task<SnackDto?> GetByIdAsync(int id);
    Task CreateAsync(SnackDto dto);
    Task UpdateAsync(SnackDto dto);
    Task DeleteAsync(int id);
}