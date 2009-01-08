-- =============================================
-- Create date: 07 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Create tables in the new HPF database
--		Apply database changes on:  07 Jan 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

CREATE TABLE funding_source (
  funding_source_id INTEGER  NOT NULL   IDENTITY ,
  funding_source_name VARCHAR(50)    ,
  funding_source_comment VARCHAR(300)    ,
  billing_frequency TINYINT    ,
  billing_email VARCHAR(50)    ,
  billing_delivery_method_cd VARCHAR(15)    ,
  export_format_cd VARCHAR(15)    ,
  contact_fname VARCHAR(30)    ,
  contact_lname VARCHAR(30)    ,
  billing_addr_1 VARCHAR(50)    ,
  billing_addr_2 VARCHAR(50)    ,
  zip VARCHAR(20)    ,
  zip_plus_4 VARCHAR(20)    ,
  city VARCHAR(30)    ,
  state_cd VARCHAR(15)    ,
  eff_dt DATETIME    ,
  exp_dt DATETIME    ,
  active_ind VARCHAR(1)    ,
  gen_mult_files_ind VARCHAR(1)    ,
  accounting_link_TBD VARCHAR(30)    ,
  funding_source_abbrev VARCHAR(10)  NOT NULL  ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(funding_source_id));
GO



CREATE TABLE geocode_ref (
  geocode_ref_id INTEGER  NOT NULL   IDENTITY ,
  zip_code VARCHAR(5)    ,
  zip_type VARCHAR(1)  NOT NULL  ,
  city_name VARCHAR(100)    ,
  city_type VARCHAR(1)    ,
  county_name VARCHAR(100)    ,
  county_FIPS VARCHAR(5)    ,
  state_name VARCHAR(100)    ,
  state_abbr VARCHAR(2)    ,
  state_FIPS VARCHAR(2)    ,
  MSA_code VARCHAR(4)    ,
  area_code VARCHAR(20)    ,
  time_zone VARCHAR(20)    ,
  utc NUMERIC(3,1)    ,
  dst VARCHAR(1)    ,
  latitude VARCHAR(20)    ,
  longitude VARCHAR(20)      ,
PRIMARY KEY(geocode_ref_id));
GO



CREATE TABLE system_activity_log (
  system_activity_log_id INTEGER  NOT NULL   IDENTITY ,
  sys_activity_cd DATETIME    ,
  activity_dt DATETIME    ,
  activity_log_msg VARCHAR(1000)    ,
  activity_error_msg VARCHAR(1000)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(system_activity_log_id));
GO



CREATE TABLE ccrc_user (
  ccrc_user_id INTEGER  NOT NULL   IDENTITY ,
  user_login_id VARCHAR(30)    ,
  active_ind VARCHAR(1)    ,
  user_role_str_TBD VARCHAR(30)    ,
  fname VARCHAR(30)    ,
  lname VARCHAR(30)    ,
  email VARCHAR(50)    ,
  phone VARCHAR(20)    ,
  last_login_dt DATETIME    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(ccrc_user_id));
GO



CREATE TABLE change_audit (
  change_audit_id INTEGER  NOT NULL   IDENTITY ,
  audit_dt DATETIME  NOT NULL  ,
  audit_table VARCHAR(50)  NOT NULL  ,
  audit_record_key INTEGER  NOT NULL  ,
  audit_col_name VARCHAR(50)  NOT NULL  ,
  audit_old_value VARCHAR(8000)  NOT NULL  ,
  audit_new_value VARCHAR(8000)  NOT NULL  ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(change_audit_id));
GO



CREATE TABLE congress_dist_ref (
  congress_dist_ref_id INTEGER  NOT NULL   IDENTITY ,
  state_FIPS VARCHAR(2)    ,
  zip VARCHAR(5)    ,
  congress_dist VARCHAR(2)      ,
PRIMARY KEY(congress_dist_ref_id));
GO



CREATE TABLE program (
  program_id INTEGER  NOT NULL   IDENTITY ,
  program_name VARCHAR(50)    ,
  program_comment VARCHAR(300)    ,
  start_dt DATETIME    ,
  end_dt DATETIME    ,
  allow_noncontracted_svcr_ind CHAR(1)    ,
  service_cd VARCHAR(15)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(program_id));
