namespace onlineCinema.Domain.Entities;

public class Choreographer
{
    public int ChoreographerId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;

    // Додав контактні дані (корисно для адмінки студії)
    public string? Phone { get; set; }
    public string? Email { get; set; }

    // Додав короткий опис/досвід (можна виводити на сайті)
    public string? Bio { get; set; }

    // Зв'язок "багато-до-багатьох" з виступами/групами
    public virtual ICollection<ChoreographerPerformance> ChoreographerPerformances { get; set; } = new List<ChoreographerPerformance>();

    // Додав прямий зв'язок із заняттями, якщо хореограф веде конкретні уроки
    public virtual ICollection<DanceClass> DanceClasses { get; set; } = new List<DanceClass>();
}