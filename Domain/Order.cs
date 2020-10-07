using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order
    {
        public int? OrderID { get; set; }
        public List<Product> Products { get; set; }

    }
}
