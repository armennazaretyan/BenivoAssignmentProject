using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.InfoServices
{
    public class BaseService
    {
        protected IUnitOfWork unit;

        protected BaseService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
    }
}