GO



CREATE TABLE ref_code_set (
  ref_code_set_name VARCHAR(30)  NOT NULL  ,
  code_set_comment VARCHAR(300)      ,
PRIMARY KEY(ref_code_set_name));
GO



CREATE TABLE servicer (
  servicer_id INTEGER  NOT NULL   IDENTITY ,
  servicer_name VARCHAR(50)    ,
  contact_fname VARCHAR(30)    ,
  contact_lname VARCHAR(30)    ,
  contact_email VARCHAR(50)    ,
  phone VARCHAR(20)    ,
  fax VARCHAR(30)    ,
  active_ind VARCHAR(1)    ,
  funding_agreement_ind VARCHAR(1)    ,
  secure_delivery_method_cd VARCHAR(15)    ,
  couseling_sum_format_cd VARCHAR(15)    ,
  hud_servicer_num VARCHAR(20)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(servicer_id));
GO



CREATE TABLE menu_group (
  menu_group_id INTEGER  NOT NULL   IDENTITY ,
  group_name VARCHAR(50)  NOT NULL  ,
  group_sort_order INTEGER  NOT NULL  ,
  group_target VARCHAR(200)      ,
PRIMARY KEY(menu_group_id));
GO

CREATE TABLE outcome_type (
  outcome_type_id INTEGER  NOT NULL   IDENTITY ,
  outcome_type_name VARCHAR(50)    ,
  outcome_type_comment VARCHAR(300)    ,
  payable_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(outcome_type_id));
GO



CREATE TABLE call_center (
  call_center_id INTEGER  NOT NULL   IDENTITY ,
  call_center_name VARCHAR(50)    ,
  contact_fname VARCHAR(30)    ,
  contact_lname VARCHAR(30)    ,
  phone VARCHAR(20)    ,
  fax VARCHAR(20)    ,
  email VARCHAR(50)    ,
  call_center_cd VARCHAR(15)    ,
  active_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(call_center_id));
GO



CREATE TABLE Area_Median_Income (
  area_median_income_id INTEGER  NOT NULL   IDENTITY ,
  state_alpha VARCHAR(2)    ,
  fips VARCHAR(15)    ,
  state INT    ,
  county INT    ,
  county_name VARCHAR(30)    ,
  CBSASub VARCHAR(30)    ,
  metro_area_name VARCHAR(50)    ,
  median1999 INT    ,
  median2008 INT    ,
  state_name VARCHAR(20)    ,
  L50_1 INTEGER    ,
  L50_2 INTEGER    ,
  L50_3 INTEGER    ,
  L50_4 INTEGER    ,
  L50_5 INTEGER    ,
  L50_6 INTEGER    ,
  L50_7 INTEGER    ,
  L50_8 INTEGER    ,
  L30_1 INTEGER    ,
  L30_2 INTEGER    ,
  L30_3 INTEGER    ,
  L30_4 INTEGER    ,
  L30_5 INTEGER    ,
  L30_6 INTEGER    ,
  L30_7 INTEGER    ,
  L30_8 INTEGER    ,
  L80_1 INTEGER    ,
  L80_2 INTEGER    ,
  L80_3 INTEGER    ,
  L80_4 INTEGER    ,
  L80_5 INTEGER    ,
  L80_6 INTEGER    ,
  L80_7 INTEGER    ,
  L80_8 INTEGER    ,
  msa INTEGER    ,
  county_town_name VARCHAR(30)    ,
  metro INT    ,
  create_dt DATETIME    NOT NULL,
  create_user_id VARCHAR(30)    NOT NULL,
  create_app_name VARCHAR(20)    NOT NULL,
  chg_lst_dt DATETIME    NOT NULL,
  chg_lst_user_id VARCHAR(30)    NOT NULL,
  chg_lst_app_name VARCHAR(20)      NOT NULL,
PRIMARY KEY(area_median_income_id));
GO



