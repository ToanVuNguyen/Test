-- =============================================
-- Create date: 19 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 22 Feb 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

ALTER TABLE menu_security ADD hpf_user_id INTEGER NULL;

UPDATE	menu_security SET		hpf_user_id = ccrc_user_id;

ALTER TABLE menu_security DROP COLUMN ccrc_user_id;
ALTER TABLE menu_security  ADD CONSTRAINT hpf_user_FK1 FOREIGN KEY(hpf_user_id) REFERENCES hpf_user(hpf_user_id) ;
ALTER TABLE menu_security ALTER COLUMN hpf_user_id INTEGER NOT NULL;

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


UPDATE call set selected_agency_id = NULL WHERE selected_agency_id <> '2';
ALTER TABLE call ALTER COLUMN selected_agency_id INT NULL;
ALTER TABLE call ADD CONSTRAINT agency_FK1 FOREIGN KEY (selected_agency_id) REFERENCES agency(agency_id);

ALTER TABLE Invoice_payment ADD invoice_payment_comment varchar(300) null;
ALTER TABLE Invoice_payment ADD payment_file varchar(100) null;

ALTER TABLE area_median_income ADD median_income INTEGER NULL;
UPDATE area_median_income SET median_income = median2008 ;
ALTER TABLE area_median_income DROP COLUMN median2008 ;
ALTER TABLE area_median_income ALTER COLUMN metro_area_name varchar(100) NULL;
ALTER TABLE area_median_income ALTER COLUMN county_name varchar(50) NULL;
ALTER TABLE area_median_income ALTER COLUMN county_town_name varchar(50) NULL;

DELETE from case_loan where loan_1st_2nd_cd is null;
ALTER TABLE Case_loan ALTER COLUMN loan_1st_2nd_cd VARCHAR(15) NOT NULL;
