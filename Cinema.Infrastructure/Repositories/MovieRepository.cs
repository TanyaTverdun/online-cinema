using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace onlineCinema.Infrastructure.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Movie?> GetByIdWithAllDetailsAsync(int id)
        {
            return await _db.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCasts)
                    .ThenInclude(mc => mc.CastMember)
                .Include(m => m.MovieDirectors)
                    .ThenInclude(md => md.Director)
                .Include(m => m.MovieLanguages)
                    .ThenInclude(ml => ml.Language)
                .Include(m => m.MovieFeatures)
                    .ThenInclude(mf => mf.Feature)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
