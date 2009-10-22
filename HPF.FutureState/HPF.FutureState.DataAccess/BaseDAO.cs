using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common;

namespace HPF.FutureState.DataAccess
{
    public abstract class BaseDAO
    {
        protected virtual string ConnectionString
        {
            get
            {
                return HPFConfigurationSettings.HPF_DB_CONNECTION_STRING;
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
                Connection = connection,
                CommandTimeout = connection.ConnectionTimeout
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateCommand(string spName, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                Connection = connection,
                Transaction = transaction,
                CommandTimeout = connection.ConnectionTimeout
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateCommand(string spName, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                Connection = transaction.Connection,
                Transaction = transaction,
                CommandTimeout = transaction.Connection.ConnectionTimeout
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
                Connection = connection,
                CommandTimeout = connection.ConnectionTimeout
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateSPCommand(string spName, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = transaction,
                Connection = connection,
                CommandTimeout = connection.ConnectionTimeout
            };
            return sqlCommand;
        }

        protected virtual SqlCommand CreateSPCommand(string spName, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(spName)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = transaction,
                Connection = transaction.Connection,
                CommandTimeout = transaction.Connection.ConnectionTimeout
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
            if (obj == null)
                return null;

            if (!string.IsNullOrEmpty(obj.ToString()))
                return obj.ToString();
            return null;

        }

        /// <summary>
        /// Convert an object to Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static int? ConvertToInt(object obj)
        {
            int returnValue = 0;
            
            if (obj == null || !int.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }

        /// <summary>
        /// Convert an object to Byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static byte? ConvertToByte(object obj)
        {
            byte returnValue = 0;
            if (obj == null || !byte.TryParse(obj.ToString(), out returnValue))
                return null;
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
            if (obj == null || !bool.TryParse(obj.ToString(), out returnValue))
                return false;
            return returnValue;
        }

        /// <summary>
        /// Convert an object to decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static decimal? ConvertToDecimal(object obj)
        {
            decimal returnValue = 0;
            if (obj == null || !decimal.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }

        /// <summary>
        /// Convert an object to datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static DateTime? ConvertToDateTime(object obj)
        {            
            if (obj == null)
                return null;
            try
            {
                return  (DateTime?)obj;
            }
            catch 
            {
                return null;
            }            
        }

        /// <summary>
        /// Convert an object to long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static long? ConvertToLong(object obj)
        {
            long returnValue = 0;
            if (obj == null || !long.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }

        /// <summary>
        /// Convert an object to double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static double? ConvertToDouble(object obj)
        {
            double returnValue = 0;
            if (obj == null || !double.TryParse(obj.ToString(), out returnValue))
                return null;
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
            if (!string.IsNullOrEmpty(value))
                return (SqlString)value;
            return SqlString.Null;
        }
        #endregion

        #endregion
    }
}