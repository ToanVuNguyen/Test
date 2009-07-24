-- =============================================
-- Create date: 24 Jul 2009
-- Project : HPF 
-- Build 
-- =============================================
USE HPF
GO

CREATE TABLE [dbo].[admin_task_log](
	admin_task_log_id int IDENTITY(1,1) NOT NULL,
	task_name varchar(50) NOT NULL,
	record_count int NOT NULL,
	fc_id_list text NOT NULL,
	task_notes varchar(1000) NULL,
	create_dt datetime NOT NULL,
	create_user_id varchar(30) NOT NULL,
	create_app_name varchar(20) NOT NULL,
	chg_lst_dt datetime NOT NULL,
	chg_lst_user_id varchar(30) NOT NULL,
	chg_lst_app_name varchar(20) NOT NULL,
	CONSTRAINT PK_admin_task_log PRIMARY KEY CLUSTERED ([admin_task_log_id] ASC)
)
GO

INSERT INTO menu_group (menu_group_id,group_name, group_sort_order, group_target) VALUES (4,'Admin',4,'#');

INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   
VALUES (19,4,'Send Summaries',1,'SendSummaryToServicer.aspx',1);
INSERT INTO menu_item (menu_item_id, menu_group_id,item_name, item_sort_order, item_target, visibled)   
VALUES (20,4,'Make Duplicates',2,'MarkDuplicateCases.aspx',1);

INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (21,34,19,'U');
INSERT INTO menu_security (menu_security_id, hpf_user_id, menu_item_id, permission_value)   VALUES (22,34,20,'U');