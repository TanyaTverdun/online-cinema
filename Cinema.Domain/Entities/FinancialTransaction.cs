using onlineCinema.Domain.Enums;

namespace onlineCinema.Domain.Entities;

public class FinancialTransaction
{
    // Первинний ключ
    public int PaymentId { get; set; }

    // Сума платежу (у БД буде тип money)
    public decimal Amount { get; set; }

    // Дата та час проведення транзакції
    public DateTime PaymentDate { get; set; }

    // Статус (наприклад: Pending, Completed, Failed, Refunded)
    public PaymentStatus Status { get; set; }

    // Тип або опис платежу (наприклад, "Оренда костюма", "Абонемент", "Мерч")
    public string? Description { get; set; }

    // Зв'язок із бронюванням костюма (необов'язково)
    public int? BookingId { get; set; }
    public virtual CostumeBooking? CostumeBooking { get; set; }

    // Додав зворотний зв'язок із журналом відвідувань (якщо оплата йде за разовий візит)
    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
}