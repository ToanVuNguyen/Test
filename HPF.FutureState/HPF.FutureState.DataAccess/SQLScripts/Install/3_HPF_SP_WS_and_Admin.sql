-- =============================================
-- Create date: 02 Apr 2009
-- Project : HPF 
-- Build 
-- Description:	Create stored procedures, functions are being used in HPF Web service and HPF Web Admin
-- =============================================
USE [hpf]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_loan_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_servicer_get_from_FcId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_servicer_get_from_FcId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_loan_delete]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_loan_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_geo_code_ref_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_geo_code_ref_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_call_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_call_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_activity_log_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_activity_log_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_set_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_set_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_set_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_set_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_asset_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_asset_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_item_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_item_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_set_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_set_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_post_counseling_status_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_post_counseling_status_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_post_counseling_status_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_post_counseling_status_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_audit_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_audit_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_audit_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_audit_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_security_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_menu_security_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_item_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_item_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_detail_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_detail_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_outcome_item_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_outcome_item_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_case_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_case_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_case_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_case_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_asset_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_asset_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_update_app]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_update_app]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_get_from_agencyID_and_caseNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_get_from_agencyID_and_caseNumber]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_update_ws]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_update_ws]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_hpf_user_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_hpf_user_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_get_from_login]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_hpf_user_get_from_login]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_hpf_user_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_check_foreign_key]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_call_check_foreign_key]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_program_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_program_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClearLogs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClearLogs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_detail_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_detail_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_Invoice_case_validate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_Invoice_case_validate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_post_counseling_status_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_post_counseling_status_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_type_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_outcome_type_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_outcome_item_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_detail_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_detail_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_payment_search]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_loan_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_ref_code_item_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_ref_code_item_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_search]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_activity_log_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_activity_log_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_search]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_audit_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_case_audit_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_Invoice_case_update_for_payment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_Invoice_case_update_for_payment]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_detail_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_detail_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_case_update_for_invoice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_case_update_for_invoice]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_detail_get_call]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_detail_get_call]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_detail_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_detail_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_payment_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_payment_insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_payment_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_ws_user_get_from_username_password]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_ws_user_get_from_username_password]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_servicer_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_servicer_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_group_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_menu_group_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_bar_permission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_menu_bar_permission]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_security_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_menu_security_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WriteLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WriteLog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_view_budget_category_code]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_view_budget_category_code]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_subcategory_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_budget_subcategory_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_funding_source_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_funding_source_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_center_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_call_center_get]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddCategory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_get_duplicate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_get_duplicate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_search_app_dynamic]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_search_app_dynamic]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_search_draft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_search_draft]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_search_ws]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_foreclosure_case_search_ws]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_case_search_draft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_invoice_case_search_draft]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_case_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_agency_payable_case_update]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_decryption]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[hpf_decryption]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertCategoryLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertCategoryLog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_encryption]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[hpf_encryption]
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
	, @pi_where_str varchar(8000) = null
AS
SET NOCOUNT ON
DECLARE @v_str varchar(8000);
BEGIN
	SELECT @v_str= 
		''SELECT	CL.Acct_Num, A.Agency_Name, CL.Servicer_ID, FC.FC_ID, S.Servicer_name
				, FC.borrower_fname, FC.borrower_lname, FC.prop_zip
				, FC.agency_case_num, FC.counselor_fname, FC.counselor_lname
				, FC.counselor_phone, FC.counselor_email, FC.counselor_ext
				, O.outcome_dt, ot.outcome_type_name
		FROM	Foreclosure_Case FC, Case_Loan CL, Agency A, Servicer S, outcome_type ot
				, (SELECT max(i1.outcome_item_id) as outcome_item_id, fc_id, outcome_type_id, outcome_dt FROM outcome_item i1
				WHERE outcome_dt = (SELECT max(outcome_dt) FROM outcome_item i2	WHERE outcome_deleted_dt IS NULL AND i1.fc_id = i2.fc_id)
				GROUP BY fc_id, outcome_type_id, outcome_dt ) O
		WHERE  	FC.fc_id=CL.fc_id 
				AND o.outcome_type_id = ot.outcome_type_id
				AND o.fc_id = FC.fc_id
				AND FC.Agency_Id = A.Agency_Id
				AND CL.Servicer_ID = S.Servicer_ID
				AND (FC.Duplicate_Ind= ''''N'''')
				AND	((FC.Completed_dt is null AND FC.Create_dt > DATEADD(year,-1, GetDate())) OR (FC.Completed_dt > DATEADD(year,-1, GetDate())))
				AND ('' + @pi_where_str + '')''	
	IF (@pi_fc_id is not null)
					SELECT @v_str = @v_str + '' AND FC.fc_id <> '' + cast(@pi_fc_id as varchar(10))

	PRINT @v_str;
	EXECUTE (@v_str);
	
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
	@pi_servicer int=-1, 	
	@pi_pagesize int,
	@pi_pagenum int,
	@whereclause nvarchar(1000)=null,
	@po_totalrownum int output
)
AS
BEGIN	
DECLARE @cmdstring nvarchar(4000)
DECLARE @parameterlist nvarchar(1000)

	SET @cmdstring=
	''SELECT rownum , fc_id, agency_id, agency_name, agency_case_num, completed_dt, intake_dt
		, borrower_fname, borrower_lname, borrower_last4_SSN, co_borrower_fname, co_borrower_lname, co_borrower_last4_SSN
		, prop_addr1, prop_city, prop_state_cd, prop_zip, bankruptcy_ind, fc_notice_received_ind
		, counselor_email, counselor_phone,counselor_fname,counselor_lname, counselor_ext, loan_list      
	FROM	(SELECT rownum=ROW_NUMBER() OVER (ORDER BY borrower_lname asc,co_borrower_lname asc,borrower_fname asc,
				co_borrower_fname asc,f.fc_id desc ),f.agency_id,f.fc_id, completed_dt, intake_dt, borrower_fname, borrower_lname, borrower_last4_SSN, co_borrower_fname,
				co_borrower_lname, co_borrower_last4_SSN, prop_addr1, prop_city, prop_state_cd, prop_zip, a.agency_name, 
				counselor_email, counselor_phone,counselor_fname,counselor_lname, agency_case_num, counselor_ext, loan_list, bankruptcy_ind, fc_notice_received_ind     
			FROM	foreclosure_case f INNER JOIN Agency a ON a.agency_id = f.agency_id 
			WHERE	EXISTS ( SELECT f1.fc_id FROM foreclosure_case f1 INNER JOIN program p ON f1.program_id = p.program_id LEFT OUTER JOIN case_loan l ON f1.fc_id = l.fc_id
							WHERE f1.fc_id = f.fc_id ''+@whereclause+'')
		)ff
	WHERE rownum BETWEEN @pi_pagesize*(@pi_pagenum-1)+1 AND (@pi_pagesize*@pi_pagenum);
	SELECT @po_totalrownum =COUNT(*) FROM foreclosure_case f 
	WHERE EXISTS ( SELECT f1.fc_id FROM foreclosure_case f1 INNER JOIN program p 
		ON f1.program_id = p.program_id INNER JOIN case_loan l ON f1.fc_id = l.fc_id     
	WHERE f1.fc_id = f.fc_id ''+@whereclause+'')'';
	
	SET @parameterlist=''
	@pi_last4SSN varchar(4)=null , @pi_fname varchar(30)=null, @pi_lname varchar(30)=null,
	@pi_fc_id int =-1, @pi_agencycaseid varchar(30)=null, @pi_loannum varchar(50)=null,
	@pi_propzip varchar(5)=null, @pi_propstate varchar(15)=null,
	@pi_duplicate char(1)=null,
	@pi_agencyid int=-1, @pi_programid int=-1, @pi_servicer int=-1,	
	@pi_pagesize int, @pi_pagenum int, @po_totalrownum int output''
	
	exec sp_executesql 
	@cmdstring,
	@parameterlist,
	@pi_last4SSN, @pi_fname, @pi_lname,
	@pi_fc_id, @pi_agencycaseid, @pi_loannum,
	@pi_propzip, @pi_propstate,
	@pi_duplicate,
	@pi_agencyid, @pi_programid,@pi_servicer,
	@pi_pagesize,@pi_pagenum, @po_totalrownum  output
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_search_draft]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'












-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 05 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Search FCase for create draft new Agency Payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_search_draft]
	@pi_agency_id				int ,
    @pi_start_dt				datetime ,
    @pi_end_dt					datetime ,	
	@pi_case_completed_ind		varchar(1) = ''Y'', 
	@pi_consent_flag				int = 0
