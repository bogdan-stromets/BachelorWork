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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Xml.Serialization;
using AccountingSystem.Model;
using AccountingSystem.Utilities;
using AccountingSystem.View;

namespace AccountingSystem.ViewModel
{
    class OrderVM : ViewModelBase, IDbSort, IDbSearch
    {
        public ICommand SortCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ObservableCollection<OrdersModel> Buttons { get; private set; }
        private DataTable dt;
        private bool ascSort;
        private string searchText;

        #region Header Properties
        private string header_num = "№", header_customer = "Customer", header_state = "State", header_order_date = "Order Date", sortPointer = "";
        public string Header_num 
        {
            get { return header_num + sortPointer; }
            set { header_num = value; OnPropertyChanged(); }
        }
        public string Header_customer
        {
            get { return header_customer; }
            set { header_customer = value; OnPropertyChanged(); }
        }        
        public string Header_state
        {
            get { return header_state + sortPointer; }
            set { header_state = value; OnPropertyChanged(); }
        }        
        public string Header_order_date
        {
            get { return header_order_date + sortPointer; }
            set { header_order_date = value; OnPropertyChanged(); }
        }
        #endregion

        public string SearchText
        {
            get => searchText;
            set { searchText = value; OnPropertyChanged(); }
        }
        public bool Sort 
        {
            get => ascSort;
            set { ascSort = value; OnPropertyChanged(); }
        }
        public DataTable OrdersData
        {
            get { return dt; }
            private set { dt = value; OnPropertyChanged(); }
        }

        public OrderVM()
        {
            InitCommands();
            InitVM(TableName.orders.ToString());
        }

        protected override void InitCommands()
        {
            SortCommand = new RelayCommand(SortType);
            SearchCommand = new RelayCommand(Search);
        }

        protected override void InitVM(string tableName = "", string command = "", bool sort = false)
        {
            OrdersData = new DB().GetTableData(tableName,sort,command);
          
            if (Buttons == null)
                Buttons = new ObservableCollection<OrdersModel>();
            else 
                Buttons.Clear();

            foreach (DataRow dr in OrdersData.Rows)
            {
                Buttons.Add(new OrdersModel(dr));
            }
        }
        public void AscendingSort(string colName) => InitVM(command: $"SELECT* FROM `{TableName.orders}` ORDER BY `{TableName.orders}`.`{colName}` ASC", sort: true);

        public void DescendingSort(string colName) => InitVM(command: $"SELECT* FROM `{TableName.orders}` ORDER BY `{TableName.orders}`.`{colName}` DESC", sort: true);
       
        public void SortType(object obj)
        {
            Sort = !Sort;
            if (Sort)
                AscendingSort(NameConverter(obj.ToString()));
            else
                DescendingSort(NameConverter(obj.ToString()));
        }

        public string NameConverter(string targetValue)
        {
            return targetValue switch
            {
                "№" => "id_order",
                "Order Date" => "date",
                "Customer" => "customer_full_name",
                "State" => "is_delivered",
                _ => targetValue,
            };
        }

        public void Search(object obj) => InitVM(command: SearchStringCommand(TableName.orders, SearchText, new string[] { "id_order", "order_info", "customer_full_name", "is_delivered"}), sort: true);
    }
}
