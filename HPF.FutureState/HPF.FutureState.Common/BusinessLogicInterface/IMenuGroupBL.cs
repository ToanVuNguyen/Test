using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface IMenuGroupBL
    {
        /// <summary>
        /// Get List of MenuSecurity
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        MenuGroupDTOCollection GetMenuGroupCollectionByUserID(int userId);
    }
    
}