AS
DECLARE @v_sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
	SET @v_sql = 
		''SELECT	f1.fc_id, f1.agency_id, f1.agency_case_num, f1.create_dt, f1.completed_dt
			, f1.pmt_rate, l.acct_num, s.servicer_name
			, f1.borrower_fname + '''' '''' + f1.borrower_lname as borrower_name			
			, f1.servicer_consent_ind, f1.funding_consent_ind
		FROM	(SELECT f.fc_id, f.agency_id, f.agency_case_num, f.completed_dt, ar.pmt_rate, f.borrower_fname , f.borrower_lname 
						, f.servicer_consent_ind, f.funding_consent_ind, f.never_pay_reason_cd, f.create_dt
				FROM foreclosure_case f 
					LEFT OUTER JOIN agency_rate ar ON	f.agency_id = ar.agency_id AND f.program_id = ar.program_id 
									AND f.completed_dt >= ar.eff_dt	and (f.completed_dt <= ar.exp_dt OR ar.exp_dt is NULL)
				) f1 INNER JOIN case_loan l ON f1.fc_id= l.fc_id AND l.loan_1st_2nd_cd= ''''1ST'''''';
	SELECT @v_sql = @v_sql + '' INNER JOIN servicer s ON l.servicer_id = s.servicer_id ''
				+ '' LEFT OUTER JOIN (SELECT apc.fc_id FROM agency_payable_case apc, agency_payable ap 
									WHERE	apc.agency_payable_id = ap.agency_payable_id	and ap.status_cd = ''''ACTIVE'''' AND ap.agency_id = ''+ cast(@pi_agency_id as varchar(10)) + '') apc ON apc.fc_id = f1.fc_id ''
				+ '' WHERE f1.agency_id = '' + cast(@pi_agency_id as varchar(10))
				+ '' AND f1.never_pay_reason_cd IS NULL AND apc.fc_id IS NULL '';
	IF (@pi_case_completed_ind = ''Y'')
		SELECT @v_sql = @v_sql +'' AND f1.completed_dt IS NOT NULL AND f1.completed_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(20)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(20)) + '''''''';
	IF (@pi_case_completed_ind = ''N'')
		SELECT @v_sql = @v_sql +'' AND f1.completed_dt IS NULL AND f1.create_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(20)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(20)) + '''''''';
	IF (@pi_case_completed_ind =''B'')
		SELECT @v_sql = @v_sql +'' AND (f1.completed_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(20)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(20)) + ''''''''
						+ '' OR f1.create_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(20)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(20)) + '''''')'';

	IF (@pi_consent_flag = 0)
		SELECT @v_sql = @v_sql + '' AND (f1.servicer_consent_ind = ''''Y'''' OR f1.funding_consent_ind = ''''Y'''')'' 
	SELECT @v_sql=@v_sql+'' ORDER BY f1.completed_dt asc,f1.create_dt asc'';
	PRINT (@v_sql)
	EXECUTE (@v_sql);
END












' 
END
GO
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
	Set @v_select_clause = 
			''SELECT TOP(@pi_number_of_rows)							
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
					case_loan.[loan_1st_2nd_cd],
					case_loan.[loan_delinq_status_cd] as [delinquent_cd],
					foreclosure_case.[bankruptcy_ind],
					foreclosure_case.[fc_notice_received_ind],					
					foreclosure_case.[loan_list] as [loan_number],
					servicer.[servicer_name] as [loan_servicer], 
					foreclosure_case.[agency_case_num],
					case_loan.[case_loan_id]
				FROM	foreclosure_case, Agency, case_loan, servicer ''

	 Set @v_orderby_clause = '' ORDER BY foreclosure_case.[borrower_lname] asc
							, foreclosure_case.[borrower_fname] asc
							, foreclosure_case.[co_borrower_lname] asc
							, foreclosure_case.[co_borrower_fname] asc
							, foreclosure_case.[fc_id] desc''
	Set @v_full_string = @v_select_clause + @pi_where_clause + @v_orderby_clause;
	
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
	print @v_full_string		
End





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_case_search_draft]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
















-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 19 Mar 2009
-- Project : HPF 
-- Build 
-- Description:	Search FCase for create draft new Invoice case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_case_search_draft]
	@pi_funding_source_id		int = null,
	@pi_selected_servicer_option_1 int = null, -- 1: checked, 0: unchecked, null : unchecked
	@pi_selected_servicer_option_2 int = null,
	@pi_selected_servicer_option_3 int = null,
	@pi_selected_servicer_option_4 int = null,
	@pi_selected_servicer_option_5 int = null,

	@pi_program_id				int = null,
    @pi_start_dt				datetime = null,
    @pi_end_dt					datetime = null,	
	@pi_duplicate_ind			varchar(1) = null,
	@pi_case_completed_ind		varchar(1) = null,
	@pi_multiple_billing_ind	varchar(1) = null,
	@pi_funding_consent_ind		varchar(1) = null,				
	@pi_servicer_consent_qty	int = -1,

	@pi_max_number_cases		int = null,
	@pi_gender_cd				varchar(15)= null,
	@pi_race_cd					varchar(15)= null,	
	@pi_hispanic_ind			varchar(1)= null,
	@pi_household_cd			varchar(15)= null,
	@pi_city					varchar(30)= null,
	@pi_state_cd				varchar(15)= null,
	@pi_min_age					int = null,
	@pi_max_age					int = null,
	@pi_min_gross_annual_income	numeric(15, 2) = null,
	@pi_max_gross_annual_income	numeric(15, 2) = null
AS
DECLARE @v_sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
	IF 	@pi_max_number_cases IS NOT NULL
		SELECT @v_sql = ''SELECT	TOP ''+ cast(@pi_max_number_cases as varchar(5));
	ELSE
		SELECT @v_sql = ''SELECT	'';

	SELECT @v_sql = @v_sql	+'' f1.fc_id, f1.funding_source_id, f1.agency_case_num, f1.completed_dt,f1.create_dt
				, f1.bill_rate, f1.acct_num, s.servicer_name, f1.borrower_fname + '''' '''' + f1.borrower_lname as borrower_name			
		FROM	(SELECT f.fc_id, fr.funding_source_id, f.program_id, f.agency_case_num, f.completed_dt, fr.bill_rate, f.borrower_fname, f.borrower_lname''
					+ '' ,f.duplicate_ind, f.servicer_consent_ind, f.funding_consent_ind, f.never_bill_reason_cd ''
					+ '' ,f.gender_cd, f.race_cd, DATEDIFF(year, borrower_DOB, GETDATE()) as age''
					+ '' ,f.household_gross_annual_income_amt, f.household_cd, prop_city, prop_state_cd, f.create_dt, f.hispanic_ind ''
					+ '' , l.acct_num, l.servicer_id ''
				+ '' FROM foreclosure_case f ''
					+	'' INNER JOIN case_loan l ON f.fc_id = l.fc_id AND l.loan_1st_2nd_cd= ''''1ST'''' '';
	IF (@pi_selected_servicer_option_1 IS NULL AND @pi_selected_servicer_option_2 IS NULL AND @pi_selected_servicer_option_3 IS NULL  AND @pi_selected_servicer_option_4 IS NULL AND @pi_selected_servicer_option_5 IS NULL)
		SELECT @v_sql = @v_sql	
						+	'' INNER JOIN funding_source_group fsg ON fsg.servicer_id = l.servicer_id AND fsg.funding_source_id = '' + cast(@pi_funding_source_id as varchar(10));
	SELECT @v_sql = @v_sql	
						+ '' LEFT OUTER JOIN funding_source_rate fr ''
						+ '' ON	f.program_id = fr.program_id ''
						+ '' AND isnull(f.completed_dt, f.intake_dt)  BETWEEN fr.eff_dt AND isnull(fr.exp_dt, getdate()) AND fr.funding_source_id =  ''	+ cast(@pi_funding_source_id as varchar(10))
			+ '' ) f1 ''
			+ '' LEFT OUTER JOIN (SELECT fc_id, count(fc_id) as count_fc_id FROM invoice_case ic, invoice i WHERE i.invoice_id = ic.invoice_id AND i.funding_source_id ='' + cast(@pi_funding_source_id as varchar(10)) +'' GROUP BY fc_id) ic1 ON ic1.fc_id = f1.fc_id ''
			+ '' LEFT OUTER JOIN (SELECT fc_id, count(fc_id) as count_fc_id FROM invoice_case ic, invoice i WHERE i.invoice_id = ic.invoice_id AND i.funding_source_id ='' + cast(@pi_funding_source_id as varchar(10)) +'' AND ic.pmt_reject_reason_cd IS NOT NULL GROUP BY fc_id) ic2 ON ic2.fc_id = f1.fc_id ''
			+ '' LEFT OUTER JOIN (SELECT fc_id, count(fc_id) as count_fc_id FROM invoice_case ic, invoice i WHERE i.invoice_id = ic.invoice_id AND ic.invoice_case_pmt_amt IS NOT NULL AND i.funding_source_id ='' + cast(@pi_funding_source_id as varchar(10)) + '' GROUP BY ic.fc_id) ic3 ON ic3.fc_id = f1.fc_id ''
			+ '' LEFT OUTER JOIN funding_source fs ON f1.funding_source_id = fs.funding_source_id ''
			+ '' INNER JOIN servicer s ON f1.servicer_id = s.servicer_id ''
			+ '' WHERE f1.never_bill_reason_cd IS NULL AND (ic3.count_fc_id IS NULL OR  ic3.count_fc_id = 0) ''
			+ '' AND (isnull(ic1.count_fc_id, 0) = isnull(ic2.count_fc_id, 0) + isnull(ic3.count_fc_id, 0) )'';

	IF (@pi_funding_source_id IS NOT NULL) 	AND (@pi_selected_servicer_option_1 IS NULL AND @pi_selected_servicer_option_2 IS NULL AND @pi_selected_servicer_option_3 IS NULL  AND @pi_selected_servicer_option_4 IS NULL AND @pi_selected_servicer_option_5 IS NULL)
		SELECT @v_sql = @v_sql +  '' AND f1.funding_source_id = '' + cast(@pi_funding_source_id as varchar(10));

	IF (@pi_selected_servicer_option_1 = 1 OR @pi_selected_servicer_option_2 = 1 OR @pi_selected_servicer_option_3 = 1 OR @pi_selected_servicer_option_4 = 1 OR @pi_selected_servicer_option_5 = 1)
	BEGIN
		SELECT @v_sql = @v_sql +  '' AND ( (0=1) '';
		IF @pi_selected_servicer_option_1 = 1
			SELECT @v_sql = @v_sql	+  '' OR (SELECT count(fc_id) FROM invoice_case ic''
									+ '' WHERE ic.fc_id = f1.fc_id AND s.funding_agreement_ind = ''''Y'''' AND ic.pmt_reject_reason_cd <> ''''FRED'''')> 0'';
		IF @pi_selected_servicer_option_2 = 1
			SELECT @v_sql = @v_sql	+  '' OR (SELECT count(fc_id) FROM invoice_case ic ''
									+  '' WHERE ic.fc_id = f1.fc_id AND s.funding_agreement_ind = ''''Y'''' AND pmt_reject_reason_cd = ''''FRED'''')>0'';	
		IF @pi_selected_servicer_option_3 = 1
			SELECT @v_sql = @v_sql	+  '' OR (SELECT count(fc_id) FROM invoice_case ic INNER JOIN invoice i ON i.invoice_id = ic.invoice_id ''
									+  '' WHERE ic.fc_id = f1.fc_id AND i.funding_source_id = 135 AND pmt_reject_reason_cd = ''''FREDDUPE'''') >0 '';
		IF @pi_selected_servicer_option_4 = 1
			SELECT @v_sql = @v_sql	+  '' OR (s.funding_agreement_ind = ''''N'''') '';
		IF @pi_selected_servicer_option_5 = 1
			SELECT @v_sql = @v_sql +  '' OR( (SELECT count(fc_id) FROM invoice_case ic WHERE ic.fc_id = f1.fc_id AND pmt_reject_reason_cd IS NOT NULL AND invoice_case_pmt_amt IS NULL)  '' 
								+ ''		= (SELECT count(fc_id) FROM invoice_case ic WHERE ic.fc_id = f1.fc_id )  )'';
		SELECT @v_sql = @v_sql +  '')''	;
	END;
	IF @pi_program_id IS NOT NULL 
		SELECT @v_sql = @v_sql + '' AND f1.program_id = '' + cast(@pi_program_id as varchar(10));

	IF (@pi_case_completed_ind = ''Y'')
		SELECT @v_sql = @v_sql +'' AND f1.completed_dt IS NOT NULL AND f1.completed_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(30)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(30)) + '''''''';
	ELSE IF (@pi_case_completed_ind = ''N'')
			SELECT @v_sql = @v_sql +'' AND f1.completed_dt IS NULL AND f1.create_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(30)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(30)) + '''''''';
		ELSE IF (@pi_case_completed_ind IS NULL)
			SELECT @v_sql = @v_sql	+'' AND (f1.create_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(30)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(30)) + ''''''''
									+'' OR f1.completed_dt BETWEEN '''''' + cast(@pi_start_dt as varchar(30)) + '''''' AND '''''' + cast(@pi_end_dt as varchar(30)) + '''''')'';
	IF @pi_duplicate_ind IS NOT NULL 
		SELECT @v_sql = @v_sql + '' AND f1.duplicate_ind = '''''' + cast(@pi_duplicate_ind as varchar(1)) + '''''''';

	IF (@pi_selected_servicer_option_1 IS NULL AND @pi_selected_servicer_option_2 IS NULL AND @pi_selected_servicer_option_3 IS NULL  AND @pi_selected_servicer_option_5 IS NULL)
		IF @pi_multiple_billing_ind = ''N''
			SELECT @v_sql = @v_sql	+ '' AND (ic1.count_fc_id IS NULL  OR ic1.count_fc_id = ic2.count_fc_id )'';	
		ELSE IF @pi_multiple_billing_ind = ''Y''
			SELECT @v_sql = @v_sql	+ '' AND (isnull(ic1.count_fc_id, 0) = isnull(ic2.count_fc_id, 0)) '' 

	IF @pi_funding_consent_ind = ''N''
		SELECT @v_sql = @v_sql + '' AND f1.funding_consent_ind = ''''Y'''''';  
	IF (@pi_servicer_consent_qty > 0 )
		SELECT @v_sql = @v_sql +  '' AND f1.servicer_consent_ind = ''''Y'''''';

	IF @pi_gender_cd IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.gender_cd = '''''' + cast(@pi_gender_cd as varchar(15))+ '''''''';
	IF @pi_race_cd IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.race_cd = '''''' + cast(@pi_race_cd as varchar(15))+ '''''''';
	IF @pi_hispanic_ind IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.hispanic_ind = '''''' + cast(@pi_hispanic_ind as varchar(1))+ '''''''';
	IF @pi_household_cd IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.household_cd= '''''' + cast(@pi_household_cd as varchar(15))+ '''''''';
	IF @pi_state_cd IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.prop_state_cd = '''''' + cast(@pi_state_cd as varchar(15))+ '''''''';
	IF @pi_city IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.prop_city = '''''' + cast(@pi_city as varchar(30))+ '''''''';
	IF @pi_min_gross_annual_income IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.household_gross_annual_income_amt >= '' + cast(@pi_min_gross_annual_income as varchar(15));
	IF @pi_max_gross_annual_income IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.household_gross_annual_income_amt <= '' + cast(@pi_max_gross_annual_income as varchar(15));
	IF @pi_min_age IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.age >='' + cast(@pi_min_age as varchar(3))
	IF @pi_max_age IS NOT NULL
		SELECT @v_sql = @v_sql + '' AND f1.age <='' + cast(@pi_max_age as varchar(3))
	SELECT @v_sql = @v_sql + '' ORDER BY f1.completed_dt ASC, f1.create_dt ASC '';

	Print @v_sql;
	EXECUTE (@v_sql);
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_case_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		QuyenNguyen
-- Create date: 12 Feb 2009
-- Description:	Update acency payable case
--	If @pi_update_flag = 0 => Update "Takeback Marked Cases"
--	If @pi_update_flag = 1 => Update "Pay/Unpay Marked Cases"
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_case_update]
	@pi_update_flag		int 
	,@pi_str_agency_payable_case_id varchar(2000)
	,@pi_takeback_pmt_reason_cd varchar(15) 
	,@pi_chg_lst_dt datetime 
	,@pi_chg_lst_user_id varchar(30) 
	,@pi_chg_lst_app_name varchar(20)
AS
DECLARE @sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
--	Update "Takeback Marked Cases"
	IF @pi_update_flag = 0
	BEGIN
		IF @pi_takeback_pmt_reason_cd <>'''' AND @pi_takeback_pmt_reason_cd IS NOT NULL
			SELECT @sql =''UPDATE	agency_payable_case ''
			+ '' SET		takeback_pmt_identified_dt = getdate()''
					+ '',takeback_pmt_reason_cd = '''''' + @pi_takeback_pmt_reason_cd + ''''''''
					+ '',chg_lst_dt = '''''' + cast(@pi_chg_lst_dt as varchar(20)) + ''''''''
					+ '',chg_lst_user_id = '''''' + @pi_chg_lst_user_id + ''''''''
					+ '',chg_lst_app_name ='''''' + @pi_chg_lst_app_name + ''''''''
			+ '' WHERE agency_payable_case_id IN ('' + @pi_str_agency_payable_case_id  + '')'';

		IF @pi_takeback_pmt_reason_cd ='''' OR @pi_takeback_pmt_reason_cd IS NULL
			SELECT @sql =''UPDATE	agency_payable_case ''
			+ '' SET		takeback_pmt_identified_dt = null''
					+ '',takeback_pmt_reason_cd = null''
					+ '',chg_lst_dt = '''''' + cast(@pi_chg_lst_dt as varchar(20)) + ''''''''
					+ '',chg_lst_user_id = '''''' + @pi_chg_lst_user_id + ''''''''
					+ '',chg_lst_app_name ='''''' + @pi_chg_lst_app_name + ''''''''
			+ '' WHERE agency_payable_case_id IN ('' + @pi_str_agency_payable_case_id  + '')'';
		EXECUTE (@sql);		
	END;
	
--	Update "Pay Marked Cases"
	IF @pi_update_flag = 1 
	BEGIN
		SELECT @sql = ''UPDATE	agency_payable_case''
		+ '' SET		agency_payable_case.chg_lst_dt ='''''' + cast( @pi_chg_lst_dt as varchar(20)) + ''''''''
		+ ''		,agency_payable_case.chg_lst_user_id = '''''' +  @pi_chg_lst_user_id + ''''''''
		+ ''		,agency_payable_case.chg_lst_app_name = '''''' + @pi_chg_lst_app_name + ''''''''
		+ ''  	,agency_payable_case.NFMC_difference_paid_amt = ar.nfmc_upcharge_pmt_rate''
		+ '' FROM	agency_payable_case ''
		+ '' INNER JOIN agency_payable a ON agency_payable_case.agency_payable_id = a.agency_payable_id''
		+ '' INNER JOIN foreclosure_case f ON agency_payable_case.fc_id = f.fc_id  ''
		+ ''		LEFT OUTER JOIN agency_rate ar ON a.agency_id = ar.agency_id AND f.program_id = ar.program_id''
		+ ''						AND isnull(f.completed_dt, f.create_dt) BETWEEN ar.eff_dt AND isnull(ar.exp_dt, getdate()) ''
		+ '' WHERE	agency_payable_case.agency_payable_case_id IN ('' + @pi_str_agency_payable_case_id  + '')''
		+ ''			AND NFMC_difference_paid_amt IS NULL;'';
		EXECUTE  (@sql);
	END;

--	Update "Unpay Marked Cases"
	IF @pi_update_flag = 2
	BEGIN		
		SELECT @sql = ''UPDATE	agency_payable_case''
				+ '' SET		agency_payable_case.chg_lst_dt = '''''' + cast(@pi_chg_lst_dt as varchar(20)) + ''''''''
				+ ''			,agency_payable_case.chg_lst_user_id = '''''' + @pi_chg_lst_user_id +''''''''
				+ ''			,agency_payable_case.chg_lst_app_name = '''''' + @pi_chg_lst_app_name + ''''''''
	  			+ ''			,agency_payable_case.NFMC_difference_paid_amt = NULL''
				+ '' FROM	agency_payable_case ''
				+ ''	WHERE	agency_payable_case.agency_payable_case_id IN ('' + @pi_str_agency_payable_case_id + '')''
				+ ''			AND NFMC_difference_paid_amt IS NOT NULL'';
		EXECUTE  (@sql);
	END;
END





' 
END
GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_decryption]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	Decryption a string
-- =============================================
CREATE function [dbo].[hpf_decryption](@s VARCHAR(1024), @k VARCHAR(16)) 
returns  VARCHAR(255)
as
BEGIN
--Returns a string decrypted with key k ( TEA encryption )
--TEA- Tiny Encryption Algorithm, copyrightDavid J Wheeler & Roger M Needham
DECLARE @result VARCHAR(1024), @l bigint, @i bigint, @j bigint, @temp bigint, @x bigint
declare @y bigint, @z bigint, @sum bigint, @delta bigint, @n bigint, @q bigint
declare @k0 bigint, @k1 bigint, @k2 bigint, @k3 bigint
declare @v0 bigint, @v1 bigint, @tvb VARBINARY(4), @t1 bigint, @t2 bigint, @t3 bigint
SET @i=LEN(@k)
IF @i<16--if the pwd<16 char
	BEGIN
	SET @k=@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k--add pwd to itself
	SET @k=LEFT(@k,16)
	END
SET @l=(LEN(@s) % 8)
IF @l<>0--if there are no complete 64 bit blocks
	BEGIN
	SET @i=(LEN(@s))/8+1
	SET @l= @i*8-len(@s)
	SET @s=@s+replicate(CHAR(0),@l)
	END
SET @k0=ASCII(SUBSTRING(@k, 1,1))*16777216+ASCII(SUBSTRING(@k, 2,1))*65536+
ASCII(SUBSTRING(@k, 3, 1))*256+ASCII(SUBSTRING(@k ,4, 1))
SET @k1=ASCII(SUBSTRING(@k, 5, 1))*16777216+ASCII(SUBSTRING(@k ,6, 1))*65536+
ASCII(SUBSTRING(@k, 7, 1))*256+ASCII(SUBSTRING(@k, 8, 1))
SET @k2=ASCII(SUBSTRING(@k, 1,9))*16777216+ASCII(SUBSTRING(@k ,10, 1))*65536+
ASCII(SUBSTRING(@k, 11, 1))*256+ASCII(SUBSTRING(@k ,12, 1))
SET @k3=ASCII(SUBSTRING(@k, 1,13))*16777216+ASCII(SUBSTRING(@k ,14, 1))*65536+
ASCII(SUBSTRING(@k, 15, 1))*256+ASCII(SUBSTRING(@k ,16, 1))
SET @i=1
SET @result=''''
WHILE @i<=LEN(@s)
	BEGIN
	SET @v0=convert(bigint,(ASCII(SUBSTRING(@s,(@i-1)+ 1,1))))*16777216+
convert(bigint,(ASCII(SUBSTRING(@s,(@i-1)+ 2,1))))*65536+
	convert(bigint,(ASCII(SUBSTRING(@s, (@i-1)+3, 1))))*256+
convert(bigint,(ASCII(SUBSTRING(@s ,(@i-1)+4, 1))))
	SET @v1=convert(bigint,(ASCII(SUBSTRING(@s,(@i-1)+ 5, 1))))*16777216+
convert(bigint,(ASCII(SUBSTRING(@s ,(@i-1)+6, 1))))*65536+
	convert(bigint,(ASCII(SUBSTRING(@s, (@i-1)+7, 1))))*256+
convert(bigint,(ASCII(SUBSTRING(@s, (@i-1)+8, 1))))

	set @delta=2654435769
	 SET @sum=3337565984
	set @n=32
	SET @y=@v0
	SET @z=@v1
	WHILE @n>0
		BEGIN

set @t1= (convert(bigint,(@y*16)) +@k2) & convert(bigint,4294967295)
set @t2= (convert(bigint,(@y+@sum)) & convert(bigint,4294967295))
set @t3= (convert(bigint,(@y/32)) +@k3) & convert(bigint,4294967295)
SET @z=@z- (convert(bigint,(@t1^@t2^@t3))) & convert(bigint,4294967295)
SET @z=convert(bigint,@z) & convert(bigint,4294967295)
--print str(@y)+''    ''+str(@t1)+''    ''+str(@t2)+''    ''+str(@t3)+''    ''+str(@z)

set @t1= (convert(bigint,(@z*16)) +@k0) & convert(bigint,4294967295)
set @t2= (convert(bigint,(@z+@sum)) & convert(bigint,4294967295))
set @t3= (convert(bigint,(@z/32)) +@k1) & convert(bigint,4294967295)
SET @y=@y- (convert(bigint,(@t1^@t2^@t3))) & convert(bigint,4294967295)
SET @y=convert(bigint,@y) & convert(bigint,4294967295)

