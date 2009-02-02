using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface IReferenceCodeValidatorBL
    {
        /// <summary>
        /// Reference value validator
        /// </summary>
        /// <param name="refCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Validate(string refCode, string value);
    }
}
