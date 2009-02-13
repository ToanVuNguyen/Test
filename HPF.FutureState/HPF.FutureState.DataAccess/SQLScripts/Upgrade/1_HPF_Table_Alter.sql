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