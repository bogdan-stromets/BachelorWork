using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class CategoryModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public CategoryModel(DataRow dr)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            name = dr.ItemArray[1].ToString();
        }
    }
}
