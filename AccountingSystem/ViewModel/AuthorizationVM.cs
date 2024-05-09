using AccountingSystem.Model;
using AccountingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AccountingSystem.ViewModel
{
    class AuthorizationVM : Utilities.ViewModelBase
    {
        public List<AuthorizationModel> data { get; }
        private string login;
        private string password;
        private DataTable dt;

        public ICommand AuthorizationCommand { get; set; }
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged(); }
        }     
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private void CheckLogin(object obj)
        {
            AuthorizationModel auth = data.Find(x => x.login == login && x.password == password);
            if (auth != null)
            {
                //MessageBox.Show("Logged IN :)");
                new MainWindow().Show();
                CustomerVM.id_employeer = auth.id;
            }
            else
            {
                MessageBox.Show("Login or Password incorrect :(");
                ResetLoginPass();
            }
        }

        private void ResetLoginPass()
        {
            Login = string.Empty;
            Password = string.Empty;
        }

        public AuthorizationVM()
        {
            AuthorizationCommand = new RelayCommand(CheckLogin);

            dt = new DB().GetTableData(TableName.employees.ToString());
            data = new List<AuthorizationModel>();

            foreach (DataRow dr in dt.Rows)
            {
                data.Add(new AuthorizationModel(dr));
            }
        }
    }
}
