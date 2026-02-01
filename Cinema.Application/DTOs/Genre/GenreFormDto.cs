using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.Genre
{
    public class GenreFormDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;
    }
}
