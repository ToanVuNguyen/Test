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
        public bool WebUserLogin(string userName, string password)
        {
            return SecurityDAO.Instance.WebUserLogin(userName, password);
        }

        public bool WebUserChangePassword(string userName, string oldPassword, string confirmPassword)
        {
            UserDTO currentUser = SecurityDAO.Instance.GetWebUser(userName);
            currentUser.SetInsertTrackingInformation(currentUser.HPFUserId.ToString());
            if (currentUser.Password.ToLower() == oldPassword.ToLower())
            {
                currentUser.Password = confirmPassword;
                SecurityDAO.Instance.UpdateWebUser(currentUser);
            }
            else
                throw new DataValidationException("Wrong old password.");
            return true;
        }

        /// <summary>
        /// Web Service User Login
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="wsType">Service type</param>
        //public bool WSUserLogin(string userName, string password, WSType wsType)
        //{
        //    //This function does not have transaction.
        //    try
        //    {
        //        WSUserDTO user = SecurityDAO.Instance.GetWSUser(userName, password);

        //        if (user == null || user.ActiveInd == string.Empty || user.ActiveInd == "" || user.ActiveInd.ToUpper().CompareTo("N") == 0)
        //            return false;
        //        switch (wsType)
        //        {
        //            case WSType.Agency:
        //                if (user.AgencyId != 0)
        //                    return true;
        //                break;
        //            case WSType.CallCenter:
        //                if (user.CallCenterId != 0)
        //                    return true;
        //                break;
        //            case WSType.Any:
        //                if (user.AgencyId != 0 && user.CallCenterId != 0)
        //                    return true;
        //                break;
        //            default:
        //                return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //    return false;
        //}

        /// <summary>
        /// Web Service User Login
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="wsType">Service type</param>
        /// <returns>WSUserDTO if login successfully</returns>
        public WSUserDTO WSUserLogin(string userName, string password, WSType wsType)
        {
            //This function does not have transaction.
            try
            {
                WSUserDTO user = SecurityDAO.Instance.GetWSUser(userName, password);

                if (user == null || user.ActiveInd == string.Empty || user.ActiveInd == "" || user.ActiveInd.ToUpper().CompareTo("N") == 0)
                    return null;
                switch (wsType)
                {
                    case WSType.Agency:
                        if (user.AgencyId != null)
                            return user;
                        break;
                    case WSType.CallCenter:
                        if (user.CallCenterId != null)
                            return user;
                        break;
                    case WSType.Any:
                        if (user.AgencyId != null && user.CallCenterId != null)
                            return user;
                        break;
                    default:
                        return null;
                }
            }
            catch (Exception Ex)
            {
                return null;
            }

            return null;
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

        /// <summary>
        /// Get WebUser for Billing & Admin
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>UserDTO</returns>
        public UserDTO GetWebUser(string userName)
        {
            return SecurityDAO.Instance.GetWebUser(userName);
        }

        public CallCenterDTO GetCallCenter(int callCenterId)
        {
            return SecurityDAO.Instance.GetCallCenter(callCenterId);
        }
        #endregion
    }
}
