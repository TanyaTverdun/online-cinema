namespace onlineCinema.Areas.Admin.Models;

public class CastMemberViewModel
{
    public int CastId { get; set; }
    public string CastFirstName { get; set; } = string.Empty;
    public string CastLastName { get; set; } = string.Empty;
    public string CastMiddleName { get; set; } = string.Empty;
}
