using onlineCinema.Application.DTOs.Movie;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardDto>> GetMoviesForShowcaseAsync();
        Task<MovieDetailsDto?> GetMovieDetailsAsync(int id);
        Task<MovieFormDto?> GetMovieForEditAsync(int id);
        Task AddMovieAsync(MovieFormDto model);
        Task UpdateMovieAsync(MovieFormDto model);
        Task DeleteMovieAsync(int id);
        Task<MovieDropdownsDto> GetMovieDropdownsValuesAsync();
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    }
}