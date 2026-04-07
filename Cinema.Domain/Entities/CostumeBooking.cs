namespace onlineCinema.Domain.Entities
{
    public class CostumeBooking
    {
        // Первинний ключ
        public int BookingId { get; set; }

        // Заміри (важливо для пошиття/підбору костюма)
        public string Measurements { get; set; } = string.Empty;

        // Дата бронювання або дата, на коли потрібен костюм
        public DateTime BookingDate { get; set; }

        // Зв'язок з користувачем (Identity)
        // Використовуємо string, бо в Identity за замовчуванням Id - це string (Guid)
        public string ApplicationUserId { get; set; } = string.Empty;
        public virtual DanceMember ApplicationUser { get; set; } = null!;

        // Фінансова транзакція (оплата оренди/застави)
        public int? PaymentId { get; set; }
        public virtual FinancialTransaction? FinancialTransaction { get; set; }

        // Зв'язки
        public virtual ICollection<MerchOrder> MerchOrders { get; set; } = new List<MerchOrder>();
        public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
    }
}