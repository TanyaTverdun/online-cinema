using System.Threading.Tasks;

namespace onlineCinema.Application.Interfaces
{
    public interface IUnitOfWork
    {
        // Властивості тепер називаються відповідно до сутностей танцювальної студії
        ICostumeBookingRepository Bookings { get; }         // Було Booking
        IDancerRepository Dancers { get; }                  // Було CastMember
        IStudioBranchRepository Branches { get; }           // Було Cinema
        IChoreographerRepository Choreographers { get; }    // Було Director
        IRequirementRepository Requirements { get; }        // Було Feature
        IDanceStyleRepository Styles { get; }               // Було Genre
        IDanceHallRepository Halls { get; }                 // Було Hall
        ISkillLevelRepository SkillLevels { get; }          // Було Language
        IPerformanceRepository Performances { get; }        // Було Movie
        IFinancialTransactionRepository Payments { get; }   // Було Payment
        IInventaryRepository Inventory { get; }             // Було Seat
        IDanceClassRepository Classes { get; }              // Було Session
        IStudioMerchRepository Merch { get; }               // Було Snack
        IAttendanceLogRepository AttendanceLogs { get; }    // Було Ticket
        IStatisticRepository Statistics { get; }

        Task SaveAsync();
    }
}