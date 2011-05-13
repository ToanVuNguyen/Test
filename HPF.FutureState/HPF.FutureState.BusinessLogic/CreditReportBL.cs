using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;

namespace HPF.FutureState.BusinessLogic
{
    public class CreditReportBL:BaseBusinessLogic
    {
        private static readonly CreditReportBL instance = new CreditReportBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CreditReportBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected CreditReportBL()
        {
        }
        public CreditReportDTOCollection GetCreditReportCollection(int? fcId)
        {
            return CreditReportDAO.Instance.GetCreditReportCollection(fcId);
        }
    }
}
