using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces;

public interface ICastMemberService
{
    Task<IEnumerable<CastMemberDto>> GetAllAsync();
    Task<CastMemberDto?> GetByIdAsync(int id);
    Task CreateAsync(CastMemberCreateUpdateDto dto);
    Task UpdateAsync(CastMemberCreateUpdateDto dto);
    Task DeleteAsync(int id);
}