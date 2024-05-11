using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class DeviceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ManufacturerModel manufacturer { get; set; }
        public decimal price { get; set; }
        public CategoryModel category { get; set; }
        public int stock_size { get; set; }
        public string pictureURL { get; set; }
        public List<ManufacturerModel> manufacturerList { get; set; }
        public List<CategoryModel> categoryList { get; set; }
        private DB dB;

        public DeviceModel(DataRow dr)
        {
            dB = new DB();
            id = int.Parse(dr.ItemArray[0].ToString());
            name = dr.ItemArray[1].ToString();
            description = dr.ItemArray[2].ToString();
            price = int.Parse(dr.ItemArray[4].ToString());
            stock_size = int.Parse(dr.ItemArray[6].ToString());
            pictureURL = dr.ItemArray[7].ToString();
            GetManufacturer(int.Parse(dr.ItemArray[3].ToString()));
            GetCategory(int.Parse(dr.ItemArray[5].ToString()));
        }

        public DeviceModel(int device_id)
        {
            dB = new DB();
            DataTable data = dB.GetTableData(TableName.devices.ToString());
            foreach (DataRow dr in data.Rows) 
            {
                if (dr.ItemArray[0].ToString() == device_id.ToString())
                {
                    id = int.Parse(dr.ItemArray[0].ToString());
                    name = dr.ItemArray[1].ToString();
                    description = dr.ItemArray[2].ToString();
                    price = int.Parse(dr.ItemArray[4].ToString());
                    stock_size = int.Parse(dr.ItemArray[6].ToString());
                    pictureURL = dr.ItemArray[7].ToString();
                    GetManufacturer(int.Parse(dr.ItemArray[3].ToString()));
                    GetCategory(int.Parse(dr.ItemArray[5].ToString()));
                    break;
                }
            }
        }

        private void GetCategory(int id)
        {
            DataTable dt = dB.GetTableData(TableName.categories.ToString());
            categoryList = new List<CategoryModel>();
            foreach (DataRow dr in dt.Rows)
            {
                categoryList.Add(new CategoryModel(dr));
            }
            category = categoryList.Find(x => x.id == id);
        }
        private void GetManufacturer(int id)
        {
            DataTable dt = dB.GetTableData(TableName.manufacturers.ToString());
            manufacturerList = new List<ManufacturerModel>();
            foreach (DataRow dr in dt.Rows)
            {
                manufacturerList.Add(new ManufacturerModel(dr));
            }

            manufacturer = manufacturerList.Find(x => x.id == id);
        }

        internal string GetUpdateCommand()
        {
            return $"UPDATE `{TableName.devices}` SET `amount` = '{stock_size}' WHERE `{TableName.devices}`.`id_device` = {id}";
        }
    }
}
