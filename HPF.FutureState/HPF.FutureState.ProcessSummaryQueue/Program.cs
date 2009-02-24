using System;
using System.IO;
using System.Threading;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HPF.FutureState.ProcessSummaryQueue
{
    class Program
    {
        private const string MUTEX_NAME = "HPF.FutureState.ProcessSummaryQueue";

        private const int SLEEPING_TIME = 5000;//Miliseconds

        static void Main()
        {
            //Single Instance checking
            bool canRun;
            var mutex = new Mutex(true, MUTEX_NAME, out canRun);
            if (!canRun)
            {                
                return;
            }
            GC.KeepAlive(mutex);
            Logger.Write("Test Logging", "General");
            //ProcessSummaryQueue();
        }

        private static void ProcessSummaryQueue()
        {
            var queue = new HPFSummaryQueue();            
            var entry = GetCompleteCaseEntry(queue);
            while (entry != null)
            {
                ProcessCompletedCaseEntry(entry);
                Thread.Sleep(SLEEPING_TIME);//Make a thread safe
                entry = queue.ReceiveCompletedCaseFromQueue();
            }
        }

        /// <summary>
        /// Get a completed Case Entry from MSMQ
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        private static HPFSummaryQueueEntry GetCompleteCaseEntry(HPFSummaryQueue queue)
        {
            return queue.ReceiveCompletedCaseFromQueue();
        }

        /// <summary>
        /// Process a completed Case Entry
        /// </summary>
        /// <param name="entry"></param>
        private static void ProcessCompletedCaseEntry(HPFSummaryQueueEntry entry)
        {
            SummaryReportBL.Instance.SendCompletedCaseSummary(entry.FC_ID);
        }
    }
}
