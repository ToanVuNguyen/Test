﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects.BillingAdmin;
using HPF.FutureState.DataAccess.BillingAdmin;
using System.Data;

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
        /// <summary>
        /// Get Program Name and Program ID to display in DDLB
        /// </summary>
        /// <returns></returns>
        public DataSet GetProgram()
        {
            DataSet result = AppForeclosureCaseDAO.CreateInstance().AppGetProgram();
            return result;
        }
        /// <summary>
        /// Get State Name and State ID to display in DDLB
        /// </summary>
        /// <returns></returns>
        public DataSet GetState()
        {
            DataSet result = AppForeclosureCaseDAO.CreateInstance().AppGetState();
            return result;
        }
        /// <summary>
        /// Get Agency Name and Agency ID to display in DDLB
        /// </summary>
        /// <returns></returns>
        public DataSet GetAgency()
        {
            DataSet result = AppForeclosureCaseDAO.CreateInstance().AppGetAgency();
            return result;
        }
    }
}
