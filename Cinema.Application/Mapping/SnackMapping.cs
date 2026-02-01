using System.Collections.Generic;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SnackMapping
    {
        [MapperIgnoreSource(nameof(Snack.SnackBookings))]
        public partial SnackDto MapToDto(Snack entity);

        [MapperIgnoreTarget(nameof(Snack.SnackBookings))]
        public partial Snack MapToEntity(SnackDto dto);

        [MapperIgnoreTarget(nameof(Snack.SnackBookings))]
        [MapperIgnoreTarget(nameof(Snack.SnackId))]
        [MapperIgnoreSource(nameof(SnackDto.SnackId))]
        public partial void UpdateEntityFromDto(SnackDto dto, Snack entity);

        public partial IEnumerable<SnackDto> MapToDtoList(IEnumerable<Snack> entities);
    }
}