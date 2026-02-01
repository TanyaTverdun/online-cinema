using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SnackMapping
    {
        public partial SnackDto MapToDto(Snack snack);

        public partial List<SnackDto> MapToDtoList(IEnumerable<Snack> snacks);

        public partial Snack MapToEntity(SnackDto dto);
        public partial void UpdateEntityFromDto(SnackDto dto, Snack entity);
    }
}