using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Model
{
    class AuthorizationModel
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public AuthorizationModel(DataRow dr)
        {
            id = int.Parse(dr.ItemArray[0].ToString());
            login = dr.ItemArray[5].ToString();
            password = dr.ItemArray[6].ToString();
        }
    }
}
