using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer
{
    public class DiscountDto
    {
        public string OrderId { get; set; }
        public string OriginalPrice { get; set; }
        public string TotalPriceAfterDiscount { get; set; }
        public string Rebate { get; set; }

        public DiscountDto( string orderId, string originalPrice, string totalPriceAfterDiscount, string rebate)
        {
            OrderId = orderId;
            OriginalPrice = originalPrice;
            TotalPriceAfterDiscount = totalPriceAfterDiscount;
            Rebate = rebate;
        }
    }
}
