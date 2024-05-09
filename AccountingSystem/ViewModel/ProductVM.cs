using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Model;
using AccountingSystem.Utilities;

namespace AccountingSystem.ViewModel
{
    class ProductVM : ViewModelBase
    {
        public ObservableCollection<DeviceModel> devices { get; }
        private DataTable dt;

        public DataTable DevicesData
        {
            get { return dt; }
            private set { dt = value; OnPropertyChanged(); }
        }

        public ProductVM()
        {
            DevicesData = new DB().GetTableData(TableName.devices.ToString());
            devices = new ObservableCollection<DeviceModel>();
            foreach (DataRow dr in DevicesData.Rows)
            {
                devices.Add(new DeviceModel(dr));
            }
        }
    }
}
