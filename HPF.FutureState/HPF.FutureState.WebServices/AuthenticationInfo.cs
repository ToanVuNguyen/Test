using System.Web.Services.Protocols;

namespace HPF.FutureState.WebServices
{
    public class AuthenticationInfo : SoapHeader
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public AuthenticationInfo()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
