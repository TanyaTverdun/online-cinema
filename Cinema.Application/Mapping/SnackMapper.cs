using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SnackMapper
    {
        [MapProperty(nameof(Snack.SnackName), nameof(SnackDto.Name))]
        public partial SnackDto MapSnackToDto(Snack snack);

        public partial List<SnackDto> MapSnacksToDtos(IEnumerable<Snack> snacks);

        public partial SnackBooking MapSelectedSnackDtoToEntity(SelectedSnackDto dto);

        public partial List<SnackBooking> MapSelectedSnackDtoToEntityList(List<SelectedSnackDto> dtos, int bookingId);
    }
}
