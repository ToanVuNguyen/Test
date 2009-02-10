﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class UserDTO : BaseDTO
    {

        public int? HPFUserId { get; set; }
        public string UserName { get; set; }
        private string _Password=string.Empty;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public char? IsActivate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin { get; set; }
        public string UserRole { get; set; }
          
    }
}
