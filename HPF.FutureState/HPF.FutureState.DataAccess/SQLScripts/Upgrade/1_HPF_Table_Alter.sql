-- =============================================
-- Create date: 16 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 15 Jan 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

ALTER TABLE agency_payable_case 
ADD NFMC_difference_eligible_ind varchar(1) Not NULL;
ALTER TABLE agency_payable_case 
ALTER COLUMN NFMC_difference_paid_ind varchar(1) NOT NULL;

IF EXISTS (SELECT name from sys.indexes
             WHERE name = N'ws_user_login_username_UK')
    DROP INDEX ws_user_login_username_UK ON ws_user;
GO
CREATE UNIQUE INDEX ws_user_login_username_UK
    ON ws_user(login_username);
GO

ALTER TABLE foreclosure_case DROP COLUMN case_complete_ind;
ALTER TABLE foreclosure_case ADD agency_media_interest_ind VARCHAR(1) NULL;

DISABLE TRIGGER trg_foreclosure_case_update_audit ON foreclosure_case;
DISABLE TRIGGER trg_foreclosure_case_update ON foreclosure_case;
UPDATE foreclosure_case SET agency_media_interest_ind = agency_media_consent_ind ;
ENABLE TRIGGER trg_foreclosure_case_update_audit ON foreclosure_case;
ENABLE TRIGGER trg_foreclosure_case_update ON foreclosure_case;

ALTER TABLE foreclosure_case DROP COLUMN agency_media_consent_ind;
ALTER TABLE foreclosure_case DROP COLUMN hpf_network_candidate_ind;

ALTER TABLE agency_payable DROP COLUMN pmt_cd;
ALTER TABLE invoice DROP COLUMN invoice_cd; 

CREATE TABLE hpf_user (
  hpf_user_id INTEGER  NOT NULL   IDENTITY ,
  user_login_id VARCHAR(30)    ,
  password varchar(128) NULL ,
  active_ind VARCHAR(1)    ,
  user_role_str_TBD VARCHAR(30)    ,
  fname VARCHAR(30)    ,
  lname VARCHAR(30)    ,
  email VARCHAR(50)    ,
  phone VARCHAR(20)    ,
  last_login_dt DATETIME    ,
  create_dt DATETIME  NOT NULL  ,
  create_user_id VARCHAR(30)  NOT NULL  ,
  create_app_name VARCHAR(20)  NOT NULL  ,
  chg_lst_dt DATETIME  NOT NULL  ,
  chg_lst_user_id VARCHAR(30)  NOT NULL  ,
  chg_lst_app_name VARCHAR(20)  NOT NULL    ,
PRIMARY KEY(hpf_user_id));
GO

INSERT INTO HPF_user( user_login_id,active_ind,user_role_str_TBD,fname,lname,email,phone,last_login_dt,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)
SELECT user_login_id,active_ind,user_role_str_TBD,fname,lname,email,phone,last_login_dt,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name
FROM CCRC_USER;

DROP TABLE CCRC_USER;

ALTER TABLE MENU_ITEM ADD Visibled bit NULL;

-- "Add ref_code_item Marital Status, Payment code , Payment Reject Reason Code, Never Bill Reason Code, Never Pay Reason Code, Agency Payable Status, Invoice Status, Final Disposition"

/****** Object:  Index [IX_foreclosure_case_Complete_dt_Agency_id]    Script Date: 01/16/2009 14:01:05 ******/
DROP INDEX [IX_foreclosure_case_Complete_dt_Agency_id] ON [dbo].[foreclosure_case] WITH ( ONLINE = OFF )
GO
USE [hpf]
GO
CREATE NONCLUSTERED INDEX [IX_foreclosure_case_Complete_dt_Agency_id] ON [dbo].[foreclosure_case] 
(
	[completed_dt] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
