using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IHallService
    {
        Task<IEnumerable<HallDto>> GetAllHallsAsync();
        Task<HallDto?> GetHallByIdAsync(int id);
        Task<HallDto?> CreateHallAsync(HallDto hallDto, List<int> selectedFeatureIds);
        Task<HallDto?> EditHallAsync(HallDto hallDto, List<int> selectedFeatureIds);
        Task<bool> DeleteHallAsync(int id);
        Task<IEnumerable<Feature>> GetAllFeaturesAsync();
    }
}
