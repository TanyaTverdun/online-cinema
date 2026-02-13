using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Movie;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IDirectorService
    {
        Task<IEnumerable<DirectorFormDto>> GetAllAsync();
        Task<DirectorFormDto?> GetByIdAsync(int id);
        Task AddAsync(DirectorFormDto dto);
        Task UpdateAsync(DirectorFormDto dto);
        Task DeleteAsync(int id);
    }
}