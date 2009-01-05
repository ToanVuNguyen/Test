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
        public const string PROCESSING_EXCEPTION_DUPLICATE_FORECLOSURE_CASE = "Duplicate Foreclosure case";        
        public const string PROCESSING_EXCEPTION_INVALID_AGENCY_CASE_NUM_OR_AGENCY_ID= "Either Agency Case Number or Agency ID is invalid";
        public const string PROCESSING_EXCEPTION_EXISTING_AGENCY_CASE_NUM_AND_AGENCY_ID = "Case with pair of Agency Case Number and Agency ID already existed";
        public const string PROCESSING_EXCEPTION_INVALID_FC_ID = "Invalid Foreclosure Case ID";
        public const string PROCESSING_EXCEPTION_INVALID_FC_ID_FOR_AGENCY_ID = "Case with pair of Agency Case Number and Agency ID is not in Database";
        public const string PROCESSING_EXCEPTION_MISSING_REQUIRED_FIELD = "Missing required fields";
        public const string MISC_PROCESSING_EXCEPTION = "Miscellaneous processing exception" ;

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
