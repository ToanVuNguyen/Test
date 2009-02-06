using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.WebServices
{
    public class BaseAgencyWebService : BaseWebService
    {
        protected override bool IsAuthenticated()
        {          
            var user = SecurityBL.Instance.WSUserLogin(Authentication.UserName, Authentication.Password, WSType.Agency);
            if (user != null)
            {
                CurrentAgencyID = user.AgencyId;
                return true;
            }
            throw new AuthenticationException(ErrorMessages.AUTHENTICATION_ERROR_MSG);
        }        
    }
}
