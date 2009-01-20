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
    public class CaseLoanBL : BaseBusinessLogic, ICaseLoanBL
    {
        private static readonly CaseLoanBL instance = new CaseLoanBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseLoanBL Instance
        {
            get
            {
                return instance;
            }
        }
       
        protected CaseLoanBL()
        {
            
        }

        #region Implementation of ICaseLoanBL

        public CaseLoanDTOCollection RetrieveCaseLoan(int fcId)
        {
            return CaseLoanDAO.Instance.ReadCaseLoan(fcId);
        }
        #endregion        
       
    }
}
