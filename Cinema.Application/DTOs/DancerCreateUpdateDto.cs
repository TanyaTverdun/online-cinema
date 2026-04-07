namespace onlineCinema.Application.DTOs;

public class DancerCreateUpdateDto
{
    public int DancerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public int? SkillLevelId { get; set; }
}
