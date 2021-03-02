using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Common
    {
        private SqlConnection conn;

        private string connectionString = @"server=(localdb)\mssqllocaldb;initial catalog=BOA;integrated security=true";


        public void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }




        public object spExecuteScalar(string spName, SqlParameter[] parameters)
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
            }


            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }


            catch (Exception e)
            {
                conn.Close();
                return false;
            }


        }


        public bool SpExecute(string spName, SqlParameter[] parameters)
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
            }


            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }

            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }


        public SqlDataReader GetData(string spName, SqlParameter[] parameters)
        {



            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
            }


            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
             conn.Open();
            try
            {
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
