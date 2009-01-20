using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeItemDTOCollection : BaseDTOCollection<RefCodeItemDTO>
    {
        private Dictionary<string, RefCodeItemDTOCollection> _RefCodeItemList;

        public RefCodeItemDTOCollection()
        {
            _RefCodeItemList = new Dictionary<string, RefCodeItemDTOCollection>();
        }

        public RefCodeItemDTOCollection GetRefCodeItemByRefCode(string refCode)
        {            
            if (_RefCodeItemList.ContainsKey(refCode))
                return _RefCodeItemList[refCode];
            else
            {
                RefCodeItemDTOCollection refCodeList = new RefCodeItemDTOCollection();
                foreach (var item in this)
                {
                    if (item.RefCodeSetName.ToUpper().Trim() == refCode.ToUpper().Trim())
                        refCodeList.Add(item);
                }
                _RefCodeItemList.Add(refCode, refCodeList);
                return refCodeList;
            }            
        }
    }
}
