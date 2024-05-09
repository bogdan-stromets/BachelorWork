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
        public CartModel cart { get; set; }
        public DateTime date { get; set; }
        public string order_info { get; set; }
        public bool is_delivered { get; set; }
        public decimal sum { get; set; }

        public OrdersModel(DataRow dr)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            cart = new CartModel(this);
            date = DateTime.Parse(dr.ItemArray[2].ToString());
            order_info = dr.ItemArray[4].ToString();
            is_delivered = bool.Parse(dr.ItemArray[3].ToString());
        }


    }
}
