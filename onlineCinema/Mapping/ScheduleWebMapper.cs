using onlineCinema.Application.DTOs;
using onlineCinema.ViewModels;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class MovieScheduleViewModelMapper
    {
        private const string LabelToday = "Сьогодні";
        private const string LabelTomorrow = "Завтра";
        private const string DisplayDateFormat = "d MMM";
        private const string CurrencySuffix = "грн";
        private const string TimeFormat = "HH:mm";
        private const string IdDateFormat = "yyyyMMdd";
        private const string TabIdPrefix = "day-";

        public MovieScheduleViewModel MapMovieScheduleDtoToViewModel(MovieScheduleDto dto)
        {
            var vm = MapBase(dto);

            vm.Days = dto.Schedule.Select((day, index) => MapToDayVm(day, index)).ToList();

            return vm;
        }

        [MapProperty(nameof(MovieScheduleDto.Schedule), nameof(MovieScheduleViewModel.Days))]
        private partial MovieScheduleViewModel MapBase(MovieScheduleDto dto);

        private ScheduleDayViewModel MapToDayVm(DailyScheduleDto day, int index)
        {
            return new ScheduleDayViewModel
            {
                DateLabel = GetHumanReadableDate(day.Date),
                TabId = $"{TabIdPrefix}{day.Date.ToString(IdDateFormat)}",
                IsActive = index == 0,
                Sessions = day.Sessions.Select(MapToSessionVm).ToList()
            };
        }

        private SessionCardViewModel MapToSessionVm(SessionScheduleDto s)
        {
            return new SessionCardViewModel
            {
                SessionId = s.SessionId,
                Time = s.StartDateTime.ToString(TimeFormat),
                HallName = s.HallName,
                Features = s.FeatureNames,
                Price = $"{s.BasePrice:0} {CurrencySuffix}"
            };
        }

        private string GetHumanReadableDate(DateTime date)
        {
            if (date.Date == DateTime.Today)
            {
                return LabelToday;
            }

            if (date.Date == DateTime.Today.AddDays(1))
            {
                return LabelTomorrow;
            }

            return date.ToString(DisplayDateFormat);
        }
    }
}
