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
        public const string ERR100 = "ERR100";
        public const string ERR101 = "ERR101";
        public const string ERR102 = "ERR102";
        public const string ERR103 = "ERR103";
        public const string ERR104 = "ERR104";
        public const string ERR105 = "ERR105";
        public const string ERR106 = "ERR106";
        public const string ERR107 = "ERR107";
        public const string ERR108 = "ERR108";
        public const string ERR109 = "ERR109";
        public const string ERR110 = "ERR110";
        public const string ERR111 = "ERR111";
        public const string ERR112 = "ERR112";
        public const string ERR113 = "ERR113";
        public const string ERR114 = "ERR114";
        public const string ERR115 = "ERR115";
        public const string ERR116 = "ERR116";
        public const string ERR117 = "ERR117";
        public const string ERR118 = "ERR118";
        public const string ERR119 = "ERR119";
        public const string ERR120 = "ERR120";
        public const string ERR121 = "ERR121";
        public const string ERR122 = "ERR122";
        public const string ERR123 = "ERR123";
        public const string ERR124 = "ERR124";
        public const string ERR125 = "ERR125";
        public const string ERR126 = "ERR126";
        public const string ERR127 = "ERR127";
        public const string ERR128 = "ERR128";
        public const string ERR200 = "ERR200";
        public const string ERR201 = "ERR201";
        public const string ERR202 = "ERR202";
        public const string ERR203 = "ERR203";
        public const string ERR204 = "ERR204";
        public const string ERR205 = "ERR205";
        public const string ERR206 = "ERR206";
        public const string ERR207 = "ERR207";
        public const string ERR208 = "ERR208";
        public const string ERR209 = "ERR209";
        public const string ERR210 = "ERR210";
        public const string ERR211 = "ERR211";
        public const string ERR212 = "ERR212";
        public const string ERR213 = "ERR213";
        public const string ERR214 = "ERR214";
        public const string ERR215 = "ERR215";
        public const string ERR216 = "ERR216";
        public const string ERR217 = "ERR217";
        public const string ERR250 = "ERR250";
        public const string ERR251 = "ERR251";
        public const string ERR252 = "ERR252";
        public const string ERR253 = "ERR253";
        public const string ERR254 = "ERR254";
        public const string ERR255 = "ERR255";
        public const string ERR256 = "ERR256";
        
        public const string WARN300 = "WARN300";
        public const string WARN301 = "WARN301";
        public const string WARN302 = "WARN302";
        public const string WARN303 = "WARN303";
        public const string WARN304 = "WARN304";
        public const string WARN305 = "WARN305";
        public const string WARN306 = "WARN306";
        public const string WARN307 = "WARN307";
        public const string WARN308 = "WARN308";
        public const string WARN309 = "WARN309";
        public const string WARN310 = "WARN310";
        public const string WARN311 = "WARN311";
        public const string WARN312 = "WARN312";
        public const string WARN313 = "WARN313";
        public const string WARN314 = "WARN314";
        public const string WARN315 = "WARN315";
        public const string WARN316 = "WARN316";
        public const string WARN317 = "WARN317";
        public const string WARN318 = "WARN318";
        public const string WARN319 = "WARN319";
        public const string WARN320 = "WARN320";
        public const string WARN321 = "WARN321";
        public const string WARN322 = "WARN322";
        public const string WARN323 = "WARN323";
        public const string WARN324 = "WARN324";
        public const string WARN325 = "WARN325";
        public const string WARN326 = "WARN326";
        public const string WARN327 = "WARN327";
        public const string WARN328 = "WARN328";
        public const string WARN329 = "WARN329";
        public const string WARN330 = "WARN330";
        public const string WARN331 = "WARN331";
        public const string WARN332 = "WARN332";
        public const string WARN333 = "WARN333";
        public const string WARN334 = "WARN334";
        public const string WARN335 = "WARN335";
        public const string WARN336 = "WARN336";
        public const string WARN337 = "WARN337";
        //
        public const string ERR600 = "ERR600";
        public const string ERR601 = "ERR601";
        public const string ERR999 = "ERR999";

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
                errorMessageDict.Add(ERR100,"An AgencyId is required to save a foreclosure case.");
                errorMessageDict.Add(ERR101,"A ProgramId is required to save a foreclosure case.");
                errorMessageDict.Add(ERR102,"An AgencyCaseNum is required to save a foreclosure case.");
                errorMessageDict.Add(ERR103,"An IntakeDt is required to save a foreclosure case.");
                errorMessageDict.Add(ERR104,"A CaseSourceCd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR105,"A BorrowerFname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR106,"A BorrowerLname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR107,"A PrimaryContactNo is required to save a foreclosure case.");
                errorMessageDict.Add(ERR108,"A ContactAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add(ERR109,"A ContactCity is required to save a foreclosure case.");
                errorMessageDict.Add(ERR110,"A ContactStateCd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR111,"A ContactZip is required to save a foreclosure case.");
                errorMessageDict.Add(ERR112,"A PropAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add(ERR113,"A PropCity is required to save a foreclosure case.");
                errorMessageDict.Add(ERR114,"A PropStateCd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR115,"A PropZip is required to save a foreclosure case.");
                errorMessageDict.Add(ERR116,"The FundingConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR117,"The ServicerConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR118,"A CounselorIdRef of the assigned counselor is required to save a foreclosure case.");
                errorMessageDict.Add(ERR119,"A CounselorLname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR120,"A CounselorFname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR121,"A CounselorEmail is required to save a foreclosure case.");
                errorMessageDict.Add(ERR122,"A CounselorPhone is required to save a foreclosure case.");
                errorMessageDict.Add(ERR123,"The OwnerOccupiedInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR124,"The PrimaryResidenceInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR125,"A ChgLstUserId is required to save a foreclosure case.");
                errorMessageDict.Add(ERR126, "Missing loans.  At least one loan is required to save a foreclosure case.");
                errorMessageDict.Add(ERR127, "One or more of your loans is missing a ServicerID.");
                errorMessageDict.Add(ERR128, "One or more of your loans is missing a Loan Num.");    

                errorMessageDict.Add(ERR200,"An invalid code was provided for IncomeEarnersCd.");
                errorMessageDict.Add(ERR201,"An invalid code was provided for CaseSourceCd.");
                errorMessageDict.Add(ERR202,"An invalid code was provided for RaceCd.");
                errorMessageDict.Add(ERR203,"An invalid code was provided for HouseholdCd.");
                errorMessageDict.Add(ERR204,"An invalid code was provided for DfltReason1stCd.");
                errorMessageDict.Add(ERR205,"An invalid code was provided for DfltReason2ndCd.");
                errorMessageDict.Add(ERR206,"An invalid code was provided for HudTerminationReasonCd.");
                errorMessageDict.Add(ERR207,"An invalid code was provided for HudOutcomeCd.");
                errorMessageDict.Add(ERR208,"An invalid code was provided for CounselingDurationCd.");
                errorMessageDict.Add(ERR209,"An invalid code was provided for GenderCd.");
                errorMessageDict.Add(ERR210,"An invalid code was provided for ContactStateCd.");
                errorMessageDict.Add(ERR211,"An invalid code was provided for PropStateCd.");
                errorMessageDict.Add(ERR212,"An invalid code was provided for BorrowerEducLevelCompletedCd.");
                errorMessageDict.Add(ERR213,"An invalid code was provided for BorrowerMaritalStatusCd.");
                errorMessageDict.Add(ERR214,"An invalid code was provided for BorrowerPreferredLangCd.");
                errorMessageDict.Add(ERR215,"An invalid code was provided for SummarySentOtherCd.");
                errorMessageDict.Add(ERR216,"An invalid code was provided for PropertyCd.");
                errorMessageDict.Add(ERR217,"An invalid code was provided for MilitaryServiceCd.");

                errorMessageDict.Add(ERR250, "Neither a HPF FcId nor an AgencyCaseNum was provided. Please provide an AgencyCaseNum for new cases and both and AgencyCaseNum and HPF FcId are required for updates to existing cases.");
                errorMessageDict.Add(ERR251, "An invalid HPF FcID was provided.  Please correct the FcID and resend the foreclosure case.");
                errorMessageDict.Add(ERR252, "An invalid AgencyID was provided for the Corresponding FcId.  Your agency does not own this foreclosure case. ");
                errorMessageDict.Add(ERR253, "Duplicate Case Found for Servicer: {0}, Account Number: {1}, Zip Code: {2}. "
                                              +"Borrower Name: {3} {4} The case is currently being worked on by: {5} {6} of {7}. "
                                              +"Counselor Phone: {8} {9} Counselor Email: {10} Last Outcome Date: {11} "
                                              +"Last Outcome: {12} ");                
                errorMessageDict.Add(ERR254, "An update was submitted without a FcID. All updates require a HPF FcId. Please correct and resubmit the case.");
                errorMessageDict.Add(ERR255, "A previously completed case cannot become uncompleted. An update was submitted without all fields required to compleg.");
                errorMessageDict.Add(ERR256, "Only one loan can be designated as '1st'.");
                

                errorMessageDict.Add(WARN300,"An IncomeEarnersCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN301,"A RaceCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN302,"A HouseholdCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN303,"A DfltReason1stCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN304,"A DfltReason2ndCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN305,"A HudOutcomeCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN306,"A CounselingDurationCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN307,"A GenderCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN308,"A BorrowerDob is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN309,"The BankruptcyInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN310,"The HispanicInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN311,"The FcNoticeReceivedInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN312,"An OccupantNum is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN313,"LoanDefltReasonNotes are required to complete a foreclosure case.");
                errorMessageDict.Add(WARN314,"ActionItemsNotes is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN315,"The DiscussedSolutionWithSrvcrInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN316,"The WorkedWithAnotherAgencyInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN317,"The ContactedSrvcrRecentlyInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN318,"The HasWorkoutPlanInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN319,"A HouseholdGrossAnnualIncomeAmt is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN320,"A Loan1st2ndCd is required on all loans to complete a foreclosure case.");

                errorMessageDict.Add(WARN321, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN322, "A TermLengthCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN323, "A LoanDelinqStatusCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN324, "A InterestRate is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN325, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN326, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN327, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN328, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN329, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN330, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN331, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN332, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN333, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN334, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN335, "A Budget is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN336, "A BudgetSubcategoryID is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN337, "A BudgetItemAmt is required on all loans to complete a foreclosure case.");
                
                //
                errorMessageDict.Add(ERR999, "You don't have permission to access this page.");

            }
        }
    }
}
