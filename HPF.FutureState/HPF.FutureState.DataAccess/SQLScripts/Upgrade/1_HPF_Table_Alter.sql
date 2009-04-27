-- =============================================
-- Create date: 27 Apr 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 27 Apr 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

ALTER TABLE invoice_payment ALTER COLUMN payment_file varchar(255) NULL;