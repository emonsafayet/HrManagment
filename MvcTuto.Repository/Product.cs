//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcTuto.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> ImageId { get; set; }
    
        public virtual ImageStore ImageStore { get; set; }
    }
}
