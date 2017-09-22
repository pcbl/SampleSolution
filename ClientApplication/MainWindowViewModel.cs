using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientApplication.ProductDataService;

namespace ClientApplication
{
    public class MainWindowViewModel: INotifyPropertyChanged
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

            UserName = "StartName";

            ChangeUserName = new RelayCommand((parameter) =>
            {
                UserName = $"User_{DateTime.Now.Ticks}";
            });
        }

        public ObservableCollection<Product> Products { get; set; }

        public RelayCommand Load { get; set; }

        public RelayCommand ChangeUserName { get; set; }

        public string UserName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
