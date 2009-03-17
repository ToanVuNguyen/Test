using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ExcelFileReaderException : Exception
    {
        public int ErrorCode { get; set; }

        public ExcelFileReaderException(string message):base(message)
        {
            
        }

    }
}