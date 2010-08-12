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
    public class CaseEvaluationBL: BaseBusinessLogic
    {
        private static readonly CaseEvaluationBL instance = new CaseEvaluationBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseEvaluationBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected CaseEvaluationBL()
        {

        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseEvalHeader"></param>
        public void SaveSelectForQCEvalHeader(CaseEvalHeaderDTO caseEvalHeader)
        {
            try
            {
                if (caseEvalHeader.EvalType == EvaluationType.DESKTOP)
                {
                    caseEvalHeader.EvaluationYearMonth = DateTime.Today.Year.ToString() + DateTime.Today.AddMonths(-1).ToString();
                    caseEvalHeader.EvalType = EvaluationType.DESKTOP;
                    caseEvalHeader.EvalStatus = EvaluationStatus.AGENCY_INPUT_REQUIRED;
                }
                else
                {
                    caseEvalHeader.EvaluationYearMonth = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString();
                    caseEvalHeader.EvalType = EvaluationType.ONSITE;
                    caseEvalHeader.EvalStatus = EvaluationStatus.HPF_INPUT_REQUIRED;
                }

                CaseEvalHeaderDAO.Instance.InsertCaseEvalHeader(caseEvalHeader);
            }
            catch
            {
                throw;
            }
        }

        public class EvaluationType
        {
            public const string DESKTOP = "Desktop";
            public const string ONSITE = "Onsite";
        }

        public class EvaluationStatus
        {
            public const string AGENCY_INPUT_REQUIRED = "Agency Input Required";
            public const string HPF_INPUT_REQUIRED = "HPF Input Required";
            public const string RECON_REQUIRED_AGENCY_INPUT = "Recon Required/Agency Input";
            public const string RECON_REQUIRED_HPF_INPUT = "Recon Required/HPF Input";
            public const string RESULT_WITHIN_RANGE = "Result within Range";
            public const string CLOSED = "Closed";
        }

        public class ResultLevel
        {
            public const string MASTERY = "Mastery";
            public const string PROFICIENCY = "Proficiency";
            public const string NOVICE = "Novice";
            public const string REMEDIATION = "Remediation";
        }
    }
}
