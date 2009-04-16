-- =============================================
-- Create date: 15 Apr 2009
-- Project : HPF 
-- Build 
-- Description:	Create indexes are being used in HPF
-- =============================================
USE [hpf]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'Invoice_FKIndex1')
	DROP INDEX [Invoice_FKIndex1] ON [dbo].[Invoice] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[foreclosure_case]') AND name = N'IX_foreclosure_case_Complete_dt_Agency_id')
	DROP INDEX [IX_foreclosure_case_Complete_dt_Agency_id] ON [dbo].[foreclosure_case] WITH ( ONLINE = OFF )
GO

IF EXISTS (SELECT name from sys.indexes WHERE name = N'ws_user_login_username_UK')
    DROP INDEX ws_user_login_username_UK ON ws_user;
GO
CREATE UNIQUE INDEX ws_user_login_username_UK ON ws_user(login_username);
GO


---- Indexes for initial data ---
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ref_code_item]') AND name = N'IX_ref_code_item_01')
	DROP INDEX [IX_ref_code_item_01] ON [dbo].[ref_code_item] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_ref_code_item_01] ON [dbo].[ref_code_item] 
(
	[ref_code_set_name] ASC
)
INCLUDE ( [code],[code_desc]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Area_Median_Income]') AND name = N'IX_Area_Median_Income_01')
	DROP INDEX [IX_Area_Median_Income_01] ON [dbo].[Area_Median_Income] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_Area_Median_Income_01] 
ON [dbo].[Area_Median_Income] 
(
	[median_income] DESC
)
INCLUDE ( [fips],[msa]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) 
ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[geocode_ref]') AND name = N'IX_geocode_ref_01')
	DROP INDEX [IX_geocode_ref_01] ON [dbo].[geocode_ref] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_geocode_ref_01] 
ON [dbo].[geocode_ref] 
(
	[zip_code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) 
ON [PRIMARY]
GO

---- Indexes for New Agency Payable ---
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[foreclosure_case]') AND name = N'IX_foreclosure_case_01')
	DROP INDEX [IX_foreclosure_case_01] ON [dbo].[foreclosure_case] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_foreclosure_case_01] ON [dbo].[foreclosure_case] 
(
	[never_pay_reason_cd] ASC,
	[agency_id] ASC,
	[program_id] ASC,
	[fc_id] ASC
)
INCLUDE ( [agency_case_num],
[borrower_fname],
[borrower_lname],
[completed_dt],
[funding_consent_ind],
[servicer_consent_ind],
[create_dt]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

---- Indexes for New invoice ---
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[foreclosure_case]') AND name = N'IX_foreclosure_case_02')
	DROP INDEX [IX_foreclosure_case_02] ON [dbo].[foreclosure_case] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_foreclosure_case_02] ON [dbo].[foreclosure_case] 
(
	[duplicate_ind] ASC,
	[funding_consent_ind] ASC,
	[never_bill_reason_cd] ASC,
	[create_dt] ASC,
	[completed_dt] ASC,
	[fc_id] ASC,
	[program_id] ASC
)
INCLUDE ( [agency_case_num],
[intake_dt],
[borrower_fname],
[borrower_lname]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND name = N'IX_Invoice_01')
	DROP INDEX [IX_Invoice_01] ON [dbo].[Invoice] WITH ( ONLINE = OFF )
GO
CREATE INDEX IX_Invoice_01 ON Invoice (funding_source_id ASC)
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[servicer]') AND name = N'IX_servicer_01')
	DROP INDEX [IX_servicer_01] ON [dbo].[servicer] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_servicer_01] ON [dbo].[servicer] 
(
	[funding_agreement_ind] ASC,
	[servicer_id] ASC
)
INCLUDE ( [servicer_name]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

---- Indexes for Web Service ---------
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[foreclosure_case]') AND name = N'IX_foreclosure_case_03')
	DROP INDEX [IX_foreclosure_case_03] ON [dbo].[foreclosure_case] WITH ( ONLINE = OFF )
GO
-- index for hpf_foreclosure_case_search_ws
CREATE NONCLUSTERED INDEX [IX_foreclosure_case_03] ON [dbo].[foreclosure_case] 
(
	[duplicate_ind] ASC,
	[agency_case_num] ASC,
	[prop_zip] ASC,
	[borrower_last4_SSN] ASC,
	[co_borrower_last4_SSN] ASC,
	[borrower_fname] ASC,
	[co_borrower_fname] ASC,
	[borrower_lname] ASC,
	[co_borrower_lname] ASC,
	[fc_id] ASC,
	[agency_id] ASC
)INCLUDE 
([intake_dt],
[prop_addr1],
[prop_addr2],
[prop_city],
[prop_state_cd],
[bankruptcy_ind],
[fc_notice_received_ind],
[completed_dt],
[counselor_lname],
[counselor_fname],
[counselor_email],
[counselor_phone],
[counselor_ext],
[loan_list]
) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) 
ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[case_loan]') AND name = N'IX_case_loan_01')
	DROP INDEX [IX_case_loan_01] ON [dbo].[case_loan] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_case_loan_01] ON [dbo].[case_loan] 
(
	[fc_id] ASC,
	[loan_1st_2nd_cd] ASC,
	[servicer_id] ASC
)
INCLUDE ( [acct_num]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[case_loan]') AND name = N'IX_case_loan_02')
	DROP INDEX [IX_case_loan_02] ON [dbo].[case_loan] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_case_loan_02] ON [dbo].[case_loan] 
(
	[acct_num] ASC,
	[servicer_id] ASC,
	[fc_id] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[outcome_item]') AND name = N'IX_outcome_item_01')
	DROP INDEX [IX_outcome_item_01] ON [dbo].[outcome_item] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_outcome_item_01] ON [dbo].[outcome_item] 
(
	[fc_id] ASC,
	[outcome_dt] ASC,
	[outcome_deleted_dt] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[outcome_item]') AND name = N'IX_outcome_item_02')
	DROP INDEX [IX_outcome_item_02] ON [dbo].[outcome_item] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_outcome_item_02] ON [dbo].[outcome_item] 
(
	[fc_id] ASC,
	[outcome_dt] ASC,
	[outcome_type_id] ASC
)
INCLUDE ( [outcome_item_id]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[budget_set]') AND name = N'IX_budget_set_01')
	DROP INDEX [IX_budget_set_01] ON [dbo].[budget_set] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_budget_set_01] ON [dbo].[budget_set] 
(
	[fc_id] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[budget_item]') AND name = N'IX_budget_item_01')
	DROP INDEX [IX_budget_item_01] ON [dbo].[budget_item] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IX_budget_item_01] ON [dbo].[budget_item] 
(
	[budget_set_id] ASC
)
INCLUDE ( [budget_item_id],
[budget_subcategory_id],
[budget_item_amt],
[budget_note]
) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO