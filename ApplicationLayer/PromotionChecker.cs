﻿using Domain;
using System;
using System.Linq;
using Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public class PromotionChecker:IPromotionChecker
    {

        public async Task<List<Product>> GetProducts()
        {
            return await DataContext.GetProducts();
        }
        //returns PromotionID and count of promotions
        public async Task<DiscountDto> GetTotalPrice(Order ord)
        {
            var productList = await DataContext.GetProducts();
            List<Product> products = ord.Products.Join(productList, d => d.Id, s => s.Id, (d, s) => {
                d.Price = s.Price;
                return d;
            }).ToList<Product>();

            var promotions = await DataContext.GetAvailablePromotions();
            List<decimal> PriceAfterDiscount = promotions
                   .Select(promo =>GetTotalPrice(products, promo))
                   .ToList();
            decimal originalPrice = products.Sum(x => x.Price);
            decimal totalPriceAfterDiscount = PriceAfterDiscount.Sum();
            decimal rebate = originalPrice - totalPriceAfterDiscount;
            DiscountDto discountDetails = new DiscountDto(ord.OrderID.ToString(), originalPrice.ToString(), totalPriceAfterDiscount.ToString(), rebate.ToString());
            return discountDetails;
           
        }

        private decimal GetTotalPrice(List<Product> products, Promotion promotion )
        {

            decimal d = 0M;
            //get count of promoted products in order
            var copp = products
                .GroupBy(x => x.Id)
                .Where(grp => promotion.ProductInfo.Any(y => grp.Key == y.Key && grp.Count() >= y.Value))
                .Select(grp => grp.Count())
                .Sum();

            //store in variable to calculate without promotion
            var noPromo = copp;

            //get count of promoted products from promotion
            int ppc = promotion.ProductInfo.Sum(kvp => kvp.Value);
            while (copp >= ppc)
            {
                d += promotion.PromoPrice;
                copp -= ppc;
            }
            //get the total price
            if (noPromo == 0 || (copp != 0 && copp < ppc))
            {
                decimal prodPrice = 0;
                var key = products
                    .GroupBy(x => x.Id)
                     .Where(grp => promotion.ProductInfo.Any(y => grp.Key == y.Key));

                if (copp != 0)
                {
                    prodPrice = key.Select(s => s.First().Price).First();
                    d += (copp * prodPrice);
                }
                else
                {
                    d += key.Select(s => s.Sum(v => v.Price))
                    .Sum();

                }

            }
            return d;

        }

       
    }
}
