-- =============================================
-- Create date: 30 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 30 Dec 2008. 
--				Refer to file "DB Track changes.xls"
-- =============================================


USE HPF
GO
ALTER TABLE case_loan DROP COLUMN arm_loan_ind;

DELETE FROM ref_code_item WHERE ref_code_set_name = 'Loan 1st 2nd';

INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Loan 1st 2nd','1st','1st Mortgage','',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Loan 1st 2nd','2nd','2nd Mortgage','',2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'SCONS', 'Servicer Consent Change', '',1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'FCONS', 'Funding Consent Change', '', 2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'SERV', 'Servicer Change', '', 3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'LOAN', 'Loan Number Change', '', 4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', '1ST2ND', 'Loan 1st 2nd Change', '', 5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'NBILL', 'Never Bill Reason Change', '', 6,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'NPAY', 'Never Pay Reason Change', '', 7,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'AGENCY', 'Agency Change', '', 8,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'DUPE', 'Duplicate Indicator Change', '', 9,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'EMAIL', 'User Requested Case Summary Email', '', 10,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'SUMM', 'Case Summary Sent', '', 11,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Activity code', 'CAP', 'Client Action Plan Sent', '', 12,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');

INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Export format code', 'FIS', 'FIS Export', '', 1,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Export format code', 'ICLEAR', 'iClear Export', '', 2,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Export format code', 'NFMC', 'NFMC Export', '', 3,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Export format code', 'STDSERV', 'HPF Standard Export', '', 4,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
INSERT INTO ref_code_item (ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name, chg_lst_dt, chg_lst_user_id,chg_lst_app_name) 
VALUES('Export format code', 'HSBC', 'HSBC  Export', '', 5,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');
  -- Store Procesure/Function
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_search_ws]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
	Set @v_select_clause =	''Select @po_total_rows = count(foreclosure_case.fc_id) 
							From foreclosure_case, Agency, case_loan, servicer ''
	Set @v_parameter_list = ''@pi_agency_case_num varchar(30) = null ,
							@pi_borrower_fname varchar(30) = null,
							@pi_borrower_lname varchar(30) = null,
							@pi_borrower_last4_SSN varchar(9) = null,
							@pi_prop_zip varchar(5) = null,
							@pi_loan_number varchar(30) = null,
							@pi_number_of_rows int,
							@po_total_rows int output''
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
	Set @v_select_clause = ''
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
					foreclosure_case, Agency, case_loan, servicer ''

	 Set @v_orderby_clause = '' ORDER BY foreclosure_case.[borrower_fname] asc
							, foreclosure_case.[borrower_lname] asc
							, foreclosure_case.[co_borrower_fname] asc
							, foreclosure_case.[co_borrower_lname] asc
							, foreclosure_case.[fc_id] desc''
	Set @v_full_string = @v_select_clause + @pi_where_clause + @v_orderby_clause
	
	Set @v_parameter_list = ''@pi_agency_case_num varchar(30) = null ,
							@pi_borrower_fname varchar(30) = null,
							@pi_borrower_lname varchar(30) = null,
							@pi_borrower_last4_SSN varchar(9) = null,
							@pi_prop_zip varchar(5) = null,
							@pi_loan_number varchar(30) = null,
							@pi_number_of_rows int''
													
	
	
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_agency_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
	INSERT INTO #agency VALUES(-1, ''ALL'');
	INSERT INTO #agency 
	SELECT a.agency_id, a.agency_name
	FROM Agency a
	ORDER BY a.agency_name;
	
	SELECT agency_id, agency_name FROM #agency;	 
	DROP TABLE #Agency
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_program_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Thao Nguyen
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_program_get]
AS
CREATE TABLE #program([program_id] int, [program_name] varchar(50));
BEGIN

	INSERT INTO #program VALUES(-1, ''ALL'');
	INSERT INTO #program 
	SELECT p.program_id, p.[program_name]
	FROM program p
	ORDER BY p.[program_name];
	
	SELECT program_id, [program_name] FROM #program;	 
	DROP TABLE #program

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_state_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Thao Nguyen
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_state_get]
AS
CREATE TABLE #foreclosure_case (prop_state_cd varchar(15));
BEGIN
INSERT INTO #foreclosure_case VALUES(''ALL'');
	INSERT INTO #foreclosure_case 
	SELECT distinct (prop_state_cd)
	FROM foreclosure_case f
	ORDER BY f.prop_state_cd;
	
	SELECT prop_state_cd FROM #foreclosure_case;	 
	DROP TABLE #foreclosure_case	

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_search_app_dynamic]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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

