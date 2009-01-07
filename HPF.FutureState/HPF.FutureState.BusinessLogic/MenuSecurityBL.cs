using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;

namespace HPF.FutureState.BusinessLogic
{
    public class MenuSecurityBL : IMenuSecurityBL
    {
        private static readonly MenuSecurityBL instance = new MenuSecurityBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static MenuSecurityBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected MenuSecurityBL()
        {            
        }

        #region IMenuSecurityBL Members

        public Common.DataTransferObjects.MenuSecurityDTOCollection GetMenuSecurityList(string userId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}