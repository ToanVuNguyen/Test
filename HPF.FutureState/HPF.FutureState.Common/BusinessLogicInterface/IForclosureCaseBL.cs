using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface IForclosureCaseBL
    {
        /// <summary>
        /// Save a ForeClosureCaseSet
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeClosureCaseSetDTO</param>
        void SaveForeClosureCaseSet(ForeclosureCaseSetDTO foreClosureCaseSet);

        ForeclosureCaseSearchResult SearchForeClosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria);
    }
}
