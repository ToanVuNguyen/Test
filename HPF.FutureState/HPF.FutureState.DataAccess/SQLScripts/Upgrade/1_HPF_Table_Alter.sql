-- =============================================
-- Create date: 26 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 26 Mar 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO


ALTER TABLE Invoice_payment ALTER COLUMN payment_file varchar(300) NULL;

DELETE FROM ref_code_item WHERE ref_code_set_name IN ('hud termination reason code', 'hud outcome code');
DELETE FROM ref_code_item WHERE ref_code_set_name = 'household code' AND code = 'SINGL';
DELETE FROM ref_code_item WHERE ref_code_set_name = 'credit bureau code' AND code= 'TU';

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','100','Brought mortgage current','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','101','Mortgage refinanced','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','102','Mortgage modified','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','103','Received second mortgage','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','104','Initiated forbearance agreement/repayment plan','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','105','Executed a deed-in-lieu','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','106','Sold property/chose alternative housing solution','',7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','107','Pre-foreclosure sale','',8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','108','Mortgage foreclosed','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','109','Counseled and referred to another social service or emergency assistance agency','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','110','Obtained partial claim loan from FHA lender','',11,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','111','Bankruptcy','',12,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','112','Entered debt management plan','',13,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','113','Counseled and referred for legal assistance','',14,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','114','Currently receiving foreclosure prevention/budget counseling','',15,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','115','Withdrew from counseling','',16,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud outcome code','116','Other','',17,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud termination reason code','501','Clients needs have been met/situation resolved','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud termination reason code','502','Agency determines addtl counseling would not resolve situation','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud termination reason code','503','Unable to contact client','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud termination reason code','504','Client has not followed agreed upon action plan','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud termination reason code','505','Client terminated counseling','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('hud termination reason code','506','Client failed to attend counseling appointment','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('household code','SINGLE','Single adult','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('credit bureau code','TRANSUNION','TransUnion','TransUnion',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

UPDATE foreclosure_case
SET		hud_termination_reason_cd = '50'+ hud_termination_reason_cd
WHERE hud_termination_reason_cd IS NOT NULL;

UPDATE	foreclosure_case
SET		hud_outcome_cd = cast((cast(hud_outcome_cd as int) + 99) as varchar(15))
WHERE	hud_outcome_cd IS NOT NULL;

UPDATE	foreclosure_case SET	household_cd = 'SINGLE' WHERE	household_cd = 'SINGL';
UPDATE	foreclosure_case SET	intake_credit_bureau_cd = 'TRANSUNION' WHERE	intake_credit_bureau_cd = 'TU';
UPDATE	case_post_counseling_status SET	credit_bureau_cd = 'TRANSUNION' WHERE	credit_bureau_cd = 'TU';
