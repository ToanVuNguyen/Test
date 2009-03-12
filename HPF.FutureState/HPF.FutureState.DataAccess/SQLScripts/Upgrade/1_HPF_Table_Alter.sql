-- =============================================
-- Create date: 12 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 12 Mar 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

ALTER TABLE agency ADD sharepoint_foldername varchar(256) NULL;
ALTER TABLE funding_source ADD sharepoint_foldername varchar(256) NULL;
ALTER TABLE servicer ADD sharepoint_foldername varchar(256) NULL;

UPDATE	ref_code_item  SET	code = 'UNK' where	ref_code_set_name = 'loan delinquency status code' and code ='Unknown' ;
UPDATE	case_loan SET	loan_delinq_status_cd = 'UNK' WHERE	loan_delinq_status_cd = 'Unknown';
UPDATE	case_post_counseling_status SET	loan_delinq_status_cd = 'UNK' WHERE	loan_delinq_status_cd = 'Unknown';

UPDATE  ref_code_item  SET code = 'FREDDUPE'  WHERE ref_code_set_name = 'payment reject reason code' AND code= 'FREDDUP';
UPDATE  invoice_case SET		pmt_reject_reason_cd = 'FREDDUPE' WHERE pmt_reject_reason_cd = 'FREDDUP';

ALTER TABLE Funding_source Add phone varchar(20) null ;
