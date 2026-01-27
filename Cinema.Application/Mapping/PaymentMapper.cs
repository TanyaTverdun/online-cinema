using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class PaymentMapper
    {
        public Payment CreateCompletedPayment(int bookingId, decimal amount)
        {
            return new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Completed
            };
        }
    }
}
