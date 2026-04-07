namespace onlineCinema.Application.DTOs.Performance; // Було .Movie

public class ChoreographerFormDto // Було DirectorFormDto
{
    public int ChoreographerId { get; set; } // Було DirectorId
    public string FirstName { get; set; } = string.Empty; // Було DirectorFirstName
    public string LastName { get; set; } = string.Empty;  // Було DirectorLastName
    public string MiddleName { get; set; } = string.Empty; // Було DirectorMiddleName
}