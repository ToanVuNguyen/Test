using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.BusinessLogic
{
    public class ReportBL
    {
        private static readonly ReportBL instance = new ReportBL();
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static ReportBL Instance
        {
            get { return instance; }
        }

        protected ReportBL()
        {
            
        }
    }
}
