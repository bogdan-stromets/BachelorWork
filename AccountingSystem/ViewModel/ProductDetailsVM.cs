using AccountingSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.ViewModel
{
    class ProductDetailsVM : Utilities.ViewModelBase
    {
        private DeviceModel _device;
        public DeviceModel CurrentDevice
        {
            get { return _device; }
            private set { _device = value; OnPropertyChanged(); }
        }
        public ProductDetailsVM()
        {
            DeviceModel device = NavigationVM.CurrentObject as DeviceModel;
            CurrentDevice = new DeviceModel(device.id);
        }
    }
}
