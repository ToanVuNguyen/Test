-- =============================================
-- Create date: 19 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 19 Mar 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('audit failure reason code','Audit failure reason code');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','VERBPRIV','Deficiency in verbal privacy statement','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','WRITPRIV','Deficiency in written privacy statement','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','BADRFD','Failure to identify RFD','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','NOSVCRCONTACT','Failure to contact servicer','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','BADRECOMEND','Deficiency in providing proper options/recommendation','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','TOMANYOPTIONS','Provided too many options','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','UNPROFESSIONAL','Unprofessional interaction','',7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','NOEMPATHY','Lack of Empathy','',8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','NOCAPIN24HRS','CAP not sent w/in 24 hours','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','REF2NONAFFIL','Referred to unaffiliated agency','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','NOTHOMEOWNER','Session with someone other than homeowner','',11,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','NOCOUNSEL','Billed but no counseling occurred','',12,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('audit failure reason code','DUPE','Duplicate','',13,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
