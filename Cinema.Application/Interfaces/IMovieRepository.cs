using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<Movie?> GetByIdWithAllDetailsAsync(int id);
        Task<Movie?> GetByIdWithFeaturesAsync(int id);

    }
}
