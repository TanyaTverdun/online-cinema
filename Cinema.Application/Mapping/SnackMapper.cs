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
        [MapProperty(nameof(StudioMerch.SnackName), nameof(SnackDto.Name))]
        public partial SnackDto MapSnackToDto(StudioMerch snack);

        public partial List<SnackDto> 
            MapSnacksToDtos(IEnumerable<StudioMerch> snacks);

        public partial MerchOrder 
            MapSelectedSnackDtoToEntity(SelectedSnackDto dto);

        public partial List<MerchOrder> 
            MapSelectedSnackDtoToEntityList(
                List<SelectedSnackDto> dtos, 
                int bookingId);

        public partial StudioMerch MapToEntity(SnackDto dto);
        public partial void UpdateEntityFromDto(
            SnackDto dto, 
            StudioMerch entity);
    }
}
