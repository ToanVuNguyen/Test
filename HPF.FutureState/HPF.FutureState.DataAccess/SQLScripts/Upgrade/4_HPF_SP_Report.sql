-- =============================================
-- Create date: 23 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Create stored procedures, functions are being used in HPF Reports
-- =============================================
USE [hpf]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HPFStandard_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HPFStandard_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_ExternalReferrals]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Hpf_rpt_ExternalReferrals]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCounselingByServicer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CompletedCounselingByServicer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PotentialDuplicates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PotentialDuplicates]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_HSBC_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_HSBC_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_FIS_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_FIS_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_FIS_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_FIS_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyCompletionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_DailyCompletionReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Outcome]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Outcome]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCasesByState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CompletedCasesByState]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableSummary_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PayableSummary_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_IncompletedCounselingCases]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_IncompletedCounselingCases]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_PayableSummary_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_PayableSummary_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_CompletedCounselingSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Hpf_rpt_CompletedCounselingSummary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CaseFundingSourceSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CaseFundingSourceSummary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceExportFile_NFMC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceExportFile_NFMC]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CompletedCounselingDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CompletedCounselingDetail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_DailyAverageCompletionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_DailyAverageCompletionReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CaseSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CaseSource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Budget]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Budget]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_Budget_asset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_Budget_asset]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_CounselingSummary_get_FC_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hpf_rpt_UnbilledCaseReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Hpf_rpt_UnbilledCaseReport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_ AgencyPaymentCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_ AgencyPaymentCheck]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceSummary_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_detail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_InvoiceSummary_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_header]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_replace_non_numeric_char]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[hpf_rpt_replace_non_numeric_char]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_count_day]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_count_day]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_get_dateRange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_get_dateRange]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_rpt_AgencyPaymentCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_rpt_AgencyPaymentCheck]
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
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 13 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	hpf_rpt_CompletedCounselingDetail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_CompletedCounselingDetail] 
	(@pi_agency_id int, @pi_program_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
DECLARE @v_fc_id				INT
		, @v_outcome_item_id	INT 
		, @v_outcome_type_id	INT 
		, @v_outcome_dt			datetime
		, @v_outcome_deleted_dt datetime
		, @v_nonprofitreferral_key_num varchar(10)
		, @v_ext_ref_other_name varchar(50);

CREATE TABLE #counselingDetail
( fc_id  INT 
, agency_id  INT 
, call_id  INT 
, program_id  INT 
, agency_case_num varchar(30)
, agency_client_num varchar(30)
, intake_dt  datetime
, income_earners_cd varchar(15)
, case_source_cd varchar(15)
, race_cd varchar(15)
, household_cd varchar(15)
, never_bill_reason_cd varchar(15)
, never_pay_reason_cd varchar(15)
, dflt_reason_1st_cd varchar(15)
, dflt_reason_2nd_cd varchar(15)
, hud_termination_reason_cd varchar(15)
, hud_termination_dt  datetime
, hud_outcome_cd varchar(15)
, AMI_percentage  INT 
, counseling_duration_cd varchar(15)
, gender_cd varchar(15)
, borrower_fname varchar(30)
, borrower_lname varchar(30)
, borrower_mname varchar(30)
, mother_maiden_lname varchar(30)
, borrower_ssn varchar(255)
, borrower_last4_SSN varchar(4)
, borrower_DOB  datetime
, co_borrower_fname varchar(30)
, co_borrower_lname varchar(30)
, co_borrower_mname varchar(30)
, co_borrower_ssn varchar(255)
, co_borrower_last4_SSN varchar(4)
, co_borrower_DOB  datetime
, primary_contact_no varchar(20)
, second_contact_no varchar(20)
, email_1 varchar(50)
, contact_zip_plus4 varchar(4)
, email_2 varchar(50)
, contact_addr1 varchar(50)
, contact_addr2 varchar(50)
, contact_city varchar(30)
, contact_state_cd varchar(15)
, contact_zip varchar(5)
, prop_addr1 varchar(50)
, prop_addr2 varchar(50)
, prop_city varchar(30)
, prop_state_cd varchar(15)
, prop_zip varchar(5)
, prop_zip_plus_4 varchar(4)
, bankruptcy_ind varchar(1)
, bankruptcy_attorney varchar(50)
, bankruptcy_pmt_current_ind varchar(1)
, borrower_educ_level_completed_cd varchar(15)
, borrower_marital_status_cd varchar(15)
, borrower_preferred_lang_cd varchar(15)
, borrower_occupation varchar(50)
, co_borrower_occupation varchar(50)
, hispanic_ind varchar(1)
, duplicate_ind varchar(1)
, fc_notice_received_ind varchar(1)
, completed_dt  datetime
, funding_consent_ind varchar(1)
, servicer_consent_ind varchar(1)
, hpf_media_candidate_ind varchar(1)
, hpf_success_story_ind varchar(1)
, agency_success_story_ind varchar(1)
, borrower_disabled_ind varchar(1)
, co_borrower_disabled_ind varchar(1)
, summary_sent_other_cd varchar(15)
, summary_sent_other_dt  datetime
, summary_sent_dt  datetime
, occupant_num tinyint
, loan_dflt_reason_notes varchar(8000)
, action_items_notes varchar(8000)
, followup_notes varchar(8000)
, prim_res_est_mkt_value  numeric(15, 2)
, counselor_email varchar(50)
, counselor_phone varchar(20)
, counselor_ext varchar(20)
, discussed_solution_with_srvcr_ind varchar(1)
, worked_with_another_agency_ind varchar(1)
, contacted_srvcr_recently_ind varchar(1)
, has_workout_plan_ind varchar(1)
, srvcr_workout_plan_current_ind varchar(1)
, opt_out_newsletter_ind varchar(1)
, opt_out_survey_ind varchar(1)
, do_not_call_ind varchar(1)
, owner_occupied_ind varchar(1)
, primary_residence_ind varchar(1)
, realty_company varchar(50)
, property_cd varchar(15)
, for_sale_ind varchar(1)
, home_sale_price  numeric(15, 2)
, home_purchase_year  INT 
, home_purchase_price  numeric(15, 2)
, home_current_market_value  numeric(15, 2)
, military_service_cd varchar(15)
, household_gross_annual_income_amt  numeric(15, 2)
, loan_list varchar(500)
, counselor_fname varchar(30)
, counselor_lname varchar(30)
, intake_credit_score varchar(4)
, intake_credit_bureau_cd varchar(15)
, counselor_id_ref varchar(30)
, fc_sale_dt  datetime
, agency_media_interest_ind varchar(1)
--, ccrc_referral_seq numeric(5)
--, ccrc_sor_ind varchar(1)

, case_loan_id  INT 
, servicer_id  INT 
, other_servicer_name varchar(50)
, acct_num varchar(30)
, loan_1st_2nd_cd varchar(15)
, mortgage_type_cd varchar(15)
, arm_reset_ind varchar(1)
, term_length_cd varchar(15)
, loan_delinq_status_cd varchar(15)
, current_loan_balance_amt  numeric(15, 2)
, orig_loan_amt  numeric(15, 2)
, interest_rate numeric(5, 3)
, originating_lender_name varchar(50)
, orig_mortgage_co_FDIC_NCUA_num varchar(20)
, orig_mortgage_co_name varchar(50)
, orginal_loan_num varchar(30)
, investor_loan_num varchar(30)
, current_servicer_FDIC_NCUA_num varchar(30)
, investor_num varchar(30)
, investor_name varchar(50)
, changed_acct_num varchar(100)
, mortgage_program_cd varchar(15)
, freddie_servicer_num varchar(30)

, outcome_item_id1  INT 
, outcome_type_id1  INT 
, outcome_dt1  datetime
, outcome_deleted_dt1  datetime
, nonprofitreferral_key_num1 varchar(10)
, ext_ref_other_name1 varchar(50)

, outcome_item_id2  INT 
, outcome_type_id2  INT 
, outcome_dt2  datetime
, outcome_deleted_dt2  datetime
, nonprofitreferral_key_num2 varchar(10)
, ext_ref_other_name2 varchar(50)

, outcome_item_id3  INT 
, outcome_type_id3  INT 
, outcome_dt3  datetime
, outcome_deleted_dt3  datetime
, nonprofitreferral_key_num3 varchar(10)
, ext_ref_other_name3 varchar(50)
)
CREATE TABLE #selected_Foreclosure_case (fc_id INT);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));


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
			(fc_id,agency_id,call_id,program_id
			,agency_case_num,agency_client_num
			,intake_dt
			,income_earners_cd,case_source_cd,race_cd,household_cd
			,never_bill_reason_cd,never_pay_reason_cd
			,dflt_reason_1st_cd,dflt_reason_2nd_cd
			,hud_termination_reason_cd,hud_termination_dt,hud_outcome_cd
			,AMI_percentage
			,counseling_duration_cd,gender_cd
			,borrower_fname,borrower_lname,borrower_mname,mother_maiden_lname,borrower_ssn,borrower_last4_SSN,borrower_DOB
			,co_borrower_fname,co_borrower_lname,co_borrower_mname,co_borrower_ssn,co_borrower_last4_SSN,co_borrower_DOB
			,primary_contact_no,second_contact_no
			,email_1,contact_zip_plus4,email_2,contact_addr1,contact_addr2,contact_city,contact_state_cd,contact_zip
			,prop_addr1,prop_addr2,prop_city,prop_state_cd,prop_zip,prop_zip_plus_4
			,bankruptcy_ind,bankruptcy_attorney,bankruptcy_pmt_current_ind
			,borrower_educ_level_completed_cd
			,borrower_marital_status_cd
			,borrower_preferred_lang_cd
			,borrower_occupation
			,co_borrower_occupation
			,hispanic_ind,duplicate_ind,fc_notice_received_ind
			,completed_dt
			,funding_consent_ind,servicer_consent_ind
			,hpf_media_candidate_ind,hpf_success_story_ind,agency_success_story_ind
			,borrower_disabled_ind,co_borrower_disabled_ind
			,summary_sent_other_cd,summary_sent_other_dt,summary_sent_dt
			,occupant_num
			,loan_dflt_reason_notes,action_items_notes,followup_notes
			,prim_res_est_mkt_value
			,counselor_email,counselor_phone,counselor_ext
			,discussed_solution_with_srvcr_ind,worked_with_another_agency_ind,contacted_srvcr_recently_ind,has_workout_plan_ind,srvcr_workout_plan_current_ind
			,opt_out_newsletter_ind,opt_out_survey_ind,do_not_call_ind
			,owner_occupied_ind,primary_residence_ind,realty_company,property_cd,for_sale_ind
			,home_sale_price,home_purchase_year,home_purchase_price,home_current_market_value
			,military_service_cd
			,household_gross_annual_income_amt
			,loan_list
			,counselor_fname,counselor_lname
			,intake_credit_score,intake_credit_bureau_cd
			,counselor_id_ref
			,fc_sale_dt
			,agency_media_interest_ind

			,case_loan_id
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
			,orig_mortgage_co_FDIC_NCUA_num
			,orig_mortgage_co_name
			,orginal_loan_num
			,investor_loan_num
			,current_servicer_FDIC_NCUA_num
			,investor_num
			,investor_name
			,changed_acct_num
			,mortgage_program_cd
			,freddie_servicer_num
		)
		SELECT	f.fc_id,agency_id,call_id,program_id
				,agency_case_num,agency_client_num
				,intake_dt
				,income_earners_cd,case_source_cd,race_cd,household_cd
				,never_bill_reason_cd,never_pay_reason_cd
				,dflt_reason_1st_cd,dflt_reason_2nd_cd
				,hud_termination_reason_cd,hud_termination_dt,hud_outcome_cd
				,AMI_percentage
				,counseling_duration_cd,gender_cd
				,borrower_fname,borrower_lname,borrower_mname,mother_maiden_lname,borrower_ssn,borrower_last4_SSN,borrower_DOB
				,co_borrower_fname,co_borrower_lname,co_borrower_mname,co_borrower_ssn,co_borrower_last4_SSN,co_borrower_DOB
				,primary_contact_no	,second_contact_no
				,email_1,contact_zip_plus4,email_2,contact_addr1,contact_addr2,contact_city,contact_state_cd,contact_zip
				,prop_addr1,prop_addr2,prop_city,prop_state_cd,prop_zip	,prop_zip_plus_4
				,bankruptcy_ind	,bankruptcy_attorney,bankruptcy_pmt_current_ind
				,borrower_educ_level_completed_cd
				,borrower_marital_status_cd
				,borrower_preferred_lang_cd
				,borrower_occupation
				,co_borrower_occupation
				,hispanic_ind,duplicate_ind	,fc_notice_received_ind
				,completed_dt
				,funding_consent_ind,servicer_consent_ind
				,hpf_media_candidate_ind,hpf_success_story_ind
				,agency_success_story_ind,borrower_disabled_ind ,co_borrower_disabled_ind
				,summary_sent_other_cd,summary_sent_other_dt,summary_sent_dt
				,occupant_num
				,loan_dflt_reason_notes,action_items_notes ,followup_notes
				,prim_res_est_mkt_value
				,counselor_email,counselor_phone,counselor_ext
				,discussed_solution_with_srvcr_ind ,worked_with_another_agency_ind	,contacted_srvcr_recently_ind ,has_workout_plan_ind	,srvcr_workout_plan_current_ind
				,opt_out_newsletter_ind	,opt_out_survey_ind	,do_not_call_ind
				,owner_occupied_ind	,primary_residence_ind	,realty_company	,property_cd,for_sale_ind
				,home_sale_price ,home_purchase_year ,home_purchase_price ,home_current_market_value
				,military_service_cd
				,household_gross_annual_income_amt
				,loan_list
				,counselor_fname,counselor_lname
				,intake_credit_score ,intake_credit_bureau_cd
				,counselor_id_ref
				,fc_sale_dt
				,agency_media_interest_ind
				-- ,ccrc_referral_seq, ccrc_sor_ind

				,case_loan_id
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
				,orig_mortgage_co_FDIC_NCUA_num
				,orig_mortgage_co_name
				,orginal_loan_num
				,investor_loan_num
				,current_servicer_FDIC_NCUA_num
				,investor_num
				,investor_name
				,changed_acct_num
				,mortgage_program_cd
				,freddie_servicer_num
		FROM	foreclosure_case f, case_loan l
		WHERE	f.fc_id = l.fc_id AND l.loan_1st_2nd_cd = ''1ST''
				AND f.fc_id IN (SELECT fc_id FROM #selected_foreclosure_case);

	-- Get 4 lastest active outcome_items
	DECLARE @v_outcome_count INT, @v_next_fc_id INT;
	DECLARE v_cur CURSOR FOR 
		SELECT	fc_id, outcome_item_id, outcome_type_id, outcome_dt, outcome_deleted_dt, nonprofitreferral_key_num, ext_ref_other_name
		FROM	outcome_item
		WHERE	fc_id IN (SELECT fc_id FROM #selected_foreclosure_case)
				AND outcome_deleted_dt IS NULL
		ORDER BY fc_id ASC, outcome_item_id DESC;

	
	OPEN v_cur;
	FETCH v_cur INTO @v_fc_id, @v_outcome_item_id, @v_outcome_type_id, @v_outcome_dt, @v_outcome_deleted_dt, @v_nonprofitreferral_key_num, @v_ext_ref_other_name;
	SELECT @v_outcome_count = 1;
	SELECT @v_next_fc_id = @v_fc_id;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF	@v_next_fc_id <> @v_fc_id
		BEGIN
			SELECT @v_next_fc_id = @v_fc_id;
			SELECT @v_outcome_count = 1;
		END;

		IF @v_outcome_count = 1 		
			UPDATE	#counselingDetail
			SET		outcome_item_id1 = @v_outcome_item_id, outcome_type_id1 = @v_outcome_type_id
					, outcome_dt1 = @v_outcome_dt, outcome_deleted_dt1 = @v_outcome_deleted_dt
					, nonprofitreferral_key_num1 = @v_nonprofitreferral_key_num, ext_ref_other_name1 = @v_ext_ref_other_name
			WHERE	fc_id = @v_fc_id;

		IF @v_outcome_count = 2
			UPDATE	#counselingDetail
			SET		outcome_item_id2 = @v_outcome_item_id, outcome_type_id2 = @v_outcome_type_id
					, outcome_dt2 = @v_outcome_dt, outcome_deleted_dt2 = @v_outcome_deleted_dt
					, nonprofitreferral_key_num2 = @v_nonprofitreferral_key_num, ext_ref_other_name2 = @v_ext_ref_other_name
			WHERE	fc_id = @v_fc_id;


		IF @v_outcome_count = 3
			UPDATE	#counselingDetail
			SET		outcome_item_id3 = @v_outcome_item_id, outcome_type_id3 = @v_outcome_type_id
					, outcome_dt3 = @v_outcome_dt, outcome_deleted_dt3 = @v_outcome_deleted_dt
					, nonprofitreferral_key_num3 = @v_nonprofitreferral_key_num, ext_ref_other_name3 = @v_ext_ref_other_name
			WHERE	fc_id = @v_fc_id;

		SELECT @v_outcome_count = @v_outcome_count + 1;
		FETCH	v_cur INTO @v_fc_id, @v_outcome_item_id, @v_outcome_type_id, @v_outcome_dt, @v_outcome_deleted_dt, @v_nonprofitreferral_key_num, @v_ext_ref_other_name;
	END

	CLOSE v_cur;
	DEALLOCATE v_cur;
	SELECT * FROM #counselingDetail;
	DROP TABLE #counselingDetail;
	DROP TABLE #selected_foreclosure_case;
END;





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
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

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
				FROM outcome_item oi1, foreclosure_case f
				WHERE oi1.fc_id = f.fc_id 
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
			, l.interest_rate 
			, f.prop_city, f.prop_state_cd, f.prop_zip
			, f.occupant_num
			, f.completed_dt
			, rci2.code_desc case_source
			, rci3.code_desc loan_delinquency
			, p.program_name
			, a.agency_name
			, f.agency_case_num
			, f.counselor_lname, f.counselor_fname
			, rci4.code_desc counseling_duration 			
			, (isnull(b.total_income, 0) - isnull(b.total_expenses, 0)) as MTHLY_NET_INCOME
			, rci5.code_desc HUD_OUTCOME
			, f.hud_termination_dt
			, f.hud_termination_reason_cd
	FROM	foreclosure_case f INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.loan_1st_2nd_cd = ''1ST''
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''mortgage program code'') rci ON rci.code = l.mortgage_program_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''mortgage type code'') rci1 ON rci1.code = l.mortgage_type_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''case source code'') rci2 ON rci2.code = f.case_source_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''loan delinquency status code'') rci3 ON rci3.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''counseling duration code'') rci4 ON rci4.code = f.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''hud outcome code'') rci5 ON rci5.code = f.hud_outcome_cd
			LEFT OUTER JOIN (SELECT fc_id, total_income, total_expenses FROM budget_set 
			WHERE budget_set_id = (SELECT MAX(budget_set_id )from budget_set bs WHERE bs.fc_id IN (SELECT fc_id FROM invoice_case WHERE invoice_id = @pi_invoice_id) GROUP BY bs.fc_id))b ON b.fc_id = f.fc_id
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
			, f.income_earners_cd, f.case_source_cd, f.race_cd, f.household_cd
			, f.never_bill_reason_cd, f.never_pay_reason_cd
			, cd3.code_desc as dflt_reason_1st_cd, cd4.code_desc as dflt_reason_2nd_cd
			, f.hud_termination_reason_cd, convert(varchar(20), f.hud_termination_dt, 101) hud_termination_dt, f.hud_outcome_cd
			, f.AMI_percentage
			, cd1.code_desc as counseling_duration_cd, f.gender_cd
			, f.borrower_fname, f.borrower_lname, f.borrower_mname, f.mother_maiden_lname
			, ''XXX-XX-'' + f.borrower_last4_SSN borrower_last4_SSN, convert(varchar(20), f.borrower_DOB, 101) borrower_DOB
			, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_mname
			, ''XXX-XX-'' + f.co_borrower_last4_SSN co_borrower_last4_SSN, convert(varchar(20), f.co_borrower_DOB, 101) co_borrower_DOB
			, f.primary_contact_no, f.second_contact_no
			, f.email_1, f.email_2
			, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip, f.contact_zip_plus4
			, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip, f.prop_zip_plus_4
			, case when f.bankruptcy_ind = ''Y'' then ''Yes'' when f.bankruptcy_ind = ''N'' then ''No'' ELSE NULL END bankruptcy_ind
			, f.bankruptcy_attorney
			, case when f.bankruptcy_pmt_current_ind = ''Y'' then ''Yes'' when f.bankruptcy_pmt_current_ind = ''N'' then ''No'' ELSE NULL END bankruptcy_pmt_current_ind
			, f.borrower_educ_level_completed_cd
			, f.borrower_marital_status_cd, f.borrower_preferred_lang_cd, f.borrower_occupation
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
			, f.summary_sent_other_cd, convert(varchar(20), f.summary_sent_other_dt, 101) as summary_sent_other_dt, convert(varchar(20), f.summary_sent_dt, 101) summary_sent_dt
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
			, f.realty_company, f.property_cd
			, case when f.for_sale_ind= ''Y'' then ''Yes'' when f.for_sale_ind= ''N'' then ''No'' ELSE NULL END for_sale_ind
			, ''$'' + convert(varchar(30), cast(f.home_sale_price as money),1) as home_sale_price
			, f.home_purchase_year
			, ''$'' + convert(varchar(30), cast(f.home_purchase_price as money),1) as home_purchase_price
			, ''$'' + convert(varchar(30), cast(f.home_current_market_value as money),1) as home_current_market_value
			, cd2.code_desc as military_service_cd
			, ''$'' + convert(varchar(30), cast(f.household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt
			, f.loan_list
			, f.intake_credit_score, f.intake_credit_bureau_cd			
			, isnull(l.other_servicer_name, s.servicer_name) as Servicer_name
			, l.acct_num
			, l.loan_1st_2nd_cd
			, l.mortgage_type_cd
			, case when l.arm_reset_ind= ''Y'' then ''Yes'' when l.arm_reset_ind= ''N'' then ''No'' ELSE NULL END arm_reset_ind
			, l.term_length_cd
			, l.loan_delinq_status_cd
			, ''$'' + convert(varchar(30), cast(l.current_loan_balance_amt as money),1) as current_loan_balance_amt
			, ''$'' + convert(varchar(30), cast(l.orig_loan_amt as money),1) as orig_loan_amt
			, l.interest_rate
			, l.originating_lender_name
			, l.orig_mortgage_co_FDIC_NCUA_num
			, l.orig_mortgage_co_name
			, l.orginal_loan_num
			, l.current_servicer_FDIC_NCUA_num			
			, l.mortgage_program_cd
			, ''$'' + convert(varchar(30), cast(bs.total_income as money),1) as total_income
			, ''$'' + convert(varchar(30), cast(bs.total_expenses as money),1) as total_expenses
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
			, case_loan l, servicer s, agency a			
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
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
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
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));
	IF @pi_agency_id > 0
	BEGIN
		IF @pi_showNeverBillCases = 0	
			SELECT	f.fc_id, a.agency_name, l.acct_num, s.servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id 	FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	AND ic.fc_id IS NULL 
				AND f.agency_id = @pi_agency_id
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND f.never_bill_reason_cd IS NULL 
			ORDER BY a.agency_name, f.fc_id, s.servicer_name, l.acct_num;
		IF @pi_showNeverBillCases = 1
			SELECT	f.fc_id, a.agency_name, l.acct_num, s.servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id 	FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	AND ic.fc_id IS NULL 
				AND f.agency_id = @pi_agency_id
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND f.never_bill_reason_cd IS NOT NULL 
			ORDER BY a.agency_name, f.fc_id, s.servicer_name, l.acct_num;
	END
	ELSE
	BEGIN
		IF @pi_showNeverBillCases = 0	
			SELECT	f.fc_id, a.agency_name, l.acct_num, s.servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id 	FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	AND ic.fc_id IS NULL 
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND f.never_bill_reason_cd IS NULL 
			ORDER BY a.agency_name, f.fc_id, s.servicer_name, l.acct_num;
		IF @pi_showNeverBillCases = 1
			SELECT	f.fc_id, a.agency_name, l.acct_num, s.servicer_name
					, f.borrower_lname, f.borrower_fname
					, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
					, f.never_bill_reason_cd,f.never_pay_reason_cd
			FROM	agency a, case_loan l, servicer s
					, foreclosure_case f LEFT OUTER JOIN 
					(SELECT ic2.fc_id 	FROM invoice_case ic2 INNER JOIN invoice i ON ic2.invoice_id = i.invoice_id AND i.status_cd = ''ACTIVE'')ic ON  f.fc_id = ic.fc_id
			WHERE	a.agency_id = f.agency_id AND f.fc_id = l.fc_id  AND l.servicer_id = s.servicer_id 
				AND l.loan_1st_2nd_cd = ''1ST''	AND ic.fc_id IS NULL 
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt
				AND f.never_bill_reason_cd IS NOT NULL 
			ORDER BY a.agency_name, f.fc_id, s.servicer_name, l.acct_num;
	END;
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
EXEC dbo.sp_executesql @statement = N'








