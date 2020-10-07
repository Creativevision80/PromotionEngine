using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public Dictionary<string, int> ProductInfo { get; set; }
        public decimal PromoPrice { get; set; }
    }
}
