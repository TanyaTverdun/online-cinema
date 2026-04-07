namespace onlineCinema.Application.DTOs;

public class DancerDto
{
    public int DancerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string? SkillLevelName { get; set; }
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}