using NUnit.Framework;
using DataRepository;
using DataRepository.Model;
using Rhino.Mocks;

namespace DataService.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _service;
        [SetUp]
        public void Setup()
        {        
            var mockedProductsRepo = MockRepository.GenerateStub<IRepository<Product>>();
            mockedProductsRepo.Stub(x => x.Get()).Return(new[]
            {
                new Product {Id = 1, Name = "Apple", Category = "Fruit"}
            });

            mockedProductsRepo.Stub(x => x.GetById(Arg<int>.Is.Anything))
                .WhenCalled(call=> call.ReturnValue = new Product { Id = (int)call.Arguments[0], Name = "Apple", Category = "Fruit" })
                .Return(null);

            //_service = new ProductService(mockedProductsRepo);

            _service = new ProductService(new ProductRepository());
        }

        [Test]
        public void WhenGetProductsExpectAtLeastOne()
        {
            var products = _service.GetProducts();
            CollectionAssert.IsNotEmpty(products);
        }


        [Test]
        public void WhenGet1ProductsExpectOne()
        {
            int expectedId = 1;
            var product = _service.GetProductById(expectedId);
            Assert.AreEqual(expectedId, product.Id);
        }
    }
}
