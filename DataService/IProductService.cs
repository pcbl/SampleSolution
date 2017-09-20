using System.ServiceModel;
using DataRepository.Model;

namespace DataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {      

        [OperationContract]
        Product[] GetProducts();

        [OperationContract]
        Product GetProductById(int id);
    }
    
}
