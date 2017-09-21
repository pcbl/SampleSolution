using Autofac;
using DataRepository;
using DataRepository.Model;
using NUnit.Framework;
using Rhino.Mocks;


namespace DataService.Tests
{
    [TestFixture]
    public class TestBase
    {
        [SetUp]
        public virtual void BeforeTests()
        {
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterType<ProductService>().AsImplementedInterfaces();


            //Mocking and injecting Product Repo
            var mockedProductsRepo = MockRepository.GenerateStub<IRepository<Product>>();
            mockedProductsRepo.Stub(x => x.Get()).Return(new[]
            {
                new Product {Id = 1, Name = "Apple", Category = "Fruit"}
            });

            mockedProductsRepo.Stub(x => x.GetById(Arg<int>.Is.Anything))
                .WhenCalled(call => call.ReturnValue = new Product { Id = (int)call.Arguments[0], Name = "Apple", Category = "Fruit" })
                .Return(null);            

            builder.RegisterInstance(mockedProductsRepo).AsImplementedInterfaces();
                

            Container = builder.Build();
        }

        [TearDown]
        public virtual void AfterTests()
        {
            Container?.Dispose();
        }

        protected IContainer Container;
    }
}
