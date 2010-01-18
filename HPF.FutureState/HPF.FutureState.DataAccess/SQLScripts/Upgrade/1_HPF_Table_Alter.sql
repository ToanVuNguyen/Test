-- =============================================
-- Create date: 19 Jan 2010
-- Project : HPF 
-- Build 
-- Description:	
-- =============================================
USE HPF
GO

CREATE TABLE mha_help
(	borrower_fname		varchar(30)
	,borrower_lname		varchar(30)
	,servicer			varchar(50)
	,servicer_id		int
	,acct_num			varchar(30)
	,counselor_name		varchar(60)
	,counselor_email	varchar(50)
	,call_src			varchar(50)
	,voicemail_dt		datetime
	,mha_help_reason	varchar(100)
	,comments			varchar(3000)
	,privacy_consent	varchar(10)
	,borrower_in_trial_mod	varchar(10)
	,trial_mod_before_sept1	varchar(10)
	,current_on_payments	varchar(10)
	,wage_earner		varchar(10)
	,two_paystubs_sent	varchar(10)
	,all_docs_submitted	varchar(10)
	,list_of_docs_submitted	varchar(500)
	,borrower_phone		varchar(50)
	,best_time_to_reach	varchar(30)
	,borrower_email		varchar(60)
	,address			varchar(3000)
	,city				varchar(255)
	,state				varchar(2)
	,zip				varchar(5)
	,mha_help_resolution	varchar(200)
	,item_created_dt	datetime
	,item_created_user	varchar(50)
	,item_modified_dt	datetime
	,item_modified_user	varchar(50)
	,created_dt			datetime
	,created_user_id		varchar(30)
	,created_app_name	varchar(20)
	,chg_lst_dt			datetime
	,chg_lst_user_id	varchar(30)
	,chg_lst_app_name	varchar(20)
);
