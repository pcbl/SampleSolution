using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.ProductDataService;

namespace ClientApplication
{
    public class MainWindowViewModel
    {
        private readonly ProductServiceClient _service;

        public MainWindowViewModel()
        {
            _service = new ProductServiceClient();
            Products = new ObservableCollection<Product>();

            Load = new RelayCommand((parameter) =>
            {
                Products.Clear();
                var allproducts = _service.GetProducts();
                foreach (var product in allproducts)
                {
                    Products.Add(product);
                }
            });
        }

        public ObservableCollection<Product> Products { get; set; }

        public RelayCommand Load { get; set; }

    }
}
