-- =============================================
-- Create date: 10 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 13 Feb 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO
ALTER TABLE case_audit DROP COLUMN appropriate_outcome_cd;
ALTER TABLE case_audit ADD appropriate_outcome_ind varchar(1) NULL;

ALTER TABLE case_audit DROP COLUMN reason_for_default_cd 
ALTER TABLE case_audit ADD appropriate_reason_for_dflt_ind varchar(1) NULL

ALTER TABLE case_audit DROP COLUMN complete_budget_cd ;
ALTER TABLE case_audit ADD complete_budget_ind varchar(1) NULL;

ALTER TABLE case_audit DROP COLUMN onsite_audit_ind ;
ALTER TABLE case_audit ADD audit_type_cd varchar(15) Null;

ALTER TABLE case_audit DROP COLUMN client_action_plan_cd ;
ALTER TABLE case_audit ADD client_action_plan_ind varchar(1) NULL;

ALTER TABLE case_audit DROP COLUMN verbal_privacy_cd ;
ALTER TABLE case_audit ADD verbal_privacy_consent_ind varchar(1) NULL;

ALTER TABLE case_audit DROP COLUMN written_privacy_cd ;
ALTER TABLE case_audit ADD written_privacy_consent_ind varchar(1) NULL;

ALTER TABLE case_audit DROP COLUMN compliant_cd ;
ALTER TABLE case_audit ADD compliant_ind varchar(1) NULL;

ALTER TABLE case_audit ADD audit_failure_reason_cd varchar(15) NULL;
ALTER TABLE case_audit DROP COLUMN data_intake_complete_cd;
ALTER TABLE case_audit DROP COLUMN session_cd;


ALTER TABLE case_post_counseling_status DROP COLUMN followup_outcome_cd;
ALTER TABLE case_post_counseling_status ADD outcome_type_id int NULL;
ALTER TABLE case_post_counseling_status 
ADD CONSTRAINT outcome_type_FK  FOREIGN KEY(outcome_type_id )
    REFERENCES outcome_type(outcome_type_id);
ALTER TABLE case_post_counseling_status DROP COLUMN case_status_cd;

INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('audit type code', 'Audit type code');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('audit type code','ONSITE','On-Site Audit','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('audit type code','DESK','Desk Audit','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

DELETE FROM ref_code_set WHERE ref_code_set_name = 'case followup source code';
INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('follow up source code', 'Follow up source code');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('follow up source code','CREDITREPORT','Credit Report','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('follow up source code','HOMEOWNER','Homeowner','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('follow up source code','HPF','Home Ownership Preservation Foundation','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('follow up source code','3RDPARTY','Third Party Provider','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('follow up source code','AGENCY','Counseling Agency Network','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

ALTER TABLE agency ADD finance_contact_fname varchar(30) null;
ALTER TABLE agency ADD finance_contact_lname varchar(30) null;
ALTER TABLE agency ADD finance_phone varchar(20) null;
ALTER TABLE agency ADD finance_fax varchar(20) null;
ALTER TABLE agency ADD finance_email varchar(50) null;
ALTER TABLE agency ADD finance_addr1 varchar(50) null;
ALTER TABLE agency ADD finance_addr2 varchar(50) null;
ALTER TABLE agency ADD finance_city varchar(30) null;
ALTER TABLE agency ADD finance_state_cd varchar(15) null;
ALTER TABLE agency ADD finance_zip varchar(5) null;
ALTER TABLE agency ADD finance_zip_plus_4 varchar(4) null;

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('payment reject reason code','FREDDUP','Freddie Previously paid/Duplicate','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('payment reject reason code','NFMCDUP','NFMC Previously paid/Duplicate','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

delete ref_code_item WHERE ref_code_set_name ='counseling durarion code';
delete ref_code_set WHERE ref_code_set_name ='counseling durarion code';

INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('counseling duration code', 'Counseling duration code');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('counseling duration code','<30','Less than 30 minutes','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('counseling duration code','30-59','30-59 minutes','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('counseling duration code','60-89','60-89 minutes','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('counseling duration code','90-120','90-120 minutes','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('counseling duration code','>121','More than 120 minutes','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','1STLOANDEL','1st Loan Deleted','',13,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','1STLOANINS','1st Loan Inserted','',14,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','2NDLOANDEL','2nd Loan Deleted','',15,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','2NDLOANINS','2nd Loan Inserted','',16,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','CONV','Initial CCRC Conversion','',17,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','CONVINS','Insert After Conversion','',18,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('activity code','CONVUPD','Updated After Conversion','',19,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');


DELETE FROM menu_security;
DELETE FROM menu_item;
DELETE FROM menu_group;

INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (1,'Accounting',3,'#');
INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (2,'Home',1,'Default.aspx');
INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (3,'Foreclosure Case',2,'SearchForeclosureCase.aspx');
INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (4,'Reports',4,'default.aspx');
INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (5,'Change Password',5,'ChangePassword.aspx');
INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (6,'Admin',6,'#');
INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (7,'Logout',7,'Logout.aspx');

INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (1,1,'Funding Source Invoices',1,'FundingSourceInvoice.aspx');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (2,1,'Agency Accounts Payables',2,'AgencyPayable.aspx');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (3,6,'Agency',1,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (4,6,'Funding Source',2,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (5,6,'Servicer',3,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (6,6,'Programs',5,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (7,6,'Rates',4,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (8,6,'General Codes',6,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (9,6,'Budget Categories',7,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (10,6,'Outcomes',8,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (11,6,'User',9,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (12,6,'WS User',10,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (13,6,'Call Center',11,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (14,6,'Congressional Districts',12,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (15,6,'Area Median Income',13,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (16,6,'Geocode Reference',14,'#');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (17,6,'ForeclosureDetail',15,'ForeclosureCaseInfo.aspx');
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target)   VALUES (18,1,'Invoice Payments',3,'InvoicePayment.aspx');

INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (1,1,1,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (2,1,2,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (3,1,3,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (4,1,4,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (5,1,5,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (6,1,6,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (7,1,7,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (8,1,8,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (9,1,9,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (10,1,10,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (11,1,11,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (12,1,12,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (13,1,13,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (14,1,14,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (15,1,15,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (16,1,16,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (17,4,1,'R');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (18,4,2,'R');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (19,1,17,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (20,4,17,'R');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (21,1,18,'U');
INSERT INTO menu_security (menu_security_id, ccrc_user_id, menu_item_id, permission_value)   VALUES (22,4,18,'R');