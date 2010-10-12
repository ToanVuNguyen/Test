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
            var ex = ValidateRefCodeItem(refCode);
            if (ex.Count > 0)
            {
                var ex1 = new DataValidationException(ex);
                throw ex1;
            }
            RefCodeItemDAO.Instance.SaveRefCodeItem(refCode);
        }
        /// <summary>
        /// 1: Min request validate of Code Set
        /// 2: Min request validate of Code Value
        /// 3: Min request validate of Code Description
        /// 4: Min request validate of Sort Order
        /// 5: Validate of sort order
        /// </summary>
        private ExceptionMessageCollection ValidateRefCodeItem(RefCodeItemDTO refCode)
        {
            ExceptionMessageCollection ex = new ExceptionMessageCollection();
            ex = HPFValidator.ValidateToGetExceptionMessage(refCode, Constant.RULESET_MIN_REQUIRE_FIELD) ;
            if (ex.Count>0)
                return ex;
            //Check duplicate sort order
            RefCodeSearchCriteriaDTO criteria = new RefCodeSearchCriteriaDTO();
            criteria.CodeSetName = refCode.RefCodeSetName;
            criteria.IncludedInActive = true;
            RefCodeItemDTOCollection results = RefCodeItemDAO.Instance.GetRefCodeItems(criteria);
            foreach (RefCodeItemDTO item in results)
                if ((item.SortOrder == refCode.SortOrder) &&(string.Compare(item.CodeValue,refCode.CodeValue)!=0))
                {
                    ex.AddExceptionMessage(ErrorMessages.ERR1123, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1123));
                    break;
                }
            return ex;
        }  
    }
}
