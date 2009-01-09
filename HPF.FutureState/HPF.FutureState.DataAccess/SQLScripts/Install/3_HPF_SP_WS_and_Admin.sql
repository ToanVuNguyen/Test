USE [hpf]
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_search_ws]    Script Date: 01/09/2009 16:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen 
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Seacrh case (WEB SERVICE)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_search_ws]
	@pi_agency_case_num varchar(30) = null ,
	@pi_borrower_fname varchar(30) = null,
	@pi_borrower_lname varchar(30) = null,
	@pi_borrower_last4_SSN varchar(9) = null,
	@pi_prop_zip varchar(5) = null,
	@pi_loan_number varchar(30) = null,

	@pi_number_of_rows int = 50,
	@pi_where_clause varchar(2000) = null,

	@po_total_rows int = 0 output
As 
Declare	@v_select_clause nvarchar(1500)	
		, @v_orderby_clause nvarchar(500)		
		, @v_full_string nvarchar(4000)
		, @v_parameter_list nvarchar(1000)

Begin
	

	-- get total number of rows
	Set @v_select_clause =	'Select @po_total_rows = count(foreclosure_case.fc_id) 
							From foreclosure_case, Agency, case_loan, servicer '
	Set @v_parameter_list = '@pi_agency_case_num varchar(30) = null ,
							@pi_borrower_fname varchar(30) = null,
							@pi_borrower_lname varchar(30) = null,
							@pi_borrower_last4_SSN varchar(9) = null,
							@pi_prop_zip varchar(5) = null,
							@pi_loan_number varchar(30) = null,
							@pi_number_of_rows int,
							@po_total_rows int output'
	Set @v_full_string = @v_select_clause + @pi_where_clause
	execute sp_executesql @v_full_string
						, @v_parameter_list
							, @pi_agency_case_num
							, @pi_borrower_fname
							, @pi_borrower_lname
							, @pi_borrower_last4_SSN
							, @pi_prop_zip
							, @pi_loan_number
							, @pi_number_of_rows
							, @po_total_rows output 

	
	--get records
	Set @v_select_clause = '
				Select Top(@pi_number_of_rows)							
					foreclosure_case.[fc_id],
					foreclosure_case.[intake_dt],
					foreclosure_case.[borrower_fname],
					foreclosure_case.[borrower_lname],
					foreclosure_case.[borrower_last4_SSN],
					foreclosure_case.[co_borrower_fname],
					foreclosure_case.[co_borrower_lname],
					foreclosure_case.[co_borrower_last4_SSN],
					foreclosure_case.[prop_addr1],
					foreclosure_case.[prop_addr2],
					foreclosure_case.[prop_city],
					foreclosure_case.[prop_state_cd],
					foreclosure_case.[prop_zip],
					Agency.[agency_name] as [agency_name],
					foreclosure_case.[counselor_fname],
					foreclosure_case.[counselor_lname],
					foreclosure_case.[counselor_phone],
					foreclosure_case.[counselor_ext],
					foreclosure_case.[counselor_email],
					foreclosure_case.[completed_dt],
					case_loan.[loan_delinq_status_cd] as [delinquent_cd],
					foreclosure_case.[bankruptcy_ind],
					foreclosure_case.[fc_notice_received_ind],
					foreclosure_case.[loan_list] as [loan_number],
					servicer.[servicer_name] as [loan_servicer], 
					foreclosure_case.[agency_case_num],

					case_loan.[case_loan_id]

				From
					foreclosure_case, Agency, case_loan, servicer '

	 Set @v_orderby_clause = ' ORDER BY foreclosure_case.[borrower_fname] asc
							, foreclosure_case.[borrower_lname] asc
							, foreclosure_case.[co_borrower_fname] asc
							, foreclosure_case.[co_borrower_lname] asc
							, foreclosure_case.[fc_id] desc'
	Set @v_full_string = @v_select_clause + @pi_where_clause + @v_orderby_clause
	
	Set @v_parameter_list = '@pi_agency_case_num varchar(30) = null ,
							@pi_borrower_fname varchar(30) = null,
							@pi_borrower_lname varchar(30) = null,
							@pi_borrower_last4_SSN varchar(9) = null,
							@pi_prop_zip varchar(5) = null,
							@pi_loan_number varchar(30) = null,
							@pi_number_of_rows int'
													
	
	
	execute sp_executesql @v_full_string
						, @v_parameter_list
							, @pi_agency_case_num
							, @pi_borrower_fname
							, @pi_borrower_lname
							, @pi_borrower_last4_SSN
							, @pi_prop_zip
							, @pi_loan_number
							, @pi_number_of_rows	
		
	--Set @po_total_rows = 100
		
