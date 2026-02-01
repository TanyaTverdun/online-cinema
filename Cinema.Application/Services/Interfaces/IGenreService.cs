using System.Collections.Generic;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;        // Додано для доступу до GenreDto (Read модель)
using onlineCinema.Application.DTOs.Genre;  // Додано для доступу до GenreFormDto (Write модель)

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IGenreService
    {
        // Для читання (GET) повертаємо GenreDto, бо нам потрібен ID
        Task<IEnumerable<GenreDto>> GetAllAsync();

        // Для пошуку (GET) теж повертаємо GenreDto
        Task<GenreDto?> GetByIdAsync(int id);

        // Для створення (POST) приймаємо форму GenreFormDto
        // ВАЖЛИВО: Перейменовано з AddAsync на CreateAsync, щоб співпадало з Контролером
        Task CreateAsync(GenreFormDto dto);

        // Для оновлення (POST/PUT) приймаємо форму
        Task UpdateAsync(GenreFormDto dto);

        Task DeleteAsync(int id);
    }
}