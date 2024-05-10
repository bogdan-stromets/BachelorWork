using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AccountingSystem.Model;
using AccountingSystem.Utilities;

namespace AccountingSystem.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand ProductDetailsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand OrderDetailsCommand { get; set; }
        public ICommand TransactionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Customer(object obj) => CurrentView = new CustomerVM();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void ProductDetails(object obj) => CurrentView = new ProductDetailsVM((obj as DeviceModel));
        private void Order(object obj) => CurrentView = new OrderVM();
        private void OrderDetails(object obj) => CurrentView = new OrderDetailsVM(obj as OrdersModel);
        private void Transaction(object obj) => CurrentView = new TransactionVM();
        private void Shipment(object obj) => CurrentView = new ShipmentVM();
        private void Setting(object obj) => CurrentView = new SettingVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            ProductsCommand = new RelayCommand(Product);
            ProductDetailsCommand = new RelayCommand(ProductDetails);
            OrdersCommand = new RelayCommand(Order);
            OrderDetailsCommand = new RelayCommand(OrderDetails);
            TransactionsCommand = new RelayCommand(Transaction);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
