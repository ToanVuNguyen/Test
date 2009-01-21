-- =============================================
-- Create date: 21 Jan 2009
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
GRANT EXECUTE ON hpf_geo_code_ref_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_item_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_item_insert TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_item_update TO hpf_ws_user;
GRANT EXECUTE ON hpf_outcome_type_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_program_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_ref_code_item_get TO hpf_ws_user;
GRANT EXECUTE ON hpf_servicer_get TO hpf_ws_user;
