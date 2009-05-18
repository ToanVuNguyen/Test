using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using System.Linq;


namespace HPF.FutureState.BusinessLogic
{
    public class RefCodeItemBL : BaseBusinessLogic, IRefCodeItem
    {
        private static readonly RefCodeItemBL instance = new RefCodeItemBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static RefCodeItemBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected RefCodeItemBL()
        {
            
        }
      
        /// <summary>
        /// Get a Get Ref Code Items
        /// </summary>        
        /// <returns></returns>
        public RefCodeItemDTOCollection GetRefCodeItems()
        {
            return RefCodeItemDAO.Instance.GetRefCodeItems();
        }

        public RefCodeItemDTOCollection GetRefCodeItemsForAgency(string refCodeName)
        {
            return RefCodeItemDAO.Instance.GetRefCodeItemsFromDatabase("Y", refCodeName);            
        }        
    }
}
