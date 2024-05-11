using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using AccountingSystem.Model;
using AccountingSystem.Utilities;

namespace AccountingSystem.ViewModel
{
    class ProductVM : ViewModelBase, IDbSort
    {
        public ICommand SortCommand { get ; set; }
        public ObservableCollection<DeviceModel> devices { get; private set; }
        private DataTable dt;
        private bool ascSort;
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

        public ProductVM()
        {
            SortCommand = new RelayCommand(SortType);
            InitVM(TableName.devices.ToString());
        }

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
