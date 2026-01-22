using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Domain.Enums
{
    public enum PaymentStatus : byte
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Refunded = 3
    }
}
