using onlineCinema.Application.DTOs.Snack;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SnackMapper
    {
        [MapProperty(nameof(Snack.SnackName), nameof(SnackDto.Name))]
        public partial SnackDto MapSnackToDto(Snack snack);

        public List<SnackDto> MapSnacksToDtos(IEnumerable<Snack> snacks)
            => snacks.Select(MapSnackToDto).ToList();

        public partial SnackBooking
            MapSelectedSnackDtoToEntity(SelectedSnackDto dto);

        public partial List<SnackBooking>
            MapSelectedSnackDtoToEntityList(
                List<SelectedSnackDto> dtos,
                int bookingId);

        [MapProperty(nameof(SnackDto.Name), nameof(Snack.SnackName))]
        public partial Snack MapToEntity(SnackDto dto);

        [MapProperty(nameof(SnackDto.Name), nameof(Snack.SnackName))]
        public partial void UpdateEntityFromDto(
            SnackDto dto,
            Snack entity);
    }
}
