-- =============================================
-- Create date: 18 Feb 2009
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