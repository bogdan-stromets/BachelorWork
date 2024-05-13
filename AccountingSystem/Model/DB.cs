using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AccountingSystem.Model
{
    enum TableName {cart, categories,devices,manufacturers,orders, employees, statistics }
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

        public DataTable GetTableData(string tableName = "", bool sort = false, string commandStr = "")
        {
            commandStr = !sort ? $"SELECT * FROM `{tableName}`" : commandStr;

            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            OpenConnection();
            MySqlCommand command = new MySqlCommand(commandStr, GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            CloseConnection();
            return dt;
        }

        public void ExecuteSQLCommand(string commandStr)
        {
            OpenConnection();
            MySqlCommand command = new MySqlCommand(commandStr, GetConnection());
            command.ExecuteNonQuery();
            CloseConnection();
        }

    }
}

