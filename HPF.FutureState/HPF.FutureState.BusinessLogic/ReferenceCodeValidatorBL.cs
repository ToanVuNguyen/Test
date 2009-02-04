using HPF.FutureState.Common.BusinessLogicInterface;

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
            if ((refCodeItemCollectionByCode == null || refCodeItemCollectionByCode.Count < 1) && codeValue != null)
                return false;
            //
            return refCodeItemCollectionByCode.ContainCode(codeValue);            
        }        
    }
}
