using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeSetDTOCollection: BaseDTOCollection<RefCodeSetDTO>
    {
        public RefCodeSetDTO GetCodeSetByName(string codeSetName)
        {
            return this.SingleOrDefault(i => i.RefCodeSetName == codeSetName);
        }
    }
}
