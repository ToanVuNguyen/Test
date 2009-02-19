-- =============================================
-- Create date: 22 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 13 Feb 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO
DELETE ref_code_item WHERE	ref_code_set_name = 'payment reject reason code' AND code IN ('FREDDUP', 'NFMCDUP');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('payment reject reason code','FREDDUPE','Freddie Previously paid/Duplicate','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) 
VALUES('payment reject reason code','NFMCDUPE','NFMC Previously paid/Duplicate','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');


ALTER TABLE menu_security ADD hpf_user_id INTEGER NULL;

UPDATE	menu_security SET		hpf_user_id = ccrc_user_id;
ALTER TABLE menu_security DROP COLUMN ccrc_user_id;

ALTER TABLE menu_security 
ADD CONSTRAINT hpf_user_FK1 FOREIGN KEY(hpf_user_id) REFERENCES hpf_user(hpf_user_id) ;

ALTER TABLE menu_security ALTER COLUMN hpf_user_id INTEGER NOT NULL;

delete  from menu_security;
delete  from menu_item;
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (1,1,'Funding Source Invoices',1,'FundingSourceInvoice.aspx',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (2,1,'Agency Accounts Payables',2,'AgencyPayable.aspx',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (3,6,'Agency',1,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (4,6,'Funding Source',2,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (5,6,'Servicer',3,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (6,6,'Programs',5,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (7,6,'Rates',4,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (8,6,'General Codes',6,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (9,6,'Budget Categories',7,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (10,6,'Outcomes',8,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (11,6,'User',9,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (12,6,'WS User',10,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (13,6,'Call Center',11,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (14,6,'Congressional Districts',12,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (15,6,'Area Median Income',13,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (16,6,'Geocode Reference',14,'#',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (17,3,'ForeclosureDetail',1,'ForeclosureCaseInfo.aspx',0);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   VALUES (18,1,'Invoice Payments',3,'InvoicePayment.aspx',1);

INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (1,1,1,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (2,1,2,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (3,1,3,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (4,1,4,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (5,1,5,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (6,1,6,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (7,1,7,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (8,1,8,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (9,1,9,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (10,1,10,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (11,1,11,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (12,1,12,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (13,1,13,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (14,1,14,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (15,1,15,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (16,1,16,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (17,4,1,'R');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (18,4,2,'R');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (19,1,17,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (20,4,17,'R');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (21,1,18,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (22,4,18,'R');

Use HPF
GO
DECLARE	@return_value int, @po_hpf_user_id int, @today datetime, @v_menu_security_id int
SELECT @today = getdate();
select @v_menu_security_id = max(menu_security_id) from menu_security;

-- FCView         Foreclosure Case - View Only
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'FCView', @pi_password = N'FCView', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'FCView',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 17, @pi_permission_value = 'R', @pi_hpf_user_id  = @po_hpf_user_id

-- FCEdit         Foreclosure Case - Edit
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'FCEdit', @pi_password = N'FCEdit', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'FCEdit',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 17, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id

-- FCVE           Foreclosure Case - View Only and Foreclosure Case - Edit
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'FCVE', @pi_password = N'FCVE', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'FCVE',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 17, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id

-- ActView        Accounting - View Only
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'ActView', @pi_password = N'ActView', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'ActView',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 1, @pi_permission_value = 'R', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 2, @pi_permission_value = 'R', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 18, @pi_permission_value = 'R', @pi_hpf_user_id  = @po_hpf_user_id

-- ActEdit        Accounting - Edit
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'ActEdit', @pi_password = N'ActEdit', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'ActEdit',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 1, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 2, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 18, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id

--ACTVE          Accounting - View Only and Accounting - Edit
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'ACTVE', @pi_password = N'ACTVE', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'ACTVE',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT;
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 1, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 2, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 18, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id

--ALL   Foreclosure Case - View Only and Foreclosure Case - Edit and Accounting - View Only and Accounting - Edit 
EXEC	@return_value = [dbo].[hpf_hpf_user_insert]
		@pi_user_login_id = N'ALL', @pi_password = N'ALL', @pi_active_ind = N'Y',	@pi_user_role_str_TBD = NULL,
		@pi_fname = N'ALL',	@pi_lname = NULL,	@pi_email = NULL, @pi_phone = NULL, @pi_last_login_dt = @today,
		@pi_create_dt = @today, @pi_create_user_id = N'Installation', @pi_create_app_name = N'SQL Studio',
		@pi_chg_lst_dt = @today,	@pi_chg_lst_user_id = N'Installation', @pi_chg_lst_app_name = N'SQL Studio',
		@po_hpf_user_id = @po_hpf_user_id OUTPUT
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 17, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 1, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 2, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id
select @v_menu_security_id = @v_menu_security_id +1;
EXEC	@return_value = [dbo].[hpf_menu_security_insert]	
		@pi_menu_security_id = 	@v_menu_security_id , @pi_menu_item_id = 18, @pi_permission_value = 'U', @pi_hpf_user_id  = @po_hpf_user_id;



ALTER TABLE Agency ADD NFMC_branch_num varchar(30) Null;

UPDATE	Agency SET		NFMC_branch_num = '84096' WHERE	Agency_name = 'CCCS of Atlanta';
UPDATE	Agency SET		NFMC_branch_num = '84095' WHERE	Agency_name = 'CCCS of San Francisco';
UPDATE	Agency SET		NFMC_branch_num = '84289' WHERE	Agency_name = 'MMI';
UPDATE	Agency SET		NFMC_branch_num = '84094' WHERE	Agency_name = 'NovaDebt';
UPDATE	Agency SET		NFMC_branch_num = '84093' WHERE	Agency_name = 'Springboard';
UPDATE	Agency SET		NFMC_branch_num = '82210' WHERE	Agency_name = 'Auriton';
UPDATE	Agency SET		NFMC_branch_num = '84468' WHERE	Agency_name = 'By Design';
UPDATE	Agency SET		NFMC_branch_num = '84467' WHERE	Agency_name = 'CCCS Dallas';
UPDATE	Agency SET		NFMC_branch_num = '84466' WHERE	Agency_name = 'CCCS-Central Florida';
UPDATE	Agency SET		NFMC_branch_num = '84469' WHERE	Agency_name = 'Greenpath';     

ALTER TABLE case_loan ADD changed_acct_num VARCHAR(100) NULL;
ALTER TABLE case_loan ADD mortgage_program_cd varchar(15) NULL;
ALTER TABLE case_loan ADD freddie_servicer_num Varchar(30) null ;

DELETE FROM ref_code_item  WHERE ref_code_set_name = 'secure delivery method code' AND code= 'SEMAIL';
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)  VALUES('secure delivery method code','SECEMAIL', 'Secure Email','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

UPDATE foreclosure_case SET		duplicate_ind = 'N' WHERE	duplicate_ind IS NULL;
UPDATE foreclosure_case SET	prop_addr1 = 'TEMP DATA' WHERE prop_addr1 IS NULL;
UPDATE foreclosure_case SET prop_city = 'TEMP DATA' WHERE prop_city IS NULL;
UPDATE foreclosure_case SET prop_state_cd = 'AK' WHERE prop_state_cd IS NULL;
UPDATE foreclosure_case SET prop_zip = '00000' WHERE prop_zip IS NULL;

ALTER TABLE foreclosure_case ALTER COLUMN duplicate_ind varchar(1) NOT NULL;
ALTER TABLE foreclosure_case ALTER COLUMN prop_addr1 varchar(50) NOT NULL;
ALTER TABLE foreclosure_case ALTER COLUMN prop_city varchar(30) NOT NULL;
ALTER TABLE foreclosure_case ALTER COLUMN prop_state_cd varchar(15) NOT NULL;
ALTER TABLE foreclosure_case ALTER COLUMN prop_zip varchar(5) NOT NULL;


UPDATE call set selected_agency_id = 2 WHERE selected_agency_id IN ('1', 'SelectedAgencyId')
UPDATE call set selected_agency_id = NULL WHERE selected_agency_id = ' ';
UPDATE call set selected_agency_id = NULL WHERE selected_agency_id <> '2';
ALTER TABLE call ALTER COLUMN selected_agency_id INT NULL;
ALTER TABLE call ADD CONSTRAINT agency_FK1 FOREIGN KEY (selected_agency_id) REFERENCES agency(agency_id);

DELETE FROM ref_code_item WHERE ref_code_set_name = 'term length code';
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','1', '1','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','3','3','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','5','5','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','7','7','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','10','10','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','15','15','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','20','20','',7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','30','30','',8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','40','40','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','UNK','Unknown','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('term length code','OTHER', 'Other','',11,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
 
INSERT INTO ref_code_set (ref_code_set_name,code_set_comment) VALUES ('mortgage program code','Mortgage program code');

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage program code','FHA', 'FHA - Federal Housing Administration Loan','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage program code','VA', 'VA - Veterans Administration Loan','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage program code','CONV', 'Conventional Loan','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage program code','PRIV', 'Privately Held Loan','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage program code','UNK', 'Unknown Loan Program','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

DELETE FROM ref_code_item WHERE ref_code_set_name = 'mortgage type code';
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage type code','ARM','Adjustable Rate Mortgage','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage type code','FIXED','Fixed Rate Mortgage','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage type code','POA','Payment Option ARM','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage type code','INTONLY','Interest Only','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage type code','HYBARM','Hybrid Arm','',5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('mortgage type code','UNK','Homeowner does not know','',6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

UPDATE ref_code_item SET code = 'HPFSTD' WHERE ref_code_set_name = 'export format code' AND code = 'STDSERV';

ALTER TABLE Invoice_payment ADD invoice_payment_comment varchar(300) null;
ALTER TABLE Invoice_payment ADD payment_file varchar(100) null;

DELETE ref_code_item WHERE ref_code_set_name = 'takeback reason code';
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)  
VALUES('takeback reason code','DUPE', 'Servicer Reported Dupe','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)  
VALUES('takeback reason code','UTL', 'Unable to locate loan in portfolio','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)  
VALUES('takeback reason code','FREDDUPE', 'FreddieMac Reported Dupe','',3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)  
VALUES('takeback reason code','NFMCDUPEIN', 'NFMC Reported Dupe In-Network','',4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

DELETE ref_code_item WHERE ref_code_set_name = 'payment reject reason code';
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'UTL', 'Unable to locate loan in portfolio','', 1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'DOCS', 'Invalid counseling documentation','', 2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'PIF', 'Paid in full prior to counseling','', 3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'BK', 'Bankruptcy','', 4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'REO', 'REO','', 5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'FCL', 'Foreclosure sale before counseling','', 6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'FRED', 'Freddie Mac loan','', 7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code', 'DUPE', 'Previously paid/Duplicate','', 8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code','FREDDUP','Freddie Previously paid/Duplicate','',9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code','NFMCDUPEIN','NFMC Previously Paid/Duplicate In-Network','',10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('payment reject reason code','NFMCDUPEOUT','NFMC Previously Paid/Duplicate Out-of-Network','',11,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

UPDATE invoice SET invoice_pmt_amt = NULL;
UPDATE invoice_case SET invoice_case_pmt_amt = NULL, invoice_payment_id = NULL;
Delete from invoice_payment;

