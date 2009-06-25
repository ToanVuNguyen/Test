-- =============================================
-- Create date: 26 Jun 2009
-- Project : HPF 
-- Build 
-- Description:	Create stored procedures, functions are being used in HPF Reports
-- =============================================
USE [hpf]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyActivityReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_DailyActivityReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyCompletionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_DailyCompletionReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HUD_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HUD_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_IncompletedCounselingCases]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_IncompletedCounselingCases]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PotentialDuplicates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PotentialDuplicates]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceSummary_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableSummary_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PayableSummary_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HSBC_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HSBC_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_FIS_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_FIS_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableExportFile_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PayableExportFile_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_UnbilledCaseReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Hpf_rpt_UnbilledCaseReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_CompletedCounselingSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Hpf_rpt_CompletedCounselingSummary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_AgencyPaymentCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_AgencyPaymentCheck]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCounselingByServicer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CompletedCounselingByServicer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HPFStandard_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HPFStandard_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_ExternalReferrals]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Hpf_rpt_ExternalReferrals]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Outcome]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Outcome]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Outcome]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Outcome]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CaseFundingSourceSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CaseFundingSourceSummary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableSummary_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PayableSummary_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Budget]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Budget]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceSummary_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCasesByState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CompletedCasesByState]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableExportFile_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PayableExportFile_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CaseSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CaseSource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget_asset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget_asset]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_FIS_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_FIS_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Budget_asset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Budget_asset]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyAverageCompletionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_DailyAverageCompletionReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyCompletionByServicerReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_DailyCompletionByServicerReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_NFMC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_NFMC]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCounselingDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CompletedCounselingDetail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_replace_non_numeric_char]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[hpf_rpt_replace_non_numeric_char]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_get_dateRange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_get_dateRange]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_count_day]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_count_day]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_MHADailySummaryReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_MHADailySummaryReport]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_MHA_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_MHA_Report]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_MHA_Weekly_Executive_Summary_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_MHA_Weekly_Executive_Summary_Report]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_count_day]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	hpf_countDay; Count Week Day in a date range	
-- =============================================
CREATE procedure [dbo].[hpf_rpt_count_day]( @pi_day_type int, @pi_from_dt datetime, @pi_to_dt datetime, @po_count int OUTPUT )
as
BEGIN
CREATE table #duration (dt datetime, dt_day int);
while @pi_from_dt <= @pi_to_dt
Begin
	Insert into #duration values(@pi_from_dt, datepart(weekday, @pi_from_dt));
	SELECT @pi_from_dt = @pi_from_dt + 1;
end;
	-- If count Sunday
	if (@pi_day_type = 1)
		Select @po_count = count(*) from #duration where datepart(weekday, dt) = 1;	
	-- If count Monday to Friday
	if (@pi_day_type = 2)
		Select  @po_count = count(*) from #duration where datepart(weekday, dt) in (2,3,4,5,6);
	-- If count Saturday
	if (@pi_day_type = 7)
		Select  @po_count = count(*) from #duration where datepart(weekday, dt) = 7;	
	DROP table #duration;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_get_dateRange]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 08 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Hpf_getDateRange of Mon, quarter or year	
--				@pi_range_type = 1 --> Month
--				@pi_range_type = 2 --> Quarter
--				@pi_range_type = 3 --> Year					
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_get_dateRange] (@pi_range_type int, @pi_range_value int, @pi_year int, @po_start_dt datetime OUTPUT, @po_end_dt datetime OUTPUT)
AS
DECLARE @v_string varchar(20);
BEGIN
	-- IF  pi_range_type is MONTH
	IF (@pi_range_type = 1)
	BEGIN
		SELECT @v_string = cast(@pi_year as varchar(4))+ ''-''+ cast(@pi_range_value as varchar(4));
		SELECT @po_start_dt = cast(@v_string + ''-01'' as datetime);
		IF (@pi_range_value IN(1,3,5,7,8,10,12))
			SELECT @po_end_dt = cast(@v_string + ''-31'' as datetime);
		ELSE
			IF (@pi_range_value IN(4, 6, 9, 11))
				SELECT @po_end_dt = cast(@v_string + ''-30'' as datetime);
			ELSE
				IF (@pi_range_value = 2 AND @pi_year%4 = 0)
					SELECT @po_end_dt = cast(@v_string + ''-29'' as datetime);
				ELSE
					IF (@pi_range_value = 2 AND @pi_year%4 <> 0)
					SELECT @po_end_dt = cast(@v_string + ''-28'' as datetime);
	END;
-- IF  pi_range_type is QUARTER
	IF (@pi_range_type = 2)
	BEGIN	
		IF (@pi_range_value= 1)
		BEGIN
			SELECT	@po_start_dt = cast(cast(@pi_year as varchar(4))+ ''-01-01'' as datetime);
			SELECT	@po_end_dt = cast(cast(@pi_year as varchar(4))+ ''-03-31'' as datetime);
		END
		ELSE
			IF (@pi_range_value= 2)
			BEGIN
				SELECT	@po_start_dt = cast(cast(@pi_year as varchar(4))+ ''-04-01'' as datetime);
				SELECT	@po_end_dt = cast(cast(@pi_year as varchar(4))+ ''-06-30'' as datetime);
			END
			ELSE
				IF (@pi_range_value= 3)
				BEGIN
					SELECT	@po_start_dt = cast(cast(@pi_year as varchar(4))+ ''-07-01'' as datetime);
					SELECT	@po_end_dt = cast(cast(@pi_year as varchar(4))+ ''-09-30'' as datetime);
				END
				ELSE
					IF (@pi_range_value= 4)
					BEGIN
						SELECT	@po_start_dt = cast(cast(@pi_year as varchar(4))+ ''-10-01'' as datetime);
						SELECT	@po_end_dt = cast(cast(@pi_year as varchar(4))+ ''-12-31'' as datetime);
					END;
	END;
-- IF  pi_range_type is YEAR
	IF (@pi_range_type = 3)
	BEGIN
		SELECT	@po_start_dt = cast(cast(@pi_year as varchar(4))+ ''-01-01'' as datetime);
		SELECT	@po_end_dt = cast(cast(@pi_year as varchar(4))+ ''-12-31'' as datetime);
	END
END;

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_NFMC]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 27 Feb 2009
-- Description:	HPF REPORT - R58 - Invoice Export File - NFMC
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_NFMC] 
	(@pi_invoice_id integer)
AS
CREATE TABLE #invoiceNFMC
(	NFMC_branch_num varchar(30)
	, fc_id			int
	, intake_dt		datetime
	, borrower_fname	varchar(30)
	, borrower_lname	varchar(30)
	, borrower_dob		datetime
	, race_cd			varchar(15)
	, hispanic_ind		varchar(1)
	, gender_cd			varchar(15)
	, household_cd		varchar(15)
	, household_gross_annual_income_amt	numeric(15,2)
	, ami_percentage	numeric(15,2)
	, prop_addr1		varchar(50)
	, prop_addr2		varchar(50)
	, prop_city			varchar(30)
	, prop_state_cd		varchar(15)
	, prop_zip			varchar(20)
	, counseling_duration_cd	varchar(15)
	, orig_lender_name	varchar(50)
	, orig_mortgage_co	varchar(50)
	, orig_loan_num		varchar(30)	
	, servicer_name		varchar(50)
	, current_servicer	varchar(50)
	, acct_num			varchar(30)
	, intake_credit_score	varchar(4)	
	, whyNoCreditScore	varchar(1)
	, intake_credit_bureau_cd	varchar(15)	
	, budget_item_amt	numeric(15,2)
	, has2ndLoan		varchar(1)
	, loanProductType	varchar(30)
	, interestOnly		varchar(1)
	, hybrid			varchar(1)
	, optionARM			varchar(1)
	, VAorHFAInsured	varchar(1)
	, privatelyHeld		varchar(1)
	, ARMreset			varchar(1)
	, dflt_reason_1st_cd 	varchar(15)
	, loan_delinq_status_cd varchar(15)
	, counseling_outcome_cd 	int	
	, outcome_dt		datetime		
	, outcome_item_id	int
	, outcome_type_id	int
	, mortgage_program_cd varchar(15)
)
BEGIN
	INSERT INTO #invoiceNFMC 
		(NFMC_branch_num , fc_id, intake_dt		
		, borrower_fname, borrower_lname, borrower_dob		
		, race_cd, hispanic_ind	, gender_cd, household_cd, household_gross_annual_income_amt, ami_percentage
		, prop_addr1, prop_addr2, prop_city, prop_state_cd, prop_zip			
		, counseling_duration_cd	
		, orig_lender_name, orig_mortgage_co, orig_loan_num		
		, servicer_name, current_servicer, acct_num			
		, intake_credit_score, whyNoCreditScore, intake_credit_bureau_cd	
		, budget_item_amt	
		, loanProductType
		, interestOnly, hybrid, optionARM, VAorHFAInsured, privatelyHeld, ARMreset			
		, dflt_reason_1st_cd 	
		, loan_delinq_status_cd 
		, mortgage_program_cd
		)
	SELECT	a.NFMC_branch_num, f.fc_id, f.intake_dt
			, f.borrower_fname, f.borrower_lname, f.borrower_dob		
			, case when cast(race_cd as int) <=10 then cast(race_cd as int) - 1 ELSE NULL END Race_cd 
			, case	when f.hispanic_ind= ''Y'' then 1 
					when f.hispanic_ind= ''N'' then 0 
					when f.hispanic_ind IS NULL then 2 ELSE NULL END hispanic_ind
			, case	when f.gender_cd= ''FEMALE'' then 0
					when f.gender_cd= ''MALE'' then 1 ELSE NULL END gender_cd
			, case	when f.household_cd = ''SINGL'' then 1
					when f.household_cd = ''FHOH'' then 2
					when f.household_cd = ''MHOH'' then 3
					when f.household_cd = ''MNODEP'' then 4
					when f.household_cd = ''MWDEP'' then 5
					when f.household_cd = ''MADULT'' then 6
					when f.household_cd = ''OTHER'' then 7 ELSE NULL END household_cd
			, f.household_gross_annual_income_amt, f.ami_percentage
			, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip + isnull(''-'' + f.prop_zip_plus_4, '''') as prop_zip
			, case	when f.counseling_duration_cd = ''<30'' then 0.5
					when f.counseling_duration_cd = ''30-59'' then 1
					when f.counseling_duration_cd = ''60-89'' then 1.5
					when f.counseling_duration_cd = ''90-120'' then 2
					when f.counseling_duration_cd = ''>121'' then 3 ELSE NULL END counseling_duration_cd	
			, l.originating_lender_name orig_lender_name
			, isnull(l.orig_mortgage_co_FDIC_NCUA_num, orig_mortgage_co_name) orig_mortgage_co
			, isnull(l.orginal_loan_num, '''') orig_loan_num		
			, s.servicer_name
			, isnull(l.current_servicer_FDIC_NCUA_num, current_servicer_FDIC_NCUA_num) current_servicer
			, l.acct_num			
			, f.intake_credit_score
			, case	when f.intake_credit_score IS NULL then ''3'' ELSE '''' END whyNoCreditScore
			, case	when f.intake_credit_bureau_cd = ''TU'' then ''TransUnion''
					when f.intake_credit_bureau_cd = ''EQUIFAX'' then ''Equifax''
					when f.intake_credit_bureau_cd = ''EXPERION'' then ''Experion''
					when f.intake_credit_bureau_cd = ''TRI-MERGE'' then ''Tri-merge''
					ELSE NULL END intake_credit_bureau_cd	
	, bi.budget_item_amt	
	, case	when (l.mortgage_type_cd = ''FIXED'' AND f.has_workout_plan_ind = ''N'' AND l.interest_rate < 8 ) then 1
			when (l.mortgage_type_cd = ''FIXED'' AND f.has_workout_plan_ind = ''N'' AND l.interest_rate >=8 ) then 2
			when (l.mortgage_type_cd IN (''ARM'', ''POA'', ''INTONLY'', ''HYBARM'') AND f.has_workout_plan_ind = ''N'' AND l.interest_rate < 8 ) then 3
			when (l.mortgage_type_cd IN (''ARM'', ''POA'', ''INTONLY'', ''HYBARM'') AND f.has_workout_plan_ind = ''N'' AND l.interest_rate >= 8 ) then 4
			when (l.mortgage_type_cd = ''FIXED'' AND f.has_workout_plan_ind = ''Y'' AND l.interest_rate < 8 ) then 7
			when (l.mortgage_type_cd = ''FIXED'' AND f.has_workout_plan_ind = ''Y'' AND l.interest_rate >=8 ) then 8
			when (l.mortgage_type_cd IN (''ARM'', ''POA'', ''INTONLY'', ''HYBARM'') AND f.has_workout_plan_ind = ''Y'' AND l.interest_rate < 8 ) then 9
			when (l.mortgage_type_cd IN (''ARM'', ''POA'', ''INTONLY'', ''HYBARM'') AND f.has_workout_plan_ind = ''Y'' AND l.interest_rate >= 8 ) then 10
			when (l.mortgage_type_cd = ''UNK'') then 11 
			ELSE NULL END loanProductType
	, case	when l.mortgage_type_cd = ''INTONLY'' then 1 ELSE 0 END interestOnly
	, case	when l.mortgage_type_cd = ''HYBARM'' then 1 ELSE 0 END hybrid
	, case	when l.mortgage_type_cd = ''POA'' then 1 ELSE 0 END optionARM
	, case	when l.mortgage_program_cd IN (''FHA'', ''VA'') then 1 ELSE 0 END VAorHFAInsured
	, case	when l.mortgage_program_cd = ''PRIV'' then 1 ELSE 0 END privatelyHeld
	, case	when (l.mortgage_type_cd IN (''ARM'', ''POA'', ''HYBARM'', ''INTONLY'') AND l.arm_reset_ind = ''Y'')then 1 
			when (l.mortgage_type_cd IN (''ARM'', ''POA'', ''HYBARM'', ''INTONLY'') AND l.arm_reset_ind = ''N'')then 0 ELSE NULL END ARMreset	
	, case	when f.dflt_reason_1st_cd = ''1'' then 7
			when f.dflt_reason_1st_cd = ''2'' then 4
			when f.dflt_reason_1st_cd = ''3'' then 4
			when f.dflt_reason_1st_cd = ''4'' then 7
			when f.dflt_reason_1st_cd = ''5'' then 6
			when f.dflt_reason_1st_cd = ''6'' then 1
			when f.dflt_reason_1st_cd = ''7'' then 2
			when f.dflt_reason_1st_cd = ''8'' then 10
			when f.dflt_reason_1st_cd = ''9'' then 10
			when f.dflt_reason_1st_cd = ''10'' then 10
			when f.dflt_reason_1st_cd = ''11'' then 1
			when f.dflt_reason_1st_cd = ''12'' then 10
			when f.dflt_reason_1st_cd = ''13'' then 3
			when f.dflt_reason_1st_cd = ''14'' then 8
			when f.dflt_reason_1st_cd = ''15'' then 1
			when f.dflt_reason_1st_cd = ''16'' then 10
			when f.dflt_reason_1st_cd = ''17'' then 5
			when f.dflt_reason_1st_cd = ''18'' then 10
			when f.dflt_reason_1st_cd = ''19'' then 9
			when f.dflt_reason_1st_cd = ''20'' then 10
			when f.dflt_reason_1st_cd = ''21'' then 10
			when f.dflt_reason_1st_cd = ''22'' then 10
			when f.dflt_reason_1st_cd = ''23'' then 10
			when f.dflt_reason_1st_cd = ''24'' then 10
			when f.dflt_reason_1st_cd = ''25'' then 10 ELSE NULL END dflt_reason_1st_cd
	, case	when l.loan_delinq_status_cd = ''CUR'' then 1
			when l.loan_delinq_status_cd = ''<30'' then 1
			when l.loan_delinq_status_cd = ''30-59'' then 2
			when l.loan_delinq_status_cd = ''60-89'' then 3
			when l.loan_delinq_status_cd = ''90-119'' then 4
			when l.loan_delinq_status_cd = ''120+'' then 5
			when l.loan_delinq_status_cd = ''UNK'' then 4 ELSE NULL END loan_delinq_status_cd
	, l.mortgage_program_cd
	FROM	foreclosure_case f
			LEFT OUTER JOIN (SELECT bs1.fc_id, bi1.budget_item_amt FROM budget_item bi1, budget_set bs1
						WHERE	bi1.budget_set_id = bs1.budget_set_id									
								AND bi1.budget_subcategory_id = 8
								AND bi1.budget_set_id IN (SELECT max(budget_set_id) FROM budget_set bs2, invoice_case ic2 
													WHERE ic2.fc_id = bs2.fc_id AND ic2.invoice_id = @pi_invoice_id GROUP BY bs2.fc_id)
							) bi ON bi.fc_id = f.fc_id
			, agency a, invoice_case ic, case_loan l, servicer s
	WHERE	f.agency_id = a.agency_id AND f.fc_id = ic.fc_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id 
			AND l.loan_1st_2nd_cd = ''1ST''
			AND ic.invoice_id = @pi_invoice_id;

	-- Calculate Has2ndLoan
	UPDATE	#invoiceNFMC
	SET		has2ndLoan = ''1''
	FROM	#invoiceNFMC, case_loan l
	WHERE	#invoiceNFMC.fc_id = l.fc_id AND l.loan_1st_2nd_cd = ''2ND'';

	UPDATE	#invoiceNFMC SET	has2ndLoan = ''0'' WHERE	has2ndLoan IS NULL;

	-- Calculate Outcome_dt	
	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.outcome_item_id= o.outcome_item_id
			, #invoiceNFMC.outcome_dt = o.outcome_dt
			, #invoiceNFMC.outcome_type_id = o.outcome_type_id
	FROM	#invoiceNFMC,
			(SELECT	min(oi.outcome_item_id) outcome_item_id, oi.outcome_dt, oi.outcome_type_id, oi.fc_id
			FROM	outcome_item oi, outcome_type ot, #invoiceNFMC
			WHERE	oi.outcome_type_id = ot.outcome_type_id
					AND #invoiceNFMC.fc_id = oi.fc_id 
					AND ot.payable_ind = ''Y''
			GROUP BY oi.outcome_dt, oi.outcome_type_id, oi.fc_id
			) o
	WHERE #invoiceNFMC.fc_id = o.fc_id ;

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.outcome_item_id= o.outcome_item_id
			, #invoiceNFMC.outcome_dt = o.outcome_dt
			, #invoiceNFMC.outcome_type_id = o.outcome_type_id
	FROM	#invoiceNFMC,
			(SELECT	min(oi.outcome_item_id) outcome_item_id, oi.outcome_dt, oi.outcome_type_id, oi.fc_id
			FROM	outcome_item oi, outcome_type ot, #invoiceNFMC
			WHERE	oi.outcome_type_id = ot.outcome_type_id
					AND #invoiceNFMC.fc_id = oi.fc_id 
					AND ot.payable_ind = ''N''
					AND #invoiceNFMC.outcome_dt IS NULL
			GROUP BY oi.outcome_dt, oi.outcome_type_id, oi.fc_id
			) o
	WHERE #invoiceNFMC.fc_id = o.fc_id AND #invoiceNFMC.outcome_dt IS NULL;

	-- Calculate counseling_outcome_cd
	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 2
	WHERE	#invoiceNFMC.outcome_type_id IN (6, 8, 27, 23);

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 3
	WHERE	#invoiceNFMC.outcome_type_id IN (21)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 20
	WHERE	#invoiceNFMC.outcome_type_id IN (2, 3, 16, 17, 25, 26, 28)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 52
	WHERE	#invoiceNFMC.outcome_type_id IN (1, 12, 14, 15)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 54
	WHERE	#invoiceNFMC.outcome_type_id IN (18)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 56
	WHERE	#invoiceNFMC.outcome_type_id IN (29)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 57
	WHERE	#invoiceNFMC.outcome_type_id IN (9, 10, 11)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 100
	WHERE	#invoiceNFMC.outcome_type_id IN (24)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 104
	WHERE	#invoiceNFMC.outcome_type_id IN (13)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 105
	WHERE	#invoiceNFMC.outcome_type_id IN (30) AND mortgage_program_cd = ''FHA'';

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 106
	WHERE	#invoiceNFMC.outcome_type_id IN (30) AND (mortgage_program_cd <> ''FHA'' OR mortgage_program_cd IS NULL);

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 107
	WHERE	#invoiceNFMC.outcome_type_id IN (22)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 110
	WHERE	#invoiceNFMC.outcome_type_id IN (7)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 111
	WHERE	#invoiceNFMC.outcome_type_id IN (20)

	UPDATE	#invoiceNFMC
	SET		#invoiceNFMC.counseling_outcome_cd = 112
	WHERE	#invoiceNFMC.outcome_type_id IN (5)

	SELECT	NFMC_branch_num , fc_id, intake_dt, borrower_fname, borrower_lname, borrower_dob
			, race_cd, hispanic_ind, gender_cd
			, household_cd, cast(household_gross_annual_income_amt as numeric(18)) household_gross_annual_income_amt, ami_percentage	
			, prop_addr1, prop_addr2, prop_city, prop_state_cd, prop_zip, counseling_duration_cd	
			, orig_lender_name	, orig_mortgage_co	, orig_loan_num		
			, servicer_name		, current_servicer	, acct_num			
			, intake_credit_score	, whyNoCreditScore	, intake_credit_bureau_cd	
			, budget_item_amt, has2ndLoan		
			, loanProductType	
			, interestOnly, hybrid, optionARM, VAorHFAInsured, privatelyHeld, ARMreset			
			, dflt_reason_1st_cd , loan_delinq_status_cd 
			, counseling_outcome_cd , outcome_dt
	FROM	#invoiceNFMC;
	DROP TABLE #invoiceNFMC;	
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCounselingDetail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 13 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	hpf_rpt_CompletedCounselingDetail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CompletedCounselingDetail] 
	(@pi_agency_id int, @pi_program_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
CREATE TABLE #counselingDetail
( fc_id  INT 
, agency_name  varchar(50)
, completed_dt  datetime
, call_id		INT
, program_name  varchar(50)       
, agency_case_num varchar(30)
, agency_client_num varchar(30)
, intake_dt		varchar(20)
, income_earners_cd varchar(80)
, case_source_cd varchar(80)
, race_cd varchar(80)
, household_cd varchar(80)
, dflt_reason_1st_cd varchar(80)
, dflt_reason_2nd_cd varchar(80)
, hud_termination_reason_cd varchar(80)
, hud_termination_dt  varchar(20)       
, hud_outcome_cd varchar(80)
, counseling_duration_cd varchar(80)
, gender_cd varchar(80)
, borrower_fname varchar(30)
, borrower_lname varchar(30)
, mother_maiden_lname varchar(30)
, borrower_last4_SSN varchar(4)
, borrower_DOB  varchar(20)
, co_borrower_fname varchar(30)
, co_borrower_lname varchar(30)
, co_borrower_last4_SSN varchar(4)
, co_borrower_DOB  varchar(20)
, primary_contact_no varchar(20)
, second_contact_no varchar(20)
, email_1 varchar(50)
, prop_addr1 varchar(50)
, prop_addr2 varchar(50)
, prop_city varchar(30)
, prop_state_cd varchar(50)
, prop_zip varchar(5)
, bankruptcy_ind varchar(1)
, borrower_educ_level_completed_cd varchar(80)
, borrower_marital_status_cd varchar(80)
, hispanic_ind varchar(1)
, fc_notice_received_ind varchar(1)
, servicer_consent_ind varchar(1)
, borrower_disabled_ind varchar(1)
, summary_sent_other_cd varchar(80)
, summary_sent_other_dt  datetime
, summary_sent_dt  datetime
, occupant_num tinyint
, counselor_lname varchar(30)
, counselor_fname varchar(30)       
, owner_occupied_ind varchar(1)
, military_service_cd varchar(80)
, household_gross_annual_income_amt  varchar(30)
, intake_credit_score varchar(4)
, servicer_name varchar(50)
, acct_num varchar(30)
, mortgage_type_cd varchar(80)
, arm_reset_ind varchar(1)
, term_length_cd varchar(80)
, loan_delinq_status_cd varchar(80)
, current_loan_balance_amt  varchar(30)			
, interest_rate numeric(5, 3)

, outcome_1 varchar(100)
, outcome_2 varchar(100)
, outcome_3 varchar(100)
)
CREATE TABLE #selected_Foreclosure_case (fc_id INT);
CREATE TABLE #t_outcomes
(	fc_id		INT
	, outcome_type_id INT
	, outcome_type_name varchar(300)
	, rownum INT
);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
	IF (@pi_agency_id > 0 )
	BEGIN
		IF (@pi_program_id > 0)
			INSERT INTO #selected_Foreclosure_case
				SELECT	fc_id FROM foreclosure_case f
				WHERE	f.agency_id = @pi_agency_id AND program_id = @pi_program_id
						AND f.completed_dt IS NOT NULL AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
		ELSE
			INSERT INTO #selected_Foreclosure_case
				SELECT	fc_id FROM foreclosure_case f
				WHERE	f.agency_id = @pi_agency_id 
						AND f.completed_dt IS NOT NULL AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
	END
	ELSE
	BEGIN
		IF (@pi_program_id > 0)
			INSERT INTO #selected_Foreclosure_case
				SELECT	fc_id FROM foreclosure_case f
				WHERE	program_id = @pi_program_id
						AND f.completed_dt IS NOT NULL AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
		ELSE
			INSERT INTO #selected_Foreclosure_case
				SELECT	fc_id FROM foreclosure_case f
				WHERE	f.completed_dt IS NOT NULL AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
	END

	INSERT INTO #counselingDetail 
		(fc_id, agency_name, completed_dt, call_id, program_name
		, agency_case_num, agency_client_num, intake_dt
		, income_earners_cd, case_source_cd, race_cd, household_cd
		, dflt_reason_1st_cd, dflt_reason_2nd_cd, hud_termination_reason_cd
		, hud_termination_dt
		, hud_outcome_cd, counseling_duration_cd, gender_cd
		, borrower_fname, borrower_lname, mother_maiden_lname, borrower_last4_ssn, borrower_dob
		, co_borrower_fname, co_borrower_lname, co_borrower_last4_ssn, co_borrower_dob
		, primary_contact_no, second_contact_no, email_1
		, prop_addr1, prop_addr2, prop_city, prop_state_cd, prop_zip
		, bankruptcy_ind, borrower_educ_level_completed_cd, borrower_marital_status_cd
		, hispanic_ind, fc_notice_received_ind, servicer_consent_ind, borrower_disabled_ind
		, summary_sent_other_cd, summary_sent_other_dt, summary_sent_dt
		, occupant_num, counselor_lname, counselor_fname, owner_occupied_ind
		, military_service_cd, household_gross_annual_income_amt, intake_credit_score
		, servicer_name, acct_num, mortgage_type_cd, arm_reset_ind, term_length_cd, loan_delinq_status_cd
		, current_loan_balance_amt, interest_rate
		)
	select fc.fc_id,
       a.agency_name,
       fc.completed_dt,
       fc.call_id,
       p.program_name, 
       fc.agency_case_num,
       fc.agency_client_num,
       convert(varchar(10), fc.intake_dt, 101) as intake_dt,
       r1.code_desc as income_earners, --fc.income_earners_cd,
       r2.code_desc as case_source, --fc.case_source_cd,
       r3.code_desc as race, --fc.race_cd,
       r4.code_desc as household, --fc.household_cd,
       r5.code_desc as default_reason_1st, --fc.dflt_reason_1st_cd,
       r6.code_desc as default_reason_2nd, --fc.dflt_reason_2nd_cd,
       r7.code_desc as hud_termination_reason, --fc.hud_termination_reason_cd,
       convert(varchar(10), fc.hud_termination_dt, 101) as hud_termination_dt,
       r8.code_desc as hud_outcome, --fc.hud_outcome_cd,
       r9.code_desc as counseling_duration, --fc.counseling_duration_cd,
       r10.code_desc as gender, --fc.gender_cd,
       fc.borrower_fname,
       fc.borrower_lname,
       fc.mother_maiden_lname,
       fc.borrower_last4_ssn,
       convert(varchar(10), fc.borrower_dob, 101) as borrower_dob,
       fc.co_borrower_fname,
       fc.co_borrower_lname,
       fc.co_borrower_last4_ssn,
       convert(varchar(10), fc.co_borrower_dob, 101) as co_borrower_dob,
       fc.primary_contact_no,
       fc.second_contact_no,
       fc.email_1,
       fc.prop_addr1,
       fc.prop_addr2,
       fc.prop_city,
       fc.prop_state_cd,
       fc.prop_zip,
       fc.bankruptcy_ind,
       r11.code_desc as borrower_educ_level_completed, --fc.borrower_educ_level_completed_cd,
       r12.code_desc as borrower_marital_status, --fc.borrower_marital_status_cd,
       fc.hispanic_ind,
       fc.fc_notice_received_ind,
       fc.servicer_consent_ind,
       fc.borrower_disabled_ind,
       r13.code_desc as summary_sent_other, --fc.summary_sent_other_cd,
       fc.summary_sent_other_dt,
       fc.summary_sent_dt,
       fc.occupant_num,
       fc.counselor_lname,
       fc.counselor_fname,
       fc.owner_occupied_ind,
       r14.code_desc as military_service, --fc.military_service_cd,
       ''$'' + convert(varchar(30), cast(fc.household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt,
       fc.intake_credit_score,
		case cl.servicer_id when 12982 then cl.other_servicer_name else s.servicer_name end as servicer_name,
       cl.acct_num,
       r15.code_desc as mortgage_type, --cl.mortgage_type_cd,
       cl.arm_reset_ind,
       r16.code_desc as term_length, --cl.term_length_cd,
       r17.code_desc as loan_delinq_status, --cl.loan_delinq_status_cd,
       ''$'' + convert(varchar(30), cast(cl.current_loan_balance_amt as money),1) current_loan_balance_amt,
       cl.interest_rate
	from   agency a, program p, servicer s, #selected_Foreclosure_case sf, 
		foreclosure_case fc
		left outer join ref_code_item r1 on fc.income_earners_cd = r1.code and r1.ref_code_set_name = ''income earners code''
		left outer join ref_code_item r2 on fc.case_source_cd = r2.code and r2.ref_code_set_name = ''case source code''
		left outer join ref_code_item r3 on fc.race_cd = r3.code and r3.ref_code_set_name = ''race code''
		left outer join ref_code_item r4 on fc.household_cd = r4.code and r4.ref_code_set_name = ''household code''
		left outer join ref_code_item r5 on fc.dflt_reason_1st_cd = r5.code and r5.ref_code_set_name = ''default reason code''
		left outer join ref_code_item r6 on fc.dflt_reason_2nd_cd = r6.code and r6.ref_code_set_name = ''default reason code''
		left outer join ref_code_item r7 on fc.hud_termination_reason_cd = r7.code and r7.ref_code_set_name = ''hud termination reason code''
		left outer join ref_code_item r8 on fc.hud_outcome_cd = r8.code and r8.ref_code_set_name = ''hud outcome code''
		left outer join ref_code_item r9 on fc.counseling_duration_cd = r9.code and r9.ref_code_set_name = ''counseling duration code''
		left outer join ref_code_item r10 on fc.gender_cd = r10.code and r10.ref_code_set_name = ''gender code''
		left outer join ref_code_item r11 on borrower_educ_level_completed_cd = r11.code and r11.ref_code_set_name = ''education level completed code''
		left outer join ref_code_item r12 on fc.borrower_marital_status_cd = r12.code and r12.ref_code_set_name = ''marital status code''
		left outer join ref_code_item r13 on fc.summary_sent_other_cd = r13.code and r13.ref_code_set_name = ''summary sent other code''
		left outer join ref_code_item r14 on fc.military_service_cd = r14.code and r14.ref_code_set_name = ''military service code''
		, case_loan cl
		left outer join ref_code_item r15 on cl.mortgage_type_cd = r15.code and r15.ref_code_set_name = ''mortgage type code''
		left outer join ref_code_item r16 on cl.term_length_cd = r16.code and r16.ref_code_set_name = ''term length code''
		left outer join ref_code_item r17 on cl.loan_delinq_status_cd = r17.code and r17.ref_code_set_name = ''loan delinquency status code''
	where  fc.fc_id = cl.fc_id and fc.fc_id = sf.fc_id
		and    fc.agency_id = a.agency_id
		and    fc.program_id = p.program_id
		and    cl.servicer_id = s.servicer_id
		and    cl.loan_1st_2nd_cd = ''1ST''
		and    fc.completed_dt between @pi_from_dt and @pi_to_dt;

-- Select the first three outcomes for each foreclosure case
	INSERT INTO #t_outcomes	(fc_id, outcome_type_id, outcome_type_name, rownum)
		SELECT	oi.fc_id, oi.outcome_type_id, ot.outcome_type_name,
				row_number() OVER (PARTITION BY oi.fc_id ORDER BY oi.outcome_dt, oi.outcome_item_id) AS rownum
		FROM	outcome_item AS oi,
				outcome_type AS ot,
				foreclosure_case fc, #selected_Foreclosure_case sf
		WHERE	oi.outcome_type_id = ot.outcome_type_id
				and    oi.fc_id = fc.fc_id AND fc.fc_id = sf.fc_id
				and    ot.payable_ind = ''Y''
				and    oi.outcome_deleted_dt IS NULL
				and    oi.outcome_dt >= dateadd(day, -90, fc.completed_dt)
				and    fc.completed_dt between @pi_from_dt and @pi_to_dt;
-- Display the first three outcomes for each foreclosure case in a single row
	UPDATE	#counselingDetail 
	SET		#counselingDetail.outcome_1 = oi.outcome_1
			, #counselingDetail.outcome_2 = oi.outcome_2
			, #counselingDetail.outcome_3 = oi.outcome_3
	FROM	#counselingDetail 
			INNER JOIN 
			(SELECT o1.fc_id,
				   o1.outcome_type_name AS outcome_1,
				   o2.outcome_type_name AS outcome_2,
				   o3.outcome_type_name AS outcome_3
			FROM   #t_outcomes o1
				   LEFT OUTER JOIN #t_outcomes o2 ON o2.rownum = o1.rownum + 1 AND o2.fc_id = o1.fc_id
				   LEFT OUTER JOIN #t_outcomes o3 ON o3.rownum = o1.rownum + 2 AND o3.fc_id = o1.fc_id
				   LEFT OUTER JOIN #t_outcomes o4 ON o4.rownum = o1.rownum - 1 AND o4.fc_id = o1.fc_id
			WHERE  o4.outcome_type_id IS NULL
			) oi ON oi.fc_id = #counselingDetail.fc_id;			

	SELECT * FROM #counselingDetail
	Order by agency_name, fc_id;

	DROP TABLE #counselingDetail;
	DROP TABLE #selected_foreclosure_case;
	DROP TABLE #t_outcomes;
END;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_replace_non_numeric_char]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 29 Feb
-- Description:	Replace all non-numeric char
-- =============================================
CREATE FUNCTION [dbo].[hpf_rpt_replace_non_numeric_char]
(@pi_value	varchar(30))
RETURNS varchar(30)
AS
BEGIN	
	DECLARE @v_value varchar(30);
	SELECT @v_value = '''';
	SELECT @pi_value = Upper(rtrim(ltrim(@pi_value)));

	WHILE len(@pi_value) >0
	BEGIN		
		IF (ASCII(@pi_value)>=48 AND ASCII(@pi_value) <=57)
			SELECT @v_value = @v_value + substring(@pi_value,1,1);
		SELECT @pi_value = substring(@pi_value,2,30);
	END;
	RETURN @v_value;
END
' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyActivityReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 21 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_DailyActivityReport	
--				Report name : Daily Activity Report			
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_DailyActivityReport]
	(@pi_Mon varchar(20), @pi_year integer)
