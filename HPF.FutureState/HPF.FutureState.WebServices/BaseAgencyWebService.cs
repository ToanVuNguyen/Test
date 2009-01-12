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
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.WebServices
{
    public class BaseAgencyWebService : BaseWebService
    {
        protected override bool IsAuthenticated()
        {          
            WSUserDTO user = SecurityBL.Instance.WSUserLogin(Authentication.UserName, Authentication.Password, WSType.Agency);
            if (user != null)
                return true;
            throw new AuthenticationException("Access Deny.");
        }

        /// <summary>
        /// Get Current AgencyId
        /// </summary>
        /// <returns></returns>
        protected int GetAgencyId()
        {
            return SecurityBL.Instance.GetWSUser(Authentication.UserName).AgencyId;
        }
    }
}