CREATE TABLE Agency (
  agency_id INTEGER  NOT NULL   IDENTITY ,
  agency_name VARCHAR(50)    ,
  contact_fname VARCHAR(30)    ,
  contact_lname VARCHAR(30)    ,
  phone VARCHAR(20)    ,
  fax VARCHAR(20)    ,
  email VARCHAR(50)    ,
  active_ind VARCHAR(1)    ,
  hud_agency_num VARCHAR(20)    ,
  hud_agency_sub_grantee_num VARCHAR(20)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(agency_id));
GO



CREATE TABLE budget_category (
  budget_category_id INTEGER  NOT NULL   IDENTITY ,
  budget_category_cd VARCHAR(15)    ,
  budget_category_name VARCHAR(50)    ,
  budget_category_comment VARCHAR(300)    ,
  sort_order SMALLINT    ,
  active_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(budget_category_id));
GO



CREATE TABLE invoice_payment (
  invoice_payment_id INTEGER  NOT NULL   IDENTITY ,
  funding_source_id INTEGER  NOT NULL  ,
  pmt_num VARCHAR(30)    ,
  pmt_dt DATETIME    ,
  pmt_cd VARCHAR(15)    ,
  pmt_amt NUMERIC(15,2)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(invoice_payment_id)  ,
  FOREIGN KEY(funding_source_id)
    REFERENCES funding_source(funding_source_id));
GO


CREATE INDEX payment_FKIndex1 ON invoice_payment (funding_source_id);
GO



CREATE TABLE Invoice (
  Invoice_id INTEGER  NOT NULL   IDENTITY ,
  funding_source_id INTEGER  NOT NULL  ,
  invoice_dt DATETIME    ,
  invoice_cd VARCHAR(15)    ,
  status_cd VARCHAR(15)    ,
  period_start_dt DATETIME    ,
  period_end_dt DATETIME    ,
  invoice_comment VARCHAR(300)    ,
  accounting_link_TBD VARCHAR(30)    ,
  invoice_bill_amt NUMERIC(15,2)    ,
  invoice_pmt_amt NUMERIC(15,2)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(Invoice_id)  ,
  FOREIGN KEY(funding_source_id)
    REFERENCES funding_source(funding_source_id));
GO


CREATE INDEX Invoice_FKIndex1 ON Invoice (funding_source_id);
GO



CREATE TABLE ref_code_item (
  ref_code_item_id INTEGER  NOT NULL   IDENTITY ,
  ref_code_set_name VARCHAR(30)  NOT NULL  ,
  code VARCHAR(15)    ,
  code_desc VARCHAR(80)    ,
  code_comment VARCHAR(300)    ,
  sort_order SMALLINT    ,
  active_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(ref_code_item_id)  ,
  FOREIGN KEY(ref_code_set_name)
    REFERENCES ref_code_set(ref_code_set_name));
GO


CREATE INDEX ref_code_item_FKIndex1 ON ref_code_item (ref_code_set_name);
GO



CREATE TABLE menu_item (
  menu_item_id INTEGER  NOT NULL   IDENTITY ,
  menu_group_id INTEGER  NOT NULL  ,
  item_name VARCHAR(50)  NOT NULL  ,
  item_sort_order INTEGER  NOT NULL  ,
  item_target VARCHAR(200)      ,
PRIMARY KEY(menu_item_id)  ,
  FOREIGN KEY(menu_group_id)
    REFERENCES menu_group(menu_group_id));
GO


CREATE INDEX menu_item_FKIndex1 ON menu_item (menu_group_id);
GO



CREATE TABLE agency_payable (
  agency_payable_id INTEGER  NOT NULL   IDENTITY ,
  agency_id INTEGER  NOT NULL  ,
  pmt_dt DATETIME    ,
  pmt_cd VARCHAR(15)    ,
  status_cd VARCHAR(15)    ,
  period_start_dt DATETIME    ,
  period_end_dt DATETIME    ,
  pmt_comment VARCHAR(300)    ,
  accounting_link_TBD VARCHAR(30)    ,
  agency_payable_pmt_amt NUMERIC(15,2)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(agency_payable_id)  ,
  FOREIGN KEY(agency_id)
    REFERENCES Agency(agency_id));