SET @cmdstring=''with foreclosure_hpf as(
	SELECT rownum=ROW_NUMBER() OVER (ORDER BY borrower_lname desc,co_borrower_lname desc,borrower_fname asc,
	 co_borrower_fname asc,f.fc_id desc ), f.fc_id, f.agency_id,
	 completed_dt, intake_dt, borrower_fname, borrower_lname, borrower_last4_SSN, co_borrower_fname,
	 co_borrower_lname, co_borrower_last4_SSN, prop_addr1, prop_city, prop_state_cd, prop_zip, a.agency_name, 
	 counselor_email, counselor_phone,counselor_fname,counselor_lname, 
     counselor_ext, loan_list, bankruptcy_ind, fc_notice_received_ind, l.loan_delinq_status_cd 
     
	FROM Agency a INNER JOIN foreclosure_case f ON a.agency_id = f.agency_id INNER JOIN program p 
     ON f.program_id = p.program_id INNER JOIN case_loan l ON f.fc_id = l.fc_id 
	WHERE (1=1''+@whereclause+''))
	
	SELECT *
FROM foreclosure_hpf 
WHERE rownum BETWEEN @pi_pagesize*(@pi_pagenum-1)+1 AND (@pi_pagesize*@pi_pagenum)

SELECT @po_totalrownum =COUNT(*) 
FROM Agency a INNER JOIN foreclosure_case f ON a.agency_id = f.agency_id INNER JOIN program p 
     ON f.program_id = p.program_id INNER JOIN case_loan l ON f.fc_id = l.fc_id 
	WHERE (1=1''+@whereclause+'')''


	
	SET @parameterlist=''
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
	@po_totalrownum int output''

	
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
	
	INSERT INTO #t_Daily (Agency_id, Agency_name, Day1)
		SELECT	a.agency_id, a.agency_name,  count(f1.fc_id) as Number
		FROM	foreclosure_case f1 RIGHT OUTER JOIN agency a ON a.agency_id = f1.agency_id
					AND f1.completed_dt = cast((''01'' + @v_Mon_year) as datetime)
		GROUP BY a.agency_name, a.agency_id, f1.completed_dt;

	UPDATE	#t_Daily
	SET		#t_Daily.Day2 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''02'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day3 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''03'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day4 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''04'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day5 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''05'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day6 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''06'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day7 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''07'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day8 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''08'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day9 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''09'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day10 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''10'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day11 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''11'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day12 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''12'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day13 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''13'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day14 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''14'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day15 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''15'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day16 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''16'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day17 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''17'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day18 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''18'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day19 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''19'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day20 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''20'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day21 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''21'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day22 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''22'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day23 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''23'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day24 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''24'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day25 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''25'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day26 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''26'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day27 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''27'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	UPDATE	#t_Daily
	SET		#t_Daily.Day28 = t.Number
	FROM	#t_Daily INNER JOIN 
			(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
			WHERE	f1.completed_dt = cast((''28'' + @v_Mon_year) as datetime)
			GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;

	IF (@pi_Mon IN (''Jan'', ''Mar'', ''Apr'', ''May'', ''Jun'', ''Jul'', ''Aug'', ''Sep'', ''Oct'', ''Nov'', ''Dec'')
		OR (@pi_Mon = ''Feb'' AND @pi_year%4 = 0))
		UPDATE	#t_Daily
		SET		#t_Daily.Day29 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	f1.completed_dt = cast((''29'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;
	IF (@pi_Mon <> ''Feb'')
		UPDATE	#t_Daily
		SET		#t_Daily.Day30 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	f1.completed_dt = cast((''30'' + @v_Mon_year) as datetime)
				GROUP BY f1.agency_id) t ON #t_Daily.agency_id = t.agency_id;
	IF (@pi_Mon NOT IN (''Feb'', ''Apr'', ''Jun'', ''Sep'', ''Nov''))
		UPDATE	#t_Daily
		SET		#t_Daily.Day31 = t.Number
		FROM	#t_Daily INNER JOIN 
				(SELECT	f1.agency_id, count(f1.fc_id) as Number	FROM	foreclosure_case f1 
				WHERE	f1.completed_dt = cast((''31'' + @v_Mon_year) as datetime)
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
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_count_day]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_wildcard_search_string]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'Create Function [dbo].[hpf_wildcard_search_string](@str nvarchar(100))
Returns nvarchar(102)
as
Begin	
	return ''%'' + @str + ''%''
End' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_check_over_one_year]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
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
' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_search_app1]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Thao Nguyen 
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Seacrh case (WEB APP)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_search_app1]
(	@pi_last4SSN varchar(4)=null ,
	@pi_fname varchar(30)=null,
	@pi_lname varchar(30)=null,
	@pi_fc_id int =-1,
	@pi_agencycaseid varchar(30)=null ,
	@pi_loannum varchar(50)=null,
	@pi_propzip varchar(5)=null,
	@pi_propstate varchar(15)=null,
	@pi_duplicate char(1)=null,
	@pi_agencyid int=-1,
	@pi_programid int=-1 
)
as
DECLARE @SQL varchar(8000);
BEGIN	
	SELECT @SQL = 
		''SELECT  f.fc_id, f.agency_id, completed_dt, intake_dt, borrower_fname, borrower_lname, borrower_last4_SSN, co_borrower_fname, co_borrower_lname, co_borrower_last4_SSN, prop_addr1, prop_city, prop_state_cd, prop_zip, a.agency_name, counselor_fname+ '''','''' +counselor_lname as counselor_fullname, counselor_email, counselor_phone, counselor_ext, loan_list, bankruptcy_ind, fc_notice_received_ind, l.loan_delinq_status_cd FROM Agency a INNER JOIN foreclosure_case f ON a.agency_id = f.agency_id INNER JOIN program p ON f.program_id = p.program_id INNER JOIN case_loan l ON f.fc_id = l.fc_id WHERE  1=1 '';	
	IF (@pi_last4SSN is not null) 
		SELECT @SQL = @SQL + '' AND ( borrower_last4_SSN='''''' + @pi_last4SSN +  '''''' or co_borrower_last4_SSN='''''' + @pi_last4SSN + '''''') '';
	IF (@pi_fname is not null)
		SELECT @SQL = @SQL + '' and ( borrower_fname like '''''' + @pi_fname + '''''' or co_borrower_fname like '''''' + @pi_fname + '''''' )'';
	IF (@pi_lname is not null)			
		SELECT @SQL = @SQL + '' and ( borrower_lname like '''''' + @pi_lname + '''''' or co_borrower_lname like '''''' + @pi_lname + '''''' )'';
	IF (@pi_fc_id <>-1)			
		SELECT @SQL = @SQL + '' and f.fc_id= '' + cast (@pi_fc_id as varchar(20));
	IF (@pi_agencyid <>-1)			
		SELECT @SQL = @SQL + '' and f.agency_id='' + cast(@pi_agencyid as varchar(20)) ;
	IF (@pi_loannum is not null)			
		SELECT @SQL = @SQL + '' and l.acct_num =''''''+ @pi_loannum  + '''''''';
	IF (@pi_propzip is not null)			
		SELECT @SQL = @SQL + '' and f.prop_zip='''''' + @pi_propzip + '''''''';
	IF (@pi_propstate is not null)			
		SELECT @SQL = @SQL + '' and f.prop_state_cd='''''' + @pi_propstate + '''''''';
	IF (@pi_programid <>-1)			
		SELECT @SQL = @SQL + '' and f.program_id=''+ cast (@pi_programid as varchar(20)) ;
	IF (@pi_duplicate is not null)			 
		SELECT @SQL = @SQL + '' and f.duplicate_ind ='''''' + @pi_duplicate + '''''''';
	IF (@pi_agencycaseid is not null)			
		SELECT @SQL = @SQL + '' and f.agency_case_num ='''''' + @pi_agencycaseid + '''''''';
	SELECT @SQL = @SQL + '' ORDER BY borrower_lname desc,co_borrower_lname desc,borrower_fname asc,co_borrower_fname asc,f.fc_id desc;'';
	
	Execute (@SQL);
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
					(Select code as state from ref_code_item where ref_code_set_name = ''State'') a ON a.state = f1.prop_state_cd
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_ref_code_item_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_ws_user_get_from_username_password]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_geo_code_ref_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
	@pi_create_user_id varchar(15) = null,
	@pi_create_app_name varchar(20) = null,
	@pi_chg_lst_dt datetime = null,
	@pi_chg_lst_user_id varchar(15) = null,
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_activity_log_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_activity_log_insert]
	-- Add the parameters for the stored procedure here
		@pi_fc_id int = null
		,@pi_activity_cd varchar(15) = null
		,@pi_activity_dt datetime = null
		,@pi_activity_note varchar(2000) = null
		,@pi_create_dt datetime = null
		,@pi_create_user_id varchar(15) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(15) = null
		,@pi_chg_lst_app_name varchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [hpf].[dbo].[activity_log]
           ([fc_id]
           ,[activity_cd]
           ,[activity_dt]
           ,[activity_note]
           ,[create_dt]
           ,[create_user_id]
           ,[create_app_name]
           ,[chg_lst_dt]
           ,[chg_lst_user_id]
           ,[chg_lst_app_name])
     VALUES
           (
			@pi_fc_id
			,@pi_activity_cd
			,@pi_activity_dt
			,@pi_activity_note
			,@pi_create_dt
			,@pi_create_user_id
			,@pi_create_app_name
			,@pi_chg_lst_dt
			,@pi_chg_lst_user_id
			,@pi_chg_lst_app_name
			)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_search_app]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Thao Nguyen 
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Seacrh case (WEB APP) pager with code
-- =============================================
CREATE PROCEDURE [dbo].[hpf_foreclosure_case_search_app]
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
@po_totalrownum int=0 output
)
AS
BEGIN	
	WITH foreclosure_hpf AS 
	(SELECT ''rownum''=ROW_NUMBER() OVER (ORDER BY borrower_lname desc,co_borrower_lname desc,borrower_fname asc,
	 co_borrower_fname asc,f.fc_id desc ), f.fc_id, f.agency_id,
	 completed_dt, intake_dt, borrower_fname, borrower_lname, borrower_last4_SSN, co_borrower_fname,
	 co_borrower_lname, co_borrower_last4_SSN, prop_addr1, prop_city, prop_state_cd, prop_zip, a.agency_name, 
	 counselor_email, counselor_phone, counselor_fname,counselor_lname , 
     counselor_ext, loan_list, bankruptcy_ind, fc_notice_received_ind, l.loan_delinq_status_cd 
     
	FROM dbo.foreclosure_case f INNER JOIN
                      dbo.case_loan l ON f.fc_id = l.fc_id INNER JOIN
                      dbo.program p ON f.program_id = p.program_id INNER JOIN
                      dbo.Agency a ON f.agency_id = a.agency_id
	WHERE ( 
	((@pi_last4SSN is null or borrower_last4_SSN = @pi_last4SSN) or (@pi_last4SSN is null  or co_borrower_last4_SSN= @pi_last4SSN))
 	AND((@pi_fname is null or borrower_fname like @pi_fname)OR (@pi_fname is null or co_borrower_fname like @pi_fname))
	AND ((@pi_lname is null or borrower_lname like @pi_lname)OR (@pi_lname is null or co_borrower_lname like @pi_lname))
	AND (@pi_fc_id=-1 or f.fc_id= @pi_fc_id )
	AND (@pi_agencyid=-1 or f.agency_id= @pi_agencyid )
	AND ((@pi_loannum  is null)or(l.acct_num = @pi_loannum ))
	AND ((@pi_propzip is null) or (f.prop_zip=@pi_propzip))
	AND ((@pi_propstate is null)or(f.prop_state_cd=@pi_propstate))
	AND ((@pi_programid=-1) or (f.program_id=@pi_programid ))
	AND ((@pi_duplicate is null)or(f.duplicate_ind =@pi_duplicate))
	AND ((@pi_agencycaseid is null) or(f.agency_case_num = @pi_agencycaseid ))
	
	)

)	
SELECT *
FROM foreclosure_hpf 
WHERE rownum BETWEEN @pi_pagesize*(@pi_pagenum-1)+1 AND @pi_pagesize*@pi_pagenum

