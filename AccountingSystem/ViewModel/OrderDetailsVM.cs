using AccountingSystem.Model;
using AccountingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.ViewModel
{
    class OrderDetailsVM : Utilities.ViewModelBase
    {
        private static OrdersModel currentOrder;
        public static ObservableCollection<DeviceModel> devices { get; set; }
        public OrdersModel Order 
        { 
            get { return currentOrder; }
            set { currentOrder = value; OnPropertyChanged(); }
        }
        public OrderDetailsVM()
        {
        }


        public OrderDetailsVM(OrdersModel order)
        {
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
    }
}
