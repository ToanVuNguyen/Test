using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using CCRCEncryption;
using System.IO;
public sealed class Sqlhelper
{
  

    public Sqlhelper()
    {
    }

    public SqlConnection GetConnectionString()
    {
        SqlConnection MyConnection = new SqlConnection(SecureConnection.GetConnectionStringFromRegistry ());
        return MyConnection;
    }

    public SqlDataReader GetReader(string SQL)
    {
        SqlDataReader rd = null;
    
            SqlConnection MyConnection = GetConnectionString();

            using (SqlCommand selectCommand = MyConnection.CreateCommand())
            {
                selectCommand.CommandText = SQL;// 
                selectCommand.Connection.Open();
                rd = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }


        return rd;
    }

    public void ExecuteNonQuery(string SQL)
    {
        using (SqlConnection MyConnection = GetConnectionString())
        {
            using (SqlCommand command = new SqlCommand(SQL))
            {
                command.Connection = MyConnection;
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
    }

    public void ExecuteNonQuery(string storedProcedureName, out int outputParam, params object[] parameterValues)
    {
            using (SqlConnection MyConnection = GetConnectionString())
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = MyConnection;
                    for (int i = 0; i < parameterValues.Length; i++)
                    {
                        command.Parameters.Add(parameterValues[i]);
                    }
                    command.Connection.Open();
                    command.ExecuteNonQuery();

                    outputParam = (int)command.Parameters[0].Value;
                }
            }
    }


    public string ExecuteScalar(string SQL, out string outputParam)
    {
        using (SqlConnection MyConnection = GetConnectionString())
        {
            using (SqlCommand command = new SqlCommand(SQL))
            {
                command.Connection = MyConnection;
                command.Connection.Open();
                outputParam = command.ExecuteScalar().ToString();
                return outputParam;
            }
        }
    }




}