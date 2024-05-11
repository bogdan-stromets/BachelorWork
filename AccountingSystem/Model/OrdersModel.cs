using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class OrdersModel
    {
        public int id { get; set; }
        public List<CartModel> cart { get; set; }
        public DateTime date { get; set; }
        public string order_info { get; set; }
        public bool is_delivered { get; set; }
        public decimal sum { get; set; }

        public OrdersModel()
        {
            
        }
        public OrdersModel(DataRow dr)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            date = DateTime.Parse(dr.ItemArray[2].ToString());
            order_info = dr.ItemArray[4].ToString();
            is_delivered = bool.Parse(dr.ItemArray[3].ToString());
            cart = new List<CartModel>();
            CartFill();
            sum = GetSum();
        }

        private decimal GetSum()
        {
            decimal sum = 0;
            foreach (CartModel cart in cart)
            {
                sum += cart.device.price * cart.amount;
            }
            return sum;
        }

        private void CartFill()
        {
            DataTable dt = new DB().GetTableData(TableName.cart.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.ItemArray[1].ToString() == id.ToString())
                    cart.Add(new CartModel(dr,this));
            }
        }

        public string GetUpdateCommand()
        {
            return $"UPDATE `{TableName.orders}` SET `is_delivered` = '{Convert.ToInt32(is_delivered)}' WHERE `{TableName.orders}`.`id_order` = {id}";
        }
    }
}
