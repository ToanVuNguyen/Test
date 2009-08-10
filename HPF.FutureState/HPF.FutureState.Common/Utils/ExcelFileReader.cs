using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HPF.FutureState.Common.Utils
{
    /// <summary>
    /// Read all information of a ExcelSheet to DataSet
    /// </summary>
    public static class ExcelFileReader
    {
        private const string CONNECTION_STRING = @"Driver=Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb);DBQ={0}";

        private const string SELECT_STATEMENT = "Select * from [{0}$]";
        //

        public static string TempDir = "C:\\";

        /// <summary>
        /// Read from file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet Read(string filename, string sheetName)
        {
            try
            {
                var adapter = GetDataAdapter(filename, sheetName);
                var ds = new DataSet();
                adapter.Fill(ds);
                adapter.SelectCommand.Connection.Close();
                return ds;
            }
            catch (OdbcException Ex)
            {
                if (Ex.Errors[0].NativeError == -1002)
                    throw new ExcelFileReaderException("ERROR--ExcelSheet name \"" + sheetName + "\" does not exist.") { ErrorCode = -1 };
                else if (Ex.Errors[0].NativeError == -5015 || Ex.Errors[0].NativeError == 63)
                    throw new ExcelFileReaderException("ERROR--The file must be an Excel format.") { ErrorCode = -2 };                

                throw;
            }            
        }

        /// <summary>
        /// Read from a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet Read(Stream stream, string sheetName)
        {
            var tempFileName = TempDir + "\\" + Guid.NewGuid() + ".xls";
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                PushStreamToTempFile(stream, tempFileName);
                var ds = Read(tempFileName, sheetName);
                return ds;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);                
                throw;
            }
            finally
            {
                File.Delete(tempFileName);
            }
        }

        /// <summary>
        /// Read from a data buffer
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet Read(byte[] data, string sheetName)
        {
            var stream = new MemoryStream(data);
            var ds = Read(stream, sheetName);
            stream.Close();
            return ds;
        }

        private static void PushStreamToTempFile(Stream stream, string tempFileName)
        {
            var fs = new FileStream(tempFileName, FileMode.Create);
            var buffer = new byte[1024];            
            var count = stream.Read(buffer, 0, 1024);    
            if(count > 0)
            {
                fs.Write(buffer, 0, count);
                while(count > 0)
                {
                    count = stream.Read(buffer, 0, 1024);
                    fs.Write(buffer, 0, count);
                }
            }
            fs.Close();
        }

        private static OdbcDataAdapter GetDataAdapter(string filename, string sheetName)
        {
            return new OdbcDataAdapter(GetSelectCommand(GetExcelConnection(filename), sheetName));
        }        

        private static OdbcConnection GetExcelConnection(string fileName)
        {
            return new OdbcConnection(string.Format(CONNECTION_STRING, fileName));
        }

        private static OdbcCommand GetSelectCommand(OdbcConnection connection, string sheetName)
        {
            return new OdbcCommand(string.Format(SELECT_STATEMENT,sheetName), connection);
        }
    }
}
