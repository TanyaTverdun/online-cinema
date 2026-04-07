using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces;

public interface IFinancialTransactionRepository : IGenericRepository<FinancialTransaction>
{
    Task<IEnumerable<FinancialTransaction>> GetAllWithBookingAsync();
    Task<FinancialTransaction?> GetByIdWithBookingAsync(int id);

    Task<(IEnumerable<FinancialTransaction> Items, int TotalCount)> GetPaymentsSeekAsync(
        int? lastId,
        int pageSize,
        string? email,
        string? performanceTitle, // ВИПРАВЛЕНО: Було movieTitle
        DateTime? date);
}