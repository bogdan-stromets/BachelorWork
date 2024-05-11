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
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountingSystem.ViewModel
{
    class AuthorizationVM : ViewModelBase
    {
        public List<AuthorizationModel> data { get; }
        private string login;
        private DataTable dt;

        public ICommand AuthorizationCommand { get; set; }
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged(); }
        }     

        private void CheckLogin(object obj)
        {
            string password = (obj as PasswordBox).Password;
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
                ResetLoginPass(obj as PasswordBox);
            }
        }

        private void ResetLoginPass(PasswordBox password)
        {
            Login = string.Empty;
            password.Password = string.Empty;
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
