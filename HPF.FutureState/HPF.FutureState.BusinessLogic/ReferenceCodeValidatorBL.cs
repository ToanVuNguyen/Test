using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common;

namespace HPF.FutureState.BusinessLogic
{
    public class ReferenceCodeValidatorBL : IReferenceCodeValidatorBL
    {
        public bool Validate(string refCode, string codeValue)
        {
            //Get RefCode
            var refCodeItemCollection = RefCodeItemBL.Instance.GetRefCodeItems();
            if (refCodeItemCollection == null || refCodeItemCollection.Count < 1)
                return false;
            //Get group of RefCode with same RefCode name.
            //The group was cached itself in refCodeItemCollection.
            var refCodeItemCollectionByCode = refCodeItemCollection.GetRefCodeItemsByRefCode(refCode);
            if ((refCodeItemCollectionByCode == null || refCodeItemCollectionByCode.Count < 1) && !string.IsNullOrEmpty(codeValue))
                return false;
            //
            if (!string.IsNullOrEmpty(codeValue))
            {
                var codeItem = refCodeItemCollectionByCode.GetRefCodeItemByCode(codeValue);
                if (codeItem == null || codeItem.ActiveInd != Constant.INDICATOR_YES)
                    return false;
            }
            return true;
            //return refCodeItemCollectionByCode.ContainCode(codeValue);
        }
    }
}
