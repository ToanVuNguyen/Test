using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseBL : BaseBusinessLogic
    {
        private static readonly ForeclosureCaseBL instance = new ForeclosureCaseBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ForeclosureCaseBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected ForeclosureCaseBL()
        {
            

        }
    }
}