GO


CREATE INDEX agency_payment_FKIndex1 ON agency_payable (agency_id);
GO



CREATE TABLE call (
  call_id INTEGER  NOT NULL   IDENTITY ,
  call_center_id INTEGER    NOT NULL,
  cc_agent_id_key VARCHAR(55)    ,
  start_dt DATETIME    NOT NULL,
  end_dt DATETIME    NOT NULL,
  dnis VARCHAR(10)    ,
  call_center VARCHAR(4)    ,
  call_source_cd VARCHAR(15)    ,
  reason_for_call VARCHAR(75)    ,
  loan_acct_num VARCHAR(30)    ,
  fname VARCHAR(30)    ,
  lname VARCHAR(30)    ,
  servicer_id INTEGER    ,
  other_servicer_name VARCHAR(50)    ,
  prop_zip_full9 VARCHAR(9)    ,
  prev_agency_id INTEGER    ,
  selected_agency_id VARCHAR(20)    ,
  screen_rout VARCHAR(2000)    ,
  final_dispo_cd VARCHAR(15)    ,
  trans_num VARCHAR(12)    ,
  cc_call_key VARCHAR(18)    NOT NULL,
  loan_delinq_status_cd VARCHAR(15)    ,
  selected_counselor VARCHAR(40)    ,
  homeowner_ind VARCHAR(1)    ,
  power_of_attorney_ind VARCHAR(1)    ,
  authorized_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(call_id)  ,
  FOREIGN KEY(call_center_id)
    REFERENCES call_center(call_center_id));
GO


CREATE INDEX call_FKIndex1 ON call (call_center_id);
GO



CREATE TABLE budget_subcategory (
  budget_subcategory_id INTEGER  NOT NULL   IDENTITY ,
  budget_category_id INTEGER  NOT NULL  ,
  budget_subcategory_name VARCHAR(50)    ,
  budget_subcategory_comment VARCHAR(300)    ,
  sort_order SMALLINT    ,
  active_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(budget_subcategory_id)  ,
  FOREIGN KEY(budget_category_id)
    REFERENCES budget_category(budget_category_id));
GO


CREATE INDEX budget_subcategory_FKIndex1 ON budget_subcategory (budget_category_id);
GO



CREATE TABLE funding_source_group (
  funding_source_id INTEGER  NOT NULL  ,
  servicer_id INTEGER  NOT NULL  ,
  eff_dt DATETIME    ,
  exp_dt DATETIME    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(funding_source_id, servicer_id)    ,
  FOREIGN KEY(funding_source_id)
    REFERENCES funding_source(funding_source_id),
  FOREIGN KEY(servicer_id)
    REFERENCES servicer(servicer_id));
GO


CREATE INDEX funding_source_has_servicer_FKIndex1 ON funding_source_group (funding_source_id);
GO
CREATE INDEX funding_source_has_servicer_FKIndex2 ON funding_source_group (servicer_id);
GO



CREATE TABLE funding_source_rate (
  funding_source_rate_id INTEGER  NOT NULL   IDENTITY ,
  program_id INTEGER  NOT NULL  ,
  funding_source_id INTEGER  NOT NULL  ,
  bill_rate NUMERIC(15,2)    ,
  eff_dt DATETIME    ,
  exp_dt DATETIME    ,
  rate_comment VARCHAR(100)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(funding_source_rate_id)    ,
  FOREIGN KEY(funding_source_id)
    REFERENCES funding_source(funding_source_id),
  FOREIGN KEY(program_id)
    REFERENCES program(program_id));
GO


CREATE INDEX funding_source_rate_FKIndex1 ON funding_source_rate (funding_source_id);
GO
CREATE INDEX funding_source_rate_FKIndex2 ON funding_source_rate (program_id);
GO



