using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.Entities
{
    public class PhoneNumber
    {
        public int PhoneNumberID { get; set; }
        public String Number { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}