SET @sum=@sum-@delta
	set @n=@n-1
		END
	SET @v0=@y
	SET @v1=@z	
	SET @tvb=CONVERT(VARBINARY(4),@v0)
	SET @result= @result+CONVERT(varchar(4),@tvb)
	SET @tvb=CONVERT(VARBINARY(4),@v1)
	SET @result= @result+CONVERT(varchar(4),@tvb)
	SET @i=@i+8
	END
	SET @result=REPLACE(@result,CHAR(0),'''')

RETURN   @result
END








' 
END

GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_encryption]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	Encryption a string
-- =============================================
CREATE function [dbo].[hpf_encryption](@s VARCHAR(1024), @k VARCHAR(16)) 
returns  VARCHAR(255)
as
BEGIN
--Returns a string encrypted with key k ( TEA encryption )
--TEA- Tiny Encryption Algorithm, copyrightDavid J Wheeler & Roger M Needham
DECLARE @result VARCHAR(1024), @l bigint, @i bigint, @j bigint, @temp bigint, @x bigint
declare @y bigint, @z bigint, @sum bigint, @delta bigint, @n bigint, @q bigint
declare @k0 bigint, @k1 bigint, @k2 bigint, @k3 bigint
declare @v0 bigint, @v1 bigint, @tvb VARBINARY(4), @t1 bigint, @t2 bigint, @t3 bigint
SET @i=LEN(@k)
IF @i<16--if the pwd<16 char
	BEGIN
	SET @k=@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k+@k--add pwd to itself
	SET @k=LEFT(@k,16)
	END
SET @l=(LEN(@s) % 8)
IF @l<>0--if there are no complete 64 bit blocks
	BEGIN
	SET @i=(LEN(@s))/8+1
	SET @l= @i*8-len(@s)
	SET @s=@s+replicate(CHAR(0),@l)
	END
SET @k0=ASCII(SUBSTRING(@k, 1,1))*16777216+ASCII(SUBSTRING(@k, 2,1))*65536+
ASCII(SUBSTRING(@k, 3, 1))*256+ASCII(SUBSTRING(@k ,4, 1))
SET @k1=ASCII(SUBSTRING(@k, 5, 1))*16777216+ASCII(SUBSTRING(@k ,6, 1))*65536+
ASCII(SUBSTRING(@k, 7, 1))*256+ASCII(SUBSTRING(@k, 8, 1))
SET @k2=ASCII(SUBSTRING(@k, 1,9))*16777216+ASCII(SUBSTRING(@k ,10, 1))*65536+
ASCII(SUBSTRING(@k, 11, 1))*256+ASCII(SUBSTRING(@k ,12, 1))
SET @k3=ASCII(SUBSTRING(@k, 1,13))*16777216+ASCII(SUBSTRING(@k ,14, 1))*65536+
ASCII(SUBSTRING(@k, 15, 1))*256+ASCII(SUBSTRING(@k ,16, 1))

SET @i=1
SET @result=''''
WHILE @i<=LEN(@s)
	BEGIN
	SET @v0=ASCII(SUBSTRING(@s,(@i-1)+ 1,1))*16777216+ASCII(SUBSTRING(@s,(@i-1)+ 2,1))*65536+
	ASCII(SUBSTRING(@s, (@i-1)+3, 1))*256+ASCII(SUBSTRING(@s ,(@i-1)+4, 1))
	SET @v1=ASCII(SUBSTRING(@s,(@i-1)+ 5, 1))*16777216+ASCII(SUBSTRING(@s ,(@i-1)+6, 1))*65536+
	ASCII(SUBSTRING(@s, (@i-1)+7, 1))*256+ASCII(SUBSTRING(@s, (@i-1)+8, 1))

	set @delta=2654435769
	 SET @sum=0
	set @n=32
	SET @y=@v0
	SET @z=@v1
	WHILE @n>0
		BEGIN
SET @sum=@sum+@delta
set @t1= (convert(bigint,(@z*16)) +@k0) & convert(bigint,4294967295)
set @t2= (convert(bigint,(@z+@sum)) & convert(bigint,4294967295))
set @t3= (convert(bigint,(@z/32)) +@k1) & convert(bigint,4294967295)
SET @y=@y+ (convert(bigint,(@t1^@t2^@t3))) & convert(bigint,4294967295)
SET @y=convert(bigint,@y) & convert(bigint,4294967295)

set @t1= (convert(bigint,(@y*16)) +@k2) & convert(bigint,4294967295)
set @t2= (convert(bigint,(@y+@sum)) & convert(bigint,4294967295))
set @t3= (convert(bigint,(@y/32)) +@k3) & convert(bigint,4294967295)
SET @z=@z+ (convert(bigint,(@t1^@t2^@t3))) & convert(bigint,4294967295)
SET @z=convert(bigint,@z) & convert(bigint,4294967295)

	set @n=@n-1
		END
	SET @v0=@y
	SET @v1=@z	

	SET @tvb=CONVERT(VARBINARY(4),@v0)
	SET @result= @result+CONVERT(varchar(4),@tvb)
	SET @tvb=CONVERT(VARBINARY(4),@v1)
	SET @result= @result+CONVERT(varchar(4),@tvb)
	SET @i=@i+8
	END
--IF @l<>0
--	SET @result=LEFT(@result,LEN(@result)-@l)
return  @result
END






' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_Invoice_case_validate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 25 Feb 2009
-- Project : HPF 
-- =============================================
CREATE PROCEDURE [dbo].[hpf_Invoice_case_validate]
	@pi_XMLDOC				varchar(8000)
	, @pi_funding_source_id int
AS
declare @xml_hndl int 
CREATE TABLE #temp_invoice_case 
	(row_index			int
	, fc_id				int
	, invoice_case_id	int
	, invoice_case_pmt_amt	numeric(15,2)
	, reject_reason_code	varchar(15)
	, error_msg				varchar(100))
BEGIN
/* Step 1: prepare the XML Document by executing a system stored procedure */
	EXEC sp_xml_preparedocument @xml_hndl OUTPUT, @pi_XMLDOC 
/* Step 2: insert into temp table */
	INSERT INTO #temp_invoice_case (row_index, fc_id, invoice_case_id, invoice_case_pmt_amt, reject_reason_code)
		SELECT	row_index, fc_id, invoice_case_id, invoice_case_pmt_amt,reject_reason_code
		FROM	OPENXML(@xml_hndl, ''/invoice_cases/invoice_case'', 1)
				WITH(row_index			int
					, fc_id				int
					, invoice_case_id	int 
					, invoice_case_pmt_amt	numeric(15,2)
					, reject_reason_code	varchar(15)); 					
	UPDATE	#temp_invoice_case 	SET		fc_id = NULL	WHERE	fc_id = '''';
	UPDATE	#temp_invoice_case	SET		invoice_case_id = NULL	WHERE	invoice_case_id = '''';

/* Step 3: Validate business rules	*/

	/* IF Invoice_case_id is provided */
	/* - Invoice_case_id not exist : Error_msg = ERR0670 */
	UPDATE	#temp_invoice_case
	SET		#temp_invoice_case.error_msg = isnull(error_msg, '' '') + ''ERR0670,''
	FROM	invoice_case ic LEFT OUTER JOIN #temp_invoice_case ON ic.invoice_case_id = #temp_invoice_case.invoice_case_id
	WHERE	#temp_invoice_case.invoice_case_id IS NOT NULL
			AND #temp_invoice_case.invoice_case_id IS NULL;
	/* - Invoice_case_id exists but fc_id is invalid : Error_msg = ERR0680*/
	UPDATE	#temp_invoice_case
	SET		error_msg = isnull(error_msg, '' '') + ''ERR0680,''
	FROM	#temp_invoice_case, invoice_case ic
	WHERE	#temp_invoice_case.invoice_case_id IS NOT NULL
			AND #temp_invoice_case.invoice_case_id = ic.invoice_case_id 
			AND #temp_invoice_case.fc_id <> ic.fc_id;
	/* - Invoice_case_id exists, fc_id exists but funding_cource_id is invalid : Error_msg = ERR0681 */
	UPDATE	#temp_invoice_case
	SET		error_msg = isnull(error_msg, '' '') + ''ERR0681,''
	FROM	#temp_invoice_case, invoice_case ic, invoice i
	WHERE	#temp_invoice_case.invoice_case_id IS NOT NULL
			AND #temp_invoice_case.invoice_case_id = ic.invoice_case_id 
			AND #temp_invoice_case.fc_id = ic.fc_id
			AND ic.invoice_id = i.invoice_id
			AND	i.funding_source_id <> @pi_funding_source_id;

	/* ELSE Invoice_case_id is not provided */
	/* - fc_id for funding_cource is invalid: Error_msg = ERR0682*/
	UPDATE	#temp_invoice_case
	SET		error_msg = isnull(error_msg, '' '') + ''ERR0682,''
	FROM	#temp_invoice_case, invoice_case ic, invoice i
	WHERE	#temp_invoice_case.invoice_case_id IS NULL
			AND ic.invoice_id = i.invoice_id
			AND	#temp_invoice_case.fc_id = ic.fc_id
			AND i.funding_source_id <> @pi_funding_source_id;

	/* ELSE Invoice_case_id AND fc_id are not provided : Error_msg = ERR0678 */
	UPDATE	#temp_invoice_case
	SET		error_msg = isnull(error_msg, '' '') + ''ERR0678,''
	FROM	#temp_invoice_case, invoice_case ic, invoice i
	WHERE	#temp_invoice_case.invoice_case_id IS NULL 
			AND #temp_invoice_case.fc_id IS NULL;	
		
		
	/* IF Invoice_case_id is provided */
	/* - Invoice is existed and CANCELLED		: Error_msg = ERR0671*/
	UPDATE	#temp_invoice_case
	SET		error_msg = isnull(error_msg, '' '') + ''ERR0671,''
	WHERE	invoice_case_id IN (SELECT invoice_case_id FROM invoice_case ic INNER JOIN invoice i ON ic.invoice_id = i.invoice_id AND i.status_cd = ''CANCELLED'');
	
	/* - invoice_case_pmt_amt <> invoice_case_bill_amt  : Error_msg =  ERR0672*/
	UPDATE	#temp_invoice_case	
	SET		error_msg = isnull(error_msg, '' '') + ''ERR0672,''
	WHERE	EXISTS 
			(SELECT invoice_case_id FROM invoice_case 
			WHERE invoice_case.invoice_case_id = #temp_invoice_case.invoice_case_id
				AND invoice_case.invoice_case_bill_amt <> #temp_invoice_case.invoice_case_pmt_amt
				AND #temp_invoice_case.reject_reason_code='''');
	
	SELECT row_index, invoice_case_id, ltrim(substring(error_msg, 1, len(error_msg)-1)) as error_code 
	FROM #temp_invoice_case;

	-- Step 4: Remove XML document
	EXEC sp_xml_removedocument @xml_hndl ;
	DROP TABLE  #temp_invoice_case;
END; 

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Sinh Tran
-- Create date: 19 Jan 2008
-- Project : HPF 
-- Build 
-- Description:	Update HPFUser
-- =============================================
CREATE PROCEDURE [dbo].[hpf_hpf_user_update]
	-- Add the parameters for the stored procedure here
	 @pi_user_login_id varchar(30)
    ,@pi_password varchar(128)=NULL
    ,@pi_active_ind varchar(1) =NULL          
    ,@pi_user_role_str_tbd varchar(30) = NULL
	,@pi_fname varchar(30)=NULL
	,@pi_lname varchar(30)=NULL
	,@pi_email varchar(50)=NULL
	,@pi_phone varchar(20)=NULL
	,@pi_last_login_dt datetime =NULL
	,@pi_create_dt datetime=NULL
	,@pi_create_user_id varchar(30)=NULL
	,@pi_create_app_name varchar(20)=NULL
	,@pi_chg_lst_dt datetime=NULL
	,@pi_chg_lst_user_id varchar(30)=NULL
	,@pi_chg_lst_app_name varchar(20)=NULL
    
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE hpf_user
   SET  
       [password] = @pi_password 
      ,[active_ind] = @pi_active_ind 
      ,[user_role_str_TBD] =@pi_user_role_str_tbd
      ,[fname] = @pi_fname 
      ,[lname] = @pi_lname 
      ,[email] = @pi_email 
      ,[phone] = @pi_phone 
      ,[last_login_dt] = @pi_last_login_dt 
      ,[create_dt] = @pi_create_dt 
      ,[create_user_id] = @pi_create_user_id 
      ,[create_app_name] = @pi_create_app_name 
      ,[chg_lst_dt] = @pi_chg_lst_dt
      ,[chg_lst_user_id] = @pi_chg_lst_user_id 
      ,[chg_lst_app_name] =@pi_chg_lst_app_name
     WHERE [user_login_id] = @pi_user_login_id
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 17 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Insert HPFUser
-- =============================================
CREATE PROCEDURE [dbo].[hpf_hpf_user_insert]	
	@pi_user_login_id varchar(30)
	, @pi_password varchar(128)
	, @pi_active_ind varchar(1)
	, @pi_user_role_str_TBD varchar(30)
	, @pi_fname varchar(30)
	, @pi_lname varchar(30)
	, @pi_email varchar(50)
	, @pi_phone varchar(20)
	, @pi_last_login_dt datetime
	, @pi_create_dt datetime
	, @pi_create_user_id varchar(30)
	, @pi_create_app_name varchar(20)
	, @pi_chg_lst_dt datetime
	, @pi_chg_lst_user_id varchar(30)
	, @pi_chg_lst_app_name varchar(20)    
	, @po_hpf_user_id int OUTPUT
AS
BEGIN
	INSERT INTO hpf_user(user_login_id, password,active_ind,user_role_str_TBD,fname,lname,email,phone,last_login_dt
			,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name)
	VALUES (@pi_user_login_id,@pi_password,@pi_active_ind,@pi_user_role_str_TBD,@pi_fname,@pi_lname,@pi_email,@pi_phone,@pi_last_login_dt
			,@pi_create_dt,@pi_create_user_id,@pi_create_app_name,@pi_chg_lst_dt,@pi_chg_lst_user_id,@pi_chg_lst_app_name);
	SET @po_hpf_user_id = SCOPE_IDENTITY();
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_get_from_login]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Sinh Tran
-- Create date: 01/08/2009
-- Project : HPF 
-- Build 
-- Description:	Get Web user with username provided
-- =============================================
CREATE PROCEDURE [dbo].[hpf_hpf_user_get_from_login] 	
	@pi_username varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT hpf_user_id, user_login_id, active_ind, user_role_str_TBD, fname, lname, email, phone, last_login_dt, create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name,password
	FROM hpf_user
	WHERE user_login_id = @pi_username
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_hpf_user_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		<Quyen Nguyen>
-- Create date: 11 Feb 2009
-- Description:	get all hpf_user
-- =============================================
CREATE PROCEDURE [dbo].[hpf_hpf_user_get]
	(@pi_hpf_user_id int = -1)
AS
BEGIN
	IF (@pi_hpf_user_id = -1)
		SELECT	hpf_user_id, user_login_id,password,active_ind
			,user_role_str_TBD
			,fname,lname
			,email,phone,last_login_dt
		FROM	hpf_user
		ORDER BY fname ASC;
	IF (@pi_hpf_user_id <> -1)	
		SELECT	hpf_user_id, user_login_id,password,active_ind
			,user_role_str_TBD
			,fname,lname
			,email,phone,last_login_dt
		FROM	hpf_user
		WHERE	hpf_user_id = @pi_hpf_user_id;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_audit_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		<Quyen Nguyen>
-- Create date: 11 Feb 2009
-- Description:	get all case_audit of a FC_id
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_audit_get]
	(@pi_fc_id int)