End
GO
/****** Object:  StoredProcedure [dbo].[Hpf_agency_get]    Script Date: 01/09/2009 16:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	 Get agency
-- =============================================
CREATE PROCEDURE [dbo].[Hpf_agency_get] 	
AS
CREATE TABLE #agency (agency_id int, agency_name varchar(50));
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- Get all agencies
	INSERT INTO #agency VALUES(-1, 'ALL');
	INSERT INTO #agency 
	SELECT a.agency_id, a.agency_name
	FROM Agency a
	ORDER BY a.agency_name;
	
	SELECT agency_id, agency_name FROM #agency;	 
	DROP TABLE #Agency
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_program_get]    Script Date: 01/09/2009 16:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thao Nguyen
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_program_get]
AS
CREATE TABLE #program([program_id] int, [program_name] varchar(50));
BEGIN

	INSERT INTO #program VALUES(-1, 'ALL');
	INSERT INTO #program 
	SELECT p.program_id, p.[program_name]
	FROM program p
	ORDER BY p.[program_name];
	
	SELECT program_id, [program_name] FROM #program;	 
	DROP TABLE #program

END
GO
/****** Object:  StoredProcedure [dbo].[hpf_agency_payable_search_draft]    Script Date: 01/09/2009 16:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 05 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Search FCase for create draft new Agency Payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_search_draft]
	@pi_agency_id				int = null,
    @pi_start_dt				datetime = null,
    @pi_end_dt					datetime = null,	
	@pi_case_completed_ind		varchar(1) = null,
	@pi_servicer_consent_ind	varchar(1) = null,
	@pi_funding_consent_ind		varchar(1) = null,
	@pi_loan_1st_2nd_cd			varchar(15)= null,
	@pi_max_number_cases		int = null
AS
DECLARE @v_sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
	If 	@pi_max_number_cases IS NULL OR @pi_max_number_cases > 500
		SELECT @pi_max_number_cases = 500
	SET @v_sql = 
		'SELECT	TOP '+ cast(@pi_max_number_cases as varchar(4))+' f1.fc_id, f1.agency_id, f1.agency_case_num, f1.completed_dt
			, f1.pmt_rate, l.acct_num, s.servicer_name
			, f1.borrower_fname + '' '' + f1.borrower_lname as borrower_name			
		FROM	(SELECT f.fc_id, f.agency_id, f.agency_case_num, f.completed_dt, ar.pmt_rate, f.borrower_fname , f.borrower_lname 
						, f.case_complete_ind, f.servicer_consent_ind, f.funding_consent_ind
				FROM foreclosure_case f LEFT OUTER JOIN agency_rate ar 
				ON	f.agency_id = ar.agency_id 
					AND f.program_id = ar.program_id 			
					AND f.completed_dt IS NOT NULL 
					AND f.completed_dt BETWEEN ar.eff_dt AND exp_dt	
				) f1 INNER JOIN case_loan l ON f1.fc_id= l.fc_id';
	IF @pi_loan_1st_2nd_cd IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND l.loan_1st_2nd_cd = ''' + cast(@pi_loan_1st_2nd_cd  as varchar(15)) + '''';
	SELECT @v_sql = @v_sql + ' INNER JOIN servicer s ON l.servicer_id = s.servicer_id'
				+ ' WHERE f1.agency_id = ' + cast(@pi_agency_id as varchar(10))
				+ ' AND f1.completed_dt BETWEEN ''' + cast(@pi_start_dt as varchar(11)) + ''' AND ''' + cast(@pi_end_dt as varchar(11)) + '''';
	IF @pi_case_completed_ind IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.case_complete_ind = ''' + cast(@pi_case_completed_ind as varchar(1)) + '''';
	IF @pi_servicer_consent_ind IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.servicer_consent_ind = ''' + cast(@pi_servicer_consent_ind  as varchar(1)) + '''';
	IF @pi_funding_consent_ind IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.funding_consent_ind = ''' + cast(@pi_funding_consent_ind  as varchar(1))+ '''';
	--Print @v_sql;
	EXECUTE (@v_sql);
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_invoice_case_insert]    Script Date: 01/09/2009 16:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen
-- Create date: 2009/01/06
-- Description:	Insert invoice case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_case_insert]
	-- Add the parameters for the stored procedure here
	
	@pi_fc_id int  = -1
	,@pi_invoice_payment_id int = null
	,@pi_Invoice_id int = -1
	,@pi_pmt_reject_reason_cd varchar(15) = null
	,@pi_invoice_case_pmt_amt decimal(15,2) = null
	,@pi_invoice_case_bill_amt decimal(15,2) = null
	,@pi_in_dispute_ind varchar(1) = null
	,@pi_rebill_ind varchar(1) = null
	,@pi_intent_to_pay_flg_TBD int = 0
	,@pi_create_dt datetime = null
	,@pi_create_user_id varchar(30) = null
	,@pi_create_app_name varchar(20) = null
	,@pi_chg_lst_dt datetime = null
	,@pi_chg_lst_user_id varchar(30) = null
	,@pi_chg_lst_app_name varchar(20) = null
	,@pi_NFMC_difference_paid_ind varchar(1)  = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [hpf].[dbo].[dbo.invoice_case]
           (
			[fc_id]
			,invoice_payment_id
			,Invoice_id
			,pmt_reject_reason_cd
			,invoice_case_pmt_amt
			,invoice_case_bill_amt
			,in_dispute_ind
			,rebill_ind
			,intent_to_pay_flg_TBD			
           ,[create_dt]
           ,[create_user_id]
           ,[create_app_name]
           ,[chg_lst_dt]
           ,[chg_lst_user_id]
           ,[chg_lst_app_name]
			)
     VALUES
		(	
			@pi_fc_id
			,@pi_invoice_payment_id
			,@pi_Invoice_id
			,@pi_pmt_reject_reason_cd
			,@pi_invoice_case_pmt_amt
			,@pi_invoice_case_bill_amt
			,@pi_in_dispute_ind
			,@pi_rebill_ind
			,@pi_intent_to_pay_flg_TBD
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
		)
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_invoice_search]    Script Date: 01/09/2009 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 06 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Search Invoice list
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_search]
	@pi_funding_source_id		int = null,
	@pi_start_dt				datetime = null,
	@pi_end_dt					datetime = null
AS
DECLARE @v_sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
	SELECT @v_sql = 'SELECT	i.funding_source_id, fs.funding_source_name, i.invoice_id, i.invoice_dt
			, cast(i.period_start_dt as varchar(11)) + ''-'' + cast(i.period_end_dt as varchar(11)) as invoice_period
			, i.invoice_bill_amt, i.invoice_pmt_amt, i.status_cd, i.invoice_comment
	FROM	invoice i INNER JOIN funding_source fs ON i.funding_source_id = fs.funding_source_id
	WHERE	i.period_start_dt >= ''' + cast(@pi_start_dt as varchar(11))+ ''' AND i.period_end_dt <= ''' + cast(@pi_end_dt as varchar(11)) + '''';
	IF @pi_funding_source_id > 0
		SELECT @v_sql = @v_sql + ' AND i.funding_source_id =' + cast(@pi_funding_source_id as varchar(10));
	EXECUTE (@v_sql);
--	PRINT @v_sql;
END;
GO
/****** Object:  StoredProcedure [dbo].[hpf_invoice_case_search_draft]    Script Date: 01/09/2009 16:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 06 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Search FCase for create draft new Invoice case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_case_search_draft]
	@pi_funding_source_id		int = null,
	@pi_program_id				int = null,
    @pi_start_dt				datetime = null,
    @pi_end_dt					datetime = null,	
	@pi_duplicate_ind			varchar(1) = 'N',
	@pi_case_completed_ind		varchar(1) = 'N',
	@pi_is_billed_ind			varchar(1) = 'N',				
	@pi_servicer_consent_ind	varchar(1) = 'N',
	@pi_funding_consent_ind		varchar(1) = 'N',
	@pi_loan_1st_2nd_cd			varchar(15)= null,
	@pi_max_number_cases		int = null,
	@pi_gender_cd				varchar(15)= null,
	@pi_race_cd					varchar(15)= null,	
	@pi_ethnicity_cd			varchar(15)= null,
	@pi_household_cd			varchar(15)= null,
	@pi_city					varchar(30)= null,
	@pi_state_cd				varchar(15)= null,
	@pi_min_age					int = null,
	@pi_max_age					int = null,
	@pi_min_gross_annual_income	int = null,
	@pi_max_gross_annual_income	int = null
AS
DECLARE @v_sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
	If 	@pi_max_number_cases IS NULL OR @pi_max_number_cases > 500
		SELECT @pi_max_number_cases = 500
	SET @v_sql = 
		'SELECT	TOP '+ cast(@pi_max_number_cases as varchar(4))+' f1.fc_id, f1.funding_source_id, f1.agency_case_num, f1.completed_dt
			, f1.bill_rate, l.acct_num, s.servicer_name
			, f1.borrower_fname + '' '' + f1.borrower_lname as borrower_name			
		FROM	(SELECT f.fc_id, fr.funding_source_id, f.program_id, f.agency_case_num, f.completed_dt, fr.bill_rate, f.borrower_fname, f.borrower_lname'
					+ ' ,f.duplicate_ind, f.case_complete_ind, f.servicer_consent_ind, f.funding_consent_ind'
					+ ' ,f.gender_cd, f.race_cd, DATEDIFF(year, borrower_DOB, GETDATE()) as age'
					+ ' ,f.household_gross_annual_income_amt, f.household_cd, prop_city, prop_state_cd'
				+ ' FROM foreclosure_case f LEFT OUTER JOIN funding_source_rate fr '
				+ ' ON	f.program_id = fr.program_id AND f.completed_dt IS NOT NULL '
				+ ' AND f.completed_dt BETWEEN fr.eff_dt AND fr.exp_dt	'
				+ ' ) f1 INNER JOIN case_loan l ON f1.fc_id= l.fc_id';
	IF @pi_loan_1st_2nd_cd IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND l.loan_1st_2nd_cd = ''' + cast(@pi_loan_1st_2nd_cd  as varchar(15)) + '''';
	SELECT @v_sql = @v_sql + ' INNER JOIN servicer s ON l.servicer_id = s.servicer_id'
				+ ' WHERE f1.funding_source_id = ' + cast(@pi_funding_source_id as varchar(10))
				+ ' AND f1.completed_dt BETWEEN ''' + cast(@pi_start_dt as varchar(11)) + ''' AND ''' + cast(@pi_end_dt as varchar(11)) + '''';
	IF @pi_program_id IS NOT NULL 
		SELECT @v_sql = @v_sql + ' AND f1.program_id = ' + cast(@pi_program_id as varchar(10));
	IF @pi_duplicate_ind IS NOT NULL 
		SELECT @v_sql = @v_sql + ' AND f1.duplicate_ind = ''' + cast(@pi_duplicate_ind as varchar(5)) + '''';
	IF @pi_case_completed_ind IS NOT NULL 
		SELECT @v_sql = @v_sql + ' AND f1.case_complete_ind = ''' + cast(@pi_case_completed_ind as varchar(5)) + '''';
	IF @pi_is_billed_ind = 'Y'
		SELECT @v_sql = @v_sql + ' AND f1.fc_id EXISTS (SELECT fc_id FROM invoice_case)'
	IF @pi_servicer_consent_ind IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.servicer_consent_ind = ''' + cast(@pi_servicer_consent_ind  as varchar(5)) + '''';
	IF @pi_funding_consent_ind IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.funding_consent_ind = ''' + cast(@pi_funding_consent_ind  as varchar(5))+ '''';
	IF @pi_gender_cd IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.gender_cd = ''' + cast(@pi_gender_cd as varchar(15))+ '''';
	IF @pi_race_cd IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.race_cd = ''' + cast(@pi_race_cd as varchar(15))+ '''';
	IF @pi_household_cd IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.household_cd = ''' + cast(@pi_household_cd as varchar(15))+ '''';
	IF @pi_state_cd IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.prop_state_cd = ''' + cast(@pi_state_cd as varchar(15))+ '''';
	IF @pi_city IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.prop_city = ''' + cast(@pi_city as varchar(30))+ '''';
	IF @pi_min_gross_annual_income IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.household_gross_annual_income_amt >= ' + cast(@pi_min_gross_annual_income as varchar(15));
	IF @pi_max_gross_annual_income IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.household_gross_annual_income_amt <= ' + cast(@pi_max_gross_annual_income as varchar(15));
	IF @pi_min_age IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.age >=' + cast(@pi_min_age as varchar(3))
	IF @pi_max_age IS NOT NULL
		SELECT @v_sql = @v_sql + ' AND f1.age <=' + cast(@pi_max_age as varchar(3))
	 Print @v_sql;
	EXECUTE (@v_sql);
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_search_app_dynamic]    Script Date: 01/09/2009 16:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thao Nguyen 
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Seacrh case (WEB APP) pager with code
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_search_app_dynamic]
(
	@pi_last4SSN varchar(4)=null ,
	@pi_fname varchar(30)=null,
	@pi_lname varchar(30)=null,
	@pi_fc_id int =-1,
	@pi_agencycaseid varchar(30)=null,
	@pi_loannum varchar(50)=null,
	@pi_propzip varchar(5)=null,
	@pi_propstate varchar(15)=null,
	@pi_duplicate char(1)=null,
	@pi_agencyid int=-1,
	@pi_programid int=-1, 	
@pi_pagesize int,
@pi_pagenum int,
@whereclause nvarchar(1000)=null,
@po_totalrownum int output
)
AS
BEGIN	
DECLARE @cmdstring nvarchar(4000)
DECLARE @parameterlist nvarchar(1000)

SET @cmdstring='with foreclosure_hpf as(
	SELECT rownum=ROW_NUMBER() OVER (ORDER BY borrower_lname desc,co_borrower_lname desc,borrower_fname asc,
	 co_borrower_fname asc,f.fc_id desc ), f.fc_id, f.agency_id,
	 completed_dt, intake_dt, borrower_fname, borrower_lname, borrower_last4_SSN, co_borrower_fname,
	 co_borrower_lname, co_borrower_last4_SSN, prop_addr1, prop_city, prop_state_cd, prop_zip, a.agency_name, 
	 counselor_email, counselor_phone,counselor_fname,counselor_lname, agency_case_num,
     counselor_ext, loan_list, bankruptcy_ind, fc_notice_received_ind, l.loan_delinq_status_cd 
     
	FROM Agency a INNER JOIN foreclosure_case f ON a.agency_id = f.agency_id INNER JOIN program p 
     ON f.program_id = p.program_id INNER JOIN case_loan l ON f.fc_id = l.fc_id 
	WHERE (1=1'+@whereclause+'))
	
	SELECT *
FROM foreclosure_hpf 
WHERE rownum BETWEEN @pi_pagesize*(@pi_pagenum-1)+1 AND (@pi_pagesize*@pi_pagenum)

SELECT @po_totalrownum =COUNT(*) 
FROM Agency a INNER JOIN foreclosure_case f ON a.agency_id = f.agency_id INNER JOIN program p 
     ON f.program_id = p.program_id INNER JOIN case_loan l ON f.fc_id = l.fc_id 
	WHERE (1=1'+@whereclause+')'


	
	SET @parameterlist='
	@pi_last4SSN varchar(4)=null ,
	@pi_fname varchar(30)=null,
	@pi_lname varchar(30)=null,
	@pi_fc_id int =-1,
	@pi_agencycaseid varchar(30)=null,
	@pi_loannum varchar(50)=null,
	@pi_propzip varchar(5)=null,
	@pi_propstate varchar(15)=null,
	@pi_duplicate char(1)=null,
	@pi_agencyid int=-1,
	@pi_programid int=-1, 	
	@pi_pagesize int,
	@pi_pagenum int,
	@po_totalrownum int output'

	
	exec sp_executesql 
	@cmdstring,
	@parameterlist,
	@pi_last4SSN,
	@pi_fname,
	@pi_lname,
	@pi_fc_id,
	@pi_agencycaseid,
	@pi_loannum,
	@pi_propzip,
	@pi_propstate,
	@pi_duplicate,
	@pi_agencyid,
	@pi_programid,
	@pi_pagesize,
	@pi_pagenum,
	@po_totalrownum  output

END
GO
/****** Object:  UserDefinedFunction [dbo].[hpf_wildcard_search_string]    Script Date: 01/09/2009 16:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[hpf_wildcard_search_string](@str nvarchar(100))
Returns nvarchar(102)
as
Begin	
	return '%' + @str + '%'
End
GO
/****** Object:  UserDefinedFunction [dbo].[hpf_check_over_one_year]    Script Date: 01/09/2009 16:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[hpf_check_over_one_year](@completed_dt datetime = null)
Returns int
As
Begin		
	Declare @today AS datetime, @oneyearbefore As datetime, @returnvalue as int
	set @today = getdate()
	set @oneyearbefore = dateadd(Dd, -1, @today)
	set @oneyearbefore = dateadd(Yy, -1, @today)
	If (((@completed_dt <= @today) and (@completed_dt >= @oneyearbefore)) or (@completed_dt is null))
		set @returnvalue = -1
	else
		set @returnvalue = 1
	return @returnvalue
End
GO
/****** Object:  StoredProcedure [dbo].[hpf_case_loan_delete]    Script Date: 01/09/2009 16:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Delete case loan
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_loan_delete]	
	       @pi_fc_id int
           ,@pi_servicer_id int           
           ,@pi_acct_num varchar(30)           
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	DELETE FROM case_loan
	WHERE	[servicer_id] = @pi_servicer_id
			AND [acct_num] = @pi_acct_num
			AND [fc_id] = @pi_fc_id
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_outcome_type_get]    Script Date: 01/09/2009 16:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 07 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Get OutcomeType list
-- =============================================
CREATE PROCEDURE [dbo].[hpf_outcome_type_get]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [outcome_type_id]
		  ,[outcome_type_name]	
		  ,[payable_ind]	  
	FROM [dbo].[outcome_type]
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_ref_code_item_get]    Script Date: 01/09/2009 16:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get reference code item
-- =============================================
CREATE PROCEDURE [dbo].[hpf_ref_code_item_get]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	ref_code_item_id
			, ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind
			, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name
	FROM	ref_code_item
	ORDER BY ref_code_set_name, sort_order;
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_agency_payable_insert]    Script Date: 01/09/2009 16:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20090106
-- Description:	Insert agency payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_insert]
	-- Add the parameters for the stored procedure here
	 @pi_agency_id int = null
	,@pi_pmt_dt datetime = null
	,@pi_pmt_cd varchar(15) = null
	,@pi_status_cd varchar(15) = null
	,@pi_period_start_dt datetime = null
	,@pi_period_end_dt datetime = null
	,@pi_pmt_comment varchar(300) = null
	,@pi_accounting_link_TBD varchar(30) = null
	,@pi_create_dt datetime = null
	,@pi_create_user_id varchar(30) = null
	,@pi_create_app_name varchar(20) = null
	,@pi_chg_lst_dt datetime = null
	,@pi_chg_lst_user_id varchar(30) = null
	,@pi_chg_lst_app_name varchar(20) = null
	,@pi_agency_payable_pmt_amt numeric(15,2) = null
	,@po_agency_payable_id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [hpf].[dbo].[agency_payable]
           ([agency_id]
           ,[pmt_dt]
           ,[pmt_cd]
           ,[status_cd]
           ,[period_start_dt]
           ,[period_end_dt]
           ,[pmt_comment]
           ,[accounting_link_TBD]
           ,[create_dt]
           ,[create_user_id]
           ,[create_app_name]
           ,[chg_lst_dt]
           ,[chg_lst_user_id]
           ,[chg_lst_app_name]
           ,[agency_payable_pmt_amt])
     VALUES
           (
			@pi_agency_id
			,@pi_pmt_dt
			,@pi_pmt_cd
			,@pi_status_cd
			,@pi_period_start_dt
			,@pi_period_end_dt
			,@pi_pmt_comment
			,@pi_accounting_link_TBD
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
			,@pi_agency_payable_pmt_amt
		)

SET @po_agency_payable_id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_agency_payable_update]    Script Date: 01/09/2009 16:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 2009/01/06
-- Description:	Update agency payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_update]
	-- Add the parameters for the stored procedure here
	@agency_payable_id int = null
	,@pi_agency_id int = null
	,@pi_pmt_dt datetime = null
	,@pi_pmt_cd varchar(15) = null
	,@pi_status_cd varchar(15) = null
	,@pi_period_start_dt datetime = null
	,@pi_period_end_dt datetime = null
	,@pi_pmt_comment varchar(300) = null
	,@pi_accounting_link_TBD varchar(30) = null	
	,@pi_chg_lst_dt datetime = null
	,@pi_chg_lst_user_id varchar(30) = null
	,@pi_chg_lst_app_name varchar(20) = null
	,@pi_agency_payable_pmt_amt numeric(15,2) = null	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [hpf].[dbo].[agency_payable]
   SET [agency_id] = @pi_agency_id
      ,[pmt_dt] = @pi_pmt_dt
      ,[pmt_cd] = @pi_pmt_cd
      ,[status_cd] = @pi_status_cd
      ,[period_start_dt] = @pi_period_start_dt
      ,[period_end_dt] = @pi_period_end_dt
      ,[pmt_comment] = @pi_pmt_comment
      ,[accounting_link_TBD] = @pi_accounting_link_TBD      
      ,[chg_lst_dt] = @pi_chg_lst_dt
      ,[chg_lst_user_id] = @pi_chg_lst_user_id
      ,[chg_lst_app_name] = @pi_chg_lst_app_name
      ,[agency_payable_pmt_amt] = @pi_agency_payable_pmt_amt
 WHERE [agency_payable_id] = @agency_payable_id
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_case_loan_insert]    Script Date: 01/09/2009 16:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insert case loan
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_loan_insert]
	        @pi_fc_id int
           ,@pi_servicer_id int = null
           ,@pi_other_servicer_name varchar(50) = null
           ,@pi_acct_num varchar(30) = null
           ,@pi_loan_1st_2nd_cd varchar(15) = null
           ,@pi_mortgage_type_cd varchar(15) = null
           ,@pi_arm_reset_ind varchar(1) = null
           ,@pi_term_length_cd varchar(15) = null
           ,@pi_loan_delinq_status_cd varchar(15) = null
           ,@pi_current_loan_balance_amt numeric(15,2) = null
           ,@pi_orig_loan_amt numeric(15,2) = null
           ,@pi_interest_rate numeric(5,3) = null
           ,@pi_originating_lender_name varchar(50) = null
           ,@pi_orig_mortgage_co_FDIC_NCUS_num varchar(20) = null
           ,@pi_orig_mortgage_co_name varchar(50) = null
           ,@pi_orginal_loan_num varchar(30) = null
           ,@pi_FDIC_NCUS_num_current_servicer_TBD varchar(30) = null
           ,@pi_current_servicer_name_TBD varchar(30) = null  
           ,@pi_investor_loan_num varchar(30) = null
           ,@pi_create_dt datetime = null
		   ,@pi_create_user_id  varchar(30) = null
		   ,@pi_create_app_name varchar(20) = null
		   ,@pi_chg_lst_dt datetime = null
		   ,@pi_chg_lst_user_id varchar(30) = null
		   ,@pi_chg_lst_app_name varchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[case_loan]
           ([fc_id]
           ,[servicer_id]
           ,[other_servicer_name]
           ,[acct_num]
           ,[loan_1st_2nd_cd]
           ,[mortgage_type_cd]
           ,[arm_reset_ind]
           ,[term_length_cd]
           ,[loan_delinq_status_cd]
           ,[current_loan_balance_amt]
           ,[orig_loan_amt]
           ,[interest_rate]
           ,[originating_lender_name]
           ,[orig_mortgage_co_FDIC_NCUS_num]
           ,[orig_mortgage_co_name]
           ,[orginal_loan_num]
           ,[FDIC_NCUS_num_current_servicer_TBD]
           ,[current_servicer_name_TBD]           
           ,[investor_loan_num]
           ,[create_dt]
		   ,[create_user_id]
		   ,[create_app_name]
		   ,[chg_lst_dt]
		   ,[chg_lst_user_id]
		   ,[chg_lst_app_name]           
           )
     VALUES
           (           
            @pi_fc_id
           ,@pi_servicer_id
           ,@pi_other_servicer_name
           ,@pi_acct_num
           ,@pi_loan_1st_2nd_cd
           ,@pi_mortgage_type_cd
           ,@pi_arm_reset_ind
           ,@pi_term_length_cd
           ,@pi_loan_delinq_status_cd
           ,@pi_current_loan_balance_amt
           ,@pi_orig_loan_amt
           ,@pi_interest_rate
           ,@pi_originating_lender_name
           ,@pi_orig_mortgage_co_FDIC_NCUS_num
           ,@pi_orig_mortgage_co_name
           ,@pi_orginal_loan_num
           ,@pi_FDIC_NCUS_num_current_servicer_TBD
           ,@pi_current_servicer_name_TBD           
           ,@pi_investor_loan_num   
           ,@pi_create_dt
		   ,@pi_create_user_id
		   ,@pi_create_app_name
		   ,@pi_chg_lst_dt
	  	   ,@pi_chg_lst_user_id
		   ,@pi_chg_lst_app_name            
          )
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_case_loan_update]    Script Date: 01/09/2009 16:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[hpf_case_loan_update]
	-- Add the parameters for the stored procedure here
	       @pi_case_loan_id int
		   ,@pi_fc_id int
           ,@pi_servicer_id int
           ,@pi_other_servicer_name varchar(50)
           ,@pi_acct_num varchar(30)
           ,@pi_loan_1st_2nd_cd varchar(15)
           ,@pi_mortgage_type_cd varchar(15)
           ,@pi_arm_reset_ind varchar(1)
           ,@pi_term_length_cd varchar(15)
           ,@pi_loan_delinq_status_cd varchar(15)
           ,@pi_current_loan_balance_amt numeric(15,2)
           ,@pi_orig_loan_amt numeric(15,2)
           ,@pi_interest_rate numeric(5,3)
           ,@pi_originating_lender_name varchar(50)
           ,@pi_orig_mortgage_co_FDIC_NCUS_num varchar(20)
           ,@pi_orig_mortgage_co_name varchar(50)           
           ,@pi_FDIC_NCUS_num_current_servicer_TBD varchar(30)
           ,@pi_current_servicer_name_TBD varchar(30)                      
           ,@pi_investor_loan_num varchar(30)
           ,@pi_orginal_loan_num varchar(30)           
		   ,@pi_chg_lst_dt datetime = null
		   ,@pi_chg_lst_user_id varchar(30) = null
		   ,@pi_chg_lst_app_name varchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[case_loan]
   SET [acct_num] = @pi_acct_num
	  ,[servicer_id] = @pi_servicer_id
	  ,[other_servicer_name] = @pi_other_servicer_name      
      ,[loan_1st_2nd_cd] = @pi_loan_1st_2nd_cd
      ,[mortgage_type_cd] = @pi_mortgage_type_cd
      ,[arm_reset_ind] = @pi_arm_reset_ind
      ,[term_length_cd] = @pi_term_length_cd
      ,[loan_delinq_status_cd] = @pi_loan_delinq_status_cd
      ,[current_loan_balance_amt] = @pi_current_loan_balance_amt
      ,[orig_loan_amt] = @pi_orig_loan_amt
      ,[interest_rate] = @pi_interest_rate
      ,[originating_lender_name] = @pi_originating_lender_name
      ,[orig_mortgage_co_FDIC_NCUS_num] = @pi_orig_mortgage_co_FDIC_NCUS_num
      ,[orig_mortgage_co_name] = @pi_orig_mortgage_co_name      
      ,[FDIC_NCUS_num_current_servicer_TBD] = @pi_FDIC_NCUS_num_current_servicer_TBD
      ,[current_servicer_name_TBD] = @pi_current_servicer_name_TBD            
      ,[investor_loan_num] = @pi_investor_loan_num
      ,[orginal_loan_num] = @pi_orginal_loan_num
 WHERE [case_loan_id] = @pi_case_loan_id
       
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_case_loan_get]    Script Date: 01/09/2009 16:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get case loan (WEB Service)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_loan_get]
	@pi_fc_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT	case_loan_id
			,fc_id
			,servicer_id
			,other_servicer_name
			,acct_num
			,loan_1st_2nd_cd
			,mortgage_type_cd
			,arm_reset_ind
			,term_length_cd
			,loan_delinq_status_cd
			,current_loan_balance_amt
			,orig_loan_amt
			,interest_rate
			,originating_lender_name
			,orig_mortgage_co_FDIC_NCUS_num
			,orig_mortgage_co_name
			,orginal_loan_num
			,FDIC_NCUS_num_current_servicer_TBD
			,current_servicer_name_TBD
			,create_dt
			,create_app_name
			,create_user_id
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
			,investor_loan_num 
    FROM Case_Loan
    WHERE fc_id = @pi_fc_id      
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_ws_user_get_from_username_password]    Script Date: 01/09/2009 16:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		PhuocDang
-- Create date: 12/09/2008
-- Project : HPF 
-- Build 
-- Description:	Get user with username and password provided
-- =============================================
CREATE PROCEDURE [dbo].[hpf_ws_user_get_from_username_password] 	
	@pi_username varchar(30), 
	@pi_password varchar(30)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ws_user_id, agency_id, call_center_id, active_ind, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name
	FROM ws_user
	WHERE login_username = @pi_username
	AND	login_password = @pi_password
   
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_menu_bar_permission]    Script Date: 01/09/2009 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sinh Tran
-- Create date: 01/08/2009
-- Project : HPF 
-- Build 
-- Description:	Get menu_item permission for one user
-- =============================================
CREATE PROCEDURE [dbo].[hpf_menu_bar_permission] 	
	@pi_userid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT mg.menu_group_id,group_name,group_sort_order,group_target,mi.menu_item_id,item_name,item_sort_order,item_target,permission_value,visibled 
	FROM menu_security ms,menu_item mi,menu_group mg 
	WHERE		ms.ccrc_user_id=@pi_userid 
			AND ms.menu_item_id=mi.menu_item_id
			AND mi.menu_group_id = mg.menu_group_id
	ORDER BY group_sort_order,item_sort_order
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_menu_group_get]    Script Date: 01/09/2009 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sinh Tran
-- Create date: 01/08/2009
-- Project : HPF 
-- Build 
-- Description:	Get all menu groups
-- =============================================
CREATE PROCEDURE [dbo].[hpf_menu_group_get] 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT	menu_group_id, group_name, group_sort_order, group_target
	FROM	menu_group 
	ORDER BY group_sort_order
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_menu_security_get]    Script Date: 01/09/2009 16:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sinh Tran
-- Create date: 01/08/2009
-- Project : HPF 
-- Build 
-- Description:	Get Web user with username provided
-- =============================================
CREATE PROCEDURE [dbo].[hpf_menu_security_get] 	
	@pi_userid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT permission_value,item_target
	FROM menu_item mi,menu_security ms
	WHERE ms.ccrc_user_id=@pi_userid AND ms.menu_item_id=mi.menu_item_id
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_geo_code_ref_get]    Script Date: 01/09/2009 16:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 12/24/2008
-- Description:	Get GeoCodeRef
-- =============================================
CREATE PROCEDURE [dbo].[hpf_geo_code_ref_get]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
       [geocode_ref_id]
      ,[zip_code]
      ,[zip_type]
      ,[city_name]
      ,[city_type]
      ,[county_name]
      ,[county_FIPS]
      ,[state_name]
      ,[state_abbr]
      ,[state_FIPS]
      ,[MSA_code]
      ,[area_code]
      ,[time_zone]
      ,[utc]
      ,[dst]
      ,[latitude]
      ,[longitude]
   FROM [geocode_ref] 
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_call_get]    Script Date: 01/09/2009 16:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get call
-- =============================================
CREATE PROCEDURE [dbo].[hpf_call_get]
	-- Add the parameters for the stored procedure here
	@pi_call_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	call_id
			,call_center_id
			,cc_agent_id_key
			,start_dt
			,end_dt
			,dnis
			,call_center
			,call_source_cd
			,reason_for_call
			,loan_acct_num
			,fname
			,lname
			,servicer_id
			,other_servicer_name
			,prop_zip_full9
			,prev_agency_id
			,selected_agency_id
			,screen_rout
			,final_dispo_cd
			,trans_num
			,cc_call_key
			,loan_delinq_status_cd
			,selected_counselor
			,homeowner_ind
			,power_of_attorney_ind
			,authorized_ind
			,create_dt
			,create_user_id
			,create_app_name
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
  FROM		call
  WHERE		call_id = @pi_call_id 
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_call_insert]    Script Date: 01/09/2009 16:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insert call
-- =============================================
CREATE PROCEDURE [dbo].[hpf_call_insert]	
	@pi_call_center_id int = 0,
	@pi_cc_agent_id_key varchar(55) = null,	
	@pi_start_dt datetime = null,
	@pi_end_dt datetime = null,
	@pi_dnis varchar(10) = null,
	@pi_call_center varchar(4) = null,
	@pi_call_source_cd varchar(15) = null,	
	@pi_reason_for_call varchar(75) = null,
	@pi_loan_acct_num varchar(30) = null,
	@pi_fname varchar(30) = null,
	@pi_lname varchar(30) = null,
	@pi_servicer_id int = 0,
	@pi_other_servicer_name varchar(50) = null,
	@pi_prop_zip_full9 varchar(9) = null,
	@pi_prev_agency_id int = 0,
	@pi_selected_agency_id varchar(20) = null,
	@pi_screen_rout varchar(2000) = null,
	@pi_final_dispo_cd varchar(15) = null,
	@pi_trans_num varchar(12) = null,
	@pi_cc_call_key varchar(18) = null,
	@pi_loan_delinq_status_cd varchar(15) = null,
	@pi_selected_counselor varchar(40) = null,
	@pi_homeowner_ind  varchar(1) = null,
	@pi_power_of_attorney_ind varchar(1) = null,
	@pi_authorized_ind varchar(1) = null,
	@pi_create_dt datetime = null,
	@pi_create_user_id varchar(30) = null,
	@pi_create_app_name varchar(20) = null,
	@pi_chg_lst_dt datetime = null,
	@pi_chg_lst_user_id varchar(30) = null,
	@pi_chg_lst_app_name varchar(20) = null,
	@po_call_id int OUTPUT
AS

SET NOCOUNT ON

INSERT INTO [dbo].[call] (
	[call_center_id],
	[cc_agent_id_key],	
	[start_dt],
	[end_dt],
	[dnis],
	[call_center],
	[call_source_cd],	
	[reason_for_call],
	[loan_acct_num],
	[fname],
	[lname],
	[servicer_id],
	[other_servicer_name],
	[prop_zip_full9],
	[prev_agency_id],
	[selected_agency_id],
	[screen_rout],
	[final_dispo_cd],
	[trans_num],
	[cc_call_key],
	[loan_delinq_status_cd],
	[selected_counselor],
	[homeowner_ind],
	[power_of_attorney_ind],
	[authorized_ind],
	[create_dt],
	[create_user_id],
	[create_app_name],
	[chg_lst_dt],
	[chg_lst_user_id],
	[chg_lst_app_name]
) VALUES (
	@pi_call_center_id,
	@pi_cc_agent_id_key,	
	@pi_start_dt,
	@pi_end_dt,
	@pi_dnis,
	@pi_call_center,
	@pi_call_source_cd,	
	@pi_reason_for_call,
	@pi_loan_acct_num,
	@pi_fname,
	@pi_lname,
	@pi_servicer_id,
	@pi_other_servicer_name,
	@pi_prop_zip_full9,
	@pi_prev_agency_id,
	@pi_selected_agency_id,
	@pi_screen_rout,
	@pi_final_dispo_cd,
	@pi_trans_num,
	@pi_cc_call_key,
	@pi_loan_delinq_status_cd,
	@pi_selected_counselor,
	@pi_homeowner_ind,
	@pi_power_of_attorney_ind,
	@pi_authorized_ind,
	@pi_create_dt,
	@pi_create_user_id,
	@pi_create_app_name,
	@pi_chg_lst_dt,
	@pi_chg_lst_user_id,
	@pi_chg_lst_app_name
)

SET @po_call_id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_set_insert]    Script Date: 01/09/2009 16:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insert budget set
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_set_insert]
	-- Add the parameters for the stored procedure here
		@pi_fc_id int = null 
		,@pi_total_income numeric(15,2) = null
		,@pi_total_expenses numeric(15,2) = null
		,@pi_total_assets numeric(15,2) = null
		,@pi_budget_set_dt datetime = null
		,@pi_create_dt datetime = null
		,@pi_create_user_id  varchar(30) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(30) = null
		,@pi_chg_lst_app_name varchar(20) = null
		,@po_budget_set_id int OutPut
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[budget_set]
           (
			[fc_id]			  
			,[total_income]
			,[total_expenses]
			,[total_assets]           
			,[budget_set_dt]  
			,[create_dt]
			,[create_user_id]
			,[create_app_name]
			,[chg_lst_dt]
			,[chg_lst_user_id]
			,[chg_lst_app_name]
           )
     VALUES
           (
			@pi_fc_id			   
			,@pi_total_income
			,@pi_total_expenses
			,@pi_total_assets
			,@pi_budget_set_dt                
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
           )
SET @po_budget_set_id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_set_update]    Script Date: 01/09/2009 16:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Update budget set
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_set_update]
	-- Add the parameters for the stored procedure here
	@pi_total_income numeric(15,2)
    ,@pi_total_expenses numeric(15,2)
    ,@pi_total_assets numeric(15,2)           
    ,@pi_budget_set_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE	[dbo].[budget_set]
    SET		[total_income] = @pi_total_income
			,[total_expenses] = @pi_total_expenses
			,[total_assets] = @pi_total_assets           
     WHERE [budget_set_id] = @pi_budget_set_id     
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_asset_get]    Script Date: 01/09/2009 16:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get budget asset(WEB Service)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_asset_get]
-- Add the parameters for the stored procedure here
   @pi_fc_id int   
AS
BEGIN
	SELECT	budget_asset_id
			,budget_set_id
			,asset_name
			,asset_value
			,create_dt
			,create_user_id
			,create_app_name
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
	FROM budget_asset
	WHERE budget_set_id =
	(
	   SELECT MAX(budget_set_id) as budget_set_id 
	   FROM budget_set
	   WHERE fc_id = @pi_fc_id
	)    
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_item_get]    Script Date: 01/09/2009 16:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get budget item(WEB Service)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_item_get]
   @pi_fc_id int   
AS
BEGIN
	SELECT	budget_item_id
			,budget_set_id
			,budget_subcategory_id
			,budget_item_amt
			,budget_note
			,create_dt
			,create_user_id
			,create_app_name
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
	FROM	budget_item 
	WHERE	budget_set_id =
			(
			SELECT MAX(budget_set_id) as budget_set_id 
			FROM budget_set
			WHERE fc_id = @pi_fc_id
			)    
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_agency_payable_case_insert]    Script Date: 01/09/2009 16:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 2009/01/06
-- Description:	Insert acency payable case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_case_insert]
	-- Add the parameters for the stored procedure here	
	@pi_fc_id int  = null
	,@pi_agency_payable_id int = null
	,@pi_pmt_dt datetime = null
	,@pi_pmt_amt numeric(15,2) = null
	,@pi_create_dt datetime = null
	,@pi_create_user_id varchar(30) = null
	,@pi_create_app_name varchar(20) = null
	,@pi_chg_lst_dt datetime = null
	,@pi_chg_lst_user_id varchar(30) = null
	,@pi_chg_lst_app_name varchar(20) = null
	,@pi_NFMC_difference_paid_ind varchar(1)  = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [hpf].[dbo].[agency_payable_case]
           ([fc_id]
           ,[agency_payable_id]
           ,[pmt_dt]
           ,[pmt_amt]
           ,[create_dt]
           ,[create_user_id]
           ,[create_app_name]
           ,[chg_lst_dt]
           ,[chg_lst_user_id]
           ,[chg_lst_app_name]
           ,[NFMC_difference_paid_ind])
     VALUES
		(	
			@pi_fc_id
			,@pi_agency_payable_id
			,@pi_pmt_dt
			,@pi_pmt_amt
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
			,@pi_NFMC_difference_paid_ind
		)
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_agency_payable_case_update]    Script Date: 01/09/2009 16:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 2009/01/06
-- Description:	Insert acency payable case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_case_update]
	-- Add the parameters for the stored procedure here
	@pi_agency_payable_case_id int = null	
	,@pi_pmt_dt datetime = null
	,@pi_pmt_amt numeric(15,2) = null	
	,@pi_chg_lst_dt datetime = null
	,@pi_chg_lst_user_id varchar(30) = null
	,@pi_chg_lst_app_name varchar(20) = null
	,@pi_NFMC_difference_paid_ind varchar(1)  = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [hpf].[dbo].[agency_payable_case]
   SET [pmt_dt] = @pi_pmt_dt
      ,[pmt_amt] = @pi_pmt_amt      
      ,[chg_lst_dt] = @pi_chg_lst_dt
      ,[chg_lst_user_id] = @pi_chg_lst_user_id
      ,[chg_lst_app_name] = @pi_chg_lst_app_name
      ,[NFMC_difference_paid_ind] = @pi_NFMC_difference_paid_ind
 WHERE [agency_payable_case_id] = @pi_agency_payable_case_id
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_item_insert]    Script Date: 01/09/2009 16:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insert budget item
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_item_insert] 
	-- Add the parameters for the stored procedure here
		@pi_budget_set_id int = null
		,@pi_budget_subcategory_id int = null
		,@pi_budget_item_amt numeric(15, 2) = null
		,@pi_budget_note varchar(100)	= null
		,@pi_create_dt datetime = null
		,@pi_create_user_id  varchar(30) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(30) = null
		,@pi_chg_lst_app_name varchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[budget_item]
           (
			[budget_set_id]
			,[budget_subcategory_id]
			,[budget_item_amt]
			,[budget_note]
			,[create_dt]
			,[create_user_id]
			,[create_app_name]
			,[chg_lst_dt]
			,[chg_lst_user_id]
			,[chg_lst_app_name]
           )
     VALUES
           ( 
			@pi_budget_set_id
			,@pi_budget_subcategory_id
			,@pi_budget_item_amt
			,@pi_budget_note
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
			)
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_outcome_item_get]    Script Date: 01/09/2009 16:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get outcome item(WEB Service)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_outcome_item_get]
	@pi_fc_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT	outcome_item_id
			,fc_id
			,outcome_type_id
			,outcome_dt
			,outcome_deleted_dt
			,nonprofitreferral_key_num
			,ext_ref_other_name
			,create_dt
			,create_user_id
			,create_app_name
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
    FROM Outcome_Item 
    WHERE fc_id = @pi_fc_id
      AND outcome_deleted_dt IS null
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_outcome_item_update]    Script Date: 01/09/2009 16:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Update deleted  outcome item
-- =============================================
CREATE PROCEDURE [dbo].[hpf_outcome_item_update] 
		@pi_outcome_item_id int = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(30) = null
		,@pi_chg_lst_app_name varchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[outcome_item]
	SET [outcome_deleted_dt] = getdate()
		,[chg_lst_dt] = @pi_chg_lst_dt
		,[chg_lst_user_id] = @pi_chg_lst_user_id
		,[chg_lst_app_name] = @pi_chg_lst_app_name
	WHERE [outcome_item_id] = @pi_outcome_item_id ;
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_outcome_item_insert]    Script Date: 01/09/2009 16:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insesrt outcome item
-- =============================================
CREATE PROCEDURE [dbo].[hpf_outcome_item_insert]
	        @pi_outcome_type_id int = null
		   ,@pi_fc_id int = null	
           ,@pi_outcome_dt datetime = null
           ,@pi_outcome_deleted_dt datetime = null
           ,@pi_nonprofitreferral_key_num varchar(10) = null
           ,@pi_ext_ref_other_name varchar(50) = null             
			,@pi_create_dt datetime = null
			,@pi_create_user_id  varchar(30) = null
			,@pi_create_app_name varchar(20) = null
			,@pi_chg_lst_dt datetime = null
			,@pi_chg_lst_user_id varchar(30) = null
			,@pi_chg_lst_app_name varchar(20) = null
       
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[outcome_item]
           ([outcome_type_id]
           ,[fc_id]
           ,[outcome_dt]
           ,[outcome_deleted_dt]
           ,[nonprofitreferral_key_num]
           ,[ext_ref_other_name]
			,[create_dt]
			,[create_user_id]
			,[create_app_name]
			,[chg_lst_dt]
			,[chg_lst_user_id]
			,[chg_lst_app_name]
            )
     VALUES
           (@pi_outcome_type_id
           ,@pi_fc_id
           ,@pi_outcome_dt
           ,@pi_outcome_deleted_dt
           ,@pi_nonprofitreferral_key_num
           ,@pi_ext_ref_other_name
           ,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name)
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_web_user_get_from_username]    Script Date: 01/09/2009 16:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sinh Tran
-- Create date: 01/08/2009
-- Project : HPF 
-- Build 
-- Description:	Get Web user with username provided
-- =============================================
CREATE PROCEDURE [dbo].[hpf_web_user_get_from_username] 	
	@pi_username varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT ccrc_user_id, user_login_id, active_ind, user_role_str_TBD, fname, lname, email, phone, last_login_dt, create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name
	FROM ccrc_user
	WHERE user_login_id = @pi_username
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_asset_insert]    Script Date: 01/09/2009 16:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insert budget asset
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_asset_insert]
	-- Add the parameters for the stored procedure here
		@pi_budget_set_id int = null
		,@pi_asset_name varchar(50) = null
		,@pi_asset_value numeric(15,2) = null    
		,@pi_create_dt datetime = null
		,@pi_create_user_id  varchar(30) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(30) = null
		,@pi_chg_lst_app_name varchar(20) = null   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[budget_asset]
           (
			[budget_set_id]
			,[asset_name]
			,[asset_value]
			,[create_dt]
			,[create_user_id]
			,[create_app_name]
			,[chg_lst_dt]
			,[chg_lst_user_id]
			,[chg_lst_app_name]
           )
     VALUES
           (
			@pi_budget_set_id
			,@pi_asset_name
			,@pi_asset_value
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
           )
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_get_from_agencyID_and_caseNumber]    Script Date: 01/09/2009 16:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get case from Agency ID and case number
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_get_from_agencyID_and_caseNumber]
	@pi_agency_id int = 0,
	@pi_agency_case_num varchar(30) = null	
As 

Begin
	SELECT	fc_id
			,agency_id
			--,call_id
			--,program_id
			,agency_case_num
			/*,agency_client_num
			,intake_dt
			,income_earners_cd
			,case_source_cd
			,race_cd
			,household_cd
			,never_bill_reason_cd
			,never_pay_reason_cd
			,dflt_reason_1st_cd
			,dflt_reason_2nd_cd
			,hud_termination_reason_cd
			,hud_termination_dt
			,hud_outcome_cd
			,AMI_percentage
			,counseling_duration_cd
			,gender_cd
			,borrower_fname
			,borrower_lname
			,borrower_mname
			,mother_maiden_lname
			,borrower_ssn
			,borrower_last4_SSN
			,borrower_DOB
			,co_borrower_fname
			,co_borrower_lname
			,co_borrower_mname
			,co_borrower_ssn
			,co_borrower_last4_SSN
			,co_borrower_DOB
			,primary_contact_no
			,second_contact_no
			,email_1
			,contact_zip_plus4
			,email_2
			,contact_addr1
			,contact_addr2
			,contact_city
			,contact_state_cd
			,contact_zip
			,prop_addr1
			,prop_addr2
			,prop_city
			,prop_state_cd
			,prop_zip
			,prop_zip_plus_4
			,bankruptcy_ind
			,bankruptcy_attorney
			,bankruptcy_pmt_current_ind
			,borrower_educ_level_completed_cd
			,borrower_marital_status_cd
			,borrower_preferred_lang_cd
			,borrower_occupation
			,co_borrower_occupation
			,hispanic_ind
			,duplicate_ind
			,fc_notice_received_ind
			,case_complete_ind
			,completed_dt
			,funding_consent_ind
			,servicer_consent_ind
			,agency_media_consent_ind
			,hpf_media_candidate_ind
			,hpf_network_candidate_ind
			,hpf_success_story_ind
			,agency_success_story_ind
			,borrower_disabled_ind
			,co_borrower_disabled_ind
			,summary_sent_other_cd
			,summary_sent_other_dt
			,summary_sent_dt
			,occupant_num
			,loan_dflt_reason_notes
			,action_items_notes
			,followup_notes
			,prim_res_est_mkt_value
			,counselor_email
			,counselor_phone
			,counselor_ext
			,discussed_solution_with_srvcr_ind
			,worked_with_another_agency_ind
			,contacted_srvcr_recently_ind
			,has_workout_plan_ind
			,srvcr_workout_plan_current_ind
			,opt_out_newsletter_ind
			,opt_out_survey_ind
			,do_not_call_ind
			,owner_occupied_ind
			,primary_residence_ind
			,realty_company
			,property_cd
			,for_sale_ind
			,home_sale_price
			,home_purchase_year
			,home_purchase_price
			,home_current_market_value
			,military_service_cd
			,household_gross_annual_income_amt
			,loan_list
			,create_dt
			,create_user_id
			,create_app_name
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
			,counselor_fname
			,counselor_lname
			,intake_credit_score
			,intake_credit_bureau_cd
			,counselor_id_ref
			,fc_sale_date*/
	FROM	foreclosure_case 
	WHERE	agency_id = @pi_agency_id AND
			agency_case_num = @pi_agency_case_num
