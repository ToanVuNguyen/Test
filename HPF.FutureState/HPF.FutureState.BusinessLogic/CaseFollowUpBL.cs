using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common;
using System.Collections.Generic;
using System;

namespace HPF.FutureState.BusinessLogic
{
    public class CaseFollowUpBL: BaseBusinessLogic
    {
        private static readonly CaseFollowUpBL instance = new CaseFollowUpBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseFollowUpBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected CaseFollowUpBL()
        {

        }

        public CaseFollowUpDTOCollection RetrieveCaseFollowUps(int fcId)
        {
            return CaseFollowUpDAO.Instance.GetCaseFollowUp(fcId);
        }

        public OutcomeTypeDTOCollection RetrieveOutcomeTypes()
        {
            return OutcomeDAO.Instance.GetOutcomeType();
        }

        public bool SaveCaseFollowUp(CaseFollowUpDTO caseFollowUp, string workingUserId, bool isUpdated)
        {
            if (isUpdated)
            {
                caseFollowUp.SetUpdateTrackingInformation(workingUserId);
                return CaseFollowUpDAO.Instance.SaveCaseFollowUp(caseFollowUp, true);
            }
            caseFollowUp.SetInsertTrackingInformation(workingUserId);
            return CaseFollowUpDAO.Instance.SaveCaseFollowUp(caseFollowUp, false);

        }
    }
}
