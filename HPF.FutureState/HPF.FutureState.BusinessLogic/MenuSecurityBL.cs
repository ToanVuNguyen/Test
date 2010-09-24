using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.DataTransferObjects;

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

        public MenuSecurityDTOCollection GetMenuSecurityList(int userId)
        {
            var instance = MenuSecurityDAO.CreateInstance();
            return instance.GetMenuSecurityListByUserID(userId);
        }
        #endregion
        public MenuSecurityDTOCollection RetriveAllMenuSecurityByUser(int userId)
        {
            var instance = MenuSecurityDAO.CreateInstance();
            return instance.GetAllMenuSecurityListByUserID(userId);
        }
        public UserDTO RetriveUserInfoById(int userId)
        {
            return HPFUserDAO.Instance.GetHpfUserById(userId);
        }
        public void UpdateUserSecurity(MenuSecurityDTOCollection items,UserDTO user)
        {
            var instance = MenuSecurityDAO.CreateInstance();
            try
            {
                int latestMenuSecurityId = instance.GetLatestMenuSecurityId();
                instance.Begin();
                instance.UpdateHpfUser(user);
                foreach (MenuSecurityDTO item in items)
                {
                    if (item.StatusChanged == (byte)StatusChanged.Insert)
                    {
                        latestMenuSecurityId++;
                        item.MenuSecurityId = latestMenuSecurityId;
                        instance.InsertMenuSecurity(item);
                    }
                    else if (item.StatusChanged == (byte)StatusChanged.Update)
                        instance.UpdateMenuSecurity(item);
                    else 
                        instance.DeleteMenuSecurity(item.MenuSecurityId.Value);
                }
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.Cancel();
                throw ex;
            }
        }
        public enum StatusChanged : byte
        {
            Insert = 0, Remove = 1, Update = 2
        }
    }
}