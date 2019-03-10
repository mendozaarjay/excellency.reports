using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Excellency.Reports
{
    public partial class SCObjects
    {
        /// <summary>
        /// This method will return a scalar value based on the sql query/command text.
        /// </summary>
        /// <param name="SqlQuery">Sql Query/Command Text</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static string ReturnText(string SqlQuery, string SqlConnectionString)
        {
            string _returnThis = string.Empty;
            SqlConnection cn = new SqlConnection();
            try
            {

                cn.ConnectionString = SqlConnectionString;
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SqlQuery;
                _returnThis = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                _returnThis = string.Empty;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
            return _returnThis;
        }
        /// <summary>
        /// This method will return a scalar value based on the sql query/command text asynchronously.
        /// </summary>
        /// <param name="SqlQuery">Sql Query/Command Text</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static async Task<string> ReturnTextAsync(string SqlQuery, string SqlConnectionString)
        {
            string _returnThis = string.Empty;
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = SqlConnectionString;
                await cn.OpenAsync();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SqlQuery;
                var a = await cmd.ExecuteScalarAsync();
                _returnThis = a.ToString();
            }
            catch (Exception ex)
            {
                _returnThis = string.Empty;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
            return _returnThis;
        }
        /// <summary>
        /// This method will return a datatable based on the sql query/command text.
        /// </summary>
        /// <param name="SqlQuery">Sql Query/Command Text</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static DataTable LoadDataTable(string SqlQuery, string SqlConnectionString)
        {
            SqlConnection cn = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                cn.ConnectionString = SqlConnectionString;
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
            return dt;
        }
        /// <summary>
        /// This method will return a datatable based on the sql query/command text asynchronously.
        /// </summary>
        /// <param name="SqlQuery">Sql Query/Command Text</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static async Task<DataTable> LoadDataTableAsync(string SqlQuery, string SqlConnectionString)
        {
            SqlConnection cn = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                cn.ConnectionString = SqlConnectionString;
                await cn.OpenAsync();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;
                da.SelectCommand = cmd;
                await Task.Run(() => da.Fill(dt));
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
            return dt;
        }
        /// <summary>
        /// This will return a string confirmation if the stored procedure was successfully executed.
        /// </summary>
        /// <param name="cmd">Sql Command</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static string ExecuteNonQuery(SqlCommand cmd, string SqlConnectionString)
        {
            string _returnThis = string.Empty;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = SqlConnectionString;
            cn.Open();
            SqlTransaction sqlTransaction = cn.BeginTransaction();
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqlTransaction;
                cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                _returnThis = "Process has been successfully completed.";
            }
            catch (Exception ex)
            {
                _returnThis = "Errors.\n" + ex.Message;
                sqlTransaction.Rollback();
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return _returnThis;
        }
        /// <summary>
        /// This will return a string confirmation if the stored procedure was successfully executed asynchronously.
        /// </summary>
        /// <param name="cmd">Sql Command</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static async Task<string> ExecuteNonQueryAsync(SqlCommand cmd, string SqlConnectionString)
        {
            string _returnThis = string.Empty;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = SqlConnectionString;
            await cn.OpenAsync();
            SqlTransaction sqlTransaction = cn.BeginTransaction();
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqlTransaction;
                var _checkThis = await cmd.ExecuteNonQueryAsync();
                _returnThis = "Process has been successfully completed.";
                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                _returnThis = "Errors.\n" + ex.Message;
                sqlTransaction.Rollback();
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return _returnThis;
        }
        /// <summary>
        /// This will return a string confirmation if the stored procedure was successfully executed.
        /// </summary>
        /// <param name="cmdList">List of Sql Command</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static string ExecuteNonQuery(List<SqlCommand> cmdList, string SqlConnectionString)
        {
            string _returnThis = string.Empty;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = SqlConnectionString;
            cn.Open();
            SqlTransaction sqlTransaction = cn.BeginTransaction();
            try
            {
                foreach (SqlCommand cmd in cmdList)
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = sqlTransaction;
                    cmd.ExecuteNonQuery();
                }
                _returnThis = "Process has been successfully completed.";
                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                _returnThis = "Errors.\n" + ex.Message;
                sqlTransaction.Rollback();
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return _returnThis;
        }
        /// <summary>
        /// This will return a string confirmation if the stored procedure was successfully executed asynchronously.
        /// </summary>
        /// <param name="cmdList">List of Sql Command</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static async Task<string> ExecuteNonQueryAsync(List<SqlCommand> cmdList, string SqlConnectionString)
        {
            string _returnThis = string.Empty;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = SqlConnectionString;
            await cn.OpenAsync();
            SqlTransaction sqlTransaction = cn.BeginTransaction();
            try
            {
                foreach (SqlCommand cmd in cmdList)
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = sqlTransaction;
                    var _checkThis = await cmd.ExecuteNonQueryAsync();
                }
                _returnThis = "Process has been successfully completed.";
                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                _returnThis = "Errors.\n" + ex.Message;
                sqlTransaction.Rollback();
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return _returnThis;
        }
        /// <summary>
        /// This will return a datatable based on the stored procedure.
        /// </summary>
        /// <param name="cmd">Sql Command</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static DataTable ExecGetData(SqlCommand cmd, string SqlConnectionString)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = SqlConnectionString;
            cn.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return dt;
        }
        /// <summary>
        /// This will return a datatable based on the stored procedure asynchronously. 
        /// </summary>
        /// <param name="cmd">Sql Command</param>
        /// <param name="SqlConnectionString">Sql Connection String</param>
        /// <returns></returns>
        public static async Task<DataTable> ExecGetDataAsync(SqlCommand cmd, string SqlConnectionString)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = SqlConnectionString;
            await cn.OpenAsync();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                await Task.Run(() => da.Fill(dt));
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return dt;
        }
    }
}
