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
        public List<DeviceModel> devices { get; set; }
        public int amount { get; set; }

        public CartModel(OrdersModel targetOrder)
        {
            order = targetOrder;
            DB dB = new DB();
            DataTable dt = dB.GetTableData(TableName.cart.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr.ItemArray[1].ToString()) == order.id)
                {
                    //devices.Add();
                }
            }
            devices = new List<DeviceModel>();
        }
    }
}
