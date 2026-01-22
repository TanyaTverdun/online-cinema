using onlineCinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Interfaces
{
    public interface ISnackRepository
    {

        Task<IEnumerable<Snack>> GetAllAsync();
        Task<Snack?> GetByIdAsync(int id);
    }
}
