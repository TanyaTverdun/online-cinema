namespace onlineCinema.Domain.Entities;

public class Dancer
{
    // Первинний ключ
    public int DancerId { get; set; }

    // ПІБ (прибрав префікс Dancer, бо він надлишковий всередині класу Dancer)
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;

    // Зв'язок "багато-до-багатьох" із виступами
    public virtual ICollection<PerformanceDancers> PerformanceDancers { get; set; } = new List<PerformanceDancers>();

    // Додав зв'язок із логами відвідувань, щоб ми знали, хто саме був на занятті
    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
}