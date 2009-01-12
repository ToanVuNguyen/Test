using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface ISecurityBL
    {
        /// <summary>
        /// User login for administrator and billing
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool WebUserLogin(string userName, string password);
        /// <summary>
        /// Web Service User Login
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="wsType">Service type</param>
        /// <returns></returns>
        WSUserDTO WSUserLogin(string userName, string password, WSType wsType);

        /// <summary>
        /// Add a new Webservice User to the system
        /// </summary>
        /// <param name="aWSUser"></param>
        void WSAddUser(WSUserDTO aWSUser);
        /// <summary>
        /// Get WS User by username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>WSUserDTO</returns>
        WSUserDTO GetWSUser(string userName);

        /// <summary>
        /// Get WebUser for Billing & Admin
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>UserDTO</returns>
        UserDTO GetWebUser(string userName);
    }
}
