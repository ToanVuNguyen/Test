using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeItemDTOCollection : BaseDTOCollection<RefCodeItemDTO>
    {
        private readonly Dictionary<string, RefCodeItemDTOCollection> _RefCodeItemList;

        public RefCodeItemDTOCollection()
        {
            _RefCodeItemList = new Dictionary<string, RefCodeItemDTOCollection>();
        }

        public RefCodeItemDTOCollection GetRefCodeItemsByRefCode(string refCode)
        {            
            if (_RefCodeItemList.ContainsKey(refCode))
                return _RefCodeItemList[refCode];
            var refCodeList = new RefCodeItemDTOCollection();
            foreach (var item in this)
            {
                if (item.RefCodeSetName.ToUpper().Trim() == refCode.ToUpper().Trim())
                    refCodeList.Add(item);
            }
            _RefCodeItemList.Add(refCode, refCodeList);
            return refCodeList;
        }
        /// <summary>
        /// Check code value is containted in the collection or not
        /// </summary>
        /// <param name="codeValue"></param>
        /// <returns></returns>
        public bool ContainCode(string codeValue)
        {
            
            if (string.IsNullOrEmpty(codeValue))
                return true;

            return (this.SingleOrDefault(item => item.CodeValue.ToUpper().Trim() == codeValue.ToUpper().Trim()) != null);
            

        }        
    }
}
