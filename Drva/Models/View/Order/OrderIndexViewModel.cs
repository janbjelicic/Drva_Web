using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.View
{
    public class OrderIndexViewModel
    {
        public int OrderID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public String Customer { get; set; }
        public List<String> Units { get; set; }
    }
}