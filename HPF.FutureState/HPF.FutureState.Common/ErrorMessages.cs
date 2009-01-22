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
        public const string ERR0100 = "ERR0100";
        public const string ERR0101 = "ERR0101";
        public const string ERR0102 = "ERR0102";
        public const string ERR0103 = "ERR0103";
        public const string ERR0104 = "ERR0104";
        public const string ERR0105 = "ERR0105";
        public const string ERR0106 = "ERR0106";
        public const string ERR0107 = "ERR0107";
        public const string ERR0108 = "ERR0108";
        public const string ERR0109 = "ERR0109";
        public const string ERR0110 = "ERR0110";
        public const string ERR0111 = "ERR0111";
        public const string ERR0112 = "ERR0112";
        public const string ERR0113 = "ERR0113";
        public const string ERR0114 = "ERR0114";
        public const string ERR0115 = "ERR0115";
        public const string ERR0116 = "ERR0116";
        public const string ERR0117 = "ERR0117";
        public const string ERR0118 = "ERR0118";
        public const string ERR0119 = "ERR0119";
        public const string ERR0120 = "ERR0120";
        public const string ERR0121 = "ERR0121";
        public const string ERR0122 = "ERR0122";
        public const string ERR0123 = "ERR0123";
        public const string ERR0124 = "ERR0124";
        public const string ERR0125 = "ERR0125";
        public const string ERR0126 = "ERR0126";
        public const string ERR0127 = "ERR0127";
        public const string ERR0128 = "ERR0128";
        public const string ERR0129 = "ERR0129";

        public const string ERR0200 = "ERR0200";
        public const string ERR0201 = "ERR0201";
        public const string ERR0202 = "ERR0202";
        public const string ERR0203 = "ERR0203";
        public const string ERR0204 = "ERR0204";
        public const string ERR0205 = "ERR0205";
        public const string ERR0206 = "ERR0206";
        public const string ERR0207 = "ERR0207";
        public const string ERR0208 = "ERR0208";
        public const string ERR0209 = "ERR0209";
        public const string ERR0210 = "ERR0210";
        public const string ERR0211 = "ERR0211";
        public const string ERR0212 = "ERR0212";
        public const string ERR0213 = "ERR0213";
        public const string ERR0214 = "ERR0214";
        public const string ERR0215 = "ERR0215";
        public const string ERR0216 = "ERR0216";
        public const string ERR0217 = "ERR0217";

        public const string ERR0250 = "ERR0250";
        public const string ERR0251 = "ERR0251";
        public const string ERR0252 = "ERR0252";
        public const string ERR0253 = "ERR0253";
        public const string ERR0254 = "ERR0254";
        public const string ERR0255 = "ERR0255";
        public const string ERR0256 = "ERR0256";

        public const string ERR0350 = "A CcCallKey is required to save a call.";
        public const string ERR0351 = "A StartDt is required to save a call.";
        public const string ERR0352 = "An EndDt is required to save a call.";
        public const string ERR0353 = "A FinalDispoCd is required to save a call.";
        
        public const string WARN0300 = "WARN0300";
        public const string WARN0301 = "WARN0301";
        public const string WARN0302 = "WARN0302";
        public const string WARN0303 = "WARN0303";
        public const string WARN0304 = "WARN0304";
        public const string WARN0305 = "WARN0305";
        public const string WARN0306 = "WARN0306";
        public const string WARN0307 = "WARN0307";
        public const string WARN0308 = "WARN0308";
        public const string WARN0309 = "WARN0309";
        public const string WARN0310 = "WARN0310";
        public const string WARN0311 = "WARN0311";
        public const string WARN0312 = "WARN0312";
        public const string WARN0313 = "WARN0313";
        public const string WARN0314 = "WARN0314";
        public const string WARN0315 = "WARN0315";
        public const string WARN0316 = "WARN0316";
        public const string WARN0317 = "WARN0317";
        public const string WARN0318 = "WARN0318";
        public const string WARN0319 = "WARN0319";
        public const string WARN0320 = "WARN0320";
        public const string WARN0321 = "WARN0321";
        public const string WARN0322 = "WARN0322";
        public const string WARN0323 = "WARN0323";
        public const string WARN0324 = "WARN0324";
        public const string WARN0325 = "WARN0325";
        public const string WARN0326 = "WARN0326";
        public const string WARN0327 = "WARN0327";
        public const string WARN0328 = "WARN0328";
        public const string WARN0329 = "WARN0329";

        public const string WARN0330 = "WARN0330";
        public const string WARN0331 = "WARN0331";
        public const string WARN0332 = "WARN0332";
        public const string WARN0333 = "WARN0333";
        public const string WARN0334 = "WARN0334";
        public const string WARN0335 = "WARN0335";
        public const string WARN0336 = "WARN0336";
        public const string WARN0337 = "WARN0337";
        //
        public const string ERR0600 = "An outcome that already has a Delete Date cannot be deleted.";
        public const string ERR0601 = "An outcome must have a Delete Date to be reinstated.";
        public const string ERR0999 = "ERR0999";

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
                errorMessageDict.Add(ERR0100,"An AgencyId is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0101,"A ProgramId is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0102,"An AgencyCaseNum is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0103,"An IntakeDt is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0104,"A CaseSourceCd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0105,"A BorrowerFname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0106,"A BorrowerLname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0107,"A PrimaryContactNo is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0108,"A ContactAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0109,"A ContactCity is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0110,"A ContactStateCd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0111,"A ContactZip is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0112,"A PropAddr1 is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0113,"A PropCity is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0114,"A PropStateCd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0115,"A PropZip is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0116,"The FundingConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0117,"The ServicerConsentInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0118,"A CounselorIdRef of the assigned counselor is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0119,"A CounselorLname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0120,"A CounselorFname is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0121,"A CounselorEmail is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0122,"A CounselorPhone is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0123,"The OwnerOccupiedInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0124,"The PrimaryResidenceInd is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0125,"A ChgLstUserId is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0126, "Missing loans.  At least one loan is required to save a foreclosure case.");
                errorMessageDict.Add(ERR0127, "One or more of your loans is missing a ServicerID.");
                errorMessageDict.Add(ERR0128, "One or more of your loans is missing a Loan Num.");
                errorMessageDict.Add(ERR0129, "An OutcomeTypeID is required to save a foreclosure case.");

                errorMessageDict.Add(ERR0200,"An invalid code was provided for IncomeEarnersCd.");
                errorMessageDict.Add(ERR0201,"An invalid code was provided for CaseSourceCd.");
                errorMessageDict.Add(ERR0202,"An invalid code was provided for RaceCd.");
                errorMessageDict.Add(ERR0203,"An invalid code was provided for HouseholdCd.");
                errorMessageDict.Add(ERR0204,"An invalid code was provided for DfltReason1stCd.");
                errorMessageDict.Add(ERR0205,"An invalid code was provided for DfltReason2ndCd.");
                errorMessageDict.Add(ERR0206,"An invalid code was provided for HudTerminationReasonCd.");
                errorMessageDict.Add(ERR0207,"An invalid code was provided for HudOutcomeCd.");
                errorMessageDict.Add(ERR0208,"An invalid code was provided for CounselingDurationCd.");
                errorMessageDict.Add(ERR0209,"An invalid code was provided for GenderCd.");
                errorMessageDict.Add(ERR0210,"An invalid code was provided for ContactStateCd.");
                errorMessageDict.Add(ERR0211,"An invalid code was provided for PropStateCd.");
                errorMessageDict.Add(ERR0212,"An invalid code was provided for BorrowerEducLevelCompletedCd.");
                errorMessageDict.Add(ERR0213,"An invalid code was provided for BorrowerMaritalStatusCd.");
                errorMessageDict.Add(ERR0214,"An invalid code was provided for BorrowerPreferredLangCd.");
                errorMessageDict.Add(ERR0215,"An invalid code was provided for SummarySentOtherCd.");
                errorMessageDict.Add(ERR0216,"An invalid code was provided for PropertyCd.");
                errorMessageDict.Add(ERR0217,"An invalid code was provided for MilitaryServiceCd.");

                errorMessageDict.Add(ERR0250, "Neither a HPF FcId nor an AgencyCaseNum was provided. Please provide an AgencyCaseNum for new cases and both and AgencyCaseNum and HPF FcId are required for updates to existing cases.");
                errorMessageDict.Add(ERR0251, "An invalid HPF FcID was provided.  Please correct the FcID and resend the foreclosure case.");
                errorMessageDict.Add(ERR0252, "An invalid AgencyID was provided for the Corresponding FcId.  Your agency does not own this foreclosure case. ");
                errorMessageDict.Add(ERR0253, "Duplicate Case Found for Servicer: {0}, Account Number: {1}, Zip Code: {2}. "
                                              +"Borrower Name: {3} {4} The case is currently being worked on by: {5} {6} of {7}. "
                                              +"Counselor Phone: {8} {9} Counselor Email: {10} Last Outcome Date: {11} "
                                              +"Last Outcome: {12} ");                
                errorMessageDict.Add(ERR0254, "An update was submitted without a FcID. All updates require a HPF FcId. Please correct and resubmit the case.");
                errorMessageDict.Add(ERR0255, "A previously completed case cannot become uncompleted. An update was submitted without all fields required to compleg.");
                errorMessageDict.Add(ERR0256, "Only one loan can be designated as '1st'.");

                errorMessageDict.Add(ERR0350, "A CcCallKey is required to save a call.");
                errorMessageDict.Add(ERR0351, "A StartDt is required to save a call.");
                errorMessageDict.Add(ERR0352, "An EndDt is required to save a call.");
                errorMessageDict.Add(ERR0353, "A FinalDispoCd is required to save a call.");

                errorMessageDict.Add(WARN0300,"An IncomeEarnersCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0301,"A RaceCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0302,"A HouseholdCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0303,"A DfltReason1stCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0304,"A DfltReason2ndCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0305,"A HudOutcomeCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0306,"A CounselingDurationCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0307,"A GenderCd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0308,"A BorrowerDob is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0309,"The BankruptcyInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0310,"The HispanicInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0311,"The FcNoticeReceivedInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0312,"An OccupantNum is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0313,"LoanDefltReasonNotes are required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0314,"ActionItemsNotes is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0315,"The DiscussedSolutionWithSrvcrInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0316,"The WorkedWithAnotherAgencyInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0317,"The ContactedSrvcrRecentlyInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0318,"The HasWorkoutPlanInd is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0319,"A HouseholdGrossAnnualIncomeAmt is required to complete a foreclosure case.");
                errorMessageDict.Add(WARN0320,"A Loan1st2ndCd is required on all loans to complete a foreclosure case.");

                errorMessageDict.Add(WARN0321, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN0322, "A TermLengthCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN0323, "A LoanDelinqStatusCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN0324, "A InterestRate is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN0325, "A MortgageTypeCd is required on all loans to complete a foreclosure case.");
                errorMessageDict.Add(WARN0326, "A billable OutcomeTypeID is required to save a foreclosure case.");
                errorMessageDict.Add(WARN0327, "A Budget is required to save a foreclosure case.");
                errorMessageDict.Add(WARN0328, "A BudgetSubcategoryID is required on all budget Items to complete a foreclosure case.");
                errorMessageDict.Add(WARN0329, "A BudgetItemAmt is required on all budget Items to complete a foreclosure case.");                
                
                //
                errorMessageDict.Add(ERR0999, "You don't have permission to access this page.");

            }
        }
    }
}
