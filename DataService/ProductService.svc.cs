using System.Linq;
using DataRepository;
using DataRepository.Model;

namespace DataService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public Product[] GetProducts()
        {
            return _repository.Get().ToArray();
        }

        public Product GetProductById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