AS
Declare @v_Mon_year varchar(20);
CREATE TABLE #t_Daily
(Agency_id	int,
Agency_name	varchar(50),
Day1		int,Day2		int,Day3		int,Day4		int,Day5		int,
Day6		int,Day7		int,Day8		int,Day9		int,Day10		int,
Day11		int,Day12		int,Day13		int,Day14		int,Day15		int,
Day16		int,Day17		int,Day18		int,Day19		int,Day20		int,
Day21		int,Day22		int,Day23		int,Day24		int,Day25		int,
Day26		int,Day27		int,Day28		int,Day29		int,Day30		int,
Day31		int
)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT @v_Mon_Year = ''-'' + @pi_Mon + ''-'' + cast(@pi_Year as varchar);
	
--	INSERT INTO #t_Daily (Agency_id, Agency_name, Day1)
--		SELECT	distinct a.agency_id, a.agency_name,  count(f1.fc_id) as Number
--		FROM	foreclosure_case f1 RIGHT OUTER JOIN agency a ON a.agency_id = f1.agency_id
--					AND cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''01'' + @v_Mon_year) as datetime)
--		GROUP BY a.agency_name, a.agency_id, f1.create_dt;

	INSERT INTO #t_Daily (Agency_id, Agency_name, Day1)
		select a.agency_id, a.agency_name, 0
		from agency a

	UPDATE	#t_Daily
	SET		#t_Daily.Day1 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''01'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day2 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''02'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day3 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''03'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day4 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''04'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day5 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''05'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day6 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''06'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day7 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''07'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day8 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''08'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day9 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''09'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day10 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''10'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day11 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''11'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day12 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''12'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day13 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''13'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day14 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''14'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day15 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''15'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day16 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''16'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day17 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''17'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day18 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''18'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day19 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''19'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day20 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''20'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day21 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''21'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day22 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''22'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day23 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''23'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day24 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''24'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day25 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''25'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day26 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''26'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day27 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''27'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day28 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''28'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	IF (@pi_Mon IN (''Jan'', ''Mar'', ''Apr'', ''May'', ''Jun'', ''Jul'', ''Aug'', ''Sep'', ''Oct'', ''Nov'', ''Dec'')
		OR (@pi_Mon = ''Feb'' AND @pi_year%4 = 0))
		UPDATE	#t_Daily
		SET		#t_Daily.Day29 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''29'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;
	IF (@pi_Mon <> ''Feb'')
		UPDATE	#t_Daily
		SET		#t_Daily.Day30 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''30'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;
	IF (@pi_Mon NOT IN (''Feb'', ''Apr'', ''Jun'', ''Sep'', ''Nov''))
		UPDATE	#t_Daily
		SET		#t_Daily.Day31 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	cast(cast(f1.create_dt as varchar(11)) as datetime) = cast((''31'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	SELECT  Agency_name
			, isnull(Day1, 0) as Day1, isnull(Day2, 0) as Day2, isnull(Day3, 0) as Day3, isnull(Day4, 0) as Day4, isnull(Day5, 0) as Day5
			, isnull(Day6, 0) as Day6, isnull(Day7, 0) as Day7, isnull(Day8, 0) as Day8, isnull(Day9, 0) as Day9, isnull(Day10, 0) as Day10
			, isnull(Day11, 0) as Day11, isnull(Day12, 0) as Day12, isnull(Day13, 0) as Day13, isnull(Day14, 0) as Day14, isnull(Day15, 0) as Day15
			, isnull(Day16, 0) as Day16, isnull(Day17, 0) as Day17, isnull(Day18, 0) as Day18, isnull(Day19, 0) as Day19, isnull(Day20, 0) as Day20
			, isnull(Day21, 0) as Day21, isnull(Day22, 0) as Day22, isnull(Day23, 0) as Day23, isnull(Day24, 0) as Day24, isnull(Day25, 0) as Day25
			, isnull(Day26, 0) as Day26, isnull(Day27, 0) as Day27, isnull(Day28, 0) as Day28, isnull(Day29, 0) as Day29, isnull(Day30, 0) as Day30
			, isnull(Day31, 0) as Day31
	FROM #T_Daily order by agency_name;
	DROP TABLE #t_Daily;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyCompletionReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_DailyCompletionReport	
--				Report name : Daily Completion Report			
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_DailyCompletionReport]
	(@pi_Mon varchar(20), @pi_year integer)
AS
Declare @v_Mon_year varchar(20);
CREATE TABLE #t_Daily
(Agency_id	int,
Agency_name	varchar(50),
Day1		int,Day2		int,Day3		int,Day4		int,Day5		int,
Day6		int,Day7		int,Day8		int,Day9		int,Day10		int,
Day11		int,Day12		int,Day13		int,Day14		int,Day15		int,
Day16		int,Day17		int,Day18		int,Day19		int,Day20		int,
Day21		int,Day22		int,Day23		int,Day24		int,Day25		int,
Day26		int,Day27		int,Day28		int,Day29		int,Day30		int,
Day31		int
)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT @v_Mon_Year = ''-'' + @pi_Mon + ''-'' + cast(@pi_Year as varchar);
	
--	INSERT INTO #t_Daily (Agency_id, Agency_name, Day1)
--		SELECT distinct	a.agency_id, a.agency_name,  count(f1.fc_id) as Number
--		FROM	foreclosure_case f1 RIGHT OUTER JOIN agency a ON a.agency_id = f1.agency_id
--					AND cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''01'' + @v_Mon_year) as datetime)
--		GROUP BY a.agency_name, a.agency_id, f1.completed_dt;

	INSERT INTO #t_Daily (Agency_id, Agency_name, Day1)
		select a.agency_id, a.agency_name, 0
		from agency a

	UPDATE	#t_Daily
	SET		#t_Daily.Day1 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''01'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day2 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''02'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day3 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''03'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day4 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''04'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day5 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''05'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day6 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''06'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day7 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''07'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day8 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''08'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day9 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''09'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day10 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''10'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day11 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''11'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day12 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''12'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day13 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''13'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day14 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''14'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day15 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''15'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day16 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''16'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day17 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''17'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day18 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''18'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day19 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''19'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day20 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''20'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day21 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''21'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day22 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''22'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day23 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''23'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day24 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''24'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day25 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''25'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day26 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''26'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day27 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''27'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day28 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''28'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	IF (@pi_Mon IN (''Jan'', ''Mar'', ''Apr'', ''May'', ''Jun'', ''Jul'', ''Aug'', ''Sep'', ''Oct'', ''Nov'', ''Dec'')
		OR (@pi_Mon = ''Feb'' AND @pi_year%4 = 0))
		UPDATE	#t_Daily
		SET		#t_Daily.Day29 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''29'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;
	IF (@pi_Mon <> ''Feb'')
		UPDATE	#t_Daily
		SET		#t_Daily.Day30 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''30'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;
	IF (@pi_Mon NOT IN (''Feb'', ''Apr'', ''Jun'', ''Sep'', ''Nov''))
		UPDATE	#t_Daily
		SET		#t_Daily.Day31 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	cast(cast(f1.completed_dt as varchar(11)) as datetime) = cast((''31'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	SELECT  Agency_name
			, isnull(Day1, 0) as Day1, isnull(Day2, 0) as Day2, isnull(Day3, 0) as Day3, isnull(Day4, 0) as Day4, isnull(Day5, 0) as Day5
			, isnull(Day6, 0) as Day6, isnull(Day7, 0) as Day7, isnull(Day8, 0) as Day8, isnull(Day9, 0) as Day9, isnull(Day10, 0) as Day10
			, isnull(Day11, 0) as Day11, isnull(Day12, 0) as Day12, isnull(Day13, 0) as Day13, isnull(Day14, 0) as Day14, isnull(Day15, 0) as Day15
			, isnull(Day16, 0) as Day16, isnull(Day17, 0) as Day17, isnull(Day18, 0) as Day18, isnull(Day19, 0) as Day19, isnull(Day20, 0) as Day20
			, isnull(Day21, 0) as Day21, isnull(Day22, 0) as Day22, isnull(Day23, 0) as Day23, isnull(Day24, 0) as Day24, isnull(Day25, 0) as Day25
			, isnull(Day26, 0) as Day26, isnull(Day27, 0) as Day27, isnull(Day28, 0) as Day28, isnull(Day29, 0) as Day29, isnull(Day30, 0) as Day30
			, isnull(Day31, 0) as Day31
	FROM #T_Daily order by agency_name;
	DROP TABLE #t_Daily;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyCompletionByServicerReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 20 May 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_DailyCompletionReportByServicer	
