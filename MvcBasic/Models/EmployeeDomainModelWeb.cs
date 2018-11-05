using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBasic.Models
{
    public class EmployeeDomainModelWeb
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Address { get; set; }
        public Nullable<int> IsDeleted { get; set; }

        //Custom attribute
        public string DepartmentName { get; set; }
        public bool Remember { get; set; }
        public string SiteName { get; set; }

        public int ExtraValue { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}