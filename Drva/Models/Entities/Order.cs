using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Unit> Units { get; set; }

    }
}