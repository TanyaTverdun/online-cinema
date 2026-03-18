namespace onlineCinema.Application.Services.Interfaces
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
