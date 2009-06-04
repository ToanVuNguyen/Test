-- =============================================
-- Create date: 2 Jun 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 2 Jun 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO
ALTER TABLE call ADD delinq_ind	varchar(1)	Null;
ALTER TABLE call ADD prop_street_addr	varchar(50)	Null;
ALTER TABLE call ADD prim_res_ind	varchar(1)	Null;
ALTER TABLE call ADD max_loan_amt_ind	varchar(1)	Null;
ALTER TABLE call ADD cust_phone	varchar(10)	Null;
ALTER TABLE call ADD loan_lookup_cd	varchar(15)	Null;
ALTER TABLE call ADD orig_prior2009_ind	varchar(1)	Null;
ALTER TABLE call ADD payment_amt	Numeric(7)	Null;
ALTER TABLE call ADD gross_inc_amt	Numeric(8)	Null;
ALTER TABLE call ADD dti_ind	varchar(1)	Null;
ALTER TABLE call ADD servicer_ca_num	int	Null;
ALTER TABLE call ADD servicer_ca_last_contact_dt	datetime	Null;
ALTER TABLE call ADD servicer_ca_id	int	Null;
ALTER TABLE call ADD servicer_ca_other_name	varchar(50)	Null;
ALTER TABLE call ADD mha_info_share_ind	varchar(1)	Null;
ALTER TABLE call ADD ict_call_id	varchar(40)	Null;
ALTER TABLE call ADD mha_eligibility_cd	varchar(15)	Null;

INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('mha eligibility code','MHA eligibility code');
INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('mha loan lookup code','MHA loan lookup code');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha loan lookup code','FAN','Fannie','ICT sends 1',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha loan lookup code','FRED','Freddie','ICT sends 2',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha loan lookup code','NOTFOUND','Not Found',NULL,3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha eligibility code','UNK','Unknown',NULL,1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha eligibility code','HAMP','HAMP Eligible',NULL,2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha eligibility code','HARP','HARP Eligible',NULL,3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha eligibility code','NOTHAMP','Not HAMP Eligible',NULL,4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha eligibility code','NOTHARP','Not HARP Eligible',NULL,5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('mha eligibility code','MHAINELIG','MHA Ineligible',NULL,6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
