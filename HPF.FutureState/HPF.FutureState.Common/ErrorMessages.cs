﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common
{
    public static class ErrorMessages
    {
        private static readonly Dictionary<int, string> errorMessageDict = new Dictionary<int, string>();

        public static string GetExceptionMessage(int exceptionId)
        {
            LoadErrorMessageDict();
            return null;
        }

        private static void LoadErrorMessageDict()
        {
            if (errorMessageDict.Count == 0)
            {
                //Load error message to errorMessageDict
            }
        }
    }
}
