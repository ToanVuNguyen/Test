using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.BatchManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BatchJobBL.Instance.ProcessBatchJobs();
            }
            catch (HPFException Ex)
            {
                //Log Error down the text file
                ExceptionProcessor.HandleException(Ex);
                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = "Batch Manager Error: found error in batch job id " + Ex.GetBatchJobId(),
                    Body = "Messsage: " + Ex.Message + "\nTrace: " + Ex.StackTrace
                };
                mail.Send();
            }
        }                        
    }
}
