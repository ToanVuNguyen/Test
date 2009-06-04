-- =============================================
-- Create date: 12 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Create tables in the new HPF database
--		Apply database changes on:  12 Mar 2009
--		Refer to file "DB Track changes.xls"
-- ============================================
USE HPF
GO

CREATE TABLE funding_source (
  funding_source_id INTEGER  NOT NULL   IDENTITY  (1000,1),
  funding_source_name VARCHAR(50)    ,
  funding_source_comment VARCHAR(300)    ,
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
  funding_source_abbrev VARCHAR(10)  NOT NULL  ,
  sharepoint_foldername varchar(256) NULL,
  phone varchar(20) NULL,
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



CREATE TABLE hpf_user (
  hpf_user_id INTEGER  NOT NULL   IDENTITY ,
  user_login_id VARCHAR(30)    ,
  active_ind VARCHAR(1)    ,
  user_role_str_TBD VARCHAR(30)    ,
  fname VARCHAR(30)    ,
  lname VARCHAR(30)    ,
  email VARCHAR(50)    ,
  phone VARCHAR(20)    ,
  last_login_dt DATETIME    ,
  password	VARCHAR(128)    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(hpf_user_id));
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
  program_id INTEGER  NOT NULL   IDENTITY (2000,1),
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
  code_set_comment VARCHAR(300) NULL,
  agency_usage_ind varchar(1) NULL, 
PRIMARY KEY(ref_code_set_name));
GO



CREATE TABLE servicer (
  servicer_id INTEGER  NOT NULL   IDENTITY (60000,1),
  servicer_name VARCHAR(50)     NOT NULL  ,
  contact_fname VARCHAR(30)    ,
  contact_lname VARCHAR(30)    ,
  contact_email VARCHAR(50)    ,
  phone VARCHAR(100)    ,
  fax VARCHAR(100)    ,
  active_ind VARCHAR(1)    ,
  funding_agreement_ind VARCHAR(1)    ,
  secure_delivery_method_cd VARCHAR(15)    ,
  couseling_sum_format_cd VARCHAR(15)    ,
  iclear_servicer_num Varchar(30) Null,
  fis_servicer_num varchar(30) Null,
  sharepoint_foldername varchar(256) NULL,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(servicer_id));
GO



CREATE TABLE menu_group (
  menu_group_id INTEGER  NOT NULL,
  group_name VARCHAR(50)  NOT NULL  ,
  group_sort_order INTEGER  NOT NULL  ,
  group_target VARCHAR(200)      ,
PRIMARY KEY(menu_group_id));
GO

CREATE TABLE outcome_type (
  outcome_type_id INTEGER  NOT NULL   IDENTITY (1000, 1),
  outcome_type_name VARCHAR(50)    ,
  outcome_type_comment VARCHAR(300)    ,
  payable_ind VARCHAR(1)    ,
  inactive_dt datetime NULL,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(outcome_type_id));
GO



CREATE TABLE call_center (
  call_center_id INTEGER  NOT NULL   IDENTITY (1000,1),
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
  state varchar(2),
  county varchar(3),
  county_name VARCHAR(50)    ,
  CBSASub VARCHAR(30)    ,
  metro_area_name VARCHAR(100)    ,
  median1999 INT    ,
  median_income INT    NULL,
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
  msa  varchar(4)     ,
  county_town_name VARCHAR(50)    ,
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
  agency_id INTEGER  NOT NULL   IDENTITY (30000,1),
  agency_name VARCHAR(50)     NOT NULL  ,
  contact_fname VARCHAR(30)    ,
  contact_lname VARCHAR(30)    ,
  phone VARCHAR(20)    ,
  fax VARCHAR(20)    ,
  email VARCHAR(50)    ,
  active_ind VARCHAR(1)    ,
  hud_agency_num VARCHAR(20)    ,
  hud_agency_sub_grantee_num VARCHAR(20)    ,
  finance_contact_fname varchar(30) null,
  finance_contact_lname varchar(30) null,
  finance_phone varchar(20) null,
  finance_fax varchar(20) null,
  finance_email varchar(50) null,
  finance_addr1 varchar(50) null,
  finance_addr2 varchar(50) null,
  finance_city varchar(30) null,
  finance_state_cd varchar(15) null,
  finance_zip varchar(5) null,
  finance_zip_plus_4 varchar(4) null,
  NFMC_branch_num varchar(30) Null,
  sharepoint_foldername varchar(256) NULL,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(agency_id));
