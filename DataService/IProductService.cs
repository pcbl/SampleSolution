using System.ServiceModel;
using DataRepository.Model;

namespace DataService
{
    [ServiceContract]
    public interface IProductService
    {      

        [OperationContract]
        Product[] GetProducts();

        [OperationContract]
        Product GetProductById(int id);
    }
    
}
