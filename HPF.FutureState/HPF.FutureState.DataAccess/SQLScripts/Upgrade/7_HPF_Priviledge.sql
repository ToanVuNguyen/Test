-- =============================================
-- Create date: 21 Apr 2009
-- Project : HPF 
-- Build 
-- Description: GRANT permissions to the deployed users can SELECT, INSERT, UPDATE, DELETE, EXECUTE
-- =============================================
USE HPF
GO
/* GRANT PERMISSION for hpf_ws_user */
GRANT EXECUTE ON hpf_agency_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_agency_detail_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_asset_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_asset_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_item_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_item_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_set_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_set_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_set_update TO hpf_ws_user;
GRANT EXECUTE ON hpf_budget_subcategory_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_call_center_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_call_check_foreign_key TO hpf_ws_user;
GRANT EXECUTE ON hpf_call_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_call_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_case_loan_delete TO hpf_ws_user;
GRANT EXECUTE ON hpf_case_loan_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_case_loan_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_case_loan_update TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_detail_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_detail_get_call TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_get_duplicate TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_get_from_agencyID_and_caseNumber TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_search_ws TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_update TO hpf_ws_user;
GRANT EXECUTE ON hpf_foreclosure_case_update_ws TO hpf_ws_user;

GRANT EXECUTE ON hpf_geo_code_ref_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_item_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_item_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_item_update TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_type_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_program_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_ref_code_item_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_servicer_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_ws_user_get_from_username_password TO hpf_ws_user;
GRANT EXECUTE ON hpf_view_budget_category_code TO hpf_ws_user;
GRANT EXECUTE ON hpf_servicer_get_from_FcId TO hpf_ws_user;
GRANT EXECUTE ON hpf_activity_log_insert  TO hpf_ws_user;

GRANT SELECT ON activity_log TO hpf_ws_user;
GRANT SELECT ON Agency TO hpf_ws_user;
GRANT SELECT ON agency_payable TO hpf_ws_user;
GRANT SELECT ON agency_payable_case TO hpf_ws_user;
GRANT SELECT ON agency_rate TO hpf_ws_user;
GRANT SELECT ON Area_Median_Income TO hpf_ws_user;
GRANT SELECT ON budget_asset TO hpf_ws_user;
GRANT SELECT ON budget_category TO hpf_ws_user;
GRANT SELECT ON budget_item TO hpf_ws_user;
GRANT SELECT ON budget_set TO hpf_ws_user;
GRANT SELECT ON budget_subcategory TO hpf_ws_user;
GRANT SELECT ON call TO hpf_ws_user;
GRANT SELECT ON call_center TO hpf_ws_user;
GRANT SELECT ON case_audit TO hpf_ws_user;
GRANT SELECT ON case_loan TO hpf_ws_user;
GRANT SELECT ON case_post_counseling_status TO hpf_ws_user;
GRANT SELECT ON Category TO hpf_ws_user;
GRANT SELECT ON CategoryLog TO hpf_ws_user;
GRANT SELECT ON change_audit TO hpf_ws_user;
GRANT SELECT ON congress_dist_ref TO hpf_ws_user;
GRANT SELECT ON foreclosure_case TO hpf_ws_user;
GRANT SELECT ON funding_source TO hpf_ws_user;
GRANT SELECT ON funding_source_group TO hpf_ws_user;
GRANT SELECT ON funding_source_rate TO hpf_ws_user;
GRANT SELECT ON geocode_ref TO hpf_ws_user;
GRANT SELECT ON hpf_user TO hpf_ws_user;
GRANT SELECT ON Invoice TO hpf_ws_user;
GRANT SELECT ON invoice_case TO hpf_ws_user;
GRANT SELECT ON invoice_payment TO hpf_ws_user;
GRANT SELECT ON Log TO hpf_ws_user;
GRANT SELECT ON menu_group TO hpf_ws_user;
GRANT SELECT ON menu_item TO hpf_ws_user;
GRANT SELECT ON menu_security TO hpf_ws_user;
GRANT SELECT ON outcome_item TO hpf_ws_user;
GRANT SELECT ON outcome_type TO hpf_ws_user;
GRANT SELECT ON program TO hpf_ws_user;
GRANT SELECT ON ref_code_item TO hpf_ws_user;
GRANT SELECT ON ref_code_set TO hpf_ws_user;
GRANT SELECT ON servicer TO hpf_ws_user;
GRANT SELECT ON system_activity_log TO hpf_ws_user;
GRANT SELECT ON ws_user TO hpf_ws_user;

