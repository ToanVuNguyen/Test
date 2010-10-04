using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;
using System.Text.RegularExpressions;

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
        public void UpdateUserSecurity(MenuSecurityDTOCollection items, UserDTO user)
        {
            var instance = MenuSecurityDAO.CreateInstance();
            try
            {
                instance.Begin();
                DataValidationException ex = ValidateUser(user);
                if (ex.ExceptionMessages.Count > 0) throw ex;
                int latestMenuSecurityId = instance.GetLatestMenuSecurityId();
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
        public int InsertUserSecurity(MenuSecurityDTOCollection items, UserDTO user)
        {
            var instance = MenuSecurityDAO.CreateInstance();
            try
            {
                instance.Begin();
                DataValidationException ex = ValidateUser(user);
                if (ex.ExceptionMessages.Count > 0) throw ex;
                int latestMenuSecurityId = instance.GetLatestMenuSecurityId();
                user.HPFUserId = instance.InsertHpfUser(user);
                foreach (MenuSecurityDTO item in items)
                {
                    if (item.StatusChanged == (byte)StatusChanged.Insert)
                    {
                        latestMenuSecurityId++;
                        item.MenuSecurityId = latestMenuSecurityId;
                        item.HpfUserId = user.HPFUserId;
                        instance.InsertMenuSecurity(item);
                    }
                    else if (item.StatusChanged == (byte)StatusChanged.Update)
                        instance.UpdateMenuSecurity(item);
                    else
                        instance.DeleteMenuSecurity(item.MenuSecurityId.Value);
                }
                instance.Commit();
                return user.HPFUserId.Value;
            }
            catch (Exception ex)
            {
                instance.Cancel();
                throw ex;
            }
        }
        private DataValidationException ValidateUser(UserDTO user)
        {
            DataValidationException ex = new DataValidationException();
            HPFUserDTOCollection hpfUsers = HPFUserBL.Instance.RetriveHpfUsersFromDatabase();
            string regex = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
            Regex rxValidationEmail = new Regex(regex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (HPFUserDTO hpfUser in hpfUsers)
            {
                if ((hpfUser.HpfUserId != user.HPFUserId) && (hpfUser.UserLoginId == user.UserName))
                {
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Can not insert duplicated user name !!!" });
                    break;
                }
            }
            if (string.IsNullOrEmpty(user.UserName))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Can not insert an emty user name !" });
            if (string.IsNullOrEmpty(user.Password))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Can not insert a blank password !" });
            if (string.IsNullOrEmpty(user.FirstName))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Can not insert a blank first name !" });
            if (string.IsNullOrEmpty(user.LastName))
                ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Can not insert a blank last name !" });
            if (string.Compare(user.UserType, Constant.USER_TYPE_AGENCY) == 0)
            {
                if (string.IsNullOrEmpty(user.Email) || !rxValidationEmail.IsMatch(user.Email))
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Can not insert an invalid email !" });
                if (user.AgencyId == null)
                    ex.ExceptionMessages.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Please choose agency !" });
            }
            return ex;
        }
        public enum StatusChanged : byte
        {
            Insert = 0, Remove = 1, Update = 2
        }
    }
}