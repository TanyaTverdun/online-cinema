using Microsoft.Extensions.Options;
using onlineCinema.Application.Configurations;
using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaymentMapper _mapper;
        private readonly StatisticsSettings _settings;

        public PaymentService(
            IUnitOfWork unitOfWork,
            PaymentMapper mapper,
            IOptions<StatisticsSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<PagedResult<PaymentAdminDto>> GetPaymentsForAdminAsync(
            int? lastId, string? email, string? movie, DateTime? date)
        {
            int pageSize = _settings.AdminPageSize;

            var (entities, totalCount) = await _unitOfWork.Payment.GetPaymentsSeekAsync(
                lastId, 
                pageSize, 
                email, 
                movie, 
                date);

            return _mapper.MapToPagedResult(entities, totalCount, pageSize);
        }

        public async Task RefundPaymentAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payment.GetByIdAsync(paymentId);

            if (payment != null && payment.Status == PaymentStatus.Completed)
            {
                payment.Status = PaymentStatus.Refunded;
                _unitOfWork.Payment.Update(payment);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
