using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SessionMapper
    {
        public Session MapToSession(SessionCreateDto dto)
        {
            return new Session
            {
                MovieId = dto.MovieId,
                HallId = dto.HallId,
                ShowingDateTime = dto.ShowingDateTime,
                BasePrice = dto.BasePrice
            };
        }
        public MovieScheduleDto MapToMovieSchedule(
            Movie movie,
            IEnumerable<Session> sessions)
        {
            var scheduleDto = MapMovieToScheduleDtoBase(movie);

            scheduleDto.Schedule = sessions
                .GroupBy(s => s.ShowingDateTime.Date)
                .OrderBy(g => g.Key)
                .Select(g => new DailyScheduleDto
                {
                    Date = g.Key,
                    Sessions = g.Select(MapSessionToDto).ToList()
                })
                .ToList();

            return scheduleDto;
        }

        [MapperIgnoreTarget(nameof(MovieScheduleDto.Schedule))]
        [MapProperty(nameof(Movie.Id), nameof(MovieScheduleDto.MovieId))]
        [MapProperty(nameof(Movie.Title), nameof(MovieScheduleDto.MovieTitle))]
        [MapProperty(nameof(Movie.Runtime), nameof(MovieScheduleDto.Runtime))]
        private partial MovieScheduleDto MapMovieToScheduleDtoBase(Movie movie);

        
        [MapProperty(nameof(Session.ShowingDateTime), nameof(SessionScheduleDto.StartDateTime))]
        [MapProperty(nameof(Session.BasePrice), nameof(SessionScheduleDto.BasePrice))]
        [MapProperty(nameof(Session.Hall), nameof(SessionScheduleDto.HallName))]
        [MapProperty(nameof(Session.Hall.HallFeatures), nameof(SessionScheduleDto.FeatureNames))]
        public partial SessionScheduleDto MapSessionToDto(Session session);

        
        private string MapHallToHallName(Hall hall)
            => $"Зал {hall.HallNumber}";

        private List<string> MapHallFeaturesToFeatureNames(
            ICollection<HallFeature> hallFeatures)
            => hallFeatures
                .Select(hf => hf.Feature.Name)
                .ToList();
    }
}
