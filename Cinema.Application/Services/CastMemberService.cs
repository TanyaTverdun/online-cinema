using onlineCinema.Application.DTOs;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.Interfaces;

namespace onlineCinema.Application.Services;

public class CastMemberService : ICastMemberService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CastMemberMapping _mapper;

    public CastMemberService(IUnitOfWork unitOfWork, CastMemberMapping mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CastMemberDto>> GetAllAsync()
    {
        var entities = await _unitOfWork.CastMember.GetAllAsync();
        return _mapper.MapToDtoList(entities);
    }

    public async Task<CastMemberDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.CastMember.GetByIdAsync(id);
        return entity != null ? _mapper.MapToDto(entity) : null;
    }

    public async Task CreateAsync(CastMemberCreateUpdateDto dto)
    {
        var entity = _mapper.MapToEntity(dto);
        await _unitOfWork.CastMember.AddAsync(entity);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(CastMemberCreateUpdateDto dto)
    {
        var entity = await _unitOfWork.CastMember.GetByIdAsync(dto.CastId);
        if (entity != null)
        {
            _mapper.UpdateEntityFromDto(dto, entity);
            _unitOfWork.CastMember.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.CastMember.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.CastMember.Remove(entity); 
            await _unitOfWork.SaveAsync();
        }
    }
}