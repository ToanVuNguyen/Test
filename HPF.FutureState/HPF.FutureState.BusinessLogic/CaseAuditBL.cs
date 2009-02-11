using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;

using System.Collections.Generic;
using System;

namespace HPF.FutureState.BusinessLogic
{
    public class CaseAuditBL: BaseBusinessLogic
    {
        private static readonly CaseAuditBL instance = new CaseAuditBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseAuditBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected CaseAuditBL()
        {

        }
        public CaseAuditDTOCollection RetrieveCaseAudits (int fcId)
        {            
            return CaseAuditDAO.Instance.GetCaseAudits(fcId);
        }

        public bool SaveCaseAudit(CaseAuditDTO caseAudit, string workingUserId, bool isUpdated)
        {
            if (isUpdated)
            {
                caseAudit.SetUpdateTrackingInformation(workingUserId);
                return CaseAuditDAO.Instance.SaveCaseAudit(caseAudit, true);
            }
            caseAudit.SetInsertTrackingInformation(workingUserId);
            return CaseAuditDAO.Instance.SaveCaseAudit(caseAudit, false);

        }
    }
}
