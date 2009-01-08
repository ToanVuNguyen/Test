using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.BusinessLogic
{
    public class MenuGroupBL:IMenuGroupBL
    {
        private static readonly MenuGroupBL instance = new MenuGroupBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static MenuGroupBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected MenuGroupBL()
        {
        }
        #region IMenuGroupBL Members
        public MenuGroupDTOCollection GetMenuGroupCollectionByUserID(int userId)
        {
            return MenuGroupDAO.Instance.GetMenuGroupCollectionByUserID(userId);
        }
        #endregion
    }
}
