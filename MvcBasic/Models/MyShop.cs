using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBasic.Models
{
    public class MyShop
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public bool isAvailable { get; set; }
    }
}