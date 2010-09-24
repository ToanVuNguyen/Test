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
    public class HPFUserBL : BaseBusinessLogic
    {
        private static readonly HPFUserBL instance = new HPFUserBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static HPFUserBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected HPFUserBL()
        {
            
        }

        public HPFUserDTOCollection RetrieveHpfUsers()
        {
            return HPFUserDAO.Instance.GetHpfUsers();
        }
        public HPFUserDTOCollection RetriveHpfUsersByAgencyId(int agencyId)
        {
            return HPFUserDAO.Instance.GetHpfUsersByAgencyId(agencyId);
        }
        public void UpdateHpfUser(HPFUserDTO hpfUser)
        {
            HPFUserDAO.Instance.UpdateHpfUser(hpfUser);
        }
        public HPFUserDTO InsertHpfUser(HPFUserDTO hpfUser)
        {
            return HPFUserDAO.Instance.InsertHpfUser(hpfUser);
        }
    }
    
}