AS
BEGIN
	SELECT	case_audit_id, fc_id
			, audit_dt, audit_comment
			, reviewed_by, RTRIM(LTRIM((d.fullname))) reviewed_by_name
			, appropriate_outcome_ind,appropriate_reason_for_dflt_ind
			, complete_budget_ind
			, audit_type_cd, a.code_desc audit_type_cd_desc
			, client_action_plan_ind
			, verbal_privacy_consent_ind, written_privacy_consent_ind
			, compliant_ind
			, audit_failure_reason_cd,  b.code_desc audit_failure_reason_cd_desc
	FROM	case_audit c
			LEFT OUTER JOIN (SELECT user_login_id, (isnull(fname, '''') + '' '' + isnull(lname, '''')) fullname FROM hpf_user) d on c.reviewed_by = d.user_login_id
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name= ''audit type code'')  a ON  c.audit_type_cd = a.code
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name= ''audit failure reason code'')  b ON  c.audit_failure_reason_cd = b.code
	WHERE	fc_id = @pi_fc_id 			
	ORDER BY audit_dt desc
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
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		QUYEN Nguyen
-- Create date: 19 Feb 2009
-- Description:	Get all Program
-- =============================================
CREATE PROCEDURE [dbo].[hpf_program_get]
AS
BEGIN
		SELECT -1 as program_id, '' ALL'' as program_name
		UNION
		SELECT p.program_id, ltrim(p.program_name) as program_name	FROM program p
		ORDER BY program_name;	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_detail_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






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
			,fc.agency_id
			,call_id
			,fc.program_id
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
			,completed_dt
			,funding_consent_ind
			,servicer_consent_ind
			,agency_media_interest_ind
			,hpf_media_candidate_ind
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
			,fc.create_dt
			,fc.create_user_id
			,fc.create_app_name
			,fc.chg_lst_dt
			,fc.chg_lst_user_id
			,fc.chg_lst_app_name
			,counselor_fname
			,counselor_lname
			,intake_credit_score
			,intake_credit_bureau_cd
			,counselor_id_ref
			,fc_sale_dt
			,pc.code_desc as property_desc
			,gc.code_desc as gender_desc
			,rc.code_desc as race_desc
			,plc.code_desc as language_desc
			,elc.code_desc as education_desc
			,msc.code_desc as marital_desc
			,misc.code_desc as military_desc
			,cd.code_desc as counseling_desc
			,csc.code_desc as case_source_desc
			,sso.code_desc as summary_desc
			,drc1.code_desc as default_reason1_desc
			,drc2.code_desc as default_reason2_desc
			,hc.code_desc as household_desc
			,iec.code_desc as income_desc
			,cbc.code_desc as credit_desc
			,htr.code_desc as hud_termination_desc
			,hoc.code_desc as hud_outcome_desc
			,p.program_name
			
	FROM foreclosure_case fc
	
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''property code'')pc ON pc.code = fc.property_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''gender code'')gc ON gc.code=fc.gender_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''race code'')rc ON rc.code=fc.race_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''preferred language code'')plc ON plc.code=fc.borrower_preferred_lang_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''education level completed code'')elc ON elc.code=fc.borrower_educ_level_completed_cd
	LEFT OUTER JOIN( select code,code_desc from ref_code_item 
					where ref_code_set_name=''marital status code'')msc ON msc.code=fc.borrower_marital_status_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''military service code'')misc ON misc.code=fc.military_service_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''counseling duration code'')cd ON cd.code=fc.counseling_duration_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''case source code'')csc ON csc.code=fc.case_source_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''summary sent other code'')sso ON sso.code=fc.summary_sent_other_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''default reason code'')drc1 ON drc1.code=fc.dflt_reason_1st_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''default reason code'')drc2 ON drc2.code=fc.dflt_reason_2nd_cd				
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''household code'')hc ON hc.code=fc.household_cd
	LEFT OUTER JOIN( select code,code_desc from ref_code_item 
					where ref_code_set_name=''income earners code'')iec ON iec.code=fc.income_earners_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''credit bureau code'')cbc ON cbc.code=fc.intake_credit_bureau_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''hud termination reason code'')htr ON htr.code=fc.hud_termination_reason_cd
	LEFT OUTER JOIN(select code,code_desc from ref_code_item 
					where ref_code_set_name=''hud outcome code'')hoc ON hoc.code=fc.hud_outcome_cd
	INNER JOIN program p ON p.program_id=fc.program_id				
	WHERE fc_id = @pi_fc_id	
End


  
  
  
 
  

  
  
  
  
  











' 
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

    SELECT	cl.case_loan_id
			,cl.fc_id
			,cl.servicer_id
			,sv.servicer_name
			,cl.other_servicer_name
			,cl.acct_num
			,cl.loan_1st_2nd_cd
			,cl.mortgage_type_cd
			,cl.arm_reset_ind
			,cl.term_length_cd
			,cl.loan_delinq_status_cd
			,cl.current_loan_balance_amt
			,cl.orig_loan_amt
			,cl.interest_rate
			,cl.originating_lender_name
			,cl.orig_mortgage_co_FDIC_NCUA_num
			,cl.orig_mortgage_co_name
			,cl.orginal_loan_num
			,cl.current_servicer_FDIC_NCUA_num
			,cl.create_dt
			,cl.create_app_name
			,cl.create_user_id
			,cl.chg_lst_dt
			,cl.chg_lst_user_id
			,cl.chg_lst_app_name
			,cl.investor_loan_num 
			,cl.investor_num
			,cl.investor_name
			,cl.mortgage_program_cd
			,sv.secure_delivery_method_cd		
			,delinq.code_desc as delinq_code_desc
			,termlength.code_desc as term_code_desc
			,mortgagetype.code_desc as mortgage_type_code_desc
			,mortgageprogram.code_desc as mortgage_program_code_desc
    FROM Case_Loan cl
	LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''loan delinquency status code'') delinq
			ON delinq.code = cl.loan_delinq_status_cd
	LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''term length code'') termlength
			ON termlength.code = cl.term_length_cd
	LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''mortgage type code'') mortgagetype
			ON mortgagetype.code = cl.mortgage_type_cd
	LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''mortgage program code'') mortgageprogram
			ON mortgageprogram.code = cl.mortgage_program_cd
	LEFT JOIN servicer sv ON cl.servicer_id = sv.servicer_id
    WHERE fc_id = @pi_fc_id      
    ORDER BY loan_1st_2nd_cd, acct_num asc
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		QuyenNguyen
-- Create date: 12 Feb 2009
-- Description:	Update acency payable case
--	If @pi_update_flag = 0 => Update "Takeback Marked Cases"
--	If @pi_update_flag = 1 => Update "Pay/Unpay Marked Cases"
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_get]
	(@pi_agency_payable_id int = -1)
AS
BEGIN
	SELECT	ap.agency_id , a.agency_name, ap.period_start_dt, ap.period_end_dt
			, ap.agency_payable_id, ap.pmt_comment, ap.agency_payable_pmt_amt as Total_payable,a.sharepoint_foldername
	FROM	agency_payable ap, agency a
	
	WHERE	ap.agency_id = a.agency_id
			AND ap.agency_payable_id = @pi_agency_payable_id;

	SELECT	apc.agency_payable_case_id,apc.fc_id, f.agency_case_num, f.completed_dt, apc.pmt_amt,f.create_dt, l.acct_num, s.servicer_name
			, f.borrower_fname +'' ''+ isnull(f.borrower_mname, '' '')+'' ''+ f.borrower_lname as Borrower
			, apc.NFMC_difference_eligible_ind, NFMC_difference_paid_amt
			, apc.takeback_pmt_reason_cd, apc.takeback_pmt_identified_dt,tbr.code_desc as takeback_reason_desc
	FROM	agency_payable_case apc
	LEFT OUTER JOIN (select code,code_desc from ref_code_item where ref_code_set_name=''takeback reason code'') tbr ON tbr.code= apc.takeback_pmt_reason_cd
	INNER JOIN foreclosure_case f ON apc.fc_id=f.fc_id
	INNER JOIN case_loan l ON f.fc_id=l.fc_id
	INNER JOIN servicer s ON l.servicer_id=s.servicer_id
	
	WHERE	l.loan_1st_2nd_cd = ''1st''
			AND apc.agency_payable_id = @pi_agency_payable_id
			ORDER BY apc.agency_payable_case_id asc;
END;' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		<Quyen Nguyen>
-- Create date: 03 Feb 2009
-- Description:	get an Invoice and its invoice_cases
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_get]
	(
	@pi_invoice_id int
	)
AS
BEGIN
	SELECT	fs.funding_source_name, fs.funding_source_id, i.Invoice_id
			, i.period_start_dt, i.period_end_dt						
			, i.invoice_bill_amt, i.invoice_pmt_amt, i.invoice_comment,i.status_cd,i.invoice_dt
			, i.create_dt,i.create_user_id,i.create_app_name,i.chg_lst_dt,i.chg_lst_user_id,i.chg_lst_app_name
	FROM	Invoice i, funding_source fs 			
	WHERE	i.funding_source_id = fs.funding_source_id
			AND i.invoice_id = @pi_invoice_id;

	SELECT	ic.invoice_case_id,ic.fc_id, f.agency_case_num, f.completed_dt, ic.invoice_case_bill_amt
			, l.acct_num, s.servicer_name
			, rtrim(ltrim(f.borrower_fname + '' '' + isnull(f.borrower_mname, '' '')+ '' ''  + f.borrower_lname)) as borrower_name
			, ip.pmt_dt, ic.invoice_case_pmt_amt
			, rci.code_desc as pmt_reject_reason_cd
			, l.investor_loan_num
			, ic.create_dt,ic.create_user_id,ic.create_app_name,ic.chg_lst_dt,ic.chg_lst_user_id,ic.chg_lst_app_name
			, f.create_dt as foreclosure_case_create_dt
	FROM	invoice_case ic LEFT OUTER JOIN invoice_payment ip ON ic.invoice_payment_id = ip.invoice_payment_id 
			LEFT OUTER JOIN (select code, code_desc  from ref_code_item where ref_code_set_name = ''payment reject reason code'') rci ON rci.code = ic.pmt_reject_reason_cd
			, foreclosure_case f, case_loan l, servicer s
	WHERE	ic.fc_id = f.fc_id 
			AND f.fc_id = l.fc_id		
			AND l.servicer_id = s.servicer_id
			AND l.loan_1st_2nd_cd = ''1ST''
			AND ic.invoice_id = @pi_invoice_id
	ORDER BY f.completed_dt, f.create_dt ASC;
END






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_check_foreign_key]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

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
	@pi_selected_agency_id int = 0,

	@po_call_center_id int OUTPUT,
	@po_servicer_id int OUTPUT,
	@po_prev_agency_id int OUTPUT,
	@po_selected_agency_id int OUTPUT
AS

Begin
	Select @po_call_center_id = call_center_id from call_center where call_center_id = @pi_call_center_id
	Select @po_servicer_id = servicer_id  from servicer where servicer_id = @pi_servicer_id
	Select @po_prev_agency_id = agency_id from Agency where agency_id = @pi_prev_agency_id
	Select @po_selected_agency_id = agency_id from Agency where agency_id = @pi_selected_agency_id
End




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_servicer_get_from_FcId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







-- =============================================
-- Author:		Khoa Do
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get case loan (WEB Service)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_servicer_get_from_FcId]
	@pi_fc_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT	cl.fc_id
			,sv.servicer_id
			,sv.servicer_name
			,sv.contact_fname
			,sv.contact_lname
			,sv.contact_email	
			,sv.phone
			,sv.fax	
			,sv.active_ind
			,sv.funding_agreement_ind
			,sv.secure_delivery_method_cd	
			,sv.couseling_sum_format_cd			
			,sv.sharepoint_foldername
    FROM Case_Loan cl
	LEFT JOIN servicer sv ON cl.servicer_id = sv.servicer_id
    WHERE fc_id = @pi_fc_id      
END









' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_detail_get_call]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		PD
-- Create date: Mar 16 2009
-- Project : HPF 
-- Build 
-- Description:	Get case from call_id
-- History:
-- Date: Mar 18 2009 by Quyen. Fix redundant results with outcomes
-- =============================================

CREATE PROCEDURE [dbo].[hpf_foreclosure_case_detail_get_call]
				@pi_call_id int
As 
Begin
	SELECT	distinct fc.fc_id,
			sv.servicer_name,
			ln.acct_num,
			fc.prop_zip,
			fc.borrower_fname,
			fc.borrower_lname,
			fc.counselor_fname,
			fc.counselor_lname,
			a.agency_name,
			fc.counselor_phone,
			fc.counselor_ext,
			fc.counselor_email,
			max(oi.outcome_dt)outcome_dt,
			ot.outcome_type_name
			
	FROM
		foreclosure_case fc,
		case_loan ln,
		servicer sv,
		(select oi1.outcome_item_ID, outcome_type_id, fc_id, outcome_dt 
		from outcome_item oi1
			,( SELECT max(outcome_item_ID)  outcome_item_ID FROM outcome_item WHERE outcome_deleted_dt IS NULL GROUP BY fc_id) oi2
		WHERE	oi1.outcome_item_ID = oi2.outcome_item_ID 
		)oi,
		outcome_type ot,
		agency a	
	WHERE	ln.fc_id = fc.fc_id 
			AND ln.servicer_id = sv.servicer_id
			AND ln.loan_1st_2nd_cd = ''1ST''
			AND oi.fc_id = fc.fc_id
			AND oi.outcome_type_id = ot.outcome_type_id
			AND a.agency_id = fc.agency_id
			AND call_id = @pi_call_id --538		
	GROUP BY fc.fc_id,
			fc.prop_zip, 
			fc.borrower_fname,
			fc.borrower_lname,			
			fc.counselor_fname,
			fc.counselor_lname,
			fc.agency_id,
			fc.counselor_phone,
			fc.counselor_ext,
			fc.counselor_email,
			sv.servicer_name,
			ln.acct_num,
			ot.outcome_type_name,
			a.agency_name
End


  
  
  
 
  

  
  
  
  
  













' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_servicer_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Khoa Do
-- Create date: 09 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Get Servicer list
-- =============================================
CREATE PROCEDURE [dbo].[hpf_servicer_get]
	@pi_funding_source_id		int = null
	,@pi_servicer_id int = null	
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
	
	IF @pi_funding_source_id > 0
		SELECT	s.servicer_id,s.servicer_name 
		FROM	servicer s, funding_source_group fsg 
		WHERE	fsg.funding_source_id= @pi_funding_source_id
				AND fsg.servicer_id=s.servicer_id
		ORDER BY servicer_name;
	ELSE
		IF @pi_servicer_id > 0
			SELECT s.servicer_id,s.servicer_name FROM	servicer s 
			WHERE s.servicer_id = @pi_servicer_id
			ORDER BY servicer_name;
		ELSE 
			SELECT -1 as servicer_id, ''ALL'' as servicer_name
			UNION
			SELECT s.servicer_id,s.servicer_name FROM	servicer s ORDER BY servicer_name;
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_group_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_bar_permission]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

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
	WHERE		ms.hpf_user_id=@pi_userid 
			AND ms.menu_item_id=mi.menu_item_id
			AND mi.menu_group_id = mg.menu_group_id
	ORDER BY group_sort_order,item_sort_order
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
	@pi_fc_id int,
	@pi_get_all_indicator int = null
AS
DECLARE @v_sql varchar(8000);
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if (@pi_get_all_indicator is not null)
		SELECT	outcome_item_id,fc_id
				,OI.outcome_type_id, OT.outcome_type_name
				,OI.outcome_dt, OI.outcome_deleted_dt
				,OI.nonprofitreferral_key_num,OI.ext_ref_other_name
				,OI.create_dt,OI.create_user_id	,OI.create_app_name
				,OI.chg_lst_dt,OI.chg_lst_user_id,OI.chg_lst_app_name
		FROM	Outcome_Item OI, outcome_type OT
		WHERE	fc_id = @pi_fc_id
				AND OI.outcome_type_id = OT.outcome_type_id 
		ORDER BY OI.outcome_dt desc,OI.outcome_deleted_dt desc;
	if (@pi_get_all_indicator is null)
		SELECT	outcome_item_id,fc_id
				,OI.outcome_type_id, OT.outcome_type_name
				,OI.outcome_dt, OI.outcome_deleted_dt
				,OI.nonprofitreferral_key_num,OI.ext_ref_other_name
				,OI.create_dt,OI.create_user_id	,OI.create_app_name
				,OI.chg_lst_dt,OI.chg_lst_user_id,OI.chg_lst_app_name
		FROM	Outcome_Item OI, outcome_type OT
		WHERE	fc_id = @pi_fc_id
				AND OI.outcome_type_id = OT.outcome_type_id 
				AND outcome_deleted_dt IS null
		ORDER BY OI.outcome_dt desc,OI.outcome_deleted_dt desc;
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_type_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
	ORDER BY outcome_type_name asc
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_post_counseling_status_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		<Quyen Nguyen>
-- Create date: 11 Feb 2009
-- Description:	get all case_post_counseling_status of a FC_id
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_post_counseling_status_get]
	(@pi_fc_id int)
AS
BEGIN
	SELECT	case_post_counseling_status_id
			,a.fc_id
			,a.followup_dt
			,a.followup_comment
			,a.followup_source_cd
			,follow.code_desc as followup_source_cd_desc
			,a.loan_delinq_status_cd
			,delinq.code_desc as loan_delinq_status_cd_desc
			,a.still_in_house_ind
			,a.credit_score
			,a.credit_bureau_cd
			,credit.code_desc as credit_bureau_cd_desc
			,a.credit_report_dt
			,a.outcome_type_id
			,ot.outcome_type_name
	FROM	case_post_counseling_status a
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''follow up source code'') follow
			ON follow.code = a.followup_source_cd
			LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''loan delinquency status code'') delinq
			ON delinq.code = a.loan_delinq_status_cd
			LEFT OUTER JOIN(SELECT code, code_desc FROM ref_code_item 
			WHERE	ref_code_set_name = ''credit burreau code'') credit
			ON credit.code = a.credit_bureau_cd
			LEFT OUTER JOIN outcome_type ot	
			ON ot.outcome_type_id = a.outcome_type_id						
	WHERE	fc_id = @pi_fc_id
	ORDER BY a.followup_dt desc;
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_call_center_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Thien Nguyen
-- Create date: 12/09/2008
-- Project : HPF 
-- Build 
-- Description:	Get Call_Center Info
-- =============================================
CREATE PROCEDURE [dbo].[hpf_call_center_get] 	
	@pi_call_center_id int = 0	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT call_center_id, call_center_name
	FROM call_center
	WHERE call_center_id = @pi_call_center_id
	
   