/************************************************************/
/* GRANT PERMISSION for hpf_app_user */
GRANT EXECUTE ON hpf_activity_log_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_activity_log_get TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_get TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_detail_get TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_case_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_update TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_search TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_search_draft TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_detail_get TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_get TO hpf_app_user;
GRANT EXECUTE ON hpf_agency_payable_case_update TO hpf_app_user;
GRANT EXECUTE ON hpf_budget_subcategory_get TO hpf_app_user;
GRANT EXECUTE ON hpf_budget_set_get TO hpf_app_user;
GRANT EXECUTE ON hpf_budget_detail_get TO hpf_app_user;
GRANT EXECUTE ON hpf_budget_asset_get TO hpf_app_user;
GRANT EXECUTE ON hpf_budget_item_get TO hpf_app_user;

GRANT EXECUTE ON hpf_case_audit_update TO hpf_app_user;
GRANT EXECUTE ON hpf_case_audit_get TO hpf_app_user;
GRANT EXECUTE ON hpf_case_audit_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_case_loan_get TO hpf_app_user;
GRANT EXECUTE ON hpf_case_post_counseling_status_get TO hpf_app_user;
GRANT EXECUTE ON hpf_case_post_counseling_status_insert  TO hpf_app_user;
GRANT EXECUTE ON hpf_case_post_counseling_status_update TO hpf_app_user;

GRANT EXECUTE ON hpf_foreclosure_case_detail_get TO hpf_app_user;
GRANT EXECUTE ON hpf_foreclosure_case_update_app TO hpf_app_user;
GRANT EXECUTE ON hpf_foreclosure_case_search_app_dynamic TO hpf_app_user;
GRANT EXECUTE ON hpf_foreclosure_case_detail_get TO hpf_app_user;
GRANT EXECUTE ON hpf_funding_source_get TO hpf_app_user;
GRANT EXECUTE ON hpf_hpf_user_get TO hpf_app_user;
GRANT EXECUTE ON hpf_hpf_user_update TO hpf_app_user;

GRANT EXECUTE ON hpf_invoice_search TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_update TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_case_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_case_search_draft TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_detail_get TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_get TO hpf_app_user;
GRANT EXECUTE ON hpf_Invoice_case_validate TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_case_update_for_invoice TO hpf_app_user;
GRANT EXECUTE ON hpf_Invoice_case_update_for_payment TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_payment_update TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_payment_insert TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_payment_search TO hpf_app_user;
GRANT EXECUTE ON hpf_invoice_payment_get TO hpf_app_user;

GRANT EXECUTE ON hpf_menu_group_get TO hpf_app_user;
GRANT EXECUTE ON hpf_menu_bar_permission TO hpf_app_user;
GRANT EXECUTE ON hpf_menu_security_get TO hpf_app_user;

GRANT EXECUTE ON hpf_outcome_item_get TO hpf_app_user;
GRANT EXECUTE ON hpf_outcome_type_get TO hpf_app_user;

GRANT EXECUTE ON hpf_servicer_get TO hpf_app_user;
GRANT EXECUTE ON hpf_program_get TO hpf_app_user;
GRANT EXECUTE ON hpf_hpf_user_get_from_login TO hpf_app_user;
GRANT EXECUTE ON hpf_geo_code_ref_get TO hpf_app_user;
GRANT EXECUTE ON hpf_ref_code_item_get TO hpf_app_user;
GRANT EXECUTE ON hpf_ws_user_get_from_username_password TO hpf_app_user;

