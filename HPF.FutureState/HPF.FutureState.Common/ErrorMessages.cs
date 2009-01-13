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
                errorMessageDict.Add("ERR100","An AgencyId is required to save a foreclosure case.");
                errorMessageDict.Add("ERR101","A ProgramId is required to save a foreclosure case.");
                errorMessageDict.Add("ERR102","An AgencyCaseNum is required to save a foreclosure case.");
                errorMessageDict.Add("ERR103","An IntakeDt is required to save a foreclosure case.");
                errorMessageDict.Add("ERR104","A CaseSourceCd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR105","A BorrowerFname is required to save a foreclosure case.");
                errorMessageDict.Add("ERR106","A BorrowerLname is required to save a foreclosure case.");
                errorMessageDict.Add("ERR107","A PrimaryContactNo is required to save a foreclosure case.");
                errorMessageDict.Add("ERR108","A ContactAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add("ERR109","A ContactCity is required to save a foreclosure case.");
                errorMessageDict.Add("ERR110","A ContactStateCd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR111","A ContactZip is required to save a foreclosure case.");
                errorMessageDict.Add("ERR112","A PropAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add("ERR113","A PropCity is required to save a foreclosure case.");
                errorMessageDict.Add("ERR114","A PropStateCd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR115","A PropZip is required to save a foreclosure case.");
                errorMessageDict.Add("ERR116","The FundingConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR117","The ServicerConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR118","A CounselorIdRef of the assigned counselor is required to save a foreclosure case.");
                errorMessageDict.Add("ERR119","A CounselorLname is required to save a foreclosure case.");
                errorMessageDict.Add("ERR120","A CounselorFname is required to save a foreclosure case.");
                errorMessageDict.Add("ERR121","A CounselorEmail is required to save a foreclosure case.");
                errorMessageDict.Add("ERR122","A CounselorPhone is required to save a foreclosure case.");
                errorMessageDict.Add("ERR123","The OwnerOccupiedInd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR124","The PrimaryResidenceInd is required to save a foreclosure case.");
                errorMessageDict.Add("ERR125","A ChgLstUserId is required to save a foreclosure case.");
                errorMessageDict.Add("ERR126", "Missing loans.  At least one loan is required to save a foreclosure case.");
                errorMessageDict.Add("ERR127", "One or more of your loans is missing a ServicerID.");
                errorMessageDict.Add("ERR128", "One or more of your loans is missing a Loan Num.");    

                errorMessageDict.Add("ERR200","An invalid code was provided for IncomeEarnersCd.");
                errorMessageDict.Add("ERR201","An invalid code was provided for CaseSourceCd.");
                errorMessageDict.Add("ERR202","An invalid code was provided for RaceCd.");
                errorMessageDict.Add("ERR203","An invalid code was provided for HouseholdCd.");
                errorMessageDict.Add("ERR204","An invalid code was provided for DfltReason1stCd.");
                errorMessageDict.Add("ERR205","An invalid code was provided for DfltReason2ndCd.");
                errorMessageDict.Add("ERR206","An invalid code was provided for HudTerminationReasonCd.");
                errorMessageDict.Add("ERR207","An invalid code was provided for HudOutcomeCd.");
                errorMessageDict.Add("ERR208","An invalid code was provided for CounselingDurationCd.");
                errorMessageDict.Add("ERR209","An invalid code was provided for GenderCd.");
                errorMessageDict.Add("ERR210","An invalid code was provided for ContactStateCd.");
                errorMessageDict.Add("ERR211","An invalid code was provided for PropStateCd.");
                errorMessageDict.Add("ERR212","An invalid code was provided for BorrowerEducLevelCompletedCd.");
                errorMessageDict.Add("ERR213","An invalid code was provided for BorrowerMaritalStatusCd.");
                errorMessageDict.Add("ERR214","An invalid code was provided for BorrowerPreferredLangCd.");
                errorMessageDict.Add("ERR215","An invalid code was provided for SummarySentOtherCd.");
                errorMessageDict.Add("ERR216","An invalid code was provided for PropertyCd.");
                errorMessageDict.Add("ERR217","An invalid code was provided for MilitaryServiceCd.");

                errorMessageDict.Add("ERR250", "Neither a HPF FcId nor an AgencyCaseNum was provided. Please provide an AgencyCaseNum for new cases and both and AgencyCaseNum and HPF FcId are required for updates to existing cases.");
                errorMessageDict.Add("ERR251", "An invalid HPF FcID was provided.  Please correct the FcID and resend the foreclosure case.");
                errorMessageDict.Add("ERR252", "An invalid AgencyID was provided for the Corresponding FcId.  Your agency does not own this foreclosure case. ");
                errorMessageDict.Add("ERR253", "Duplicate Case Found for Servicer: {0}, Account Number: {1}, Zip Code: {2}. "
                                                +"Borrower Name: {3} {4} The case is currently being worked on by: {5} {6} of {7}. "
                                                +"Counselor Phone: {8} {9} Counselor Email: {10} Last Outcome Date: {11} for the case without an {12} "
                                                +"Last Outcome: {13} ");                
                errorMessageDict.Add("ERR254", "An update was submitted without a FcID. All updates require a HPF FcId. Please correct and resubmit the case.");
                errorMessageDict.Add("ERR255", "A previously completed case cannot become uncompleted. An update was submitted without all fields required to compleg.");
                errorMessageDict.Add("ERR256", "Only one loan can be designated as '1st'.");
                

                errorMessageDict.Add("WARN300","An IncomeEarnersCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN301","A RaceCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN302","A HouseholdCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN303","A DfltReason1stCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN304","A DfltReason2ndCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN305","A HudOutcomeCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN306","A CounselingDurationCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN307","A GenderCd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN308","A BorrowerDob is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN309","The BankruptcyInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN310","The HispanicInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN311","The FcNoticeReceivedInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN312","An OccupantNum is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN313","LoanDefltReasonNotes are required to complete a foreclosure case.");
                errorMessageDict.Add("WARN314","ActionItemsNotes is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN315","The DiscussedSolutionWithSrvcrInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN316","The WorkedWithAnotherAgencyInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN317","The ContactedSrvcrRecentlyInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN318","The HasWorkoutPlanInd is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN319","A HouseholdGrossAnnualIncomeAmt is required to complete a foreclosure case.");
                errorMessageDict.Add("WARN320","A Loan1st2ndCd is required on all loans to complete a foreclosure case.");

                errorMessageDict.Add("WARN321", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN322", "A TermLengthCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN323", "A LoanDelinqStatusCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN324", "A InterestRate is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN325", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN326", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN327", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN328", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN329", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN330", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN331", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN332", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN333", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN334", "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN335", "A Budget is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN336", "A BudgetSubcategoryID is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add("WARN337", "A BudgetItemAmt is required on all loans to complete a foreclosure case.");

            }
        }
    }
}