End
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_detail_get]    Script Date: 01/09/2009 16:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get case from FC_id
-- =============================================

CREATE PROCEDURE [dbo].[hpf_foreclosure_case_detail_get]
	@pi_fc_id int = 0	
As 
Begin
	SELECT	fc_id
			,agency_id
			,call_id
			,program_id
			,agency_case_num
			,agency_client_num
			,intake_dt
			,income_earners_cd
			,case_source_cd
			,race_cd
			,household_cd
			,never_bill_reason_cd
			,never_pay_reason_cd
			,dflt_reason_1st_cd
			,dflt_reason_2nd_cd
			,hud_termination_reason_cd
			,hud_termination_dt
			,hud_outcome_cd
			,AMI_percentage
			,counseling_duration_cd
			,gender_cd
			,borrower_fname
			,borrower_lname
			,borrower_mname
			,mother_maiden_lname
			
			,borrower_last4_SSN
			,borrower_DOB
			,co_borrower_fname
			,co_borrower_lname
			,co_borrower_mname
			
			,co_borrower_last4_SSN
			,co_borrower_DOB
			,primary_contact_no
			,second_contact_no
			,email_1
			,contact_zip_plus4
			,email_2
			,contact_addr1
			,contact_addr2
			,contact_city
			,contact_state_cd
			,contact_zip
			,prop_addr1
			,prop_addr2
			,prop_city
			,prop_state_cd
			,prop_zip
			,prop_zip_plus_4
			,bankruptcy_ind
			,bankruptcy_attorney
			,bankruptcy_pmt_current_ind
			,borrower_educ_level_completed_cd
			,borrower_marital_status_cd
			,borrower_preferred_lang_cd
			,borrower_occupation
			,co_borrower_occupation
			,hispanic_ind
			,duplicate_ind
			,fc_notice_received_ind
			,case_complete_ind
			,completed_dt
			,funding_consent_ind
			,servicer_consent_ind
			,agency_media_consent_ind
			,hpf_media_candidate_ind
			,hpf_network_candidate_ind
			,hpf_success_story_ind
			,agency_success_story_ind
			,borrower_disabled_ind
			,co_borrower_disabled_ind
			,summary_sent_other_cd
			,summary_sent_other_dt
			,summary_sent_dt
			,occupant_num
			,loan_dflt_reason_notes
			,action_items_notes
			,followup_notes
			,prim_res_est_mkt_value
			,counselor_email
			,counselor_phone
			,counselor_ext
			,discussed_solution_with_srvcr_ind
			,worked_with_another_agency_ind
			,contacted_srvcr_recently_ind
			,has_workout_plan_ind
			,srvcr_workout_plan_current_ind
			,opt_out_newsletter_ind
			,opt_out_survey_ind
			,do_not_call_ind
			,owner_occupied_ind
			,primary_residence_ind
			,realty_company
			,property_cd
			,for_sale_ind
			,home_sale_price
			,home_purchase_year
			,home_purchase_price
			,home_current_market_value
			,military_service_cd
			,household_gross_annual_income_amt
			,loan_list
			,create_dt
			,create_user_id
			,create_app_name
			,chg_lst_dt
			,chg_lst_user_id
			,chg_lst_app_name
			,counselor_fname
			,counselor_lname
			,intake_credit_score
			,intake_credit_bureau_cd
			,counselor_id_ref
			,fc_sale_dt
	FROM foreclosure_case WHERE fc_id = @pi_fc_id		
