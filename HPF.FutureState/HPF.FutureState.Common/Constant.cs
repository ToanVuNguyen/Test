namespace HPF.FutureState.Common
{
    public class Constant
    {
        public const string LOAN_1ST = "1ST";
        public const string INCOME = "1";
        public const string EXPENSES = "2";        
        public const string RULESET_MIN_REQUIRE_FIELD = "RequirePartialValidate";
        public const string RULESET_COMPLETE = "Complete";
        public const string RULESET_LENGTH = "Length";
        public const string PAYABLE_IND = "Y";
        public const string BANKRUPTCY_IND_YES = "Y";
        public const string FORSALE_IND_YES = "Y";
        public const string DUPLICATE_YES = "Y";
        public const string DUPLICATE_NO = "N";
        public const string INDICATOR_YES = "Y";
        public const string INDICATOR_NO = "N";
        public const string INDICATOR_YES_FULL = "YES";
        public const string INDICATOR_NO_FULL = "NO";
        public const string NEVER_PAY_REASON_CODE_DUPE = "DUPE";
        public const string NEVER_BILL_REASON_CODE_DUPE = "DUPE";
        //
        public const string CALL_CENTER_OTHER = "OTHER";
        public const string SERVICER_OTHER = "OTHER (LENDER NAME IN NOTES)";
        public const string SUB_CATEGORY_NAME_MORTGAGE = "MORTGAGE";
        public const string OUTCOME_TYPE_NAME_EXTERNAL_REFERAL = "EXTERNAL REFERAL";        
        public const string MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE = "AppFundingSourceInvoicesPage.aspx";
        public const string MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE = "AgencyAccountsPayable.aspx";
        public const string MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_SEARCH = "AppForeClosureCaseSearchPage.aspx";
        public const string MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL = "AppForeclosureCaseDetailPage.aspx";
        //
        public const string HPF_CACHE_REF_CODE_ITEM = "refCodeItem";
        public const string HPF_CACHE_FUNDING_SOURCE = "FUNDING_SOURCE";
        public const string HPF_CACHE_PROGRAM = "PROGRAM";
        public const string HPF_CACHE_SERVICER = "SERVICER";
        public const string HPF_CACHE_CASE_LOAN = "CASELOAN";
        public const string HPF_CACHE_OUTCOME_ITEM = "OUTCOMEITEM";
        public const string HPF_CACHE_OUTCOME_TYPE = "OUTCOMETYPE";
        public const string HPF_CACHE_BUDGET_CATEGORY_CODE = "BUDGETCATEGORYCODE";
        public const string HPF_CACHE_BUDGET_SUBCATEGORY = "BUDGETSUBCATEGORY";
        public const string HPF_CACHE_AGENCY = "HPFCACHEAGENCY";
        public const string HPF_CACHE_PAYMENT_TYPE = "HPFCACHPAYMENTTYPE";
        //
        public const string REF_CODE_SET_AUDIT_TYPE_CODE = "audit type code";
        public const string REF_CODE_SET_AUDIT_FAILURE_REASON_CODE = "audit failure reason code";
        //
        public const int HPF_QUEUE_READING_TIMEOUT = 10;//Seconds
        public const string HPF_QUEUE_PATH = @".\Private$\HPFSummaryQueue";
    }
}
