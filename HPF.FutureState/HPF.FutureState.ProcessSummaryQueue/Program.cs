using System;
using System.Threading;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using HPF.FutureState.Common;
namespace HPF.FutureState.ProcessSummaryQueue
{
    class Program
    {
        private const string MUTEX_NAME = "HPF.FutureState.ProcessSummaryQueue";

        private const int SLEEPING_TIME = 5000;//Miliseconds

        static void Main(string[] args)
        {
            //Single Instance checking
            bool canRun;
            var mutex = new Mutex(true, MUTEX_NAME, out canRun);
            if (!canRun)
            {
                return;
            }
            GC.KeepAlive(mutex);

            //------------Test Data
            if (args.Length > 0)
            {
                Logger.Write("Test fc_id:" + args[0], "General");
                var queue = new HPFSummaryQueue();
                queue.SendACompletedCaseToQueue(Convert.ToInt32(args[0]));
                return;
            }            
            //------------End Test Data
            ProcessSummaryQueue();
        }

        private static void ProcessSummaryQueue()
        {
            var queue = new HPFSummaryQueue();
            var entry = queue.ReceiveCompletedCaseFromQueue();
            while (entry != null)
            {                
                ProcessCompletedCaseEntry(entry);                
                Thread.Sleep(SLEEPING_TIME);//Make a thread safe
                entry = queue.ReceiveCompletedCaseFromQueue();                
            }
        }        

        /// <summary>
        /// Process a completed Case Entry
        /// </summary>
        /// <param name="entry"></param>
        private static void ProcessCompletedCaseEntry(HPFSummaryQueueEntry entry)
        {
            try
            {
                SummaryReportBL.Instance.SendCompletedCaseSummary(entry.FC_ID);
            }
            catch (Exception Ex)
            {                
                //Log Error down the text file
                ExceptionProcessor.HandleException(Ex);
                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = "Proccessing Quece Error. FCid " + entry.FC_ID.Value.ToString(),
                    Body = "Messsage: " + Ex.Message + "\nTrace: " + Ex.StackTrace
                };
                mail.Send();
            }     
        }
    }
}
