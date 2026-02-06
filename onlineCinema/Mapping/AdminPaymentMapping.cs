using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminPaymentMapping
    {
        [MapProperty(nameof(PaymentAdminDto.Status), 
            nameof(PaymentAdminViewModel.StatusName), Use = nameof(MapStatus))]
        [MapProperty(nameof(PaymentAdminDto.MovieSessionDateTime), 
            nameof(PaymentAdminViewModel.FormattedSessionDate), Use = nameof(FormatSession))]
        [MapProperty(nameof(PaymentAdminDto.OrderDate), 
            nameof(PaymentAdminViewModel.FormattedDate), Use = nameof(FormatDate))]
        [MapProperty(nameof(PaymentAdminDto.TicketsInfo), 
            nameof(PaymentAdminViewModel.TicketsDetail))]
        [MapProperty(nameof(PaymentAdminDto.SnacksInfo), 
            nameof(PaymentAdminViewModel.SnacksDetail))]
        public partial PaymentAdminViewModel ToViewModel(PaymentAdminDto dto);

        private string FormatSession(DateTime dt)
            => dt == DateTime.MinValue ? "—" : dt.ToString("dd.MM.yyyy HH:mm");

        [MapProperty(nameof(PagedResult<PaymentAdminDto>.Items), 
            nameof(PaymentListViewModel.Payments))]
        public partial PaymentListViewModel ToListViewModel(PagedResult<PaymentAdminDto> pagedDto);

        private string FormatDate(DateTime dt) => dt.ToString("g");

        public PaymentListViewModel MapWithDetails(
            PagedResult<PaymentAdminDto> pagedDto,
            int? lastId, string? email, string? movie, 
            DateTime? date, string? successMessage)
        {
            var vm = ToListViewModel(pagedDto);

            for (int i = 0; i < pagedDto.Items.Count; i++)
            {
                vm.Payments[i].IsRefundable = pagedDto.Items[i].Status == PaymentStatus.Completed;
            }

            vm.LastId = lastId;
            vm.NextId = pagedDto.Items.LastOrDefault()?.PaymentId;
            vm.HasNextPage = pagedDto.HasNextPage;
            vm.TotalCount = pagedDto.TotalCount;

            vm.SearchEmail = email;
            vm.SearchMovie = movie;
            vm.SearchDate = date;
            vm.SuccessMessage = successMessage;

            return vm;
        }

        private string MapStatus(PaymentStatus status)
        {
            return status switch
            {
                PaymentStatus.Completed => "Оплачено",
                PaymentStatus.Refunded => "Повернено",
                PaymentStatus.Pending => "Очікується",
                _ => "Помилка"
            };
        }
    }
}
