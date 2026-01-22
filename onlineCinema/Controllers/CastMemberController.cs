using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;

namespace onlineCinema.Controllers
{
    public class CastMemberController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CastMemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
