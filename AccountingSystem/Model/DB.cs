using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AccountingSystem.Model
{
    enum TableName {cart, categories,devices,manufacturers,orders, employees}
    class DB
    {
        private MySqlConnection connection;
        private string connectionStr;

        public DB()
        {
            connectionStr = "server=localhost;port=3306;username=root;password=root;database=computerequip";
            connection = new MySqlConnection(connectionStr);
        }
        private void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection GetConnection() { return connection; }

        public DataTable GetTableData(string tableName)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            OpenConnection();
            MySqlCommand command = new MySqlCommand($"SELECT * FROM `{tableName}`", GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            CloseConnection();

            return dt;
        }

    }
}

