using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Movie;

namespace onlineCinema.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardDto>> GetMoviesForShowcaseAsync();

        Task<MovieDetailsDto?> GetMovieDetailsAsync(int id);

        Task<MovieFormDto?> GetMovieForEditAsync(int id);

        Task AddMovieAsync(MovieFormDto model);
        Task UpdateMovieAsync(MovieFormDto model);
        Task DeleteMovieAsync(int id);
    }
}