using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Domain.Enums
{
    public enum AgeRating : byte
    {
        [Display(Name = "0+")]
        Age0 = 0,
        [Display(Name = "6+")]
        Age6 = 6,
        [Display(Name = "13+")]
        Age13 = 13,
        [Display(Name = "16+")]
        Age16 = 16,
        [Display(Name = "18+")]
        Age18 = 18
    }
}