End
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_update_app]    Script Date: 01/09/2009 16:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thao Nguyen 
-- Create date: <Create Date,,>
-- Description:	Foreclosure Case Update in App
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_update_app]
(
@pi_fc_id int=null ,
@pi_agency_id int =null, 
@pi_duplicate_ind varchar(1)=null,
@pi_loan_dflt_reason_notes varchar(8000)=null,
@pi_action_items_notes varchar(8000)=null,
@pi_followup_notes varchar(8000)=null,
@pi_opt_out_newsletter_ind varchar(1)=null,
@pi_opt_out_survey_ind varchar(1)=null,
@pi_do_not_call_ind varchar(1)=null,
@pi_hpf_success_story_ind varchar(1)=null,
@pi_hpf_media_candidate_ind varchar(1)=null
)
AS
BEGIN
 UPDATE [dbo].[foreclosure_case]
   SET 
			[agency_id]=@pi_agency_id,
            [duplicate_ind]=@pi_duplicate_ind,
            [loan_dflt_reason_notes]=@pi_loan_dflt_reason_notes,
            [action_items_notes]=@pi_action_items_notes,
            [followup_notes]=@pi_followup_notes,
            [opt_out_newsletter_ind]=@pi_opt_out_newsletter_ind,
            [opt_out_survey_ind]=@pi_opt_out_survey_ind,
            [do_not_call_ind]=@pi_do_not_call_ind,
            [hpf_success_story_ind]=@pi_hpf_success_story_ind,
            [hpf_media_candidate_ind]=@pi_hpf_media_candidate_ind
