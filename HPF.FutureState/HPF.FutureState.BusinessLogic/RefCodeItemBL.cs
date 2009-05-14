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
            RefCodeItemDTOCollection refCodes = RefCodeItemDAO.Instance.GetRefCodeItems();

            if (refCodeName == null || refCodeName == string.Empty)
                return GetRefCodeItemsForAgency(refCodes);

            return GetRefCodeItemsForAgency(refCodes, refCodeName);                
        }

        private RefCodeItemDTOCollection GetRefCodeItemsForAgency(RefCodeItemDTOCollection refCodes)
        {
            RefCodeItemDTOCollection output = new RefCodeItemDTOCollection();
            foreach (RefCodeItemDTO refCode in refCodes)
            {
                if (refCode.AgencyUsageInd == "Y")
                    output.Add(refCode);
            }
            return output;
        }

        private RefCodeItemDTOCollection GetRefCodeItemsForAgency(RefCodeItemDTOCollection refCodes, string codeName)
        {
            string codeNameUpper = codeName.ToUpper();
            RefCodeItemDTOCollection output = new RefCodeItemDTOCollection();
            foreach (RefCodeItemDTO refCode in refCodes)
            {
                if (refCode.AgencyUsageInd == "Y" && refCode.Code.ToUpper().CompareTo(codeNameUpper) == 0)
                    output.Add(refCode);
            }

            return output;
        }
    }
}