-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 26 Feb 2009
-- Description:	HPF REPORT - R55 - Invoice Summary - Get Invoice detail
-- =============================================
CREATE PROCEDURE [dbo].[hpf_rpt_InvoiceSummary_detail] 
	(@pi_invoice_id integer)
AS
BEGIN
	SELECT	convert(varchar(11), i.period_start_dt, 101) as period_start_dt
			,convert(varchar(11), i.period_end_dt, 101) as period_end_dt
			, s.servicer_name
			, count(ic.fc_id) as sessions
			, p.program_name, ic.invoice_case_bill_amt as rate
	FROM	invoice i, invoice_case ic, foreclosure_case f, case_loan l
			, servicer s, program p, funding_source_group fsg
	WHERE	i.invoice_id = ic.invoice_id 
			AND ic.fc_id= f.fc_id
			AND f.fc_id = l.fc_id
			AND l.servicer_id = s.servicer_id
			AND	f.program_id = p.program_id
			AND s.servicer_id = fsg.servicer_id AND fsg.funding_source_id = i.funding_source_id
			AND i.invoice_id = @pi_invoice_id			
	GROUP BY	i.period_start_dt, i.period_end_dt, s.servicer_name
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
		, cast(ic.invoice_case_pmt_amt as numeric(18)) invoice_case_bill_amt
		, s.fis_servicer_num
		, l.acct_num as Loan_number
		, f.borrower_lname + '','' + f.borrower_fname as Borrower_Name
		, f.prop_addr1, f.prop_addr2, f.prop_city, geo.county_name
		, f.prop_state_cd, f.prop_zip, f.prop_zip_plus_4
		, convert(varchar(11),f.intake_dt, 101) intake_dt
		, f.fc_id		
	FROM	invoice_case ic, invoice i
			, foreclosure_case f, case_loan l, servicer s
			, geocode_ref as geo
	WHERE	ic.fc_id = f.fc_id AND i.invoice_id = ic.invoice_id AND f.fc_id = l.fc_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND ic.invoice_id = @pi_invoice_id
			AND geo.city_type = ''D'' AND geo.zip_code= f.prop_zip
	ORDER BY ic.invoice_case_id ASC;
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
	FROM	invoice i, funding_source fs
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
	FROM	agency_payable ap, agency a
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
			, isnull(apc.pmt_amt, 0) as Rate			
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
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

	IF @pi_servicer_id >0
		SELECT	f.fc_id, l.acct_num, s.servicer_name, a.agency_name
				, convert(varchar(20),f.completed_dt, 101) as completed_dt
				, convert(varchar(20),f.intake_dt, 101) as intake_dt
				, f.borrower_fname, f.borrower_lname
				, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, convert(varchar(20),f.summary_sent_dt, 101) as summary_sent_dt
				, f.loan_list, f.funding_consent_ind, f.servicer_consent_ind
		FROM	foreclosure_case f, case_loan l, servicer s, agency a
		WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
				AND l.loan_1st_2nd_cd = ''1st''
				AND (f.duplicate_ind IS NULL OR f.duplicate_ind = ''N'')
				AND f.never_bill_reason_cd IS NULL
				AND l.servicer_id = @pi_servicer_id
				AND f.completed_dt BETWEEN @pi_from_dt AND @pi_to_dt;
	IF @pi_servicer_id < 0
		SELECT	f.fc_id, l.acct_num, s.servicer_name, a.agency_name
				, convert(varchar(20),f.completed_dt, 101) as completed_dt
				, convert(varchar(20),f.intake_dt, 101) as intake_dt
				, f.borrower_fname, f.borrower_lname
				, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, convert(varchar(20),f.summary_sent_dt, 101) as summary_sent_dt
				, f.loan_list, f.funding_consent_ind, f.servicer_consent_ind		FROM	foreclosure_case f, case_loan l, servicer s, agency a
		WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
				AND l.loan_1st_2nd_cd = ''1st''
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
	(@pi_from_dt datetime, @pi_to_dt datetime)
