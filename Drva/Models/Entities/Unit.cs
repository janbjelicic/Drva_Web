using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drva.Models.Entities
{
    public class Unit
    {
        public int UnitID { get; set; }
        public String WoodType { get; set; }
        public String Price { get; set; }
        public String Amount { get; set; }
        public String SawingType { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

    }
}