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
    class ProductDetailsVM : Utilities.ViewModelBase
    {
        public ICommand DeleteCommand { get; set; }
        private DeviceModel _device;
        public DeviceModel CurrentDevice
        {
            get { return _device; }
            private set { _device = value; OnPropertyChanged(); }
        }
        private void DeleteProduct(object obj)
        { 
            DB db = new DB();
            MessageBoxResult result = MessageBox.Show("Do you really want to remove the product?","Warning",MessageBoxButton.YesNo,MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                db.ExecuteSQLCommand($"DELETE FROM {TableName.cart} WHERE `{TableName.cart}`.`id_device` = {CurrentDevice.id}");
                db.ExecuteSQLCommand($"TRUNCATE TABLE {TableName.statistics}");
                db.ExecuteSQLCommand($"DELETE FROM {TableName.devices} WHERE `{TableName.devices}`.`id_device` = {CurrentDevice.id}");
                NavigationVM.ExecuteProductCommand();
            }
        }

        public ProductDetailsVM()
        {
            DeleteCommand = new RelayCommand(DeleteProduct);
            DeviceModel device = NavigationVM.CurrentObject as DeviceModel;
            CurrentDevice = new DeviceModel(device.id);
        }
    }
}
