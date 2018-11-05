using MvcTuto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTuto.Business.Interface
{
    public interface IEmployeeBusiness
    {
         
        List<EmployeeDomainModel> GetAllEmployee();
        string AddUpdateEmployee(EmployeeDomainModel emp);
    }
}
