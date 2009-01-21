-- =============================================
-- Create date: 21 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 20 Jan 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

ALTER TABLE case_loan
ADD current_servicer_FDIC_NCUS_num varchar(30) NULL;

UPDATE case_loan SET current_servicer_FDIC_NCUS_num = FDIC_NCUS_Num_current_servicer_TBD;

ALTER TABLE Case_loan 
DROP COLUMN FDIC_NCUS_Num_current_servicer_TBD ;

ALTER TABLE [dbo].[ref_code_item]  
DROP CONSTRAINT [FK__ref_code___ref_c__1A14E395];

UPDATE	ref_code_set SET		ref_code_set_name = lower(ref_code_set_name);

UPDATE	ref_code_item SET		ref_code_set_name = lower(ref_code_set_name);

UPDATE	ref_code_set SET		ref_code_set_name = 'agency payable status code', code_set_comment = 'Agency payable status code'
WHERE	ref_code_set_name = 'agency payable - status code';

UPDATE	ref_code_set SET		ref_code_set_name = 'invoice status code', code_set_comment = 'Invoice status code'
WHERE	ref_code_set_name = 'invoice - status code';

UPDATE	ref_code_set SET		ref_code_set_name = 'program service code', code_set_comment = 'Program service code'
WHERE	ref_code_set_name = 'program - service code';

UPDATE	ref_code_item SET		ref_code_set_name = 'agency payable status code'
WHERE	ref_code_set_name = 'agency payable - status code';

UPDATE	ref_code_item SET		ref_code_set_name = 'invoice status code'
WHERE	ref_code_set_name = 'invoice - status code';

UPDATE	ref_code_item SET		ref_code_set_name = 'program service code'
WHERE	ref_code_set_name = 'program - service code';

UPDATE	ref_code_set SET ref_code_set_name = 'education level completed code', code_set_comment = 'Education level completed code'
WHERE	ref_code_set_name = 'education code';

UPDATE	ref_code_item SET ref_code_set_name = 'education level completed code' 
WHERE	ref_code_set_name = 'education code';

UPDATE	ref_code_set SET ref_code_set_name = 'preferred language code', code_set_comment = 'Preferred language code'
WHERE	ref_code_set_name = 'language code';

UPDATE	ref_code_item SET ref_code_set_name = 'preferred language code' 
WHERE	ref_code_set_name = 'language code';

ALTER TABLE [dbo].[ref_code_item]  WITH CHECK ADD  CONSTRAINT [FK_ref_code_set] FOREIGN KEY([ref_code_set_name])
REFERENCES [dbo].[ref_code_set] ([ref_code_set_name])


INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','N','No academic credentials','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','H','High school diploma or equivalent','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','T','Trade or craft certificate','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','A','Associate degree','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','B','Bachelor''s degree','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','M','Master''s degree (M.A., M.S.)','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','P','Professional degree (e.g., M.L.S., J.D., M.S.W., etc.)','',7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('education level completed code','D','Doctorate (e.g., Ph.D., D.Sc., M.D., D.Pharm., D.L.S., Ed.D.,etc.)','',8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','ale', 'Aleut','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','chi/zho', 'Chinese','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','cze/ces', 'Czech','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','eng',	'English','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','fil',	'Filipino','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','fre/fra',	'French','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','ger/deu',	'German','',7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','hin',	'Hindi','',8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','hmn',	'Hmong','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','jpn',	'Japanese','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','kor',	'Korean','',11,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','lao',	'Lao','',12,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','fil',	'Pilipino','',13,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','rus',	'Russian','',14,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','som',	'Somali','',15,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','spa',	'Spanish','',16,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','tgl',	'Tagalog','',17,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('preferred language code','vie',	'Vietnamese','',18,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');


ALTER TABLE foreclosure_case ALTER COLUMN borrower_SSN varchar(255) NULL;
ALTER TABLE foreclosure_case ALTER COLUMN co_borrower_SSN varchar(255) NULL;

ALTER TABLE foreclosure_case WITH CHECK ADD  CONSTRAINT [FK_call_id] FOREIGN KEY(call_id) REFERENCES call(call_id);
ALTER TABLE case_loan WITH CHECK ADD  CONSTRAINT [FK_loan_servicer_id] FOREIGN KEY(servicer_id) REFERENCES servicer(servicer_id);