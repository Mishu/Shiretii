using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection;
using PiOTTDAL.Entities;
using PiOTTCommon.CustomExceptions;
using PiOTTDAL.Entities.Attributes;

namespace PiOTTDAL.Queries.Base
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
        private List<T> ExecuteQuery<T>(string query)
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
                            property.SetValue(instance, reader.GetValue(index), null);
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
        protected List<T> GetAll<T>()
        {
            string query = String.Format("select * from {0}",typeof(T).Name);

            return ExecuteQuery<T>(query);
        }

        /// <summary>
        /// Receives a type and returns a collection with
        /// all the objects of the specified type, found
        /// in the database, filtered by the column that
        /// has the specified attribute.
        /// </summary>
        protected T GetByAttribute<T>(string name, Type attributeType)
        {
            string typeName = typeof(T).Name;

            PropertyInfo property = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, attributeType)).FirstOrDefault();

            if (property == null)
                throw new Exception(String.Format("Attribute {0} not found on entity {1}", attributeType.Name, typeName));

            string query = String.Format("select * from `{0}` where `{1}` = '{2}'", typeName, property.Name, name);

            List<T> results = ExecuteQuery<T>(query);

            if (results.Count == 0)
                throw new DALException(String.Format("No result with value {0} was found in table {1}", name, typeName));

            return ExecuteQuery<T>(query).FirstOrDefault();
        }

        protected void InsertEntity<T>(T entity)
        {
            List<T> result = new List<T>();
            Type entityType = typeof(T);
            T instance = (T)Activator.CreateInstance(entityType);
            List<PropertyInfo> columns = instance.GetType().GetProperties().ToList();
            MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.Connection = connection;

            try
            {
                connection.Open();

                StringBuilder columnsForInsert = new StringBuilder();
                StringBuilder parametersForInsert = new StringBuilder();
                foreach (PropertyInfo prop in columns)
                {
                    columnsForInsert.AppendFormat("{0},", prop.Name);
                    if (Attribute.IsDefined(prop, typeof(ID)))
                        parametersForInsert.AppendFormat("NULL,", prop.Name);
                    else
                    {
                        parametersForInsert.AppendFormat("@{1},", prop.Name);
                        cmd.Parameters.AddWithValue(String.Format("@{0}", prop.Name), prop.GetValue(entity, null));
                    }
                }

                string columnsInsert = columnsForInsert.ToString();
                RemoveLastComma(columnsInsert);

                string parametersInsert = parametersForInsert.ToString();
                RemoveLastComma(parametersInsert);

                string sqlCommand = String.Format("insert into {0}({1}) values({2})"
                    ,entityType.Name
                    ,columnsInsert
                    ,parametersInsert);

                cmd.CommandText = sqlCommand;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private void RemoveLastComma(string str)
        {
            if (str.EndsWith(","))
                str = str.Remove(str.Length - 1);
        }
    }
}
