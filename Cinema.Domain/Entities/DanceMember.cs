using Microsoft.AspNetCore.Identity;

namespace onlineCinema.Domain.Entities;

public class DanceMember : IdentityUser
{
    // Додаткові поля профілю
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Поле Email вже є в IdentityUser, тому закоментоване було правильним рішенням.
    // PhoneNumber також є в IdentityUser, ключове слово 'new' прибираємо, 
    // щоб не перекривати базову логіку Identity.

    public DateTime? DateOfBirth { get; set; }

    // Зв'язки (навігаційні властивості)
    public virtual ICollection<CostumeBooking> CostumeBookings { get; set; } = new List<CostumeBooking>();

    // Додав зворотний зв'язок із журналом відвідувань, щоб знати, які заняття відвідав юзер
    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
}