AS
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

	/* Potential duplicates are records with
	1. Address1 (only the 1st 12 chars) AND zip
	2.Primary_contact no
	3.the 1st 7 chars of acct_num and Servicer name
	*/
	SELECT f.fc_id, a.agency_name, l.acct_num, s.servicer_name
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
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = ''1st''
			AND substring(f.prop_addr1, 1, 12) + '';'' + f.prop_zip IN
				( SELECT	substring(f1.prop_addr1, 1, 12)+ '';'' + f1.prop_zip FROM foreclosure_case f1 
				GROUP BY	substring(f1.prop_addr1, 1, 12), f1.prop_zip HAVING	count(*) > 1)
			AND f.create_dt BETWEEN @pi_from_dt AND @pi_to_dt
	UNION 
	SELECT f.fc_id, a.agency_name, l.acct_num, s.servicer_name
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
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = ''1st''
			AND f.primary_contact_no IN 
				(SELECT primary_contact_no FROM foreclosure_case 
				GROUP BY	primary_contact_no	HAVING	count(*) > 1)				
			AND f.create_dt BETWEEN @pi_from_dt AND @pi_to_dt
	UNION
	SELECT f.fc_id, a.agency_name, l.acct_num, s.servicer_name
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
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
			AND	f.agency_id = a.agency_id AND f.program_id = p.program_id
			AND l.loan_1st_2nd_cd = ''1st''
			AND substring(l.acct_num, 1, 7)  + '':'' + cast(l.servicer_id as varchar(50)) IN 
				(SELECT		substring(l1.acct_num, 1, 7)  + '';'' + cast(l1.servicer_id as varchar(50)) FROM case_loan l1
				GROUP BY	substring(l1.acct_num, 1, 7), l1.servicer_id HAVING count(*) > 1)				
			AND f.create_dt BETWEEN @pi_from_dt AND @pi_to_dt
	ORDER BY s.servicer_name, l.acct_num;
