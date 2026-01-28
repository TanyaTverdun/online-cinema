using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs
{
    public class SessionDto
    {
            public int SessionId { get; set; }
            public DateTime ShowingDatetime { get; set; }
            public decimal BasePrice { get; set; }
            public string MovieTitle { get; set; } = default!;
            public string MoviePosterImage { get; set; } = default!;
            public string HallName { get; set; } 
    }
    }


