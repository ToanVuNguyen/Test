-- =============================================
-- Create date: 06 Aug 2009
-- Project : HPF 
-- Build 
-- =============================================
USE HPF
GO
UPDATE	menu_item 
SET		item_name = 'Mark Duplicates'
where	menu_item_id = 20 and menu_group_id = 4;

CREATE TABLE [dbo].[batch_job](
	[batch_job_id] [int] IDENTITY(1,1) NOT NULL,
	[job_name] [varchar](50) NOT NULL,
	[job_description] [varchar](100)  NULL,
	[job_frequency] [varchar](15)  NOT NULL,
	[job_start_dt] [datetime] NOT NULL,
	[job_last_start_dt] [datetime] NOT NULL,
	[requestor_type] [varchar](20)  NOT NULL,
	[requestor_id] [int] NOT NULL,
	[output_format] [varchar](50)  NOT NULL,
	[output_destination] [varchar](500)  NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [varchar](30)  NOT NULL,
	[create_app_name] [varchar](20)  NOT NULL,
	[chg_lst_dt] [datetime] NOT NULL,
	[chg_user_id] [varchar](30)  NOT NULL,
	[chg_app_name] [varchar](20)  NOT NULL,
 CONSTRAINT [PK_batch_job] PRIMARY KEY CLUSTERED 
(
	[batch_job_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

--=================
CREATE TABLE [dbo].[batch_job_log](
	[batch_job_log_id] [int] IDENTITY(1,1) NOT NULL,
	[batch_job_id] [int] NOT NULL,
	[job_result] [varchar](50)  NOT NULL,
	[record_count] [int] NOT NULL,
	[job_notes] [varchar](500)  NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [varchar](30)  NOT NULL,
	[create_app_name] [varchar](20)  NOT NULL,
	[chg_lst_dt] [datetime] NOT NULL,
	[chg_user_id] [varchar](30)  NOT NULL,
	[chg_app_name] [varchar](20)  NOT NULL,
 CONSTRAINT [PK_batch_job_log] PRIMARY KEY CLUSTERED 
(
	[batch_job_log_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[batch_job_log]  WITH CHECK ADD  CONSTRAINT [FK_batch_job_log_batch_job] FOREIGN KEY([batch_job_id])
REFERENCES [dbo].[batch_job] ([batch_job_id])
GO