GO



CREATE TABLE budget_category (
  budget_category_id INTEGER  NOT NULL   IDENTITY (1000,1),
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
  invoice_payment_id INTEGER  NOT NULL   IDENTITY(1000, 1) ,
  funding_source_id INTEGER  NOT NULL  ,
  pmt_num VARCHAR(30)    ,
  pmt_dt DATETIME    ,
  pmt_cd VARCHAR(15)    ,
  pmt_amt NUMERIC(15,2)    ,
  invoice_payment_comment varchar(300) null,
  payment_file varchar(255) null,
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

CREATE TABLE Invoice (
  Invoice_id INTEGER  NOT NULL   IDENTITY(1000, 1) ,
  funding_source_id INTEGER  NOT NULL  ,
  invoice_dt DATETIME    ,
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

CREATE TABLE menu_item (
  menu_item_id INTEGER  NOT NULL  ,
  menu_group_id INTEGER  NOT NULL  ,
  item_name VARCHAR(50)  NOT NULL  ,
  item_sort_order INTEGER  NOT NULL  ,
  item_target VARCHAR(200)      ,
  visibled bit NULL,
PRIMARY KEY(menu_item_id)  ,
  FOREIGN KEY(menu_group_id)
    REFERENCES menu_group(menu_group_id));
GO


CREATE TABLE agency_payable (
  agency_payable_id INTEGER  NOT NULL   IDENTITY (1000, 1),
  agency_id INTEGER  NOT NULL  ,
  pmt_dt DATETIME    ,
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


CREATE TABLE call (
  call_id INTEGER  NOT NULL   IDENTITY (1000, 1),
  call_center_id INTEGER    NOT NULL,
  cc_agent_id_key VARCHAR(55)    ,
  start_dt DATETIME    NOT NULL,
  end_dt DATETIME    NOT NULL,
  dnis VARCHAR(10)    ,
  call_center_name VARCHAR(4)    ,
  call_source_cd VARCHAR(15)    ,
  reason_for_call VARCHAR(75)    ,
  loan_acct_num VARCHAR(30)    ,
  fname VARCHAR(30)    ,
  lname VARCHAR(30)    ,
  servicer_id INTEGER    ,
  other_servicer_name VARCHAR(50)    ,
  prop_zip_full9 VARCHAR(9)    ,
  prev_agency_id INTEGER    ,
  selected_agency_id  	INTEGER NULL,
  screen_rout VARCHAR(2000)    ,
  final_dispo_cd VARCHAR(15)    ,
  trans_num VARCHAR(12)    ,
  cc_call_key VARCHAR(18)    NOT NULL,
  loan_delinq_status_cd VARCHAR(15)    ,
  selected_counselor VARCHAR(40)    ,
  homeowner_ind VARCHAR(1)    ,
  power_of_attorney_ind VARCHAR(1)    ,
  authorized_ind VARCHAR(1)    ,
  city varchar(30) NULL,
  state varchar(2) NULL,
  nonprofitreferral_key_num1 Varchar(10) NULL,
  nonprofitreferral_key_num2 Varchar(10) NULL,
  nonprofitreferral_key_num3 Varchar(10) NULL,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
,delinq_ind	varchar(1)	Null
,prop_street_addr	varchar(50)	Null
,prim_res_ind	varchar(1)	Null
,max_loan_amt_ind	varchar(1)	Null
,cust_phone	varchar(10)	Null
,loan_lookup_cd	varchar(15)	Null
,orig_prior2009_ind	varchar(1)	Null
,payment_amt	Numeric(7)	Null
,gross_inc_amt	Numeric(8)	Null
,dti_ind	varchar(1)	Null
,servicer_ca_num	int	Null
,servicer_ca_last_contact_dt	datetime	Null
,servicer_ca_id	int	Null
,servicer_ca_other_name	varchar(50)	Null
,mha_info_share_ind	varchar(1)	Null
,ict_call_id	varchar(40)	Null
,mha_eligibility_cd	varchar(15)	Null
,PRIMARY KEY(call_id)  ,
  FOREIGN KEY(call_center_id)
    REFERENCES call_center(call_center_id));
GO

ALTER TABLE call ADD CONSTRAINT agency_FK1 FOREIGN KEY (selected_agency_id) REFERENCES agency(agency_id);

CREATE TABLE budget_subcategory (
  budget_subcategory_id INTEGER  NOT NULL   IDENTITY (1000,1),
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

CREATE TABLE agency_rate (
  agency_rate_id INTEGER  NOT NULL   IDENTITY ,
  program_id INTEGER  NOT NULL  ,
  agency_id INTEGER  NOT NULL  ,
  pmt_rate NUMERIC(15, 2)    ,
  eff_dt DATETIME    ,
  exp_dt DATETIME    ,
  rate_comment VARCHAR(300)    ,
  nfmc_upcharge_pmt_rate numeric(15,2) NULL, 
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


CREATE TABLE menu_security (
  menu_security_id INTEGER  NOT NULL ,
  hpf_user_id INTEGER  NOT NULL  ,
  menu_item_id INTEGER  NOT NULL  ,
  permission_value VARCHAR(1)  NOT NULL    ,
PRIMARY KEY(menu_security_id)    ,
  FOREIGN KEY(menu_item_id)
    REFERENCES menu_item(menu_item_id),
  FOREIGN KEY(hpf_user_id)
    REFERENCES hpf_user(hpf_user_id));
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
  never_bill_reason_cd VARCHAR(15) ,
  never_pay_reason_cd VARCHAR(15),
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
  borrower_ssn VARCHAR(255)    ,
  borrower_last4_SSN VARCHAR(4)    ,
  borrower_DOB DATETIME    ,
  co_borrower_fname VARCHAR(30)    ,
  co_borrower_lname VARCHAR(30)    ,
  co_borrower_mname VARCHAR(30)    ,
  co_borrower_ssn VARCHAR(255)    ,
  co_borrower_last4_SSN VARCHAR(4)    ,
  co_borrower_DOB DATETIME    ,
  primary_contact_no VARCHAR(20)  NOT NULL  ,
  second_contact_no VARCHAR(20)    ,
  email_1 VARCHAR(50)    ,
  contact_zip_plus4 VARCHAR(4)    ,
  email_2 VARCHAR(50)    ,
  contact_addr1 VARCHAR(50)  	NOT NULL  ,
  contact_addr2 VARCHAR(50)    ,
  contact_city VARCHAR(30)  	NOT NULL  ,
  contact_state_cd VARCHAR(15)  	NOT NULL  ,
  contact_zip VARCHAR(5)  		NOT NULL  ,
  prop_addr1 VARCHAR(50)    	NOT NULL ,
  prop_addr2 VARCHAR(50)    ,
  prop_city VARCHAR(30)    		NOT NULL ,
  prop_state_cd VARCHAR(15)    	NOT NULL ,
  prop_zip VARCHAR(5)    		NOT NULL ,
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
  duplicate_ind VARCHAR(1)    	NOT NULL ,
  fc_notice_received_ind VARCHAR(1)    ,
  completed_dt DATETIME    ,
  funding_consent_ind VARCHAR(1)  NOT NULL  ,
  servicer_consent_ind VARCHAR(1)  NOT NULL  ,
  agency_media_interest_ind VARCHAR(1)    ,
  hpf_media_candidate_ind VARCHAR(1)    ,
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
  opt_out_newsletter_ind VARCHAR(1)  NOT NULL,
  opt_out_survey_ind VARCHAR(1)  NOT NULL ,
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
  ccrc_referral_seq numeric(8) NULL,
  ccrc_sor_ind varchar(1) NULL,
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


CREATE TABLE case_audit (
  case_audit_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  appropriate_outcome_ind varchar(1) NULL,
  appropriate_reason_for_dflt_ind varchar(1) NULL,
  complete_budget_ind varchar(1) NULL,
  audit_type_cd varchar(15) Null,
  audit_dt DATETIME    NULL,
  audit_comment VARCHAR(300)    NULL,
  reviewed_by VARCHAR(30)    NULL,
  client_action_plan_ind varchar(1) NULL,  
  verbal_privacy_consent_ind varchar(1) NULL,  
  written_privacy_consent_ind varchar(1) NULL,
  compliant_ind varchar(1) NULL,
  audit_failure_reason_cd varchar(15) NULL,
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


CREATE TABLE case_loan (
  case_loan_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  servicer_id INTEGER  NOT NULL  ,
  other_servicer_name VARCHAR(50)    ,
  acct_num VARCHAR(30)  NOT NULL  ,
  loan_1st_2nd_cd VARCHAR(15)    NOT NULL  ,
  mortgage_type_cd VARCHAR(15)    ,
  arm_reset_ind VARCHAR(1)    ,
  term_length_cd VARCHAR(15)    ,
  loan_delinq_status_cd VARCHAR(15)    ,
  current_loan_balance_amt NUMERIC(15,2)    ,
  orig_loan_amt NUMERIC(15,2)    ,
  interest_rate NUMERIC(5,3)    ,
  originating_lender_name VARCHAR(50)    ,
  orig_mortgage_co_FDIC_NCUA_num VARCHAR(20)    ,
  orig_mortgage_co_name VARCHAR(50)    ,
  orginal_loan_num VARCHAR(30)    ,
  current_servicer_FDIC_NCUA_num VARCHAR(30)    ,  
  investor_loan_num VARCHAR(30)    ,
  investor_num varchar(30) null,
 investor_name varchar(50) null,
  changed_acct_num VARCHAR(100) NULL,
  mortgage_program_cd varchar(15) NULL,
  freddie_servicer_num Varchar(30) null ,
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


CREATE TABLE case_post_counseling_status (
  case_post_counseling_status_id INTEGER  NOT NULL   IDENTITY ,
  outcome_type_id INTEGER  NULL  ,
  fc_id INTEGER  NOT NULL  ,
  followup_dt DATETIME  NOT NULL  ,
  followup_comment VARCHAR(8000)    ,
  followup_source_cd VARCHAR(15)  NOT NULL  ,
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

ALTER TABLE case_post_counseling_status 
ADD CONSTRAINT outcome_type_FK  FOREIGN KEY(outcome_type_id )
    REFERENCES outcome_type(outcome_type_id);

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


CREATE TABLE agency_payable_case (
  agency_payable_case_id INTEGER  NOT NULL   IDENTITY ,
  fc_id INTEGER  NOT NULL  ,
  agency_payable_id INTEGER  NOT NULL  ,
  pmt_dt DATETIME    ,
  pmt_amt NUMERIC(15,2)    ,
  NFMC_difference_eligible_ind varchar(1) Not NULL,
  takeback_pmt_identified_dt datetime NULL,
  takeback_pmt_reason_cd varchar(15) NULL,
  NFMC_difference_paid_amt Numeric (15,2) Null,
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

/****** Object: Table [dbo].[Log] Script Date: 01/19/2009 11:53:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
    [LogID] [int] IDENTITY(1,1) NOT NULL,
    [EventID] [int] NULL,
    [Priority] [int] NOT NULL,
    [Severity] [nvarchar](32) NOT NULL,
    [Title] [nvarchar](256) NOT NULL,
    [Timestamp] [datetime] NOT NULL,
    [MachineName] [nvarchar](32) NOT NULL,
    [AppDomainName] [nvarchar](512) NOT NULL,
    [ProcessID] [nvarchar](256) NOT NULL,
    [ProcessName] [nvarchar](512) NOT NULL,
    [ThreadName] [nvarchar](512) NULL,
    [Win32ThreadId] [nvarchar](128) NULL,
    [Message] [nvarchar](1500) NULL,
    [FormattedMessage] [ntext] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED(LogID ASC)
    WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)
GO

/****** Object: Table [dbo].[Category] Script Date: 01/19/2009 15:28:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
    [CategoryID] [int] IDENTITY(1,1) NOT NULL,
    [CategoryName] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED([CategoryID] ASC)
    WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object: Table [dbo].[CategoryLog] Script Date: 01/19/2009 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryLog](
    [CategoryLogID] [int] IDENTITY(1,1) NOT NULL,
    [CategoryID] [int] NOT NULL,
    [LogID] [int] NOT NULL,
 CONSTRAINT [PK_CategoryLog] PRIMARY KEY CLUSTERED ([CategoryLogID] ASC)
    WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE foreclosure_case WITH CHECK ADD  CONSTRAINT [FK_call_id] FOREIGN KEY(call_id) REFERENCES call(call_id)
GO
ALTER TABLE case_loan WITH CHECK ADD  CONSTRAINT [FK_loan_servicer_id] FOREIGN KEY(servicer_id) REFERENCES servicer(servicer_id)
GO

CREATE TABLE nonprofitreferral
(id		INT NOT NULL PRIMARY KEY
, referral_org_name		varchar(150)
, referral_org_state	varchar(15)
, referral_org_zip		varchar(30)
, referral_contact_email	varchar(200)
);
