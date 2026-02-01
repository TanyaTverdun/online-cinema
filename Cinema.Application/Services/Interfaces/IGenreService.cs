using System.Collections.Generic;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Genre;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();

        Task<GenreDto?> GetByIdAsync(int id);

        Task CreateAsync(GenreFormDto dto);

        Task UpdateAsync(GenreFormDto dto);

        Task DeleteAsync(int id);
    }
}