CREATE TABLE agency_rate (
  agency_rate_id INTEGER  NOT NULL   IDENTITY ,
  program_id INTEGER  NOT NULL  ,
  agency_id INTEGER  NOT NULL  ,
  pmt_rate NUMERIC(15, 2)    ,
  eff_dt DATETIME    ,
  exp_dt DATETIME    ,
  rate_comment VARCHAR(300)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(agency_rate_id)    ,
  FOREIGN KEY(agency_id)
    REFERENCES Agency(agency_id),
  FOREIGN KEY(program_id)
    REFERENCES program(program_id));
GO


CREATE INDEX agent_rate_FKIndex1 ON agency_rate (agency_id);
GO
CREATE INDEX agency_rate_FKIndex2 ON agency_rate (program_id);
GO



CREATE TABLE menu_security (
  menu_security_id INTEGER  NOT NULL   IDENTITY ,
  ccrc_user_id INTEGER  NOT NULL  ,
  menu_item_id INTEGER  NOT NULL  ,
  permission_value VARCHAR(1)  NOT NULL    ,
PRIMARY KEY(menu_security_id)    ,
  FOREIGN KEY(menu_item_id)
    REFERENCES menu_item(menu_item_id),
  FOREIGN KEY(ccrc_user_id)
    REFERENCES ccrc_user(ccrc_user_id));
GO


CREATE INDEX menu_security_FKIndex1 ON menu_security (menu_item_id);
GO
CREATE INDEX menu_security_FKIndex2 ON menu_security (ccrc_user_id);
GO



CREATE TABLE ws_user (
  ws_user_id INTEGER  NOT NULL   IDENTITY ,
  agency_id INTEGER    ,
  call_center_id INTEGER    ,
  login_username VARCHAR(30)    ,
  login_password VARCHAR(30)    ,
  active_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(ws_user_id)    ,
  FOREIGN KEY(call_center_id)
    REFERENCES call_center(call_center_id),
  FOREIGN KEY(agency_id)
    REFERENCES Agency(agency_id));
GO


CREATE INDEX ws_user_FKIndex1 ON ws_user (call_center_id);
GO
CREATE INDEX ws_user_FKIndex2 ON ws_user (agency_id);
GO



