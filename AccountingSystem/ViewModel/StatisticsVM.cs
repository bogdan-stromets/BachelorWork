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
    class StatisticsVM : ViewModelBase
    {
        private StatisticsModel statistics;
        private DataTable dt;

        public StatisticsModel Stats
        {
            get { return statistics; }
            set { statistics = value; OnPropertyChanged(); }
        }

        public StatisticsVM()
        {
            dt = new DB().GetTableData(TableName.statistics.ToString());

            Stats = new StatisticsModel(dt.Rows.Count != 0 ? dt.Rows[0] : null);
        }
    }
}
