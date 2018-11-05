using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcBasic.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Department")]
        public Nullable<int> DepartmentId { get; set; }

        //Custom attribute 
        public string DepartmentName { get; set; }

        public bool Remember { get; set; }

        public string SiteName { get; set; }

        public string ExtraValue { get; set; }

        public string CurrentDate { get; set; }
    }
}