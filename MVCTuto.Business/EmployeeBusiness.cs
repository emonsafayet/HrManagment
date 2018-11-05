using MvcTuto.Domain;
using MvcTuto.Repository;
using MVCTuto.Business.Interface;
using MVCTutorial.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTuto.Business
{


    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EmployeeRepository empRepository;
        private readonly DepartmentRepository departmentRepository;

        public EmployeeBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            empRepository = new EmployeeRepository(unitOfWork);
            departmentRepository = new DepartmentRepository(unitOfWork);

        }

        public string GetEmployeeName(int EmpId) { return "Safayet" + EmpId; }


        public List<EmployeeDomainModel> GetAllEmployee()
        {

            List<EmployeeDomainModel> list = empRepository.GetAll().Select(m => new EmployeeDomainModel
            {
                EmployeeID = m.EmployeeID,
                Name = m.Name,
                DepartmentName = m.tblDepartment.DepartmentName,
                Address = m.Address
            }).ToList();
            return list;
        }

        public string AddUpdateEmployee(EmployeeDomainModel empModel)
        {
            string result = "";
            if (empModel.EmployeeID > 0)
            {
                tblEmployee emp = empRepository.SingleOrDefault(x => x.EmployeeID == empModel.EmployeeID);
                if (emp != null)
                {
                    emp.Name = empModel.Name;
                    emp.Address = empModel.Address;
                    emp.DepartmentId = empModel.DepartmentId;

                    empRepository.Update(emp);
                    result = "Updated";
                }
            }
            else
            {
                tblEmployee emp = new tblEmployee();
                emp.Name = empModel.Name;
                emp.Address = empModel.Address;
                emp.DepartmentId = empModel.DepartmentId;
                emp.isDeleted = false;

                var record = empRepository.Insert(emp);

                result = "Inserted";

            }

            return result;
        }
    }

}