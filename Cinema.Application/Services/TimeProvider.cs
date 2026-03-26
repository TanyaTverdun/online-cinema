using Microsoft.Extensions.Options;
using onlineCinema.Application.Configurations;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class LocalTimeProvider : ITimeProvider
    {
        private readonly TimeZoneInfo _timeZone;

        public LocalTimeProvider(IOptions<TimeSettings> settings)
        {
            _timeZone = TimeZoneInfo
                .FindSystemTimeZoneById(settings.Value.TimeZoneId);
        }

        public DateTime Now => TimeZoneInfo
            .ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);

        public DateTime Today => Now.Date;
    }
}
