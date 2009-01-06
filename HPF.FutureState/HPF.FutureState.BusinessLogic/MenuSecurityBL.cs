using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;

namespace HPF.FutureState.BusinessLogic
{
    public class MenuSecurityBL : IMenuSecurityBL
    {
        #region IMenuSecurityBL Members

        public Common.DataTransferObjects.MenuSecurityDTOCollection GetMenuSecurityList(string userId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}