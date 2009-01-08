﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.DataAccess;

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

        public Common.DataTransferObjects.MenuSecurityDTOCollection GetMenuSecurityList(int userId)
        {
            return MenuSecurityDAO.Instance.GetMenuSecurityListByUserID(userId);
        }

        #endregion
    }
}