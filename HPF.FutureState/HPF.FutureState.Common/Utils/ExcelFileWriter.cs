using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Odbc;

namespace HPF.FutureState.Common.Utils
{    
    public class ExcelDataRow
    {
        public Collection<string> Columns{get;set;}        
    }
    
    public static class ExcelFileWriter
    {
        public const int PAGE_ROW_COUNT = 64990;
        private const string CONNECTION_STRING = "Driver=Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb);FIRSTROWHASNAMES=1;READONLY=FALSE;CREATE_DB=\"{0}\";DBQ={0}";

        public static void PutToExcel(string filename, string sheetName, string[] headers, Collection<ExcelDataRow> rows )
        {
            try
            {
                //int sheetCount = rows.Count/PAGE_ROW_COUNT;
                var connection = GetExcelConnection(filename);
                connection.Open();
                //if (sheetCount <= 0) sheetCount = 1;
                //for (int i = 1; i <= sheetCount; i++)
                {
                    //sheetName += i;
                    try
                    {
                        OdbcCommand command = GetCreateSheetCommand(connection, sheetName, headers);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    catch
                    {
                        Console.WriteLine("Write date into existing sheet.");
                    }
                    foreach (ExcelDataRow row in rows)
                    {
                        var insertCommand = GetInsertSheetCommand(connection, sheetName, headers, row.Columns);
                        insertCommand.ExecuteNonQuery();
                        insertCommand.Dispose();
                    }
                }
                connection.Close();
            }
            catch (OdbcException Ex)
            {
                throw;
            }
        }

        private static OdbcConnection GetExcelConnection(string fileName)
        {
            return new OdbcConnection(string.Format(CONNECTION_STRING, fileName));
        }

        private static OdbcCommand GetCreateSheetCommand(OdbcConnection connection, string sheetName, string[] headers)
        {
            string createStr = "CREATE TABLE [" + sheetName + "](";
            for (int i = 0; i < headers.Length; i++)
            {
                if (i > 0) createStr += ",";
                createStr += "[" + headers[i] + "] TEXT";
            }
            createStr += ");";
            return new OdbcCommand(createStr, connection);
        }

        private static OdbcCommand GetInsertSheetCommand(OdbcConnection connection, string sheetName, string[] headers, Collection<string> datas)
        {
            string insertStr = "INSERT INTO [" + sheetName + "](";

            for (int i = 0; i < headers.Length; i++)
            {
                if (i > 0) insertStr += ",";
                insertStr += "["+ headers[i] + "]";
            }
            insertStr += ") VALUES(";

            for (int j = 0; j < datas.Count; j++)
            {
                if (j > 0) insertStr += ",";
                insertStr += "'" + (string.IsNullOrEmpty(datas[j]) ? " " : datas[j].Replace("'", "''")) + "'";
            }

            insertStr += ");";

            return new OdbcCommand(insertStr, connection);
        }
    }
}
