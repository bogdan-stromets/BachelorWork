using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AccountingSystem.Model;
using AccountingSystem.Utilities;

namespace AccountingSystem.ViewModel
{
    class CustomerVM : ViewModelBase
    {
        private EmployeerModel employeer;
        private DataTable dt;
        public static int id_employeer { get; set; }

        public DataTable EmployeersDataTable
        {
            get {return dt;}
            set { dt = value; OnPropertyChanged(); }
        }

        public EmployeerModel Employeer
        {
            get { return employeer; }
            set { employeer = value; OnPropertyChanged(); }
        }

        public CustomerVM()
        {
            EmployeersDataTable = new DB().GetTableData(TableName.employees.ToString());
            foreach (DataRow dr in EmployeersDataTable.Rows)
            {
                if ((int)dr.ItemArray[0] == id_employeer)
                {
                    Employeer = new EmployeerModel(dr);
                    break;
                }
            }
        }
    }
}
