using onlineCinema.Application.DTOs.Person;

namespace onlineCinema.Application.Services.Interfaces;

public interface ICastMemberService
{
    Task<IEnumerable<CastMemberDto>> GetAllAsync();
    Task<CastMemberDto?> GetByIdAsync(int id);
    Task CreateAsync(CastMemberDto dto);
    Task UpdateAsync(CastMemberDto dto);
    Task DeleteAsync(int id);
}