using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AccountingSystem.Model
{
    class EmployeerModel
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string surname { get; private set; } 
        public string position { get; private set; }
        public string photoURL { get; private set; }
        public string fullName { get { return surname + " " + name; } }

        public EmployeerModel(DataRow dr)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            name = dr.ItemArray[1].ToString();
            surname = dr.ItemArray[2].ToString();
            position = dr.ItemArray[3].ToString();
            photoURL = dr.ItemArray[4].ToString();

        }
    }
}