WHERE [fc_id] = @pi_fc_id 
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_insert]    Script Date: 01/09/2009 16:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Insert case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_insert]
	       @pi_agency_id int = null,
           @pi_call_id int = null,
           @pi_program_id int = null,
           @pi_agency_case_num varchar(30) = null,
           @pi_agency_client_num varchar(30) = null,
           @pi_intake_dt datetime = null,
           @pi_income_earners_cd varchar(15) = null,
           @pi_case_source_cd varchar(15) = null,
           @pi_race_cd varchar(15) = null,
           @pi_household_cd varchar(15) = null,
           @pi_never_bill_reason_cd varchar(15) = null,
           @pi_never_pay_reason_cd varchar(15) = null,
           @pi_dflt_reason_1st_cd varchar(15) = null,
           @pi_dflt_reason_2nd_cd varchar(15) = null,
           @pi_hud_termination_reason_cd varchar(15) = null,
           @pi_hud_termination_dt datetime = null,
           @pi_hud_outcome_cd varchar(15) = null,
           @pi_AMI_percentage int = null,
           @pi_counseling_duration_cd varchar(15) = null,
           @pi_gender_cd varchar(15) = null,
           @pi_borrower_fname varchar(30) = null,
           @pi_borrower_lname varchar(30) = null,
           @pi_borrower_mname varchar(30) = null,
           @pi_mother_maiden_lname varchar(30) = null,
           @pi_borrower_ssn varchar(9) = null,
           @pi_borrower_last4_SSN varchar(4) = null,
           @pi_borrower_DOB datetime = null,
           @pi_co_borrower_fname varchar(30) = null,
           @pi_co_borrower_lname varchar(30) = null,
           @pi_co_borrower_mname varchar(30) = null,
           @pi_co_borrower_ssn varchar(9) = null,
           @pi_co_borrower_last4_SSN varchar(4) = null,
           @pi_co_borrower_DOB datetime = null,
           @pi_primary_contact_no varchar(20) = null,
           @pi_second_contact_no varchar(20) = null,
           @pi_email_1 varchar(50) = null,
           @pi_contact_zip_plus4 varchar(4) = null,
           @pi_email_2 varchar(50) = null,
           @pi_contact_addr1 varchar(50) = null,
           @pi_contact_addr2 varchar(50) = null,
           @pi_contact_city varchar(30) = null,
           @pi_contact_state_cd varchar(15) = null,
           @pi_contact_zip varchar(5) = null,
           @pi_prop_addr1 varchar(50) = null,
           @pi_prop_addr2 varchar(50) = null,
           @pi_prop_city varchar(30) = null,
           @pi_prop_state_cd varchar(15) = null,
           @pi_prop_zip varchar(5) = null,
           @pi_prop_zip_plus_4 varchar(4) = null,
           @pi_bankruptcy_ind varchar(1) = null,
           @pi_bankruptcy_attorney varchar(50) = null,
           @pi_bankruptcy_pmt_current_ind varchar(1) = null,
           @pi_borrower_educ_level_completed_cd varchar(15) = null,
           @pi_borrower_marital_status_cd varchar(15) = null,
           @pi_borrower_preferred_lang_cd varchar(15) = null,
           @pi_borrower_occupation varchar(50) = null,
           @pi_co_borrower_occupation varchar(50) = null,
           @pi_hispanic_ind varchar(1) = null,
           @pi_duplicate_ind varchar(1) = null,
           @pi_fc_notice_received_ind varchar(1) = null,
           @pi_case_complete_ind varchar(1) = null,
           @pi_completed_dt datetime = null,
           @pi_funding_consent_ind varchar(1) = null,
           @pi_servicer_consent_ind varchar(1) = null,
           @pi_agency_media_consent_ind varchar(1) = null,
           @pi_hpf_media_candidate_ind varchar(1) = null,
           @pi_hpf_network_candidate_ind varchar(1) = null,
           @pi_hpf_success_story_ind varchar(1) = null,
           @pi_agency_success_story_ind varchar(1) = null,
           @pi_borrower_disabled_ind varchar(1) = null,
           @pi_co_borrower_disabled_ind varchar(1) = null,
           @pi_summary_sent_other_cd varchar(15) = null,
           @pi_summary_sent_other_dt datetime = null,
           @pi_summary_sent_dt datetime = null,
           @pi_occupant_num tinyint = null,
           @pi_loan_dflt_reason_notes varchar(8000) = null,
           @pi_action_items_notes varchar(8000) = null,
           @pi_followup_notes varchar(8000) = null,
           @pi_prim_res_est_mkt_value numeric(15,2) = null,           
           @pi_counselor_email varchar(50) = null,
           @pi_counselor_phone varchar(20) = null,
           @pi_counselor_ext varchar(20) = null,
           @pi_discussed_solution_with_srvcr_ind varchar(1) = null,
           @pi_worked_with_another_agency_ind varchar(1) = null,
           @pi_contacted_srvcr_recently_ind varchar(1) = null,
           @pi_has_workout_plan_ind varchar(1) = null,
           @pi_srvcr_workout_plan_current_ind varchar(1) = null,
           @pi_opt_out_newsletter_ind varchar(1) = null,
           @pi_opt_out_survey_ind varchar(1) = null,
           @pi_do_not_call_ind varchar(1) = null,
           @pi_owner_occupied_ind varchar(1) = null,
           @pi_primary_residence_ind varchar(1) = null,
           @pi_realty_company varchar(50) = null,
           @pi_property_cd varchar(15) = null,
           @pi_for_sale_ind varchar(1) = null,
           @pi_home_sale_price numeric(15,2) = null,
           @pi_home_purchase_year int = null,
           @pi_home_purchase_price numeric(15,2) = null,
           @pi_home_current_market_value numeric(15,2) = null,
           @pi_military_service_cd varchar(15) = null,
           @pi_household_gross_annual_income_amt numeric(15,2) = null,           
           @pi_counselor_lname varchar(30) = null,
           @pi_counselor_fname varchar(30) = null,
           @pi_counselor_id_ref varchar(30) = null,
           @pi_intake_credit_score varchar(4) = null, 
		   @pi_intake_credit_bureau_cd varchar(15) = null, 
		   @pi_create_dt datetime = getdate,
		   @pi_create_user_id  varchar(30) = null,
		   @pi_create_app_name varchar(20) = null,
		   @pi_chg_lst_dt datetime = getdate ,
		   @pi_chg_lst_user_id varchar(30) = null,
		   @pi_chg_lst_app_name varchar(20) = null,
		   @pi_fc_sale_dt datetime = null,
           @po_fc_id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[foreclosure_case]
           ([agency_id]
           ,[call_id]
           ,[program_id]
           ,[agency_case_num]
           ,[agency_client_num]
           ,[intake_dt]
           ,[income_earners_cd]
           ,[case_source_cd]
           ,[race_cd]
           ,[household_cd]
           ,[never_bill_reason_cd]
           ,[never_pay_reason_cd]
           ,[dflt_reason_1st_cd]
           ,[dflt_reason_2nd_cd]
           ,[hud_termination_reason_cd]
           ,[hud_termination_dt]
           ,[hud_outcome_cd]
           ,[AMI_percentage]
           ,[counseling_duration_cd]
           ,[gender_cd]
           ,[borrower_fname]
           ,[borrower_lname]
           ,[borrower_mname]
           ,[mother_maiden_lname]
           ,[borrower_ssn]
           ,[borrower_last4_SSN]
           ,[borrower_DOB]
           ,[co_borrower_fname]
           ,[co_borrower_lname]
           ,[co_borrower_mname]
           ,[co_borrower_ssn]
           ,[co_borrower_last4_SSN]
           ,[co_borrower_DOB]
           ,[primary_contact_no]
           ,[second_contact_no]
           ,[email_1]
           ,[contact_zip_plus4]
           ,[email_2]
           ,[contact_addr1]
           ,[contact_addr2]
           ,[contact_city]
           ,[contact_state_cd]
           ,[contact_zip]
           ,[prop_addr1]
           ,[prop_addr2]
           ,[prop_city]
           ,[prop_state_cd]
           ,[prop_zip]
           ,[prop_zip_plus_4]
           ,[bankruptcy_ind]
           ,[bankruptcy_attorney]
           ,[bankruptcy_pmt_current_ind]
           ,[borrower_educ_level_completed_cd]
           ,[borrower_marital_status_cd]
           ,[borrower_preferred_lang_cd]
           ,[borrower_occupation]
           ,[co_borrower_occupation]
           ,[hispanic_ind]
           ,[duplicate_ind]
           ,[fc_notice_received_ind]
           ,[case_complete_ind]
           ,[completed_dt]
           ,[funding_consent_ind]
           ,[servicer_consent_ind]
           ,[agency_media_consent_ind]
           ,[hpf_media_candidate_ind]
           ,[hpf_network_candidate_ind]
           ,[hpf_success_story_ind]
           ,[agency_success_story_ind]
           ,[borrower_disabled_ind]
           ,[co_borrower_disabled_ind]
           ,[summary_sent_other_cd]
           ,[summary_sent_other_dt]
           ,[summary_sent_dt]
           ,[occupant_num]
           ,[loan_dflt_reason_notes]
           ,[action_items_notes]
           ,[followup_notes]
           ,[prim_res_est_mkt_value]           
           ,[counselor_email]
           ,[counselor_phone]
           ,[counselor_ext]
           ,[discussed_solution_with_srvcr_ind]
           ,[worked_with_another_agency_ind]
           ,[contacted_srvcr_recently_ind]
           ,[has_workout_plan_ind]
           ,[srvcr_workout_plan_current_ind]
           ,[opt_out_newsletter_ind]
           ,[opt_out_survey_ind]
           ,[do_not_call_ind]
           ,[owner_occupied_ind]
           ,[primary_residence_ind]
           ,[realty_company]
           ,[property_cd]
           ,[for_sale_ind]
           ,[home_sale_price]
           ,[home_purchase_year]
           ,[home_purchase_price]
           ,[home_current_market_value]
           ,[military_service_cd]
           ,[household_gross_annual_income_amt]           
           ,[counselor_lname]
           ,[counselor_fname]
           ,[counselor_id_ref]
		   ,[intake_credit_score]
		   ,[intake_credit_bureau_cd]
		   ,[fc_sale_dt]
		   ,[create_dt]
		   ,[create_user_id]
		   ,[create_app_name]
		   ,[chg_lst_dt]
		   ,[chg_lst_user_id]
		   ,[chg_lst_app_name])
     VALUES
           (
	       @pi_agency_id, 
           @pi_call_id,
           @pi_program_id,
           @pi_agency_case_num,
           @pi_agency_client_num,
           @pi_intake_dt,
           @pi_income_earners_cd,
           @pi_case_source_cd,
           @pi_race_cd,
           @pi_household_cd,
           @pi_never_bill_reason_cd,
           @pi_never_pay_reason_cd,
           @pi_dflt_reason_1st_cd,
           @pi_dflt_reason_2nd_cd,
           @pi_hud_termination_reason_cd,
           @pi_hud_termination_dt,
           @pi_hud_outcome_cd,
           @pi_AMI_percentage,
           @pi_counseling_duration_cd,
           @pi_gender_cd,
           @pi_borrower_fname,
           @pi_borrower_lname,
           @pi_borrower_mname,
           @pi_mother_maiden_lname,
           @pi_borrower_ssn,
           @pi_borrower_last4_SSN,
           @pi_borrower_DOB,
           @pi_co_borrower_fname,
           @pi_co_borrower_lname,
           @pi_co_borrower_mname,
           @pi_co_borrower_ssn,
           @pi_co_borrower_last4_SSN,
           @pi_co_borrower_DOB,
           @pi_primary_contact_no,
           @pi_second_contact_no,
           @pi_email_1,
           @pi_contact_zip_plus4,
           @pi_email_2,
           @pi_contact_addr1,
           @pi_contact_addr2,
           @pi_contact_city,
           @pi_contact_state_cd,
           @pi_contact_zip,
           @pi_prop_addr1,
           @pi_prop_addr2,
           @pi_prop_city,
           @pi_prop_state_cd,
           @pi_prop_zip,
           @pi_prop_zip_plus_4,
           @pi_bankruptcy_ind,
           @pi_bankruptcy_attorney,
           @pi_bankruptcy_pmt_current_ind,
           @pi_borrower_educ_level_completed_cd,
           @pi_borrower_marital_status_cd,
           @pi_borrower_preferred_lang_cd,
           @pi_borrower_occupation,
           @pi_co_borrower_occupation,
           @pi_hispanic_ind,
           @pi_duplicate_ind,
           @pi_fc_notice_received_ind,
           @pi_case_complete_ind,
           @pi_completed_dt,
           @pi_funding_consent_ind,
           @pi_servicer_consent_ind,
           @pi_agency_media_consent_ind,
           @pi_hpf_media_candidate_ind,
           @pi_hpf_network_candidate_ind,
           @pi_hpf_success_story_ind,
           @pi_agency_success_story_ind,
           @pi_borrower_disabled_ind,
           @pi_co_borrower_disabled_ind,
           @pi_summary_sent_other_cd,
           @pi_summary_sent_other_dt,
           @pi_summary_sent_dt,
           @pi_occupant_num,
           @pi_loan_dflt_reason_notes,
           @pi_action_items_notes,
           @pi_followup_notes,
           @pi_prim_res_est_mkt_value,           
           @pi_counselor_email,
           @pi_counselor_phone,
           @pi_counselor_ext,
           @pi_discussed_solution_with_srvcr_ind,
           @pi_worked_with_another_agency_ind,
           @pi_contacted_srvcr_recently_ind,
           @pi_has_workout_plan_ind,
           @pi_srvcr_workout_plan_current_ind,
           @pi_opt_out_newsletter_ind,
           @pi_opt_out_survey_ind,
           @pi_do_not_call_ind,
           @pi_owner_occupied_ind,
           @pi_primary_residence_ind,
           @pi_realty_company,
           @pi_property_cd,
           @pi_for_sale_ind,
           @pi_home_sale_price,
           @pi_home_purchase_year,
           @pi_home_purchase_price,
           @pi_home_current_market_value,
           @pi_military_service_cd,
           @pi_household_gross_annual_income_amt,                                       
           @pi_counselor_lname,
           @pi_counselor_fname,
           @pi_counselor_id_ref,
           @pi_intake_credit_score,
		   @pi_intake_credit_bureau_cd,
		   @pi_fc_sale_dt,
		   @pi_create_dt,
		   @pi_create_user_id,
		   @pi_create_app_name,
		   @pi_chg_lst_dt,
		   @pi_chg_lst_user_id,
		   @pi_chg_lst_app_name
         )
