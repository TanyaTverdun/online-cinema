using onlineCinema.Application.DTOs;
using onlineCinema.ViewModels;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class SessionViewModelMapper
    {
        public partial SessionCreateDto MapToCreateDto(SessionCreateViewModel vm);

        public partial SessionUpdateDto MapToUpdateDto(SessionEditViewModel vm);

        public partial SessionEditViewModel MapToEditViewModel(SessionDto dto);
    }
}
  
