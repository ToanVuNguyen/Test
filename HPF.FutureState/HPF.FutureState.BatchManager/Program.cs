using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.BatchManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {                
                if (args.Length > 0)
                {
                    if (args[0] == "-CompletedCounselingDetailReport") //in testing)
                    {
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Invalid argumnets...");
                            Console.WriteLine("-CompletedCounselingDetailReport startDate endDate");
                            return;
                        }
                        DateTime startDate = DateTime.Parse(args[1]);
                        DateTime endDate = DateTime.Parse(args[2]);
                        string spFolder = null;
                        if (args.Length >= 4)
                            spFolder = args[3];
                        Console.WriteLine("Exporting excel in progress...");
                        BatchJobBL.Instance.GenerateCompletedCounselingDetailReport(startDate, endDate, spFolder);
                    }
                    else if (args[0] == "-ATTCallingRecordImport" && args.Length > 1)
                    {
                        BatchJobBL.Instance.ImportATTCallingData(args[1]);
                    }
                    return;
                }

                BatchJobBL.Instance.ProcessBatchJobs();
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);

                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = "Batch Manager Error",
                    Body = "Messsage: " + ex.Message + "\nTrace: " + ex.StackTrace
                };
                mail.Send();
            }
        }     
    }
}
