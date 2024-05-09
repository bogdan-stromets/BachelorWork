using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class ManufacturerModel
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public ManufacturerModel(DataRow dr)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            name = dr.ItemArray[1].ToString();
        }
    }
}
