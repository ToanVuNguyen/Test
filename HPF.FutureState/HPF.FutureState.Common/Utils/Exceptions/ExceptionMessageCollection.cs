using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ExceptionMessageCollection : BaseDTOCollection<ExceptionMessage>
    {      
        public void AddExceptionMessage(string message)
        {
            Add(new ExceptionMessage
            {                
                Message = message
            });
        }

        public void AddExceptionMessage(string errorCode, string message)
        {            
            Add(new ExceptionMessage
            {
                ErrorCode = errorCode,
                Message = message                 
            });
        }

        public void AddException(string errorCode)
        {         
            Add(new ExceptionMessage
            {
                ErrorCode = errorCode,
                Message = ErrorMessages.GetExceptionMessage(errorCode)
            });
        }
        public override string ToString()
        {
            var result = string.Empty;
            foreach (var message in this)
            {
                result += message + "|";
            }
            return result;
        }

        public ExceptionMessageCollection GetExceptionMessages(string errorCode)
        {
            ExceptionMessageCollection result = new ExceptionMessageCollection();
            foreach (ExceptionMessage msg in this)
            {
                if (msg.ErrorCode == errorCode)
                    result.Add(msg);

            }

            return result;
        }
    }
}
