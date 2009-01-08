-- =============================================
-- Create date: 07 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 07 Jan 2009
--		Refer to file "DB Track changes.xls"
-- =============================================

USE HPF
GO
CREATE TABLE menu_group	
(menu_group_id		int	not null,
group_name			varchar(50)	not null,
group_sort_order	int	not null,
group_target		varchar(200)	null,
PRIMARY KEY(menu_group_id)
)
GO

CREATE TABLE menu_item	
(menu_item_id	integer	not null,	
menu_group_id	integer	not null,	
item_name	varchar(50)	not null,	
item_sort_order	integer	not null,	
item_target	varchar(200)	null,
-- visibled	bit null,
PRIMARY KEY(menu_item_id),
FOREIGN KEY(menu_group_id) REFERENCES menu_group(menu_group_id)
)
GO
CREATE TABLE menu_security	
(menu_security_id	integer	not null,
	ccrc_user_id	integer	not null,	
	menu_item_id	integer	not null,	
	permission_value	char(1)	not null,
PRIMARY KEY(menu_security_id),
FOREIGN KEY(menu_item_id) REFERENCES menu_item(menu_item_id)
)
GO

CREATE TABLE change_audit	
(	change_audit_id	int	not null IDENTITY ,
	audit_dt		datetime	not null,
	audit_table		varchar(50)	not null,	
	audit_record_key	int		not null,
	audit_col_name	varchar(50)	not null,
	audit_old_value	varchar(8000)	not null,
	audit_new_value	varchar(8000)	not null,	
	create_dt		datetime	not null,
	create_user_id	varchar(15)	not null,	
	create_app_name	varchar(20)	not null,	
PRIMARY KEY(change_audit_id)
)
GO
DELETE FROM Call;
DROP INDEX call_FKIndex1 ;
DROP INDEX IFK_Rel_34 ;

ALTER TABLE call
ALTER COLUMN call_center_id	int	Not Null;
ALTER TABLE call
ALTER COLUMN start_dt	datetime	Not Null;
ALTER TABLE call
ALTER COLUMN end_dt	datetime	Not Null;
ALTER TABLE call
ALTER COLUMN cc_call_key	varchar(18)	Not Null;

CREATE INDEX call_FKIndex1 ON call (call_center_id);
GO

ALTER TABLE case_loan
ADD investor_loan_num varchar(30) null;

UPDATE case_loan SET investor_loan_num = freddie_loan_num;

ALTER TABLE case_loan
DROP COLUMN freddie_loan_num;

ALTER TABLE activity_log ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE activity_log ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE Agency ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE Agency ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE agency_payable ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE agency_payable ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE agency_payable_case ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE agency_payable_case ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE agency_rate ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE agency_rate ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE Area_Median_Income ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE Area_Median_Income ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_asset ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_asset ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_category ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_category ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_item ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_item ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_set ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_set ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_subcategory ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE budget_subcategory ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE call ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE call ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE call_center ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE call_center ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE case_audit ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE case_audit ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE case_loan ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE case_loan ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE case_post_counseling_status ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE case_post_counseling_status ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE ccrc_user ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE ccrc_user ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE change_audit ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE foreclosure_case ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE foreclosure_case ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE funding_source ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE funding_source ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE funding_source_group ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE funding_source_group ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE funding_source_rate ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE funding_source_rate ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE Invoice ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE Invoice ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE invoice_case ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE invoice_case ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE invoice_payment ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE invoice_payment ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE outcome_item ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE outcome_item ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE outcome_type ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE outcome_type ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE program ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE program ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE ref_code_item ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE ref_code_item ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE servicer ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE servicer ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE system_activity_log ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE system_activity_log ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;
ALTER TABLE ws_user ALTER COLUMN create_user_id VARCHAR(30) NOT NULL;
ALTER TABLE ws_user ALTER COLUMN chg_lst_user_id VARCHAR(30) NOT NULL;

UPDATE foreclosure_case 
SET counselor_id_ref = 'TEMP DATA' WHERE counselor_id_ref IS NULL;

ALTER TABLE foreclosure_case ALTER COLUMN counselor_id_ref varchar(30) NOT NULL;
ALTER TABLE foreclosure_case ADD fc_sale_dt DATETIME NULL;

ALTER TABLE funding_source ADD funding_source_abbrev VARCHAR(10) NULL;
UPDATE funding_source SET funding_source_abbrev = funding_source_name;
ALTER TABLE funding_source ALTER COLUMN funding_source_abbrev VARCHAR(10) NOT NULL;

ALTER TABLE foreclosure_case DROP COLUMN fc_sale_date_set_ind;