--				Report name : Daily Completion Report By Servicer
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_DailyCompletionByServicerReport]
	(@pi_Mon varchar(2), @pi_year integer)
AS
Declare @v_Mon_year varchar(20);
CREATE TABLE #t_Daily
(servicer_id	int,
servicer_name	varchar(50),
other_ind		varchar(1),
secure_delivery_method_cd varchar(50),
Day1		int,Day2		int,Day3		int,Day4		int,Day5		int,
Day6		int,Day7		int,Day8		int,Day9		int,Day10		int,
Day11		int,Day12		int,Day13		int,Day14		int,Day15		int,
Day16		int,Day17		int,Day18		int,Day19		int,Day20		int,
Day21		int,Day22		int,Day23		int,Day24		int,Day25		int,
Day26		int,Day27		int,Day28		int,Day29		int,Day30		int,
Day31		int
)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT @v_Mon_Year = ''-'' + @pi_Mon + ''-'' + cast(@pi_Year as varchar(4));
	
	INSERT INTO #t_Daily (Servicer_id, Servicer_name, other_ind, secure_delivery_method_cd, Day1)
		select	s.servicer_id, s.servicer_name, ''N'', secure_delivery_method_cd, 0
		from	servicer s INNER JOIN case_loan l ON l.servicer_id = s.servicer_id
				INNER JOIN foreclosure_case f ON l.fc_id = f.fc_id AND l.servicer_id <> 12982
		WHERE	convert(varchar(6), f.completed_dt, 112) = cast(@pi_Year as varchar(4)) + @pi_Mon
		UNION
		select	distinct 12982, l.other_servicer_name, ''Y'', NULL, 0
		from	case_loan l INNER JOIN foreclosure_case f ON l.fc_id = f.fc_id AND l.servicer_id = 12982
		WHERE	convert(varchar(6), f.completed_dt, 112) = cast(@pi_Year as varchar(4)) + @pi_Mon;		

	UPDATE	#t_Daily
	SET		#t_Daily.Day1 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''01'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''01'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day2 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''02'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''02'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day3 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''03'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''03'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day4 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''04'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''04'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day5 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''05'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''05'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day6 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''06'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''06'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day7 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''07'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''07'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day8 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''08'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''08'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day9 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''09'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''09'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day10 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''10'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''10'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;


	UPDATE	#t_Daily
	SET		#t_Daily.Day11 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''11'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''11'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day12 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''12'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''12'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day13 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''13'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''13'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day14 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''14'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''14'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day15 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''15'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''15'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day16 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''16'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''16'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day17 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''17'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''17'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day18 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''18'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''18'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day19 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''19'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''19'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day20 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''20'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''20'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day21 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''21'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''21'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day22 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''22'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''22'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day23 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''23'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''23'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day24 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''24'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''24'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day25 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''25'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''25'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day26 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''26'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''26'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day27 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''27'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''27'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	UPDATE	#t_Daily
	SET		#t_Daily.Day28 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''28'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''28'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	IF (@pi_Mon IN (''01'', ''03'', ''04'', ''05'', ''06'', ''07'', ''08'', ''09'', ''10'', ''11'', ''12'')
		OR (@pi_Mon = ''02'' AND @pi_year%4 = 0))
		UPDATE	#t_Daily
		SET		#t_Daily.Day29 = t.Number
		FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''29'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''29'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	IF (@pi_Mon <> ''02'')
		UPDATE	#t_Daily
		SET		#t_Daily.Day30 = t.Number
		FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''30'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''30'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	IF (@pi_Mon NOT IN (''02'', ''04'', ''06'', ''09'', ''11''))
		UPDATE	#t_Daily
		SET		#t_Daily.Day31 = t.Number
		FROM	#t_Daily INNER JOIN 
			(SELECT	l.servicer_id, s.servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id <> 12982
					INNER JOIN servicer s ON l.servicer_id = s.servicer_id
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''31'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, s.servicer_name
			UNION
			SELECT	l.servicer_id, l.other_servicer_name as servicer_name, count(f.fc_id) as Number	
			FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.servicer_id = 12982
			WHERE	cast(cast(f.completed_dt as varchar(11)) as datetime) = convert(datetime, (''31'' + @v_Mon_year), 103)		
			GROUP BY l.servicer_id, l.other_servicer_name
			) t ON #t_Daily.servicer_id = t.servicer_id AND #t_Daily.servicer_name = t.servicer_name;

	SELECT  Servicer_name, other_ind, secure_delivery_method_cd
			, isnull(Day1, 0) as Day1, isnull(Day2, 0) as Day2, isnull(Day3, 0) as Day3, isnull(Day4, 0) as Day4, isnull(Day5, 0) as Day5
			, isnull(Day6, 0) as Day6, isnull(Day7, 0) as Day7, isnull(Day8, 0) as Day8, isnull(Day9, 0) as Day9, isnull(Day10, 0) as Day10
			, isnull(Day11, 0) as Day11, isnull(Day12, 0) as Day12, isnull(Day13, 0) as Day13, isnull(Day14, 0) as Day14, isnull(Day15, 0) as Day15
			, isnull(Day16, 0) as Day16, isnull(Day17, 0) as Day17, isnull(Day18, 0) as Day18, isnull(Day19, 0) as Day19, isnull(Day20, 0) as Day20
			, isnull(Day21, 0) as Day21, isnull(Day22, 0) as Day22, isnull(Day23, 0) as Day23, isnull(Day24, 0) as Day24, isnull(Day25, 0) as Day25
			, isnull(Day26, 0) as Day26, isnull(Day27, 0) as Day27, isnull(Day28, 0) as Day28, isnull(Day29, 0) as Day29, isnull(Day30, 0) as Day30
			, isnull(Day31, 0) as Day31
	FROM #T_Daily 
	WHERE servicer_name IS NOT NULL
	order by servicer_name;

	DROP TABLE #t_Daily;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_IncompletedCounselingCases]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 28 Nov 2008
-- Description:	HPF REPORT - R3 - InCompleted Counseling Cases 		
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_IncompletedCounselingCases] 
	(@pi_agency_id integer, @pi_from_dt datetime, @pi_to_dt datetime)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);	
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

    -- Report R3: incompleted counseling Cases
	-- Get all Agencies
	IF (@pi_agency_id =-1 )
		SELECT	a.fc_id, aa.agency_name, call_id, b.program_name, agency_case_num
			, convert(varchar, intake_dt, 101) as intake_dt
			, cd11.code_desc income_earners_cd
			, cd14.code_desc case_source_cd
			, cd13.code_desc race_cd
			, cd10.code_desc household_cd
			, cd3.code_desc dflt_reason_1st_cd, cd4.code_desc  dflt_reason_2nd_cd, hud_outcome_cd
			, cd1.code_desc counseling_duration_cd, gender_cd
			, borrower_fname, borrower_lname, borrower_mname, convert(varchar, borrower_DOB, 101) as  borrower_DOB
			, primary_contact_no, contact_addr1, contact_city, contact_state_cd, contact_zip
			, prop_addr1, prop_city, prop_state_cd, prop_zip
			, CASE bankruptcy_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  bankruptcy_ind END bankruptcy_ind
			, CASE owner_occupied_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  owner_occupied_ind END owner_occupied_ind
			, CASE hispanic_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  hispanic_ind END hispanic_ind
			, CASE fc_notice_received_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  fc_notice_received_ind END fc_notice_received_ind
			, CASE funding_consent_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  funding_consent_ind END funding_consent_ind
			, CASE servicer_consent_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  servicer_consent_ind END servicer_consent_ind
			, occupant_num, loan_dflt_reason_notes, action_items_notes
			, counselor_id_ref, counselor_fname + '' '' + counselor_lname as counselor_full_name , counselor_email, counselor_phone, counselor_ext
			, CASE discussed_solution_with_srvcr_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  discussed_solution_with_srvcr_ind END discussed_solution_with_srvcr_ind
			, CASE worked_with_another_agency_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  worked_with_another_agency_ind END worked_with_another_agency_ind
			, CASE contacted_srvcr_recently_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  contacted_srvcr_recently_ind END contacted_srvcr_recently_ind
			, CASE has_workout_plan_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  has_workout_plan_ind END has_workout_plan_ind
			, CASE opt_out_newsletter_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  opt_out_newsletter_ind END opt_out_newsletter_ind
			, CASE opt_out_survey_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  opt_out_survey_ind END opt_out_survey_ind
			, CASE do_not_call_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  do_not_call_ind END do_not_call_ind
			, CASE primary_residence_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  primary_residence_ind END primary_residence_ind
			, bankruptcy_attorney
			, ''$'' + convert(varchar(30), cast(household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt
			, a.loan_list
			, l.Loan_no, cd6.code_desc loan_1st_2nd_cd, cd8.code_desc mortgage_type_cd, cd9.code_desc term_length_cd, cd5.code_desc loan_delinq_status_cd
			, cast(l.interest_rate as varchar(10)) + ''%'' as interest_rate
		FROM	foreclosure_case a
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name =''counseling duration code''	) cd1 ON cd1.code = a.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd3 ON cd3.code = dflt_reason_1st_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd4 ON cd4.code = dflt_reason_2nd_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''household code'') cd10 ON cd10.code = a.household_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''income earners code'') cd11 ON cd11.code = a.income_earners_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''race code'') cd13 ON cd13.code = a.race_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''case source code'') cd14 ON cd14.code = a.case_source_cd
			, (Select fc_id, acct_num + ''-'' + s.servicer_name as Loan_no, loan_1st_2nd_cd, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, interest_rate
				FROM case_loan cl INNER JOIN servicer s ON cl.servicer_id = s.servicer_id
				)l 
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan delinquency status code'') cd5 ON cd5.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan 1st 2nd code'') cd6 ON cd6.code = l.loan_1st_2nd_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''mortgage type code'') cd8 ON cd8.code = l.mortgage_type_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''term length code'') cd9 ON cd9.code = l.term_length_cd
				, program b, agency aa
		WHERE a.program_id = b.program_id AND a.agency_id = aa.agency_id AND a.fc_id = l.fc_id
			AND a.intake_dt between @pi_from_dt and @pi_to_dt			
			AND a.completed_dt IS NULL
		ORDER BY aa.Agency_name, a.agency_case_num;
	ELSE
	-- Get specific agency
	IF (@pi_agency_id >0 )
		SELECT	a.fc_id, aa.agency_name, call_id, b.program_name, agency_case_num
			, convert(varchar, intake_dt, 101) as intake_dt
			, cd11.code_desc income_earners_cd
			, cd14.code_desc case_source_cd
			, cd13.code_desc race_cd
			, cd10.code_desc household_cd
			, cd3.code_desc dflt_reason_1st_cd, cd4.code_desc  dflt_reason_2nd_cd, hud_outcome_cd
			, cd1.code_desc counseling_duration_cd, gender_cd
			, borrower_fname, borrower_lname, borrower_mname, convert(varchar, borrower_DOB, 101) as  borrower_DOB
			, primary_contact_no, contact_addr1, contact_city, contact_state_cd, contact_zip
			, prop_addr1, prop_city, prop_state_cd, prop_zip
			, CASE bankruptcy_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  bankruptcy_ind END bankruptcy_ind
			, CASE owner_occupied_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  owner_occupied_ind END owner_occupied_ind
			, CASE hispanic_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  hispanic_ind END hispanic_ind
			, CASE fc_notice_received_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  fc_notice_received_ind END fc_notice_received_ind
			, CASE funding_consent_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  funding_consent_ind END funding_consent_ind
			, CASE servicer_consent_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  servicer_consent_ind END servicer_consent_ind
			, occupant_num, loan_dflt_reason_notes, action_items_notes
			, counselor_id_ref, counselor_fname + '' '' + counselor_lname as counselor_full_name , counselor_email, counselor_phone, counselor_ext
			, CASE discussed_solution_with_srvcr_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  discussed_solution_with_srvcr_ind END discussed_solution_with_srvcr_ind
			, CASE worked_with_another_agency_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  worked_with_another_agency_ind END worked_with_another_agency_ind
			, CASE contacted_srvcr_recently_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  contacted_srvcr_recently_ind END contacted_srvcr_recently_ind
			, CASE has_workout_plan_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  has_workout_plan_ind END has_workout_plan_ind
			, CASE opt_out_newsletter_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  opt_out_newsletter_ind END opt_out_newsletter_ind
			, CASE opt_out_survey_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  opt_out_survey_ind END opt_out_survey_ind
			, CASE do_not_call_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  do_not_call_ind END do_not_call_ind
			, CASE primary_residence_ind WHEN ''Y'' then ''Yes'' WHEN ''N''  then ''No'' ELSE  primary_residence_ind END primary_residence_ind
			, bankruptcy_attorney
			, ''$'' + convert(varchar(30), cast(household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt
			, a.loan_list
			, l.Loan_no, cd6.code_desc loan_1st_2nd_cd, cd8.code_desc mortgage_type_cd, cd9.code_desc term_length_cd, cd5.code_desc loan_delinq_status_cd
			, cast(l.interest_rate as varchar(10)) + ''%'' as interest_rate
		FROM	foreclosure_case a
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name =''counseling duration code''	) cd1 ON cd1.code = a.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd3 ON cd3.code = dflt_reason_1st_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd4 ON cd4.code = dflt_reason_2nd_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''household code'') cd10 ON cd10.code = a.household_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''income earners code'') cd11 ON cd11.code = a.income_earners_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''race code'') cd13 ON cd13.code = a.race_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''case source code'') cd14 ON cd14.code = a.case_source_cd
			, (Select fc_id, acct_num + ''-'' + s.servicer_name as Loan_no, loan_1st_2nd_cd, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, interest_rate
				FROM case_loan cl INNER JOIN servicer s ON cl.servicer_id = s.servicer_id
				)l 
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan delinquency status code'') cd5 ON cd5.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan 1st 2nd code'') cd6 ON cd6.code = l.loan_1st_2nd_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''mortgage type code'') cd8 ON cd8.code = l.mortgage_type_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''term length code'') cd9 ON cd9.code = l.term_length_cd
				, program b, agency aa
		WHERE a.program_id = b.program_id AND a.agency_id = aa.agency_id AND a.fc_id = l.fc_id
			and a.intake_dt between @pi_from_dt and @pi_to_dt			
			AND a.completed_dt IS NULL
			AND a.agency_id = @pi_agency_id 
		ORDER BY aa.Agency_name, a.agency_case_num;
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PotentialDuplicates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 13 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	hpf_rpt_PotentialDuplicates
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_PotentialDuplicates] 
	(@pi_from_dt datetime
	, @pi_to_dt datetime
	, @pi_filteredBy int	-- 0: By Acct & Servicer; 1: By Address; 2: By Phone
	)
AS
BEGIN
	DECLARE @v_from_last_year_dt datetime;

	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
	SELECT @v_from_last_year_dt = DateAdd(year, -1, @pi_from_dt);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

	/* Potential duplicates are records with
	0.the 1st 7 chars of acct_num and Servicer name
	1. Address1 (only the 1st 12 chars) AND zip
	2.Primary_contact no	
	*/
