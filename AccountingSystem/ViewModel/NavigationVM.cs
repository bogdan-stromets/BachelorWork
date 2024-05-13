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
        private object _currentView,_prevView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }       
        public object PrevView
        {
            get { return _prevView; }
            set { _prevView = value; OnPropertyChanged(); }
        }
        public static object CurrentObject { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public static ICommand ProductsStaticCommand { get; set; }
        public ICommand ProductDetailsCommand { get; set; }
        public ICommand MostProductCommand { get; set; }
        public ICommand LeastProductCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand OrderDetailsCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand PrevPageCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Customer(object obj) => CurrentView = new CustomerVM();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void Order(object obj) => CurrentView = new OrderVM();
        private void ProductDetails(object obj) 
        {
            CurrentObject = obj;
            PrevView = CurrentView;
            CurrentView = new ProductDetailsVM();
        }
        private void MostProductDetails(object obj)
        {
            PrevView = CurrentView;
            CurrentObject = (obj as StatisticsVM).Stats.most_popular_product;
            CurrentView = new ProductDetailsVM();
        }      
        private void LeastProductDetails(object obj)
        {
            PrevView = CurrentView;
            CurrentObject = (obj as StatisticsVM).Stats.least_popular_product;
            CurrentView = new ProductDetailsVM();
        }
        private void OrderDetails(object obj)  
        {
            CurrentObject = obj;
            PrevView = CurrentView;
            CurrentView = new OrderDetailsVM();
        }
        private void Statistics(object obj) => CurrentView = new StatisticsVM();
        private void Shipment(object obj) => CurrentView = new ShipmentVM();
        private void Setting(object obj) => CurrentView = new SettingVM();
        private void PrevPage(object obj) => CurrentView = PrevView;
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            ProductsCommand = new RelayCommand(Product);
            ProductsStaticCommand = new RelayCommand(Product);
            ProductDetailsCommand = new RelayCommand(ProductDetails);
            OrdersCommand = new RelayCommand(Order);
            OrderDetailsCommand = new RelayCommand(OrderDetails);
            StatisticsCommand = new RelayCommand(Statistics);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);
            MostProductCommand = new RelayCommand(MostProductDetails);
            LeastProductCommand = new RelayCommand(LeastProductDetails);
            PrevPageCommand = new RelayCommand(PrevPage);
            // Startup Page
            CurrentView = new HomeVM();
        }

        public static void ExecuteProductCommand()
        {
            ProductsStaticCommand.Execute(null);
        }
    }
}
