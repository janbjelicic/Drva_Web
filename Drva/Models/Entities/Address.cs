using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.Entities
{
    public class Address
    {
        public int AddressID { get; set; }
        public String PostNumber { get; set; }
        public String StreetAndNumber { get; set; }
        public String Place { get; set; }
        public String District { get; set; }
    }
}