using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;

namespace Persistance
{
    public static class DataContext
    { 
        public static async Task<List<Product>> GetProducts()
        {
            var filePath =  Directory.GetParent(ToApplicationPath()).FullName  + "\\Persistance\\Products.json";            

            //List<Product> products = await Task.FromResult(JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Path.Combine( Directory.GetParent( Directory.GetCurrentDirectory()).FullName, "Products.json"))));
            List<Product> products = await Task.FromResult(JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText( filePath)));
            return products;
        }
        public static async Task<List<Promotion>> GetAvailablePromotions()
        {
            var filePath = Directory.GetParent(ToApplicationPath()).FullName + "\\Persistance\\PromotionPrice.json";
            List<Promotion> promotion = await Task.FromResult(JsonConvert.DeserializeObject<List<Promotion>>(File.ReadAllText(filePath)));
            return promotion;
        }

        public static string ToApplicationPath()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                                .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

    }
}