SELECT @po_totalrownum=COUNT(*)
	 
	FROM Agency a INNER JOIN foreclosure_case f ON a.agency_id = f.agency_id INNER JOIN program p 
     ON f.program_id = p.program_id INNER JOIN case_loan l ON f.fc_id = l.fc_id 
	WHERE ( 	
	((@pi_last4SSN is null or borrower_last4_SSN = @pi_last4SSN) or (@pi_last4SSN is null  or co_borrower_last4_SSN= @pi_last4SSN))
 	AND((@pi_fname is null or borrower_fname like @pi_fname)OR (@pi_fname is null or co_borrower_fname like @pi_fname))
	AND ((@pi_lname is null or borrower_lname like @pi_lname)OR (@pi_lname is null or co_borrower_lname like @pi_lname))
	AND (@pi_fc_id=-1 or f.fc_id= @pi_fc_id )
	AND (@pi_agencyid=-1 or f.agency_id= @pi_agencyid )
	AND ((@pi_loannum  is null)or(l.acct_num = @pi_loannum ))
	AND ((@pi_propzip is null) or (f.prop_zip=@pi_propzip))
	AND ((@pi_propstate is null)or(f.prop_state_cd=@pi_propstate))
	AND ((@pi_programid=-1) or (f.program_id=@pi_programid ))
	AND ((@pi_duplicate is null)or(f.duplicate_ind =@pi_duplicate))
	AND ((@pi_agencycaseid is null) or(f.agency_case_num = @pi_agencycaseid )))
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_get_duplicate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
					WHERE  	FC.Duplicate_Ind = ''N'' 
							AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate())) 
							AND	(FC.Agency_ID = @pi_agency_id)
							AND	(FC.Agency_Case_Num = @pi_agency_case_num)				
					) Table1
		WHERE  	FC.fc_id=CL.fc_id 
				AND FC.Agency_Id = A.Agency_Id
				AND CL.Servicer_ID = S.Servicer_ID
				AND FC.Duplicate_Ind= ''N'' 
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
					WHERE  	FC.Duplicate_Ind = ''N'' 
							AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate())) 				
							AND	(FC.fc_id = @pi_fc_id)
				) Table1
		WHERE  	FC.fc_id=CL.fc_id 
				AND FC.Agency_Id = A.Agency_Id
				AND CL.Servicer_ID = S.Servicer_ID
				AND FC.Duplicate_Ind= ''N'' 
				AND	(FC.Completed_dt is null OR FC.Completed_dt> DATEADD(year,-1, GetDate()))
				AND Table1.acct_num = CL.Acct_Num
				AND Table1.servicer_id = CL.Servicer_ID
				AND Table1.fc_id <> FC.FC_ID
	
		
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

    -- Report R3: incompleted counseling Cases
	-- Get all Agencies
	IF (@pi_agency_id =-1 )
		SELECT	a.fc_id, aa.agency_name, call_id, b.program_name, agency_case_num
			, convert(varchar, intake_dt, 101) as intake_dt
			, income_earners_cd, case_source_cd, race_cd, household_cd
			, dflt_reason_1st_cd, dflt_reason_2nd_cd, hud_outcome_cd
			, counseling_duration_cd, gender_cd
			, borrower_fname, borrower_lname, borrower_mname, borrower_DOB
			, primary_contact_no, contact_addr1, contact_city, contact_state_cd, contact_zip
			, prop_addr1, prop_city, prop_state_cd, prop_zip
			, bankruptcy_ind, owner_occupied_ind, hispanic_ind, fc_notice_received_ind
			, funding_consent_ind, servicer_consent_ind
			, occupant_num, loan_dflt_reason_notes, action_items_notes
			, counselor_id_ref, counselor_fname + '' '' + counselor_lname as counselor_full_name , counselor_email, counselor_phone, counselor_ext
			, discussed_solution_with_srvcr_ind, worked_with_another_agency_ind, contacted_srvcr_recently_ind, has_workout_plan_ind
			, fc_sale_date_set_ind
			, opt_out_newsletter_ind, opt_out_survey_ind, do_not_call_ind, primary_residence_ind
			, bankruptcy_attorney, household_gross_annual_income_amt, a.loan_list
			, l.Loan_no, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.arm_loan_ind, l.term_length_cd, l.loan_delinq_status_cd, l.interest_rate
		FROM	foreclosure_case a, program b, agency aa
				,(Select fc_id, acct_num + ''-'' + s.servicer_name as Loan_no, loan_1st_2nd_cd, mortgage_type_cd, arm_loan_ind, term_length_cd, loan_delinq_status_cd, interest_rate
				FROM case_loan cl INNER JOIN servicer s ON cl.servicer_id = s.servicer_id
				)l 
		WHERE a.program_id = b.program_id AND a.agency_id = aa.agency_id AND a.fc_id = l.fc_id
			AND a.intake_dt between @pi_from_dt and @pi_to_dt			
			AND a.completed_dt IS NULL
		ORDER BY aa.Agency_name, a.agency_case_num;
	ELSE
	-- Get specific agency
	IF (@pi_agency_id >0 )
		SELECT	a.fc_id, aa.agency_name, call_id, b.program_name, agency_case_num
			, convert(varchar, intake_dt, 101) as intake_dt
			, income_earners_cd, case_source_cd, race_cd, household_cd
			, dflt_reason_1st_cd, dflt_reason_2nd_cd, hud_outcome_cd
			, counseling_duration_cd, gender_cd
			, borrower_fname, borrower_lname, borrower_mname, borrower_DOB
			, primary_contact_no, contact_addr1, contact_city, contact_state_cd, contact_zip
			, prop_addr1, prop_city, prop_state_cd, prop_zip
			, bankruptcy_ind, owner_occupied_ind, hispanic_ind, fc_notice_received_ind
			, funding_consent_ind, servicer_consent_ind
			, occupant_num, loan_dflt_reason_notes, action_items_notes
			, counselor_id_ref, counselor_fname + '' '' + counselor_lname as counselor_full_name, counselor_email, counselor_phone, counselor_ext
			, discussed_solution_with_srvcr_ind, worked_with_another_agency_ind, contacted_srvcr_recently_ind, has_workout_plan_ind
			, fc_sale_date_set_ind
			, opt_out_newsletter_ind, opt_out_survey_ind, do_not_call_ind, primary_residence_ind
			, household_gross_annual_income_amt, a.loan_list
			, l.Loan_no, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.arm_loan_ind, l.term_length_cd, l.loan_delinq_status_cd, l.interest_rate
		FROM	foreclosure_case a, program b, agency aa
			, (Select fc_id, acct_num + ''-'' + s.servicer_name as Loan_no, loan_1st_2nd_cd, mortgage_type_cd, arm_loan_ind, term_length_cd, loan_delinq_status_cd, interest_rate
				FROM case_loan cl INNER JOIN servicer s ON cl.servicer_id = s.servicer_id
				)l
		WHERE a.program_id = b.program_id AND a.agency_id = aa.agency_id AND a.fc_id = l.fc_id
			AND a.agency_id = @pi_agency_id and a.intake_dt between @pi_from_dt and @pi_to_dt			
			AND a.completed_dt IS NULL
		ORDER BY aa.Agency_name, a.agency_case_num;