GRANT SELECT ON activity_log TO hpf_app_user;
GRANT SELECT ON Agency TO hpf_app_user;
GRANT SELECT ON agency_payable TO hpf_app_user;
GRANT SELECT, UPDATE ON agency_payable_case TO hpf_app_user;
GRANT SELECT ON agency_rate TO hpf_app_user;
GRANT SELECT ON Area_Median_Income TO hpf_app_user;
GRANT SELECT ON budget_asset TO hpf_app_user;
GRANT SELECT ON budget_category TO hpf_app_user;
GRANT SELECT ON budget_item TO hpf_app_user;
GRANT SELECT ON budget_set TO hpf_app_user;
GRANT SELECT ON budget_subcategory TO hpf_app_user;
GRANT SELECT ON call TO hpf_app_user;
GRANT SELECT ON call_center TO hpf_app_user;
GRANT SELECT ON case_audit TO hpf_app_user;
GRANT SELECT ON case_loan TO hpf_app_user;
GRANT SELECT ON case_post_counseling_status TO hpf_app_user;
GRANT SELECT ON Category TO hpf_app_user;
GRANT SELECT ON CategoryLog TO hpf_app_user;
GRANT SELECT ON change_audit TO hpf_app_user;
GRANT SELECT ON congress_dist_ref TO hpf_app_user;
GRANT SELECT ON foreclosure_case TO hpf_app_user;
GRANT SELECT ON funding_source TO hpf_app_user;
GRANT SELECT ON funding_source_group TO hpf_app_user;
GRANT SELECT ON funding_source_rate TO hpf_app_user;
GRANT SELECT ON geocode_ref TO hpf_app_user;
GRANT SELECT ON hpf_user TO hpf_app_user;
GRANT SELECT ON Invoice TO hpf_app_user;
GRANT SELECT, UPDATE ON invoice_case TO hpf_app_user;
GRANT SELECT ON invoice_payment TO hpf_app_user;
GRANT SELECT ON Log TO hpf_app_user;
GRANT SELECT ON menu_group TO hpf_app_user;
GRANT SELECT ON menu_item TO hpf_app_user;
GRANT SELECT ON menu_security TO hpf_app_user;
GRANT SELECT ON outcome_item TO hpf_app_user;
GRANT SELECT ON outcome_type TO hpf_app_user;
GRANT SELECT ON program TO hpf_app_user;
GRANT SELECT ON ref_code_item TO hpf_app_user;
GRANT SELECT ON ref_code_set TO hpf_app_user;
GRANT SELECT ON servicer TO hpf_app_user;
GRANT SELECT ON system_activity_log TO hpf_app_user;
GRANT SELECT ON ws_user TO hpf_app_user;

/************************************************************/
/* GRANT PERMISSION for hpf_report_user */

GRANT EXECUTE ON hpf_rpt_AgencyPaymentCheck TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CaseFundingSourceSummary TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CaseSource TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CompletedCasesByState TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CompletedCounselingByServicer TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CompletedCounselingDetail TO hpf_report_user;
GRANT EXECUTE ON Hpf_rpt_CompletedCounselingSummary TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummary_get_FC_Budget TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummary_get_FC_Budget_asset TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummary_get_FC_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummary_get_FC_Outcome TO hpf_report_user;

GRANT EXECUTE ON hpf_rpt_CounselingSummaryForAgency_get_FC_Budget TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummaryForAgency_get_FC_Budget_asset TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummaryForAgency_get_FC_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_CounselingSummaryForAgency_get_FC_Outcome TO hpf_report_user;

GRANT EXECUTE ON hpf_rpt_count_day TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_DailyAverageCompletionReport TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_DailyActivityReport  TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_DailyCompletionReport TO hpf_report_user;
GRANT EXECUTE ON Hpf_rpt_ExternalReferrals TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_get_dateRange TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_IncompletedCounselingCases TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_FIS_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_FIS_header TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_header TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_HPFStandard_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_HSBC_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_HUD_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceExportFile_NFMC TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceSummary_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_InvoiceSummary_header TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_PayableSummary_detail TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_PayableSummary_header TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_PotentialDuplicates TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_replace_non_numeric_char TO hpf_report_user;
GRANT EXECUTE ON Hpf_rpt_UnbilledCaseReport TO hpf_report_user;
GRANT EXECUTE ON hpf_agency_get TO hpf_report_user;
GRANT EXECUTE ON hpf_funding_source_get TO hpf_report_user;
GRANT EXECUTE ON hpf_program_get TO hpf_report_user;
GRANT EXECUTE ON hpf_servicer_get TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_PayableExportFile_header TO hpf_report_user;
GRANT EXECUTE ON hpf_rpt_PayableExportFile_detail TO hpf_report_user;


GRANT SELECT ON program TO hpf_report_user;
/************************************************************/