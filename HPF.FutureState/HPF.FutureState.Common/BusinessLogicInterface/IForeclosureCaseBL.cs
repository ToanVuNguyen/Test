using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface IForeclosureCaseBL
    {
        /// <summary>
        /// Save a ForeclosureCaseSet
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeclosureCaseSetDTO</param>
        void SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreClosureCaseSet);

        ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria);
    }
}
