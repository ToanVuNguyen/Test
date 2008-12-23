using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects.BillingAdmin;
using HPF.FutureState.DataAccess.BillingAdmin;

namespace HPF.FutureState.BusinessLogic.BillingAdmin
{
    public class AppForeclosureCaseBL:BaseBusinessLogic
    {
        private static readonly AppForeclosureCaseBL _instace = new AppForeclosureCaseBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static AppForeclosureCaseBL Instance
        {
            get { return _instace; }
        }
        protected AppForeclosureCaseBL()
        {

        }
        /// <summary>
        /// Search Foreclosure Case
        /// </summary>
        /// <param name="searchCriteria">Search criteria</param>
        /// <returns>Collection of AppForeclosureCaseSearchResult</returns>
        public AppForeclosureCaseSearchResult AppSearchforeClosureCase(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            AppForeclosureCaseSearchResult result = AppForeclosureCaseDAO.CreateInstance().AppSearchForeclosureCase(searchCriteria);
            return result;
        }
    }
}