END

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
CREATE PROCEDURE [dbo].[Hpf_rpt_CompletedCounselingSummary] (@pi_from_dt datetime, @pi_to_dt datetime, @pi_Agency_id int)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- Get all agencies
	IF (@pi_agency_id = -1)
		SELECT	a.agency_name as Agency, l.acct_num as Loan, s.servicer_name as Servicer
				, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
				, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
		FROM	agency a, foreclosure_case f, case_loan l, servicer s
		WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
				AND f.case_complete_ind = ''Y''
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND l.loan_1st_2nd_cd = ''1st''
		ORDER BY a.agency_name, f.prop_state_cd, f.prop_city, f.prop_zip, f.loan_list;
	ELSE
    -- Get specific agency
		IF (@pi_agency_id > 0)
		SELECT	a.agency_name as Agency, l.acct_num as Loan, s.servicer_name as Servicer
				, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
				, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
		FROM	agency a, foreclosure_case f, case_loan l, servicer s
		WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
				AND f.case_complete_ind = ''Y''
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND l.loan_1st_2nd_cd = ''1st''
				AND f.agency_id = @pi_agency_id
		ORDER BY a.agency_name, f.prop_state_cd, f.prop_city, f.prop_zip, f.loan_list;		
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
           ,@pi_arm_loan_ind varchar(1) = null
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
           ,@pi_freddie_loan_num varchar(30) = null
           ,@pi_create_dt datetime = null
		   ,@pi_create_user_id  varchar(15) = null
		   ,@pi_create_app_name varchar(20) = null
		   ,@pi_chg_lst_dt datetime = null
		   ,@pi_chg_lst_user_id varchar(15) = null
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
           ,[arm_loan_ind]
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
           ,[freddie_loan_num]
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
           ,@pi_arm_loan_ind
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
           ,@pi_freddie_loan_num   
           ,@pi_create_dt
		   ,@pi_create_user_id
		   ,@pi_create_app_name
		   ,@pi_chg_lst_dt
	  	   ,@pi_chg_lst_user_id
		   ,@pi_chg_lst_app_name            
          )
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[hpf_case_loan_update]
	-- Add the parameters for the stored procedure here
	       @fc_id int
           ,@servicer_id int
           ,@other_servicer_name varchar(50)
           ,@acct_num varchar(30)
           ,@loan_1st_2nd_cd varchar(15)
           ,@mortgage_type_cd varchar(15)
           ,@arm_loan_ind varchar(1)
           ,@arm_reset_ind varchar(1)
           ,@term_length_cd varchar(15)
           ,@loan_delinq_status_cd varchar(15)
           ,@current_loan_balance_amt numeric(15,2)
           ,@orig_loan_amt numeric(15,2)
           ,@interest_rate numeric(5,3)
           ,@originating_lender_name varchar(50)
           ,@orig_mortgage_co_FDIC_NCUS_num varchar(20)
           ,@orig_mortgage_co_name varchar(50)           
           ,@FDIC_NCUS_num_current_servicer_TBD varchar(30)
           ,@current_servicer_name_TBD varchar(30)                      
           ,@freddie_loan_num varchar(30)
           ,@orginal_loan_num varchar(30)
           ,@case_loan_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[case_loan]
   SET [other_servicer_name] = @other_servicer_name      
      ,[loan_1st_2nd_cd] = @loan_1st_2nd_cd
      ,[mortgage_type_cd] = @mortgage_type_cd
      ,[arm_loan_ind] = @arm_loan_ind
      ,[arm_reset_ind] = @arm_reset_ind
      ,[term_length_cd] = @term_length_cd
      ,[loan_delinq_status_cd] = @loan_delinq_status_cd
      ,[current_loan_balance_amt] = @current_loan_balance_amt
      ,[orig_loan_amt] = @orig_loan_amt
      ,[interest_rate] = @interest_rate
      ,[originating_lender_name] = @originating_lender_name
      ,[orig_mortgage_co_FDIC_NCUS_num] = @orig_mortgage_co_FDIC_NCUS_num
      ,[orig_mortgage_co_name] = @orig_mortgage_co_name      
      ,[FDIC_NCUS_num_current_servicer_TBD] = @FDIC_NCUS_num_current_servicer_TBD
      ,[current_servicer_name_TBD] = @current_servicer_name_TBD            
      ,[freddie_loan_num] = @freddie_loan_num
      ,[orginal_loan_num] = @orginal_loan_num
 WHERE [servicer_id] = @servicer_id
       AND [acct_num] = @acct_num
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
			,arm_loan_ind
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
			,freddie_loan_num 
    FROM Case_Loan
    WHERE fc_id = @pi_fc_id      
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_set_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
		,@pi_create_user_id  varchar(15) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(15) = null
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_set_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_item_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_asset_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_item_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
		,@pi_create_user_id  varchar(15) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(15) = null
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
			,@pi_create_user_id  varchar(15) = null
			,@pi_create_app_name varchar(20) = null
			,@pi_chg_lst_dt datetime = null
			,@pi_chg_lst_user_id varchar(15) = null
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
			,outcome_set_id 
    FROM Outcome_Item 
    WHERE fc_id = @pi_fc_id
      AND outcome_deleted_dt IS null
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
		,@pi_chg_lst_user_id varchar(15) = null
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_asset_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
		,@pi_create_user_id  varchar(15) = null
		,@pi_create_app_name varchar(20) = null
		,@pi_chg_lst_dt datetime = null
		,@pi_chg_lst_user_id varchar(15) = null
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_get_from_agencyID_and_caseNumber]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

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
			,fc_sale_date_set_ind
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
	FROM	foreclosure_case 
	WHERE	agency_id = @pi_agency_id AND
			agency_case_num = @pi_agency_case_num
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_get_from_fcid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get case from FC_id
-- =============================================

