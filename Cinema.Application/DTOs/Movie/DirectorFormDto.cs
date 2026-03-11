namespace onlineCinema.Application.DTOs.Movie
{
    public class DirectorFormDto
    {
        public int DirectorId { get; set; }
        public string DirectorFirstName { get; set; } = string.Empty;
        public string DirectorLastName { get; set; } = string.Empty;
        public string DirectorMiddleName { get; set; } = string.Empty;
    }
}