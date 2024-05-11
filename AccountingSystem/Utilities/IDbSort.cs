using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSystem.Utilities
{
    interface IDbSort
    {
        ICommand SortCommand { get; set; }
        void AscendingSort(string colName);
        void DescendingSort(string colName);
        void SortType(object obj);
        string NameConverter(string targetValue);
    }
}
