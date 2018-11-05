using MVCTutorial.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTuto.Repository
{
    public class EmployeeRepository : BaseRepository<tblEmployee>
    {
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
