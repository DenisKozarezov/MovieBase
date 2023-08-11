using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text;

namespace Core.Query
{
    /// <summary>
    /// Модуль запросов.
    /// </summary>
    sealed class Generic
    {
        private static string MergeToUpdateQuery(string tables, string output, string conditions)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE " + tables);
            builder.Append(" SET " + output);
            if (conditions.Length > 0) builder.Append(" WHERE " + conditions);
            return builder.ToString();
        }
        private static string MergeToSelectQuery(string output, string tables, string conditions, SelectType type = SelectType.USUAL)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT " + (type == SelectType.DISTINCT ? "DISTINCT " : string.Empty) + output);
            builder.Append(" FROM " + tables);
            if (conditions.Length > 0) builder.Append(" WHERE " + conditions);
            return builder.ToString();
        }

        public static T ExecuteScalar<T>(string function, QueryParametersBuilder builder = null)
        {
            T result = default;
            try
            {
                SqlConnection connection = new SqlConnection(Constants.SQLConnectionString);

                connection.InfoMessage += (s, e) => MessageBox.Show(e.Message, "Запрос", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand command = new SqlCommand();
       
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT dbo.{function}({builder?.Parameters})";
                builder?.Construct(command);

                connection.Open();
                result = (T)command.ExecuteScalar();
                builder?.RetrieveOutput(command);
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }
            return result;
        }
        public static DataTable SelectFrom(string source, QueryParametersBuilder builder = null)
        {
            DataTable result = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection(Constants.SQLConnectionString);

                connection.InfoMessage += (s, e) => MessageBox.Show(e.Message, "Запрос", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM dbo.{source}({builder?.Parameters})";
                builder?.Construct(command);

                connection.Open();
                result.Load(command.ExecuteReader());
                builder?.RetrieveOutput(command);
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return result;
        }
        public static DataTable SelectFrom(GenericAdvancedSearchArgs args, QueryParametersBuilder builder = null)
        {
            DataTable result = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection(Constants.SQLConnectionString);

                connection.InfoMessage += (s, e) => MessageBox.Show(e.Message, "Запрос", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = MergeToSelectQuery(args.GetOutput(), args.GetTables(), args.GetConditions(), args.SelectType);
                builder?.Construct(command);

                connection.Open();
                result.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return result;
        }
        public static void ExecuteNonQuery(string query, CommandType type = CommandType.Text, QueryParametersBuilder builder = null)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Constants.SQLConnectionString);

                connection.InfoMessage += (s, e) => MessageBox.Show(e.Message, "Запрос", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = type;
                command.CommandText = query;
                builder?.Construct(command);

                connection.Open();
                command.ExecuteNonQuery();
                builder?.RetrieveOutput(command);

                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Осуществляет многокритериальный поиск с произвольным числом аргументов.
        /// </summary>
        /// <param name="args">Аргументы запроса.</param>
        /// <returns></returns>
        public static DataTable AdvancedSearch(GenericAdvancedSearchArgs args)
        {
            return SelectFrom(args);
        }

        /// <summary>
        /// Обновляет запись в произвольной таблице с переменными аргументами.
        /// </summary>
        /// <param name="args">Аргументы запроса.</param>
        public static void Update(GenericAdvancedSearchArgs args) => ExecuteNonQuery(MergeToUpdateQuery(args.GetTables(), args.GetOutput(), args.GetConditions()));
    }
}