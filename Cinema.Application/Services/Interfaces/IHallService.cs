using onlineCinema.Application.DTOs.Hall;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IHallService
    {
        Task<IEnumerable<HallDto>> GetAllHallsAsync();
        Task<HallDto?> GetHallByIdAsync(int id);
        Task<HallDto?> CreateHallAsync(HallDto hallDto);
        Task<HallDto?> EditHallAsync(HallDto hallDto);
        Task<bool> DeleteHallAsync(int id);
        Task<IEnumerable<Feature>> GetAllFeaturesAsync();
        Task<HallDto?> GetHallDetailsAsync(int id);
    }
}
