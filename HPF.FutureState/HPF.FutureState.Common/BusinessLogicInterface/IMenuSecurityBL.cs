using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface IMenuSecurityBL
    {
        /// <summary>
        /// Get List of MenuSecurity
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        MenuSecurityDTOCollection GetMenuSecurityList(string userId);
    }
}
