using System.Linq;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SessionMapper
    {
        [MapperIgnoreTarget(nameof(DanceClass.SessionId))]
        [MapperIgnoreTarget(nameof(DanceClass.Movie))]
        [MapperIgnoreTarget(nameof(DanceClass.Hall))]
        [MapperIgnoreTarget(nameof(DanceClass.Tickets))]
        public partial DanceClass MapToSession(SessionCreateDto dto);
            
        public MovieScheduleDto MapToMovieSchedule(
            Performance movie,
            IEnumerable<DanceClass> sessions)
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
        [MapProperty(nameof(Performance.Id), nameof(MovieScheduleDto.MovieId))]
        [MapProperty(nameof(Performance.Title), nameof(MovieScheduleDto.MovieTitle))]
        [MapProperty(nameof(Performance.Runtime), nameof(MovieScheduleDto.Runtime))]
        public partial MovieScheduleDto MapMovieToScheduleDtoBase(Performance movie);

        [MapProperty(nameof(DanceClass.ShowingDateTime), 
            nameof(SessionScheduleDto.StartDateTime))]
        [MapProperty(nameof(DanceClass.BasePrice), 
            nameof(SessionScheduleDto.BasePrice))]
        [MapProperty(nameof(DanceClass.Hall), 
            nameof(SessionScheduleDto.HallName))]
        [MapProperty(nameof(DanceClass.Hall.HallFeatures), 
            nameof(SessionScheduleDto.FeatureNames))]
        public partial SessionScheduleDto MapSessionToDto(DanceClass session);

        private string MapHallToHallName(DanceHall hall)
            => $"Зал {hall.HallNumber}";

        private List<string> MapHallFeaturesToFeatureNames(
            ICollection<HallEquipment> hallFeatures)
            => hallFeatures
                .Select(hf => hf.Feature.Name)
                .ToList();

        [MapProperty(nameof(DanceClass.SessionId), 
            nameof(SessionDto.Id))]
        [MapProperty(nameof(DanceClass.Movie.Title), 
            nameof(SessionDto.MovieTitle))]
        [MapProperty(nameof(DanceClass.Hall.HallNumber), 
            nameof(SessionDto.HallNumber))]
        public partial SessionDto MapToDto(DanceClass session);

        public partial IEnumerable<SessionDto> 
            MapToDtoList(IEnumerable<DanceClass> sessions);

        [MapperIgnoreTarget(nameof(DanceClass.Tickets))]
        public partial void 
            UpdateEntityFromDto(SessionUpdateDto dto, DanceClass session);
    }
}
