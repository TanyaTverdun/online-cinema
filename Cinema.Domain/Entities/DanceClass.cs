namespace onlineCinema.Domain.Entities;

public class DanceClass
{
    // Первинний ключ
    public int ClassId { get; set; }

    // Зв'язок із виступом/програмою (наприклад, "Hip-Hop Intensive")
    public int PerformanceId { get; set; }
    public virtual Performance Performance { get; set; } = null!;

    // Зв'язок із залом
    public int HallId { get; set; }
    public virtual DanceHall Hall { get; set; } = null!;

    // Час проведення та ціна
    public DateTime StartDateTime { get; set; }

    // Використовуємо decimal для грошей (у БД буде money)
    public decimal DropinPrice { get; set; }

    // Журнал відвідувань для цього конкретного заняття
    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
}