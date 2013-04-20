using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection;
using PiOTTDAL.Entities;

namespace PiOTTDAL.Queryes
{
    public class QueryRunner
    {
        private string connectionString;
        private MySqlConnection connection;

        /// <summary>
        /// The constructor gets the database connection string
        /// </summary>
        public QueryRunner()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Receives a type and a query and returns a collection of
        /// objects of the specified type, that are retrieved from
        /// the database, using the given query.
        /// </summary>
        public List<T> Execute<T>(string query)
        {
            List<T> result = new List<T>();
            T instance = (T)Activator.CreateInstance(typeof(T));
            List<PropertyInfo> columns = instance.GetType().GetProperties().ToList();

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        instance = (T)Activator.CreateInstance(typeof(T));
                        foreach (PropertyInfo property in columns)
                        {
                            int index = reader.GetOrdinal(property.Name);
                            property.SetValue(instance, reader.GetValue(index));
                        }

                        result.Add(instance);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Receives a type and returns a collection with
        /// all the objects of the specified type, found
        /// in the database
        /// </summary>
        public List<T> GetAll<T>()
        {
            string query = String.Format("select * from {0}",typeof(T).Name);

            return Execute<T>(query);
        }

        /// <summary>
        /// Receives a type and returns a collection with
        /// all the objects of the specified type, found
        /// in the database, filtered by the column that
        /// has the specified attribute.
        /// </summary>
        public List<T> GetByAttribute<T>(string name, Type attributeType)
        {
            string typeName = typeof(T).Name;

            PropertyInfo property = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, attributeType)).FirstOrDefault();

            if (property == null)
                throw new Exception(String.Format("Attribute {0} not found on entity {1}", attributeType.Name, typeName));

            string query = String.Format("select * from {0} where {1} = '{2}'", typeName, property.Name, name);

            return Execute<T>(query);
        }
    }
}
