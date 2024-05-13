using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class StatisticsModel
    {
        public int id { get; set; }
        public int all_sum { get; set; }
        public DeviceModel most_popular_product { get; set; }
        public DeviceModel least_popular_product { get; set; }
        public int deliveries_number { get; set; }

        private readonly DB dB;

        public StatisticsModel(DataRow dr = null)
        {
            dB = new DB();
            if (dr == null)
                CreateNewStats();
            else
                GetStats(dr);
        }

        private void GetStats(DataRow dr)
        {
            id = Convert.ToInt32(dr.ItemArray[0]);
            all_sum =  GetAllCash();
            most_popular_product = GetMostPopularProduct();
            least_popular_product = GetLeastPopularProduct();
            deliveries_number = GetDeliveriesNumber();
            SaveChanges();
        }

        private void SaveChanges()
        {
            string command = $"UPDATE `{TableName.statistics}` SET `all_sum` = '{all_sum}', `id_most_popular_product` = '{most_popular_product.id}', `id_least_popular_product` = '{least_popular_product.id}', `deliveries_number` = '{deliveries_number}' WHERE `statistics`.`id_stat` = {id};";
            dB.ExecuteSQLCommand(command);
        }
        private void CreateNewStats()
        {
            all_sum = GetAllCash();
            most_popular_product = GetMostPopularProduct();
            least_popular_product = GetLeastPopularProduct();
            deliveries_number = GetDeliveriesNumber();
            string command = $"INSERT INTO `{TableName.statistics}` (`id_stat`, `all_sum`, `id_most_popular_product`, `id_least_popular_product`, `deliveries_number`) VALUES (NULL, '{all_sum}', '{most_popular_product.id}', '{least_popular_product.id}', '{deliveries_number}');";
            dB.ExecuteSQLCommand(command);
        }

        private int GetDeliveriesNumber()
        {
            int deliveries = 0;
            DataTable dt = dB.GetTableData(TableName.orders.ToString());
            List<OrdersModel> orders = new List<OrdersModel>();

            foreach (DataRow dr in dt.Rows)
                orders.Add(new OrdersModel(dr));

            orders.ForEach(o => deliveries += Convert.ToInt32(o.is_delivered));
            return deliveries;
        }

        private DeviceModel GetLeastPopularProduct()
        {
            DeviceModel device = null;
            DataTable dt = dB.GetTableData(TableName.orders.ToString());
            List<OrdersModel> orders = new List<OrdersModel>();
            int counter = int.MaxValue;
            foreach (DataRow dr in dt.Rows)
                orders.Add(new OrdersModel(dr));

            foreach (OrdersModel o in orders)
                foreach (CartModel cart in o.cart)
                {
                    if (cart.amount < counter)
                    {
                        counter = cart.amount;
                        device = cart.device;
                    }
                }

            return device;
        }

        private DeviceModel GetMostPopularProduct()
        {
            DeviceModel device = null;
            DataTable dt = dB.GetTableData(TableName.orders.ToString());
            List<OrdersModel> orders = new List<OrdersModel>();
            int counter = int.MinValue;
            foreach (DataRow dr in dt.Rows) 
                orders.Add(new OrdersModel(dr));

            foreach (OrdersModel o in orders)
                foreach (CartModel cart in o.cart)
                {
                    if (cart.amount > counter)
                    {
                        counter = cart.amount;
                        device = cart.device;
                    }
                }

            return device;
        }

        private int GetAllCash()
        {
            int cash = 0;
            DataTable dt = dB.GetTableData(TableName.orders.ToString());
            List<OrdersModel> orders = new List<OrdersModel>();
            
            foreach (DataRow dr in dt.Rows) 
                orders.Add(new OrdersModel(dr));

            orders.ForEach(o => cash += o.is_delivered ? (int)o.sum : 0);
            return cash;
        }
    }
}
