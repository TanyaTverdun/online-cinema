using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Domain.Enums
{
    public enum PerformanceStatus : byte
    {
        InPreparation = 1, // В процесі підготовки
        Active = 2,        // Готовий до виступів
        Archived = 3       // В архіві
    }
}
