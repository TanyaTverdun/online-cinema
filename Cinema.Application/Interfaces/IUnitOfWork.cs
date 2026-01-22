using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IBookingRepository Booking { get; }
        ICastMemberRepository CastMember { get; }
        ICinemaRepository Cinema { get; }
        IDirectorRepository Director { get; }
        IFeatureRepository Feature { get; }
        IGenreRepository Genre { get; }
        IHallRepository Hall { get; }
        ILanguageRepository Language { get; }
        IMovieRepository Movie { get; }
        IPaymentRepository Payment { get; }
        ISeatRepository Seat { get; }
        ISessionRepository Session { get; }
        ISnackRepository Snack { get; }
        ITicketRepository Ticket { get; }
        Task SaveAsync();
    }
}