END;


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
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

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
			, opt_out_newsletter_ind, opt_out_survey_ind, do_not_call_ind, primary_residence_ind
			, bankruptcy_attorney, household_gross_annual_income_amt, a.loan_list
			, l.Loan_no, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.interest_rate
		FROM	foreclosure_case a, program b, agency aa
				,(Select fc_id, acct_num + ''-'' + s.servicer_name as Loan_no, loan_1st_2nd_cd, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, interest_rate
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
			, opt_out_newsletter_ind, opt_out_survey_ind, do_not_call_ind, primary_residence_ind
			, household_gross_annual_income_amt, a.loan_list
			, l.Loan_no, l.loan_1st_2nd_cd, l.mortgage_type_cd, l.term_length_cd, l.loan_delinq_status_cd, l.interest_rate
		FROM	foreclosure_case a, program b, agency aa
			, (Select fc_id, acct_num + ''-'' + s.servicer_name as Loan_no, loan_1st_2nd_cd, mortgage_type_cd, term_length_cd, loan_delinq_status_cd, interest_rate
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
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));
	IF @pi_agency_id > 0 
		SELECT	f.Fc_id, a.Agency_name, f.Completed_dt, f.Agency_case_num, f.Intake_dt, l.Acct_num, s.Servicer_name
				, f.borrower_fname + '' '' + f.borrower_lname  as borrower_name
				, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, f.summary_sent_dt, f.counselor_id_ref, f.counselor_fname + '' '' + f.counselor_lname as counselor_name
		FROM	foreclosure_case f LEFT OUTER JOIN agency_payable_case apc ON f.fc_id = apc.fc_id
				, agency a, servicer s, case_loan l
		WHERE	f.fc_id = l.fc_id AND f.agency_id = a.agency_id AND l.servicer_id = s.servicer_id AND apc.fc_id IS NULL AND f.never_pay_reason_cd IS NULL 
				AND l.loan_1st_2nd_cd = ''1ST'' AND	f.completed_dt BETWEEN  @pi_from_dt AND @pi_to_dt 
				AND f.agency_id =@pi_agency_id 
		ORDER BY 	a.Agency_name, f.fc_id ;
	ELSE
		SELECT	f.Fc_id, a.Agency_name, f.Completed_dt, f.Agency_case_num, f.Intake_dt, l.Acct_num, s.Servicer_name
				, f.borrower_fname + '' '' + f.borrower_lname  as borrower_name
				, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, f.summary_sent_dt, f.counselor_id_ref, f.counselor_fname + '' '' + f.counselor_lname as counselor_name
		FROM	foreclosure_case f LEFT OUTER JOIN agency_payable_case apc ON f.fc_id = apc.fc_id
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
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + ''-'' + cast(@pi_to_dt as varchar(20));

    -- Get all agencies
	IF (@pi_agency_id = -1)
		SELECT	a.agency_name as Agency, l.acct_num as Loan, s.servicer_name as Servicer
				, f.prop_addr1 as Property_address, prop_city as City, prop_state_cd as State, prop_zip as Zip
				, f.counselor_lname as Counselor_last_name, f.counselor_fname as Counselor_first_name
		FROM	agency a, foreclosure_case f, case_loan l, servicer s
		WHERE	f.agency_id = a.agency_id AND f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id
				AND f.completed_dt IS NOT NULL
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
				AND f.completed_dt IS NOT NULL
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
	SELECT	ic.invoice_case_id, convert(varchar(11), f.intake_dt, 101) intake_dt, cast(ic.invoice_case_pmt_amt as numeric(18)) invoice_case_pmt_amt
	FROM	invoice_case ic, foreclosure_case f
	WHERE	ic.fc_id = f.fc_id 
			AND ic.invoice_id = @pi_invoice_id
	ORDER BY ic.invoice_case_id ASC;