CREATE TABLE foreclosure_case (
  fc_id INTEGER  NOT NULL   IDENTITY ,
  agency_id INTEGER  NOT NULL  ,
  call_id INTEGER    ,
  program_id INTEGER  NOT NULL  ,
  agency_case_num VARCHAR(30)  NOT NULL  ,
  agency_client_num VARCHAR(30)    ,
  intake_dt DATETIME  NOT NULL  ,
  income_earners_cd VARCHAR(15)    ,
  case_source_cd VARCHAR(15)    ,
  race_cd VARCHAR(15)    ,
  household_cd VARCHAR(15)    ,
  never_bill_reason_cd VARCHAR(15)   DEFAULT 'N' ,
  never_pay_reason_cd VARCHAR(15)   DEFAULT 'N' ,
  dflt_reason_1st_cd VARCHAR(15)    ,
  dflt_reason_2nd_cd VARCHAR(15)    ,
  hud_termination_reason_cd VARCHAR(15)    ,
  hud_termination_dt DATETIME    ,
  hud_outcome_cd VARCHAR(15)    ,
  AMI_percentage INTEGER    ,
  counseling_duration_cd VARCHAR(15)    ,
  gender_cd VARCHAR(15)    ,
  borrower_fname VARCHAR(30)  NOT NULL  ,
  borrower_lname VARCHAR(30)  NOT NULL  ,
  borrower_mname VARCHAR(30)    ,
  mother_maiden_lname VARCHAR(30)    ,
  borrower_ssn VARCHAR(9)    ,
  borrower_last4_SSN VARCHAR(4)    ,
  borrower_DOB DATETIME    ,
  co_borrower_fname VARCHAR(30)    ,
  co_borrower_lname VARCHAR(30)    ,
  co_borrower_mname VARCHAR(30)    ,
  co_borrower_ssn VARCHAR(9)    ,
  co_borrower_last4_SSN VARCHAR(4)    ,
  co_borrower_DOB DATETIME    ,
  primary_contact_no VARCHAR(20)  NOT NULL  ,
  second_contact_no VARCHAR(20)    ,
  email_1 VARCHAR(50)    ,
  contact_zip_plus4 VARCHAR(4)    ,
  email_2 VARCHAR(50)    ,
  contact_addr1 VARCHAR(50)  NOT NULL  ,
  contact_addr2 VARCHAR(50)    ,
  contact_city VARCHAR(30)  NOT NULL  ,
  contact_state_cd VARCHAR(15)  NOT NULL  ,
  contact_zip VARCHAR(5)  NOT NULL  ,
  prop_addr1 VARCHAR(50)    ,
  prop_addr2 VARCHAR(50)    ,
  prop_city VARCHAR(30)    ,
  prop_state_cd VARCHAR(15)    ,
  prop_zip VARCHAR(5)    ,
  prop_zip_plus_4 VARCHAR(4)    ,
  bankruptcy_ind VARCHAR(1)    ,
  bankruptcy_attorney VARCHAR(50)    ,
  bankruptcy_pmt_current_ind VARCHAR(1)    ,
  borrower_educ_level_completed_cd VARCHAR(15)    ,
  borrower_marital_status_cd VARCHAR(15)    ,
  borrower_preferred_lang_cd VARCHAR(15)    ,
  borrower_occupation VARCHAR(50)    ,
  co_borrower_occupation VARCHAR(50)    ,
  hispanic_ind VARCHAR(1)    ,
  duplicate_ind VARCHAR(1)    ,
  fc_notice_received_ind VARCHAR(1)    ,
  case_complete_ind VARCHAR(1)    ,
  completed_dt DATETIME    ,
  funding_consent_ind VARCHAR(1)  NOT NULL  ,
  servicer_consent_ind VARCHAR(1)  NOT NULL  ,
  agency_media_consent_ind VARCHAR(1)    ,
  hpf_media_candidate_ind VARCHAR(1)    ,
  hpf_network_candidate_ind VARCHAR(1)    ,
  hpf_success_story_ind VARCHAR(1)    ,
  agency_success_story_ind VARCHAR(1)    ,
  borrower_disabled_ind VARCHAR(1)    ,
  co_borrower_disabled_ind VARCHAR(1)    ,
  summary_sent_other_cd VARCHAR(15)    ,
  summary_sent_other_dt DATETIME    ,
  summary_sent_dt DATETIME    ,
  occupant_num TINYINT    ,
  loan_dflt_reason_notes VARCHAR(8000)    ,
  action_items_notes VARCHAR(8000)    ,
  followup_notes VARCHAR(8000)    ,
  prim_res_est_mkt_value NUMERIC(15,2)    ,
  counselor_id_ref VARCHAR(30)  NOT NULL  ,
  counselor_lname VARCHAR(30)  NOT NULL  ,
  counselor_fname VARCHAR(30)  NOT NULL  ,
  counselor_email VARCHAR(50)  NOT NULL  ,
  counselor_phone VARCHAR(20)  NOT NULL  ,
  counselor_ext VARCHAR(20)    ,
  discussed_solution_with_srvcr_ind VARCHAR(1)    ,
  worked_with_another_agency_ind VARCHAR(1)    ,
  contacted_srvcr_recently_ind VARCHAR(1)    ,
  has_workout_plan_ind VARCHAR(1)    ,
  srvcr_workout_plan_current_ind VARCHAR(1)    ,
  opt_out_newsletter_ind VARCHAR(1)  NOT NULL  ,
  opt_out_survey_ind VARCHAR(1)  NOT NULL  ,
  do_not_call_ind VARCHAR(1)  NOT NULL  ,
  owner_occupied_ind VARCHAR(1)  NOT NULL  ,
  primary_residence_ind VARCHAR(1)  NOT NULL  ,
  realty_company VARCHAR(50)    ,
  property_cd VARCHAR(15)    ,
  for_sale_ind VARCHAR(1)    ,
  home_sale_price NUMERIC(15,2)    ,
  home_purchase_year INT    ,
  home_purchase_price NUMERIC(15,2)    ,
  home_current_market_value NUMERIC(15,2)    ,
  military_service_cd VARCHAR(15)    ,
  household_gross_annual_income_amt NUMERIC(15,2)    ,
  loan_list VARCHAR(500)    ,
  intake_credit_score VARCHAR(4)    ,
  intake_credit_bureau_cd VARCHAR(15)    ,
  fc_sale_dt DATETIME    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(fc_id)      ,
  FOREIGN KEY(agency_id)
    REFERENCES Agency(agency_id),
  FOREIGN KEY(call_id)
    REFERENCES call(call_id),
  FOREIGN KEY(program_id)
    REFERENCES program(program_id));
