using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;

namespace onlineCinema.Controllers
{
    public class FeatureController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeatureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