IF (@pi_filteredBy = 0)
	SELECT f.fc_id, a.agency_name, l.acct_num
		, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
		, f.borrower_fname, f.borrower_lname, f.borrower_last4_ssn
		, f.primary_contact_no, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
		, f.agency_case_num
		, convert(varchar(20), f.intake_dt, 101) as intake_dt, convert(varchar(20), f.completed_dt, 101) as completed_dt
		, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
		, l.servicer_id, f.loan_list
		, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_last4_ssn
		, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip
		, f.counselor_lname, f.counselor_fname
		, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.mortgage_program_cd
		, f.never_bill_reason_cd, f.never_pay_reason_cd
	FROM	foreclosure_case f, case_loan l, agency a, program p, servicer s
			, (SELECT	DISTINCT	substring(lmo.acct_num, 1, 9)  + '';'' + cast(lmo.servicer_id as varchar(50)) as loan_info
			FROM	case_loan lmo, foreclosure_case fcmo
					, (SELECT	substring(lall.acct_num, 1, 9)  + '';'' + cast(lall.servicer_id as varchar(50)) as loan_info
						FROM	case_loan lall, foreclosure_case fcall
						WHERE	lall.fc_id = fcall.fc_id
								AND fcall.duplicate_ind <>''Y''
								AND lall.loan_1st_2nd_cd = ''1ST'' and lall.servicer_id <> 12982					
								AND isnull(fcall.completed_dt, fcall.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
						GROUP BY	substring(lall.acct_num, 1, 9)  + '';'' + cast(lall.servicer_id as varchar(50)) 
						HAVING count(*) > 1			
						) cf1
			WHERE	lmo.fc_id = fcmo.fc_id
					AND fcmo.duplicate_ind <> ''Y''
					AND lmo.loan_1st_2nd_cd = ''1ST''	AND lmo.servicer_id <> 12982					
					AND isnull(fcmo.completed_dt, fcmo.create_dt) BETWEEN @pi_from_dt AND @pi_to_dt
					AND substring(lmo.acct_num, 1, 9)  + '';'' + cast(lmo.servicer_id as varchar(50)) = cf1.loan_info						
			) cf
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = ''1ST'' AND l.servicer_id <> 12982
			AND f.duplicate_ind <> ''Y''			
			AND isnull(f.completed_dt, f.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
			AND substring(l.acct_num, 1, 9)  + '';'' + cast(l.servicer_id as varchar(50)) = cf.loan_info				
	ORDER BY servicer_name, substring(l.acct_num, 1, 9);							

IF (@pi_filteredBy = 1)
	SELECT f.fc_id, a.agency_name, l.acct_num
		, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
		, f.borrower_fname, f.borrower_lname, f.borrower_last4_ssn
		, f.primary_contact_no, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
		, f.agency_case_num
		, convert(varchar(20), f.intake_dt, 101) as intake_dt, convert(varchar(20), f.completed_dt, 101) as completed_dt
		, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
		, l.servicer_id, f.loan_list
		, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_last4_ssn
		, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip
		, f.counselor_lname, f.counselor_fname
		, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.mortgage_program_cd
		, f.never_bill_reason_cd, f.never_pay_reason_cd
	FROM	foreclosure_case f, case_loan l, agency a, program p, servicer s
			, (SELECT DISTINCT substring(fcmo.prop_addr1, 1, 12) + '';'' + fcmo.prop_zip as addr_info
			FROM	foreclosure_case fcmo
					,(SELECT substring(fcall.prop_addr1, 1, 12) + '';'' + fcall.prop_zip as addr_info
					FROM	foreclosure_case fcall
					WHERE	fcall.duplicate_ind <>''Y''							
							AND isnull(fcall.completed_dt, fcall.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
					GROUP BY	substring(fcall.prop_addr1, 1, 12) + '';'' + fcall.prop_zip 
					HAVING count(*) > 1			
					)cf1
			WHERE	fcmo.duplicate_ind <> ''Y''
					AND isnull(fcmo.completed_dt, fcmo.create_dt) BETWEEN @pi_from_dt AND @pi_to_dt
					AND substring(fcmo.prop_addr1, 1, 12) + '';'' + fcmo.prop_zip = cf1.addr_info					
				)cf
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = ''1ST''
			AND f.duplicate_ind <> ''Y''			
			AND isnull(f.completed_dt, f.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
			AND substring(f.prop_addr1, 1, 12) + '';'' + f.prop_zip = cf.addr_info
	order by f.prop_addr1;	
	
IF (@pi_filteredBy = 2)
	SELECT f.fc_id, a.agency_name, l.acct_num
		, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
		, f.borrower_fname, f.borrower_lname, f.borrower_last4_ssn
		, f.primary_contact_no, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
		, f.agency_case_num
		, convert(varchar(20), f.intake_dt, 101) as intake_dt, convert(varchar(20), f.completed_dt, 101) as completed_dt
		, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
		, l.servicer_id, f.loan_list
		, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_last4_ssn
		, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip
		, f.counselor_lname, f.counselor_fname
		, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.mortgage_program_cd
		, f.never_bill_reason_cd, f.never_pay_reason_cd
	FROM	foreclosure_case f, case_loan l, agency a, program p, servicer s
			, (SELECT DISTINCT replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
					replace(fcmo.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''') as phone_info
			FROM	foreclosure_case fcmo
					,(SELECT replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
									replace(fcall.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''') as phone_info
					FROM	foreclosure_case fcall
					WHERE	replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
								replace(fcall.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''') 
								not in ('''',''unknownatconv'',''NA'',''na'',''N/A'',''n/a'',''none'',''	'', ''unknown'')							
							AND isnull(fcall.completed_dt, fcall.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
							AND fcall.duplicate_ind <> ''Y''
					GROUP BY	replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (replace(fcall.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''')	
					HAVING	count(*) > 1
					)cf1
			WHERE	isnull(fcmo.completed_dt, fcmo.create_dt) BETWEEN @pi_from_dt AND @pi_to_dt
					AND fcmo.duplicate_ind <> ''Y''
					AND replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace(replace(replace (
							 replace(fcmo.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''') 
						= cf1.phone_info									
			)cf
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = ''1ST''			
			AND isnull(f.completed_dt, f.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
			AND replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
					replace(f.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''') 
				= cf.phone_info
	ORDER BY replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (replace(f.primary_contact_no,''('',''''),'')'',''''),'' '',''''),''-'',''''),''.'',''''),''/'',''''),''_'',''''),''#'',''''),''+'',''''),''*'',''''),''='',''''),''['',''''),'']'',''''),'':'','''');
END;






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceSummary_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 26 Feb 2009
-- Description:	HPF REPORT - R55 - Invoice Summary - Get Invoice detail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_detail] 
	(@pi_invoice_id integer)
AS
BEGIN
	DECLARE @v_hasServicer INT;
	select @v_hasServicer = -1;
	select @v_hasServicer = count(*)  from funding_source_group fsg, Invoice i
	where fsg.funding_source_id = i.funding_source_id
		AND i.invoice_id = @pi_invoice_id
		and getdate() between fsg.eff_dt AND isnull(fsg.exp_dt, GETDATE());

	IF @v_hasServicer > 0 
		SELECT	convert(varchar(11), i.period_start_dt, 101) as period_start_dt
				, convert(varchar(11), i.period_end_dt, 101) as period_end_dt
				, case l.servicer_id when 12982 then l.other_servicer_name ELSE s.servicer_name END servicer_name
				, count(ic.fc_id) as sessions
				, p.program_name
				, ''$'' + convert(varchar(30), cast(isnull(ic.invoice_case_bill_amt, 0)  as money),1) as rate
				, ''$'' + convert(varchar(30), cast(isnull(count(ic.fc_id)* ic.invoice_case_bill_amt, 0)  as money),1) as item_total
		FROM	invoice i, invoice_case ic, foreclosure_case f, case_loan l
				, servicer s, program p
		WHERE	i.invoice_id = ic.invoice_id 
				AND ic.fc_id= f.fc_id
				AND f.fc_id = l.fc_id AND l.loan_1st_2nd_cd = ''1ST''
				AND l.servicer_id = s.servicer_id
				AND	f.program_id = p.program_id			
				AND i.invoice_id = @pi_invoice_id			
		GROUP BY	i.period_start_dt, i.period_end_dt, s.servicer_name, l.servicer_id , l.other_servicer_name 
					, p.program_name, ic.invoice_case_bill_amt
		ORDER BY	s.servicer_name asc, p.program_name asc, ic.invoice_case_bill_amt asc;
	IF @v_hasServicer = 0 
		SELECT	convert(varchar(11), i.period_start_dt, 101) as period_start_dt
				,convert(varchar(11), i.period_end_dt, 101) as period_end_dt
				, isnull(case l.servicer_id when 12982 then l.other_servicer_name ELSE s.servicer_name END,''Multiple'' ) as servicer_name
				, count(ic.fc_id) as sessions
				, p.program_name
				, ''$'' + convert(varchar(30), cast(isnull(ic.invoice_case_bill_amt, 0)  as money),1) as rate
				, ''$'' + convert(varchar(30), cast(isnull(count(ic.fc_id)* ic.invoice_case_bill_amt, 0)  as money),1) as item_total
		FROM	invoice i, invoice_case ic, foreclosure_case f,program p,
				funding_source fs
				LEFT OUTER JOIN (funding_source_group fsg 
								INNER JOIN (servicer s inner join case_loan l on l.servicer_id = s.servicer_id AND l.loan_1ST_2ND_cd = ''1ST'')
											ON fsg.servicer_id = s.servicer_id
								) ON fsg.funding_source_id = fs.funding_source_id
		WHERE	i.invoice_id = ic.invoice_id 
				and i.funding_source_id = fs.funding_source_id
				AND ic.fc_id= f.fc_id
				AND	f.program_id = p.program_id			
				AND i.invoice_id = @pi_invoice_id			
		GROUP BY	i.period_start_dt, i.period_end_dt, s.servicer_name, l.servicer_id , l.other_servicer_name 
				, p.program_name, ic.invoice_case_bill_amt
		ORDER BY	s.servicer_name asc, p.program_name asc, ic.invoice_case_bill_amt asc;

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HUD_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 17 Mar 2009
-- Description:	HPF REPORT - R61 - Invoice Export File - HUD (detail)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HUD_detail] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	ic.fc_id, ic.invoice_case_id
			, s.servicer_name SERVICER_NAME
			, rci.code_desc MORTGAGE_PROGRAM_CD
			, rci1.code_desc MORTGAGE_TYPE_CD
			, cast(l.interest_rate as varchar(10)) + ''%'' as interest_rate 
			, f.prop_city, f.prop_state_cd, f.prop_zip
			, f.occupant_num
			, convert(varchar(20), f.completed_dt, 101) completed_dt
			, rci2.code_desc case_source
			, rci3.code_desc loan_delinquency
			, p.program_name
			, a.agency_name
			, f.agency_case_num
			, f.counselor_lname, f.counselor_fname
			, rci4.code_desc counseling_duration 			
			, convert(varchar(30), cast((isnull(b.total_income, 0) - isnull(b.total_expenses, 0)) as money), 1) as MTHLY_NET_INCOME
			, rci5.code_desc HUD_OUTCOME
			, convert(varchar(20), f.hud_termination_dt, 101) hud_termination_dt
			, rci6.code_desc hud_termination_reason_cd
	FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.loan_1st_2nd_cd = ''1ST''
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''mortgage program code'') rci ON rci.code = l.mortgage_program_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''mortgage type code'') rci1 ON rci1.code = l.mortgage_type_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''case source code'') rci2 ON rci2.code = f.case_source_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''loan delinquency status code'') rci3 ON rci3.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''counseling duration code'') rci4 ON rci4.code = f.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''hud outcome code'') rci5 ON rci5.code = f.hud_outcome_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''hud termination reason code'') rci6 ON rci6.code =f.hud_termination_reason_cd
			LEFT OUTER JOIN (SELECT fc_id, total_income, total_expenses FROM budget_set 
			WHERE budget_set_id IN (SELECT MAX(budget_set_id )from budget_set bs WHERE bs.fc_id IN (SELECT fc_id FROM invoice_case WHERE invoice_id = @pi_invoice_id) GROUP BY bs.fc_id))b ON b.fc_id = f.fc_id
			, invoice_case ic, servicer s, agency a, program p
	WHERE	ic.fc_id = f.fc_id AND l.servicer_id = s.servicer_id 
			AND f.program_id= p.program_id AND f.agency_id = a.agency_id			
			AND ic.invoice_id = @pi_invoice_id
	ORDER BY a.Agency_name ASC, ic.fc_id ASC;
END 






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableSummary_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Mar 2009
-- Description:	HPF REPORT - R62 - Payable Summary - Get Payable detail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_PayableSummary_detail] 
	(@pi_agency_payable_id integer)
AS
BEGIN
	SELECT	convert(varchar(11), ap.period_start_dt, 101) period_start_dt
			, convert(varchar(11), ap.period_end_dt, 101) period_end_dt
			, p.program_name
			, count(apc.fc_id) as sessions
			, ''$'' + convert(varchar(30), cast(isnull(apc.pmt_amt, 0) as money),1)  as Rate			
			, ''$'' + convert(varchar(30), cast(isnull(count(apc.fc_id)* apc.pmt_amt, 0) as money),1) as item_total
	FROM	agency_payable ap, agency_payable_case apc, foreclosure_case f, program p
	WHERE	ap.agency_payable_id = apc.agency_payable_id
			AND apc.fc_id = f.fc_id
			AND f.program_id = p.program_id
			AND ap.agency_payable_id = @pi_agency_payable_id
	GROUP BY ap.period_start_dt, ap.period_end_dt, p.program_name, apc.pmt_amt 
	ORDER BY p.program_name ASC , apc.pmt_amt  ASC;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HSBC_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 27 Feb 2009
-- Description:	HPF REPORT - R57 - Invoice Export File - HSBC (detail)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HSBC_detail] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	[dbo].[hpf_rpt_replace_non_numeric_char](l.acct_num) as Numeric_Loan_ID
			, l.acct_num Loan_ID
			, f.borrower_lname Last_Name
			, f.borrower_fname First_Name
			, f.prop_addr1 as ADDR_LINE_1, f.prop_city, f.prop_state_cd, f.prop_zip
			, f.primary_contact_no
			, f.intake_dt Refferal_dt
			, s.servicer_name SERVICER
			, f.completed_dt, f.summary_sent_dt, f.fc_id			
	FROM	invoice_case ic, servicer s, foreclosure_case f, case_loan l
	WHERE	ic.fc_id = f.fc_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND l.loan_1st_2nd_cd = ''1ST''
			AND ic.invoice_id = @pi_invoice_id
	ORDER BY servicer_name ASC, Numeric_Loan_ID ASC;
END 


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableExportFile_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Apr 2009
-- Description:	HPF REPORT - Payable Export File(detail)
-- =============================================
 CREATE PROCEDURE [dbo].[hpf_rpt_PayableExportFile_detail] 
	(@pi_agency_payable_id integer)
AS
BEGIN
	SELECT	pc.fc_id, pc.agency_payable_case_id
			, f.agency_case_num
			, case l.servicer_id when 12982 then l.other_servicer_name else s.servicer_name end Servicer_Name
			, l.acct_num 
			, convert(varchar(30), cast(pc.pmt_amt as money), 1) as pmt_amt
			, case pc.NFMC_difference_eligible_ind when ''Y'' then ''Yes'' when ''N'' then ''No'' else pc.NFMC_difference_eligible_ind END NFMC_difference_eligible_ind
			, convert(varchar(30), cast(pc.NFMC_difference_paid_amt as money), 1) as NFMC_difference_paid_amt 
			, convert(varchar(20), pc.takeback_pmt_identified_dt, 101) as takeback_pmt_identified_dt
			, rci1.code_desc as takeback_pmt_reason_cd
			, f.borrower_lname, f.borrower_fname, f.borrower_mname
			, f.prop_addr1 , f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
			, f.primary_contact_no, f.second_contact_no
			, convert(varchar(20), f.create_dt, 101) create_dt
			, convert(varchar(20), f.completed_dt, 101) completed_dt
			, convert(varchar(20), f.summary_sent_dt, 101) summary_sent_dt
	FROM	agency_payable_case pc 
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''takeback reason code'') rci1 ON rci1.code = pc.takeback_pmt_reason_cd
			, servicer s, foreclosure_case f, case_loan l
	WHERE	pc.fc_id = f.fc_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND l.loan_1st_2nd_cd = ''1ST''
			AND pc.agency_payable_id = @pi_agency_payable_id
	ORDER BY f.agency_case_num asc;
END 
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_FIS_header]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 27 Feb 2009
-- Description:	HPF REPORT - R59 - Invoice Export File - FIS (header)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_FIS_header] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	ic.invoice_case_id, convert(varchar(11), i.invoice_dt, 101) invoice_dt
		, cast(ic.invoice_case_bill_amt as numeric(18)) invoice_case_bill_amt
		, s.fis_servicer_num
		, l.acct_num as Loan_number
		, f.borrower_lname + '','' + f.borrower_fname as Borrower_Name
		, f.prop_addr1, f.prop_addr2, f.prop_city, geo.county_name
		, f.prop_state_cd, f.prop_zip, f.prop_zip_plus_4
		, convert(varchar(11),f.intake_dt, 101) intake_dt
		, f.fc_id		
	FROM	invoice_case ic, invoice i
			, case_loan l, servicer s
			, foreclosure_case f  left outer join geocode_ref as geo on geo.zip_code = f.prop_zip AND geo.city_type = ''D''
	WHERE	ic.fc_id = f.fc_id AND i.invoice_id = ic.invoice_id 			
			AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND loan_1st_2nd_cd = ''1ST''
			AND ic.invoice_id = @pi_invoice_id			
	ORDER BY ic.invoice_case_id ASC;
END 





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_UnbilledCaseReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_UnbilledCaseReport	
-- =============================================
CREATE PROCEDURE [dbo].[Hpf_rpt_UnbilledCaseReport] 
	(@pi_agency_id int, @pi_from_dt datetime, @pi_to_dt datetime, @pi_showNeverBillCases int )
AS
DECLARE @sql varchar(8000);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));
	IF @pi_agency_id > 0
	BEGIN
		IF @pi_showNeverBillCases = 0	
			SELECT	distinct f.fc_id, a.agency_name, l.acct_num
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	 
				AND (ic.fc_id IS NULL OR f.fc_id IN	(SELECT	ic3.fc_id
													FROM	(SELECT fc_id, count(fc_id) as total_billed FROM invoice_case GROUP BY fc_id) ic3
															,(SELECT fc_id, count(fc_id) as total_rejected FROM invoice_case 
															WHERE pmt_reject_reason_cd is not null GROUP BY fc_id) ic4
													WHERE	ic3.fc_id = ic4.fc_id and total_billed = total_rejected))
				AND f.never_bill_reason_cd IS NULL 
				AND f.agency_id = @pi_agency_id
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
			ORDER BY a.agency_name, f.fc_id, servicer_name, l.acct_num;
		IF @pi_showNeverBillCases = 1
			SELECT	distinct f.fc_id, a.agency_name, l.acct_num
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	 
				AND (ic.fc_id IS NULL OR f.fc_id IN	(SELECT	ic3.fc_id
													FROM	(SELECT fc_id, count(fc_id) as total_billed FROM invoice_case GROUP BY fc_id) ic3
															,(SELECT fc_id, count(fc_id) as total_rejected FROM invoice_case 
															WHERE pmt_reject_reason_cd is not null GROUP BY fc_id) ic4
													WHERE	ic3.fc_id = ic4.fc_id and total_billed = total_rejected))
				AND f.never_bill_reason_cd IS NOT NULL 
				AND f.agency_id = @pi_agency_id
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
			ORDER BY a.agency_name, f.fc_id, servicer_name, l.acct_num;
	END
	ELSE
	BEGIN
		IF @pi_showNeverBillCases = 0	
			SELECT	distinct f.fc_id, a.agency_name, l.acct_num
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	 
				AND (ic.fc_id IS NULL OR f.fc_id IN	(SELECT	ic3.fc_id
													FROM	(SELECT fc_id, count(fc_id) as total_billed FROM invoice_case GROUP BY fc_id) ic3
															,(SELECT fc_id, count(fc_id) as total_rejected FROM invoice_case 
															WHERE pmt_reject_reason_cd is not null GROUP BY fc_id) ic4
													WHERE	ic3.fc_id = ic4.fc_id and total_billed = total_rejected))
				AND f.never_bill_reason_cd IS NULL 
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
			ORDER BY a.agency_name, f.fc_id, servicer_name, l.acct_num;
		IF @pi_showNeverBillCases = 1
			SELECT	distinct f.fc_id, a.agency_name, l.acct_num
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	 
				AND (ic.fc_id IS NULL OR f.fc_id IN	(SELECT	ic3.fc_id
													FROM	(SELECT fc_id, count(fc_id) as total_billed FROM invoice_case GROUP BY fc_id) ic3
															,(SELECT fc_id, count(fc_id) as total_rejected FROM invoice_case 
															WHERE pmt_reject_reason_cd is not null GROUP BY fc_id) ic4
													WHERE	ic3.fc_id = ic4.fc_id and total_billed = total_rejected))
				AND f.never_bill_reason_cd IS NOT NULL 
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
			ORDER BY a.agency_name, f.fc_id, servicer_name, l.acct_num;
	END;
END;






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'















-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary - Get FC detail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_detail] 
	(@pi_fc_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT	f.fc_id, a.agency_name, f.call_id, f.program_id
			, f.agency_case_num, f.agency_client_num
			, convert(varchar(20), f.intake_dt, 101) intake_dt
			, cd11.code_desc as income_earners_cd, f.case_source_cd, f.race_cd, cd10.code_desc as household_cd
			, f.never_bill_reason_cd, f.never_pay_reason_cd
			, cd3.code_desc as dflt_reason_1st_cd, cd4.code_desc as dflt_reason_2nd_cd
			, f.hud_termination_reason_cd, convert(varchar(20), f.hud_termination_dt, 101) hud_termination_dt, f.hud_outcome_cd
			, f.AMI_percentage
			, cd1.code_desc as counseling_duration_cd
			, cd14.code_desc as gender_cd
			, f.borrower_fname, f.borrower_lname, f.borrower_mname, f.mother_maiden_lname
			, ''XXX-XX-'' + f.borrower_last4_SSN borrower_last4_SSN, convert(varchar(20), f.borrower_DOB, 101) borrower_DOB
			, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_mname
			, ''XXX-XX-'' + f.co_borrower_last4_SSN co_borrower_last4_SSN, convert(varchar(20), f.co_borrower_DOB, 101) co_borrower_DOB
			, f.primary_contact_no, f.second_contact_no
			, f.email_1, f.email_2
			, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip + isnull(''-'' + f.contact_zip_plus4, '''') as contact_zip
			, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip +  isnull(''-'' + f.prop_zip_plus_4, '''') as prop_zip
			, case when f.bankruptcy_ind = ''Y'' then ''Yes'' when f.bankruptcy_ind = ''N'' then ''No'' ELSE NULL END bankruptcy_ind
			, f.bankruptcy_attorney
			, case when f.bankruptcy_pmt_current_ind = ''Y'' then ''Yes'' when f.bankruptcy_pmt_current_ind = ''N'' then ''No'' ELSE NULL END bankruptcy_pmt_current_ind
			, f.borrower_educ_level_completed_cd
			, cd13.code_desc as borrower_marital_status_cd
			, f.borrower_preferred_lang_cd, f.borrower_occupation
			, f.co_borrower_occupation
			, case when f.hispanic_ind= ''Y'' then ''Yes'' when f.hispanic_ind= ''N'' then ''No'' ELSE NULL END hispanic_ind
			, case when f.duplicate_ind= ''Y'' then ''Yes'' when f.duplicate_ind= ''N'' then ''No'' ELSE NULL END duplicate_ind
			, case when f.fc_notice_received_ind= ''Y'' then ''Yes'' when f.fc_notice_received_ind= ''N'' then ''No'' ELSE NULL END fc_notice_received_ind
			, convert(varchar(20), f.fc_sale_dt, 101) fc_sale_dt
			, convert(varchar(20), f.completed_dt, 101)  completed_dt
			, case when f.funding_consent_ind= ''Y'' then ''Yes'' when f.funding_consent_ind= ''N'' then ''No'' ELSE NULL END funding_consent_ind
			, case when f.servicer_consent_ind= ''Y'' then ''Yes'' when f.servicer_consent_ind= ''N'' then ''No'' ELSE NULL END servicer_consent_ind
			, case when f.agency_media_interest_ind= ''Y'' then ''Yes'' when f.agency_media_interest_ind= ''N'' then ''No'' ELSE NULL END agency_media_interest_ind
			, case when f.hpf_media_candidate_ind= ''Y'' then ''Yes'' when f.hpf_media_candidate_ind= ''N'' then ''No'' ELSE NULL END hpf_media_candidate_ind
			, case when f.hpf_success_story_ind= ''Y'' then ''Yes'' when f.hpf_success_story_ind= ''N'' then ''No'' ELSE NULL END hpf_success_story_ind
			, case when f.agency_success_story_ind= ''Y'' then ''Yes'' when f.agency_success_story_ind= ''N'' then ''No'' ELSE NULL END agency_success_story_ind
			, case when f.borrower_disabled_ind= ''Y'' then ''Yes'' when f.borrower_disabled_ind= ''N'' then ''No'' ELSE NULL END borrower_disabled_ind
			, case when f.co_borrower_disabled_ind= ''Y'' then ''Yes'' when f.co_borrower_disabled_ind= ''N'' then ''No'' ELSE NULL END co_borrower_disabled_ind
			, cd12.code_desc as summary_sent_other_cd, convert(varchar(20), f.summary_sent_other_dt, 101) as summary_sent_other_dt, convert(varchar(20), f.summary_sent_dt, 101) summary_sent_dt
			, f.occupant_num
			, f.loan_dflt_reason_notes, f.action_items_notes, f.followup_notes
			, ''$'' + convert(varchar(30), cast(f.prim_res_est_mkt_value as money),1) as prim_res_est_mkt_value
			, f.counselor_id_ref, f.counselor_fname, f.counselor_lname, f.counselor_email, f.counselor_phone, f.counselor_ext
			, case when f.discussed_solution_with_srvcr_ind= ''Y'' then ''Yes'' when f.discussed_solution_with_srvcr_ind= ''N'' then ''No'' ELSE NULL END discussed_solution_with_srvcr_ind
			, case when f.worked_with_another_agency_ind= ''Y'' then ''Yes'' when f.worked_with_another_agency_ind= ''N'' then ''No'' ELSE NULL END worked_with_another_agency_ind
			, case when f.contacted_srvcr_recently_ind= ''Y'' then ''Yes'' when f.contacted_srvcr_recently_ind= ''N'' then ''No'' ELSE NULL END contacted_srvcr_recently_ind
			, case when f.has_workout_plan_ind= ''Y'' then ''Yes'' when f.has_workout_plan_ind= ''N'' then ''No'' ELSE NULL END has_workout_plan_ind
			, case when f.srvcr_workout_plan_current_ind= ''Y'' then ''Yes'' when f.srvcr_workout_plan_current_ind= ''N'' then ''No'' ELSE NULL END srvcr_workout_plan_current_ind
			, case when f.opt_out_newsletter_ind= ''Y'' then ''Yes'' when f.opt_out_newsletter_ind= ''N'' then ''No'' ELSE NULL END opt_out_newsletter_ind
			, case when f.opt_out_survey_ind= ''Y'' then ''Yes'' when f.opt_out_survey_ind= ''N'' then ''No'' ELSE NULL END opt_out_survey_ind
			, case when f.do_not_call_ind= ''Y'' then ''Yes'' when f.do_not_call_ind= ''N'' then ''No'' ELSE NULL END do_not_call_ind
			, case when f.owner_occupied_ind= ''Y'' then ''Yes'' when f.owner_occupied_ind= ''N'' then ''No'' ELSE NULL END owner_occupied_ind
			, case when f.primary_residence_ind= ''Y'' then ''Yes'' when f.primary_residence_ind= ''N'' then ''No'' ELSE NULL END primary_residence_ind
			, f.realty_company, cd7.code_desc as property_cd
			, case when f.for_sale_ind= ''Y'' then ''Yes'' when f.for_sale_ind= ''N'' then ''No'' ELSE NULL END for_sale_ind
			, ''$'' + convert(varchar(30), cast(f.home_sale_price as money),1) as home_sale_price
			, f.home_purchase_year
			, ''$'' + convert(varchar(30), cast(f.home_purchase_price as money),1) as home_purchase_price
			, ''$'' + convert(varchar(30), cast(f.home_current_market_value as money),1) as home_current_market_value
			, cd2.code_desc as military_service_cd
			, ''$'' + convert(varchar(30), cast(f.household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt
			, f.loan_list
			, f.intake_credit_score, f.intake_credit_bureau_cd			
			, case l.servicer_id when 12982 then l.other_servicer_name else s.servicer_name END Servicer_name
			, l.acct_num
			, cd6.code_desc as loan_1st_2nd_cd
			, cd8.code_desc as mortgage_type_cd
			, case when l.arm_reset_ind= ''Y'' then ''Yes'' when l.arm_reset_ind= ''N'' then ''No'' ELSE NULL END arm_reset_ind
			, l.term_length_cd
			, cd5.code_desc as loan_delinq_status_cd
			, ''$'' + convert(varchar(30), cast(l.current_loan_balance_amt as money),1) as current_loan_balance_amt
			, ''$'' + convert(varchar(30), cast(l.orig_loan_amt as money),1) as orig_loan_amt
			, l.interest_rate
			, l.originating_lender_name
			, l.orig_mortgage_co_FDIC_NCUA_num
			, l.orig_mortgage_co_name
			, l.orginal_loan_num
			, l.current_servicer_FDIC_NCUA_num			
			, cd9.code_desc as mortgage_program_cd
			, ''$'' + convert(varchar(30), cast(bs.total_income as money),1) as total_income
			, ''$'' + convert(varchar(30), cast(bs.total_expenses as money),1) as total_expenses
			, ''$'' + convert(varchar(30), cast(bs.total_income - bs.total_expenses as money),1) as total_surplus
	FROM	foreclosure_case f LEFT OUTER JOIN 
			(SELECT budget_set_id, fc_id, total_income,total_expenses, total_assets FROM budget_set 
			WHERE budget_set_id = (SELECT max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id)
			)bs ON f.fc_id = bs.fc_id
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name =''counseling duration code''	) cd1 ON cd1.code = f.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''military service code'') cd2 ON cd2.code = f.military_service_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd3 ON cd3.code = dflt_reason_1st_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd4 ON cd4.code = dflt_reason_2nd_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''property code'') cd7 ON cd7.code = f.property_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''household code'') cd10 ON cd10.code = f.household_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''income earners code'') cd11 ON cd11.code = f.income_earners_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''summary sent other code'') cd12 ON cd12.code = f.summary_sent_other_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''marital status code'') cd13 ON cd13.code = f.borrower_marital_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''gender code'') cd14 ON cd14.code = f.gender_cd
			, case_loan l
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan delinquency status code'') cd5 ON cd5.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan 1st 2nd code'') cd6 ON cd6.code = l.loan_1st_2nd_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''mortgage type code'') cd8 ON cd8.code = l.mortgage_type_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''mortgage program code'') cd9 ON cd9.code = l.mortgage_program_cd
			, servicer s, agency a			
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
			AND f.fc_id = @pi_fc_id  AND l.loan_1st_2nd_cd = ''1ST'';
END;

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_CompletedCounselingSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	 Hpf_rpt_CompletedCounselingSummary			
-- =============================================
CREATE PROCEDURE [dbo].[Hpf_rpt_CompletedCounselingSummary] (@pi_from_dt datetime, @pi_to_dt datetime, @pi_Agency_id int, @pi_program_id int)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

    -- Get all agencies
	IF (@pi_agency_id = -1)
		IF (@pi_program_id = -1) 
			SELECT	a.agency_name as Agency, l.acct_num as Loan
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer
					, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
					, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
			FROM	agency a, foreclosure_case f, case_loan l, servicer s
			WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
					AND f.completed_dt IS NOT NULL
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
					AND l.loan_1st_2nd_cd = ''1ST''
			ORDER BY a.agency_name, f.prop_state_cd, f.prop_city, f.prop_zip, f.loan_list;
		ELSE IF (@pi_program_id > 0)
			SELECT	a.agency_name as Agency, l.acct_num as Loan
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer
					, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
					, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
			FROM	agency a, foreclosure_case f, case_loan l, servicer s
			WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
					AND f.completed_dt IS NOT NULL
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
					AND l.loan_1st_2nd_cd = ''1ST''
					AND f.program_id = @pi_program_id
			ORDER BY a.agency_name, f.prop_state_cd, f.prop_city, f.prop_zip, f.loan_list;
	ELSE
    -- Get specific agency
		IF (@pi_agency_id > 0)
			IF (@pi_program_id = -1)
			SELECT	a.agency_name as Agency, l.acct_num as Loan				
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer
					, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
					, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
			FROM	agency a, foreclosure_case f, case_loan l, servicer s
			WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
					AND f.completed_dt IS NOT NULL
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
					AND l.loan_1st_2nd_cd = ''1ST''
					AND f.agency_id = @pi_agency_id
			ORDER BY a.agency_name, f.prop_state_cd, f.prop_city, f.prop_zip, f.loan_list;
			ELSE IF (@pi_program_id > 0)
			SELECT	a.agency_name as Agency, l.acct_num as Loan				
					, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer
					, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
					, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
			FROM	agency a, foreclosure_case f, case_loan l, servicer s
			WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
					AND f.completed_dt IS NOT NULL
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
					AND l.loan_1st_2nd_cd = ''1ST''
					AND f.agency_id = @pi_agency_id
					AND f.program_id = @pi_program_id
			ORDER BY a.agency_name, f.prop_state_cd, f.prop_city, f.prop_zip, f.loan_list;		
END







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_AgencyPaymentCheck]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_AgencyPaymentCheck	
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_AgencyPaymentCheck] 
	(@pi_agency_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
DECLARE @sql varchar(8000);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));
	IF @pi_agency_id > 0 
		SELECT	f.Fc_id, a.Agency_name
				, convert(varchar(20),f.Completed_dt, 101) as Completed_dt
				, f.Agency_case_num
				, convert(varchar(20),f.Intake_dt, 101) as Intake_dt
				, l.Acct_num
				, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
				, f.borrower_fname + '' '' + f.borrower_lname  as borrower_name
				, f.prop_addr2, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, convert(varchar(20),f.summary_sent_dt, 101) as summary_sent_dt
				, f.counselor_id_ref, f.counselor_fname + '' '' + f.counselor_lname as counselor_name
		FROM	foreclosure_case f LEFT OUTER JOIN (SELECT apc1.fc_id FROM agency_payable_case apc1, agency_payable ap1 
													WHERE ap1.agency_payable_id = apc1.agency_payable_id AND ap1.status_cd = ''ACTIVE''
													)apc ON f.fc_id = apc.fc_id
				, agency a, servicer s, case_loan l
		WHERE	f.fc_id = l.fc_id AND f.agency_id = a.agency_id AND l.servicer_id = s.servicer_id AND apc.fc_id IS NULL AND f.never_pay_reason_cd IS NULL 
				AND l.loan_1st_2nd_cd = ''1ST'' AND	f.completed_dt BETWEEN  @pi_from_dt AND @pi_to_dt 
				AND f.agency_id =@pi_agency_id 
		ORDER BY 	a.Agency_name, f.fc_id ;
	ELSE
		SELECT	f.Fc_id, a.Agency_name
				, convert(varchar(20),f.Completed_dt, 101) as Completed_dt
				, f.Agency_case_num
				, convert(varchar(20),f.Intake_dt, 101) as Intake_dt
				, l.Acct_num
				, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
				, f.borrower_fname + '' '' + f.borrower_lname  as borrower_name
				, f.prop_addr2, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, convert(varchar(20),f.summary_sent_dt, 101) as summary_sent_dt
				, f.counselor_id_ref, f.counselor_fname + '' '' + f.counselor_lname as counselor_name
		FROM	foreclosure_case f LEFT OUTER JOIN (SELECT apc1.fc_id FROM agency_payable_case apc1, agency_payable ap1 
													WHERE ap1.agency_payable_id = apc1.agency_payable_id AND ap1.status_cd = ''ACTIVE''
													) apc ON f.fc_id = apc.fc_id
				, agency a, servicer s, case_loan l
		WHERE	f.fc_id = l.fc_id AND f.agency_id = a.agency_id AND l.servicer_id = s.servicer_id AND apc.fc_id IS NULL AND f.never_pay_reason_cd IS NULL 
				AND l.loan_1st_2nd_cd = ''1ST'' AND	f.completed_dt BETWEEN  @pi_from_dt AND @pi_to_dt 
		ORDER BY 	a.Agency_name, f.fc_id ;
END;



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCounselingByServicer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 13 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	hpf_rpt_CompletedCounselingByServicer
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CompletedCounselingByServicer] 
	(@pi_servicer_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

	IF @pi_servicer_id >0
		SELECT	f.fc_id, l.acct_num
				, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
				, a.agency_name
				, convert(varchar(20),f.completed_dt, 101) as completed_dt
				, convert(varchar(20),f.intake_dt, 101) as intake_dt
				, f.borrower_fname, f.borrower_lname
				, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, convert(varchar(20),f.summary_sent_dt, 101) as summary_sent_dt
				, f.loan_list, f.funding_consent_ind, f.servicer_consent_ind
		FROM	foreclosure_case f, case_loan l, servicer s, agency a
		WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
				AND l.loan_1st_2nd_cd = ''1ST''
				AND (f.duplicate_ind IS NULL OR f.duplicate_ind = ''N'')
				AND f.never_bill_reason_cd IS NULL
				AND l.servicer_id = @pi_servicer_id
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
	IF @pi_servicer_id < 0
		SELECT	f.fc_id, l.acct_num
				, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
				, a.agency_name
				, convert(varchar(20),f.completed_dt, 101) as completed_dt
				, convert(varchar(20),f.intake_dt, 101) as intake_dt
				, f.borrower_fname, f.borrower_lname
				, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, convert(varchar(20),f.summary_sent_dt, 101) as summary_sent_dt
				, f.loan_list, f.funding_consent_ind, f.servicer_consent_ind		FROM	foreclosure_case f, case_loan l, servicer s, agency a
		WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
				AND l.loan_1st_2nd_cd = ''1ST''
				AND (f.duplicate_ind IS NULL OR f.duplicate_ind = ''N'')
				AND f.never_bill_reason_cd IS NULL
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
END;





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary For Agency - Get FC detail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_detail] 
	(@pi_fc_id int, @pi_agency_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT	f.fc_id, a.agency_name, f.call_id, f.program_id
			, f.agency_case_num, f.agency_client_num
			, convert(varchar(20), f.intake_dt, 101) intake_dt
			, cd11.code_desc as income_earners_cd, f.case_source_cd, f.race_cd, cd10.code_desc as household_cd
			, f.never_bill_reason_cd, f.never_pay_reason_cd
			, cd3.code_desc as dflt_reason_1st_cd, cd4.code_desc as dflt_reason_2nd_cd
			, f.hud_termination_reason_cd, convert(varchar(20), f.hud_termination_dt, 101) hud_termination_dt, f.hud_outcome_cd
			, f.AMI_percentage
			, cd1.code_desc as counseling_duration_cd
			, cd14.code_desc as gender_cd
			, f.borrower_fname, f.borrower_lname, f.borrower_mname, f.mother_maiden_lname
			, ''XXX-XX-'' + f.borrower_last4_SSN borrower_last4_SSN, convert(varchar(20), f.borrower_DOB, 101) borrower_DOB
			, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_mname
			, ''XXX-XX-'' + f.co_borrower_last4_SSN co_borrower_last4_SSN, convert(varchar(20), f.co_borrower_DOB, 101) co_borrower_DOB
			, f.primary_contact_no, f.second_contact_no
			, f.email_1, f.email_2
			, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip + isnull(''-'' + f.contact_zip_plus4, '''') as contact_zip
			, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip +  isnull(''-'' + f.prop_zip_plus_4, '''') as prop_zip
			, case when f.bankruptcy_ind = ''Y'' then ''Yes'' when f.bankruptcy_ind = ''N'' then ''No'' ELSE NULL END bankruptcy_ind
			, f.bankruptcy_attorney
			, case when f.bankruptcy_pmt_current_ind = ''Y'' then ''Yes'' when f.bankruptcy_pmt_current_ind = ''N'' then ''No'' ELSE NULL END bankruptcy_pmt_current_ind
			, f.borrower_educ_level_completed_cd
			, cd13.code_desc as borrower_marital_status_cd
			, f.borrower_preferred_lang_cd, f.borrower_occupation
			, f.co_borrower_occupation
			, case when f.hispanic_ind= ''Y'' then ''Yes'' when f.hispanic_ind= ''N'' then ''No'' ELSE NULL END hispanic_ind
			, case when f.duplicate_ind= ''Y'' then ''Yes'' when f.duplicate_ind= ''N'' then ''No'' ELSE NULL END duplicate_ind
			, case when f.fc_notice_received_ind= ''Y'' then ''Yes'' when f.fc_notice_received_ind= ''N'' then ''No'' ELSE NULL END fc_notice_received_ind
			, convert(varchar(20), f.fc_sale_dt, 101) fc_sale_dt
			, convert(varchar(20), f.completed_dt, 101)  completed_dt
			, case when f.funding_consent_ind= ''Y'' then ''Yes'' when f.funding_consent_ind= ''N'' then ''No'' ELSE NULL END funding_consent_ind
			, case when f.servicer_consent_ind= ''Y'' then ''Yes'' when f.servicer_consent_ind= ''N'' then ''No'' ELSE NULL END servicer_consent_ind
			, case when f.agency_media_interest_ind= ''Y'' then ''Yes'' when f.agency_media_interest_ind= ''N'' then ''No'' ELSE NULL END agency_media_interest_ind
			, case when f.hpf_media_candidate_ind= ''Y'' then ''Yes'' when f.hpf_media_candidate_ind= ''N'' then ''No'' ELSE NULL END hpf_media_candidate_ind
			, case when f.hpf_success_story_ind= ''Y'' then ''Yes'' when f.hpf_success_story_ind= ''N'' then ''No'' ELSE NULL END hpf_success_story_ind
			, case when f.agency_success_story_ind= ''Y'' then ''Yes'' when f.agency_success_story_ind= ''N'' then ''No'' ELSE NULL END agency_success_story_ind
			, case when f.borrower_disabled_ind= ''Y'' then ''Yes'' when f.borrower_disabled_ind= ''N'' then ''No'' ELSE NULL END borrower_disabled_ind
			, case when f.co_borrower_disabled_ind= ''Y'' then ''Yes'' when f.co_borrower_disabled_ind= ''N'' then ''No'' ELSE NULL END co_borrower_disabled_ind
			, cd12.code_desc as summary_sent_other_cd, convert(varchar(20), f.summary_sent_other_dt, 101) as summary_sent_other_dt, convert(varchar(20), f.summary_sent_dt, 101) summary_sent_dt
			, f.occupant_num
			, f.loan_dflt_reason_notes, f.action_items_notes, f.followup_notes
			, ''$'' + convert(varchar(30), cast(f.prim_res_est_mkt_value as money),1) as prim_res_est_mkt_value
			, f.counselor_id_ref, f.counselor_fname, f.counselor_lname, f.counselor_email, f.counselor_phone, f.counselor_ext
			, case when f.discussed_solution_with_srvcr_ind= ''Y'' then ''Yes'' when f.discussed_solution_with_srvcr_ind= ''N'' then ''No'' ELSE NULL END discussed_solution_with_srvcr_ind
			, case when f.worked_with_another_agency_ind= ''Y'' then ''Yes'' when f.worked_with_another_agency_ind= ''N'' then ''No'' ELSE NULL END worked_with_another_agency_ind
			, case when f.contacted_srvcr_recently_ind= ''Y'' then ''Yes'' when f.contacted_srvcr_recently_ind= ''N'' then ''No'' ELSE NULL END contacted_srvcr_recently_ind
			, case when f.has_workout_plan_ind= ''Y'' then ''Yes'' when f.has_workout_plan_ind= ''N'' then ''No'' ELSE NULL END has_workout_plan_ind
			, case when f.srvcr_workout_plan_current_ind= ''Y'' then ''Yes'' when f.srvcr_workout_plan_current_ind= ''N'' then ''No'' ELSE NULL END srvcr_workout_plan_current_ind
			, case when f.opt_out_newsletter_ind= ''Y'' then ''Yes'' when f.opt_out_newsletter_ind= ''N'' then ''No'' ELSE NULL END opt_out_newsletter_ind
			, case when f.opt_out_survey_ind= ''Y'' then ''Yes'' when f.opt_out_survey_ind= ''N'' then ''No'' ELSE NULL END opt_out_survey_ind
			, case when f.do_not_call_ind= ''Y'' then ''Yes'' when f.do_not_call_ind= ''N'' then ''No'' ELSE NULL END do_not_call_ind
			, case when f.owner_occupied_ind= ''Y'' then ''Yes'' when f.owner_occupied_ind= ''N'' then ''No'' ELSE NULL END owner_occupied_ind
			, case when f.primary_residence_ind= ''Y'' then ''Yes'' when f.primary_residence_ind= ''N'' then ''No'' ELSE NULL END primary_residence_ind
			, f.realty_company, cd7.code_desc as property_cd
			, case when f.for_sale_ind= ''Y'' then ''Yes'' when f.for_sale_ind= ''N'' then ''No'' ELSE NULL END for_sale_ind
			, ''$'' + convert(varchar(30), cast(f.home_sale_price as money),1) as home_sale_price
			, f.home_purchase_year
			, ''$'' + convert(varchar(30), cast(f.home_purchase_price as money),1) as home_purchase_price
			, ''$'' + convert(varchar(30), cast(f.home_current_market_value as money),1) as home_current_market_value
			, cd2.code_desc as military_service_cd
			, ''$'' + convert(varchar(30), cast(f.household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt
			, f.loan_list
			, f.intake_credit_score, f.intake_credit_bureau_cd			
			, case l.servicer_id when 12982 then l.other_servicer_name else s.servicer_name END Servicer_name
			, l.acct_num
			, cd6.code_desc as loan_1st_2nd_cd
			, cd8.code_desc as mortgage_type_cd
			, case when l.arm_reset_ind= ''Y'' then ''Yes'' when l.arm_reset_ind= ''N'' then ''No'' ELSE NULL END arm_reset_ind
			, l.term_length_cd
			, cd5.code_desc as loan_delinq_status_cd
			, ''$'' + convert(varchar(30), cast(l.current_loan_balance_amt as money),1) as current_loan_balance_amt
			, ''$'' + convert(varchar(30), cast(l.orig_loan_amt as money),1) as orig_loan_amt
			, l.interest_rate
			, l.originating_lender_name
			, l.orig_mortgage_co_FDIC_NCUA_num
			, l.orig_mortgage_co_name
			, l.orginal_loan_num
			, l.current_servicer_FDIC_NCUA_num			
			, cd9.code_desc as mortgage_program_cd
			, ''$'' + convert(varchar(30), cast(bs.total_income as money),1) as total_income
			, ''$'' + convert(varchar(30), cast(bs.total_expenses as money),1) as total_expenses
			, ''$'' + convert(varchar(30), cast(bs.total_income - bs.total_expenses as money),1) as total_surplus
	FROM	foreclosure_case f LEFT OUTER JOIN 
			(SELECT budget_set_id, fc_id, total_income,total_expenses, total_assets FROM budget_set 
			WHERE budget_set_id = (SELECT max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id)
			)bs ON f.fc_id = bs.fc_id
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name =''counseling duration code''	) cd1 ON cd1.code = f.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''military service code'') cd2 ON cd2.code = f.military_service_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd3 ON cd3.code = dflt_reason_1st_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''default reason code'') cd4 ON cd4.code = dflt_reason_2nd_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''property code'') cd7 ON cd7.code = f.property_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''household code'') cd10 ON cd10.code = f.household_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''income earners code'') cd11 ON cd11.code = f.income_earners_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''summary sent other code'') cd12 ON cd12.code = f.summary_sent_other_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''marital status code'') cd13 ON cd13.code = f.borrower_marital_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''gender code'') cd14 ON cd14.code = f.gender_cd
			, case_loan l
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan delinquency status code'') cd5 ON cd5.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''loan 1st 2nd code'') cd6 ON cd6.code = l.loan_1st_2nd_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''mortgage type code'') cd8 ON cd8.code = l.mortgage_type_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = ''mortgage program code'') cd9 ON cd9.code = l.mortgage_program_cd
			, servicer s, agency a			
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
			AND f.fc_id = @pi_fc_id  AND l.loan_1st_2nd_cd = ''1ST''
			AND f.agency_id = @pi_agency_id;
END;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HPFStandard_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 27 Feb 2009
-- Description:	HPF REPORT - R56 - Invoice Export File - HPF Standard (detail)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HPFStandard_detail] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	ic.fc_id, ic.invoice_case_id, ic.invoice_case_bill_amt
			, s.servicer_name, l.acct_num as Loan_Number
			, f.borrower_lname, f.borrower_fname, f.borrower_mname
			, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
			, f.primary_contact_no, f.second_contact_no
			, convert(varchar(20), f.create_dt, 101) as create_dt
			, convert(varchar(20), f.completed_dt, 101) as completed_dt
			, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
			, a.agency_name
	FROM	invoice_case ic, servicer s, foreclosure_case f, case_loan l, agency a
	WHERE	ic.fc_id = f.fc_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
			AND l.loan_1st_2nd_cd = ''1ST''
			AND ic.invoice_id = @pi_invoice_id		
	ORDER BY servicer_name ASC, acct_num ASC;
END ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_ExternalReferrals]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_ExternalReferrals	
-- =============================================
CREATE PROCEDURE [dbo].[Hpf_rpt_ExternalReferrals] 
	(@pi_agency_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
DECLARE @sql varchar(8000), @v_total_cases numeric(15,2);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);

	IF (@pi_agency_id > 0 )
	BEGIN
		SELECT	@v_total_cases = count(*)
		FROM	(SELECT DISTINCT oi1.outcome_type_id, f.fc_id, f.agency_id
				FROM outcome_item oi1, foreclosure_case f
				WHERE oi1.fc_id = f.fc_id 
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
					AND	f.agency_id = @pi_agency_id
				) oi
				, outcome_type ot, agency a
		WHERE	oi.outcome_type_id = ot.outcome_type_id 
				AND oi.agency_id = a.agency_id;

		SELECT	DISTINCT a.agency_name, ot.outcome_type_name, cast(@v_total_cases as int) as Total
				, count(oi.fc_id) as Total_cases
				, cast((count(oi.fc_id)/@v_total_cases)*100 as numeric(15,2)) as Reffered
		FROM	(SELECT DISTINCT oi1.outcome_type_id, f.fc_id, f.agency_id
				FROM outcome_item oi1, foreclosure_case f
				WHERE oi1.fc_id = f.fc_id 
					AND oi1.outcome_deleted_dt IS NULL
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
					AND	f.agency_id = @pi_agency_id
				) oi
				, outcome_type ot, agency a
		WHERE	oi.outcome_type_id = ot.outcome_type_id 
				AND oi.agency_id = a.agency_id			
		GROUP BY	a.agency_name, ot.outcome_type_name
		ORDER BY a.agency_name, ot.outcome_type_name;
	END;

	IF (@pi_agency_id =-1 )
	BEGIN
		SELECT	@v_total_cases = count(*)
		FROM	(SELECT DISTINCT oi1.outcome_type_id, f.fc_id, f.agency_id
				FROM outcome_item oi1, foreclosure_case f
				WHERE oi1.fc_id = f.fc_id 
					AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				) oi
				, outcome_type ot, agency a
		WHERE	oi.outcome_type_id = ot.outcome_type_id 
				AND oi.agency_id = a.agency_id;

		SELECT	DISTINCT a.agency_name, ot.outcome_type_name, cast(@v_total_cases as int) as Total
				, count(oi.fc_id) as Total_cases
				, cast((count(oi.fc_id)/@v_total_cases)*100 as numeric(15,2)) as Reffered
		FROM	(SELECT DISTINCT oi1.outcome_type_id, f.fc_id, f.agency_id
				FROM	outcome_item oi1, foreclosure_case f
				WHERE	oi1.fc_id = f.fc_id 
						AND oi1.outcome_deleted_dt IS NULL
						AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				) oi
				, outcome_type ot, agency a
		WHERE	oi.outcome_type_id = ot.outcome_type_id 
				AND oi.agency_id = a.agency_id			
		GROUP BY	a.agency_name, ot.outcome_type_name
		ORDER BY a.agency_name, ot.outcome_type_name;
	END;
END;







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Outcome]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary For Agency- Get FC Outcome
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Outcome] 
	(@pi_fc_id int, @pi_agency_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @v_fc_id_correct_agency_id int;

	SELECT @v_fc_id_correct_agency_id = count(fc_id) 
	FROM foreclosure_case WHERE fc_id = @pi_fc_id AND agency_id = @pi_agency_id;

	IF @v_fc_id_correct_agency_id > 0 
	BEGIN
		SELECT	t.outcome_type_name, convert(varchar(20), i.outcome_dt, 101)as outcome_dt , i.nonprofitreferral_key_num, i.ext_ref_other_name
		FROM	outcome_item i, outcome_type t
		WHERE	i.outcome_type_id = t.outcome_type_id
				AND	i.outcome_deleted_dt IS NULL
				AND i.fc_id = @pi_fc_id
	END;
	IF @v_fc_id_correct_agency_id = 0 
		SELECT	NULL outcome_type_name, NULL as outcome_dt , NULL nonprofitreferral_key_num, NULL ext_ref_other_name
END;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Outcome]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary - Get FC Outcome
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Outcome] 
	(@pi_fc_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT	t.outcome_type_name, convert(varchar(20), i.outcome_dt, 101)as outcome_dt , i.nonprofitreferral_key_num, i.ext_ref_other_name
	FROM	outcome_item i, outcome_type t
	WHERE	i.outcome_type_id = t.outcome_type_id
			AND	i.outcome_deleted_dt IS NULL
			AND i.fc_id = @pi_fc_id
END;

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyAverageCompletionReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_DailyCompletionReport	
--				Report name : Daily Completion Report			
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_DailyAverageCompletionReport] (@pi_Mon int, @pi_Year int)
AS
DECLARE	@v_TotalDay_MonFri int, @v_TotalDay_Sun int, @v_TotalDay_Sat int, @v_Total_Week int;
DECLARE @v_start_dt datetime, @v_end_dt datetime;	
CREATE TABLE #t_Daily
	(Agency_id	int,
	Agency_name	varchar(50),	
	MonFri		float,
	MonFri_total int,
	Sat			float,
	Sat_Total	int,
	Sun			float,
	Sun_Total	int,
	Weekly		float,
	Weekly_Total	int);
BEGIN
	-- Set Date range of a month
	SELECT @v_start_dt = CAST(cast(@pi_Mon as varchar) + ''-01-''+ cast(@pi_year as varchar) as datetime)
	If (Month(getdate()) = @pi_Mon ) 
		SELECT @v_end_dt = getdate();
	ELSE
		BEGIN
			If (@pi_mon in (1,3,5,7,8,10,12))
				SELECT @v_end_dt = CAST(cast(@pi_Mon as varchar) + ''-31-'' +  cast(@pi_year as varchar) as datetime);
			If (@pi_mon in (4,6,9,11))
				SELECT @v_end_dt = CAST(cast(@pi_Mon as varchar) + ''-30-'' +  cast(@pi_year as varchar) as datetime);
			If (@pi_mon= 2 and @pi_year%4 = 0)
				SELECT @v_end_dt = CAST(cast(@pi_Mon as varchar) + ''-29-'' +  cast(@pi_year as varchar) as datetime);
			If (@pi_mon= 2 and @pi_year%4 <> 0)
				SELECT @v_end_dt = CAST(cast(@pi_Mon as varchar) + ''-28-'' +  cast(@pi_year as varchar) as datetime);
		END;
	-- Calculation Total week day 
	SELECT @v_TotalDay_Sun =0;
	SELECT @v_TotalDay_MonFri =0;
	SELECT @v_TotalDay_Sat =0;
	SELECT	@v_Total_Week =0;

	EXECUTE hpf_rpt_count_day  1, @v_start_dt, @v_end_dt,@v_TotalDay_Sun OUTPUT;
	EXECUTE hpf_rpt_count_day  2, @v_start_dt, @v_end_dt,@v_TotalDay_MonFri OUTPUT;
	EXECUTE hpf_rpt_count_day  7, @v_start_dt, @v_end_dt,@v_TotalDay_Sat OUTPUT;
	
	SELECT	@v_Total_Week = datediff(week, @v_start_dt, @v_end_dt);
	
	IF @v_TotalDay_Sun = 0 SELECT @v_TotalDay_Sun = NULL;
	IF @v_TotalDay_MonFri = 0 SELECT @v_TotalDay_MonFri = NULL;
	IF @v_TotalDay_Sat = 0 SELECT @v_TotalDay_Sat = NULL;
	IF @v_Total_Week =	0 SELECT	@v_Total_Week = NULL;

	-- Insert #t_daily -> agency
	INSERT INTO #t_Daily (Agency_id, Agency_name)
		SELECT agency_id, Agency_name FROM agency;
 
	UPDATE	#t_Daily
	SET		MonFri	= round(cast(t.num_MonFri as float)/ @v_TotalDay_MonFri, 2)
			, MonFri_Total	= t.num_MonFri
			, Sun	= round(cast(t1.num_Sun as float)/ @v_TotalDay_Sun, 2)
			, Sun_Total = t1.num_Sun
			, Sat	= round(cast(t7.num_Sat as float)/ @v_TotalDay_Sat, 2)
			, Sat_Total = t7.num_Sat
			, Weekly = round(cast(tw.num_week as float)/ @v_Total_Week, 2)
			, Weekly_Total = tw.num_week
	FROM	#t_Daily
			INNER JOIN 
				(SELECT	Agency_id, count(fc_id) as num_MonFri
				FROM	foreclosure_case f
				WHERE	datepart(weekday, completed_dt) in (2,3,4,5,6)
						AND completed_dt BETWEEN @v_start_dt AND @v_end_dt
				GROUP BY Agency_id) t
			ON #t_Daily.agency_id = t.Agency_id
			INNER JOIN
				(SELECT	Agency_id, count(fc_id) as num_Sun
				FROM	foreclosure_case f
				WHERE	datepart(weekday, completed_dt) = 1
						AND completed_dt BETWEEN @v_start_dt AND @v_end_dt
				GROUP BY Agency_id) t1
			ON #t_Daily.agency_id = t1.Agency_id
			INNER JOIN
				(SELECT	Agency_id, count(fc_id) as num_Sat
				FROM	foreclosure_case f
				WHERE	datepart(weekday, completed_dt) = 7
						AND completed_dt BETWEEN @v_start_dt AND @v_end_dt
				GROUP BY Agency_id) t7
			ON #t_Daily.agency_id = t7.Agency_id
			INNER JOIN
				(SELECT	Agency_id, count(fc_id) as num_Week
				FROM	foreclosure_case f
				WHERE	completed_dt BETWEEN @v_start_dt AND @v_end_dt
				GROUP BY Agency_id) tw
			ON #t_Daily.agency_id = tw.Agency_id
			;

--SELECT ''@v_TotalDay_Sun='' + cast(isnull(@v_TotalDay_Sun, '' '') as varchar) + '',@v_TotalDay_Sat='' + cast(isnull(@v_TotalDay_Sat, '' '') as varchar) + '',@v_TotalDay_MonFri= '' + cast(isnull(@v_TotalDay_MonFri, '' '') as varchar)+ '', @v_Total_Week= '' + cast (isnull(@v_Total_Week, '' '') as varchar)

	SELECT	Agency_id, Agency_name
		, isnull(MonFri, 0) as MonFri, isnull(Sat, 0) as Sat, isnull(Sun, 0) as Sun, isnull(Weekly, 0) as Weekly
		, isnull(MonFri_Total, 0) as MonFri_Total, isnull(Sat_Total, 0) as Sat_Total, isnull(Sun_Total, 0) as Sun_Total, isnull(Weekly_Total, 0) as Weekly_Total
	FROM	#t_Daily
	ORDER BY Agency_name;
	DROP TABLE #t_Daily;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CaseSource]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_CaseSource	
--				Report name : Case Source	
--				@pi_range_type = 1 --> Month
--				@pi_range_type = 2 --> Quarter
--				@pi_range_type = 3 --> Year		
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CaseSource] 
	(@pi_State_cd varchar(15), @pi_MSA_cd varchar(15), @pi_congressional_dist varchar(15)
	, @pi_range_type int, @pi_Mon int, @pi_Quarter int, @pi_Year int)
AS
DECLARE @v_start_dt datetime, @v_end_dt datetime, @v_total int;
BEGIN
	-- Step 1: Calculate the Date range
	-- IF selects MONTH
	IF (@pi_range_type = 1)
		EXECUTE hpf_rpt_get_dateRange @pi_range_type, @pi_Mon, @pi_year, @v_start_dt OUTPUT, @v_end_dt OUTPUT;
	-- IF selects QUARTER
	IF (@pi_range_type = 2)
		EXECUTE hpf_rpt_get_dateRange @pi_range_type, @pi_Quarter, @pi_year, @v_start_dt OUTPUT, @v_end_dt OUTPUT;
	-- IF selects YEAR
	IF (@pi_range_type = 3)
		EXECUTE hpf_rpt_get_DateRange @pi_range_type, @pi_year, @pi_year, @v_start_dt OUTPUT, @v_end_dt OUTPUT;

	-- SELECT 	cast(@v_start_dt as varchar) + '' -> '' + cast(@v_end_dt as varchar);
	
	-- Step 2: get output
	-- IF Report = Case Source
	IF (@pi_State_cd IS NULL AND @pi_MSA_cd IS NULL AND @pi_congressional_dist IS NULL)
	BEGIN
		SELECT	@v_Total = count(fc_id)
		FROM	foreclosure_case f
		WHERE	f.completed_dt between @v_start_dt AND @v_end_dt;

		SELECT	f.case_source_cd as Case_source, count(fc_id) as Completed_case_qty, round(cast(count(fc_id) as float)*100/@v_Total, 2) as Percentage
		FROM	foreclosure_case f
		WHERE	f.completed_dt between @v_start_dt AND @v_end_dt
		GROUP BY f.case_source_cd;
	END;
END;



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CaseFundingSourceSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	R09 - Hpf_rpt_CaseFundingSourceSummary
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CaseFundingSourceSummary] 
	(@pi_agency_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
DECLARE @sql varchar(8000);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

	IF (@pi_agency_id > 0)
		SELECT	a.agency_name, fs.funding_source_name, count(invoice_case_id) total_invoice_case
--				, convert(varchar(30), cast(Sum(invoice_case_pmt_amt) as money), 1) as pmt_amt
				, Sum(invoice_case_pmt_amt) as pmt_amt
		FROM	agency a, funding_source fs, invoice i, invoice_case ic, foreclosure_case f, invoice_payment ip
		WHERE	a.agency_id = f.agency_id
				AND f.fc_id = ic.fc_id
				AND ic.invoice_id = i.invoice_id
				AND i.funding_source_id = fs.funding_source_id
				AND ic.invoice_payment_id = ip.invoice_payment_id
				AND ic.invoice_case_pmt_amt > 0
				AND f.completed_dt IS NOT NULL
				AND ip.pmt_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND f.agency_id = @pi_agency_id
		GROUP BY a.agency_name, fs.funding_source_name
		ORDER BY 1, 2;
	IF (@pi_agency_id < 0)
		SELECT	a.agency_name, fs.funding_source_name, count(invoice_case_id) total_invoice_case
--				, convert(varchar(30), cast(Sum(invoice_case_pmt_amt) as money), 1) as pmt_amt
				, Sum(invoice_case_pmt_amt) as pmt_amt
		FROM	agency a, funding_source fs, invoice i, invoice_case ic, foreclosure_case f, invoice_payment ip
		WHERE	a.agency_id = f.agency_id
				AND f.fc_id = ic.fc_id
				AND ic.invoice_id = i.invoice_id
				AND i.funding_source_id = fs.funding_source_id
				AND ic.invoice_payment_id = ip.invoice_payment_id
				AND ic.invoice_case_pmt_amt > 0
				AND f.completed_dt IS NOT NULL
				AND ip.pmt_dt BETWEEN @pi_from_dt AND @pi_to_dt
		GROUP BY a.agency_name, fs.funding_source_name
		ORDER BY 1, 2;
END;








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableSummary_header]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Mar 2009
-- Description:	HPF REPORT - R62 - Payable Summary - Get Payable header
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_PayableSummary_header] 
	(@pi_agency_payable_id integer)
AS
BEGIN
	SELECT	ap.agency_payable_id
			, convert(varchar(20), ap.pmt_dt, 107) as pmt_dt
			, a.agency_name
			, a.finance_contact_fname + '' '' + a.finance_contact_lname as contact_name
			, a.finance_addr1 , a.finance_addr2
			, a.finance_city + '','' + a.finance_state_cd + '' '' + a.finance_zip + isnull(''-'' + a.finance_zip_plus_4, '' '') as City_info
			, a.finance_phone
			, a.finance_email
			, total.item_total
	FROM	agency_payable ap, agency a
			, (	SELECT ''$'' + convert(varchar(30), cast(isnull(SUM(t.item_total), 0)  as money),1) as item_total
				FROM	(SELECT count(fc_id)* pmt_amt as item_total
						FROM	agency_payable_case apc1
						WHERE	agency_payable_id = @pi_agency_payable_id
						GROUP BY pmt_amt) t
			)total
	WHERE	ap.agency_id = a.agency_id
			AND ap.agency_payable_id = @pi_agency_payable_id;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Budget]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary - Get FC Budget
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Budget] 
	(@pi_fc_id int)
AS
DECLARE @v_max_budget_set_id int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

	SELECT	@v_max_budget_set_id = max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id;

	SELECT	c.budget_category_name, sc.budget_subcategory_name
			, ''$'' + convert(varchar(30), cast(i.budget_item_amt as money),1) budget_item_amt
			, i.budget_note
			, ''$'' + convert(varchar(30), cast(isnull(bi.SUM_budget_item_amt, 0)  as money),1) SUM_budget_item_amt
	FROM	budget_category c 
			INNER JOIN budget_subcategory sc ON sc.budget_category_id = c.budget_category_id
			LEFT OUTER JOIN (SELECT bs1.budget_category_id, sum(bi1.budget_item_amt) as SUM_budget_item_amt
						FROM	budget_item bi1, budget_subcategory bs1
						WHERE	bi1.budget_subcategory_id = bs1.budget_subcategory_id 
								AND bi1.budget_set_id = @v_max_budget_set_id
						GROUP BY bs1.budget_category_id
						) bi ON bi.budget_category_id = sc.budget_category_id
			LEFT OUTER JOIN 
			(SELECT i1.budget_set_id, i1.budget_item_amt, i1.budget_note, i1.budget_subcategory_id
			FROM	budget_item i1 
			WHERE	i1.budget_set_id = @v_max_budget_set_id
			) i ON  i.budget_subcategory_id = sc.budget_subcategory_id	AND i.budget_set_id = @v_max_budget_set_id
	ORDER BY c.sort_order, sc.sort_order;
END;








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary For Agency- Get FC Budget
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget] 
	(@pi_fc_id int, @pi_agency_id int)
AS
DECLARE @v_max_budget_set_id int
BEGIN	
	SET NOCOUNT ON;
	DECLARE @v_fc_id_correct_agency_id int;

	SELECT @v_fc_id_correct_agency_id = count(fc_id) 
	FROM foreclosure_case WHERE fc_id = @pi_fc_id AND agency_id = @pi_agency_id;

	IF @v_fc_id_correct_agency_id > 0 
	BEGIN
		SELECT	@v_max_budget_set_id = max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id;

		SELECT	c.budget_category_name, sc.budget_subcategory_name
				, ''$'' + convert(varchar(30), cast(i.budget_item_amt as money),1) budget_item_amt
				, i.budget_note
				, ''$'' + convert(varchar(30), cast(isnull(bi.SUM_budget_item_amt, 0)  as money),1) SUM_budget_item_amt
		FROM	budget_category c 
				INNER JOIN budget_subcategory sc ON sc.budget_category_id = c.budget_category_id
				LEFT OUTER JOIN (SELECT bs1.budget_category_id, sum(bi1.budget_item_amt) as SUM_budget_item_amt
							FROM	budget_item bi1, budget_subcategory bs1
							WHERE	bi1.budget_subcategory_id = bs1.budget_subcategory_id 
									AND bi1.budget_set_id = @v_max_budget_set_id
							GROUP BY bs1.budget_category_id
							) bi ON bi.budget_category_id = sc.budget_category_id
				LEFT OUTER JOIN 
				(SELECT i1.budget_set_id, i1.budget_item_amt, i1.budget_note, i1.budget_subcategory_id
				FROM	budget_item i1 
				WHERE	i1.budget_set_id = @v_max_budget_set_id
				) i ON  i.budget_subcategory_id = sc.budget_subcategory_id	AND i.budget_set_id = @v_max_budget_set_id
		ORDER BY c.sort_order, sc.sort_order;
	END;

	IF @v_fc_id_correct_agency_id = 0 
		SELECT	NULL budget_category_name, NULL budget_subcategory_name
				, NULL  budget_item_amt
				, NULL budget_note
				, NULL SUM_budget_item_amt		
END;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceSummary_header]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 26 Feb 2009
-- Description:	HPF REPORT - R55 - Invoice Summary - Get Invoice header
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_header] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	i.invoice_id
			, convert(varchar(20), i.invoice_dt, 107) as invoice_dt
			, fs.funding_source_name
			, fs.contact_fname + '' '' + fs.contact_lname as contact_name
			, fs.billing_addr_1 , fs.billing_addr_2
			, fs.city + '', '' + fs.state_cd + '' '' + fs.zip + isnull(''-'' + fs.zip_plus_4, '' '') as City_info
			, fs.billing_email
			, fs.phone as phone
			, total.item_total
	FROM	invoice i, funding_source fs
			, (	SELECT ''$'' + convert(varchar(30), cast(isnull(SUM(t.item_total), 0)  as money),1) as item_total
				FROM	(SELECT count(ic.fc_id)* ic.invoice_case_bill_amt as item_total
						FROM	invoice_case ic
						WHERE	ic.invoice_id = @pi_invoice_id
						GROUP BY ic.invoice_case_bill_amt) t
			)total
	WHERE	fs.funding_source_id = i.funding_source_id
			AND i.invoice_id = @pi_invoice_id;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_header]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 27 Feb 2009
-- Description:	HPF REPORT - R56 + R57 + R61 - Invoice Export File - HPF Standard + HSBC + HUD(header)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_header] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	i.invoice_id
			, convert(varchar(20), i.invoice_dt, 107) as invoice_dt
			, count(fc_id) Total_sessions
			, convert(varchar(20), i.period_start_dt, 101) as period_start_dt
			, convert(varchar(20), i.period_end_dt, 101) as period_end_dt	
	FROM	invoice i, invoice_case ic
	WHERE	i.invoice_id = ic.invoice_id
			AND i.invoice_id = @pi_invoice_id
	GROUP BY i.invoice_id, i.invoice_dt, i.period_start_dt, i.period_end_dt;
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCasesByState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_CompletedCasesByState	
--				Report name : Completed Cases By State			
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CompletedCasesByState]
	(@pi_year integer)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	Jan.state, isnull(jan.Number, 0) as Jan, isnull(feb.Number,0) as Feb,  isnull(mar.Number, 0) as Mar, isnull(apr.Number,0) as Apr, isnull(May.Number,0) as May, isnull(Jun.Number,0) as Jun
			, isnull(Jul.Number,0) as Jul, isnull(Aug.Number,0) as Aug, isnull(sep.Number, 0) as Sep, isnull(oct.Number, 0) as Oct, isnull(nov.Number,0) as Nov, isnull(dec.Number, 0) as Dec
	FROM	(Select	a.state, year(f1.completed_dt) as FC_Year, count(f1.fc_id) as Number
			From	foreclosure_case f1 RIGHT OUTER JOIN 
					(Select code as state from ref_code_item where ref_code_set_name = ''state code'') a ON a.state = f1.prop_state_cd
					AND month(f1.completed_dt) = 1	and  year(completed_dt) = @pi_year
			Group by a.state, year(f1.completed_dt), month(f1.completed_dt)) as Jan
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 2	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Feb
			ON jan.state = Feb.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 3	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Mar
			ON jan.state = Mar.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 4	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Apr
			ON jan.state = Apr.state
			LEFT OUTER JOIN		
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 5	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as May
			ON jan.state = May.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 6	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Jun
			ON jan.state = Jun.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 7	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Jul
			ON jan.state = Jul.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 8	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Aug
			ON jan.state = Aug.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 9	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Sep
			ON jan.state = Sep.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 10	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Oct
			ON jan.state = Oct.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 11	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Nov
			ON jan.state = Nov.state
			LEFT OUTER JOIN
			(Select	f.prop_state_cd as state, count(fc_id) as Number	From	foreclosure_case f 
			Where	month(f.completed_dt) = 12	and  year(completed_dt) = @pi_year
			Group by f.prop_state_cd, month(f.completed_dt)) as Dec
			ON jan.state = Dec.state		
	order by 1;
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_MHA_Report]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 22 Jun 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_MHA_Report
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_MHA_Report] 
	(@pi_from_dt datetime, @pi_to_dt datetime)
AS
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);

	SELECT	mha_eligibility_cd, mha_inelig_reason_cd, r.code_desc as Final_Disposition_cd, COUNT(*) as Quantity
	FROM	call c, ref_code_item r
	WHERE	c.create_dt BETWEEN  @pi_from_dt AND @pi_to_dt	
			AND call_center_name in (''WLT'', ''LK2'')
			AND r.ref_code_set_name = ''final disposition code''	AND c.final_dispo_cd = r.code
	GROUP BY mha_eligibility_cd, mha_inelig_reason_cd, r.code_desc
	ORDER BY Quantity desc;
END;' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_MHADailySummaryReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 17 Jun 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_MHADailySummaryReport	
-- =============================================
CREATE PROCEDURE [dbo].[Hpf_rpt_MHADailySummaryReport] 
	(@pi_from_dt datetime, @pi_to_dt datetime)
AS
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
	SELECT	dt.call_dt, isnull(HARP.qty, 0) as HARP, isnull(NOTHARP.qty, 0) as NOT_HARP
		, isnull(HAMP.qty, 0) as HAMP, isnull(NOTHAMP.qty, 0) as NOT_HAMP
		, isnull(MHAINELIG.qty, 0) as MHA_INELIG, isnull(UNK.qty, 0) as UNKNOWN
	FROM
		(SELECT	distinct cast(c.start_dt as varchar(11))as call_dt FROM call c
		WHERE	c.start_dt between @pi_from_dt and @pi_to_dt
		) as dt 
		LEFT OUTER JOIN
		(SELECT	c.call_dt, isnull(c.qty, 0) as Qty
		FROM	(SELECT	cast(c.start_dt as varchar(11))as call_dt
						, r.code_desc as mha_eligibility_cd, count(call_id) as qty
				FROM	(SELECT code, code_desc, sort_order FROM ref_code_item WHERE ref_code_set_name = ''mha eligibility code'') r 
						INNER JOIN call c 	ON c.mha_eligibility_cd = r.code AND r.code = ''HARP''
				WHERE	c.start_dt between @pi_from_dt and @pi_to_dt	AND mha_eligibility_cd is not null
				GROUP BY cast(c.start_dt as varchar(11)) , r.code_desc ) c			
		) as harp ON dt.call_dt = harp.call_dt
		LEFT OUTER JOIN
		(SELECT	c.call_dt, isnull(c.qty, 0) as Qty
		FROM	(SELECT	cast(c.start_dt as varchar(11))as call_dt
						, r.code_desc as mha_eligibility_cd, count(call_id) as qty
				FROM	(SELECT code, code_desc, sort_order FROM ref_code_item WHERE ref_code_set_name = ''mha eligibility code'') r 
						INNER JOIN call c 	ON c.mha_eligibility_cd = r.code AND r.code = ''HAMP''
				WHERE	c.start_dt between @pi_from_dt and @pi_to_dt	AND mha_eligibility_cd is not null
				GROUP BY cast(c.start_dt as varchar(11)) , r.code_desc ) c			
		) as hamp ON dt.call_dt = hamp.call_dt
		LEFT OUTER JOIN
		(SELECT	c.call_dt, isnull(c.qty, 0) as Qty
		FROM	(SELECT	cast(c.start_dt as varchar(11))as call_dt
						, r.code_desc as mha_eligibility_cd, count(call_id) as qty
				FROM	(SELECT code, code_desc, sort_order FROM ref_code_item WHERE ref_code_set_name = ''mha eligibility code'') r 
						INNER JOIN call c 	ON c.mha_eligibility_cd = r.code AND r.code = ''NOTHARP''
				WHERE	c.start_dt between @pi_from_dt and @pi_to_dt	AND mha_eligibility_cd is not null
				GROUP BY cast(c.start_dt as varchar(11)) , r.code_desc ) c			
		) as notharp ON dt.call_dt = notharp.call_dt
		LEFT OUTER JOIN
		(SELECT	c.call_dt, isnull(c.qty, 0) as Qty
		FROM	(SELECT	cast(c.start_dt as varchar(11))as call_dt
						, r.code_desc as mha_eligibility_cd, count(call_id) as qty
				FROM	(SELECT code, code_desc, sort_order FROM ref_code_item WHERE ref_code_set_name = ''mha eligibility code'') r 
						INNER JOIN call c 	ON c.mha_eligibility_cd = r.code AND r.code = ''NOTHAMP''
				WHERE	c.start_dt between @pi_from_dt and @pi_to_dt	AND mha_eligibility_cd is not null
				GROUP BY cast(c.start_dt as varchar(11)) , r.code_desc ) c			
		) as nothamp ON dt.call_dt = nothamp.call_dt
		LEFT OUTER JOIN
		(SELECT	c.call_dt, isnull(c.qty, 0) as Qty
		FROM	(SELECT	cast(c.start_dt as varchar(11))as call_dt
						, r.code_desc as mha_eligibility_cd, count(call_id) as qty
				FROM	(SELECT code, code_desc, sort_order FROM ref_code_item WHERE ref_code_set_name = ''mha eligibility code'') r 
						INNER JOIN call c 	ON c.mha_eligibility_cd = r.code AND r.code = ''MHAINELIG''
				WHERE	c.start_dt between @pi_from_dt and @pi_to_dt	AND mha_eligibility_cd is not null
				GROUP BY cast(c.start_dt as varchar(11)) , r.code_desc ) c			
		) as MHAINELIG ON dt.call_dt = MHAINELIG.call_dt
		LEFT OUTER JOIN
		(SELECT	c.call_dt, isnull(c.qty, 0) as Qty
		FROM	(SELECT	cast(c.start_dt as varchar(11))as call_dt
						, r.code_desc as mha_eligibility_cd, count(call_id) as qty
				FROM	(SELECT code, code_desc, sort_order FROM ref_code_item WHERE ref_code_set_name = ''mha eligibility code'') r 
						INNER JOIN call c 	ON c.mha_eligibility_cd = r.code AND r.code = ''UNK''
				WHERE	c.start_dt between @pi_from_dt and @pi_to_dt	AND mha_eligibility_cd is not null
				GROUP BY cast(c.start_dt as varchar(11)) , r.code_desc ) c			
		) as UNK ON dt.call_dt = UNK.call_dt
	ORDER BY dt.call_dt;
END;

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableExportFile_header]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Apr 2009
-- Description:	HPF REPORT - Payable Export File (header)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_PayableExportFile_header] 
	(@pi_agency_payable_id integer)
AS
BEGIN
	SELECT	p.agency_payable_id
			, convert(varchar(20), p.pmt_dt, 107) as agency_payable_dt
			, count(fc_id) Total_sessions
			, convert(varchar(20), p.period_start_dt, 101) as period_start_dt
			, convert(varchar(20), p.period_end_dt, 101) as period_end_dt	
	FROM	agency_payable p, agency_payable_case pc
	WHERE	p.agency_payable_id = pc.agency_payable_id
			AND p.agency_payable_id = @pi_agency_payable_id
	GROUP BY p.agency_payable_id, p.pmt_dt, p.period_start_dt, p.period_end_dt;
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget_asset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary For Agency - Get FC Budget Asset
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummaryForAgency_get_FC_Budget_asset] 
	(@pi_fc_id int, @pi_agency_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
	DECLARE @v_fc_id_correct_agency_id int;

	SELECT @v_fc_id_correct_agency_id = count(fc_id) 
	FROM foreclosure_case WHERE fc_id = @pi_fc_id AND agency_id = @pi_agency_id;

	IF @v_fc_id_correct_agency_id > 0 
	BEGIN
		DECLARE @v_max_budget_set_id INT;
		SELECT	@v_max_budget_set_id = max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id;

		SELECT	i.asset_name
				, ''$'' + convert(varchar(30), cast(i.asset_value  as money),1) as asset_value			
				, ''$'' + convert(varchar(30), cast(sba.SUM_asset_value as money),1) as SUM_asset_value
		FROM	budget_asset i
				,(SELECT SUM(asset_value) SUM_asset_value FROM budget_asset WHERE budget_set_id = @v_max_budget_set_id) sba 
		WHERE	i.budget_set_id = @v_max_budget_set_id
		;
	END
	IF @v_fc_id_correct_agency_id = 0 
		SELECT	NULL asset_name
				, NULL  as asset_value			
				, NULL  as SUM_asset_value
END;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_FIS_detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 27 Feb 2009
-- Description:	HPF REPORT - R59 - Invoice Export File - FIS (detail)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_FIS_detail] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	ic.invoice_case_id, convert(varchar(11), f.intake_dt, 101) intake_dt
			, cast(ic.invoice_case_bill_amt as numeric(18)) invoice_case_pmt_amt
	FROM	invoice_case ic, foreclosure_case f
	WHERE	ic.fc_id = f.fc_id 
			AND ic.invoice_id = @pi_invoice_id
	ORDER BY ic.invoice_case_id ASC;
END 



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Budget_asset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary - Get FC Budget Asset
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Budget_asset] 
	(@pi_fc_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
	DECLARE @v_max_budget_set_id INT;
	SELECT	@v_max_budget_set_id = max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id;

	SELECT	i.asset_name
			, ''$'' + convert(varchar(30), cast(i.asset_value  as money),1) as asset_value			
			, ''$'' + convert(varchar(30), cast(sba.SUM_asset_value as money),1) as SUM_asset_value
	FROM	budget_asset i
			,(SELECT SUM(asset_value) SUM_asset_value FROM budget_asset WHERE budget_set_id = @v_max_budget_set_id) sba 
	WHERE	i.budget_set_id = @v_max_budget_set_id
	;
END;






' 
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_rpt_MHA_Weekly_Executive_Summary_Report]    Script Date: 06/25/2009 11:12:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 24 Jun 2009
-- Project : HPF 
-- Build 
-- Description:	hpf_rpt_MHA_Weekly_Executive_Summary_Report
-- @pi_is_chart = 0 -> return  for TABLE
-- @pi_is_chart = 1 -> return  for Chart - Call Sources - This Month
-- @pi_is_chart = 2 -> return  for Chart - Servicer Complaints - This Month
-- @pi_is_chart = 3 -> return  for Chart - MHA Qualified - This Month
-- Example :declare @pi_dt datetime, @pi_is_chart int;
--			SELECT @pi_dt = getdate(), @pi_is_chart = 0;
--			EXEC [hpf_rpt_MHA_Weekly_Executive_Summary_Report] @pi_dt, @pi_is_chart;
-- =============================================

CREATE PROCEDURE [dbo].[hpf_rpt_MHA_Weekly_Executive_Summary_Report] 
	(@pi_dt datetime, @pi_is_chart int)
AS
DECLARE @v_from_this_Mon datetime, @v_from_this_wk datetime, @v_from_last_wk datetime, @v_to_last_wk datetime
		, @v_from_last_Mon datetime, @v_to_last_Mon datetime
		, @v_group_name		varchar(80), @v_item_name		varchar(100)
		, @v_last_wk		numeric(18), @v_this_wk		numeric(18), @v_mon_to_dt		numeric(18);
CREATE TABLE #temp_output
(	group_name		varchar(80)
	, item_name		varchar(100)
	, last_wk		numeric(18)
	, this_wk		numeric(18)
	, mon_to_dt		numeric(18)
)
BEGIN
/**** 1. Calculate datetime ****************************************/
	SELECT @pi_dt = dateadd(millisecond ,-2, cast(cast(@pi_dt as varchar(11)) as datetime) + 1);
	SELECT @v_from_this_Mon = cast(cast(datepart(month, @pi_dt) as varchar(2)) + '-01-' + cast(datepart(year, @pi_dt) as varchar(4)) as datetime);
	SELECT @v_from_this_wk = cast(cast(@pi_dt - datepart(dw, @pi_dt) + 1 as varchar(11)) as datetime);
	SELECT @v_from_last_wk = cast(cast((@pi_dt - datepart(dw, @pi_dt)) - datepart(dw, @pi_dt - datepart(dw, @pi_dt)) +1 as varchar(11)) as datetime);
	SELECT @v_to_last_wk = dateadd(millisecond ,-2, cast(cast(@pi_dt as varchar(11)) as datetime) - datepart(dw, @pi_dt)) ;
	SELECT @v_from_last_Mon = cast(cast(datepart(month, dateadd(month, -1, @pi_dt)) as varchar(2)) + '-01-' + cast(datepart(year, @pi_dt) as varchar(4)) as datetime);
	SELECT @v_to_last_Mon = cast(cast(datepart(month, @pi_dt) as varchar(2)) + '-01-' + cast(datepart(year, @pi_dt) as varchar(4)) as datetime) -1;
	SELECT @v_to_last_Mon = dateadd(millisecond ,-2, cast(cast(@v_to_last_Mon as varchar(11)) as datetime) + 1);
/*	print	'FromThisMon=' + cast(@v_from_this_Mon as varchar(20))  
			+ '; FromThisWk=' + cast(@v_from_this_wk as varchar(20))  
			+ ';FromLastWk=' + cast(@v_from_last_wk as varchar(20)) 
			+ ';ToLastWk=' + cast(@v_to_last_wk as varchar(20)) 
			+ 'FromLastMon=' + cast(@v_from_last_Mon as varchar(20))  
			+ 'ToLastMon=' + cast(@v_to_last_Mon as varchar(20))  
			;*/
IF (@pi_is_chart = 0)
BEGIN
/**** 2. Count MHA results *************/
	SELECT @v_group_name	= 'MHA Results';

	SELECT @v_item_name		= 'Calls Answered';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk;
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt;
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt;
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'MHA Qualification Completed';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
					AND mha_eligibility_cd in ('HAMP', 'HARP', 'NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
					AND mha_eligibility_cd in ('HAMP', 'HARP', 'NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
					AND mha_eligibility_cd in ('HAMP', 'HARP', 'NOTHAMP', 'NOTHARP', 'MHAINELIG');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	UPDATE	#temp_output SET last_wk = NULL WHERE group_name = 'MHA Results' AND item_name = 'MHA Qualification Completed' AND last_wk = 0;
	UPDATE	#temp_output SET this_wk = NULL WHERE group_name = 'MHA Results' AND item_name = 'MHA Qualification Completed' AND this_wk = 0;
	UPDATE	#temp_output SET mon_to_dt = NULL WHERE group_name = 'MHA Results' AND item_name = 'MHA Qualification Completed' AND mon_to_dt= 0;

	SELECT @v_item_name		= 'MHA Qualified';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
					AND mha_eligibility_cd in ('HAMP', 'HARP');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
					AND mha_eligibility_cd in ('HAMP', 'HARP');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
					AND mha_eligibility_cd in ('HAMP', 'HARP');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= '% MHA Qualified';
	SELECT @v_last_wk = (SELECT last_wk FROM #temp_output WHERE group_name	= 'MHA Results' AND item_name = 'MHA Qualified')/(SELECT last_wk FROM #temp_output WHERE group_name	= 'MHA Results' AND item_name = 'MHA Qualification Completed');
	SELECT @v_this_wk = (SELECT this_wk FROM #temp_output WHERE group_name	= 'MHA Results' AND item_name = 'MHA Qualified')/(SELECT this_wk FROM #temp_output WHERE group_name	= 'MHA Results' AND item_name = 'MHA Qualification Completed');
	SELECT @v_mon_to_dt = (SELECT mon_to_dt FROM #temp_output WHERE group_name	= 'MHA Results' AND item_name = 'MHA Qualified')/(SELECT mon_to_dt FROM #temp_output WHERE group_name	= 'MHA Results' AND item_name = 'MHA Qualification Completed');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Referred to Servicer for MHA';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND final_dispo_cd in ('MHAREFERRAL','MHATRANSFER');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND final_dispo_cd in ('MHAREFERRAL','MHATRANSFER');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND final_dispo_cd in ('MHAREFERRAL','MHATRANSFER');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Complaint - Servicer Contact';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND  servicer_ca_id is not null;
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND  servicer_ca_id is not null;
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND  servicer_ca_id is not null;
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Complaint - MHA Program';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND  final_dispo_cd in ('COMPLAINTMHA');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND  final_dispo_cd in ('COMPLAINTMHA');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND  final_dispo_cd in ('COMPLAINTMHA');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

/**** 3. Count MHA Eligibility Fallout Reasons *************/
	SELECT @v_group_name	= 'MHA Eligibility Fallout Reasons';

	SELECT @v_item_name		= 'DTI < 31%';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND mha_inelig_reason_cd = 'FAILEDDTI' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND mha_inelig_reason_cd = 'FAILEDDTI' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND mha_inelig_reason_cd = 'FAILEDDTI' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Loan amount > $729,750';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND mha_inelig_reason_cd = 'MAXLOANAMT' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND mha_inelig_reason_cd = 'MAXLOANAMT' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND mha_inelig_reason_cd = 'MAXLOANAMT' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Not primary residence';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND mha_inelig_reason_cd = 'NOTPRIMRES' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND mha_inelig_reason_cd = 'NOTPRIMRES' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND mha_inelig_reason_cd = 'NOTPRIMRES' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Loan lookup not Fannie/Freddie';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
						AND mha_inelig_reason_cd = 'NOTFOUND' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
						AND mha_inelig_reason_cd = 'NOTFOUND' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
						AND mha_inelig_reason_cd = 'NOTFOUND' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Loan originated after 1/1/2009';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
					AND mha_inelig_reason_cd = 'ORIGAFTER2009' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
					AND mha_inelig_reason_cd = 'ORIGAFTER2009' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
					AND mha_inelig_reason_cd = 'ORIGAFTER2009' and mha_eligibility_cd in ('NOTHAMP', 'NOTHARP', 'MHAINELIG');
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);

	SELECT @v_item_name		= 'Missing information';
	SELECT @v_last_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk
					AND mha_inelig_reason_cd in ('MISSINGLOOKUP', 'MISSINGINFO', 'MISSINGLOANAMT', 'MISSINGORIGDT', 'MISSINGDTI', 'MISSINGDELINQ', 'MISSINGPRIMRES', 'MISSINGMAXCHK')  and (mha_eligibility_cd = 'UNK' or mha_eligibility_cd is NULL);
	SELECT @v_this_wk = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt
					AND mha_inelig_reason_cd in ('MISSINGLOOKUP', 'MISSINGINFO', 'MISSINGLOANAMT', 'MISSINGORIGDT', 'MISSINGDTI', 'MISSINGDELINQ', 'MISSINGPRIMRES', 'MISSINGMAXCHK')  and (mha_eligibility_cd = 'UNK' or mha_eligibility_cd is NULL);
	SELECT @v_mon_to_dt = Count(call_id) FROM call WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt
					AND mha_inelig_reason_cd in ('MISSINGLOOKUP', 'MISSINGINFO', 'MISSINGLOANAMT', 'MISSINGORIGDT', 'MISSINGDTI', 'MISSINGDELINQ', 'MISSINGPRIMRES', 'MISSINGMAXCHK')  and (mha_eligibility_cd = 'UNK' or mha_eligibility_cd is NULL);
	INSERT INTO #temp_output VALUES (@v_group_name, @v_item_name, @v_last_wk, @v_this_wk, @v_mon_to_dt);
/**** 4. Count Loan Delinquency *************/
	SELECT @v_group_name	= 'Loan Delinquency';

	INSERT INTO #temp_output (group_name, item_name, last_wk)
		SELECT	@v_group_name, i.code_desc as item_name, count(call_id)  as last_wk
		FROM	(SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = 'loan delinquency status code') i
				LEFT OUTER JOIN (SELECT call_id, loan_delinq_status_cd FROM call 
								WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk AND loan_delinq_status_cd IS NOT NULL)c
				ON c.loan_delinq_status_cd = i.code 				
		GROUP BY i.code_desc
		UNION
		SELECT @v_group_name, '(not specified)' as item_name, count(call_id) as last_wk
		FROM	call c 
		WHERE	c.create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk  
				AND loan_delinq_status_cd IS NULL;

	UPDATE	#temp_output
	SET		#temp_output.this_wk = t.this_wk
	FROM	#temp_output
			, (	SELECT	i.code_desc as item_name, count(call_id)  as this_wk
			FROM	(SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = 'loan delinquency status code') i
					LEFT OUTER JOIN (SELECT call_id, loan_delinq_status_cd FROM call 
									WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt AND loan_delinq_status_cd IS NOT NULL)c
					ON c.loan_delinq_status_cd = i.code 				
			GROUP BY i.code_desc
			UNION
			SELECT '(not specified)' as item_name, count(call_id) as this_wk
			FROM	call c 
			WHERE	c.create_dt BETWEEN @v_from_this_wk AND @pi_dt
					AND	loan_delinq_status_cd IS NULL
			) t
	WHERE	#temp_output.group_name	= 'Loan Delinquency' AND #temp_output.item_name = t.item_name;

	UPDATE	#temp_output
	SET		#temp_output.mon_to_dt = t.mon_to_dt
	FROM	#temp_output
			, (SELECT	i.code_desc as item_name, count(call_id)  as mon_to_dt
			FROM	(SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = 'loan delinquency status code') i
					LEFT OUTER JOIN (SELECT call_id, loan_delinq_status_cd FROM call 
									WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt AND loan_delinq_status_cd IS NOT NULL)c
					ON c.loan_delinq_status_cd = i.code 				
			GROUP BY i.code_desc
			UNION
			SELECT '(not specified)' as item_name, count(call_id) as mon_to_dt
			FROM	call c 
			WHERE	c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt
					AND	loan_delinq_status_cd IS NULL
			) t
	WHERE	#temp_output.group_name	= 'Loan Delinquency' AND #temp_output.item_name = t.item_name;

/**** 5. Count MHA Loan Lookups *************/
	SELECT @v_group_name	= 'MHA Loan Lookups';

	INSERT INTO #temp_output (group_name, item_name, last_wk)
		SELECT	@v_group_name, i.code_desc as item_name, count(call_id)  as last_wk
		FROM	(SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = 'mha loan lookup code' )i 
				LEFT OUTER JOIN (SELECT call_id,loan_lookup_cd FROM call 
								WHERE create_dt BETWEEN @v_from_last_wk AND @v_to_last_wk AND loan_lookup_cd IS NOT NULL
				)c ON i.code = c.loan_lookup_cd					
		GROUP BY i.code_desc;

	UPDATE	#temp_output
	SET		#temp_output.this_wk = t.this_wk
	FROM	#temp_output
			,(SELECT	i.code_desc as item_name, count(call_id)  as this_wk
			FROM	(SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = 'mha loan lookup code' )i 
					LEFT OUTER JOIN (SELECT call_id,loan_lookup_cd FROM call 
									WHERE create_dt BETWEEN @v_from_this_wk AND @pi_dt AND loan_lookup_cd IS NOT NULL
					)c ON i.code = c.loan_lookup_cd					
			GROUP BY i.code_desc
			) t
	WHERE	#temp_output.group_name	= 'MHA Loan Lookups' AND #temp_output.item_name = t.item_name;

	UPDATE	#temp_output
	SET		#temp_output.mon_to_dt = t.mon_to_dt
	FROM	#temp_output
			,(SELECT	i.code_desc as item_name, count(call_id)  as mon_to_dt
			FROM	(SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = 'mha loan lookup code' )i 
					LEFT OUTER JOIN (SELECT call_id,loan_lookup_cd FROM call 
									WHERE create_dt BETWEEN @v_from_this_Mon AND @pi_dt AND loan_lookup_cd IS NOT NULL
					)c ON i.code = c.loan_lookup_cd					
			GROUP BY i.code_desc
			) t
	WHERE	#temp_output.group_name	= 'MHA Loan Lookups' AND #temp_output.item_name = t.item_name;

/**** 6.1 Count Top 10 Call Sources - Mon to Date *************/
	SELECT @v_group_name	= 'Top 10 Call Sources - Month to Date';

	INSERT INTO #temp_output (group_name, item_name, mon_to_dt)
		SELECT	TOP 10 @v_group_name, call_source_cd, count(call_id)  as mon_to_dt
		FROM		call 
		WHERE		create_dt BETWEEN @v_from_this_Mon AND @pi_dt 
		GROUP BY	call_source_cd 
		ORDER BY	mon_to_dt desc;
		
	/**** 6.2 Count Top 10 Call Sources - Last Month *************/
	SELECT @v_group_name	= 'Top 10 Call Sources - Last Month';
	INSERT INTO #temp_output (group_name, item_name, mon_to_dt)
		SELECT	TOP 10 @v_group_name, call_source_cd, count(call_id)  as mon_to_dt
		FROM		call 
		WHERE		create_dt BETWEEN @v_from_last_Mon AND @v_to_last_Mon
		GROUP BY	call_source_cd 
		ORDER BY	mon_to_dt desc;
/**** 7 Count Top 10 Servicer Complaints *************/
	SELECT @v_group_name	= 'Top 10 Servicer Complaints - This Month';
	INSERT INTO #temp_output (group_name, item_name, this_wk, mon_to_dt)
		SELECT TOP 10 @v_group_name, servicer_name, sum(servicer_ca_num)/count(call_id) as average, COUNT(call_id) Consumers_Reporting
		FROM	call c, servicer s
		WHERE	c.servicer_ca_id = s.servicer_id AND servicer_ca_id is not null
				AND c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt
		GROUP BY servicer_name
		ORDER BY Consumers_Reporting desc;

	SELECT group_name, item_name, isnull(last_wk, 0) as last_wk, isnull(this_wk, 0) as this_wk , isnull(mon_to_dt, 0) as mon_to_dt FROM #temp_output;
END; /* END @pi_is_chart = 0 */

IF (@pi_is_chart = 1)
BEGIN
/**** 8. Count Servicer - All calls - This month (Mon to Date) *************/
	SELECT	@v_group_name	= 'Servicer Calls - This Month';
	SELECT	@v_mon_to_dt = Count(call_id) 	
	FROM	call c, servicer s
	WHERE	c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt AND c.servicer_id = s.servicer_id;

	IF @v_mon_to_dt = 0 
		SELECT @v_mon_to_dt = NULL;
--	SELECT		call_source_cd + ' - ' + cast(cast((count(call_id)*100/@v_mon_to_dt) as numeric(18,2)) as varchar(10)) + ' %' as call_source_cd, count(call_id)  as this_mon 
	SELECT		s.servicer_name + ' - ' + cast(cast((Count(call_id)*100/@v_mon_to_dt) as numeric(18,2)) as varchar(10)) + ' %' as servicer_name
				, Count(call_id) as this_mon 
	FROM		call c, servicer s
	WHERE		c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt AND c.servicer_id = s.servicer_id
	GROUP BY	s.servicer_name 
	ORDER BY	this_mon desc;
END;
IF (@pi_is_chart = 2)
BEGIN
/**** 9 Count Servicer Complaints - This month (Mon to Date) *************/
	SELECT	@v_group_name	= 'Servicer Complaints - This Month';
	SELECT	@v_mon_to_dt = Count(call_id) 	FROM	call c, servicer s 
	WHERE	c.servicer_ca_id = s.servicer_id AND servicer_ca_id is not null	
			AND c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt;

	IF @v_mon_to_dt = 0 
		SELECT @v_mon_to_dt = NULL;

	SELECT	servicer_name + ' - ' + cast(cast((count(call_id)*100/@v_mon_to_dt) as numeric(18,2)) as varchar(10)) + ' %' as servicer_name
			, sum(servicer_ca_num)/count(call_id) as average, COUNT(call_id) as Consumers_Reporting			
	FROM	call c, servicer s
	WHERE	c.servicer_ca_id = s.servicer_id AND servicer_ca_id is not null
			AND c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt
	GROUP BY servicer_name
	ORDER BY Consumers_Reporting desc;
END;
IF (@pi_is_chart = 3)
BEGIN
/**** 10. Count Servicer - MHA Qualified ***************/
	SELECT @v_group_name	= 'Servicer MHA Qualified - This Month';
	SELECT @v_mon_to_dt = Count(call_id) 
	FROM	call c, servicer s
	WHERE	c.servicer_id = s.servicer_id
			AND c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt			
			AND c.mha_eligibility_cd in ('HAMP', 'HARP');
	IF @v_mon_to_dt = 0 
		SELECT @v_mon_to_dt = NULL;

	SELECT  s.servicer_name + ' - ' + cast(cast((Count(call_id)*100/@v_mon_to_dt) as numeric(18,2)) as varchar(10)) + ' %' as servicer_name
			, Count(call_id) as mon_to_dt
	FROM	call c, servicer s
	WHERE	c.servicer_id = s.servicer_id
			AND c.create_dt BETWEEN @v_from_this_Mon AND @pi_dt			
			AND c.mha_eligibility_cd in ('HAMP', 'HARP')
	GROUP BY s.servicer_name;
END;
/**** END PROCESSING ****************************************/	
	DROP TABLE #temp_output;
END;

GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 13 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	hpf_rpt_PotentialDuplicates
-- =============================================
ALTER PROCEDURE [dbo].[hpf_rpt_PotentialDuplicates] 
	(@pi_from_dt datetime
	, @pi_to_dt datetime
	, @pi_filteredBy int	-- 0: By Acct & Servicer; 1: By Address; 2: By Phone
	)
AS
BEGIN
	DECLARE @v_from_last_year_dt datetime;
	CREATE TABLE #temp_output
	( fc_id	int
	, agency_name varchar(50)
	, acct_num	varchar(30)
	, Servicer_name varchar(50)
	, borrower_fname	varchar(50)
	, borrower_lname	varchar(50)
	, borrower_last4_ssn varchar(4)
	, primary_contact_no varchar(20)
	, prop_addr1		varchar(50)
	, prop_addr2		varchar(50)
	, prop_city			varchar(30)
	, prop_state_cd		varchar(80)
	, prop_zip			varchar(5)
	, agency_case_num	varchar(30)
	, intake_dt			varchar(20)
	, completed_dt		varchar(20)
	, summary_sent_dt	varchar(20)
	, servicer_id		int
	, loan_list			varchar(500)
	, co_borrower_fname	varchar(30)
	, co_borrower_lname	varchar(30)
	, co_borrower_last4_ssn varchar(4)
	, contact_addr1		varchar(50)
	, contact_addr2		varchar(50)
	, contact_city		varchar(30)
	, contact_state_cd	varchar(80)
	, contact_zip		varchar(5)
	, counselor_lname	varchar(30)
	, counselor_fname	varchar(30)
	, loan_1st_2nd_cd	varchar(50)
	, mortgage_type_cd	varchar(50)
	, term_length_cd	varchar(50)
	, loan_delinq_status_cd	varchar(50)
	, mortgage_program_cd	varchar(50)
	, never_bill_reason_cd	varchar(50)
	, never_pay_reason_cd	varchar(50)
	, hopenet_billed	varchar(3)
	, hopenet_paid	varchar(3)
	);

	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(millisecond ,-2, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
	SELECT @v_from_last_year_dt = DateAdd(year, -1, @pi_from_dt);
--	Print cast(@pi_from_dt as varchar(20)) + '-' + cast(@pi_to_dt as varchar(20));

	/* Potential duplicates are records with
	0.the 1st 7 chars of acct_num and Servicer name
	1. Address1 (only the 1st 12 chars) AND zip
	2.Primary_contact no	
	*/
IF (@pi_filteredBy = 0)
	INSERT INTO #temp_output 
		( fc_id, agency_name , acct_num, Servicer_name 
		, borrower_fname, borrower_lname, borrower_last4_ssn, primary_contact_no
		, prop_addr1, prop_addr2, prop_city	, prop_state_cd	, prop_zip			
		, agency_case_num	
		, intake_dt	, completed_dt, summary_sent_dt	
		, servicer_id, loan_list
		, co_borrower_fname	, co_borrower_lname	, co_borrower_last4_ssn 
		, contact_addr1	, contact_addr2	, contact_city, contact_state_cd, contact_zip		
		, counselor_lname, counselor_fname, loan_1st_2nd_cd	
		, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, mortgage_program_cd, never_bill_reason_cd, never_pay_reason_cd
		)
	SELECT f.fc_id, a.agency_name, l.acct_num
		, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
		, f.borrower_fname, f.borrower_lname, f.borrower_last4_ssn
		, f.primary_contact_no, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
		, f.agency_case_num
		, convert(varchar(20), f.intake_dt, 101) as intake_dt, convert(varchar(20), f.completed_dt, 101) as completed_dt
		, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
		, l.servicer_id, f.loan_list
		, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_last4_ssn
		, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip
		, f.counselor_lname, f.counselor_fname
		, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.mortgage_program_cd
		, f.never_bill_reason_cd, f.never_pay_reason_cd
	FROM	foreclosure_case f, case_loan l, agency a, program p, servicer s
			, (SELECT	DISTINCT	substring(lmo.acct_num, 1, 9)  + ';' + cast(lmo.servicer_id as varchar(50)) as loan_info
			FROM	case_loan lmo, foreclosure_case fcmo
					, (SELECT	substring(lall.acct_num, 1, 9)  + ';' + cast(lall.servicer_id as varchar(50)) as loan_info
						FROM	case_loan lall, foreclosure_case fcall
						WHERE	lall.fc_id = fcall.fc_id
								AND fcall.duplicate_ind <>'Y'
								AND lall.loan_1st_2nd_cd = '1ST' and lall.servicer_id <> 12982					
								AND isnull(fcall.completed_dt, fcall.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
						GROUP BY	substring(lall.acct_num, 1, 9)  + ';' + cast(lall.servicer_id as varchar(50)) 
						HAVING count(*) > 1			
						) cf1
			WHERE	lmo.fc_id = fcmo.fc_id
					AND fcmo.duplicate_ind <> 'Y'
					AND lmo.loan_1st_2nd_cd = '1ST'	AND lmo.servicer_id <> 12982					
					AND isnull(fcmo.completed_dt, fcmo.create_dt) BETWEEN @pi_from_dt AND @pi_to_dt
					AND substring(lmo.acct_num, 1, 9)  + ';' + cast(lmo.servicer_id as varchar(50)) = cf1.loan_info						
			) cf
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = '1ST' AND l.servicer_id <> 12982
			AND f.duplicate_ind <> 'Y'			
			AND isnull(f.completed_dt, f.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
			AND substring(l.acct_num, 1, 9)  + ';' + cast(l.servicer_id as varchar(50)) = cf.loan_info				
	ORDER BY servicer_name, substring(l.acct_num, 1, 9);							

IF (@pi_filteredBy = 1)
	INSERT INTO #temp_output 
		( fc_id, agency_name , acct_num, Servicer_name 
		, borrower_fname, borrower_lname, borrower_last4_ssn, primary_contact_no
		, prop_addr1, prop_addr2, prop_city	, prop_state_cd	, prop_zip			
		, agency_case_num	
		, intake_dt	, completed_dt, summary_sent_dt	
		, servicer_id, loan_list
		, co_borrower_fname	, co_borrower_lname	, co_borrower_last4_ssn 
		, contact_addr1	, contact_addr2	, contact_city, contact_state_cd, contact_zip		
		, counselor_lname, counselor_fname, loan_1st_2nd_cd	
		, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, mortgage_program_cd, never_bill_reason_cd, never_pay_reason_cd
		)
	SELECT f.fc_id, a.agency_name, l.acct_num
		, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
		, f.borrower_fname, f.borrower_lname, f.borrower_last4_ssn
		, f.primary_contact_no, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
		, f.agency_case_num
		, convert(varchar(20), f.intake_dt, 101) as intake_dt, convert(varchar(20), f.completed_dt, 101) as completed_dt
		, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
		, l.servicer_id, f.loan_list
		, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_last4_ssn
		, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip
		, f.counselor_lname, f.counselor_fname
		, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.mortgage_program_cd
		, f.never_bill_reason_cd, f.never_pay_reason_cd
	FROM	foreclosure_case f, case_loan l, agency a, program p, servicer s
			, (SELECT DISTINCT substring(fcmo.prop_addr1, 1, 12) + ';' + fcmo.prop_zip as addr_info
			FROM	foreclosure_case fcmo
					,(SELECT substring(fcall.prop_addr1, 1, 12) + ';' + fcall.prop_zip as addr_info
					FROM	foreclosure_case fcall
					WHERE	fcall.duplicate_ind <>'Y'							
							AND isnull(fcall.completed_dt, fcall.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
					GROUP BY	substring(fcall.prop_addr1, 1, 12) + ';' + fcall.prop_zip 
					HAVING count(*) > 1			
					)cf1
			WHERE	fcmo.duplicate_ind <> 'Y'
					AND isnull(fcmo.completed_dt, fcmo.create_dt) BETWEEN @pi_from_dt AND @pi_to_dt
					AND substring(fcmo.prop_addr1, 1, 12) + ';' + fcmo.prop_zip = cf1.addr_info					
				)cf
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = '1ST'
			AND f.duplicate_ind <> 'Y'			
			AND isnull(f.completed_dt, f.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
			AND substring(f.prop_addr1, 1, 12) + ';' + f.prop_zip = cf.addr_info
	order by f.prop_addr1;	
	
IF (@pi_filteredBy = 2)
	INSERT INTO #temp_output 
		( fc_id, agency_name , acct_num, Servicer_name 
		, borrower_fname, borrower_lname, borrower_last4_ssn, primary_contact_no
		, prop_addr1, prop_addr2, prop_city	, prop_state_cd	, prop_zip			
		, agency_case_num	
		, intake_dt	, completed_dt, summary_sent_dt	
		, servicer_id, loan_list
		, co_borrower_fname	, co_borrower_lname	, co_borrower_last4_ssn 
		, contact_addr1	, contact_addr2	, contact_city, contact_state_cd, contact_zip		
		, counselor_lname, counselor_fname, loan_1st_2nd_cd	
		, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, mortgage_program_cd, never_bill_reason_cd, never_pay_reason_cd
		)
	SELECT f.fc_id, a.agency_name, l.acct_num
		, case l.servicer_id when 12982 then l.other_servicer_name else s.Servicer_name end Servicer_name
		, f.borrower_fname, f.borrower_lname, f.borrower_last4_ssn
		, f.primary_contact_no, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip
		, f.agency_case_num
		, convert(varchar(20), f.intake_dt, 101) as intake_dt, convert(varchar(20), f.completed_dt, 101) as completed_dt
		, convert(varchar(20), f.summary_sent_dt, 101) as summary_sent_dt
		, l.servicer_id, f.loan_list
		, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_last4_ssn
		, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip
		, f.counselor_lname, f.counselor_fname
		, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.mortgage_program_cd
		, f.never_bill_reason_cd, f.never_pay_reason_cd
	FROM	foreclosure_case f, case_loan l, agency a, program p, servicer s
			, (SELECT DISTINCT replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
					replace(fcmo.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','') as phone_info
			FROM	foreclosure_case fcmo
					,(SELECT replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
									replace(fcall.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','') as phone_info
					FROM	foreclosure_case fcall
					WHERE	replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
								replace(fcall.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','') 
								not in ('','unknownatconv','NA','na','N/A','n/a','none','	', 'unknown')							
							AND isnull(fcall.completed_dt, fcall.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
							AND fcall.duplicate_ind <> 'Y'
					GROUP BY	replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (replace(fcall.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','')	
					HAVING	count(*) > 1
					)cf1
			WHERE	isnull(fcmo.completed_dt, fcmo.create_dt) BETWEEN @pi_from_dt AND @pi_to_dt
					AND fcmo.duplicate_ind <> 'Y'
					AND replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace(replace(replace (
							 replace(fcmo.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','') 
						= cf1.phone_info									
			)cf
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = '1ST'			
			AND isnull(f.completed_dt, f.create_dt) BETWEEN @v_from_last_year_dt AND @pi_to_dt
			AND replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (
					replace(f.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','') 
				= cf.phone_info
	ORDER BY replace(replace(replace(Replace(replace(replace(replace(replace(replace(replace(replace (replace(replace (replace(f.primary_contact_no,'(',''),')',''),' ',''),'-',''),'.',''),'/',''),'_',''),'#',''),'+',''),'*',''),'=',''),'[',''),']',''),':','');
-- Identify  the fc_id has been billed
	UPDATE	#temp_output 
	SET		hopenet_billed = 'Yes'
	FROM	#temp_output t LEFT OUTER JOIN invoice_case ic ON t.fc_id = ic.fc_id 
	WHERE	ic.fc_id IS NOT NULL;
-- Identify  the fc_id has been paid
	UPDATE	#temp_output 
	SET		hopenet_paid = 'Yes'
	FROM	#temp_output t LEFT OUTER JOIN invoice_case ic ON t.fc_id = ic.fc_id 
	WHERE	ic.fc_id IS NOT NULL 
			AND ic.invoice_case_pmt_amt IS NOT NULL AND ic.invoice_case_pmt_amt > 0;
-- Identify  the fc_id has not been billed or paid
	UPDATE	#temp_output SET		hopenet_billed = 'No' WHERE hopenet_billed IS NULL;
	UPDATE	#temp_output SET		hopenet_paid = 'No' WHERE hopenet_paid IS NULL;	
-- Return the output
	SELECT 	fc_id, agency_name , acct_num, Servicer_name 
		, borrower_fname, borrower_lname, borrower_last4_ssn, primary_contact_no
		, prop_addr1, prop_addr2, prop_city	, prop_state_cd	, prop_zip			
		, agency_case_num	
		, intake_dt	, completed_dt, summary_sent_dt	
		, servicer_id, loan_list
		, co_borrower_fname	, co_borrower_lname	, co_borrower_last4_ssn 
		, contact_addr1	, contact_addr2	, contact_city, contact_state_cd, contact_zip		
		, counselor_lname, counselor_fname, loan_1st_2nd_cd	
		, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, mortgage_program_cd, never_bill_reason_cd, never_pay_reason_cd
		, hopenet_billed, hopenet_paid
	FROM #temp_output;
	DROP TABLE #temp_output;
END;
GO