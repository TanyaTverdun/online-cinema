using onlineCinema.Application.DTOs;
using onlineCinema.ViewModels;

namespace onlineCinema.Mapping
{
    public static class SessionViewModelMapper
    {
        public static SessionCreateDto ToDto(this SessionCreateViewModel vm)
        {
            return new SessionCreateDto
            {
                MovieId = vm.MovieId,
                HallId = vm.HallId,
                ShowingDateTime = vm.ShowingDateTime,
                BasePrice = vm.BasePrice
            };
        }
    }
}