SET @po_fc_id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_get_duplicate]    Script Date: 01/09/2009 16:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_get_duplicate]
	@pi_agency_id int = null
	, @pi_agency_case_num varchar(30) = null
	, @pi_fc_id int = null
AS

SET NOCOUNT ON
BEGIN
	IF (@pi_fc_id is null)
		SELECT	CL.Acct_Num, A.Agency_Name, CL.Servicer_ID, FC.FC_ID, S.Servicer_name
				, FC.borrower_fname, FC.borrower_lname
				, FC.agency_case_num, FC.counselor_fname, FC.counselor_lname
				, FC.counselor_phone, FC.counselor_email, FC.counselor_ext
		FROM	Foreclosure_Case FC, Case_Loan CL, Agency A, Servicer S
				, (SELECT	CL.Acct_Num as acct_num, CL.Servicer_ID as servicer_id,	FC.FC_ID as fc_id
					FROM	Foreclosure_Case FC inner join Case_Loan CL on fc.fc_id=cl.fc_id
					WHERE  	FC.Duplicate_Ind = 'N' 
							AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate())) 
							AND	(FC.Agency_ID = @pi_agency_id)
							AND	(FC.Agency_Case_Num = @pi_agency_case_num)				
					) Table1
		WHERE  	FC.fc_id=CL.fc_id 
				AND FC.Agency_Id = A.Agency_Id
				AND CL.Servicer_ID = S.Servicer_ID
				AND FC.Duplicate_Ind= 'N' 
				AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate()))
				AND Table1.acct_num = CL.Acct_Num
				AND Table1.servicer_id = CL.Servicer_ID
				AND Table1.fc_id <> FC.FC_ID
	ELSE
		SELECT	CL.Acct_Num, A.Agency_Name, CL.Servicer_ID, FC.FC_ID, S.Servicer_name
				, FC.borrower_fname, FC.borrower_lname
				, FC.agency_case_num, FC.counselor_fname, FC.counselor_lname
				, FC.counselor_phone, FC.counselor_email, FC.counselor_ext
		FROM	Foreclosure_Case FC, Case_Loan CL, Agency A, Servicer S
				, (SELECT	CL.Acct_Num as acct_num, CL.Servicer_ID as servicer_id,	FC.FC_ID as fc_id
					FROM	Foreclosure_Case FC inner join Case_Loan CL on fc.fc_id=cl.fc_id
					WHERE  	FC.Duplicate_Ind = 'N' 
							AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate())) 				
							AND	(FC.fc_id = @pi_fc_id)
				) Table1
		WHERE  	FC.fc_id=CL.fc_id 
				AND FC.Agency_Id = A.Agency_Id
				AND CL.Servicer_ID = S.Servicer_ID
				AND FC.Duplicate_Ind= 'N' 
				AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate()))
				AND Table1.acct_num = CL.Acct_Num
				AND Table1.servicer_id = CL.Servicer_ID
				AND Table1.fc_id <> FC.FC_ID
	
		
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_update]    Script Date: 01/09/2009 16:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Update case (WEB Service)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_update]
	 --Add the parameters for the stored procedure here	       
		@pi_agency_id int = null,
		@pi_call_id int = null,
		@pi_program_id int = null,
		@pi_agency_case_num varchar(30) = null,
		@pi_agency_client_num varchar(30) = null,
		@pi_intake_dt datetime = null,
		@pi_income_earners_cd varchar(15) = null,
		@pi_case_source_cd varchar(15) = null,
		@pi_race_cd varchar(15) = null,
		@pi_household_cd varchar(15) = null,
		@pi_never_bill_reason_cd varchar(15) = null,
		@pi_never_pay_reason_cd varchar(15) = null,
		@pi_dflt_reason_1st_cd varchar(15) = null,
		@pi_dflt_reason_2nd_cd varchar(15) = null,
		@pi_hud_termination_reason_cd varchar(15) = null,
		@pi_hud_termination_dt datetime = null,
		@pi_hud_outcome_cd varchar(15) = null,
		@pi_AMI_percentage int = null,
		@pi_counseling_duration_cd varchar(15) = null,
		@pi_gender_cd varchar(15) = null,
		@pi_borrower_fname varchar(30) = null,
		@pi_borrower_lname varchar(30) = null,
		@pi_borrower_mname varchar(30) = null,
		@pi_mother_maiden_lname varchar(30) = null,
		@pi_borrower_ssn varchar(9) = null,
		@pi_borrower_last4_SSN varchar(4) = null,
		@pi_borrower_DOB datetime = null,
		@pi_co_borrower_fname varchar(30) = null,
		@pi_co_borrower_lname varchar(30) = null,
		@pi_co_borrower_mname varchar(30) = null,
		@pi_co_borrower_ssn varchar(9) = null,
		@pi_co_borrower_last4_SSN varchar(4) = null,
		@pi_co_borrower_DOB datetime = null,
		@pi_primary_contact_no varchar(20) = null,
		@pi_second_contact_no varchar(20) = null,
		@pi_email_1 varchar(50) = null,
		@pi_contact_zip_plus4 varchar(4) = null,
		@pi_email_2 varchar(50) = null,
		@pi_contact_addr1 varchar(50) = null,
		@pi_contact_addr2 varchar(50) = null,
		@pi_contact_city varchar(30) = null,
		@pi_contact_state_cd varchar(15) = null,
		@pi_contact_zip varchar(5) = null,
		@pi_prop_addr1 varchar(50) = null,
		@pi_prop_addr2 varchar(50) = null,
		@pi_prop_city varchar(30) = null,
		@pi_prop_state_cd varchar(15) = null,
		@pi_prop_zip varchar(5) = null,
		@pi_prop_zip_plus_4 varchar(4) = null,
		@pi_bankruptcy_ind varchar(1) = null,
		@pi_bankruptcy_attorney varchar(50) = null,
		@pi_bankruptcy_pmt_current_ind varchar(1) = null,
		@pi_borrower_educ_level_completed_cd varchar(15) = null,
		@pi_borrower_marital_status_cd varchar(15) = null,
		@pi_borrower_preferred_lang_cd varchar(15) = null,
		@pi_borrower_occupation varchar(50) = null,
		@pi_co_borrower_occupation varchar(50) = null,
		@pi_hispanic_ind varchar(1) = null,
		@pi_duplicate_ind varchar(1) = null,
		@pi_fc_notice_received_ind varchar(1) = null,
		@pi_case_complete_ind varchar(1) = null,
		@pi_completed_dt datetime = null,
		@pi_funding_consent_ind varchar(1) = null,
		@pi_servicer_consent_ind varchar(1) = null,
		@pi_agency_media_consent_ind varchar(1) = null,
		@pi_hpf_media_candidate_ind varchar(1) = null,
		@pi_hpf_network_candidate_ind varchar(1) = null,
		@pi_hpf_success_story_ind varchar(1) = null,
		@pi_agency_success_story_ind varchar(1) = null,
		@pi_borrower_disabled_ind varchar(1) = null,
		@pi_co_borrower_disabled_ind varchar(1) = null,
		@pi_summary_sent_other_cd varchar(15) = null,
		@pi_summary_sent_other_dt datetime = null,
		@pi_summary_sent_dt datetime = null,
		@pi_occupant_num tinyint = null,
		@pi_loan_dflt_reason_notes varchar(8000) = null,
		@pi_action_items_notes varchar(8000) = null,
		@pi_followup_notes varchar(8000) = null,
		@pi_prim_res_est_mkt_value numeric(15,2) = null,           
		@pi_counselor_email varchar(50) = null,
		@pi_counselor_phone varchar(20) = null,
		@pi_counselor_ext varchar(20) = null,
		@pi_discussed_solution_with_srvcr_ind varchar(1) = null,
		@pi_worked_with_another_agency_ind varchar(1) = null,
		@pi_contacted_srvcr_recently_ind varchar(1) = null,
		@pi_has_workout_plan_ind varchar(1) = null,
		@pi_srvcr_workout_plan_current_ind varchar(1) = null,
		@pi_opt_out_newsletter_ind varchar(1) = null,
		@pi_opt_out_survey_ind varchar(1) = null,
		@pi_do_not_call_ind varchar(1) = null,
		@pi_owner_occupied_ind varchar(1) = null,
		@pi_primary_residence_ind varchar(1) = null,
		@pi_realty_company varchar(50) = null,
		@pi_property_cd varchar(15) = null,
		@pi_for_sale_ind varchar(1) = null,
		@pi_home_sale_price numeric(15,2) = null,
		@pi_home_purchase_year int = null,
		@pi_home_purchase_price numeric(15,2) = null,
		@pi_home_current_market_value numeric(15,2) = null,
		@pi_military_service_cd varchar(15) = null,
		@pi_household_gross_annual_income_amt numeric(15,2) = null,		
		@pi_counselor_lname varchar(30) = null,
		@pi_counselor_fname varchar(30) = null,           
		@pi_counselor_id_ref varchar(30) = null,
		@pi_intake_credit_score varchar(4) = null, 
		@pi_intake_credit_bureau_cd varchar(15) = null, 
		@pi_chg_lst_dt datetime = null,
		@pi_chg_lst_user_id varchar(30) = null,
		@pi_chg_lst_app_name varchar(20) = null,
		@pi_fc_sale_dt datetime = null,
		@pi_fc_id int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   UPDATE [dbo].[foreclosure_case]
   SET [agency_id] = @pi_agency_id,
       [call_id] = @pi_call_id,
       [program_id] = @pi_program_id,
       [agency_case_num] = @pi_agency_case_num,
       [agency_client_num] = @pi_agency_client_num,
       [intake_dt] = @pi_intake_dt,
       [income_earners_cd] = @pi_income_earners_cd,
       [case_source_cd] = @pi_case_source_cd,  
       [race_cd] = @pi_race_cd ,  
       [household_cd] = @pi_household_cd  ,  
       [never_bill_reason_cd] = @pi_never_bill_reason_cd  ,  
       [never_pay_reason_cd] = @pi_never_pay_reason_cd  ,  
       [dflt_reason_1st_cd] = @pi_dflt_reason_1st_cd  ,  
       [dflt_reason_2nd_cd] = @pi_dflt_reason_2nd_cd  ,  
       [hud_termination_reason_cd] = @pi_hud_termination_reason_cd  ,  
       [hud_termination_dt] = @pi_hud_termination_dt   ,
       [hud_outcome_cd] = @pi_hud_outcome_cd  ,  
       [AMI_percentage] = @pi_AMI_percentage   ,  
       [counseling_duration_cd] = @pi_counseling_duration_cd  ,  
       [gender_cd] = @pi_gender_cd  ,  
       [borrower_fname] = @pi_borrower_fname  ,  
       [borrower_lname] = @pi_borrower_lname  ,  
       [borrower_mname] = @pi_borrower_mname  ,  
       [mother_maiden_lname] = @pi_mother_maiden_lname  ,  
       [borrower_ssn] = @pi_borrower_ssn  ,  
       [borrower_last4_SSN] = @pi_borrower_last4_SSN  ,  
       [borrower_DOB] = @pi_borrower_DOB   ,
       [co_borrower_fname] = @pi_co_borrower_fname  ,  
       [co_borrower_lname] = @pi_co_borrower_lname  ,  
       [co_borrower_mname] = @pi_co_borrower_mname  ,  
       [co_borrower_ssn] = @pi_co_borrower_ssn  ,  
       [co_borrower_last4_SSN] = @pi_co_borrower_last4_SSN  ,  
       [co_borrower_DOB] = @pi_co_borrower_DOB   ,
       [primary_contact_no] = @pi_primary_contact_no  ,  
       [second_contact_no] = @pi_second_contact_no  ,  
       [email_1] = @pi_email_1  ,  
       [contact_zip_plus4] = @pi_contact_zip_plus4  ,  
       [email_2] = @pi_email_2  ,  
       [contact_addr1] = @pi_contact_addr1  ,  
       [contact_addr2] = @pi_contact_addr2  ,  
       [contact_city] = @pi_contact_city  ,  
       [contact_state_cd] = @pi_contact_state_cd  ,  
       [contact_zip] = @pi_contact_zip  ,  
       [prop_addr1] = @pi_prop_addr1  ,  
       [prop_addr2] = @pi_prop_addr2  ,  
       [prop_city] = @pi_prop_city  ,  
       [prop_state_cd] = @pi_prop_state_cd  ,  
       [prop_zip] = @pi_prop_zip  ,  
       [prop_zip_plus_4] = @pi_prop_zip_plus_4  ,  
       [bankruptcy_ind] = @pi_bankruptcy_ind  ,  
       [bankruptcy_attorney] = @pi_bankruptcy_attorney  ,  
       [bankruptcy_pmt_current_ind] = @pi_bankruptcy_pmt_current_ind  ,  
       [borrower_educ_level_completed_cd] = @pi_borrower_educ_level_completed_cd  ,  
       [borrower_marital_status_cd] = @pi_borrower_marital_status_cd  ,  
       [borrower_preferred_lang_cd] = @pi_borrower_preferred_lang_cd  ,  
       [borrower_occupation] = @pi_borrower_occupation  ,  
       [co_borrower_occupation] = @pi_co_borrower_occupation  ,  
       [hispanic_ind] = @pi_hispanic_ind  ,  
       [duplicate_ind] = @pi_duplicate_ind  ,  
       [fc_notice_received_ind] = @pi_fc_notice_received_ind  ,  
       [case_complete_ind] = @pi_case_complete_ind  ,  
       [completed_dt] = @pi_completed_dt   ,
       [funding_consent_ind] = @pi_funding_consent_ind  ,  
       [servicer_consent_ind] = @pi_servicer_consent_ind  ,  
       [agency_media_consent_ind] = @pi_agency_media_consent_ind  ,  
       [hpf_media_candidate_ind] = @pi_hpf_media_candidate_ind  ,  
       [hpf_network_candidate_ind] = @pi_hpf_network_candidate_ind  ,  
       [hpf_success_story_ind] = @pi_hpf_success_story_ind  ,  
       [agency_success_story_ind] = @pi_agency_success_story_ind  ,  
       [borrower_disabled_ind] = @pi_borrower_disabled_ind  ,  
       [co_borrower_disabled_ind] = @pi_co_borrower_disabled_ind  ,  
       [summary_sent_other_cd] = @pi_summary_sent_other_cd  ,  
       [summary_sent_other_dt] = @pi_summary_sent_other_dt   ,
       [summary_sent_dt] = @pi_summary_sent_dt   ,
       [occupant_num] = @pi_occupant_num ,  
       [loan_dflt_reason_notes] = @pi_loan_dflt_reason_notes  ,  
       [action_items_notes] = @pi_action_items_notes  ,  
       [followup_notes] = @pi_followup_notes  ,  
       [prim_res_est_mkt_value] = @pi_prim_res_est_mkt_value   ,                
       [counselor_email] = @pi_counselor_email  ,  
       [counselor_phone] = @pi_counselor_phone  ,  
       [counselor_ext] = @pi_counselor_ext  ,  
       [discussed_solution_with_srvcr_ind] = @pi_discussed_solution_with_srvcr_ind  ,  
       [worked_with_another_agency_ind] = @pi_worked_with_another_agency_ind  ,  
       [contacted_srvcr_recently_ind] = @pi_contacted_srvcr_recently_ind  ,  
       [has_workout_plan_ind] = @pi_has_workout_plan_ind  ,  
       [srvcr_workout_plan_current_ind] = @pi_srvcr_workout_plan_current_ind  ,  
       [opt_out_newsletter_ind] = @pi_opt_out_newsletter_ind  ,  
       [opt_out_survey_ind] = @pi_opt_out_survey_ind  ,  
       [do_not_call_ind] = @pi_do_not_call_ind  ,  
       [owner_occupied_ind] = @pi_owner_occupied_ind  ,  
       [primary_residence_ind] = @pi_primary_residence_ind  ,  
       [realty_company] = @pi_realty_company  ,  
       [property_cd] = @pi_property_cd  ,  
       [for_sale_ind] = @pi_for_sale_ind  ,  
       [home_sale_price] = @pi_home_sale_price   ,  
       [home_purchase_year] = @pi_home_purchase_year   ,  
       [home_purchase_price] = @pi_home_purchase_price   ,  
       [home_current_market_value] = @pi_home_current_market_value   ,  
       [military_service_cd] = @pi_military_service_cd  ,  
       [household_gross_annual_income_amt] = @pi_household_gross_annual_income_amt   ,         
       [counselor_lname] = @pi_counselor_lname  ,  
       [counselor_fname] = @pi_counselor_fname  ,  
       [counselor_id_ref] = @pi_counselor_id_ref,   
       [intake_credit_score] = @pi_intake_credit_score,
	   [intake_credit_bureau_cd] = @pi_intake_credit_bureau_cd,
	   [fc_sale_dt] = @pi_fc_sale_dt,
	   [chg_lst_dt] = @pi_chg_lst_dt,
 	   [chg_lst_user_id] = @pi_chg_lst_user_id,
	   [chg_lst_app_name] = @pi_chg_lst_app_name	
 WHERE [fc_id] = @pi_fc_id 
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_agency_detail_get]    Script Date: 01/09/2009 16:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get case from FC_id
-- =============================================

