﻿using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class HPFUserDTO:BaseDTO
    {
        public int? HpfUserId { get; set; }
        public string UserLoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ActiveInd { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}
