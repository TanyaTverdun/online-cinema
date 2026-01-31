using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Genre;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreFormDto>> GetAllAsync();
        Task<GenreFormDto?> GetByIdAsync(int id);
        Task AddAsync(GenreFormDto dto);
        Task UpdateAsync(GenreFormDto dto);
        Task DeleteAsync(int id);
    }
}
