using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSystem.Utilities
{
    interface IDbSearch
    {
        public ICommand SearchCommand { get; set; }
        public void Search(object obj);
    }
}
