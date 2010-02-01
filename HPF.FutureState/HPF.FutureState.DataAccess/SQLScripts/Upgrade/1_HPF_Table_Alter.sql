-- =============================================
-- Create date: 1 Feb 2010
-- Project : HPF 
-- Build 
-- Description:	
-- =============================================
USE HPF
GO

ALTER TABLE  mha_escalation ADD	item_created_user varchar(50) NULL;
ALTER TABLE  mha_escalation ADD item_modified_user varchar(50) NULL;
ALTER TABLE  mha_escalation ADD item_modified_dt datetime NULL;
EXEC sp_rename 'mha_escalation.created_dt', 'item_created_dt', 'COLUMN';
