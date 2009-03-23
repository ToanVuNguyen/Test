-- =============================================
-- Create date: 20 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Create objects for HPF conversion
-- =============================================
USE HPF
GO

/**** DROP & CREATE TABLE ****************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[configurations]') AND type in (N'U'))
DROP TABLE [dbo].[configurations]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[command_log]') AND type in (N'U'))
DROP TABLE [dbo].[command_log]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[execution_log]') AND type in (N'U'))
DROP TABLE [dbo].[execution_log]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[statistic_log]') AND type in (N'U'))
DROP TABLE [dbo].[statistic_log]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[map_type_code]') AND type in (N'U'))
DROP TABLE [dbo].[map_type_code]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ccrc_servicer]') AND type in (N'U'))
DROP TABLE [dbo].[ccrc_servicer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ref_result_changelog]') AND type in (N'U'))
DROP TABLE [dbo].[ref_result_changelog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[referral_budget_changelog]') AND type in (N'U'))
DROP TABLE [dbo].[referral_budget_changelog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[referral_changelog]') AND type in (N'U'))
DROP TABLE [dbo].[referral_changelog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[referral_new]') AND type in (N'U'))
DROP TABLE [dbo].[referral_new]
GO
/***** CREATE TABLE *******************************************************/
--Create tables
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[command_log](
	[log_id] [int] NOT NULL,
	[command_id] [int] IDENTITY(1,1) NOT NULL,
	[key] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[value] [varchar](max) NULL,
	[log_time] [datetime] NULL CONSTRAINT [df_etl_commandlog_logtime]  DEFAULT (getdate())
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[configurations](
	[ConfigurationFilter] [nvarchar](255) NOT NULL,
	[ConfiguredValue] [nvarchar](255) NULL,
	[PackagePath] [nvarchar](255) NOT NULL,
	[ConfiguredValueType] [nvarchar](20) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[referral_changelog](
	[referral_seq_id] [numeric](8, 0) NOT NULL,
	[loan_id] [varchar](25) NULL,
	[program_seq_id] [numeric](8, 0) NULL,
	[servicer_seq_id] [numeric](8, 0) NULL,
	[agency_seq_id] [numeric](8, 0) NOT NULL,
	[agency_referral_id] [varchar](50) NULL,
	[last_name] [varchar](30) NULL,
	[first_name] [varchar](30) NULL,
	[mi_name] [varchar](1) NULL,
	[addr_line_1] [varchar](50) NULL,
	[city] [varchar](30) NULL,
	[state] [varchar](2) NULL,
	[zip] [varchar](5) NULL,
	[referral_date] [datetime] NULL,
	[create_prog_name] [varchar](30) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user_id] [varchar](8) NOT NULL,
	[chg_lst_prog_name] [varchar](30) NOT NULL,
	[chg_lst_date] [datetime] NOT NULL,
	[chg_lst_user_id] [varchar](8) NOT NULL,
	[summary_rpt_sent_date] [datetime] NULL,
	[phn] [varchar](15) NULL,
	[loan_default_rsn] [varchar](8000) NULL,
	[action_items] [varchar](8000) NULL,
	[followup_notes] [varchar](8000) NULL,
	[success_story_ind] [varchar](1) NULL,
	[first_contact_status_type_code] [varchar](5) NULL,
	[referral_source_type_code] [varchar](5) NULL,
	[contacted_servicer_ind] [varchar](1) NULL,
	[counseling_duration_type_code] [varchar](5) NULL,
	[first_mortgage_type_code] [varchar](5) NULL,
	[occupants] [numeric](2, 0) NULL,
	[mothers_maiden_name] [varchar](30) NULL,
	[privacy_consent_ind] [varchar](1) NULL,
	[secondary_contact_number] [varchar](30) NULL,
	[email_addr] [varchar](50) NULL,
	[bankruptcy_ind] [varchar](1) NULL,
	[bankruptcy_attorney_name] [varchar](50) NULL,
	[owner_occupied_ind] [varchar](1) NULL,
	[credit_score] [varchar](3) NULL,
	[primary_dflt_rsn_type_code] [varchar](5) NULL,
	[secondary_dflt_rsn_type_code] [varchar](5) NULL,
	[hispanic_ind] [varchar](1) NULL,
	[race_type_code] [varchar](5) NULL,
	[dupe_ind] [varchar](1) NULL,
	[age] [numeric](3, 0) NULL,
	[gender_type_code] [varchar](1) NULL,
	[household_type_code] [varchar](1) NULL,
	[household_income_amt] [numeric](15, 2) NULL,
	[intake_score_type_code] [varchar](1) NULL,
	[arm_reset_ind] [varchar](1) NULL,
	[property_for_sale_ind] [varchar](1) NULL,
	[list_price_amt] [numeric](15, 2) NULL,
	[realty_company_name] [varchar](50) NULL,
	[fc_notice_recd_ind] [varchar](1) NULL,
	[income_earners_type_code] [varchar](1) NULL,
	[summary_sent_other_type_code] [varchar](1) NULL,
	[summary_sent_other_date] [datetime] NULL,
	[discussed_solution_with_srvcr_ind] [varchar](1) NULL,
	[worked_with_another_agency_ind] [varchar](1) NULL,
	[second_servicer_seq_id] [numeric](8, 0) NULL,
	[second_mortgage_type_code] [varchar](5) NULL,
	[second_loan_id] [varchar](25) NULL,
	[second_loan_status_type_code] [varchar](5) NULL,
	[hud_outcome_type_code] [varchar](5) NULL,
	[hud_term_reason_type_code] [varchar](5) NULL,
	[hud_term_date] [datetime] NULL,
	[counselor_id_ref] [varchar](30) NOT NULL,
	[counselor_fname] [varchar](30) NOT NULL,
	[counselor_lname] [varchar](30) NOT NULL,
	[counselor_email] [varchar](50) NOT NULL,
	[counselor_phone] [varchar](20) NOT NULL,
	[case_source_cd] [varchar](15) NULL,
	[counseling_duration_cd] [varchar](15) NULL,
	[dflt_reason_1st_cd] [varchar](15) NULL,
	[dflt_reason_2nd_cd] [varchar](15) NULL,
	[race_cd] [varchar](15) NULL,
	[gender_cd] [varchar](15) NULL,
	[household_cd] [varchar](15) NULL,
	[intake_credit_bureau_cd] [varchar](15) NULL,
	[income_earners_cd] [varchar](15) NULL,
	[summary_sent_other_cd] [varchar](15) NULL,
	[hud_outcome_cd] [varchar](15) NULL,
	[hud_termination_reason_cd] [varchar](15) NULL,
	[loan_delinq_status_1st_cd] [varchar](15) NULL,
	[loan_delinq_status_2nd_cd] [varchar](15) NULL,
	[mortgage_type_1st_cd] [varchar](15) NULL,
	[mortgage_type_2nd_cd] [varchar](15) NULL,
	[referral_result_date] [datetime] NULL,
	[acct_prd] [varchar] (6) NULL,
	[mthly_net_income_type_code] [varchar] (5) NULL,
	[mthly_net_income] [numeric](15, 2) NULL,
	[mthly_expense_type_code] [varchar] (5) NULL,
	[mthly_expense] [numeric](15, 2) NULL,
	[piti_amt] [numeric](15, 2) NULL,
 CONSTRAINT [PK_referral_changelog] PRIMARY KEY CLUSTERED 
(
	[referral_seq_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[referral_new](
	[fc_id] [int],
	[ccrc_referral_seq] [numeric] (8, 0),
	[create_dt] [datetime],
	[create_user_id] [varchar] (30),
	[create_app_name] [varchar] (20),
	[chg_lst_dt] [datetime],
	[chg_lst_user_id] [varchar] (30),
	[chg_lst_app_name] [varchar] (20),
 CONSTRAINT [pk_referral_new] PRIMARY KEY CLUSTERED 
(
	[fc_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[execution_log](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_log_id] [int] NULL,
	[description] [varchar](50) NULL,
	[package_name] [varchar](50) NULL,
	[package_guid] [uniqueidentifier] NOT NULL,
	[machine_name] [varchar](50) NULL,
	[execution_guid] [uniqueidentifier] NOT NULL,
	[logical_date] [datetime] NOT NULL,
	[operator] [varchar](50) NULL,
	[start_time] [datetime] NOT NULL,
	[end_time] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[failure_task] [varchar](64) NULL,
 CONSTRAINT [pk_executionlog] PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[map_type_code](
	[type] [varchar](5) NOT NULL,
	[dscr] [varchar](80) NOT NULL,
	[ref_code_set_name] [varchar](30) NOT NULL,
	[code] [varchar](15) NULL,
 CONSTRAINT [pk_map_type_code] PRIMARY KEY CLUSTERED 
(
	[type] ASC,
	[ref_code_set_name] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ccrc_servicer](
	[ccrc_servicer_seq_id] [numeric](8, 0) NOT NULL,
	[servicer_id] [int] NOT NULL,
 CONSTRAINT [pk_ccrc_servicer] PRIMARY KEY CLUSTERED 
(
	[ccrc_servicer_seq_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[statistic_log](
	[log_id] [int] NOT NULL,
	[component_name] [varchar](50) NULL,
	[rows] [int] NULL,
	[timems] [int] NULL,
	[min_rows_per_sec] [int] NULL,
	[mean_rows_per_sec]  AS (case when isnull([timems],(0))=(0) then NULL else CONVERT([int],([rows]*(1000.0))/[timems],(0)) end) PERSISTED,
	[max_rows_per_sec] [int] NULL,
	[log_time] [datetime] NULL CONSTRAINT [df_etl_statisticlog_logtime]  DEFAULT (getdate())
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_result_changelog](
	[referral_seq_id] [numeric](8, 0) null,
	[referral_result_type_code] [varchar](50) null,
	[referral_result_date] [datetime] not null,
	[acct_prd] [varchar](6) null,
	[create_prog_name] [varchar](30) not null,
	[create_date] [datetime] not null,
	[create_user_id] [varchar](8) not null,
	[chg_lst_prog_name] [varchar](30) not null,
	[chg_lst_date] [datetime] not null,
	[chg_lst_user_id] [varchar](8) not null,
	[outcome_type_id] [int] null
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[referral_budget_changelog](
	[set_id] [int] identity (1,1),
	[referral_seq_id] [numeric](8, 0) not null,
	[budget_subcategory_seq_id] [numeric](8, 0) not null,
	[budget_category_seq_id] [numeric](8, 0) not null,
	[amt] [numeric](15, 2) not null,
	[budget_note] [varchar](100) null,
	[create_prog_name] [varchar](30) not null,
	[create_date] [datetime] not null,
	[create_user_id] [varchar](8) not null,
	[chg_lst_prog_name] [varchar](30) not null,
	[chg_lst_date] [datetime] not null,
	[chg_lst_user_id] [varchar](8) not null,
PRIMARY KEY CLUSTERED 
(
	[referral_seq_id] asc,
	[budget_subcategory_seq_id] asc,
	[budget_category_seq_id] asc
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/**** DROP AND CREATE PROCEDURE ****************************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_referral_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_referral_index]
GO

/****** Object:  StoredProcedure [dbo].[hpf_referral_index]    Script Date: 02/25/2009 18:20:00 ******/
create procedure [dbo].[hpf_referral_index]
	 @targetSchema		sysname = null	--null implies 'dbo'
	,@targetTable		sysname = null	--null implies default naming
	,@filegroup			sysname = null	--null implies default
	,@dropIndexes		bit				--0=Create, 1=Drop
	,@logID				int				--ETL Load ID
	,@debug				bit = 0			--Debug mode?
with execute as caller
as
/**********************************************************************************************************
* SP Name:		
*		hpf_referral_index
* Parameters:  
*		 @targetSchema		sysname = null	--null implies 'dbo'
*		,@targetTable		sysname = null	--null implies default naming
*		,@filegroup			sysname = null	--null implies default
*		,@dropIndexes		bit				--0=Create, 1=Drop
*		,@logID				int				--ETL Load ID
*		,@debug				bit = 0			--Debug mode?
*
* Purpose: This stored procedure creates/drops indexes. It either takes a @targetTable or a
*	@date in which case it derives the canonical @tablename.
*
* Example:
	exec hpf_referral_index
		 'dbo', 'referral_changelog', null
		, 1, 0, 1
*              
**********************************************************************************************************/
begin
	set nocount on
	declare @sql varchar(2000)

	--If @targetTable is null then use the canonical construct
	set @targetTable = isnull(@targetTable, 'referral_changelog')

	--Coerce null/empty @targetSchema to dbo. Then quote to mitigate injection.
	set @targetSchema = dbo.hpf_quotename(isnull(@targetSchema, 'dbo'))

	--Add quotes to non-null @filegroup to mitigate injection
	if @filegroup is not null set @filegroup = dbo.hpf_quotename(@filegroup)

	--First define the existence clause
	set @sql = 'select * from sys.indexes where name = ''PK_' + @targetTable + ''''

	--Drop/Create the INDEXes
	if @dropIndexes = 1 begin --Drop indexes
		--Drop any other indexes here first before dropping the clustered index...

		--Construct SQL statement
		set @sql = '
		if exists (' + @sql + ')
		alter table ' + @targetSchema + '.' + dbo.hpf_quotename(@targetTable) + '
		drop constraint ' + dbo.hpf_quotename('PK_' + @targetTable)
 			
		--Execute and log the dynamic SQL
		exec hpf_executesql 'Drop Index', @sql, @logID, @debug

	end else begin --If the user requested an index object...
		--Construct SQL statement
		set @sql = '
		if not exists (' + @sql + ')
		alter table ' + @targetSchema + '.' + dbo.hpf_quotename(@targetTable) + '
		add constraint ' + dbo.hpf_quotename('PK_' + @targetTable) + '
		primary key clustered (referral_seq_id)
		with (sort_in_tempdb = off, online = off)' + isnull(' on ' + @filegroup, '')
  			
		--Execute and log the dynamic SQL
		exec hpf_executesql 'Create Index', @sql, @logID, @debug

		--Create any other indexes here after creating the clustered index...
	end --if

	set nocount off
end --proc
GO
/***********************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_executesql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_executesql]
GO


/****** Object:  StoredProcedure [dbo].[hpf_executesql]    Script Date: 02/25/2009 18:16:37 ******/
create procedure [dbo].[hpf_executesql]
	 @key		varchar(50)
	,@sql		varchar(max)
	,@logID		int
	,@debug		bit = 0		--Debug mode?
with execute as caller
as
-- exec hpf_executesql 'Test', 'select', 0, 1
begin
	set nocount on

	if @debug = 1 begin
		print '--' + @key
		print (@sql)
	end else begin
		--Write the statement to the log first so we can monitor it (with nolock)
		exec hpf_event_package_oncommand
			 @Key	= @key
			,@Type	= 'SQL'
			,@Value	= @sql
			,@logID	= @logID
		--Execute the statement
		exec (@sql)
	end --if

	set nocount off
end --proc
GO

/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_etlupdate_state]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_etlupdate_state]
GO

/****** Object:  StoredProcedure [dbo].[hpf_etlupdate_state]    Script Date: 02/25/2009 18:15:02 ******/
create procedure [dbo].[hpf_etlupdate_state]
as
/**********************************************************************************************************
* SP Name:
*		[hpf_etlupdate_state]
* Parameters:
*  
* Purpose:	This stored procedure update referral STATE which are not correct.
*              
* Example:
		exec [hpf_etlupdate_state]
*              
**********************************************************************************************************/
begin
	set nocount on

	declare @referral_state table(referral_seq_id int, state varchar(2), zip varchar(5))

	insert into @referral_state
	select r.referral_seq_id, r.state, r.zip
	from referral_changelog r
		left join ref_code_item rc on r.state = rc.code and ref_code_set_name = 'state code' 
	where rc.code is null

	update @referral_state
	set state = case when rs.zip is not null then gc.state_abbr else 'AK' end
	from @referral_state rs
		left join geocode_ref gc on gc.city_type='D' 
			and gc.zip_code = case when rs.zip is not null then rs.zip else gc.zip_code end

	update referral_changelog
	set state = rs.state
	from @referral_state rs
		inner join referral_changelog r on r.referral_seq_id = rs.referral_seq_id

	set nocount off
end --proc
GO

/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_log_event_package_onbegin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_log_event_package_onbegin]
GO

/****** Object:  StoredProcedure [dbo].[hpf_log_event_package_onbegin]    Script Date: 02/25/2009 18:17:32 ******/
create procedure [dbo].[hpf_log_event_package_onbegin]
	 @ParentLogID		int
	,@Description		varchar(50) = null
	,@PackageName		varchar(50)
	,@PackageGuid		uniqueidentifier
	,@MachineName		varchar(50)
	,@ExecutionGuid		uniqueidentifier
	,@logicalDate		datetime
	,@operator			varchar(30)
	,@logID				int = null output
with execute as caller
as
/**********************************************************************************************************
* SP Name:
*		hpf_log_event_package_onbegin
* Parameters:
*		 @ParentLogID		int
*		,@Description		varchar(50) = null
*		,@PackageName		varchar(50)
*		,@PackageGuid		uniqueidentifier
*		,@MachineName		varchar(50)
*		,@ExecutionGuid		uniqueidentifier
*		,@logicalDate		datetime
*		,@operator			varchar(30)
*		,@logID				int = null output
*  
* Purpose:	This stored procedure logs a starting event to the custom event-log table
*              
* Example:
		declare @logID int
		exec hpf_log_event_package_onbegin 
			 0, 'Description'
			,'PackageName' ,'00000000-0000-0000-0000-000000000000'
			,'MachineName', '00000000-0000-0000-0000-000000000000'
			,'2004-01-01', null, @logID output
		select * from execution_log where LogID = @logID
*              
**********************************************************************************************************/
begin
	set nocount on

	--Coalesce @logicalDate
	set @logicalDate = isnull(@logicalDate, getdate())

	--Coalesce @operator
	set @operator = nullif(ltrim(rtrim(@operator)), '')
	set @operator = isnull(@operator, suser_sname())

	--Root-level nodes should have a null parent
	if @ParentLogID <= 0 set @ParentLogID = null

	--Root-level nodes should not have a null Description
	set @Description = nullif(ltrim(rtrim(@Description)), '')
	if @Description is null and @ParentLogID is null set @Description = @PackageName

	--Insert the log record
	insert into execution_log(
		 parent_log_id
		,description
		,package_name
		,package_guid
		,machine_name
		,execution_guid
		,logical_date
		,operator
		,start_time
		,end_time
		,status
		,failure_task
	) values (
		 @ParentLogID
		,@Description
		,@PackageName
		,@PackageGuid
		,@MachineName
		,@ExecutionGuid
		,@logicalDate
		,@operator
		,getdate() --Note: This should NOT be @logicalDate
		,null
		,0	--InProcess
		,null
	)
	set @logID = scope_identity()

	set nocount off
end --proc
GO

/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_event_package_oncommand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_event_package_oncommand]
GO

--Create SPs
/****** Object:  StoredProcedure [dbo].[hpf_event_package_oncommand]    Script Date: 02/25/2009 18:15:02 ******/
create procedure [dbo].[hpf_event_package_oncommand]
	 @Key				varchar(50)
	,@Type				varchar(50)
	,@Value				varchar(max)
	,@logID				int
with execute as caller
as
/**********************************************************************************************************
* SP Name:
*		hpf_event_package_oncommand
* Parameters:
*		 @Key		varchar(50)
*		,@Type		varchar(50)
*		,@Value		varchar(max)
*		,@logID		int
*  
* Purpose:	This stored procedure logs a command entry in the custom event-log table. A command is termed
*	as any large SQL or XMLA statement that the ETL performs. It is useful for debugging purposes to know
*	the exact text of the statement.
*              
* Example:
		exec hpf_event_package_oncommand 'Create Table', 'SQL', '...sql code...', 0
		select * from command_log where log_id = 0
*              
**********************************************************************************************************/
begin
	set nocount on

	--Insert the log record
	insert into command_log(
		 log_id
		,[key]
		,[type]
		,[value]
    ) values (
		 isnull(@logID, 0)
		,@Key
		,@Type
		,@Value
	)

	set nocount off
end --proc
GO

/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_log_event_package_oncount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_log_event_package_oncount]
GO

/****** Object:  StoredProcedure [dbo].[hpf_log_event_package_oncount]    Script Date: 02/25/2009 18:18:38 ******/
create procedure [dbo].[hpf_log_event_package_oncount]
	 @logID				int
	,@ComponentName		varchar(50)
	,@Rows				int
	,@TimeMS			int
	,@MinRowsPerSec		int = null
	,@MaxRowsPerSec		int = null
with execute as caller
as
/**********************************************************************************************************
* SP Name:
*		hpf_log_event_package_oncount
* Parameters:
*		 @logID				int
*		,@ComponentName		varchar(50)
*		,@Rows				int
*		,@TimeMS			int
*		,@MinRowsPerSec		int = null
*		,@MaxRowsPerSec		int = null
*  
* Purpose:	This stored procedure logs an entry to the custom RowCount-log table.
*              
* Example:
		exec hpf_log_event_package_oncount 0, 'Test', 100, 1000, 5, 50
		select * from statistic_log where log_id = 0
*              
**********************************************************************************************************/
begin
	set nocount on

	--Insert the record
	insert into statistic_log(
		log_id, component_name, rows, timems, min_rows_per_sec, max_rows_per_sec
	) values (
		isnull(@logID, 0), @ComponentName, @Rows, @TimeMS, @MinRowsPerSec, @MaxRowsPerSec
	)

	set nocount off
end --proc
GO
/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_log_event_package_onend]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_log_event_package_onend]
GO

/****** Object:  StoredProcedure [dbo].[hpf_log_event_package_onend]    Script Date: 02/25/2009 18:19:10 ******/
create procedure [dbo].[hpf_log_event_package_onend]
	 @logID				int
with execute as caller
as
/**********************************************************************************************************
* SP Name:
*		hpf_log_event_package_onend
* Parameters:
*		@logID			int
*  
* Purpose:	This stored procedure updates an existing entry in the custom event-log table. It flags the
*	execution run as complete.
*	Status = 0: Running (Incomplete)
*	Status = 1: Complete
*	Status = 2: Failed
*              
* Example:
		declare @logID int
		set @logID = 0
		exec hpf_log_event_package_onend @logID
		select * from execution_log where log_id = @logID
*              
**********************************************************************************************************/
begin
	set nocount on

	update execution_log set
		 end_time = getdate() --note: this should not be @logicaldate
		,status = case
			when status = 0 then 1	--complete
			else status
		end --case
	where 
		log_id = @logID

	set nocount off
end --proc
GO

/*************************************************************************************************/
/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_conversion_AMI_percentage]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[hpf_conversion_AMI_percentage]
GO

--Create FNs
/****** Object:  UserDefinedFunction [dbo].[hpf_quotename]    Script Date: 02/25/2009 18:12:44 ******/
create function [dbo].[hpf_conversion_AMI_percentage](
	@pi_household_gross_annual_income_amt numeric(15,2)
	, @pi_prop_zip varchar(5)
)
returns numeric(18)
with returns null on null input,
execute as caller
as
/**********************************************************************************************************
* UDF Name:		
*		[hpf_conversion_AMI_percentage]
* Parameters:  
*		 @pi_prop_zip varchar(5)
*
* Purpose: Calculate AMI_percentage
*
* Example:
	select dbo.hpf_conversion_AMI_percentage(150, '55416')
*              
**********************************************************************************************************/
begin
	DECLARE @v_AMI_percentage numeric(18), @v_AMI_value numeric(18);

	SELECT TOP 1 @v_AMI_value = a.median_income 
	FROM	geocode_ref g, area_median_income a 
	WHERE	g.county_fips = LEFT(a.fips,5) 
				AND g.msa_code = replace(a.msa,'9999','0000')
				AND g.city_type = 'D' 
				AND	g.zip_code = @pi_prop_zip
	ORDER BY a.median_income DESC;

	SELECT @v_AMI_percentage = cast((@pi_household_gross_annual_income_amt/@v_AMI_value)*100 as numeric(18));

	return @v_AMI_percentage
end --function
GO


/**********************************************************************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_quotename]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[hpf_quotename]
GO

--Create FNs
/****** Object:  UserDefinedFunction [dbo].[hpf_quotename]    Script Date: 02/25/2009 18:12:44 ******/
create function [dbo].[hpf_quotename](
	 @name		sysname
)
returns sysname
with returns null on null input,
execute as caller
as
/**********************************************************************************************************
* UDF Name:		
*		hpf_QuoteName
* Parameters:  
*		 @name		sysname
*
* Purpose: This function adds quotes to the value passed in, unless it is already quoted.
*	Ee have no need to worry about SQL Injection, this code will
*	be released as best-practice examples of how TSQL should be written and as such it
*	is important to include SQL injection-mitigation methods.
*
* Example:
	select hpf_QuoteName('abc')
*              
**********************************************************************************************************/
begin
	--If @name is not already quoted then quote it
	if @name not like '[[]%]' set @name = quotename(@name)

	--Return result
	return @name
end --function
GO



/*************************************************************************************************/
/*************************************************************************************************/
/* Main Store Procs */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_etlinsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_etlinsert]
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_etlinsert]    Script Date: 02/25/2009 18:22:50 ******/
CREATE procedure [dbo].[hpf_foreclosure_case_etlinsert]
	 @logicalDate	datetime
	,@operator		varchar(30)
	,@logID			int			--ETL Load ID
	,@debug			bit = 0		--Debug mode
with execute as caller as
/**********************************************************************************************************
* SP Name:		
*		hpf_foreclosure_case_etlinsert
* Parameters:  
*		,@logicalDate	datetime
*		,@operator		varchar(30)
*		,@logID			int
*		,@debug			bit = 0
*
* Purpose: This stored procedure performs a batch insert of the fact rows as part of the foreclosure_case 
*	UPSERT process. Instead of seperating inserts and updates, both are written to an intermediate table 
*	and then set-based operations are used to bulk-upsert into the destination.
*              
* Example:
	hpf_foreclosure_case_etlinsert '2004-12-18', null, 0, 1
*              
**********************************************************************************************************/
begin
--	set nocount on
	DECLARE @rowcount INT;

	if @logicalDate is not null begin --Ignore bad params
		--Coalesce @operator and mitigate injection
		set @operator = nullif(ltrim(rtrim(@operator)), '')
		set @operator = isnull(@operator, suser_sname())
		set @operator = quotename(@operator, '''')

		--Derive the SQL statement
		declare @sql varchar(8000), @i int

		set @sql = ''

		insert into foreclosure_case(
			ccrc_referral_seq,
			ccrc_sor_ind,
			program_id, 
			agency_id, 
			agency_case_num,
			borrower_lname, 
			borrower_fname, 
			borrower_mname,
			contact_addr1, 
			prop_addr1, 
			contact_city, 
			prop_city,
			contact_state_cd, 
			prop_state_cd, 
			contact_zip, 
			prop_zip,
			intake_dt, 
			counselor_id_ref,
			counselor_fname,
			counselor_lname,
			counselor_email,
			counselor_phone,
			create_app_name, 
			create_dt, 
			create_user_id,
			chg_lst_app_name, 
			chg_lst_dt, 
			chg_lst_user_id,
			summary_sent_dt, 
			primary_contact_no, 
			loan_dflt_reason_notes, 
			action_items_notes,
			followup_notes, 
			agency_success_story_ind,
			bankruptcy_pmt_current_ind, 
			has_workout_plan_ind, 
			srvcr_workout_plan_current_ind,
			case_source_cd, 
			contacted_srvcr_recently_ind, 
			counseling_duration_cd,
			occupant_num, 
			mother_maiden_lname, 
			borrower_DOB,
			servicer_consent_ind,
			second_contact_no, 
			email_1, 
			bankruptcy_ind, 
			bankruptcy_attorney,
			owner_occupied_ind, 
			intake_credit_score, 
			dflt_reason_1st_cd, 
			dflt_reason_2nd_cd, 
			hispanic_ind,
			race_cd, 
			household_cd,
			never_bill_reason_cd,
			never_pay_reason_cd,
			duplicate_ind,
			gender_cd, 
			borrower_marital_status_cd,
			household_gross_annual_income_amt, 
			intake_credit_bureau_cd, 
			for_sale_ind,
			home_sale_price, 
			realty_company, 
			fc_notice_received_ind, 
			completed_dt,
			income_earners_cd,
			summary_sent_other_cd, 
			summary_sent_other_dt, 
			discussed_solution_with_srvcr_ind,
			hud_outcome_cd, 
			hud_termination_reason_cd, 
			hud_termination_dt, 
			worked_with_another_agency_ind,
			property_cd,
			primary_residence_ind,
			funding_consent_ind, 
			opt_out_newsletter_ind, 
			opt_out_survey_ind,
			do_not_call_ind
		) 
		select
			new.referral_seq_id,
			'Y',
			new.program_seq_id, 
			new.agency_seq_id, 
			substring(isnull(new.agency_referral_id,'unknown at convert'),1,30),
			isnull(new.last_name,'unknown at convert'), 
			isnull(new.first_name,'unknown at convert'), 
			new.mi_name,
			isnull(new.addr_line_1, 'unknown at convert'), 
			isnull(new.addr_line_1, 'unknown at convert'), 
			isnull(new.city, 'unknown at convert'), 
			isnull(new.city, 'unknown at convert'),
			isnull(new.state, 'AK'), 
			isnull(new.state, 'AK'), 
			isnull(new.zip, '99999'), 
			isnull(new.zip, '99999'),
			isnull(new.referral_date, isnull(new.referral_result_date,new.create_date)), 
			new.counselor_id_ref,
			new.counselor_fname,
			new.counselor_lname,
			new.counselor_email,
			new.counselor_phone,
			new.create_prog_name, 
			new.create_date, 
			new.create_user_id,
			new.chg_lst_prog_name, 
			new.chg_lst_date, 
			new.chg_lst_user_id,
			new.summary_rpt_sent_date, 
			isnull(new.phn, 'unknown at convert'), 
			isnull(new.loan_default_rsn, 'unknown at convert'), 
			isnull(new.action_items, 'unknown at convert'),
			new.followup_notes, 
			new.success_story_ind,
			case when new.first_contact_status_type_code='07' or new.bankruptcy_ind='Y' then 'N' else 'Y' end,
			case when new.first_contact_status_type_code in ('08', '09') then 'Y' else 'N' end,
			case new.first_contact_status_type_code when '08' then 'Y' when '09' then 'N' end,
			isnull(new.case_source_cd,'OTHER'), 
			isnull(new.contacted_servicer_ind,'N'), 
			isnull(new.counseling_duration_cd,'<30'),
			isnull(new.occupants,2), 
			new.mothers_maiden_name, 
			'1/1/' + cast(year(new.create_date) - case when new.age is null or new.age <12 or new.age >110 then 12 else new.age end as varchar),
			case new.privacy_consent_ind when 'N' then 'N' else 'Y' end, 
			substring(new.secondary_contact_number,1,20), 
			new.email_addr, 
			case when rtrim(ltrim(new.first_contact_status_type_code)) in ('06', '07') then 'Y' else 'N' end,
			case when rtrim(ltrim(new.first_contact_status_type_code)) in ('06', '07') or ltrim(new.bankruptcy_ind)='Y' then 'unknown at convert' else new.bankruptcy_attorney_name end,
			isnull(new.owner_occupied_ind, 'Y'), 
			new.credit_score, 
			isnull(new.dflt_reason_1st_cd, '24'), 
			isnull(new.dflt_reason_2nd_cd, '25'), 
/* Begin 19 Mar - Quyen- Fix bug-263 */
			case when new.race_type_code='12' then 'Y' 
				 when (new.hispanic_ind IS NULL OR ltrim(new.hispanic_ind) = '')  then 'N'
				 ELSE new.hispanic_ind	END hispanic_ind,
/* End 19 Mar - Quyen- Fix bug-263 */
			isnull(new.race_cd,'11'), 
			isnull(new.household_cd,'7'),
			case when new.ACCT_PRD is not null and new.ACCT_PRD < 200903 then 'CCRCBILLED' else null end,
			case when new.ACCT_PRD is not null and new.ACCT_PRD < 200903 then 'CCRCPAID' else null end,
			new.dupe_ind,
			isnull(new.gender_cd,'DECLINED'), 
			case when new.household_type_code in ('4', '5') then 'M' else null end,
			case when new.mthly_net_income_type_code in ('1','2','3','4','5') then new.mthly_net_income*12/0.7 else 0 end, 
			new.intake_credit_bureau_cd, 
			isnull(new.property_for_sale_ind,'N'),
/* Begin 19 Mar - Quyen- Fix bug-260 */
			case when new.list_price_amt = 0 AND isnull(new.property_for_sale_ind, 'N') = 'N' then NULL
				 when (ltrim(new.list_price_amt) = '' OR new.list_price_amt IS NULL) AND new.property_for_sale_ind = 'Y' then 0
				 ELSE new.list_price_amt END list_price_amt, 
/* End 19 Mar - Quyen- Fix bug-260 */
			new.realty_company_name, 
			isnull(new.fc_notice_recd_ind,'N'), 
			new.referral_result_date,
			isnull(new.income_earners_cd,'UNK'),
			new.summary_sent_other_cd, 
			new.summary_sent_other_date, 
			isnull(new.discussed_solution_with_srvcr_ind,'N'),
			isnull(new.hud_outcome_cd, 17), 
			new.hud_termination_reason_cd, 
			new.hud_term_date, 
			case new.worked_with_another_agency_ind when 'Y' then 'Y' else 'N' end,
/* Begin 19 Mar - Quyen- Fix bug-261 */
			NULL,
/* End 19 Mar - Quyen- Fix bug-261 */
			'Y','Y','N','N','N'
		from 
			referral_changelog as new
			inner join agency a on a.agency_id = new.agency_seq_id
			inner join program p on p.program_id = new.program_seq_id
			left join foreclosure_case as old 
				on new.referral_seq_id = old.ccrc_referral_seq
		where --we are only interested in unmatched (new) rows
			old.ccrc_referral_seq is null
		order by --ordered insert so index build is more efficient
			 new.referral_seq_id

/* Begin 19 Mar - Quyen- Row count when insert/update + AMI percentage*/
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert into foreclosure_case Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL

		UPDATE	foreclosure_case
		SET		foreclosure_case.AMI_percentage = dbo.hpf_conversion_AMI_percentage(foreclosure_case.household_gross_annual_income_amt, foreclosure_case.prop_zip) 
		FROM	foreclosure_case INNER JOIN referral_changelog new 
					ON new.referral_seq_id = foreclosure_case.ccrc_referral_seq 
						AND foreclosure_case.ccrc_sor_ind = 'Y' 
						AND foreclosure_case.household_gross_annual_income_amt IS NOT NULL 
/* End 19 Mar - Quyen */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert into foreclosure_case Table', @sql, @logID, @debug


		select @i = max(fc_id) from referral_new
		truncate table  referral_new

		insert into referral_new
		select 
			[fc_id],
			[ccrc_referral_seq],
			[create_dt],
			[create_user_id],
			[create_app_name],
			[chg_lst_dt],
			[chg_lst_user_id],
			[chg_lst_app_name]
		from foreclosure_case
		where fc_id > isnull(@i,0)

	end --if

--	set nocount off
end --proc
GO


/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_etlupdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_etlupdate]
GO
/****** Object:  StoredProcedure [dbo].[hpf_foreclosure_case_etlupdate]    Script Date: 02/25/2009 18:23:37 ******/
CREATE  procedure [dbo].[hpf_foreclosure_case_etlupdate]
	 @logicalDate	datetime
	,@operator		varchar(30)
	,@logID			int			--ETL Load ID
	,@debug			bit = 0		--Debug mode
with execute as caller
as
/**********************************************************************************************************
* SP Name:		
*		hpf_foreclosure_case_etlupdate
* Parameters:  
*		,@logicalDate	datetime
*		,@operator		varchar(30)
*		,@logID			int
*		,@debug			bit = 0
*
* Purpose: This stored procedure performs a batch update of the fact rows as part of the foreclosure_case 
*	UPSERT process. Instead of seperating inserts and updates, both are written to an intermediate table 
*	and then set-based operations are used to bulk-upsert into the destination.
*              
* Example:
	hpf_foreclosure_case_etlupdate '2004-12-18', null, 0, 1
*              
**********************************************************************************************************/
begin
--	set nocount on 
	DECLARE @rowcount INT;

	if @logicalDate is not null begin --Ignore bad params
		--Coalesce @operator and mitigate injection
		set @operator = nullif(ltrim(rtrim(@operator)), '')
		set @operator = isnull(@operator, suser_sname())
		set @operator = quotename(@operator, '''')
		
		--Derive the SQL statement
		declare @sql varchar(8000)
		set @sql = ''

		update foreclosure_case set
		  agency_id = new.agency_seq_id
		  ,call_id = null
		  ,program_id = new.program_seq_id
		  ,agency_case_num = substring(isnull(new.agency_referral_id,'unknown at convert'),1,30)
		  ,intake_dt = isnull(new.referral_date, isnull(new.referral_result_date,new.create_date))
		  ,income_earners_cd = isnull(new.income_earners_cd,'UNK')
		  ,case_source_cd = isnull(new.case_source_cd,'OTHER')
		  ,race_cd = isnull(new.race_cd,'11')
		  ,household_cd = isnull(new.household_cd,'7')
		  ,never_bill_reason_cd = case when new.ACCT_PRD is not null and new.ACCT_PRD < 200903 then 'CCRCBILLED' else null end
		  ,never_pay_reason_cd = case when new.ACCT_PRD is not null and new.ACCT_PRD < 200903 then 'CCRCPAID' else null end
		  ,dflt_reason_1st_cd = isnull(new.dflt_reason_1st_cd, '24')
		  ,dflt_reason_2nd_cd = isnull(new.dflt_reason_2nd_cd, '25')
		  ,hud_termination_reason_cd = new.hud_termination_reason_cd
		  ,hud_termination_dt = new.hud_term_date
		  ,hud_outcome_cd = isnull(new.hud_outcome_cd, 17)
		  ,counseling_duration_cd = isnull(new.counseling_duration_cd,'<30')
		  ,gender_cd = isnull(new.gender_cd,'DECLINED')
		  ,borrower_fname = isnull(new.first_name,'unknown at convert')
		  ,borrower_lname = isnull(new.last_name,'unknown at convert')
		  ,borrower_mname = new.mi_name
		  ,mother_maiden_lname = new.mothers_maiden_name
		  ,primary_contact_no = isnull(new.phn, 'unknown at convert')
		  ,second_contact_no = substring(new.secondary_contact_number,1,20)
		  ,email_1 = new.email_addr
		  ,contact_addr1 = isnull(new.addr_line_1, 'unknown at convert')
		  ,contact_city = isnull(new.city, 'unknown at convert')
		  ,contact_state_cd = isnull(new.state, 'AK')
		  ,contact_zip = isnull(new.zip, '99999')
		  ,prop_addr1 = isnull(new.addr_line_1, 'unknown at convert')
		  ,prop_city = isnull(new.city, 'unknown at convert')
		  ,prop_state_cd = isnull(new.state, 'AK')
		  ,prop_zip = isnull(new.zip, '99999')
		  ,bankruptcy_ind = case when new.first_contact_status_type_code in ('06', '07') then 'Y' else 'N' end
		  ,bankruptcy_attorney = case when rtrim(ltrim(new.first_contact_status_type_code)) in ('06', '07') or rtrim(new.bankruptcy_ind)='Y' then 'unknown at convert' else rtrim(ltrim(new.bankruptcy_attorney_name)) end
		  ,bankruptcy_pmt_current_ind = case when new.first_contact_status_type_code='07' or new.bankruptcy_ind='Y' then 'N' else 'Y' end
		  ,borrower_marital_status_cd = case when new.household_type_code in ('4', '5') then 'M' else null end
/* Begin 19 Mar - Quyen- Fix bug-263 */
		  ,hispanic_ind =	case when new.race_type_code='12' then 'Y' 
							when (new.hispanic_ind IS NULL OR ltrim(new.hispanic_ind) = '')  then 'N'
							ELSE new.hispanic_ind	END
/* End 19 Mar - Quyen- Fix bug-263 */
		  ,duplicate_ind = new.dupe_ind
		  ,fc_notice_received_ind = isnull(new.fc_notice_recd_ind,'N')
		  ,completed_dt = new.referral_result_date
		  ,servicer_consent_ind = case new.privacy_consent_ind when 'N' then 'N' else 'Y' end
		  ,agency_success_story_ind = new.success_story_ind
		  ,summary_sent_other_cd = new.summary_sent_other_cd
		  ,summary_sent_other_dt = new.summary_sent_other_date
		  ,summary_sent_dt = new.summary_rpt_sent_date
		  ,occupant_num = isnull(new.occupants,2)
		  ,loan_dflt_reason_notes = isnull(new.loan_default_rsn, 'unknown at convert')
		  ,action_items_notes = isnull(new.action_items, 'unknown at convert')
		  ,followup_notes = new.followup_notes
		  ,counselor_id_ref = new.counselor_id_ref
		  ,counselor_lname = new.counselor_lname
		  ,counselor_fname = new.counselor_fname
		  ,counselor_email = new.counselor_email
		  ,counselor_phone = new.counselor_phone
		  ,discussed_solution_with_srvcr_ind = isnull(new.discussed_solution_with_srvcr_ind,'N')
		  ,worked_with_another_agency_ind = case new.worked_with_another_agency_ind when 'Y' then 'Y' else 'N' end
		  ,contacted_srvcr_recently_ind = isnull(new.contacted_servicer_ind,'N')
		  ,has_workout_plan_ind = case when new.first_contact_status_type_code in ('08', '09') then 'Y' else 'N' end
		  ,srvcr_workout_plan_current_ind = case new.first_contact_status_type_code when '08' then 'Y' when '09' then 'N' end
		  ,owner_occupied_ind = isnull(new.owner_occupied_ind, 'Y')
		  ,realty_company = new.realty_company_name
		  ,for_sale_ind = isnull(new.property_for_sale_ind,'N')		  
/* Begin 19 Mar - Quyen- Fix bug-260 */
		  ,home_sale_price = case when new.list_price_amt = 0 AND isnull(new.property_for_sale_ind, 'N') = 'N' then NULL
				 when (ltrim(new.list_price_amt) = '' OR new.list_price_amt IS NULL) AND new.property_for_sale_ind = 'Y' then 0
				 ELSE new.list_price_amt END
		  
/* End 19 Mar - Quyen- Fix bug-260 */
		  ,household_gross_annual_income_amt = case when new.mthly_net_income_type_code in ('1','2','3','4','5') then new.mthly_net_income*12/0.7 else 0 end
		  ,intake_credit_score = new.credit_score
		  ,intake_credit_bureau_cd = new.intake_credit_bureau_cd
		  ,create_dt = new.create_date
		  ,create_user_id = new.create_user_id
		  ,create_app_name = new.create_prog_name
		  ,chg_lst_dt = new.chg_lst_date
		  ,chg_lst_user_id = new.chg_lst_user_id
		  ,chg_lst_app_name = new.chg_lst_prog_name			
		from
			referral_changelog as new
			inner join agency a on a.agency_id = new.agency_seq_id
			inner join program p on p.program_id = new.program_seq_id
			inner join foreclosure_case as old
				on new.referral_seq_id = old.ccrc_referral_seq and old.ccrc_sor_ind = 'Y'

/* Begin 19 Mar - Quyen- Row count when insert/update + AMI percentage */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Update from foreclosure_case Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL

		UPDATE	foreclosure_case
		SET		foreclosure_case.AMI_percentage = dbo.hpf_conversion_AMI_percentage(foreclosure_case.household_gross_annual_income_amt, foreclosure_case.prop_zip) 
		FROM	foreclosure_case INNER JOIN referral_changelog new 
					ON new.referral_seq_id = foreclosure_case.ccrc_referral_seq 
						AND foreclosure_case.ccrc_sor_ind = 'Y' 
						AND foreclosure_case.household_gross_annual_income_amt IS NOT NULL 
/* End 19 Mar - Quyen */			
		--Execute and log the dynamic SQL
		exec hpf_executesql 'Update from foreclosure_case Table', @sql, @logID, @debug

	end --if	

--	set nocount off 
end --proc

GO

/**********************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_etlinsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_loan_etlinsert]
GO

/****** Object:  StoredProcedure [dbo].[hpf_case_loan_etlinsert]    Script Date: 02/25/2009 18:21:48 ******/
CREATE procedure [dbo].[hpf_case_loan_etlinsert]
	 @logicalDate	datetime
	,@operator		varchar(30)
	,@logID			int			--ETL Load ID
	,@debug			bit = 0		--Debug mode
with execute as caller as
/**********************************************************************************************************
* SP Name:		
*		hpf_case_loan_etlinsert
* Parameters:  
*		,@logicalDate	datetime
*		,@operator		varchar(30)
*		,@logID			int
*		,@debug			bit = 0
*
* Purpose: This stored procedure performs a batch insert of the fact rows as part of the case_loan 
*	UPSERT process. Instead of seperating inserts and updates, both are written to an intermediate table 
*	and then set-based operations are used to bulk-upsert into the destination.
*              
* Example:
	hpf_case_loan_etlinsert '2004-12-18', null, 0, 1
*              
**********************************************************************************************************/
begin
--	set nocount on
	DECLARE @rowcount INT;

	if @logicalDate is not null 
	begin --Ignore bad params
		--Coalesce @operator and mitigate injection
		set @operator = nullif(ltrim(rtrim(@operator)), '')
		set @operator = isnull(@operator, suser_sname())
		set @operator = quotename(@operator, '''')

		--Derive the SQL statement
		declare @sql varchar(4000)
		set @sql = ''

		insert into case_loan(
			fc_id,
			acct_num,
			loan_1st_2nd_cd,
			servicer_id,
			other_servicer_name,
			loan_delinq_status_cd,
			mortgage_type_cd,
			arm_reset_ind,
			interest_rate,
			term_length_cd,
			mortgage_program_cd,
			create_dt,
			create_user_id,
			create_app_name,
			chg_lst_dt,
			chg_lst_user_id,
			chg_lst_app_name
		) 
		select
			fc.fc_id,
			isnull(new.loan_id, 'unknown at convert' + cast(fc.fc_id as varchar)),
			'1ST',
			isnull(s.servicer_id,12982),
			case when new.servicer_seq_id in (12982,18601) or new.servicer_seq_id is null then 'unknown at convert' else null end,
			case when new.fc_notice_recd_ind = 'Y' then '120+' else isnull(new.loan_delinq_status_1st_cd,'UNK') end,
			isnull(new.mortgage_type_1st_cd,'UNK'),
			new.arm_reset_ind,
			case when new.first_mortgage_type_code in ('A', 'D') then 7.999 when new.first_mortgage_type_code in ('B', 'E') then 8.001 else 0 end,
			30,
			case when new.first_mortgage_type_code in ('A','B','C','D','E') then 'CONV' when new.first_mortgage_type_code = 'M' then 'PRIV' else 'UNK' end,
			new.create_date,
			new.create_user_id,
			new.create_prog_name,
			new.chg_lst_date,
			new.chg_lst_user_id,
			new.chg_lst_prog_name
		from 
			referral_changelog as new
			inner join foreclosure_case fc 
				on fc.ccrc_referral_seq = new.referral_seq_id
			left join case_loan as old on fc.fc_id = old.fc_id
			left join ccrc_servicer s on s.ccrc_servicer_seq_id = new.servicer_seq_id
		where --we are only interested in unmatched (new) rows
			old.fc_id is null
		union all
		select
			fc.fc_id,
			new.second_loan_id,
			'2ND',
			isnull(s.servicer_id,12982),
			case when new.second_servicer_seq_id in (12982,18601) or new.second_servicer_seq_id is null then 'unknown at convert' else null end,
			case when new.fc_notice_recd_ind = 'Y' then '120+' else isnull(new.loan_delinq_status_2nd_cd,'UNK') end,
			isnull(new.mortgage_type_2nd_cd,'UNK'),
			null,
			case when new.second_mortgage_type_code in ('A', 'D') then 7.999 when new.second_mortgage_type_code in ('B', 'E') then 8.001 else 0 end,
			30,
			case when new.second_mortgage_type_code in ('A','B','C','D','E') then 'CONV' when new.second_mortgage_type_code = 'M' then 'PRIV' else 'UNK' end,
			new.create_date,
			new.create_user_id,
			new.create_prog_name,
			new.chg_lst_date,
			new.chg_lst_user_id,
			new.chg_lst_prog_name
		from 
			referral_changelog as new
			inner join foreclosure_case fc 
				on fc.ccrc_referral_seq = new.referral_seq_id and new.second_loan_id is not null
			left join case_loan as old on fc.fc_id = old.fc_id
			left join ccrc_servicer s on s.ccrc_servicer_seq_id = new.second_servicer_seq_id
		where --we are only interested in unmatched (new) rows
			old.fc_id is null
		order by --ordered insert so index build is more efficient
			 fc.fc_id

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert into case_loan Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert into case_loan Table', @sql, @logID, @debug

	end --if

--	set nocount off
end --proc
GO
/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_etlupdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_loan_etlupdate]
GO

/****** Object:  StoredProcedure [dbo].[hpf_case_loan_etlupdate]    Script Date: 02/25/2009 18:22:15 ******/
CREATE  procedure [dbo].[hpf_case_loan_etlupdate]
	 @logicalDate	datetime
	,@operator		varchar(30)
	,@logID			int			--ETL Load ID
	,@debug			bit = 0		--Debug mode
with execute as caller as
/**********************************************************************************************************
* SP Name:		
*		hpf_case_loan_etlupdate
* Parameters:  
*		,@logicalDate	datetime
*		,@operator		varchar(30)
*		,@logID			int
*		,@debug			bit = 0
*
* Purpose: This stored procedure performs a batch insert of the fact rows as part of the case_loan 
*	UPSERT process. Instead of seperating inserts and updates, both are written to an intermediate table 
*	and then set-based operations are used to bulk-upsert into the destination.
*              
* Example:
	hpf_case_loan_etlupdate '2004-12-18', null, 0, 1
*              
**********************************************************************************************************/
begin
--	set nocount on
	DECLARE @rowcount INT;

	if @logicalDate is not null 
	begin --Ignore bad params
		--Coalesce @operator and mitigate injection
		set @operator = nullif(ltrim(rtrim(@operator)), '')
		set @operator = isnull(@operator, suser_sname())
		set @operator = quotename(@operator, '''')

		--Derive the SQL statement
		declare @sql varchar(4000)
		set @sql = ''

		update case_loan set
			acct_num = isnull(new.loan_id, 'unknown at convert' + cast(fc.fc_id as varchar)),
			servicer_id = isnull(s.servicer_id,12982),
			loan_delinq_status_cd = case when new.fc_notice_recd_ind = 'Y' then '120+' else isnull(new.loan_delinq_status_1st_cd,'UNK') end,
			mortgage_type_cd = isnull(new.mortgage_type_1st_cd,'UNK'),
			arm_reset_ind = new.arm_reset_ind,
			interest_rate = case when new.first_mortgage_type_code in ('A', 'D') then 7.999 when new.first_mortgage_type_code in ('B', 'E') then 8.001 else 0 end,
			mortgage_program_cd = case when new.first_mortgage_type_code in ('A','B','C','D','E') then 'CONV' when new.first_mortgage_type_code = 'M' then 'PRIV' else 'UNK' end,
			create_dt = new.create_date,
			create_user_id = new.create_user_id,
			create_app_name = new.create_prog_name,
			chg_lst_dt = new.chg_lst_date,
			chg_lst_user_id = new.chg_lst_user_id,
			chg_lst_app_name = new.chg_lst_prog_name
		from 
			referral_changelog as new
			inner join foreclosure_case fc 
				on fc.ccrc_referral_seq = new.referral_seq_id and fc.ccrc_sor_ind = 'Y'
			inner join case_loan as old 
				on fc.fc_id = old.fc_id and old.loan_1st_2nd_cd = '1ST'
			left join ccrc_servicer s on s.ccrc_servicer_seq_id = new.servicer_seq_id

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Update from 1ST case_loan Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Update from 1ST case_loan Table', @sql, @logID, @debug


		--Derive the SQL statement
		set @sql = ''

		update case_loan set
			acct_num = new.second_loan_id,
			servicer_id = isnull(s.servicer_id,12982),
			loan_delinq_status_cd = case when new.fc_notice_recd_ind = 'Y' then '120+' else isnull(new.loan_delinq_status_2nd_cd,'UNK') end,
			mortgage_type_cd = isnull(new.mortgage_type_2nd_cd,'UNK'),
			arm_reset_ind = null,
			interest_rate = case when new.second_mortgage_type_code in ('A', 'D') then 7.999 when new.second_mortgage_type_code in ('B', 'E') then 8.001 else 0 end,
			mortgage_program_cd = case when new.second_mortgage_type_code in ('A','B','C','D','E') then 'CONV' when new.second_mortgage_type_code = 'M' then 'PRIV' else 'UNK' end,
			create_dt = new.create_date,
			create_user_id = new.create_user_id,
			create_app_name = new.create_prog_name,
			chg_lst_dt = new.chg_lst_date,
			chg_lst_user_id = new.chg_lst_user_id,
			chg_lst_app_name = new.chg_lst_prog_name
		from 
			referral_changelog as new
			inner join foreclosure_case fc 
				on fc.ccrc_referral_seq = new.referral_seq_id and fc.ccrc_sor_ind = 'Y'
					and new.second_loan_id is not null
			inner join case_loan as old 
				on fc.fc_id = old.fc_id and old.loan_1st_2nd_cd = '2ND'
			left join ccrc_servicer s on s.ccrc_servicer_seq_id = new.second_servicer_seq_id

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Update from 2ND case_loan Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Update from 2ND case_loan Table', @sql, @logID, @debug

	end --if

--	set nocount off
end --proc
GO

/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_referral_budget_upsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_referral_budget_upsert]
GO

/****** Object:  StoredProcedure [dbo].[hpf_referral_budget_upsert]    Script Date: 02/25/2009 18:22:15 ******/
CREATE procedure [dbo].[hpf_referral_budget_upsert]
	 @logicalDate	datetime
	,@operator		varchar(30)
	,@logID			int			--ETL Load ID
	,@debug			bit = 0		--Debug mode
with execute as caller as
/**********************************************************************************************************
* SP Name:		
*		hpf_referral_budget_upsert
* Parameters:  
*		,@logicalDate	datetime
*		,@operator		varchar(30)
*		,@logID			int
*		,@debug			bit = 0
*
* Purpose: This stored procedure performs a batch insert of the fact rows as part of the referral_budget 
*	UPSERT process. Instead of seperating inserts and updates, both are written to an intermediate table 
*	and then set-based operations are used to bulk-upsert into the destination.
*              
* Example:
	exec hpf_referral_budget_upsert '2004-12-18', null, 0, 1
*              
**********************************************************************************************************/
begin
	--set nocount on
	DECLARE @rowcount INT;

	if @logicalDate is not null 
	begin --Ignore bad params
		--Coalesce @operator and mitigate injection
		set @operator = nullif(ltrim(rtrim(@operator)), '')
		set @operator = isnull(@operator, suser_sname())
		set @operator = quotename(@operator, '''')

		select  
			rb.referral_seq_id,
			rb.create_date,
			rb.create_user_id,
			rb.create_prog_name,
			rb.chg_lst_date,
			rb.chg_lst_user_id,
			rb.chg_lst_prog_name
		into #temp1
		from referral_budget_changelog rb inner join
			(
				select referral_seq_id, max(set_id) set_id
				from referral_budget_changelog
				group by referral_seq_id
			) t on t.referral_seq_id = rb.referral_seq_id and t.set_id = rb.set_id

		select 
			fc.fc_id,
			new.referral_seq_id,
			new.create_date,
			new.create_user_id,
			new.create_prog_name,
			new.chg_lst_date,
			new.chg_lst_user_id,
			new.chg_lst_prog_name
		into #temp2
		from #temp1 as new
			inner join foreclosure_case fc
				on fc.ccrc_referral_seq = new.referral_seq_id and fc.ccrc_sor_ind = 'Y'


		declare @sql varchar(4000)
		--Derive the SQL statement, insert budget_set from referral_budget
		set @sql = ''
		insert into budget_set(
			fc_id,
			budget_set_dt,
			create_dt,
			create_user_id,
			create_app_name,
			chg_lst_dt,
			chg_lst_user_id,
			chg_lst_app_name
		)
		select distinct
			fc_id,
			chg_lst_date,
			create_date,
			create_user_id,
			create_prog_name,
			chg_lst_date,
			chg_lst_user_id,
			chg_lst_prog_name
		from #temp2
/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert into budget_set Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert from budget_set Table', @sql, @logID, @debug

		--Derive the SQL statement, insert budget_item from referral_budget
		set @sql = ''
		insert into budget_item(
			budget_set_id,
			budget_subcategory_id,
			budget_item_amt,
			budget_note,
			create_dt,
			create_user_id,
			create_app_name,
			chg_lst_dt,
			chg_lst_user_id,
			chg_lst_app_name
		)
		select
			bs.budget_set_id,
			new.budget_subcategory_seq_id,
			new.amt,
			new.budget_note,
			new.create_date,
			new.create_user_id,
			new.create_prog_name,
			new.chg_lst_date,
			new.chg_lst_user_id,
			new.chg_lst_prog_name
		from #temp2 t
			inner join referral_budget_changelog as new
				on new.referral_seq_id = t.referral_seq_id
			inner join budget_set bs on bs.fc_id = t.fc_id

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert budget_item Table from referral_budget',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert budget_item Table from referral_budget', @sql, @logID, @debug

-----------------------------------------------------------------------------
		--insert budget_item from referral
		select
			rn.fc_id,
			rc.mthly_net_income_type_code,
			rc.mthly_net_income,
			rc.mthly_expense_type_code,
			rc.mthly_expense,
			rc.piti_amt,
			rn.create_dt,
			rn.create_user_id,
			rn.create_app_name,
			rn.chg_lst_dt,
			rn.chg_lst_user_id,
			rn.chg_lst_app_name			
		into #budget_from_referral
		from referral_changelog rc
			inner join referral_new rn on rc.referral_seq_id = rn.ccrc_referral_seq

		select 
			bs.fc_id,
			bi.budget_set_id,
			bi.budget_item_id,
			bsc.budget_category_id, 
			bsc.budget_subcategory_id, 
			bi.budget_item_amt
		into #budget_item_info
		from budget_item bi
			inner join budget_subcategory bsc on bsc.budget_subcategory_id = bi.budget_subcategory_id
			inner join budget_set bs on bs.budget_set_id = bi.budget_set_id
			inner join foreclosure_case fc on fc.fc_id = bs.fc_id and fc.ccrc_sor_ind = 'Y'

		--Derive the SQL statement, insert budget_set from referral
		set @sql = ''

		insert into budget_set(
			fc_id,
			budget_set_dt,
			create_dt,
			create_user_id,
			create_app_name,
			chg_lst_dt,
			chg_lst_user_id,
			chg_lst_app_name
		)
		select distinct
			bfr.fc_id,
			bfr.chg_lst_dt,
			bfr.create_dt,
			bfr.create_user_id,
			bfr.create_app_name,
			bfr.chg_lst_dt,
			bfr.chg_lst_user_id,
			bfr.chg_lst_app_name
		from #budget_from_referral bfr
			left join #budget_item_info bif on bif.fc_id = bfr.fc_id
		where bif.fc_id is null
			and bfr.mthly_net_income_type_code in ('1','2','3','4','5')
			and bif.budget_category_id = 1 and bif.budget_item_amt >0

		union
		select distinct
			bfr.fc_id,
			bfr.chg_lst_dt,
			bfr.create_dt,
			bfr.create_user_id,
			bfr.create_app_name,
			bfr.chg_lst_dt,
			bfr.chg_lst_user_id,
			bfr.chg_lst_app_name
		from #budget_from_referral bfr
			left join #budget_item_info bif on bif.fc_id = bfr.fc_id
		where bif.fc_id is null
			and bif.budget_category_id in (3,4,5,6,7,8,9,10) and bif.budget_item_amt >0

		union
		select distinct
			bfr.fc_id,
			bfr.chg_lst_dt,
			bfr.create_dt,
			bfr.create_user_id,
			bfr.create_app_name,
			bfr.chg_lst_dt,
			bfr.chg_lst_user_id,
			bfr.chg_lst_app_name
		from #budget_from_referral bfr
			left join #budget_item_info bif on bif.fc_id = bfr.fc_id
		where bif.fc_id is null
			and bif.budget_subcategory_id =8 and bif.budget_item_amt >0

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert budget_item Table from referral',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert budget_set Table from referral', @sql, @logID, @debug

		--Derive the SQL statement, insert budget_item from referral
		set @sql = ''

		insert into budget_item(
			budget_set_id,
			budget_subcategory_id,
			budget_item_amt,
			budget_note,
			create_dt,
			create_user_id,
			create_app_name,
			chg_lst_dt,
			chg_lst_user_id,
			chg_lst_app_name
		)
		select distinct
			bs.budget_set_id,
			6,
			bfr.mthly_net_income,
			'converted MTHLY_NET_INCOME_TYPE_CODE',
			bfr.create_dt,
			bfr.create_user_id,
			bfr.create_app_name,
			bfr.chg_lst_dt,
			bfr.chg_lst_user_id,
			bfr.chg_lst_app_name
		from #budget_from_referral bfr
			inner join budget_set bs on bs.fc_id = bfr.fc_id
			left join #budget_item_info bif on bif.fc_id = bfr.fc_id
		where bfr.mthly_net_income_type_code in ('1','2','3','4','5')
			and bif.budget_category_id = 1 and bif.budget_item_amt >0
			and bif.fc_id is null

		union all
		select distinct
			bs.budget_set_id,
			49,
			bfr.mthly_expense,
			'converted MTHLY_EXPENSE_TYPE_CODE',
			bfr.create_dt,
			bfr.create_user_id,
			bfr.create_app_name,
			bfr.chg_lst_dt,
			bfr.chg_lst_user_id,
			bfr.chg_lst_app_name
		from #budget_from_referral bfr
			inner join budget_set bs on bs.fc_id = bfr.fc_id
			left join #budget_item_info bif on bif.fc_id = bfr.fc_id
		where bif.budget_category_id in (3,4,5,6,7,8,9,10) and bif.budget_item_amt >0
			and bif.fc_id is null

		union all
		select distinct
			bs.budget_set_id,
			8,
			bfr.piti_amt,
			'converted PITI_AMT',
			bfr.create_dt,
			bfr.create_user_id,
			bfr.create_app_name,
			bfr.chg_lst_dt,
			bfr.chg_lst_user_id,
			bfr.chg_lst_app_name
		from #budget_from_referral bfr
			inner join budget_set bs on bs.fc_id = bfr.fc_id
			left join #budget_item_info bif on bif.fc_id = bfr.fc_id
		where bif.budget_subcategory_id =8 and bif.budget_item_amt >0
			and bif.fc_id is null

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert budget_item Table from referral',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert budget_item Table from referral', @sql, @logID, @debug

	end --if

--	set nocount off
end --proc
GO
/*************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_etlins_upd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_outcome_item_etlins_upd]
GO
CREATE procedure [dbo].[hpf_outcome_item_etlins_upd]
	 @logicalDate	datetime
	,@operator		varchar(30)
	,@logID			int			--ETL Load ID
	,@debug			bit = 0		--Debug mode
with execute as caller as
/**********************************************************************************************************
* SP Name:		
*		hpf_outcome_item_etlins_upd
* Parameters:  
*		,@logicalDate	datetime
*		,@operator		varchar(30)
*		,@logID			int
*		,@debug			bit = 0
*
* Purpose: This stored procedure performs a batch insert of the fact rows as part of the referral_hist 
*	UPSERT process. Instead of seperating inserts and updates, both are written to an intermediate table 
*	and then set-based operations are used to bulk-upsert into the destination.
*              
* Example:
	exec hpf_outcome_item_etlins_upd '2004-12-18', null, 0, 1
*              
**********************************************************************************************************/
begin
	--set nocount on
	DECLARE @rowcount INT;
	declare @sql varchar(4000);

	if @logicalDate is not null 
	begin --Ignore bad params
		--Coalesce @operator and mitigate injection
		set @operator = nullif(ltrim(rtrim(@operator)), '')
		set @operator = isnull(@operator, suser_sname())
		set @operator = quotename(@operator, '''')
		
		SET @sql='';
		insert into outcome_item (
			fc_id, 
			outcome_type_id, 
			outcome_dt, 
			create_dt, 
			create_user_id, 
			create_app_name, 
			chg_lst_dt, 
			chg_lst_user_id, 
			chg_lst_app_name
		)
		select 
			fc.fc_id,
			isnull(new.outcome_type_id,11),
			new.referral_result_date,
			new.create_date,
			new.create_user_id,
			new.create_prog_name,
			new.chg_lst_date,
			new.chg_lst_user_id,
			new.chg_lst_prog_name
		from ref_result_changelog new
			inner join foreclosure_case fc
				on fc.ccrc_referral_seq = new.referral_seq_id and fc.ccrc_sor_ind = 'Y';

/* Begin 19 Mar - Quyen- Row count when insert/update */
		SET @rowcount  = @@rowcount;
		exec hpf_log_event_package_oncount	@logID,	@ComponentName = 'Insert into outcome_item Table',	@Rows = @rowcount, @TimeMS = 0, @MinRowsPerSec = NULL,	@MaxRowsPerSec = NULL
/* End 19 Mar - Quyen- Row count when insert/update */

		--Execute and log the dynamic SQL
		exec hpf_executesql 'Insert from outcome_item Table', @sql, @logID, @debug
	end --if

--	set nocount off
end --proc
GO
/*********************************************************************************************/
