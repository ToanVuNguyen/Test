using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class SecurityBL : BaseBusinessLogic, ISecurityBL
    {
        private static readonly SecurityBL instance = new SecurityBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static SecurityBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected SecurityBL()
        {
            
        }

        #region Implementation of ISecurityBL

        /// <summary>
        /// User login for administrator and billing
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AdminUserLogin(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Web Service User Login
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="wsType">Service type</param>
        /// <returns></returns>
        public bool WSUserLogin(string userName, string password, WSType wsType)
        {
            //This function does not have transaction.
            try
            {
                WSUserDTO user = SecurityDAO.Instance.GetWSUser(userName, password);

                if (user == null)
                    return false;

                switch (wsType)
                {
                    case WSType.Agency:
                        if (user.AgencyId != 0)
                            return true;
                        break;
                    case WSType.CallCenter:
                        if (user.CallCenterId != 0)
                            return true;
                        break;
                    case WSType.Any:
                        if (user.AgencyId != 0 && user.CallCenterId != 0)
                            return true;
                        break;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Add a new Webservice User to the system
        /// </summary>
        /// <param name="aWSUser"></param>
        public void WSAddUser(WSUserDTO aWSUser)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get WS User by username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>WSUserDTO</returns>
        public WSUserDTO GetWSUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
