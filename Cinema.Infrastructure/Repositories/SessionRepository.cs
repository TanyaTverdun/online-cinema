using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;
using onlineCinema.Application.Interfaces;
using onlineCinema.Infrastructure.Data; 

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Session>> GetScheduleAsync()
    {
        return await _context.Sessions
            .Include(s => s.Movie) 
            .Include(s => s.Hall)  
            .ToListAsync();
    }
    public async Task<IEnumerable<Session>> GetAllWithDetailsAsync()
    {
        return await _context.Sessions
            .Include(s => s.Movie) 
            .Include(s => s.Hall)  
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetAllAsync() => await _context.Sessions.ToListAsync();

    public async Task<Session?> GetByIdAsync(int id) =>
        await _context.Sessions.Include(s => s.Movie).Include(s => s.Hall)
        .FirstOrDefaultAsync(s => s.SessionId == id);

    public async Task<IEnumerable<Session>> GetByMovieIdAsync(int movieId) =>
        await _context.Sessions.Where(s => s.MovieId == movieId).ToListAsync();
}