END
' 
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
		@pi_completed_dt datetime = null,
		@pi_funding_consent_ind varchar(1) = null,
		@pi_servicer_consent_ind varchar(1) = null,
		@pi_agency_media_interest_ind varchar(1) = null,
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
-- Process in this procedure
--		1) Get *_last4_SSN
--		2) Encryption *_SSN
--		3) Calculate AMI_percentage
DECLARE @v_borrower_SSN varchar(255), @v_co_borrower_SSN varchar(255), @v_AMI_percentage numeric(18), @v_AMI_value numeric(18);
BEGIN
	SET NOCOUNT ON;
	SELECT @v_borrower_SSN = NULL, @v_co_borrower_SSN = NULL
	IF @pi_borrower_SSN IS NOT NULL
		SELECT	@v_borrower_SSN = dbo.hpf_encryption (@pi_borrower_SSN,''xyz123'');
	IF @pi_co_borrower_SSN IS NOT NULL
		SELECT	@v_co_borrower_SSN = dbo.hpf_encryption (@pi_co_borrower_SSN,''xyz123'');

	IF (@pi_household_gross_annual_income_amt IS NOT NULL)
	BEGIN
		SELECT TOP 1 @v_AMI_value = a.median_income
		FROM	geocode_ref g, area_median_income a 
		WHERE	g.county_fips = LEFT(a.fips,5) 
				AND g.msa_code = replace(a.msa,''9999'',''0000'')
				AND g.city_type = ''D'' 
				AND	g.zip_code = @pi_prop_zip
		ORDER BY a.median_income DESC;
		SELECT @v_AMI_percentage = cast((@pi_household_gross_annual_income_amt/@v_AMI_value)*100 as numeric(18));

	END;

	IF @pi_call_id = 0 	SELECT @pi_call_id = NULL;

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
       [AMI_percentage] = @v_AMI_percentage   ,  
       [counseling_duration_cd] = @pi_counseling_duration_cd  ,  
       [gender_cd] = @pi_gender_cd  ,  
       [borrower_fname] = @pi_borrower_fname  ,  
       [borrower_lname] = @pi_borrower_lname  ,  
       [borrower_mname] = @pi_borrower_mname  ,  
       [mother_maiden_lname] = @pi_mother_maiden_lname  ,  
       [borrower_ssn] = @v_borrower_SSN,
       [borrower_last4_SSN] = RIGHT(@pi_borrower_SSN, 4),  
       [borrower_DOB] = @pi_borrower_DOB   ,
       [co_borrower_fname] = @pi_co_borrower_fname  ,  
       [co_borrower_lname] = @pi_co_borrower_lname  ,  
       [co_borrower_mname] = @pi_co_borrower_mname  ,  
       [co_borrower_ssn] = @v_co_borrower_SSN,  
       [co_borrower_last4_SSN] = RIGHT(@pi_co_borrower_SSN, 4),  
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
       [completed_dt] = @pi_completed_dt   ,
       [funding_consent_ind] = @pi_funding_consent_ind  ,  
       [servicer_consent_ind] = @pi_servicer_consent_ind  ,  
       [agency_media_interest_ind] = @pi_agency_media_interest_ind  ,  
       [agency_success_story_ind] = @pi_agency_success_story_ind  ,  
       [borrower_disabled_ind] = @pi_borrower_disabled_ind  ,  
       [co_borrower_disabled_ind] = @pi_co_borrower_disabled_ind  ,  
       [summary_sent_other_cd] = @pi_summary_sent_other_cd  ,  
       [summary_sent_other_dt] = @pi_summary_sent_other_dt   ,
--       [summary_sent_dt] = @pi_summary_sent_dt   ,
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

-- Task 2:IF ccrc_sor_ind = ''Y'' then ccrc_sor_ind= ''N''
	UPDATE	foreclosure_case
	SET		ccrc_sor_ind = ''N''
	WHERE [fc_id] = @pi_fc_id AND ccrc_sor_ind = ''Y'';
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
EXEC dbo.sp_executesql @statement = N'













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
           @pi_dflt_reason_1st_cd varchar(15) = null,
           @pi_dflt_reason_2nd_cd varchar(15) = null,
           @pi_hud_termination_reason_cd varchar(15) = null,
           @pi_hud_termination_dt datetime = null,
           @pi_hud_outcome_cd varchar(15) = null,
           @pi_counseling_duration_cd varchar(15) = null,
           @pi_gender_cd varchar(15) = null,
           @pi_borrower_fname varchar(30) = null,
           @pi_borrower_lname varchar(30) = null,
           @pi_borrower_mname varchar(30) = null,
           @pi_mother_maiden_lname varchar(30) = null,
           @pi_borrower_ssn varchar(255) = null,
           @pi_borrower_last4_SSN varchar(4) = null,
           @pi_borrower_DOB datetime = null,
           @pi_co_borrower_fname varchar(30) = null,
           @pi_co_borrower_lname varchar(30) = null,
           @pi_co_borrower_mname varchar(30) = null,
           @pi_co_borrower_ssn varchar(255) = null,
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
           @pi_completed_dt datetime = null,
           @pi_funding_consent_ind varchar(1) = null,
           @pi_servicer_consent_ind varchar(1) = null,
           @pi_agency_media_interest_ind varchar(1) = null,
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
-- Process in this procedure
--		1) Get *_last4_SSN
--		2) Encryption *_SSN
--		3) Calculate AMI_percentage
DECLARE @v_borrower_SSN varchar(255), @v_co_borrower_SSN varchar(255), @v_AMI_percentage numeric(18), @v_AMI_value numeric(18);
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT @v_borrower_SSN = NULL, @v_co_borrower_SSN = NULL
	IF @pi_borrower_SSN IS NOT NULL
		SELECT	@v_borrower_SSN = dbo.hpf_encryption (@pi_borrower_SSN,''xyz123'');
	IF @pi_co_borrower_SSN IS NOT NULL
		SELECT	@v_co_borrower_SSN = dbo.hpf_encryption (@pi_co_borrower_SSN,''xyz123'');

	IF (@pi_household_gross_annual_income_amt IS NOT NULL)
	BEGIN
		SELECT TOP 1 @v_AMI_value = a.median_income 
		FROM	geocode_ref g, area_median_income a 
		WHERE	g.county_fips = LEFT(a.fips,5) 
				AND g.msa_code = replace(a.msa,''9999'',''0000'')
				AND g.city_type = ''D'' 
				AND	g.zip_code = @pi_prop_zip
		ORDER BY a.median_income DESC;
		SELECT @v_AMI_percentage = cast((@pi_household_gross_annual_income_amt/@v_AMI_value)*100 as numeric(18));

	END;


	IF @pi_call_id = 0 	SELECT @pi_call_id = NULL;
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
           ,[completed_dt]
           ,[funding_consent_ind]
           ,[servicer_consent_ind]
           ,[agency_media_interest_ind]
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
		   ,[chg_lst_app_name]
		   ,[opt_out_newsletter_ind]
		   ,[opt_out_survey_ind]
		   ,[do_not_call_ind])
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
           @pi_dflt_reason_1st_cd,
           @pi_dflt_reason_2nd_cd,
           @pi_hud_termination_reason_cd,
           @pi_hud_termination_dt,
           @pi_hud_outcome_cd,
           @v_AMI_percentage,
           @pi_counseling_duration_cd,
           @pi_gender_cd,
           @pi_borrower_fname,
           @pi_borrower_lname,
           @pi_borrower_mname,
           @pi_mother_maiden_lname,
           @v_borrower_SSN, 
           RIGHT(@pi_borrower_SSN, 4),
           @pi_borrower_DOB,
           @pi_co_borrower_fname,
           @pi_co_borrower_lname,
           @pi_co_borrower_mname,
--           dbo.hpf_encryption (@pi_co_borrower_SSN,''xyz123''),
		   @v_co_borrower_SSN ,
           RIGHT(@pi_co_borrower_SSN, 4),
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
           @pi_completed_dt,
           @pi_funding_consent_ind,
           @pi_servicer_consent_ind,
           @pi_agency_media_interest_ind,
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
			,''N'', ''N'', ''N''		
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	 Get agency
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_get] 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
    -- Get all agencies
	SELECT -1 agency_id , ''ALL'' agency_name
	UNION
	SELECT a.agency_id, a.agency_name FROM Agency a
	WHERE active_ind = ''Y'' 
	ORDER BY agency_name;
	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_detail_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_detail_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Thao Nguyen>
-- Create date: <Create Date,,>
-- Description:	<get Payment Information for accounting.>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_detail_get]
	(
	@pi_fc_id int
	)
AS
BEGIN
	SELECT	dbo.agency_payable.pmt_dt, dbo.Agency.agency_name, dbo.agency_payable.agency_payable_id, dbo.agency_payable.agency_payable_pmt_amt
			, dbo.agency_payable_case.NFMC_difference_eligible_ind,dbo.agency_payable_case.NFMC_difference_paid_amt
	FROM	dbo.agency_payable INNER JOIN
            dbo.agency_payable_case ON dbo.agency_payable.agency_payable_id = dbo.agency_payable_case.agency_payable_id INNER JOIN
            dbo.Agency ON dbo.agency_payable.agency_id = dbo.Agency.agency_id
	WHERE agency_payable_case.fc_id=@pi_fc_id
	ORDER BY dbo.agency_payable.pmt_dt desc
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_search]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Thao Nguyen
-- Create date: 12 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Search agency payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_search]
	@pi_agency_id		int = -1,
	@pi_start_dt				datetime = null,
	@pi_end_dt					datetime = null
AS
BEGIN
	SET NOCOUNT ON;
	IF @pi_agency_id <= 0
		SELECT	a.agency_name,a.agency_id, ap.agency_payable_id, ap.pmt_dt, ap.period_start_dt, 
                ap.period_end_dt, ap.agency_payable_pmt_amt, ap.status_cd, ap.pmt_comment,
                ap.agency_payable_pmt_amt,r.code_desc
		FROM	Agency a INNER JOIN agency_payable ap ON a.agency_id = ap.agency_id
		LEFT OUTER JOIN (select code,code_desc FROM ref_code_item WHERE ref_code_set_name=''agency payable status code'') r ON r.code=ap.status_cd
		WHERE	ap.period_start_dt >= @pi_start_dt AND ap.period_end_dt <= @pi_end_dt
		ORDER BY ap.period_start_dt desc,ap.period_end_dt desc;
	IF @pi_agency_id >0
		SELECT	a.agency_name,a.agency_id, ap.agency_payable_id, ap.pmt_dt, ap.period_start_dt, 
                ap.period_end_dt, ap.agency_payable_pmt_amt, ap.status_cd, ap.pmt_comment,
                ap.agency_payable_pmt_amt,r.code_desc
		FROM	Agency a INNER JOIN agency_payable ap ON a.agency_id = ap.agency_id
		LEFT OUTER JOIN (select code,code_desc FROM ref_code_item WHERE ref_code_set_name=''agency payable status code'') r ON r.code=ap.status_cd
		WHERE	ap.period_start_dt >= @pi_start_dt AND ap.period_end_dt <= @pi_end_dt
				AND a.agency_id = @pi_agency_id 
		ORDER BY ap.period_start_dt desc,ap.period_end_dt desc;
END;




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_view_budget_category_code]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Khoa Do
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
	WHERE bc.budget_category_cd IN (''INC'', ''EXP'')
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_detail_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get budget items and assets (Web APP)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_detail_get]
   @pi_budget_set_id int   
AS
BEGIN
	SELECT	c.budget_category_name as budget_category, s.budget_subcategory_name as budget_subcategory
			, i.budget_item_amt, i.budget_note
	FROM	budget_item i, budget_subcategory s, budget_category c
	WHERE	budget_set_id = @pi_budget_set_id
			AND i.budget_subcategory_id = s.budget_subcategory_id
			AND c.budget_category_id = s.budget_category_id
	ORDER BY c.sort_order, s.sort_order;

	SELECT	asset_name, asset_value
	FROM	budget_asset 
	WHERE	budget_set_id = @pi_budget_set_id
	ORDER BY asset_name;
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_search]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		<Quyen Nguyen>
-- Create date: 18 Feb 2009
-- Description: Search invoice_payment
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_payment_search]
	(
	@pi_funding_source_id int,
	@pi_start_dt datetime,
	@pi_end_dt datetime
	)
AS
DECLARE @v_sql varchar(8000);
BEGIN
	SET NOCOUNT ON;
	IF @pi_funding_source_id <= 0
		SELECT i.funding_source_id, fs.funding_source_name, i.invoice_payment_id, i.pmt_dt
				, i.pmt_dt
				, i.pmt_cd, rci.code_desc
				, i.pmt_amt,i.pmt_num
				, i.invoice_payment_comment
		FROM	invoice_payment i LEFT OUTER JOIN 
				(SELECT code, code_desc FROM ref_code_item i1 WHERE i1.ref_code_set_name = ''payment code'') rci ON rci.code= i.pmt_cd
				, funding_source fs 
		WHERE	i.funding_source_id = fs.funding_source_id 
				AND i.pmt_dt BETWEEN @pi_start_dt AND @pi_end_dt
		ORDER BY i.pmt_dt DESC;
	IF @pi_funding_source_id > 0
		SELECT i.funding_source_id, fs.funding_source_name, i.invoice_payment_id, i.pmt_dt
				, i.pmt_dt
				, i.pmt_cd, rci.code_desc
				, i.pmt_amt,i.pmt_num
				, i.invoice_payment_comment
		FROM	invoice_payment i LEFT OUTER JOIN 
				(SELECT code, code_desc FROM ref_code_item i1 WHERE i1.ref_code_set_name = ''payment code'') rci ON rci.code= i.pmt_cd
				, funding_source fs
		WHERE	i.funding_source_id = fs.funding_source_id
				AND i.pmt_dt BETWEEN @pi_start_dt AND @pi_end_dt
				AND i.funding_source_id = @pi_funding_source_id
		ORDER BY i.pmt_dt DESC;
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_Invoice_case_update_for_payment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'










-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb 2009
-- Project : HPF 
-- =============================================
CREATE PROCEDURE [dbo].[hpf_Invoice_case_update_for_payment]
	@pi_XMLDOC varchar(8000)
	,@pi_invoice_payment_id	int = -1  
	,@pi_chg_lst_dt			datetime = null
	,@pi_chg_lst_user_id	varchar(30) = null
	,@pi_chg_lst_app_name	varchar(20) = null	
AS
DECLARE @v_funding_source_id INT
declare @xml_hndl int 
CREATE TABLE #temp_invoice_case 
	(fc_id	int	
	, invoice_case_id			int
	, invoice_case_pmt_amt		numeric(15,2)	
	, reject_reason_cd			varchar(15)
	, investor_loan_num			varchar(30)
	, investor_num				varchar(30)
	, investor_name				varchar(50)	
	, Freddie_servicer_num		Varchar(30) 
	, NFMC_difference_eligible_ind varchar(1)
	, takeback_pmt_reason_cd	varchar(15)
	, takeback_pmt_identified_dt datetime
	);

BEGIN
	SELECT @v_funding_source_id = funding_source_id FROM invoice_payment WHERE invoice_payment_id = @pi_invoice_payment_id;
/* Step 1: prepare the XML Document by executing a system stored procedure */
	EXEC sp_xml_preparedocument @xml_hndl OUTPUT, @pi_XMLDOC 
/* Step 2: insert into temp table */
	INSERT INTO #temp_invoice_case (fc_id, invoice_case_id, invoice_case_pmt_amt,reject_reason_cd, investor_loan_num, investor_num, investor_name, Freddie_servicer_num)
		SELECT	fc_id, invoice_case_id, invoice_case_pmt_amt,reject_reason_cd, investor_loan_num, investor_num, investor_name, Freddie_servicer_num
		FROM	OPENXML(@xml_hndl, ''/invoice_cases/invoice_case'', 1)
				WITH(fc_id	int	
					, invoice_case_id int
					, invoice_case_pmt_amt numeric(15,2)	
					, reject_reason_cd varchar(15)
					, investor_loan_num	varchar(30)
					, investor_num	varchar(30)
					, investor_name	varchar(50)	
					, Freddie_servicer_num Varchar(30) 
					); 
					
