using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface IForeclosureCase
    {
        /// <summary>
        /// Insert Foreclosure Case
        /// </summary>
        /// <param>ForeclosureCaseDTO</param>
        /// <returns>fc_id</returns>
        int InsertForeclosureCase(ForeclosureCaseDTO foreclosureCase);        
        /// <summary>

        /// <summary>
        /// Update Foreclosure Case
        /// </summary>
        /// <param>ForeclosureCaseDTO</param>
        /// <returns>fc_id</returns>
        int SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria, int pageSize);

        /// <summary>
        /// Save ForeclosureCase Set including 
        /// ForeclosureCase, CaseLoan, BudgetAsset, BudgetItem, LogActivity...
        /// </summary>
        /// <param name="foreclosureCaseSet">a Set of different types of objects to save</param>
        void SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet);
        
    }
}
