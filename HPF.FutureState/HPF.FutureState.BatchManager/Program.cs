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

            if (args.Length > 0 && args[0] == "-CompletedCounselingDetailReport") //in testing
            {
                if (args.Length != 3)
                {
                    Console.WriteLine("Invalid argumnets...");
                    Console.WriteLine("-CompletedCounselingDetailReport startDate endDate");
                    return;
                }
                DateTime startDate = DateTime.Parse(args[1]);
                DateTime endDate = DateTime.Parse(args[2]);
                Console.WriteLine("Exporting excel in progress...");
                BatchJobBL.Instance.GenerateCompletedCounselingDetailReportTest(startDate, endDate);
                return;
            }

            BatchJobBL.Instance.ProcessBatchJobs();
        }     
    }
}