/* Step 3: Save Processing*/
IF @pi_invoice_payment_id > 0 -- Update a payment
BEGIN
	
	/* 3.1 Clear all previous payment and re-calculate invoice_pmt_amt*/
		IF (SELECT count(invoice_payment_id) FROM invoice_case where invoice_payment_id  = @pi_invoice_payment_id) > 0 
		BEGIN
			/* Clear all previous payment*/
			UPDATE	invoice_case
			SET		invoice_payment_id = NULL, invoice_case_pmt_amt = NULL, pmt_reject_reason_cd = NULL
			WHERE	invoice_payment_id = @pi_invoice_payment_id;
			/* Re-calculate invoice_pmt_amt*/
			UPDATE	invoice
			SET		invoice.invoice_pmt_amt = ic.invoice_pmt_amt
			FROM	invoice INNER JOIN 
					(SELECT invoice_id, SUM(invoice_case_pmt_amt) as invoice_pmt_amt 
					FROM invoice_case WHERE invoice_payment_id = @pi_invoice_payment_id
					GROUP BY invoice_id
					) ic ON invoice.invoice_id = ic.invoice_id;
		END;
	/* 3.2 and 3.3 Process update #temp_invoice_case */
	/* 3.2 IF reject_reason_cd  IS NOT NULL */
		UPDATE	#temp_invoice_case 	SET		investor_loan_num = NULL	WHERE	len(ltrim(investor_loan_num)) = 0;
		UPDATE	#temp_invoice_case	SET		investor_num = NULL			WHERE	len(ltrim(investor_num)) = 0;
		UPDATE	#temp_invoice_case	SET		investor_name = NULL		WHERE	len(ltrim(investor_name)) = 0;

		UPDATE	#temp_invoice_case
		SET		#temp_invoice_case.invoice_case_id = ic.invoice_case_id
		FROM	#temp_invoice_case, invoice_case ic, invoice i
		WHERE	#temp_invoice_case.invoice_case_id = -1 
				AND #temp_invoice_case.fc_id = ic.fc_id 
				AND ic.invoice_id = i.invoice_id 
				AND i.funding_source_id = @v_funding_source_id;

		UPDATE	#temp_invoice_case	SET	reject_reason_cd = NULL WHERE	len(ltrim(reject_reason_cd)) = 0;

		UPDATE	#temp_invoice_case	SET	Freddie_servicer_num = NULL WHERE	len(ltrim(Freddie_servicer_num)) = 0;
		/* 3.2.1  reject_reason_cd IS NOT NULL, SET		invoice_case_pmt_amt = NULL */
		UPDATE	#temp_invoice_case
		SET		invoice_case_pmt_amt = NULL
		WHERE	reject_reason_cd IS NOT NULL;

		/* 3.2.2 If funding source is Freddie, ID= 135 THEN table agency_payable_case with the same fc_id and set NFMC_difference_eligible_ind = ''N'' */
		UPDATE	#temp_invoice_case
		SET		NFMC_difference_eligible_ind = ''N''
		FROM	#temp_invoice_case, invoice_case ic, invoice i
		WHERE	#temp_invoice_case.reject_reason_cd IS NOT NULL 
				AND #temp_invoice_case.invoice_case_id = ic.invoice_case_id
				AND i.invoice_id = ic.invoice_id
				AND	i.funding_source_id = 135;

		/* 3.2.3 If reject_reason_cd IN (''UTL'', ''DUPE'', ''FREDDUPE'', ''NFMCDUPEIN'') THEN agency_payble_Case with the same fc and set takeback_pmt_reason_cd = reject_reason_cd, takeback_pmt_identified_dt = getdate()*/
		UPDATE	#temp_invoice_case
		SET		takeback_pmt_reason_cd = reject_reason_cd
				, takeback_pmt_identified_dt = getdate()
		FROM	#temp_invoice_case
		WHERE	#temp_invoice_case.reject_reason_cd IN (''UTL'', ''DUPE'', ''FREDDUPE'', ''NFMCDUPEIN'');				

	/* 3.3 ELSE IF reject_reason_cd  IS NULL */
		/* 3.3.1 If funding source is Neighbourworks(=Freddie, ID= 135) THEN table agency_payable_case with the same fc_id and set NFMC_difference_eligible_ind = ''Y'' */
		UPDATE	#temp_invoice_case
		SET		NFMC_difference_eligible_ind = ''Y''
		FROM	#temp_invoice_case, invoice_case ic, invoice i
		WHERE	#temp_invoice_case.reject_reason_cd IS NULL
				AND #temp_invoice_case.invoice_case_id = ic.invoice_case_id
				AND i.invoice_id = ic.invoice_id
				AND	i.funding_source_id = 135;
		/* 3.2.3 If reject_reason_cd IS NULL THEN agency_payble_Case with the same fc and set takeback_pmt_reason_cd = NULL, takeback_pmt_identified_dt = NULL */
		UPDATE	#temp_invoice_case
		SET		takeback_pmt_reason_cd = NULL
				, takeback_pmt_identified_dt = NULL
		FROM	#temp_invoice_case
		WHERE	#temp_invoice_case.reject_reason_cd IS NULL;

	/* 3.4 Process update Invoice, Invoice_case, Agency_payable_case, case_loan */
		UPDATE	invoice_case
		SET		invoice_case.pmt_reject_reason_cd = tic.reject_reason_cd
				,invoice_case.invoice_case_pmt_amt = tic.invoice_case_pmt_amt
				,invoice_case.invoice_payment_id = @pi_invoice_payment_id
				,invoice_case.chg_lst_dt = @pi_chg_lst_dt
				,invoice_case.chg_lst_user_id = @pi_chg_lst_user_id
				,invoice_case.chg_lst_app_name = @pi_chg_lst_app_name
		FROM	invoice_case , #temp_invoice_case tic
		WHERE	invoice_case.invoice_case_id = tic.invoice_case_id;

		UPDATE	agency_payable_case 
		SET		agency_payable_case.NFMC_difference_eligible_ind = tic.NFMC_difference_eligible_ind		
				,agency_payable_case.chg_lst_dt = @pi_chg_lst_dt
				,agency_payable_case.chg_lst_user_id = @pi_chg_lst_user_id
				,agency_payable_case.chg_lst_app_name = @pi_chg_lst_app_name
		FROM	agency_payable_case, #temp_invoice_case tic
		WHERE	agency_payable_case.fc_id = tic.fc_id AND tic.NFMC_difference_eligible_ind IS NOT NULL;

		UPDATE	agency_payable_case 
		SET		agency_payable_case.takeback_pmt_reason_cd = tic.takeback_pmt_reason_cd
				,agency_payable_case.takeback_pmt_identified_dt = tic.takeback_pmt_identified_dt
				,agency_payable_case.chg_lst_dt = @pi_chg_lst_dt
				,agency_payable_case.chg_lst_user_id = @pi_chg_lst_user_id
				,agency_payable_case.chg_lst_app_name = @pi_chg_lst_app_name
		FROM	agency_payable_case, #temp_invoice_case tic
		WHERE	agency_payable_case.fc_id = tic.fc_id;

		UPDATE	case_loan
		SET		case_loan.investor_loan_num = tic.investor_loan_num
				,case_loan.investor_num = tic.investor_num
				,case_loan.investor_name = tic.investor_name
				,case_loan.Freddie_servicer_num = tic.Freddie_servicer_num
				,case_loan.chg_lst_dt = @pi_chg_lst_dt
				,case_loan.chg_lst_user_id = @pi_chg_lst_user_id
				,case_loan.chg_lst_app_name = @pi_chg_lst_app_name
		FROM	case_loan , #temp_invoice_case tic
		WHERE	case_loan.fc_id = tic.fc_id AND case_loan.loan_1st_2nd_cd = ''1st'';
		
		UPDATE	Invoice 
		SET		Invoice.invoice_pmt_amt = tic.invoice_pmt_amt				
				,Invoice.chg_lst_dt = @pi_chg_lst_dt
				,Invoice.chg_lst_user_id = @pi_chg_lst_user_id
				,Invoice.chg_lst_app_name = @pi_chg_lst_app_name
		FROM	Invoice 
				, (SELECT	ic.invoice_id, SUM (tic1.invoice_case_pmt_amt) invoice_pmt_amt
					FROM	invoice_case ic, #temp_invoice_case tic1
					WHERE	ic.invoice_case_id = tic1.invoice_case_id
					GROUP BY ic.invoice_id) tic
		WHERE	Invoice.Invoice_id = tic.Invoice_id;
END;
	-- Step 4: Remove XML document
	EXEC sp_xml_removedocument @xml_hndl ;
	DROP TABLE #temp_invoice_case;
END; 










' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_detail_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Thao Nguyen>
-- Create date: <Create Date,,>
-- Description:	<get Billing information for accounting>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_detail_get]
	(
	@pi_fc_id int
	)
AS
BEGIN
	SELECT	i.invoice_dt, fs.funding_source_name, i.Invoice_id, l.acct_num, l.loan_1st_2nd_cd, 
			ic.invoice_case_pmt_amt, ic.invoice_case_bill_amt, ip.pmt_dt, ic.pmt_reject_reason_cd
	FROM	Invoice i, funding_source fs 
			, invoice_case ic LEFT OUTER JOIN invoice_payment ip ON ic.invoice_payment_id = ip.invoice_payment_id 
			, case_loan l 
	WHERE	i.funding_source_id = fs.funding_source_id
			AND i.Invoice_id = ic.Invoice_id
			AND ic.fc_id = l.fc_id
			AND ic.fc_id=@pi_fc_id	
	ORDER BY 	i.invoice_dt desc				
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Sinh Tran
-- Create date: 2009/02/11
-- Description:	Update invoice Payment
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_payment_update]
	-- Add the parameters for the stored procedure here	
	@pi_funding_source_id	int = -1
	, @pi_pmt_num			varchar(30) = null
	, @pi_pmt_dt			datetime = null
	, @pi_pmt_cd			varchar(15) = null
	, @pi_pmt_amt			numeric(15,2) = null
	, @pi_invoice_payment_comment varchar(300) = null
	, @pi_payment_file		varchar(300) = null
	, @pi_chg_lst_dt		datetime = null
	, @pi_chg_lst_user_id	varchar(30) = null
	, @pi_chg_lst_app_name	varchar(20) = null
	, @pi_invoice_payment_id int = -1
	, @pi_with_file			int =-1
AS
BEGIN	
	UPDATE	invoice_payment 
	SET		funding_source_id=@pi_funding_source_id	
			, pmt_num=@pi_pmt_num			
			, pmt_dt=@pi_pmt_dt			
			, pmt_cd=@pi_pmt_cd			
			, pmt_amt=@pi_pmt_amt			
			, invoice_payment_comment = @pi_invoice_payment_comment
			, payment_file = @pi_payment_file
			, chg_lst_dt=@pi_chg_lst_dt		
			, chg_lst_user_id=@pi_chg_lst_user_id	
			, chg_lst_app_name=@pi_chg_lst_app_name	
	WHERE	invoice_payment_id = @pi_invoice_payment_id
	IF @pi_with_file = 0
		UPDATE	invoice_case
		SET		invoice_payment_id = NULL
				, invoice_case_pmt_amt = NULL
				, pmt_reject_reason_cd = NULL
		WHERE	invoice_payment_id = @pi_invoice_payment_id;	
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb 2009
-- Project : HPF 
-- Description: hpf_invoice_payment_insert : Insert a new Invoice Payment
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_payment_insert]
	@pi_funding_source_id	int = -1
	, @pi_pmt_num			varchar(30) = null
	, @pi_pmt_dt			datetime = null
	, @pi_pmt_cd			varchar(15) = null
	, @pi_pmt_amt			numeric(15,2) = null
	, @pi_invoice_payment_comment varchar(300) = null
	, @pi_payment_file		varchar(300) = null
	, @pi_create_dt			datetime = null
	, @pi_create_user_id		varchar(30) = null
	, @pi_create_app_name		varchar(20) = null
	, @pi_chg_lst_dt		datetime = null
	, @pi_chg_lst_user_id	varchar(30) = null
	, @pi_chg_lst_app_name	varchar(20) = null
	, @po_invoice_payment_id int OUTPUT
AS
BEGIN
	INSERT INTO invoice_payment (funding_source_id, pmt_num, pmt_dt, pmt_cd, pmt_amt, invoice_payment_comment, payment_file
		, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
	VALUES (@pi_funding_source_id, @pi_pmt_num, @pi_pmt_dt, @pi_pmt_cd, @pi_pmt_amt, @pi_invoice_payment_comment, @pi_payment_file
		, @pi_create_dt, @pi_create_user_id, @pi_create_app_name, @pi_chg_lst_dt, @pi_chg_lst_user_id, @pi_chg_lst_app_name);

	SET @po_invoice_payment_id = SCOPE_IDENTITY()
END;


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_payment_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 09 Feb
-- Description:	Get an Invoice Payment
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_payment_get]
	(@pi_invoice_payment_id int = -1)
AS
BEGIN
	SELECT i.funding_source_id, fs.funding_source_name, i.invoice_payment_id
			,i.pmt_num, i.pmt_dt, i.pmt_cd, i.pmt_amt, i.invoice_payment_comment, i.payment_file
	FROM	invoice_payment i INNER JOIN funding_source fs ON i.funding_source_id = fs.funding_source_id
	WHERE	invoice_payment_id = @pi_invoice_payment_id;
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_case_update_for_invoice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'









