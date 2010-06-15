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

            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            DataValidationException dataValidationException = new DataValidationException();
            
            if (isUpdated)
            {
                caseAudit.SetUpdateTrackingInformation(workingUserId);
                dataValidationException = ValidateCaseAudit(caseAudit);
                if (dataValidationException.ExceptionMessages.Count > 0)
                    throw dataValidationException;
                return CaseAuditDAO.Instance.SaveCaseAudit(caseAudit, true);
            }

            caseAudit.SetInsertTrackingInformation(workingUserId);
            dataValidationException = ValidateCaseAudit(caseAudit);
            if (dataValidationException.ExceptionMessages.Count > 0)
                throw dataValidationException;
            return CaseAuditDAO.Instance.SaveCaseAudit(caseAudit, false);

        }

        private DataValidationException ValidateCaseAudit(CaseAuditDTO caseAudit)
        {
            DataValidationException dataValidationException = new DataValidationException();
            ValidationResults validationResults = HPFValidator.Validate<CaseAuditDTO>(caseAudit);            

            if (!validationResults.IsValid)
            {
                foreach (ValidationResult result in validationResults)
                {
                    string errorCode = string.IsNullOrEmpty(result.Tag) ? "ERROR" : result.Tag;
                    string errorMess = string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessage(result.Tag);
                    dataValidationException.ExceptionMessages.AddExceptionMessage(errorCode, errorMess);
                }
            }
            return dataValidationException;
        }
    }
}
