using MvcBasic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcBasic.Controllers
{ 
    public class TestController : Controller
    {
        // eg Home/Index
        public ActionResult Index()
        {

            MvcTutorialEntities db = new MvcTutorialEntities();

            List<EmployeeViewModel> list = db.tblEmployees.Select(x => new EmployeeViewModel { Name = x.Name, EmployeeID = x.EmployeeID, DepartmentName = x.tblDepartment.DepartmentName, Address = x.Address }).ToList();

            ViewBag.EmployeeList = list;

            //ViewBag.CountryList =new SelectList(GetCountryList(),"CountryID","CountryName");
            return View();

            //List<MyShop> ItemList = new List<MyShop>();

            //ItemList.Add(new MyShop { ItemId = 1, ItemName = "Rice", isAvailable = true });
            //ItemList.Add(new MyShop { ItemId = 2, ItemName = "Pulse", isAvailable = false });
            //ItemList.Add(new MyShop { ItemId = 3, ItemName = "Salt", isAvailable = true });
            //ItemList.Add(new MyShop { ItemId = 4, ItemName = "Sugar", isAvailable = false });


            //ViewBag.ItemList = ItemList;
            //return View();
        }

        // where we are using function overloaded method
        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {
            try
            {
                MvcTutorialEntities db = new MvcTutorialEntities();
                List<tblDepartment> departments = db.tblDepartments.ToList();
                ViewBag.DeparmentList = new SelectList(departments, "DepartmentId", "DepartmentName");

                if (model.EmployeeID > 0)
                {   //update
                    tblEmployee emp = db.tblEmployees.SingleOrDefault(x => x.EmployeeID == model.EmployeeID && x.isDeleted == false);

                    emp.DepartmentId = model.DepartmentId;
                    emp.Name = model.Name;
                    emp.Address = model.Address;
                    db.SaveChanges();
                }
                else
                {   //insert
                    tblEmployee emp = new tblEmployee();
                    emp.Address = model.Address;
                    emp.Name = model.Name;
                    emp.DepartmentId = model.DepartmentId;
                    emp.isDeleted = false;
                    db.tblEmployees.Add(emp);
                    db.SaveChanges();
                }
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult DeleteEmployee(int EmployeeId)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();
            bool result = false;

            tblEmployee emp = db.tblEmployees.SingleOrDefault(x => x.isDeleted == false && x.EmployeeID == EmployeeId);

            if (emp != null)
            {
                emp.isDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEmployee(int EmployeeId)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();
            List<EmployeeViewModel> listEmp = db.tblEmployees.Where(x => x.isDeleted == false && x.EmployeeID == EmployeeId).Select(x => new EmployeeViewModel
            {
                Name = x.Name,
                DepartmentName = x.tblDepartment.DepartmentName,
                Address = x.Address,
                EmployeeID = x.EmployeeID

            }).ToList();

            ViewBag.EmployeeList = listEmp;

            return PartialView("Partial1");
        }

        public ActionResult AddEditEmployee(int EmployeeId)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();

            List<tblDepartment> departments = db.tblDepartments.ToList();

            ViewBag.DeparmentList = new SelectList(departments, "DepartmentId", "DepartmentName");


            EmployeeViewModel model = new EmployeeViewModel();

            //edit
            if (EmployeeId > 0)
            {
                tblEmployee emp = db.tblEmployees.SingleOrDefault(x => x.EmployeeID == EmployeeId && x.isDeleted == false);
                model.EmployeeID = EmployeeId;
                model.DepartmentId = emp.DepartmentId;
                model.Name = emp.Name;
                model.Address = emp.Address;
            }
            // add


            return PartialView("Partial2", model);
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegistrationViewModel model)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();

            tblSiteUser siteUser = new tblSiteUser();

            siteUser.UserName = model.UserName;
            siteUser.EmailId = model.EmailId;
            siteUser.Address = model.Address;
            siteUser.Password = model.Password;
            siteUser.RoleId = 3;

            db.tblSiteUsers.Add(siteUser);
            db.SaveChanges();

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public JsonResult LoginUser(LoginViewModel model)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();

            tblSiteUser user = db.tblSiteUsers.SingleOrDefault(x => x.EmailId == model.EmailId && x.Password == model.Password);
            string result = "Fail";
            if (user != null)
            {
                Session["userId"] = user.UserId;
                Session["userName"] = user.UserName;
                if (user.RoleId == 3)
                {
                    result = "GeneralUser";
                }
                else if (user.RoleId == 1)
                {
                    result = "Admin";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult SideMenu()
        {
            List<MenuItem> list = new List<MenuItem>();

            list.Add(new MenuItem { Link = "/Test/Index", LinkName = "Home" });
            list.Add(new MenuItem { Link = "/Test/Login", LinkName = "Login" });
            list.Add(new MenuItem { Link = "/Test/Registration", LinkName = "Registration" });

            return PartialView("SideMenu", list);
        }

        public JsonResult ImageUpload(ProductViewModel model)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();
            int imageId = 0;
            var file = model.ImageFile;
            byte[] imagebyte = null;
            if (file != null)
            {

                file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName));

                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);

                ImageStore img = new ImageStore();

                img.ImageName = file.FileName;
                img.ImageByte = imagebyte;
                img.ImagePath = "/UploadedImage/" + file.FileName;
                img.IsDeleted = false;


                db.ImageStores.Add(img);
                db.SaveChanges();
                imageId = img.ImageId;

            }
            if (model.ImageUrl != null)
            {
                imagebyte = DownloadImage(model.ImageUrl);

                ImageStore img = new ImageStore();

                img.ImageName = "Abc";
                img.ImageByte = imagebyte;
                img.ImagePath = model.ImageUrl;
                img.IsDeleted = false;


                db.ImageStores.Add(img);
                db.SaveChanges();
                imageId = img.ImageId;
            }

            return Json(imageId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImageRetrieve(int imgID)
        {

            MvcTutorialEntities db = new MvcTutorialEntities();

            var img = db.ImageStores.SingleOrDefault(x => x.ImageId == imgID);
            return File(img.ImageByte, "image/jgp");


        }

        public byte[] DownloadImage(string Url)
        {
            var webClient = new WebClient();
            byte[] imagebytes = webClient.DownloadData(Url);
            return imagebytes;
        }

        public List<Country> GetCountryList()
        {
            MvcTutorialEntities db = new MvcTutorialEntities();
            List<Country> countries = db.Countries.ToList();
            return countries;
        }

        public ActionResult GetStateList(int CountryID)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();
            List<State> stateList = db.States.Where(x => x.CountryId == CountryID).ToList();

            ViewBag.StateOption = new SelectList(stateList, "StateId", "StateName");
            return PartialView("StateOptionPartial");
        }

        public ActionResult GetSeachRecord(string SearchText)
        {
            MvcTutorialEntities db = new MvcTutorialEntities();
            List<EmployeeViewModel> list = db.tblEmployees.Where(x => x.Name.Contains(SearchText)|| x.Address.Contains(SearchText) || x.tblDepartment.DepartmentName.Contains(SearchText)).Select(x => new EmployeeViewModel { Name = x.Name, EmployeeID = x.EmployeeID, DepartmentName = x.tblDepartment.DepartmentName, Address = x.Address }).ToList();

            return PartialView("SearchPartial", list);


        }

        // eg Home/Student
        [Route("Student")]
        public string GetStudent()  {
            return "All student record";
        }

        [Route("Student/id/{id}")]
        public string GetStudent(int id)
        {
            return "Student with id=" + id;
        }

        [Route("Student/name/{name}")]
        public string GetStudent(string name)
        {
            return "Student with name=" + name;
        }

    }
}