CREATE PROCEDURE [dbo].[hpf_agency_detail_get]
	@pi_agency_id int = 0
As 
Begin
	Select 
		agency_id,
		agency_name,
		contact_fname,
		contact_lname,
		phone,
		fax,
		email,
		active_ind,
		hud_agency_num,
		hud_agency_sub_grantee_num,
		create_dt,
		create_user_id,
		create_app_name,
		chg_lst_dt,
		chg_lst_user_id,
		chg_lst_app_name
	From Agency
	Where agency_id = @pi_agency_id
End
GO
/****** Object:  StoredProcedure [dbo].[hpf_call_check_foreign_key]    Script Date: 01/09/2009 16:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thien Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	check valid foreign key
-- =============================================
CREATE PROCEDURE [dbo].[hpf_call_check_foreign_key]	
	@pi_call_center_id int = 0,
	--@pi_call_center varchar(4) = null,
	@pi_servicer_id int = 0,
	@pi_prev_agency_id int = 0,

	@po_call_center_id int OUTPUT,
	@po_servicer_id int OUTPUT,
	@po_prev_agency_id int OUTPUT
AS

Begin
	Select @po_call_center_id = count(call_center_id) from call_center where call_center_id = @pi_call_center_id
	Select @po_servicer_id = count (servicer_id)  from servicer where servicer_id = @pi_servicer_id
	Select @po_prev_agency_id = count(agency_id) from Agency where agency_id = @pi_prev_agency_id
End
GO
/****** Object:  StoredProcedure [dbo].[hpf_servicer_get]    Script Date: 01/09/2009 16:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 09 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Get Servicer list
-- =============================================
CREATE PROCEDURE [dbo].[hpf_servicer_get]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [servicer_id]      
      ,[servicer_name]
  FROM [dbo].[servicer]
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_view_budget_category_code]    Script Date: 01/09/2009 16:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_view_budget_category_code]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT bs.budget_subcategory_id		
		, bc.budget_category_cd
	FROM budget_subcategory bs
	   INNER JOIN budget_category bc 
         ON bs.budget_category_id = bc.budget_category_id
	WHERE bc.budget_category_cd IN ('1', '2')
END
GO
/****** Object:  StoredProcedure [dbo].[hpf_budget_subcategory_get]    Script Date: 01/09/2009 16:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khoa Do
-- Create date: 07 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Get Budget Subcategory list
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_subcategory_get]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [budget_subcategory_id]      
      ,[budget_subcategory_name]      
  FROM [dbo].[budget_subcategory]
END
GO
