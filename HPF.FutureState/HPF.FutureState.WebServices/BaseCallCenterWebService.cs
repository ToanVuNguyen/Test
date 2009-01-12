using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.WebServices
{
    public class BaseCallCenterWebService : BaseWebService
    {
        protected int CallCenterID { get; set; }
        //protected string CallCenterName { get; set; }
        protected override bool IsAuthenticated()
        {
            WSUserDTO user = SecurityBL.Instance.WSUserLogin(Authentication.UserName, Authentication.Password, WSType.CallCenter);
            if (user != null)
            {
                CallCenterID = user.CallCenterId;
                
                return true;
            }
            throw new AuthenticationException(ErrorMessages.AUTHENTICATION_ERROR_MSG);
        }

        /// <summary>
        /// Get Current CallCenterId
        /// </summary>
        /// <returns></returns>
        protected CallCenterDTO GetCallCenterInfo(int callCenterId)
        {
            return SecurityBL.Instance.GetCallCenter(callCenterId);
        }
    }
}
