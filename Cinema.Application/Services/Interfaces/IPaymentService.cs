using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.AdminTickets;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PagedResult<PaymentAdminDto>> GetPaymentsForAdminAsync(int? lastId, string? email, string? movie, DateTime? date);
        Task RefundPaymentAsync(int paymentId);
    }
}
