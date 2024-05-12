using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AccountingSystem.Model;

namespace AccountingSystem.Utilities
{
    abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        protected virtual void InitVM(string tableName = "", string command = "", bool sort = false) { }
        protected virtual string SearchStringCommand(TableName table, string searchText, params string[] fields)
        {
            string command = $"SELECT * FROM {table} WHERE";

            for (int i = 0; i < fields.Length; i++)
            {
                command += i == fields.Length - 1 ? $" {fields[^1]} LIKE('%{searchText}%')" : $" {fields[i]} LIKE('%{searchText}%') OR";
            }
            return command;
        }
    }
}
