using MvcBasic.Models;
using MvcTuto.Domain;
using MVCTuto.Business;
using MVCTuto.Business.Interface;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcBasic.Controllers
{
    public class RepoController : Controller
    {
        IEmployeeBusiness empBusiness;
        //Using DI concept
        public RepoController(EmployeeBusiness _empBusiness)
        {
            empBusiness = _empBusiness;

        }
        // GET: Repo
        public ActionResult Index()
        {
            string result = AddEditEmployee();
            List<EmployeeDomainModel> listDomain = empBusiness.GetAllEmployee();

            List<EmployeeViewModel> listemployeeVM = new List<EmployeeViewModel>();

            AutoMapper.Mapper.Map(listDomain, listemployeeVM);

            ViewBag.EmployeeList = listemployeeVM;


            return View();
        }

        public string AddEditEmployee() {
            string result = "";
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.EmployeeID = 13;
            vm.Name = "Shovon";
            vm.Address = "Khulna";
            vm.DepartmentId = 2;

            EmployeeDomainModel empDomainModel = new EmployeeDomainModel();
            AutoMapper.Mapper.Map(vm,empDomainModel);

            result =empBusiness.AddUpdateEmployee(empDomainModel);
            return result;
         }
    }
}