using AccountingSystem.Model;
using AccountingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AccountingSystem.ViewModel
{
    class OrderDetailsVM : Utilities.ViewModelBase
    {
        private OrdersModel currentOrder;
        public ICommand deliverCommand { get; set; }
        public  ObservableCollection<DeviceModel> devices { get; set; }
        
        public int order_id { get => Order.id; }
        public List<CartModel> order_cart { get => Order.cart; }
        public DateTime order_date { get => Order.date; }
        public string order_info { get => Order.order_info; }
        public bool is_delivered { get => Order.is_delivered; set { Order.is_delivered = value; OnPropertyChanged(); } }
        public decimal order_sum { get => Order.sum; }


        public OrdersModel Order 
        { 
            get { return currentOrder; }
            set { currentOrder = value; OnPropertyChanged(); }
        }

        public OrderDetailsVM()
        {
            deliverCommand = new RelayCommand(Deliver);
            OrdersModel order = NavigationVM.CurrentObject as OrdersModel;
            Order = new OrdersModel()
            {
                id = order.id,
                cart = new List<CartModel>(order.cart),
                date = order.date,
                order_info = order.order_info,
                is_delivered = order.is_delivered,
                sum = order.sum
            };
            devices = new ObservableCollection<DeviceModel>();
            CollectionFill();
        }
        private void CollectionFill()
        {
            foreach (CartModel cart in Order.cart)
            {
                devices.Add(cart.device);
            }
        }

        private void Deliver(object obj)
        {
            DB dB = new DB();
            try
            {
                Order.cart.ForEach(cart => cart.device.stock_size += is_delivered ? -cart.amount : cart.amount);
                Order.cart.ForEach(cart => dB.ExecuteSQLCommand(cart.device.GetUpdateCommand()));
                dB.ExecuteSQLCommand(Order.GetUpdateCommand());
            }
            catch (Exception)
            {
                MessageBox.Show("Not enough products in stock!","Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
                Order.cart.ForEach(cart => cart.device.stock_size += cart.amount);
                is_delivered = false;
            }
        }
    }
}
