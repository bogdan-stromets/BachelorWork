using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using AccountingSystem.Model;
using AccountingSystem.Utilities;

namespace AccountingSystem.ViewModel
{
    class ProductVM : ViewModelBase, IDbSort, IDbSearch
    {
        public ICommand SearchCommand { get; set; }
        public ICommand SortCommand { get ; set; }
        public ObservableCollection<DeviceModel> devices { get; private set; }
        private DataTable dt;
        private bool ascSort;
        private string searchText;
        public DataTable DevicesData
        {
            get { return dt; }
            private set { dt = value; OnPropertyChanged(); }
        }
        public bool Sort
        {
            get => ascSort;
            set { ascSort = value; OnPropertyChanged(); }
        }
        public string SearchText
        {
            get => searchText;
            set { searchText = value; OnPropertyChanged();}
        }
        public ProductVM()
        {
            InitCommands();
            InitVM(TableName.devices.ToString());
        }
        private void InitCommands()
        {
            SortCommand = new RelayCommand(SortType);
            SearchCommand = new RelayCommand(Search);
        }
        public void Search(object obj) => InitVM(command: SearchStringCommand(TableName.devices, SearchText,new string[] {"id_device","name", "amount" }),sort: true);
        protected override void InitVM(string tableName = "", string command = "", bool sort = false)
        {
            DevicesData = new DB().GetTableData(tableName, sort, command);
            
            if (devices == null)
                devices = new ObservableCollection<DeviceModel>();
            else
                devices.Clear();

            foreach (DataRow dr in DevicesData.Rows)
            {
                devices.Add(new DeviceModel(dr));
            }
        }
        public void AscendingSort(string colName) => InitVM(command: $"SELECT* FROM `{TableName.devices}` ORDER BY `{TableName.devices}`.`{colName}` ASC", sort: true);

        public void DescendingSort(string colName) => InitVM(command: $"SELECT* FROM `{TableName.devices}` ORDER BY `{TableName.devices}`.`{colName}` DESC", sort: true);

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
                "№" => "id_device",
                "Name" => "name",
                "Manufacturer" => "id_manufacturer",
                "Category" => "id_category",
                "Stock" => "amount",
                _ => targetValue,
            };
        }
    }
}
