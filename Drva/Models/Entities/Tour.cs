using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.Entities
{
    public class Tour
    {
        public DateTime start { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}