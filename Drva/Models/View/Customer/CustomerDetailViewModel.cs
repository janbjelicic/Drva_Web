using Drva.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.View
{
    public class CustomerDetailViewModel
    {
        public int CustomerID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumbers { get; set; }
        public Address Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}