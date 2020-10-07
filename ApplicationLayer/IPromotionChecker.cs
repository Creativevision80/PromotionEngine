using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ApplicationLayer
{
    public interface IPromotionChecker
    {
        public Task<DiscountDto> GetTotalPrice(Order ord);
        public Task<List<Product>> GetProducts();
    }
}