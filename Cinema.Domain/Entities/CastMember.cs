namespace onlineCinema.Domain.Entities;

public class CastMember
{
    public int CastId { get; set; }
    public string CastFirstName { get; set; } = string.Empty;
    public string CastLastName { get; set; } = string.Empty;
    public string? CastMiddleName { get; set; }
    public ICollection<MovieCast> MovieCasts { get; set; } = [];
}