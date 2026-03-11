using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetAllWithBookingAsync();
        Task<Payment?> GetByIdWithBookingAsync(int id);
        Task<(IEnumerable<Payment> Items, int TotalCount)> GetPaymentsSeekAsync(
            int? lastId,
            int pageSize,
            string? email,
            string? movieTitle,
            DateTime? date);
    }

}
