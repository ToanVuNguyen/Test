using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            if (null != obj)
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
        protected static decimal ConvertToGetDecimal(object obj)
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
        protected static double ConvertTodouble(object obj)
        {
            double returnValue = 0;
            if (null != obj)
            {
                double.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }       
        #endregion
    }
}