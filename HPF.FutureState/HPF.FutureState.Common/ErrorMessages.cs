using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common
{
    public static class ErrorMessages
    {
        public const string AUTHENTICATION_ERROR_MSG = "Authentication failed, access to web service is denied";

        public const string PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET = "Foreclosure case set is null";
        public const string PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE = "Foreclosure case is null";       
        public const string EXCEPTION_DUPLICATE_FORECLOSURE_CASE = "Duplicate Foreclosure case";        
        public const string EXCEPTION_INVALID_AGENCY_CASE_NUM_OR_AGENCY_ID= "Either Agency Case Number or Agency ID is invalid";
        public const string EXCEPTION_EXISTING_AGENCY_CASE_NUM_AND_AGENCY_ID = "Case with pair of Agency Case Number and Agency ID already existed";
        public const string EXCEPTION_INVALID_FC_ID = "Invalid Foreclosure Case ID";
        public const string EXCEPTION_INVALID_FC_ID_FOR_AGENCY_ID = "Case with pair of Agency Case Number and Agency ID is not in Database";
        public const string EXCEPTION_MISSING_REQUIRED_FIELD = "Missing required fields";
        public const string EXCEPTION_MISCELLANEOUS = "Miscellaneous processing exception" ;
        public const string EXCEPTION_INVALID_CODE = "Invalid code";

        private static readonly Dictionary<string, string> errorMessageDict = new Dictionary<string, string>();

        public static string GetExceptionMessage(string exceptionId)
        {
            LoadErrorMessageDict();
            return errorMessageDict[exceptionId];
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
