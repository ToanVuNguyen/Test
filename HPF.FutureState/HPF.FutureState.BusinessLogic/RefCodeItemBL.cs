using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Validation;


namespace HPF.FutureState.BusinessLogic
{
    public class RefCodeItem : BaseBusinessLogic, IRefCodeItem
    {
        private static readonly RefCodeItem instance = new RefCodeItem();
        /// <summary>
        /// Singleton
        /// </summary>
        public static RefCodeItem Instance
        {
            get
            {
                return instance;
            }
        }

        protected RefCodeItem()
        {
            
        }

        #region Implementation of ICallLogBL     

        /// <summary>
        /// Get a Get Ref Code Item
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns></returns>
        public RefCodeItemDTOCollection GetRefCodeItem()
        {
            return RefCodeItemDAO.Instance.GetRefCodeItem();
        }
        #endregion        
    }
}
