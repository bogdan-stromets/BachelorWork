using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class CartModel
    {
        public int id { get; set; }
        public OrdersModel order { get; set; }
        public DeviceModel device { get; set; }
        public int amount { get; set; }

        public CartModel(DataRow dr,OrdersModel order)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            this.order = order;
            device = new DeviceModel(int.Parse(dr.ItemArray[2].ToString()));
            amount = int.Parse(dr.ItemArray[3].ToString());
        }
    }
}
