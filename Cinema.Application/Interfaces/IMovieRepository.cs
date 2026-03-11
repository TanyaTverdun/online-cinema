using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<Movie?> GetByIdWithAllDetailsAsync(int id);
    }
}
