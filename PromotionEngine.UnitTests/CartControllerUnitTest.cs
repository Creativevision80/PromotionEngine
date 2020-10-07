using System;
using System.Collections.Generic;
using ApplicationLayer;
using PromotionEngine.Controllers;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Moq;
using Xunit;
using Assert = Xunit.Assert;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PromotionEngine.UnitTests
{
    [TestClass]
    public class CartControllerUnitTest
    {
        Order order = null;
        List<Product> products = null;
        DiscountDto discountDto = null;
        public CartControllerUnitTest()
        {
            //create list of products to reuse
            products = new List<Product>()
            { new Product(){Id="A",Price=50}};
            //add it to Order
            order = new Order() { OrderID = 1, Products = products };
            discountDto = new DiscountDto("1", "50", "50","0");
        }
        [Fact]
        public async void OrderIDNullTest()
        {
            try
            {
                //Arrange
                var mockRepo = new Moq.Mock<IPromotionChecker>();
                CartController cartController = new CartController(mockRepo.Object);
                order.OrderID = null;
                //Act
                var response = await cartController.CheckOut(order);
            }
            catch(Exception ex)
            {
                //assert
                Assert.Equal("OrderId should be supplied", ex.Message);
            }
            
           


        }
        [Fact]
        public async void ProductIDNullTest()
        {
            try
            {
                //Arrange
                var mockRepo = new Moq.Mock<IPromotionChecker>();
                CartController cartController = new CartController(mockRepo.Object);
                order.Products[0].Id = null;
                //Act
                var response = await cartController.CheckOut(order);
            }
            catch(Exception ex)
            {
                //Assert
                Assert.Equal("Product Id should be supplied", ex.Message);
            }
            


        }
        [Fact]
        public async void CheckOutCalled()
        {
            
            //Arrange
            var mockRepo = new Moq.Mock<IPromotionChecker>();
            mockRepo.Setup(x => x.GetTotalPrice(order)).Returns(Task.FromResult(discountDto));
            CartController cartController = new CartController(mockRepo.Object);
            //Act
            var discount = await cartController.CheckOut(order);
            //Assert
            Assert.Equal("1", discount.Value.OrderId );
            Assert.Equal("50", discount.Value.OriginalPrice);
            Assert.Equal("50", discount.Value.TotalPriceAfterDiscount);
            Assert.Equal("0", discount.Value.Rebate);
        }
    }
}