END 


' 
END
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go






-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Hpf_rpt_AgencyPaymentCheck	
-- =============================================
ALTER PROCEDURE [dbo].[hpf_rpt_AgencyPaymentCheck] 
	(@pi_agency_id int, @pi_from_dt datetime, @pi_to_dt datetime)
AS
DECLARE @sql varchar(8000);
BEGIN
	SELECT @pi_from_dt = cast(cast(@pi_from_dt as varchar(11)) as datetime);
	SELECT @pi_to_dt = dateadd(second,-1, cast(cast(@pi_to_dt as varchar(11)) as datetime) + 1);
--	Print cast(@pi_from_dt as varchar(20)) + '-' + cast(@pi_to_dt as varchar(20));
	IF @pi_agency_id > 0 
		SELECT	f.Fc_id, a.Agency_name, f.Completed_dt, f.Agency_case_num, f.Intake_dt, l.Acct_num, s.Servicer_name
				, f.borrower_fname + ' ' + f.borrower_lname  as borrower_name
				, f.prop_addr2, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, f.summary_sent_dt, f.counselor_id_ref, f.counselor_fname + ' ' + f.counselor_lname as counselor_name
		FROM	foreclosure_case f LEFT OUTER JOIN agency_payable_case apc ON f.fc_id = apc.fc_id
				, agency a, servicer s, case_loan l
		WHERE	f.fc_id = l.fc_id AND f.agency_id = a.agency_id AND l.servicer_id = s.servicer_id AND apc.fc_id IS NULL AND f.never_pay_reason_cd IS NULL 
				AND l.loan_1st_2nd_cd = '1ST' AND	f.completed_dt BETWEEN  @pi_from_dt AND @pi_to_dt 
				AND f.agency_id =@pi_agency_id 
		ORDER BY 	a.Agency_name, f.fc_id ;
	ELSE
		SELECT	f.Fc_id, a.Agency_name, f.Completed_dt, f.Agency_case_num, f.Intake_dt, l.Acct_num, s.Servicer_name
				, f.borrower_fname + ' ' + f.borrower_lname  as borrower_name
				, f.prop_addr2, f.prop_addr1, f.prop_city, f.prop_state_cd, f.prop_zip
				, f.summary_sent_dt, f.counselor_id_ref, f.counselor_fname + ' ' + f.counselor_lname as counselor_name
		FROM	foreclosure_case f LEFT OUTER JOIN agency_payable_case apc ON f.fc_id = apc.fc_id
				, agency a, servicer s, case_loan l
		WHERE	f.fc_id = l.fc_id AND f.agency_id = a.agency_id AND l.servicer_id = s.servicer_id AND apc.fc_id IS NULL AND f.never_pay_reason_cd IS NULL 
				AND l.loan_1st_2nd_cd = '1ST' AND	f.completed_dt BETWEEN  @pi_from_dt AND @pi_to_dt 
		ORDER BY 	a.Agency_name, f.fc_id ;
