using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task<IEnumerable<Seat>> GetSeatsByHallIdAsync(int hallId);
    }
}