GO


CREATE INDEX Counselling_session_FKIndex1 ON foreclosure_case (agency_id);
GO
CREATE INDEX foreclosure_case_FKIndex2 ON foreclosure_case (call_id);
GO
CREATE INDEX foreclosure_case_FKIndex3 ON foreclosure_case (program_id);
GO



CREATE TABLE invoice_case (
  invoice_case_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  invoice_payment_id INTEGER    ,
  Invoice_id INTEGER  NOT NULL  ,
  pmt_reject_reason_cd VARCHAR(15)    ,
  invoice_case_pmt_amt NUMERIC(15,2)    ,
  invoice_case_bill_amt NUMERIC(15,2)    ,
  in_dispute_ind VARCHAR(1)    ,
  rebill_ind VARCHAR(1)    ,
  intent_to_pay_flg_TBD INTEGER    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(invoice_case_id)      ,
  FOREIGN KEY(Invoice_id)
    REFERENCES Invoice(Invoice_id),
  FOREIGN KEY(invoice_payment_id)
    REFERENCES invoice_payment(invoice_payment_id),
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX invoice_session_FKIndex1 ON invoice_case (Invoice_id);
GO
CREATE INDEX invoice_case_FKIndex2 ON invoice_case (invoice_payment_id);
GO
CREATE INDEX invoice_case_FKIndex3 ON invoice_case (fc_id);
GO



CREATE TABLE activity_log (
  activity_log_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  activity_cd VARCHAR(15)  NOT NULL  ,
  activity_dt DATETIME  NOT NULL  ,
  activity_note VARCHAR(2000)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(activity_log_id)  ,
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX activity_log_FKIndex1 ON activity_log (fc_id);
GO



CREATE TABLE budget_set (
  budget_set_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  total_income NUMERIC(15,2)    ,
  total_expenses NUMERIC(15,2)    ,
  total_assets NUMERIC(15,2)    ,
  budget_set_dt DATETIME  NOT NULL  ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(budget_set_id)  ,
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX budget_FKIndex1 ON budget_set (fc_id);
GO



CREATE TABLE case_audit (
  case_audit_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  appropriate_outcome_cd VARCHAR(1)    ,
  reason_for_default_cd VARCHAR(1)    ,
  complete_budget_cd VARCHAR(1)    ,
  onsite_audit_ind VARCHAR(1)    ,
  audit_dt DATETIME    ,
  audit_comment VARCHAR(300)    ,
  reviewed_by VARCHAR(30)    ,
  session_cd VARCHAR(15)    ,
  client_action_plan_cd VARCHAR(15)    ,
  verbal_privacy_cd VARCHAR(15)    ,
  written_privacy_cd VARCHAR(15)    ,
  data_intake_complete_cd VARCHAR(15)    ,
  compliant_cd VARCHAR(15)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(case_audit_id)  ,
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX foreclosure_case_audit_FKIndex1 ON case_audit (fc_id);
GO



CREATE TABLE case_loan (
  case_loan_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  servicer_id INTEGER  NOT NULL  ,
  other_servicer_name VARCHAR(50)    ,
  acct_num VARCHAR(30)  NOT NULL  ,
  loan_1st_2nd_cd VARCHAR(15)    ,
  mortgage_type_cd VARCHAR(15)    ,
  arm_reset_ind VARCHAR(1)    ,
  term_length_cd VARCHAR(15)    ,
  loan_delinq_status_cd VARCHAR(15)    ,
  current_loan_balance_amt NUMERIC(15,2)    ,
  orig_loan_amt NUMERIC(15,2)    ,
  interest_rate NUMERIC(5,3)    ,
  originating_lender_name VARCHAR(50)    ,
  orig_mortgage_co_FDIC_NCUS_num VARCHAR(20)    ,
  orig_mortgage_co_name VARCHAR(50)    ,
  orginal_loan_num VARCHAR(30)    ,
  FDIC_NCUS_num_current_servicer_TBD VARCHAR(30)    ,
  current_servicer_name_TBD VARCHAR(30)    ,
  investor_loan_num VARCHAR(30)    ,
  create_dt DATETIME  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(case_loan_id)  ,
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX session_Loan_FKIndex1 ON case_loan (fc_id);
GO



CREATE TABLE case_post_counseling_status (
  case_post_counseling_status_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  followup_dt DATETIME  NOT NULL  ,
  followup_comment VARCHAR(8000)    ,
  followup_source_cd VARCHAR(15)  NOT NULL  ,
  followup_outcome_cd VARCHAR(15)    ,
  case_status_cd VARCHAR(15)    ,
  loan_delinq_status_cd VARCHAR(15)    ,
  still_in_house_ind VARCHAR(1)    ,
  credit_score VARCHAR(4)    ,
  credit_bureau_cd VARCHAR(15)    ,
  credit_report_dt DATETIME    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(case_post_counseling_status_id)  ,
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX case_followup_status_FKIndex1 ON case_post_counseling_status (fc_id);
GO



CREATE TABLE outcome_item (
  outcome_item_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  outcome_type_id INTEGER  NOT NULL  ,
  outcome_dt DATETIME    ,
  outcome_deleted_dt DATETIME    ,
  nonprofitreferral_key_num VARCHAR(10)    ,
  ext_ref_other_name VARCHAR(50)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(outcome_item_id)    ,
  FOREIGN KEY(outcome_type_id)
    REFERENCES outcome_type(outcome_type_id),
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX outcome_FKIndex2 ON outcome_item (outcome_type_id);
GO
CREATE INDEX outcome_item_FKIndex2 ON outcome_item (fc_id);
GO



CREATE TABLE agency_payable_case (
  agency_payable_case_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  agency_payable_id INTEGER  NOT NULL  ,
  pmt_dt DATETIME    ,
  pmt_amt NUMERIC(15,2)    ,
  NFMC_difference_paid_ind VARCHAR(1)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(agency_payable_case_id)    ,
  FOREIGN KEY(agency_payable_id)
    REFERENCES agency_payable(agency_payable_id),
  FOREIGN KEY(fc_id)
    REFERENCES foreclosure_case(fc_id));
GO


CREATE INDEX foreclosure_case_has_agency_payment_FKIndex2 ON agency_payable_case (agency_payable_id);
GO
CREATE INDEX agency_payable_case_FKIndex2 ON agency_payable_case (fc_id);
GO



CREATE TABLE budget_item (
  budget_item_id INTEGER  NOT NULL   IDENTITY ,
  budget_set_id INTEGER  NOT NULL  ,
  budget_subcategory_id INTEGER  NOT NULL  ,
  budget_item_amt NUMERIC(15,2)  NOT NULL  ,
  budget_note VARCHAR(100)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(budget_item_id, budget_set_id)    ,
  FOREIGN KEY(budget_set_id)
    REFERENCES budget_set(budget_set_id),
  FOREIGN KEY(budget_subcategory_id)
    REFERENCES budget_subcategory(budget_subcategory_id));
GO


CREATE INDEX budget_item_FKIndex1 ON budget_item (budget_set_id);
GO
CREATE INDEX budget_item_FKIndex2 ON budget_item (budget_subcategory_id);
GO



CREATE TABLE budget_asset (
  budget_asset_id INTEGER  NOT NULL   IDENTITY ,
  budget_set_id INTEGER  NOT NULL  ,
  asset_name VARCHAR(50)    ,
  asset_value NUMERIC(15,2)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(budget_asset_id)  ,
  FOREIGN KEY(budget_set_id)
    REFERENCES budget_set(budget_set_id));
GO


CREATE INDEX budget_asset_FKIndex1 ON budget_asset (budget_set_id);
GO




