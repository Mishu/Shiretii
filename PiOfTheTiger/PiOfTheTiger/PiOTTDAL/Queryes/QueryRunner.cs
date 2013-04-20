using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection;

namespace PiOTTDAL.Queryes
{
    public class QueryRunner
    {
        private string connectionString;
        private MySqlConnection connection;

        public QueryRunner()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            connection = new MySqlConnection(connectionString);
        }

        public List<T> Execute<T>(string query)
        {
            List<T> result = new List<T>();
            T instance = (T)Activator.CreateInstance(typeof(T));
            List<PropertyInfo> columns = instance.GetType().GetProperties().ToList();

            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                List<Int32> columnsIndexes = new List<int>();
                instance = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo property in columns)
                {
                    int index = reader.GetOrdinal(property.Name);
                    property.SetValue(instance, reader.GetValue(index));
                }

                result.Add(instance);
            }

            return result;
        }
    }
}