END;


select property_cd from foreclosure_case where fc_id = 206
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go












-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	HPF REPORT - Counseling Summary - Get FC detail
-- =============================================
ALTER PROCEDURE [dbo].[hpf_rpt_CounselingSummary_get_FC_detail] 
	(@pi_fc_id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT	f.fc_id, a.agency_name, f.call_id, f.program_id
			, f.agency_case_num, f.agency_client_num
			, convert(varchar(20), f.intake_dt, 101) intake_dt
			, f.income_earners_cd, f.case_source_cd, f.race_cd, f.household_cd
			, f.never_bill_reason_cd, f.never_pay_reason_cd
			, cd3.code_desc as dflt_reason_1st_cd, cd4.code_desc as dflt_reason_2nd_cd
			, f.hud_termination_reason_cd, convert(varchar(20), f.hud_termination_dt, 101) hud_termination_dt, f.hud_outcome_cd
			, f.AMI_percentage
			, cd1.code_desc as counseling_duration_cd, f.gender_cd
			, f.borrower_fname, f.borrower_lname, f.borrower_mname, f.mother_maiden_lname
			, 'XXX-XX-' + f.borrower_last4_SSN borrower_last4_SSN, convert(varchar(20), f.borrower_DOB, 101) borrower_DOB
			, f.co_borrower_fname, f.co_borrower_lname, f.co_borrower_mname
			, 'XXX-XX-' + f.co_borrower_last4_SSN co_borrower_last4_SSN, convert(varchar(20), f.co_borrower_DOB, 101) co_borrower_DOB
			, f.primary_contact_no, f.second_contact_no
			, f.email_1, f.email_2
			, f.contact_addr1, f.contact_addr2, f.contact_city, f.contact_state_cd, f.contact_zip, f.contact_zip_plus4
			, f.prop_addr1, f.prop_addr2, f.prop_city, f.prop_state_cd, f.prop_zip, f.prop_zip_plus_4
			, case when f.bankruptcy_ind = 'Y' then 'Yes' when f.bankruptcy_ind = 'N' then 'No' ELSE NULL END bankruptcy_ind
			, f.bankruptcy_attorney
			, case when f.bankruptcy_pmt_current_ind = 'Y' then 'Yes' when f.bankruptcy_pmt_current_ind = 'N' then 'No' ELSE NULL END bankruptcy_pmt_current_ind
			, f.borrower_educ_level_completed_cd
			, f.borrower_marital_status_cd, f.borrower_preferred_lang_cd, f.borrower_occupation
			, f.co_borrower_occupation
			, case when f.hispanic_ind= 'Y' then 'Yes' when f.hispanic_ind= 'N' then 'No' ELSE NULL END hispanic_ind
			, case when f.duplicate_ind= 'Y' then 'Yes' when f.duplicate_ind= 'N' then 'No' ELSE NULL END duplicate_ind
			, case when f.fc_notice_received_ind= 'Y' then 'Yes' when f.fc_notice_received_ind= 'N' then 'No' ELSE NULL END fc_notice_received_ind
			, convert(varchar(20), f.fc_sale_dt, 101) fc_sale_dt
			, convert(varchar(20), f.completed_dt, 101)  completed_dt
			, case when f.funding_consent_ind= 'Y' then 'Yes' when f.funding_consent_ind= 'N' then 'No' ELSE NULL END funding_consent_ind
			, case when f.servicer_consent_ind= 'Y' then 'Yes' when f.servicer_consent_ind= 'N' then 'No' ELSE NULL END servicer_consent_ind
			, case when f.agency_media_interest_ind= 'Y' then 'Yes' when f.agency_media_interest_ind= 'N' then 'No' ELSE NULL END agency_media_interest_ind
			, case when f.hpf_media_candidate_ind= 'Y' then 'Yes' when f.hpf_media_candidate_ind= 'N' then 'No' ELSE NULL END hpf_media_candidate_ind
			, case when f.hpf_success_story_ind= 'Y' then 'Yes' when f.hpf_success_story_ind= 'N' then 'No' ELSE NULL END hpf_success_story_ind
			, case when f.agency_success_story_ind= 'Y' then 'Yes' when f.agency_success_story_ind= 'N' then 'No' ELSE NULL END agency_success_story_ind
			, case when f.borrower_disabled_ind= 'Y' then 'Yes' when f.borrower_disabled_ind= 'N' then 'No' ELSE NULL END borrower_disabled_ind
			, case when f.co_borrower_disabled_ind= 'Y' then 'Yes' when f.co_borrower_disabled_ind= 'N' then 'No' ELSE NULL END co_borrower_disabled_ind
			, f.summary_sent_other_cd, convert(varchar(20), f.summary_sent_other_dt, 101) as summary_sent_other_dt, convert(varchar(20), f.summary_sent_dt, 101) summary_sent_dt
			, f.occupant_num
			, f.loan_dflt_reason_notes, f.action_items_notes, f.followup_notes
			, '$' + convert(varchar(30), cast(f.prim_res_est_mkt_value as money),1) as prim_res_est_mkt_value
			, f.counselor_id_ref, f.counselor_fname, f.counselor_lname, f.counselor_email, f.counselor_phone, f.counselor_ext
			, case when f.discussed_solution_with_srvcr_ind= 'Y' then 'Yes' when f.discussed_solution_with_srvcr_ind= 'N' then 'No' ELSE NULL END discussed_solution_with_srvcr_ind
			, case when f.worked_with_another_agency_ind= 'Y' then 'Yes' when f.worked_with_another_agency_ind= 'N' then 'No' ELSE NULL END worked_with_another_agency_ind
			, case when f.contacted_srvcr_recently_ind= 'Y' then 'Yes' when f.contacted_srvcr_recently_ind= 'N' then 'No' ELSE NULL END contacted_srvcr_recently_ind
			, case when f.has_workout_plan_ind= 'Y' then 'Yes' when f.has_workout_plan_ind= 'N' then 'No' ELSE NULL END has_workout_plan_ind
			, case when f.srvcr_workout_plan_current_ind= 'Y' then 'Yes' when f.srvcr_workout_plan_current_ind= 'N' then 'No' ELSE NULL END srvcr_workout_plan_current_ind
			, case when f.opt_out_newsletter_ind= 'Y' then 'Yes' when f.opt_out_newsletter_ind= 'N' then 'No' ELSE NULL END opt_out_newsletter_ind
			, case when f.opt_out_survey_ind= 'Y' then 'Yes' when f.opt_out_survey_ind= 'N' then 'No' ELSE NULL END opt_out_survey_ind
			, case when f.do_not_call_ind= 'Y' then 'Yes' when f.do_not_call_ind= 'N' then 'No' ELSE NULL END do_not_call_ind
			, case when f.owner_occupied_ind= 'Y' then 'Yes' when f.owner_occupied_ind= 'N' then 'No' ELSE NULL END owner_occupied_ind
			, case when f.primary_residence_ind= 'Y' then 'Yes' when f.primary_residence_ind= 'N' then 'No' ELSE NULL END primary_residence_ind
			, f.realty_company, cd7.code_desc as property_cd
			, case when f.for_sale_ind= 'Y' then 'Yes' when f.for_sale_ind= 'N' then 'No' ELSE NULL END for_sale_ind
			, '$' + convert(varchar(30), cast(f.home_sale_price as money),1) as home_sale_price
			, f.home_purchase_year
			, '$' + convert(varchar(30), cast(f.home_purchase_price as money),1) as home_purchase_price
			, '$' + convert(varchar(30), cast(f.home_current_market_value as money),1) as home_current_market_value
			, cd2.code_desc as military_service_cd
			, '$' + convert(varchar(30), cast(f.household_gross_annual_income_amt as money),1) as household_gross_annual_income_amt
			, f.loan_list
			, f.intake_credit_score, f.intake_credit_bureau_cd			
			, isnull(l.other_servicer_name, s.servicer_name) as Servicer_name
			, l.acct_num
			, cd6.code_desc as loan_1st_2nd_cd
			, l.mortgage_type_cd
			, case when l.arm_reset_ind= 'Y' then 'Yes' when l.arm_reset_ind= 'N' then 'No' ELSE NULL END arm_reset_ind
			, l.term_length_cd
			, cd5.code_desc as loan_delinq_status_cd
			, '$' + convert(varchar(30), cast(l.current_loan_balance_amt as money),1) as current_loan_balance_amt
			, '$' + convert(varchar(30), cast(l.orig_loan_amt as money),1) as orig_loan_amt
			, l.interest_rate
			, l.originating_lender_name
			, l.orig_mortgage_co_FDIC_NCUA_num
			, l.orig_mortgage_co_name
			, l.orginal_loan_num
			, l.current_servicer_FDIC_NCUA_num			
			, l.mortgage_program_cd
			, '$' + convert(varchar(30), cast(bs.total_income as money),1) as total_income
			, '$' + convert(varchar(30), cast(bs.total_expenses as money),1) as total_expenses
	FROM	foreclosure_case f LEFT OUTER JOIN 
			(SELECT budget_set_id, fc_id, total_income,total_expenses, total_assets FROM budget_set 
			WHERE budget_set_id = (SELECT max(budget_set_id) FROM budget_set WHERE fc_id = @pi_fc_id)
			)bs ON f.fc_id = bs.fc_id
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name ='counseling duration code'	) cd1 ON cd1.code = f.counseling_duration_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = 'military service code') cd2 ON cd2.code = f.military_service_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = 'default reason code') cd3 ON cd3.code = dflt_reason_1st_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = 'default reason code') cd4 ON cd4.code = dflt_reason_2nd_cd			
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = 'property code') cd7 ON cd7.code = f.property_cd			
			, case_loan l
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = 'loan delinquency status code') cd5 ON cd5.code = l.loan_delinq_status_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE ref_code_set_name = 'loan 1st 2nd code') cd6 ON cd6.code = l.loan_1st_2nd_cd
			, servicer s, agency a			
	WHERE	f.fc_id = l.fc_id AND l.servicer_id = s.servicer_id AND f.agency_id = a.agency_id
			AND f.fc_id = @pi_fc_id  AND l.loan_1st_2nd_cd = '1ST';
END;

GO