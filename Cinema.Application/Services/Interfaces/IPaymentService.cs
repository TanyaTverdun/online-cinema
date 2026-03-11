using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Application.DTOs.Common;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PagedResultDto<PaymentAdminDto>> GetPaymentsForAdminAsync(
            int? lastId,
            string? email,
            string? movie,
            DateTime? date);
        Task RefundPaymentAsync(int paymentId);
    }
}
