using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Domain.Enums
{
    public enum ConditionStatus : byte
    {
        New = 1,
        Used = 2,
        NeedsRepair = 3,
        Broken = 4
    }
}
