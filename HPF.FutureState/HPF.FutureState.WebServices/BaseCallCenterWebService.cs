using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.WebServices
{
    public class BaseCallCenterWebService : BaseWebService
    {
        protected override bool IsAuthenticated()
        {
            var user = SecurityBL.Instance.WSUserLogin(Authentication.UserName, Authentication.Password, WSType.CallCenter);
            if (user != null)
            {
                CurrentCallCenterID = user.CallCenterId;                
                return true;
            }
            throw new AuthenticationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0451));
        }

        /// <summary>
        /// Get Current CallCenterId
        /// </summary>
        /// <returns></returns>
        protected static CallCenterDTO GetCallCenterInfo(int callCenterId)
        {
            return SecurityBL.Instance.GetCallCenter(callCenterId);
        }
    }
}
