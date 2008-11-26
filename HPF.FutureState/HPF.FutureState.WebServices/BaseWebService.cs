using System;
using System.Reflection;
using System.Web.Security;
using System.Web.Services;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.WebServices
{
    public class BaseWebService : WebService
    {
        public AuthenticationInfo Authentication;        

        /// <summary>
        /// Authenticate checking, this method can override for specific web service.
        /// </summary>
        /// <returns>True: Success, False : Fail</returns>
        protected virtual bool IsAuthenticated()
        {           
            return true;
        }        
    }
}