CREATE PROCEDURE [dbo].[hpf_foreclosure_case_get_from_fcid]
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
			,fc_sale_date_set_ind
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
	FROM foreclosure_case WHERE fc_id = @pi_fc_id		
End



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_temp]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[hpf_temp]	
	@outvalue int output
As 
Begin
	Select @outvalue = count(foreclosure_case.fc_id) From foreclosure_case
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
		@pi_fc_sale_date_set_ind varchar(1) = null,
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
		@pi_counselor_id_ref varchar(15) = null,
		@pi_intake_credit_score varchar(4) = null, 
		@pi_intake_credit_bureau_cd varchar(15) = null, 
		@pi_chg_lst_dt datetime = null,
		@pi_chg_lst_user_id varchar(15) = null,
		@pi_chg_lst_app_name varchar(20) = null,
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
       [fc_sale_date_set_ind] = @pi_fc_sale_date_set_ind  ,  
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
	   [chg_lst_dt] = @pi_chg_lst_dt,
 	   [chg_lst_user_id] = @pi_chg_lst_user_id,
	   [chg_lst_app_name] = @pi_chg_lst_app_name
 WHERE [fc_id] = @pi_fc_id 
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
           @pi_fc_sale_date_set_ind varchar(1) = null,
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
           @pi_counselor_id_ref varchar(15) = null,
           @pi_intake_credit_score varchar(4) = null, 
		   @pi_intake_credit_bureau_cd varchar(15) = null, 
		   @pi_create_dt datetime = getdate,
		   @pi_create_user_id  varchar(15) = null,
		   @pi_create_app_name varchar(20) = null,
		   @pi_chg_lst_dt datetime = getdate ,
		   @pi_chg_lst_user_id varchar(15) = null,
		   @pi_chg_lst_app_name varchar(20) = null,
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
           ,[fc_sale_date_set_ind]
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
           @pi_fc_sale_date_set_ind,
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
		   @pi_create_dt,
		   @pi_create_user_id,
		   @pi_create_app_name,
		   @pi_chg_lst_dt,
		   @pi_chg_lst_user_id,
		   @pi_chg_lst_app_name
         )
SET @po_fc_id = SCOPE_IDENTITY()
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_get_from_agency_id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[hpf_agency_get_from_agency_id]
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
End	' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_servicer_get_from_servicer_id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[hpf_servicer_get_from_servicer_id]
	@pi_servicer_id int = 0
As 
Begin
	Select 
		servicer_id,
		servicer_name,
		contact_fname,
		contact_lname,
		contact_email,
		phone,
		fax,
		active_ind,
		funding_agreement_ind,
		secure_delivery_method_cd,
		couseling_sum_format_cd,
		hud_servicer_num,
		create_dt,
		create_user_id,
		create_app_name,
		chg_lst_dt,
		chg_lst_user_id,
		chg_lst_app_name
	From Servicer
	Where servicer_id = @pi_servicer_id
End	' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_view_budget_category_code]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
	WHERE bc.budget_category_cd IN (''1'', ''2'')
END
' 
END
