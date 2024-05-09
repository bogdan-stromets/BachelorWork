using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using AccountingSystem.Model;
using AccountingSystem.Utilities;

namespace AccountingSystem.ViewModel
{
    class OrderVM : ViewModelBase
    {
        public ObservableCollection<OrdersModel> Buttons { get; }
        private DataTable dt;
        public DataTable OrdersData
        {
            get { return dt; }
            private set { dt = value; OnPropertyChanged(); }
        }
        public OrderVM()
        {
            OrdersData = new DB().GetTableData(TableName.orders.ToString());
            Buttons = new ObservableCollection<OrdersModel>();

            foreach (DataRow dr in OrdersData.Rows)
            {
                Buttons.Add(new OrdersModel(dr));
            }
        }
    }
}
