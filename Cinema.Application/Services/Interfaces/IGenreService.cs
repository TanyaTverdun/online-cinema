using System.Collections.Generic;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();

        Task<GenreDto?> GetByIdAsync(int id);

        Task CreateAsync(GenreDto dto);

        Task UpdateAsync(GenreDto dto);

        Task DeleteAsync(int id);
    }
}