using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using System.Linq;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;

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

        public RefCodeItemDTOCollection GetRefCodeItemsForAgency(string refCodeSetName)
        {
            return RefCodeItemDAO.Instance.GetRefCodeItemsFromDatabase("Y", refCodeSetName);            
        }

        public RefCodeItemDTOCollection GetRefCodeItems(RefCodeSearchCriteriaDTO criteria)
        {            
            if (criteria.CodeSetName == null && criteria.IncludedInActive != null)
            {
                DataValidationException dataEx = new DataValidationException();
                dataEx.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR1101, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1101));
                throw dataEx;
            }

            RefCodeItemDTOCollection results = RefCodeItemDAO.Instance.GetRefCodeItems(criteria);
            if (results.Count == 0)
            {
                DataValidationException dataEx = new DataValidationException();
                dataEx.ExceptionMessages.AddExceptionMessage(ErrorMessages.WARN1100, ErrorMessages.GetExceptionMessage(ErrorMessages.WARN1100));
                throw dataEx;
            }

            return results;
        }

        public RefCodeItemDTO GetRefCodeItem(int refCodeItemId)
        {
            return RefCodeItemDAO.Instance.GetRefCodeItem(refCodeItemId);
        }

        public void SaveRefCodeItem(RefCodeItemDTO refCode)
        {            
            if (string.IsNullOrEmpty(refCode.RefCodeSetName))
            {
                DataValidationException dataEx = new DataValidationException();
                dataEx.ExceptionMessages.AddExceptionMessage("ERROR", "A Code Set is required to save Ref Code Item.");
                throw dataEx;
            }

            RefCodeItemDAO.Instance.SaveRefCodeItem(refCode);
        }
    }
}
