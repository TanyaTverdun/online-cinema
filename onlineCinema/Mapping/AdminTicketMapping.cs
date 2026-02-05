using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminTicketViewMapping
    {
        [MapProperty(nameof(TicketAdminDto.ShowingDateTime), nameof(TicketAdminViewModel.ShowingDateTimeFormatted), Use = nameof(MapShowingDateTimeFormatted))]
        [MapProperty(nameof(TicketAdminDto.PurchaseDate), nameof(TicketAdminViewModel.PurchaseDateFormatted), Use = nameof(MapPurchaseDateFormatted))]
        public partial TicketAdminViewModel ToViewModel(TicketAdminDto dto);

        public partial List<TicketAdminViewModel> ToViewModelList(List<TicketAdminDto> dtos);

        [MapProperty(nameof(PagedResult<TicketAdminDto>.Items), nameof(TicketListViewModel.Tickets))]
        public partial TicketListViewModel ToListViewModel(PagedResult<TicketAdminDto> pagedDto);

        private string MapShowingDateTimeFormatted(DateTime dt) => dt.ToString("dd.MM.yyyy HH:mm");
        private string MapPurchaseDateFormatted(DateTime dt) => dt.ToString("g");

        public TicketListViewModel MapWithDetails(
                PagedResult<TicketAdminDto> pagedDto,
                int? lastId,
                string? email,
                string? movie,
                DateTime? date)
        {
            var vm = ToListViewModel(pagedDto);

            for (int i = 0; i < pagedDto.Items.Count; i++)
            {
                var dto = pagedDto.Items[i];
                vm.Tickets[i].SeatInfo = $"Зал {dto.HallNumber}: Ряд {dto.Row}, Місце {dto.Number}";
            }

            vm.LastId = lastId;
            vm.NextId = pagedDto.Items.LastOrDefault()?.TicketId;
            vm.HasNextPage = pagedDto.HasNextPage;
            vm.TotalCount = pagedDto.TotalCount;

            vm.SearchEmail = email;
            vm.SearchMovie = movie;
            vm.SearchDate = date;

            return vm;
        }
    }
}
