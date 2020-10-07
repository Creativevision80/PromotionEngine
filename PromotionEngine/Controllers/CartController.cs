using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationLayer;
using Newtonsoft.Json;
using Domain;
using Application.Errors;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PromotionEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly IPromotionChecker _promotionChecker;
        public CartController(IPromotionChecker promotionChecker)
        {
            _promotionChecker = promotionChecker;
        }
        // GET: api/<CartController>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var response= await _promotionChecker.GetProducts();
            if (response == null)
                throw new RestException(HttpStatusCode.NotFound, new { Products = "Not found" });
            return response;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<DiscountDto>> CheckOut([FromBody] Order order)
        { 
                if (order != null || order.Products.Any())
                {
                    if (null == order.OrderID)
                    {
                        throw new Exception("OrderId should be supplied");
                    }
                    foreach(Product p in order.Products)
                    {
                        if (string.IsNullOrEmpty(p.Id))
                        {
                            throw new Exception("Product Id should be supplied");
                        }
                    }
                }

                var response = await _promotionChecker.GetTotalPrice(order);
                if (response == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Products = "Not found" });
            return response;
            
                
        }
    }
}
