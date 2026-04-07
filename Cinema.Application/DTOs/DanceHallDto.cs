namespace onlineCinema.Application.DTOs;

public class DanceHallDto // Було HallDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Замість HallNumber (наприклад, "Main Stage", "Studio A")

    public float AreaSize { get; set; } // Площа в м²
    public int MaxPeople { get; set; } // Максимальна місткість для групових занять

    // Особливості залу (наприклад: "Балетний станок", "Амортизуюче покриття", "Дзеркала на всю стіну")
    public List<string> FeatureNames { get; set; } = new();
    public List<int> FeatureIds { get; set; } = new();
    public List<string> FeatureDescriptions { get; set; } = new();

    // Розклад занять у цьому залі
    public List<DanceClassMapDto> Classes { get; set; } = new(); // Було Sessions
}