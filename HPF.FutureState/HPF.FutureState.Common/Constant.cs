namespace HPF.FutureState.Common
{
    public class Constant
    {
        public const int PAYMENT_COMMENT_MAX_LENGTH = 300;
        public const int PAYMENT_NUMBER_MAX_LENGTH = 30;
        public const string LOAN_1ST = "1ST";
        public const string INCOME = "INC";
        public const string EXPENSES = "EXP";        
        public const string RULESET_MIN_REQUIRE_FIELD = "RequirePartialValidate";
        public const string RULESET_COMPLETE = "Complete";
        public const string RULESET_LENGTH = "Length";
        public const string RULESET_FOLLOW_UP = "FollowUp";
        public const string PAYABLE_IND = "Y";
        public const string BANKRUPTCY_IND_YES = "Y";
        public const string HAS_WORKOUT_PLAN_IND_YES = "Y";
        public const string FORSALE_IND_YES = "Y";
        public const string DUPLICATE_YES = "Y";
        public const string DUPLICATE_NO = "N";
        public const string INDICATOR_YES = "Y";
        public const string INDICATOR_NO = "N";
        public const string INDICATOR_YES_FULL = "Yes";
        public const string INDICATOR_NO_FULL = "No";
        public const string NEVER_PAY_REASON_CODE_DUPE = "DUPE";
        public const string NEVER_BILL_REASON_CODE_DUPE = "DUPE";
        public const string FOLLOW_UP_CD_CREDIT_REPORT = "CREDITREPORT";
        public const string SECURE_DELIVERY_METHOD_NOSEND = "NOSEND";
        public const string SECURE_DELIVERY_METHOD_PORTAL = "PORTAL";
        public const string SECURE_DELIVERY_METHOD_SECEMAIL = "SECEMAIL";        
        public const string MORTGATE_TYPE_CODE_ARM = "ARM";
        public const string MORTGATE_TYPE_CODE_POA = "POA";
        public const string MORTGATE_TYPE_CODE_INTONLY = "INTONLY";
        public const string MORTGATE_TYPE_CODE_HYBARM = "HYBARM";
        //
        public const string CALL_CENTER_OTHER = "OTHER";
        public const string SERVICER_OTHER = "OTHER (LENDER NAME IN NOTES)";
        public const string SUB_CATEGORY_NAME_MORTGAGE = "MORTGAGE";
        public const string OUTCOME_TYPE_NAME_EXTERNAL_REFERAL = "EXTERNAL REFERAL";        
        public const string MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE = "FundingSourceInvoice.aspx";
        public const string MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE = "AgencyPayable.aspx";
        public const string MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_SEARCH = "SearchForeclosureCase.aspx";
        public const string MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL = "ForeclosureCaseInfo.aspx";
        public const string MENU_ITEM_TARGET_APP_AGENCY_PAYABLE_INFO = "AgencyPayableInfo.aspx";
        public const string MENU_ITEM_TARGET_APP_INVOICE_PAYMENT = "InvoicePayment.aspx";
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
        public const string HPF_CACHE_HPFUSER = "HPFCACHEHPFUSER";

        //
        public const string REF_CODE_SET_AUDIT_TYPE_CODE = "audit type code";
        public const string REF_CODE_SET_AUDIT_FAILURE_REASON_CODE = "audit failure reason code";
        public const string REF_CODE_SET_FOLLOW_UP_SOURCE_CODE = "follow up source code";
        public const string REF_CODE_SET_LOAN_DELINQUENCY_CODE = "loan delinquency status code";
        public const string REF_CODE_SET_CREDIR_BERREAU_CODE = "credit bureau code";
        public const string REF_CODE_SET_STATE_CODE = "State Code";
        public const string REF_CODE_SET_GENDER_CODE = "Gender code";
        public const string REF_CODE_SET_RACE_CODE = "Race code";
        public const string REF_CODE_SET_HOUSEHOLD_CODE = "Household code";
        public const string REF_CODE_SET_AGENCY_PAYABLE_STATUS_CODE = "agency payable status code";
        public const string REF_CODE_SET_INVOICE_STATUS_CODE = "invoice status code";
        public const string REF_CODE_SET_TAKE_BACK_REASON_CODE = "takeback reason code";
        public const string REF_CODE_SET_PAYMENT_REJECT_REASON_CODE = "payment reject reason code";
        public const string REF_CODE_SET_NEVER_BILL_REASON_CODE = "never bill reason code";
        public const string REF_CODE_SET_NEVER_PAY_REASON_CODE = "never pay reason code";
        public const string REF_CODE_SET_PAYMENT_CODE = "payment code";

        public const string REF_CODE_SET_BILLING_EXPORT_FIS_CODE = "FIS";
        public const string REF_CODE_SET_BILLING_EXPORT_ICLEAR_CODE = "ICLEAR";
        public const string REF_CODE_SET_BILLING_EXPORT_NFMC_CODE = "NFMC";
        public const string REF_CODE_SET_BILLING_EXPORT_HPFSTD_CODE = "HPFSTD";
        public const string REF_CODE_SET_BILLING_EXPORT_HSBC_CODE = "HSBC";
        public const string REF_CODE_SET_BILLING_EXPORT_HUD_CODE = "HUD";


        //
        public const int HPF_QUEUE_READING_TIMEOUT = 10;//Seconds
        public const string HPF_QUEUE_PATH = @".\Private$\HPFSummaryQueue";
        //
        public const string SS_CASE_AUDIT_OBJECT = "ss_case_audit_object";
        public const string SS_CASE_FOLLOW_UP_OBJECT = "ss_case_follow_up_object";
        public const string DB_LOG_CATEGORY = "Importance";

        public const string HPF_SECURE_EMAIL = "$s$";  

        //appforeclosurecase search
        public const string RULESET_CRITERIAVALID = "CriteriaValidation";
        public const string RULESET_APPSEARCH = "AppSearchRequireCriteria";

        public const string EXCEL_FILE_TAB_NAME = "rpt_invoiceexportfile_hpfstanda";
    }
}
