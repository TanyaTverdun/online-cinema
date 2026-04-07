using System;
using System.Collections.Generic;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.AdminTickets;

public class PaymentAdminDto
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime OrderDate { get; set; }

    // Було MovieSessionDateTime
    public DateTime DanceClassDateTime { get; set; }
    public PaymentStatus Status { get; set; }
    public string UserEmail { get; set; } = string.Empty;

    // Було MovieTitle
    public string PerformanceTitle { get; set; } = string.Empty;

    // Кількість заброньованих місць/людей (було TicketCount)
    public int AttendanceCount { get; set; }

    // Інформація про записи (наприклад, "Рівень: Профі, Місце: 5")
    public List<string> AttendanceInfo { get; set; } = new();

    // Інформація про куплені товари (було SnacksInfo)
    public List<string> MerchInfo { get; set; } = new();
}