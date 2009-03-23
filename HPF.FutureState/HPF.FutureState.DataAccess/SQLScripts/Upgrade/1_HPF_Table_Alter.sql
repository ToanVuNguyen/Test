-- =============================================
-- Create date: 23 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 23 Mar 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

ALTER TABLE call ADD call_center_name varchar(4) NULL;
UPDATE call SET call_center_name = call_center;
ALTER TABLE call DROP COLUMN call_center ;
