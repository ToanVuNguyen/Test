using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.BusinessLogic
{
    public class LookupDataBL : BaseBusinessLogic
    {
        private static readonly LookupDataBL instance = new LookupDataBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static LookupDataBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected LookupDataBL()
        {
            
        }
    }
}
