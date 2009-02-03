using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace HPF.FutureState.DataAccess
{
    public abstract class BaseDAO
    {
        protected virtual string ConnectionString
        {
            get 
            { 
                return ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString; 
            }
        }

        protected virtual SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        protected virtual SqlCommand CreateCommand(string spName)
        {
            return new SqlCommand(spName);            
        }

        protected virtual SqlCommand CreateCommand(string spName, SqlConnection connection)
        {
            var sqlCommand = new SqlCommand(spName)
            {                
                Connection = connection
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateCommand(string spName, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                Connection = connection,
                Transaction = transaction                
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateCommand(string spName, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                Connection = transaction.Connection,
                Transaction = transaction
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateSPCommand(string spName)
        {
            var sqlCommand = new SqlCommand(spName)
                                 {
                                     CommandType = CommandType.StoredProcedure
                                 };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateSPCommand(string spName, SqlConnection connection)
        {
            var sqlCommand = new SqlCommand(spName)
                                 {
                                     CommandType = CommandType.StoredProcedure, 
                                     Connection = connection
                                 };            
            return sqlCommand;
        }

        protected virtual SqlCommand CreateSPCommand(string spName, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = transaction,
                Connection = connection
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateSPCommand(string spName, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = transaction,
                Connection = transaction.Connection
            };
            return sqlCommand;
        }

        #region DAO Helper
        /// <summary>
        /// Convert an object to GUID
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static Guid ConvertToGUID(object obj)
        {
            if (null != obj)
            {
                return new Guid(obj.ToString());
            }
            else
                return Guid.Empty;
        }


        /// <summary>
        /// Convert an object to string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static string ConvertToString(object obj)
        {
            if (null != obj && System.DBNull.Value != obj)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert an object to Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static int ConvertToInt(object obj)
        {
            int returnValue = 0;
            if (null != obj)
            {
                int.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Convert an object to Byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static byte ConvertToByte(object obj)
        {
            byte returnValue = 0;
            if (null != obj)
            {
                byte.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Convert an object to bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static bool ConvertToBool(object obj)
        {
            bool returnValue = false;
            if (null != obj)
            {
                bool.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Convert an object to decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static decimal ConvertToDecimal(object obj)
        {
            decimal returnValue = 0;
            if (null != obj)
            {
                decimal.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }       

        /// <summary>
        /// Convert an object to datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static DateTime ConvertToDateTime(object obj)
        {
            DateTime returnValue = DateTime.MinValue;
            if (null != obj)
            {
                DateTime.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Convert an object to long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static long ConvertToLong(object obj)
        {
            long returnValue = 0;
            if (null != obj)
            {
                long.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Convert an object to double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static double ConvertToDouble(object obj)
        {
            double returnValue = 0;
            if (null != obj)
            {
                double.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        #region convert to SQL data type
        protected static SqlDateTime NullableDateTime(DateTime? value)
        {
            if (value.HasValue && value.Value >= SqlDateTime.MinValue)
                return (SqlDateTime)value;
            return SqlDateTime.Null;
        }

        protected static SqlInt32 NullableInteger(int? value)
        {
            if (value.HasValue && value.Value >= SqlInt32.MinValue)
                return (SqlInt32)value;
            return SqlInt32.Null;
        }

        protected static SqlString NullableString(string value)
        {
            if (value != null)
                return (SqlString)value;
            return SqlString.Null;            
        }
        #endregion
        
        #endregion
    }
}