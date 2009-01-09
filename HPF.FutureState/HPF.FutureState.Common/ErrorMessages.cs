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

        public static string GetExceptionMessageCombined(string exceptionId)
        {
            LoadErrorMessageDict();
            if (errorMessageDict.ContainsKey(exceptionId))
                return exceptionId + "--" + errorMessageDict[exceptionId];            
                return "0000--Unknow Error.";
        }

        private static void LoadErrorMessageDict()
        {
            if (errorMessageDict.Count == 0)
            {
                errorMessageDict.Add("P-WS-SFC-00100","An AgencyId is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00101","A ProgramId is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00102","An AgencyCaseNum is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00103","An IntakeDt is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00104","A CaseSourceCd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00105","A BorrowerFname is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00106","A BorrowerLname is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00107","A PrimaryContactNo is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00108","A ContactAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00109","A ContactCity is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00110","A ContactStateCd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00111","A ContactZip is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00112","A PropAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00113","A PropCity is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00114","A PropStateCd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00115","A PropZip is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00116","The FundingConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00117","The ServicerConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00118","A CounselorIdRef of the assigned counselor is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00119","A CounselorLname is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00120","A CounselorFname is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00121","A CounselorEmail is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00122","A CounselorPhone is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00123","The OwnerOccupiedInd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00124","The PrimaryResidenceInd is required to save a foreclosure case.");
                errorMessageDict.Add("P-WS-SFC-00125","A ChgLstUserId is required to save a foreclosure case.");               
                errorMessageDict.Add("P-WS-SFC-00200","An invalid code was provided for IncomeEarnersCd.");
                errorMessageDict.Add("P-WS-SFC-00201","An invalid code was provided for CaseSourceCd.");
                errorMessageDict.Add("P-WS-SFC-00202","An invalid code was provided for RaceCd.");
                errorMessageDict.Add("P-WS-SFC-00203","An invalid code was provided for HouseholdCd.");
                errorMessageDict.Add("P-WS-SFC-00204","An invalid code was provided for DfltReason1stCd.");
                errorMessageDict.Add("P-WS-SFC-00205","An invalid code was provided for DfltReason2ndCd.");
                errorMessageDict.Add("P-WS-SFC-00206","An invalid code was provided for HudTerminationReasonCd.");
                errorMessageDict.Add("P-WS-SFC-00207","An invalid code was provided for HudOutcomeCd.");
                errorMessageDict.Add("P-WS-SFC-00208","An invalid code was provided for CounselingDurationCd.");
                errorMessageDict.Add("P-WS-SFC-00209","An invalid code was provided for GenderCd.");
                errorMessageDict.Add("P-WS-SFC-00210","An invalid code was provided for ContactStateCd.");
                errorMessageDict.Add("P-WS-SFC-00211","An invalid code was provided for PropStateCd.");
                errorMessageDict.Add("P-WS-SFC-00212","An invalid code was provided for BorrowerEducLevelCompletedCd.");
                errorMessageDict.Add("P-WS-SFC-00213","An invalid code was provided for BorrowerMaritalStatusCd.");
                errorMessageDict.Add("P-WS-SFC-00214","An invalid code was provided for BorrowerPreferredLangCd.");
                errorMessageDict.Add("P-WS-SFC-00215","An invalid code was provided for SummarySentOtherCd.");
                errorMessageDict.Add("P-WS-SFC-00216","An invalid code was provided for PropertyCd.");
                errorMessageDict.Add("P-WS-SFC-00217","An invalid code was provided for MilitaryServiceCd.");
                
                errorMessageDict.Add("W-WS-SFC-00300","An IncomeEarnersCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00301","A RaceCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00302","A HouseholdCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00303","A DfltReason1stCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00304","A DfltReason2ndCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00305","A HudOutcomeCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00306","A CounselingDurationCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00307","A GenderCd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00308","A BorrowerDob is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00309","The BankruptcyInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00310","The HispanicInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00311","The FcNoticeReceivedInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00312","An OccupantNum is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00313","LoanDefltReasonNotes are required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00314","ActionItemsNotes is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00315","The DiscussedSolutionWithSrvcrInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00316","The WorkedWithAnotherAgencyInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00317","The ContactedSrvcrRecentlyInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00318","The HasWorkoutPlanInd is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00319","A HouseholdGrossAnnualIncomeAmt is required to complete a foreclosure case.");
                errorMessageDict.Add("W-WS-SFC-00320","A Loan1st2ndCd is required on all loans to complete a foreclosure case.");                            
            }
        }
    }
}