-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 2009/02/04
-- Description:	Update invoice_case and invoice (Call from Edit Invoice screen)
--	If @pi_update_flag = 0 => Update "Reject Marked Cases"
--	If @pi_update_flag = 1 => Update "Unpay Marked Cases"
--	If @pi_update_flag = 2 => Update "Pay Marked Cases"
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_case_update_for_invoice]
	@pi_update_flag		int = -1
	,@pi_invoice_bill_amt	numeric(15,2) = -1
	,@pi_Invoice_id			int = -1
	,@pi_str_invoice_case_id	varchar(2000) = null
	,@pi_pmt_reject_reason_cd	varchar(15) = null
	,@pi_invoice_payment_id	int = -1
	,@pi_invoice_pmt_amt	numeric(15,2) = -1
	,@pi_chg_lst_dt			datetime= null
	,@pi_chg_lst_user_id	varchar(30)= null
	,@pi_chg_lst_app_name	varchar(20)= null	
	,@po_valid_invoice_payment_id int OUTPUT -- return 0 : Error (no exists Invoice_payment_id, >0 : No error
AS
DECLARE @v_sql varchar(4000), @v_sql1 varchar(4000), @v_NFMC_difference_eligible_ind varchar(1);
BEGIN
BEGIN TRANSACTION
	SELECT @po_valid_invoice_payment_id = -1;
IF  @pi_update_flag IN (0,1,2) 
BEGIN
	/* Update "Reject Marked Cases" */
	IF @pi_update_flag = 0 
	BEGIN
		IF @pi_pmt_reject_reason_cd IS NOT NULL
			SELECT @v_sql = ''UPDATE invoice_case '' 
							+ '' SET invoice_payment_ID = NULL, invoice_case_pmt_amt = NULL, pmt_reject_reason_cd = '''''' + cast(@pi_pmt_reject_reason_cd  as varchar(15))+ '''''''';
		ELSE
			SELECT @v_sql = ''UPDATE invoice_case '' 
							+ '' SET invoice_payment_ID = NULL, invoice_case_pmt_amt = NULL, pmt_reject_reason_cd = NULL'';
	END;
	/* Update "Unpay Marked Cases" */
	IF @pi_update_flag = 1
	BEGIN
		SELECT @v_sql = ''UPDATE invoice_case '' 
						+ '' SET invoice_payment_ID = NULL, invoice_case_pmt_amt = NULL, pmt_reject_reason_cd=NULL '';
		SELECT @v_sql1 = ''UPDATE	agency_payable_case ''
					+ '' SET	NFMC_difference_eligible_ind = ''''N''''''
					+ ''		,agency_payable_case.chg_lst_dt = '''''' + cast(@pi_chg_lst_dt as varchar(20))+ '''''''' 
					+ ''		,agency_payable_case.chg_lst_user_id = '''''' + cast(@pi_chg_lst_user_id as varchar(20))  + ''''''''
					+ ''		,agency_payable_case.chg_lst_app_name = '''''' + cast(@pi_chg_lst_app_name as varchar(20))+ ''''''''
					+ '' FROM	agency_payable_case''
					+ '' WHERE	agency_payable_case.fc_id IN (SELECT fc_id FROM invoice_case ic, invoice i ''
					+ ''			WHERE ic.invoice_id = i.invoice_id AND i.funding_source_id = 135 AND ic.invoice_case_id IN ('' + @pi_str_invoice_case_id +''));'';
	END;
	/* Update "Pay Marked Cases" */
	IF @pi_update_flag = 2
	BEGIN
		SELECT	@po_valid_invoice_payment_id = count(invoice_payment_id) 
		FROM	invoice_payment
		where	invoice_payment_ID = @pi_invoice_payment_ID;

		IF @po_valid_invoice_payment_id > 0 
		BEGIN
			SELECT @v_sql = ''UPDATE invoice_case '' 
					+ '' SET invoice_case_pmt_amt = invoice_case_bill_amt, invoice_payment_id = '' + cast(@pi_invoice_payment_id as varchar(15)) +'',pmt_reject_reason_cd=NULL ''
			SELECT @v_sql1 = ''UPDATE	agency_payable_case ''
					+ '' SET	NFMC_difference_eligible_ind = ''''Y''''''
					+ ''		,chg_lst_dt = '''''' + cast(@pi_chg_lst_dt as varchar(20))+ '''''''' 
					+ ''		,chg_lst_user_id = '''''' + cast(@pi_chg_lst_user_id as varchar(20))  + ''''''''
					+ ''		,chg_lst_app_name = '''''' + cast(@pi_chg_lst_app_name as varchar(20))+ ''''''''
					+ '' FROM	agency_payable_case''
					+ '' WHERE	agency_payable_case.fc_id IN (SELECT fc_id FROM invoice_case ic, invoice i ''
					+ ''			WHERE ic.invoice_id = i.invoice_id AND i.funding_source_id = 135 AND ic.invoice_case_id IN ('' + @pi_str_invoice_case_id +''));'';

		END;
	END
	SELECT @v_sql = @v_sql 
					+ '' ,chg_lst_dt = '''''' + cast(@pi_chg_lst_dt as varchar(20))+ '''''''' 
					+ '' ,chg_lst_user_id = '''''' + cast(@pi_chg_lst_user_id as varchar(20))  + ''''''''
					+ '' ,chg_lst_app_name = '''''' + cast(@pi_chg_lst_app_name as varchar(20))+ ''''''''
					+ '' WHERE	invoice_case_id IN ('' + @pi_str_invoice_case_id + '');'';	
	EXECUTE (@v_sql);

	IF @v_sql1 IS NOT NULL	EXECUTE (@v_sql1);

--	Print(@v_sql);
	IF @po_valid_invoice_payment_id <> 0 
		UPDATE	Invoice 
		SET		invoice_pmt_amt = @pi_invoice_pmt_amt
				,invoice_bill_amt = @pi_invoice_bill_amt
				,chg_lst_dt = @pi_chg_lst_dt
				,chg_lst_user_id = @pi_chg_lst_user_id
				,chg_lst_app_name = @pi_chg_lst_app_name
		WHERE	Invoice_id = @pi_Invoice_id

	IF @@error = 0	commit;
	ELSE rollback;
END;
END







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 2009/01/15
-- Description:	Insert invoice
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_insert]
	-- Add the parameters for the stored procedure here	
	@pi_funding_source_id int = -1
	,@pi_invoice_dt datetime = null
	,@pi_period_start_dt datetime = null
	,@pi_period_end_dt datetime= null
	,@pi_invoice_pmt_amt numeric(15,2)= null
	,@pi_invoice_bill_amt numeric(15,2)= null
	,@pi_status_cd varchar(15)= null
	,@pi_invoice_comment varchar(300)= null
	,@pi_accounting_link_TBD varchar(30)= null
	,@pi_create_dt datetime= null
	,@pi_create_user_id varchar(30)= null
	,@pi_create_app_name varchar(20)= null
	,@pi_chg_lst_dt datetime= null
	,@pi_chg_lst_user_id varchar(30)= null
	,@pi_chg_lst_app_name varchar(20)= null
	,@po_Invoice_id int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO Invoice 
	(	funding_source_id
		,invoice_dt
		,period_start_dt
		,period_end_dt
		,invoice_pmt_amt
		,invoice_bill_amt
		,status_cd
		,invoice_comment
		,accounting_link_TBD
		,create_dt
		,create_user_id
		,create_app_name
		,chg_lst_dt
		,chg_lst_user_id
		,chg_lst_app_name
	)
	VALUES
	(	@pi_funding_source_id
		,@pi_invoice_dt
		,@pi_period_start_dt
		,@pi_period_end_dt
		,@pi_invoice_pmt_amt
		,@pi_invoice_bill_amt
		,@pi_status_cd
		,@pi_invoice_comment
		,@pi_accounting_link_TBD
		,@pi_create_dt
		,@pi_create_user_id
		,@pi_create_app_name
		,@pi_chg_lst_dt
		,@pi_chg_lst_user_id
		,@pi_chg_lst_app_name
	);
	SET @po_Invoice_id = SCOPE_IDENTITY()
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 2009/01/22
-- Description:	Update invoice
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_update]
	-- Add the parameters for the stored procedure here	
	@pi_Invoice_id int = -1
	,@pi_funding_source_id int = -1
	,@pi_invoice_dt datetime = null
	,@pi_period_start_dt datetime = null
	,@pi_period_end_dt datetime= null
	,@pi_invoice_pmt_amt numeric(15,2)= null
	,@pi_invoice_bill_amt numeric(15,2)= null
	,@pi_status_cd varchar(15)= null
	,@pi_invoice_comment varchar(300)= null
	,@pi_accounting_link_TBD varchar(30)= null	
	,@pi_chg_lst_dt datetime= null
	,@pi_chg_lst_user_id varchar(30)= null
	,@pi_chg_lst_app_name varchar(20)= null	
AS
BEGIN
	UPDATE	Invoice 
	SET		funding_source_id = @pi_funding_source_id
			,invoice_dt = @pi_invoice_dt
			,status_cd = @pi_status_cd
			,period_start_dt = @pi_period_start_dt
			,period_end_dt = @pi_period_end_dt
			,invoice_pmt_amt = @pi_invoice_pmt_amt
			,invoice_bill_amt = @pi_invoice_bill_amt
			,invoice_comment = @pi_invoice_comment
			,accounting_link_TBD = @pi_accounting_link_TBD
			,chg_lst_dt = @pi_chg_lst_dt
			,chg_lst_user_id = @pi_chg_lst_user_id
			,chg_lst_app_name = @pi_chg_lst_app_name
	WHERE	Invoice_id = @pi_Invoice_id
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_search]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




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
	IF @pi_funding_source_id <= 0
		SELECT	i.funding_source_id, fs.funding_source_name, i.invoice_id, i.invoice_dt
				, i.period_start_dt, i.period_end_dt
				, i.invoice_bill_amt, i.invoice_pmt_amt
				, rci.code_desc as status_cd
				, i.invoice_comment
		FROM	invoice i INNER JOIN funding_source fs ON i.funding_source_id = fs.funding_source_id
				LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''invoice status code'') rci ON rci.code = i.status_cd
		WHERE	i.invoice_dt between @pi_start_dt and @pi_end_dt
		ORDER BY i.period_start_dt DESC, i.period_end_dt DESC;
	IF @pi_funding_source_id > 0
		SELECT	i.funding_source_id, fs.funding_source_name, i.invoice_id, i.invoice_dt
				, i.period_start_dt, i.period_end_dt
				, i.invoice_bill_amt, i.invoice_pmt_amt
				, rci.code_desc as status_cd
				, i.invoice_comment
		FROM	invoice i INNER JOIN funding_source fs ON i.funding_source_id = fs.funding_source_id
				LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name = ''invoice status code'') rci ON rci.code = i.status_cd
		WHERE	i.invoice_dt between @pi_start_dt and @pi_end_dt
				AND i.funding_source_id = @pi_funding_source_id
		ORDER BY i.period_start_dt DESC, i.period_end_dt DESC;
END;





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_activity_log_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<PhuocDang>
-- Create date: <2009,Feb, 9>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_activity_log_get]
	(
	@pi_fc_id int	
	)
AS
BEGIN
	SELECT	activity_log_id,
			activity_cd,
			a.code_desc activity_cd_desc,
			activity_dt,
			activity_note,
			create_dt, 
			create_user_id,
			create_app_name, 
			chg_lst_dt,
			chg_lst_user_id, 
			chg_lst_app_name

    FROM activity_log c	
	LEFT OUTER JOIN (SELECT code, code_desc FROM ref_code_item WHERE ref_code_set_name= ''activity code'')  a ON  c.activity_cd = a.code
	WHERE fc_id = @pi_fc_id
	ORDER BY activity_dt DESC
	
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_security_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

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
	WHERE ms.hpf_user_id=@pi_userid AND ms.menu_item_id=mi.menu_item_id
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Khoa Do
-- Create date: 20090106
-- Description:	Insert agency payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_insert]
	-- Add the parameters for the stored procedure here
	 @pi_agency_id int = null
	,@pi_pmt_dt datetime = null
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
	INSERT INTO agency_payable
           ([agency_id]
           ,[pmt_dt]
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



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Khoa Do
-- Create date: 2009/01/06
-- Description:	Update agency payable
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_update]
	-- Add the parameters for the stored procedure here
	 @pi_agency_payable_id int = null
	,@pi_agency_id int = null
	,@pi_pmt_dt datetime = null	
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
	IF(@pi_pmt_comment IS NOT NULL OR @pi_status_cd IS NOT NULL)
		UPDATE agency_payable
		SET [status_cd] = @pi_status_cd
			,[pmt_comment] = @pi_pmt_comment
			,[chg_lst_dt] = @pi_chg_lst_dt
			,[chg_lst_user_id] = @pi_chg_lst_user_id
			,[chg_lst_app_name] = @pi_chg_lst_app_name
		WHERE [agency_payable_id] = @pi_agency_payable_id	
    -- Insert statements for procedure here
	ELSE
		UPDATE agency_payable
		SET [agency_id] = @pi_agency_id
		 ,[pmt_dt] = @pi_pmt_dt
		 ,[status_cd] = @pi_status_cd
		 ,[period_start_dt] = @pi_period_start_dt
		 ,[period_end_dt] = @pi_period_end_dt
		 ,[pmt_comment] = @pi_pmt_comment
		 ,[accounting_link_TBD] = @pi_accounting_link_TBD      
		 ,[chg_lst_dt] = @pi_chg_lst_dt
		 ,[chg_lst_user_id] = @pi_chg_lst_user_id
		 ,[chg_lst_app_name] = @pi_chg_lst_app_name
		 ,[agency_payable_pmt_amt] = @pi_agency_payable_pmt_amt
 WHERE [agency_payable_id] = @pi_agency_payable_id
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
			,call_center_name
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
			, City 
			, State
			, nonprofitreferral_key_num1 
			, nonprofitreferral_key_num2 
			, nonprofitreferral_key_num3 
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
	@pi_City varchar(30) =NULL,
	@pi_State varchar(2) =NULL,
	@pi_nonprofitreferral_key_num1 Varchar(10) =NULL,
	@pi_nonprofitreferral_key_num2 Varchar(10) =NULL,
	@pi_nonprofitreferral_key_num3 Varchar(10) =NULL,
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
	[call_center_name],
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
	City,
	State,
	nonprofitreferral_key_num1,
	nonprofitreferral_key_num2,
	nonprofitreferral_key_num3,
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
	@pi_City,
	@pi_State,
	@pi_nonprofitreferral_key_num1,
	@pi_nonprofitreferral_key_num2,
	@pi_nonprofitreferral_key_num3,
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_subcategory_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
	SET NOCOUNT ON;

	SELECT [budget_subcategory_id],[budget_subcategory_name]      
	FROM [dbo].[budget_subcategory]
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_menu_security_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 17 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Insert menu_security
-- =============================================
CREATE PROCEDURE [dbo].[hpf_menu_security_insert]	
	@pi_menu_security_id int
	, @pi_menu_item_id int
	, @pi_permission_value varchar(50)
	, @pi_hpf_user_id int
AS
BEGIN
	INSERT INTO menu_security (menu_security_id,menu_item_id,permission_value,hpf_user_id)
	VALUES	(@pi_menu_security_id,@pi_menu_item_id,@pi_permission_value,@pi_hpf_user_id);
END;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_ws_user_get_from_username_password]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_update_app]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






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
@pi_hpf_media_candidate_ind varchar(1)=null,
@pi_never_pay_reason varchar(15)=null,
@pi_never_bill_reason varchar(15)=null,
@pi_chg_lst_dt datetime=null,
@pi_chg_lst_user_id varchar(30)=null,
@pi_chg_lst_app_name varchar(20)=null
)
AS
BEGIN
	IF (@pi_never_pay_reason is null and @pi_never_bill_reason is null and @pi_agency_id IS NOT null )
		 UPDATE [dbo].[foreclosure_case]
		   SET 		[agency_id]=@pi_agency_id,
					[duplicate_ind]=@pi_duplicate_ind,
					[loan_dflt_reason_notes]=@pi_loan_dflt_reason_notes,
					[action_items_notes]=@pi_action_items_notes,
					[followup_notes]=@pi_followup_notes,
					[opt_out_newsletter_ind]=@pi_opt_out_newsletter_ind,
					[opt_out_survey_ind]=@pi_opt_out_survey_ind,
					[do_not_call_ind]=@pi_do_not_call_ind,
					[hpf_success_story_ind]=@pi_hpf_success_story_ind,
					[hpf_media_candidate_ind]=@pi_hpf_media_candidate_ind,	
					[chg_lst_dt] =@pi_chg_lst_dt ,
					[chg_lst_user_id] =@pi_chg_lst_user_id ,
					[chg_lst_app_name]=@pi_chg_lst_app_name
		WHERE [fc_id] = @pi_fc_id 
	IF(@pi_duplicate_ind =  ''N'')
		BEGIN
			UPDATE	foreclosure_case
			SET		never_bill_reason_cd = NULL, 
					[chg_lst_dt] =@pi_chg_lst_dt ,
					[chg_lst_user_id] =@pi_chg_lst_user_id ,
					[chg_lst_app_name]=@pi_chg_lst_app_name
			WHERE	fc_id = @pi_fc_id AND never_bill_reason_cd = ''DUPE'';

			UPDATE	foreclosure_case
			SET		never_pay_reason_cd = NULL,
					[chg_lst_dt] =@pi_chg_lst_dt ,
					[chg_lst_user_id] =@pi_chg_lst_user_id ,
					[chg_lst_app_name]=@pi_chg_lst_app_name
			WHERE	fc_id = @pi_fc_id AND never_pay_reason_cd = ''DUPE'';
		END
	IF(@pi_duplicate_ind =  ''Y'')
		BEGIN
			UPDATE	foreclosure_case
			SET		never_bill_reason_cd = ''DUPE'',
					[chg_lst_dt] =@pi_chg_lst_dt ,
					[chg_lst_user_id] =@pi_chg_lst_user_id ,
					[chg_lst_app_name]=@pi_chg_lst_app_name
			WHERE	fc_id = @pi_fc_id AND never_bill_reason_cd IS NULL ;

			UPDATE	foreclosure_case
			SET		never_pay_reason_cd = ''DUPE'',
					[chg_lst_dt] =@pi_chg_lst_dt ,
					[chg_lst_user_id] =@pi_chg_lst_user_id ,
					[chg_lst_app_name]=@pi_chg_lst_app_name
			WHERE	fc_id = @pi_fc_id AND never_pay_reason_cd IS NULL;
		END
	ELSE	
		 UPDATE [dbo].[foreclosure_case]
		   SET 	[never_bill_reason_cd]=@pi_never_bill_reason,
				[never_pay_reason_cd]=@pi_never_pay_reason,
				[chg_lst_dt] =@pi_chg_lst_dt ,
				[chg_lst_user_id] =@pi_chg_lst_user_id ,
				[chg_lst_app_name]=@pi_chg_lst_app_name
		WHERE [fc_id] = @pi_fc_id 	
-- Task 2:IF ccrc_sor_ind = ''Y'' then ccrc_sor_ind= ''N''
	UPDATE	foreclosure_case
	SET		ccrc_sor_ind = ''N''
	WHERE [fc_id] = @pi_fc_id AND ccrc_sor_ind = ''Y'';
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
	SELECT	fc_id,agency_id,agency_case_num
	FROM	foreclosure_case 
	WHERE	agency_id = @pi_agency_id 
			AND	agency_case_num = @pi_agency_case_num
End







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_foreclosure_case_update_ws]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Update a case from Web Service
-- =============================================

CREATE PROCEDURE [dbo].[hpf_foreclosure_case_update_ws]
	@pi_fc_id int = 0	
	,@pi_summary_sent_dt datetime		
	,@chg_lst_dt datetime		
As 
Begin
	IF @pi_summary_sent_dt  IS NOT NULL
		UPDATE	foreclosure_case
		SET		summary_sent_dt = @pi_summary_sent_dt,
				chg_lst_dt = @chg_lst_dt
		WHERE	fc_id = @pi_fc_id;
-- Task 2:IF ccrc_sor_ind = ''Y'' then ccrc_sor_ind= ''N''
	UPDATE	foreclosure_case
	SET		ccrc_sor_ind = ''N''
	WHERE [fc_id] = @pi_fc_id AND ccrc_sor_ind = ''Y'';
End	



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_invoice_case_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Thien Nguyen
-- Create date: 2009/01/06
-- Description:	Insert invoice case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_invoice_case_insert]
	-- Add the parameters for the stored procedure here
	
	@pi_fc_id int  = -1	
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
	INSERT INTO invoice_case
           (
			[fc_id]			
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



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_activity_log_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Thao Nguyen>
-- Create date: <Create Date,,>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[hpf_activity_log_insert]
	(
	@pi_fc_id int,
	@pi_activity_cd varchar(15),
	@pi_activity_dt datetime,
	@pi_activity_note varchar(2000),
	@pi_create_dt datetime,
	@pi_create_user_id varchar(30), 
	@pi_create_app_name varchar(20),
	@pi_chg_lst_dt datetime, 
	@pi_chg_lst_user_id varchar(30),
	@pi_chg_lst_app_name varchar(20)
	
	)
AS
BEGIN
	INSERT INTO activity_log
	(fc_id,
	activity_cd,
	activity_dt,
	activity_note,
	create_dt, 
	create_user_id,
	create_app_name, 
	chg_lst_dt,
	chg_lst_user_id, 
	chg_lst_app_name
	) 
	VALUES
	(@pi_fc_id,
	@pi_activity_cd,
	@pi_activity_dt,
	@pi_activity_note,
	@pi_create_dt, 
	@pi_create_user_id,
	@pi_create_app_name,
	@pi_chg_lst_dt ,
	@pi_chg_lst_user_id,
	@pi_chg_lst_app_name 
	)
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
EXEC dbo.sp_executesql @statement = N'
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_budget_set_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 20 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Get budget set(Web APP)
-- =============================================
CREATE PROCEDURE [dbo].[hpf_budget_set_get]
   @pi_fc_id int   
AS
BEGIN
	SELECT	budget_set_id
			,budget_set_dt as budget_dt
			,total_income - total_expenses as Total_surplus
			,total_income
			,total_expenses
			,total_assets
	FROM	budget_set
	WHERE	fc_id = @pi_fc_id	
	ORDER BY budget_set_dt desc
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_audit_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Insert case_audit
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_audit_insert]	
	@pi_fc_id int
	, @pi_audit_dt datetime = null
	, @pi_audit_comment varchar(300) = null
	, @pi_reviewed_by varchar(30) = null
	, @pi_appropriate_outcome_ind varchar(1) = null
	, @pi_appropriate_reason_for_dflt_ind varchar(1) = null
	, @pi_complete_budget_ind varchar(1) = null
	, @pi_audit_type_cd varchar(15) = null
	, @pi_client_action_plan_ind varchar(1) = null
	, @pi_verbal_privacy_consent_ind varchar(1) = null
	, @pi_written_privacy_consent_ind varchar(1) = null
	, @pi_compliant_ind varchar(1) = null
	, @pi_audit_failure_reason_cd varchar(15) = null
	, @pi_create_user_id varchar(30)
	, @pi_create_dt datetime
	, @pi_create_app_name varchar(20)
	, @pi_chg_lst_dt datetime
	, @pi_chg_lst_user_id varchar(30)
	, @pi_chg_lst_app_name varchar(20)
	, @po_case_audit_id int OUTPUT
AS
BEGIN
	INSERT INTO case_audit(
		fc_id
		,audit_dt
		,audit_comment
		,reviewed_by
		,appropriate_outcome_ind
		,appropriate_reason_for_dflt_ind
		,complete_budget_ind
		,audit_type_cd
		,client_action_plan_ind
		,verbal_privacy_consent_ind
		,written_privacy_consent_ind
		,compliant_ind
		,audit_failure_reason_cd
		,create_user_id
		,create_dt
		,create_app_name
		,chg_lst_dt
		,chg_lst_user_id
		,chg_lst_app_name
	) 
	VALUES (
		@pi_fc_id
		,@pi_audit_dt
		,@pi_audit_comment
		,@pi_reviewed_by
		,@pi_appropriate_outcome_ind
		,@pi_appropriate_reason_for_dflt_ind
		,@pi_complete_budget_ind
		,@pi_audit_type_cd
		,@pi_client_action_plan_ind
		,@pi_verbal_privacy_consent_ind
		,@pi_written_privacy_consent_ind
		,@pi_compliant_ind
		,@pi_audit_failure_reason_cd
		,@pi_create_user_id
		,@pi_create_dt
		,@pi_create_app_name
		,@pi_chg_lst_dt
		,@pi_chg_lst_user_id
		,@pi_chg_lst_app_name
	);

	SET @po_case_audit_id = SCOPE_IDENTITY();
END;





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_audit_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	UPDATE case_audit
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_audit_update]
	@pi_case_audit_id int
	, @pi_fc_id int
	, @pi_audit_dt datetime = null
	, @pi_audit_comment varchar(300) = null
	, @pi_reviewed_by varchar(30) = null
	, @pi_appropriate_outcome_ind varchar(1) = null
	, @pi_appropriate_reason_for_dflt_ind varchar(1) = null
	, @pi_complete_budget_ind varchar(1) = null
	, @pi_audit_type_cd varchar(15) = null
	, @pi_client_action_plan_ind varchar(1) = null
	, @pi_verbal_privacy_consent_ind varchar(1) = null
	, @pi_written_privacy_consent_ind varchar(1) = null
	, @pi_compliant_ind varchar(1) = null
	, @pi_audit_failure_reason_cd varchar(15) = null
	, @pi_chg_lst_dt datetime
	, @pi_chg_lst_user_id varchar(30)
	, @pi_chg_lst_app_name varchar(20)
	
AS
BEGIN
	UPDATE case_audit
	SET	audit_dt= @pi_audit_dt
		,audit_comment= @pi_audit_comment
		,reviewed_by= @pi_reviewed_by
		,appropriate_outcome_ind= @pi_appropriate_outcome_ind
		,appropriate_reason_for_dflt_ind= @pi_appropriate_reason_for_dflt_ind
		,complete_budget_ind= @pi_complete_budget_ind
		,audit_type_cd= @pi_audit_type_cd
		,client_action_plan_ind= @pi_client_action_plan_ind
		,verbal_privacy_consent_ind= @pi_verbal_privacy_consent_ind
		,written_privacy_consent_ind= @pi_written_privacy_consent_ind
		,compliant_ind= @pi_compliant_ind
		,audit_failure_reason_cd= @pi_audit_failure_reason_cd
		,chg_lst_dt= @pi_chg_lst_dt
		,chg_lst_user_id= @pi_chg_lst_user_id
		,chg_lst_app_name= @pi_chg_lst_app_name
	WHERE case_audit_id = @pi_case_audit_id;
END;' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_loan_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'









CREATE PROCEDURE [dbo].[hpf_case_loan_update]
	-- Add the parameters for the stored procedure here	       
		@pi_fc_id int = null
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
		,@pi_orig_mortgage_co_FDIC_NCUA_num varchar(20) = null
		,@pi_orig_mortgage_co_name varchar(50) = null           
		,@pi_orginal_loan_num varchar(30)  = null
		,@pi_current_servicer_FDIC_NCUA_num varchar(30) = null
		,@pi_investor_num	varchar(30) = null
		,@pi_investor_name	varchar(50) = null
		,@pi_mortgage_program_cd varchar(15) = NULL
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
   SET  [servicer_id] = @pi_servicer_id
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
		,[orig_mortgage_co_FDIC_NCUA_num] = @pi_orig_mortgage_co_FDIC_NCUA_num
		,[orig_mortgage_co_name] = @pi_orig_mortgage_co_name      
		,[orginal_loan_num] = @pi_orginal_loan_num
		,[current_servicer_FDIC_NCUA_num] = @pi_current_servicer_FDIC_NCUA_num
		,[investor_num] = @pi_investor_num
		,[investor_name] = @pi_investor_name
		,[mortgage_program_cd] = @pi_mortgage_program_cd 
		,[chg_lst_dt] = @pi_chg_lst_dt
		,[chg_lst_user_id] = @pi_chg_lst_user_id
		,[chg_lst_app_name] = @pi_chg_lst_app_name	
 WHERE	[acct_num] = @pi_acct_num
		AND [fc_id] = @pi_fc_id 
--		AND servicer_id = @pi_servicer_id
       
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
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Khoa Do
-- Create date: 03 Dec 2008
-- Project : HPF 
-- Build 
-- Description:	Delete case loan
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_loan_delete]	
	       @pi_fc_id int           
           ,@pi_acct_num varchar(30)      
		   ,@pi_servicer_id  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	DELETE FROM case_loan
	WHERE	[acct_num] = @pi_acct_num
			AND [fc_id] = @pi_fc_id
			AND servicer_id = @pi_servicer_id
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
EXEC dbo.sp_executesql @statement = N'









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
           ,@pi_orig_mortgage_co_FDIC_NCUA_num varchar(20) = null
           ,@pi_orig_mortgage_co_name varchar(50) = null
           ,@pi_orginal_loan_num varchar(30) = null
           ,@pi_current_servicer_FDIC_NCUA_num varchar(30) = null
		   ,@pi_investor_num	varchar(30) = null
		   ,@pi_investor_name	varchar(50) = null
		   ,@pi_changed_acct_num VARCHAR(100)= NULL
		   ,@pi_mortgage_program_cd varchar(15) = NULL
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
           ,[orig_mortgage_co_FDIC_NCUA_num]
           ,[orig_mortgage_co_name]
           ,[orginal_loan_num]
           ,[current_servicer_FDIC_NCUA_num]
		   ,[investor_num]
		   ,[investor_name]
		   ,[changed_acct_num]
		   ,[mortgage_program_cd]
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
           ,@pi_orig_mortgage_co_FDIC_NCUA_num
           ,@pi_orig_mortgage_co_name
           ,@pi_orginal_loan_num
           ,@pi_current_servicer_FDIC_NCUA_num
		   ,@pi_investor_num
		   ,@pi_investor_name
		   ,@pi_changed_acct_num
		   ,@pi_mortgage_program_cd
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_post_counseling_status_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Insert case_post_counseling_status 
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_post_counseling_status_insert]	
	@pi_fc_id int = null
	, @pi_followup_dt datetime = null
	, @pi_followup_comment varchar(8000) = null
	, @pi_followup_source_cd varchar(15) = null
	, @pi_loan_delinq_status_cd varchar(15) = null
	, @pi_still_in_house_ind varchar(1) = null
	, @pi_credit_score varchar(4) = null
	, @pi_credit_bureau_cd varchar(15) = null 
	, @pi_credit_report_dt datetime = null
	, @pi_outcome_type_id int = null
	, @pi_create_dt datetime
	, @pi_create_user_id varchar(30)
	, @pi_create_app_name varchar(20)
	, @pi_chg_lst_dt datetime
	, @pi_chg_lst_user_id varchar(30)
	, @pi_chg_lst_app_name varchar(20)
	, @po_case_post_counseling_status_id int OUTPUT
AS

BEGIN
	INSERT INTO case_post_counseling_status (
	fc_id
	,followup_dt
	,followup_comment
	,followup_source_cd
	,loan_delinq_status_cd
	,still_in_house_ind
	,credit_score
	,credit_bureau_cd
	,credit_report_dt
	,outcome_type_id
	,create_dt
	,create_user_id
	,create_app_name
	,chg_lst_dt
	,chg_lst_user_id
	,chg_lst_app_name
	) 
	VALUES (
	@pi_fc_id
	,@pi_followup_dt
	,@pi_followup_comment
	,@pi_followup_source_cd
	,@pi_loan_delinq_status_cd
	,@pi_still_in_house_ind
	,@pi_credit_score
	,@pi_credit_bureau_cd
	,@pi_credit_report_dt
	,@pi_outcome_type_id
	,@pi_create_dt
	,@pi_create_user_id
	,@pi_create_app_name
	,@pi_chg_lst_dt
	,@pi_chg_lst_user_id
	,@pi_chg_lst_app_name
	);

	SET @po_case_post_counseling_status_id = SCOPE_IDENTITY();
END;





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_case_post_counseling_status_update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 10 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	UPDATE case_post_counseling_status 
-- =============================================
CREATE PROCEDURE [dbo].[hpf_case_post_counseling_status_update]
	@pi_case_post_counseling_status_id int
	, @pi_fc_id int = null
	, @pi_followup_dt datetime = null
	, @pi_followup_comment varchar(8000) = null
	, @pi_followup_source_cd varchar(15) = null
	, @pi_loan_delinq_status_cd varchar(15) = null
	, @pi_still_in_house_ind varchar(1) = null
	, @pi_credit_score varchar(4) = null
	, @pi_credit_bureau_cd varchar(15) = null
	, @pi_credit_report_dt datetime = null
	, @pi_outcome_type_id int = null
	, @pi_chg_lst_dt datetime
	, @pi_chg_lst_user_id varchar(30)
	, @pi_chg_lst_app_name varchar(20)
AS
BEGIN
	UPDATE	case_post_counseling_status 
	SET		followup_dt= @pi_followup_dt
			,followup_comment= @pi_followup_comment
			,followup_source_cd= @pi_followup_source_cd
			,loan_delinq_status_cd= @pi_loan_delinq_status_cd
			,still_in_house_ind= @pi_still_in_house_ind
			,credit_score= @pi_credit_score
			,credit_bureau_cd= @pi_credit_bureau_cd
			,credit_report_dt= @pi_credit_report_dt
			,outcome_type_id= @pi_outcome_type_id
			,chg_lst_dt= @pi_chg_lst_dt
			,chg_lst_user_id= @pi_chg_lst_user_id
			,chg_lst_app_name= @pi_chg_lst_app_name
	WHERE	case_post_counseling_status_id= @pi_case_post_counseling_status_id;
END;' 
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
		,@pi_chg_lst_user_id varchar(30) = null
		,@pi_chg_lst_app_name varchar(20) = null
		,@pi_is_instate int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	
	UPDATE [dbo].[outcome_item]
	SET [outcome_deleted_dt] = 
		CASE 
			WHEN @pi_is_instate is null THEN getdate()
			ELSE null
		END
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_outcome_item_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_agency_payable_case_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Khoa Do
-- Create date: 2009/01/06
-- Description:	Insert acency payable case
-- =============================================
CREATE PROCEDURE [dbo].[hpf_agency_payable_case_insert]
	-- Add the parameters for the stored procedure here	
	@pi_fc_id int  
	,@pi_agency_payable_id int 
	,@pi_pmt_dt datetime = null
	,@pi_pmt_amt numeric(15,2) = null
	,@pi_create_dt datetime 
	,@pi_create_user_id varchar(30) 
	,@pi_create_app_name varchar(20) 
	,@pi_chg_lst_dt datetime 
	,@pi_chg_lst_user_id varchar(30) 
	,@pi_chg_lst_app_name varchar(20) 
	,@pi_NFMC_difference_eligible_ind varchar(1)=''N''
	,@pi_takeback_pmt_identified_dt datetime =NULL
	,@pi_takeback_pmt_reason_cd varchar(15) =NULL
	,@pi_NFMC_difference_paid_amt Numeric (15,2) =NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO agency_payable_case
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
			,[NFMC_difference_eligible_ind]
			,takeback_pmt_identified_dt
			,takeback_pmt_reason_cd 
			,NFMC_difference_paid_amt
			)
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
			,@pi_NFMC_difference_eligible_ind
			,@pi_takeback_pmt_identified_dt
			,@pi_takeback_pmt_reason_cd 
			,@pi_NFMC_difference_paid_amt 
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClearLogs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ClearLogs]
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CategoryLog
	DELETE FROM [Log]
    DELETE FROM Category
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WriteLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



/****** Object:  Stored Procedure dbo.WriteLog    Script Date: 10/1/2004 3:16:36 PM ******/

CREATE PROCEDURE [dbo].[WriteLog]
(
	@EventID int, 
	@Priority int, 
	@Severity nvarchar(32), 
	@Title nvarchar(256), 
	@Timestamp datetime,
	@MachineName nvarchar(32), 
	@AppDomainName nvarchar(512),
	@ProcessID nvarchar(256),
	@ProcessName nvarchar(512),
	@ThreadName nvarchar(512),
	@Win32ThreadId nvarchar(128),
	@Message nvarchar(1500),
	@FormattedMessage ntext,
	@LogId int OUTPUT
)
AS 

	INSERT INTO [Log] (
		EventID,
		Priority,
		Severity,
		Title,
		[Timestamp],
		MachineName,
		AppDomainName,
		ProcessID,
		ProcessName,
		ThreadName,
		Win32ThreadId,
		Message,
		FormattedMessage
	)
	VALUES (
		@EventID, 
		@Priority, 
		@Severity, 
		@Title, 
		@Timestamp,
		@MachineName, 
		@AppDomainName,
		@ProcessID,
		@ProcessName,
		@ThreadName,
		@Win32ThreadId,
		@Message,
		@FormattedMessage)

	SET @LogID = @@IDENTITY
	RETURN @LogID



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertCategoryLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[InsertCategoryLog]
	@CategoryID INT,
	@LogID INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CatLogID INT
	SELECT @CatLogID FROM CategoryLog WHERE CategoryID=@CategoryID and LogID = @LogID
	IF @CatLogID IS NULL
	BEGIN
		INSERT INTO CategoryLog (CategoryID, LogID) VALUES(@CategoryID, @LogID)
		RETURN @@IDENTITY
	END
	ELSE RETURN @CatLogID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_funding_source_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 19 Feb 2009
-- Project : HPF 
-- Build 
-- Description:	Get Funding Source List
-- =============================================
CREATE PROCEDURE [dbo].[hpf_funding_source_get]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT -1 as funding_source_id,'' ALL''  as funding_source_name,null as billing_email,null as billing_delivery_method_cd
			,null as export_format_cd,null as funding_source_abbrev,null as sharepoint_foldername
	UNION
	SELECT	f.funding_source_id,ltrim(f.funding_source_name),billing_email
			,billing_delivery_method_cd,export_format_cd,funding_source_abbrev,sharepoint_foldername 
	FROM funding_source f
	ORDER BY funding_source_name; 
	
END



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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[AddCategory]
	-- Add the parameters for the function here
	@CategoryName nvarchar(64),
	@LogID int
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @CatID INT
	SELECT @CatID = CategoryID FROM Category WHERE CategoryName = @CategoryName
	IF @CatID IS NULL
	BEGIN
		INSERT INTO Category (CategoryName) VALUES(@CategoryName)
		SELECT @CatID = @@IDENTITY
	END

	EXEC InsertCategoryLog @CatID, @LogID 

	RETURN @CatID
END

' 
END
GO