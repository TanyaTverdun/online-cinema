using onlineCinema.Application.DTOs;
using onlineCinema.ViewModels;

namespace onlineCinema.Mapping
{
    public static class SessionVmMapper
    {
        public static SessionEditViewModel ToEditViewModel(this SessionDto dto)
        {
            return new SessionEditViewModel
            {
                Id = dto.Id,
                MovieId = dto.MovieId,
                HallId = dto.HallId,
                ShowingDateTime = dto.ShowingDateTime,
                BasePrice = dto.BasePrice
            };
        }
    }
}
