using System;
using System.Data;
using System.Data.Odbc;
using System.IO;

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
            var adapter = GetDataAdapter(filename, sheetName);            
            var ds = new DataSet();
            adapter.Fill(ds);
            adapter.SelectCommand.Connection.Close();            
            return ds;            
        }

        /// <summary>
        /// Read from a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet Read(Stream stream, string sheetName)
        {
            stream.Seek(0, SeekOrigin.Begin);            
            var tempFileName = TempDir + Guid.NewGuid() + ".xls";
            PushStreamToTempFile(stream, tempFileName);
            var ds = Read(tempFileName, sheetName);
            File.Delete(tempFileName);            
            return ds;
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
            return new OdbcDataAdapter(GetSelectCommand(GetExcelConnection(string.Format(filename,sheetName))));
        }        

        private static OdbcConnection GetExcelConnection(string fileName)
        {
            return new OdbcConnection(string.Format(CONNECTION_STRING, fileName));
        }

        private static OdbcCommand GetSelectCommand(OdbcConnection connection)
        {
            return new OdbcCommand(SELECT_STATEMENT, connection);
        }
    }
}
