using System.Collections.ObjectModel;
using ClientApplication.ProductDataService;

namespace ClientApplication
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            var service = new ProductServiceClient();
            Products = new ObservableCollection<Product>();

            Load = new RelayCommand((parameter) =>
            {
                Products.Clear();
                var allproducts = service.GetProducts();
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
