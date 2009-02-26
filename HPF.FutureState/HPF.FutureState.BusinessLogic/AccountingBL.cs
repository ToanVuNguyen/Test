using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common;
using System.Collections.ObjectModel;



namespace HPF.FutureState.BusinessLogic
{
    public class AccountingBL : BaseBusinessLogic
    {
        private static readonly AccountingBL instance = new AccountingBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static AccountingBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected AccountingBL()
        {

        }
        public AccountingDTO GetAccountingDetailInfo(int Fc_ID)
        {
            try
            {
                AccountingDTO result = AccountingDAO.CreateInstance().DisplayAccounting(Fc_ID);
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        public void UpdateForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            try
            {
                AccountingDAO.CreateInstance().UpdateForeclosureCase(foreclosureCase);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        

    }
}