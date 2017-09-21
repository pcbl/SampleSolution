using Autofac;
using NUnit.Framework;

namespace DataService.Tests
{
    [TestFixture]
    public class ProductServiceTests: TestBase
    {
        [Test]
        public void WhenGetProductsExpectAtLeastOne()
        {            
            var service = Container.Resolve<IProductService>();
            var products = service.GetProducts();
            CollectionAssert.IsNotEmpty(products);
        }


        [Test]
        public void WhenGet1ProductsExpectOne()
        {
            var service = Container.Resolve<IProductService>();
            int expectedId = 1;
            var product = service.GetProductById(expectedId);
            Assert.AreEqual(expectedId, product.Id);
        }
    }
}
