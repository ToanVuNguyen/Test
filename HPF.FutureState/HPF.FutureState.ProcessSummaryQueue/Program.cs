using System;
using System.Threading;
using HPF.FutureState.Common.Utils;

namespace HPF.FutureState.ProcessSummaryQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            //Single Instance checking
            bool canRun;
            var mutex = new Mutex(true, "HPF.FutureState.ProcessSummaryQueue", out canRun);
            if (!canRun)
            {                
                return;
            }
            GC.KeepAlive(mutex);  
            //
            ProcessSummaryQueue();
        }

        private static void ProcessSummaryQueue()
        {
            var queue = new HPFSummaryQueue();            
            var entry = GetCompleteCaseEntry(queue);
            while (entry != null)
            {
                ProcessCompleteCaseEntry(entry);
                Thread.Sleep(1000);//Make a thread safe
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
        private static void ProcessCompleteCaseEntry(HPFSummaryQueueEntry entry)
        {
            
        }
    }
}
