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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.WebServices
{
    public class BaseCallCenterWebService : BaseWebService
    {
        protected override bool IsAuthenticated()
        {
            if (SecurityBL.Instance.WSUserLogin(Authentication.UserName, Authentication.Password, WSType.CallCenter))
                return true;
            throw new AuthenticationFailException("Access Deny.");
        }

        /// <summary>
        /// Get Current CallCenterId
        /// </summary>
        /// <returns></returns>
        protected int GetCallCenterId()
        {
            return SecurityBL.Instance.GetWSUser(Authentication.UserName).CallCenterId;
        }
    }
}
