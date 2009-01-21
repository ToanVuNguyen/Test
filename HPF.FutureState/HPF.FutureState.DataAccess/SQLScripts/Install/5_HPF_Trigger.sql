-- =============================================
-- Create date: 21 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Create triggers are being used in HPF
-- =============================================

USE [hpf]
GO

IF EXISTS (select o.name from sysobjects o where o.xtype = 'TR' and name = N'trg_case_loan_delete')
    DROP TRIGGER trg_case_loan_delete;
GO

IF EXISTS (select o.name from sysobjects o where o.xtype = 'TR' and name = N'trg_case_loan_insert')
    DROP TRIGGER trg_case_loan_insert;
GO

IF EXISTS (select o.name from sysobjects o where o.xtype = 'TR' and name = N'trg_case_loan_update')
    DROP TRIGGER trg_case_loan_update;
GO

IF EXISTS (select o.name from sysobjects o where o.xtype = 'TR' and name = N'trg_foreclosure_case_insert')
    DROP TRIGGER trg_foreclosure_case_insert;
GO

IF EXISTS (select o.name from sysobjects o where o.xtype = 'TR' and name = N'trg_foreclosure_case_update')
    DROP TRIGGER trg_foreclosure_case_update;
GO


IF EXISTS (select o.name from sysobjects o where o.xtype = 'TR' and name = N'trg_foreclosure_case_update_audit')
    DROP TRIGGER trg_foreclosure_case_update_audit;
GO


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	
--		1) Create foreclosure_case.Loan_list 
--			When acct_num, servicer_id, or other_servicer_name changes, concatenate all the loan #'s and servicer names into one string and store in loan_list.
--		2)Insert data into activity_log table  
--			when acct_num have any update or insert
-- =============================================
CREATE TRIGGER [trg_case_loan_delete]
   ON  [dbo].[case_loan]
   AFTER DELETE
AS 
DECLARE	@v_deleted_loan varchar(100),
		@v_loan_list	varchar(500),
		@v_str			varchar(500),
		@v_fc_id		int,
		@v_old_loan_1st_2nd_cd	varchar(15),
		@v_old_acct_num				varchar(30),
		@v_summary_sent_other_cd	varchar(15),
		@v_activity_cd			varchar(15),
		@v_activity_note		varchar(2000);
BEGIN
	SET NOCOUNT ON;
	-- Task 1
	SELECT @v_Loan_list = f.loan_list
			, @v_deleted_loan = deleted.acct_num + ' - ' + s.servicer_name
			, @v_fc_id = deleted.fc_id
	FROM foreclosure_case f, deleted, servicer s
	WHERE f.fc_id = deleted.fc_id AND s.servicer_id = deleted.servicer_id;
	
	SELECT @v_str = replace(@v_loan_list, @v_deleted_loan, '');

	UPDATE	foreclosure_case 
	SET		loan_list = @v_str
	WHERE	fc_id = @v_fc_id;

	-- Task 2
	-- Change to 1st loan
	SELECT	@v_old_loan_1st_2nd_cd	= i.loan_1st_2nd_cd	
			, @v_old_acct_num = acct_num
			, @v_summary_sent_other_cd = f.summary_sent_other_cd
	FROM	deleted i, foreclosure_case f
	WHERE	i.fc_id = f.fc_id;

	IF (@v_old_loan_1st_2nd_cd IN ('1st', '2nd'))
	BEGIN
		SELECT @v_activity_cd = @v_old_loan_1st_2nd_cd + 'LOANDEL';
		SELECT @v_activity_note = @v_old_loan_1st_2nd_cd + ' - Loan #[' + @v_old_acct_num + '] Deleted';
			INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
				SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
				FROM	deleted i;
	END;

	-- A Servicer Summary is sent when Change of Loan#
	SELECT @v_activity_cd = 'SUMM';
	SELECT @v_activity_note = 'Summary sent via ' + @v_summary_sent_other_cd;
	INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
						, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	deleted i;		
END

USE [hpf]
GO
/****** Object:  Trigger [trg_case_loan_insert]    Script Date: 01/21/2009 11:27:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	
--		1) Create foreclosure_case.Loan_list 
--			When acct_num, servicer_id, or other_servicer_name changes, concatenate all the loan #'s and servicer names into one string and store in loan_list.
--		2) Write to activity_log
-- =============================================
CREATE TRIGGER [trg_case_loan_insert]
   ON  [dbo].[case_loan]
   AFTER INSERT
AS 
DECLARE	@v_Loan_list varchar(500), @v_fc_id int, 
		@v_new_loan_1st_2nd_cd	varchar(15),
		@v_new_acct_num			varchar(30),
		@v_activity_cd			varchar(15),
		@v_activity_note		varchar(2000);
BEGIN
	SET NOCOUNT ON;
	
	-- Task 1	
	SELECT @v_Loan_list = ' ';
	SELECT @v_Loan_list = isnull(loan_list, ' ') + inserted.acct_num + ' - ' + isnull(s.servicer_name, ' ') + '; ' 
			, @v_fc_id = inserted.fc_id
	FROM foreclosure_case f, inserted, servicer s
	WHERE f.fc_id = inserted.fc_id AND s.servicer_id = inserted.servicer_id;
	
	UPDATE	foreclosure_case 
	SET		loan_list = @v_loan_list
	WHERE	fc_id = @v_fc_id;

	-- Task 2
	-- Change to 1st loan
	SELECT	@v_new_loan_1st_2nd_cd	= i.loan_1st_2nd_cd	
			, @v_new_acct_num  = i.acct_num
	FROM	inserted i;	

	IF (@v_new_loan_1st_2nd_cd IN ('1st', '2nd'))
	BEGIN
		SELECT @v_activity_cd = @v_new_loan_1st_2nd_cd + 'LOANINS';
		SELECT @v_activity_note = @v_new_loan_1st_2nd_cd + ' - Loan #['+ @v_new_acct_num +'] Inserted';
			INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
				SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
				FROM	inserted i;
	END;
END
USE [hpf]
GO
/****** Object:  Trigger [trg_case_loan_update]    Script Date: 01/21/2009 11:27:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	
--		1)Insert data into activity_log table  
--			when servicer, loan_1st_2nd_cd have any update or insert
-- =============================================
CREATE TRIGGER [trg_case_loan_update]
   ON  [dbo].[case_loan]
   AFTER DELETE
AS 
DECLARE	@v_activity_cd			varchar(15),
		@v_activity_note		varchar(2000),
		@v_new_servicer_name	varchar(50),
		@v_new_loan_1st_2nd_cd	varchar(15),
		@v_old_servicer_name	varchar(50),
		@v_old_loan_1st_2nd_cd	varchar(15),
		@v_summary_sent_other_cd varchar(15)
BEGIN
	SET NOCOUNT ON;
	SELECT	@v_new_loan_1st_2nd_cd	= i.loan_1st_2nd_cd,
			@v_new_servicer_name	= s.servicer_name
	FROM	inserted i , servicer s
	WHERE	i.servicer_id = s.servicer_id;	

	SELECT	@v_old_loan_1st_2nd_cd	= d.loan_1st_2nd_cd,
			@v_old_servicer_name	= s.servicer_name,
			@v_summary_sent_other_cd = f.summary_sent_other_cd
	FROM	deleted d , servicer s, foreclosure_case f
	WHERE	d.servicer_id = s.servicer_id AND d.fc_id = f.fc_id;	

	-- Change of servicer_id
	IF (@v_old_servicer_name <> @v_new_servicer_name)
	BEGIN
		SELECT @v_activity_cd = 'SERV';
		SELECT @v_activity_note = 'Changed from ' + @v_old_servicer_name + ' to ' + @v_new_servicer_name;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;

		-- A Servicer Summary is sent when Change of servicer_id
		SELECT @v_activity_cd = 'SUMM';
		SELECT @v_activity_note = 'Summary sent via ' + @v_summary_sent_other_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
						, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	deleted i;
	END;

	-- Change of loan_1st_2nd_cd
	IF (@v_old_loan_1st_2nd_cd <> @v_new_loan_1st_2nd_cd)
	BEGIN
		SELECT @v_activity_cd = '1ST2ND';
		SELECT @v_activity_note = 'Changed from ' + @v_old_loan_1st_2nd_cd + ' to ' + @v_new_loan_1st_2nd_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;


END
USE [hpf]
GO
/****** Object:  Trigger [trg_foreclosure_case_insert]    Script Date: 01/21/2009 11:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	
--		1)Insert data into activity_log table  
--		when servicer_consent_ind or funding_consent_ind have any update or insert
-- =============================================
CREATE TRIGGER [trg_foreclosure_case_insert]
   ON  [dbo].[foreclosure_case]
   AFTER INSERT
AS 
DECLARE	@v_servicer_consent_ind varchar(1),
		@v_funding_consent_ind	varchar(1),
		@v_completed_dt			datetime,
		@v_summary_sent_other_cd varchar(15),
		@v_activity_cd			varchar(15),
		@v_borrower_SSN			varchar(9),
		@v_co_borrower_SSN		varchar(9),
		@v_borrower_last4_SSN	varchar(4),
		@v_co_borrower_last4_SSN varchar(4),
		@v_activity_note		varchar(2000);
BEGIN
	SET NOCOUNT ON;
	SELECT	@v_servicer_consent_ind = servicer_consent_ind
			,@v_funding_consent_ind = funding_consent_ind
			,@v_completed_dt = completed_dt
			,@v_summary_sent_other_cd = summary_sent_other_cd
			,@v_borrower_SSN  = borrower_SSN
			,@v_co_borrower_SSN  = co_borrower_SSN
			,@v_borrower_last4_SSN = RIGHT(borrower_SSN, 4)
			,@v_co_borrower_last4_SSN = RIGHT(co_borrower_SSN, 4)
	FROM	inserted;
-- Task 1: Insert data into Activity_log
	-- servicer_consent_ind or funding_consent_ind = 'N'
	SELECT @v_activity_note = 'Inserted as No';

	IF (@v_servicer_consent_ind = 'N')
		SELECT @v_activity_cd = 'SCONS';

	IF (@v_funding_consent_ind = 'N')
		SELECT @v_activity_cd = 'FCONS';

	IF (@v_activity_cd IS NOT NULL AND @v_activity_note IS NOT NULL)
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
						, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
				, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	
	-- A Servicer Summary is sent when case is completed
	IF (@v_completed_dt IS NOT NULL)
	BEGIN
		SELECT @v_activity_cd = 'SUMM';
		SELECT @v_activity_note = 'Summary sent via ' + @v_summary_sent_other_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
				SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
						, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
				FROM	inserted i;				
	END
END


USE [hpf]
GO
/****** Object:  Trigger [trg_foreclosure_case_update]    Script Date: 01/21/2009 11:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 07 Jan 2009
-- Project : HPF
-- Description:	
--		1) Insert data into activity_log table  
--		when servicer_consent_ind or funding_consent_ind have any update or insert
--		2) Get *_last4_SSN
--		3) Encryption *_SSN
-- =============================================
CREATE TRIGGER [trg_foreclosure_case_update]
   ON  [dbo].[foreclosure_case]
   AFTER UPDATE
AS 
DECLARE	@v_new_servicer_consent_ind varchar(1),
		@v_new_funding_consent_ind	varchar(1),
		@v_new_never_bill_reason_cd	varchar(15),
		@v_new_never_pay_reason_cd	varchar(15),
		@v_new_agency_name			varchar(50),
		@v_new_duplicate_ind		varchar(1),
		@v_new_completed_dt			datetime,
		@v_new_summary_sent_other_cd	varchar(15),
		@v_new_borrower_SSN			varchar(9),
		@v_new_co_borrower_SSN		varchar(9),
		@v_new_borrower_last4_SSN	varchar(4),
		@v_new_co_borrower_last4_SSN	varchar(4),
		@v_old_servicer_consent_ind varchar(1),
		@v_old_funding_consent_ind	varchar(1),
		@v_old_never_bill_reason_cd	varchar(15),
		@v_old_never_pay_reason_cd	varchar(15),
		@v_old_agency_name			varchar(50),
		@v_old_duplicate_ind		varchar(1),
		@v_old_borrower_SSN			varchar(9),
		@v_old_co_borrower_SSN		varchar(9),
		@v_old_borrower_last4_SSN	varchar(4),
		@v_old_co_borrower_last4_SSN	varchar(4),
		@v_activity_cd			varchar(15),
		@v_activity_note		varchar(2000);
BEGIN
	SET NOCOUNT ON;
	SELECT	@v_new_servicer_consent_ind = servicer_consent_ind
			, @v_new_funding_consent_ind = funding_consent_ind
			, @v_new_never_bill_reason_cd = never_bill_reason_cd
			, @v_new_never_pay_reason_cd = never_pay_reason_cd
			, @v_new_agency_name = a.agency_name
			, @v_new_duplicate_ind = duplicate_ind
			, @v_new_completed_dt = completed_dt
			, @v_new_summary_sent_other_cd = summary_sent_other_cd
			, @v_new_borrower_SSN = borrower_SSN
			, @v_new_co_borrower_SSN = co_borrower_SSN
			, @v_new_borrower_last4_SSN = RIGHT(borrower_SSN, 4)
			, @v_new_co_borrower_last4_SSN = RIGHT(co_borrower_SSN, 4)
	FROM	inserted i, agency a WHERE i.agency_id = a.agency_id;

	SELECT	@v_old_servicer_consent_ind = servicer_consent_ind
			, @v_old_funding_consent_ind = funding_consent_ind
			, @v_old_never_bill_reason_cd = never_bill_reason_cd
			, @v_old_never_pay_reason_cd = never_pay_reason_cd
			, @v_old_agency_name = a.agency_name
			, @v_old_duplicate_ind = duplicate_ind
			, @v_old_borrower_SSN = borrower_SSN
			, @v_old_co_borrower_SSN = co_borrower_SSN
			, @v_old_borrower_last4_SSN = RIGHT(borrower_SSN, 4)
			, @v_old_co_borrower_last4_SSN = RIGHT(co_borrower_SSN, 4)
	FROM	deleted , agency a WHERE deleted.agency_id = a.agency_id;

-- Task 1: Insert into activity_log
	-- Change of servicer_consent_ind
	IF (@v_old_servicer_consent_ind <> @v_new_servicer_consent_ind)
	BEGIN
		SELECT @v_activity_cd = 'SCONS';
		SELECT @v_activity_note = 'Changed from ' + @v_old_servicer_consent_ind + ' to ' + @v_new_servicer_consent_ind;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;

	-- Change of funding_consent_ind
	IF (@v_old_funding_consent_ind <> @v_new_funding_consent_ind)
	BEGIN
		SELECT @v_activity_cd = 'FCONS';
		SELECT @v_activity_note = 'Changed from ' + @v_old_funding_consent_ind + ' to ' + @v_new_funding_consent_ind;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;

	-- Change of never_bill_reason_cd
	IF (@v_old_never_bill_reason_cd <> @V_new_never_bill_reason_cd)
	BEGIN
		SELECT @v_activity_cd = 'NBILL';
		SELECT @v_activity_note = 'Changed from ' + @v_old_never_bill_reason_cd + ' to ' + @v_new_never_bill_reason_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;

	-- Change of never_pay_reason_cd
	IF (@v_old_never_pay_reason_cd <> @V_new_never_pay_reason_cd)
	BEGIN
		SELECT @v_activity_cd = 'NPAY';
		SELECT @v_activity_note = 'Changed from ' + @v_old_never_pay_reason_cd + ' to ' + @v_new_never_pay_reason_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;
	
	-- Change of Agency_id
	IF (@v_old_agency_name <> @v_new_agency_name)
	BEGIN
		SELECT @v_activity_cd = 'AGENCY';
		SELECT @v_activity_note = 'Changed from ' + @v_old_agency_name + ' to ' + @v_new_agency_name;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;

	-- Change of duplicate_ind
	IF (@v_old_duplicate_ind <> @v_new_duplicate_ind)
	BEGIN
		SELECT @v_activity_cd = 'DUPE';
		SELECT @v_activity_note = 'Changed from ' + @v_old_duplicate_ind + ' to ' + @v_new_duplicate_ind;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
			SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
					, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
			FROM	inserted i;
	END;

	-- A Servicer Summary is sent when case is completed
	IF (@v_new_completed_dt IS NOT NULL)
	BEGIN
		SELECT @v_activity_cd = 'SUMM';
		SELECT @v_activity_note = 'Summary sent via ' + @v_new_summary_sent_other_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
				SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
						, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
				FROM	inserted i;				
	END

-- Task 2: Get *_last4_SSN
	IF (@v_old_borrower_last4_SSN <> @v_new_borrower_last4_SSN)
		SELECT	borrower_last4_SSN = @v_new_borrower_last4_SSN	FROM	inserted;
	IF (@v_old_co_borrower_last4_SSN <> @v_new_co_borrower_last4_SSN)
		SELECT	co_borrower_last4_SSN = @v_new_co_borrower_last4_SSN FROM	inserted;

-- Task 3: Encryption *_SSN
	IF (@v_old_borrower_SSN <> @v_new_borrower_SSN)
		SELECT borrower_SSN = dbo.hpf_encryption (@v_new_borrower_SSN,'xyz123') FROM inserted;
	IF (@v_old_co_borrower_SSN <> @v_new_co_borrower_SSN)	
		SELECT co_borrower_SSN = dbo.hpf_encryption (@v_new_co_borrower_SSN,'xyz123')  FROM inserted;
END
USE [hpf]
GO
/****** Object:  Trigger [trg_foreclosure_case_update_audit]    Script Date: 01/21/2009 11:28:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quyen Nguyen
-- Create date: 12 Jan 2009
-- Description:	Audit Trigger for foreclosure_case table
-- =============================================
CREATE TRIGGER [trg_foreclosure_case_update_audit]
   ON  [dbo].[foreclosure_case]
   AFTER UPDATE
AS 
DECLARE
	@v_new_fc_id int
	,@v_new_agency_id int
	,@v_new_call_id int
	,@v_new_program_id int
	,@v_new_agency_case_num varchar(30)
	,@v_new_agency_client_num varchar(30)
	,@v_new_intake_dt datetime
	,@v_new_income_earners_cd varchar(15)
	,@v_new_case_source_cd varchar(15)
	,@v_new_race_cd varchar(15)
	,@v_new_household_cd varchar(15)
	,@v_new_never_bill_reason_cd varchar(15)
	,@v_new_never_pay_reason_cd varchar(15)
	,@v_new_dflt_reason_1st_cd varchar(15)
	,@v_new_dflt_reason_2nd_cd varchar(15)
	,@v_new_hud_termination_reason_cd varchar(15)
	,@v_new_hud_termination_dt datetime
	,@v_new_hud_outcome_cd varchar(15)
	,@v_new_AMI_percentage int
	,@v_new_counseling_duration_cd varchar(15)
	,@v_new_gender_cd varchar(15)
	,@v_new_borrower_fname varchar(30)
	,@v_new_borrower_lname varchar(30)
	,@v_new_borrower_mname varchar(30)
	,@v_new_mother_maiden_lname varchar(30)
	,@v_new_borrower_ssn varchar(9)
	,@v_new_borrower_last4_SSN varchar(4)
	,@v_new_borrower_DOB datetime
	,@v_new_co_borrower_fname varchar(30)
	,@v_new_co_borrower_lname varchar(30)
	,@v_new_co_borrower_mname varchar(30)
	,@v_new_co_borrower_ssn varchar(9)
	,@v_new_co_borrower_last4_SSN varchar(4)
	,@v_new_co_borrower_DOB datetime
	,@v_new_primary_contact_no varchar(20)
	,@v_new_second_contact_no varchar(20)
	,@v_new_email_1 varchar(50)
	,@v_new_contact_zip_plus4 varchar(4)
	,@v_new_email_2 varchar(50)
	,@v_new_contact_addr1 varchar(50)
	,@v_new_contact_addr2 varchar(50)
	,@v_new_contact_city varchar(30)
	,@v_new_contact_state_cd varchar(15)
	,@v_new_contact_zip varchar(5)
	,@v_new_prop_addr1 varchar(50)
	,@v_new_prop_addr2 varchar(50)
	,@v_new_prop_city varchar(30)
	,@v_new_prop_state_cd varchar(15)
	,@v_new_prop_zip varchar(5)
	,@v_new_prop_zip_plus_4 varchar(4)
	,@v_new_bankruptcy_ind varchar(1)
	,@v_new_bankruptcy_attorney varchar(50)
	,@v_new_bankruptcy_pmt_current_ind varchar(1)
	,@v_new_borrower_educ_level_completed_cd varchar(15)
	,@v_new_borrower_marital_status_cd varchar(15)
	,@v_new_borrower_preferred_lang_cd varchar(15)
	,@v_new_borrower_occupation varchar(50)
	,@v_new_co_borrower_occupation varchar(50)
	,@v_new_hispanic_ind varchar(1)
	,@v_new_duplicate_ind varchar(1)
	,@v_new_fc_notice_received_ind varchar(1)
	,@v_new_completed_dt datetime
	,@v_new_funding_consent_ind varchar(1)
	,@v_new_servicer_consent_ind varchar(1)
	,@v_new_agency_media_interest_ind varchar(1)
	,@v_new_hpf_media_candidate_ind varchar(1)	
	,@v_new_hpf_success_story_ind varchar(1)
	,@v_new_agency_success_story_ind varchar(1)
	,@v_new_borrower_disabled_ind varchar(1)
	,@v_new_co_borrower_disabled_ind varchar(1)
	,@v_new_summary_sent_other_cd varchar(15)
	,@v_new_summary_sent_other_dt datetime
	,@v_new_summary_sent_dt datetime
	,@v_new_occupant_num tinyint
	,@v_new_loan_dflt_reason_notes varchar(8000)
	,@v_new_action_items_notes varchar(8000)
	,@v_new_followup_notes varchar(8000)
	,@v_new_prim_res_est_mkt_value numeric(9)
	,@v_new_counselor_email varchar(50)
	,@v_new_counselor_phone varchar(20)
	,@v_new_counselor_ext varchar(20)
	,@v_new_discussed_solution_with_srvcr_ind varchar(1)
	,@v_new_worked_with_another_agency_ind varchar(1)
	,@v_new_contacted_srvcr_recently_ind varchar(1)
	,@v_new_has_workout_plan_ind varchar(1)
	,@v_new_srvcr_workout_plan_current_ind varchar(1)
	,@v_new_opt_out_newsletter_ind varchar(1)
	,@v_new_opt_out_survey_ind varchar(1)
	,@v_new_do_not_call_ind varchar(1)
	,@v_new_owner_occupied_ind varchar(1)
	,@v_new_primary_residence_ind varchar(1)
	,@v_new_realty_company varchar(50)
	,@v_new_property_cd varchar(15)
	,@v_new_for_sale_ind varchar(1)
	,@v_new_home_sale_price numeric(15, 2)
	,@v_new_home_purchase_year int
	,@v_new_home_purchase_price numeric(15, 2)
	,@v_new_home_current_market_value numeric(15, 2)
	,@v_new_military_service_cd varchar(15)
	,@v_new_household_gross_annual_income_amt numeric(15, 2)
	,@v_new_loan_list varchar(500)
	,@v_new_counselor_fname varchar(30)
	,@v_new_counselor_lname varchar(30)
	,@v_new_intake_credit_score varchar(4)
	,@v_new_intake_credit_bureau_cd varchar(15)
	,@v_new_counselor_id_ref varchar(30)
	,@v_new_fc_sale_dt datetime
	,@v_new_chg_lst_dt datetime
	,@v_new_chg_lst_user_id	varchar(30)
	,@v_new_chg_lst_app_name	varchar(20)

	,@v_old_agency_id int
	,@v_old_call_id int
	,@v_old_program_id int
	,@v_old_agency_case_num varchar(30)
	,@v_old_agency_client_num varchar(30)
	,@v_old_intake_dt datetime
	,@v_old_income_earners_cd varchar(15)
	,@v_old_case_source_cd varchar(15)
	,@v_old_race_cd varchar(15)
	,@v_old_household_cd varchar(15)
	,@v_old_never_bill_reason_cd varchar(15)
	,@v_old_never_pay_reason_cd varchar(15)
	,@v_old_dflt_reason_1st_cd varchar(15)
	,@v_old_dflt_reason_2nd_cd varchar(15)
	,@v_old_hud_termination_reason_cd varchar(15)
	,@v_old_hud_termination_dt datetime
	,@v_old_hud_outcome_cd varchar(15)
	,@v_old_AMI_percentage int
	,@v_old_counseling_duration_cd varchar(15)
	,@v_old_gender_cd varchar(15)
	,@v_old_borrower_fname varchar(30)
	,@v_old_borrower_lname varchar(30)
	,@v_old_borrower_mname varchar(30)
	,@v_old_mother_maiden_lname varchar(30)
	,@v_old_borrower_ssn varchar(9)
	,@v_old_borrower_last4_SSN varchar(4)
	,@v_old_borrower_DOB datetime
	,@v_old_co_borrower_fname varchar(30)
	,@v_old_co_borrower_lname varchar(30)
	,@v_old_co_borrower_mname varchar(30)
	,@v_old_co_borrower_ssn varchar(9)
	,@v_old_co_borrower_last4_SSN varchar(4)
	,@v_old_co_borrower_DOB datetime
	,@v_old_primary_contact_no varchar(20)
	,@v_old_second_contact_no varchar(20)
	,@v_old_email_1 varchar(50)
	,@v_old_contact_zip_plus4 varchar(4)
	,@v_old_email_2 varchar(50)
	,@v_old_contact_addr1 varchar(50)
	,@v_old_contact_addr2 varchar(50)
	,@v_old_contact_city varchar(30)
	,@v_old_contact_state_cd varchar(15)
	,@v_old_contact_zip varchar(5)
	,@v_old_prop_addr1 varchar(50)
	,@v_old_prop_addr2 varchar(50)
	,@v_old_prop_city varchar(30)
	,@v_old_prop_state_cd varchar(15)
	,@v_old_prop_zip varchar(5)
	,@v_old_prop_zip_plus_4 varchar(4)
	,@v_old_bankruptcy_ind varchar(1)
	,@v_old_bankruptcy_attorney varchar(50)
	,@v_old_bankruptcy_pmt_current_ind varchar(1)
	,@v_old_borrower_educ_level_completed_cd varchar(15)
	,@v_old_borrower_marital_status_cd varchar(15)
	,@v_old_borrower_preferred_lang_cd varchar(15)
	,@v_old_borrower_occupation varchar(50)
	,@v_old_co_borrower_occupation varchar(50)
	,@v_old_hispanic_ind varchar(1)
	,@v_old_duplicate_ind varchar(1)
	,@v_old_fc_notice_received_ind varchar(1)
	,@v_old_completed_dt datetime
	,@v_old_funding_consent_ind varchar(1)
	,@v_old_servicer_consent_ind varchar(1)
	,@v_old_agency_media_interest_ind varchar(1)
	,@v_old_hpf_media_candidate_ind varchar(1)
	,@v_old_hpf_success_story_ind varchar(1)
	,@v_old_agency_success_story_ind varchar(1)
	,@v_old_borrower_disabled_ind varchar(1)
	,@v_old_co_borrower_disabled_ind varchar(1)
	,@v_old_summary_sent_other_cd varchar(15)
	,@v_old_summary_sent_other_dt datetime
	,@v_old_summary_sent_dt datetime
	,@v_old_occupant_num tinyint
	,@v_old_loan_dflt_reason_notes varchar(8000)
	,@v_old_action_items_notes varchar(8000)
	,@v_old_followup_notes varchar(8000)
	,@v_old_prim_res_est_mkt_value numeric(9)
	,@v_old_counselor_email varchar(50)
	,@v_old_counselor_phone varchar(20)
	,@v_old_counselor_ext varchar(20)
	,@v_old_discussed_solution_with_srvcr_ind varchar(1)
	,@v_old_worked_with_another_agency_ind varchar(1)
	,@v_old_contacted_srvcr_recently_ind varchar(1)
	,@v_old_has_workout_plan_ind varchar(1)
	,@v_old_srvcr_workout_plan_current_ind varchar(1)
	,@v_old_opt_out_newsletter_ind varchar(1)
	,@v_old_opt_out_survey_ind varchar(1)
	,@v_old_do_not_call_ind varchar(1)
	,@v_old_owner_occupied_ind varchar(1)
	,@v_old_primary_residence_ind varchar(1)
	,@v_old_realty_company varchar(50)
	,@v_old_property_cd varchar(15)
	,@v_old_for_sale_ind varchar(1)
	,@v_old_home_sale_price numeric(15, 2)
	,@v_old_home_purchase_year int
	,@v_old_home_purchase_price numeric(15, 2)
	,@v_old_home_current_market_value numeric(15, 2)
	,@v_old_military_service_cd varchar(15)
	,@v_old_household_gross_annual_income_amt numeric(15, 2)
	,@v_old_loan_list varchar(500)
	,@v_old_counselor_fname varchar(30)
	,@v_old_counselor_lname varchar(30)
	,@v_old_intake_credit_score varchar(4)
	,@v_old_intake_credit_bureau_cd varchar(15)
	,@v_old_counselor_id_ref varchar(30)
	,@v_old_fc_sale_dt datetime
	
	,@v_audit_table		varchar(15)
	,@v_audit_col_name	varchar(50)
	,@v_audit_old_value	varchar(8000)
	,@v_audit_new_value	varchar(8000)
	,@v_create_dt		datetime
	,@v_create_user_id	varchar(30)
	,@v_create_app_name	varchar(20)
BEGIN
	SET NOCOUNT ON;
	SELECT	@v_new_fc_id = fc_id
			, @v_new_agency_id = agency_id
			, @v_new_call_id = call_id
			, @v_new_program_id = program_id
			, @v_new_agency_case_num = agency_case_num
			, @v_new_agency_client_num = agency_client_num
			, @v_new_intake_dt = intake_dt
			, @v_new_income_earners_cd = income_earners_cd
			, @v_new_case_source_cd = case_source_cd
			, @v_new_race_cd = race_cd
			, @v_new_household_cd = household_cd
			, @v_new_never_bill_reason_cd = never_bill_reason_cd
			, @v_new_never_pay_reason_cd = never_pay_reason_cd
			, @v_new_dflt_reason_1st_cd = dflt_reason_1st_cd
			, @v_new_dflt_reason_2nd_cd = dflt_reason_2nd_cd
			, @v_new_hud_termination_reason_cd = hud_termination_reason_cd
			, @v_new_hud_termination_dt = hud_termination_dt
			, @v_new_hud_outcome_cd = hud_outcome_cd
			, @v_new_AMI_percentage = AMI_percentage
			, @v_new_counseling_duration_cd = counseling_duration_cd
			, @v_new_gender_cd = gender_cd
			, @v_new_borrower_fname = borrower_fname
			, @v_new_borrower_lname = borrower_lname
			, @v_new_borrower_mname = borrower_mname
			, @v_new_mother_maiden_lname = mother_maiden_lname
			, @v_new_borrower_ssn = borrower_ssn
			, @v_new_borrower_last4_SSN = borrower_last4_SSN
			, @v_new_borrower_DOB = borrower_DOB
			, @v_new_co_borrower_fname = co_borrower_fname
			, @v_new_co_borrower_lname = co_borrower_lname
			, @v_new_co_borrower_mname = co_borrower_mname
			, @v_new_co_borrower_ssn = co_borrower_ssn
			, @v_new_co_borrower_last4_SSN = co_borrower_last4_SSN
			, @v_new_co_borrower_DOB = co_borrower_DOB
			, @v_new_primary_contact_no = primary_contact_no
			, @v_new_second_contact_no = second_contact_no
			, @v_new_email_1 = email_1
			, @v_new_contact_zip_plus4 = contact_zip_plus4
			, @v_new_email_2 = email_2
			, @v_new_contact_addr1 = contact_addr1
			, @v_new_contact_addr2 = contact_addr2
			, @v_new_contact_city = contact_city
			, @v_new_contact_state_cd = contact_state_cd
			, @v_new_contact_zip = contact_zip
			, @v_new_prop_addr1 = prop_addr1
			, @v_new_prop_addr2 = prop_addr2
			, @v_new_prop_city = prop_city
			, @v_new_prop_state_cd = prop_state_cd
			, @v_new_prop_zip = prop_zip
			, @v_new_prop_zip_plus_4 = prop_zip_plus_4
			, @v_new_bankruptcy_ind = bankruptcy_ind
			, @v_new_bankruptcy_attorney = bankruptcy_attorney
			, @v_new_bankruptcy_pmt_current_ind = bankruptcy_pmt_current_ind
			, @v_new_borrower_educ_level_completed_cd = borrower_educ_level_completed_cd
			, @v_new_borrower_marital_status_cd = borrower_marital_status_cd
			, @v_new_borrower_preferred_lang_cd = borrower_preferred_lang_cd
			, @v_new_borrower_occupation = borrower_occupation
			, @v_new_co_borrower_occupation = co_borrower_occupation
			, @v_new_hispanic_ind = hispanic_ind
			, @v_new_duplicate_ind = duplicate_ind
			, @v_new_fc_notice_received_ind = fc_notice_received_ind
			, @v_new_completed_dt = completed_dt
			, @v_new_funding_consent_ind = funding_consent_ind
			, @v_new_servicer_consent_ind = servicer_consent_ind
			, @v_new_agency_media_interest_ind = agency_media_interest_ind
			, @v_new_hpf_media_candidate_ind = hpf_media_candidate_ind
			, @v_new_hpf_success_story_ind = hpf_success_story_ind
			, @v_new_agency_success_story_ind = agency_success_story_ind
			, @v_new_borrower_disabled_ind = borrower_disabled_ind
			, @v_new_co_borrower_disabled_ind = co_borrower_disabled_ind
			, @v_new_summary_sent_other_cd = summary_sent_other_cd
			, @v_new_summary_sent_other_dt = summary_sent_other_dt
			, @v_new_summary_sent_dt = summary_sent_dt
			, @v_new_occupant_num = occupant_num
			, @v_new_loan_dflt_reason_notes = loan_dflt_reason_notes
			, @v_new_action_items_notes = action_items_notes
			, @v_new_followup_notes = followup_notes
			, @v_new_prim_res_est_mkt_value = prim_res_est_mkt_value
			, @v_new_counselor_email = counselor_email
			, @v_new_counselor_phone = counselor_phone
			, @v_new_counselor_ext = counselor_ext
			, @v_new_discussed_solution_with_srvcr_ind = discussed_solution_with_srvcr_ind
			, @v_new_worked_with_another_agency_ind = worked_with_another_agency_ind
			, @v_new_contacted_srvcr_recently_ind = contacted_srvcr_recently_ind
			, @v_new_has_workout_plan_ind = has_workout_plan_ind
			, @v_new_srvcr_workout_plan_current_ind = srvcr_workout_plan_current_ind
			, @v_new_opt_out_newsletter_ind = opt_out_newsletter_ind
			, @v_new_opt_out_survey_ind = opt_out_survey_ind
			, @v_new_do_not_call_ind = do_not_call_ind
			, @v_new_owner_occupied_ind = owner_occupied_ind
			, @v_new_primary_residence_ind = primary_residence_ind
			, @v_new_realty_company = realty_company
			, @v_new_property_cd = property_cd
			, @v_new_for_sale_ind = for_sale_ind
			, @v_new_home_sale_price = home_sale_price
			, @v_new_home_purchase_year = home_purchase_year
			, @v_new_home_purchase_price = home_purchase_price
			, @v_new_home_current_market_value = home_current_market_value
			, @v_new_military_service_cd = military_service_cd
			, @v_new_household_gross_annual_income_amt = household_gross_annual_income_amt
			, @v_new_loan_list = loan_list
			, @v_new_chg_lst_dt = chg_lst_dt
			, @v_new_chg_lst_user_id = chg_lst_user_id
			, @v_new_chg_lst_app_name = chg_lst_app_name
			, @v_new_counselor_fname = counselor_fname
			, @v_new_counselor_lname = counselor_lname
			, @v_new_intake_credit_score = intake_credit_score
			, @v_new_intake_credit_bureau_cd = intake_credit_bureau_cd
			, @v_new_counselor_id_ref = counselor_id_ref
			, @v_new_fc_sale_dt = fc_sale_dt
	FROM inserted;

	SELECT	@v_old_agency_id = agency_id
			, @v_old_call_id = call_id
			, @v_old_program_id = program_id
			, @v_old_agency_case_num = agency_case_num
			, @v_old_agency_client_num = agency_client_num
			, @v_old_intake_dt = intake_dt
			, @v_old_income_earners_cd = income_earners_cd
			, @v_old_case_source_cd = case_source_cd
			, @v_old_race_cd = race_cd
			, @v_old_household_cd = household_cd
			, @v_old_never_bill_reason_cd = never_bill_reason_cd
			, @v_old_never_pay_reason_cd = never_pay_reason_cd
			, @v_old_dflt_reason_1st_cd = dflt_reason_1st_cd
			, @v_old_dflt_reason_2nd_cd = dflt_reason_2nd_cd
			, @v_old_hud_termination_reason_cd = hud_termination_reason_cd
			, @v_old_hud_termination_dt = hud_termination_dt
			, @v_old_hud_outcome_cd = hud_outcome_cd
			, @v_old_AMI_percentage = AMI_percentage
			, @v_old_counseling_duration_cd = counseling_duration_cd
			, @v_old_gender_cd = gender_cd
			, @v_old_borrower_fname = borrower_fname
			, @v_old_borrower_lname = borrower_lname
			, @v_old_borrower_mname = borrower_mname
			, @v_old_mother_maiden_lname = mother_maiden_lname
			, @v_old_borrower_ssn = borrower_ssn
			, @v_old_borrower_last4_SSN = borrower_last4_SSN
			, @v_old_borrower_DOB = borrower_DOB
			, @v_old_co_borrower_fname = co_borrower_fname
			, @v_old_co_borrower_lname = co_borrower_lname
			, @v_old_co_borrower_mname = co_borrower_mname
			, @v_old_co_borrower_ssn = co_borrower_ssn
			, @v_old_co_borrower_last4_SSN = co_borrower_last4_SSN
			, @v_old_co_borrower_DOB = co_borrower_DOB
			, @v_old_primary_contact_no = primary_contact_no
			, @v_old_second_contact_no = second_contact_no
			, @v_old_email_1 = email_1
			, @v_old_contact_zip_plus4 = contact_zip_plus4
			, @v_old_email_2 = email_2
			, @v_old_contact_addr1 = contact_addr1
			, @v_old_contact_addr2 = contact_addr2
			, @v_old_contact_city = contact_city
			, @v_old_contact_state_cd = contact_state_cd
			, @v_old_contact_zip = contact_zip
			, @v_old_prop_addr1 = prop_addr1
			, @v_old_prop_addr2 = prop_addr2
			, @v_old_prop_city = prop_city
			, @v_old_prop_state_cd = prop_state_cd
			, @v_old_prop_zip = prop_zip
			, @v_old_prop_zip_plus_4 = prop_zip_plus_4
			, @v_old_bankruptcy_ind = bankruptcy_ind
			, @v_old_bankruptcy_attorney = bankruptcy_attorney
			, @v_old_bankruptcy_pmt_current_ind = bankruptcy_pmt_current_ind
			, @v_old_borrower_educ_level_completed_cd = borrower_educ_level_completed_cd
			, @v_old_borrower_marital_status_cd = borrower_marital_status_cd
			, @v_old_borrower_preferred_lang_cd = borrower_preferred_lang_cd
			, @v_old_borrower_occupation = borrower_occupation
			, @v_old_co_borrower_occupation = co_borrower_occupation
			, @v_old_hispanic_ind = hispanic_ind
			, @v_old_duplicate_ind = duplicate_ind
			, @v_old_fc_notice_received_ind = fc_notice_received_ind
			, @v_old_completed_dt = completed_dt
			, @v_old_funding_consent_ind = funding_consent_ind
			, @v_old_servicer_consent_ind = servicer_consent_ind
			, @v_old_agency_media_interest_ind = agency_media_interest_ind
			, @v_old_hpf_media_candidate_ind = hpf_media_candidate_ind			
			, @v_old_hpf_success_story_ind = hpf_success_story_ind
			, @v_old_agency_success_story_ind = agency_success_story_ind
			, @v_old_borrower_disabled_ind = borrower_disabled_ind
			, @v_old_co_borrower_disabled_ind = co_borrower_disabled_ind
			, @v_old_summary_sent_other_cd = summary_sent_other_cd
			, @v_old_summary_sent_other_dt = summary_sent_other_dt
			, @v_old_summary_sent_dt = summary_sent_dt
			, @v_old_occupant_num = occupant_num
			, @v_old_loan_dflt_reason_notes = loan_dflt_reason_notes
			, @v_old_action_items_notes = action_items_notes
			, @v_old_followup_notes = followup_notes
			, @v_old_prim_res_est_mkt_value = prim_res_est_mkt_value
			, @v_old_counselor_email = counselor_email
			, @v_old_counselor_phone = counselor_phone
			, @v_old_counselor_ext = counselor_ext
			, @v_old_discussed_solution_with_srvcr_ind = discussed_solution_with_srvcr_ind
			, @v_old_worked_with_another_agency_ind = worked_with_another_agency_ind
			, @v_old_contacted_srvcr_recently_ind = contacted_srvcr_recently_ind
			, @v_old_has_workout_plan_ind = has_workout_plan_ind
			, @v_old_srvcr_workout_plan_current_ind = srvcr_workout_plan_current_ind
			, @v_old_opt_out_newsletter_ind = opt_out_newsletter_ind
			, @v_old_opt_out_survey_ind = opt_out_survey_ind
			, @v_old_do_not_call_ind = do_not_call_ind
			, @v_old_owner_occupied_ind = owner_occupied_ind
			, @v_old_primary_residence_ind = primary_residence_ind
			, @v_old_realty_company = realty_company
			, @v_old_property_cd = property_cd
			, @v_old_for_sale_ind = for_sale_ind
			, @v_old_home_sale_price = home_sale_price
			, @v_old_home_purchase_year = home_purchase_year
			, @v_old_home_purchase_price = home_purchase_price
			, @v_old_home_current_market_value = home_current_market_value
			, @v_old_military_service_cd = military_service_cd
			, @v_old_household_gross_annual_income_amt = household_gross_annual_income_amt
			, @v_old_loan_list = loan_list
			, @v_old_counselor_fname = counselor_fname
			, @v_old_counselor_lname = counselor_lname
			, @v_old_intake_credit_score = intake_credit_score
			, @v_old_intake_credit_bureau_cd = intake_credit_bureau_cd
			, @v_old_counselor_id_ref = counselor_id_ref
			, @v_old_fc_sale_dt = fc_sale_dt
	FROM deleted;

	SELECT	@v_create_dt		= @v_new_chg_lst_dt
			,@v_create_user_id	= @v_new_chg_lst_user_id
			,@v_create_app_name	= @v_new_chg_lst_app_name
			,@v_audit_table		= 'foreclosure_case';
	IF (@v_new_agency_id <> @v_old_agency_id)
	BEGIN
		SELECT	@v_audit_col_name = 'agency_id'
				, @v_audit_old_value = cast(@v_old_agency_id as varchar(10))
				, @v_audit_new_value = cast(@v_new_agency_id as varchar(10))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_call_id <> @v_old_call_id)
	BEGIN
		SELECT	@v_audit_col_name = 'call_id'
				, @v_audit_old_value = cast(@v_old_call_id as varchar(10))
				, @v_audit_new_value = cast(@v_new_call_id as varchar(10))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_program_id <> @v_old_program_id)
	BEGIN
		SELECT	@v_audit_col_name = 'program_id'
				, @v_audit_old_value = cast(@v_old_program_id as varchar(10))
				, @v_audit_new_value = cast(@v_new_program_id as varchar(10))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_agency_case_num <> @v_old_agency_case_num)
	BEGIN
		SELECT	@v_audit_col_name = 'agency_case_num'
				, @v_audit_old_value = @v_old_agency_case_num
				, @v_audit_new_value = @v_new_agency_case_num
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_agency_client_num <> @v_old_agency_client_num)
	BEGIN
		SELECT	@v_audit_col_name = 'agency_client_num'
				, @v_audit_old_value = @v_old_agency_client_num
				, @v_audit_new_value = @v_new_agency_client_num
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_intake_dt <> @v_old_intake_dt)
	BEGIN
		SELECT	@v_audit_col_name = 'intake_dt'
				, @v_audit_old_value = cast(@v_old_intake_dt as varchar(20))
				, @v_audit_new_value = cast(@v_new_intake_dt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_income_earners_cd <> @v_old_income_earners_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'income_earners_cd'
				, @v_audit_old_value = @v_old_income_earners_cd
				, @v_audit_new_value = @v_new_income_earners_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_case_source_cd <> @v_old_case_source_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'case_source_cd'
				, @v_audit_old_value = @v_old_case_source_cd
				, @v_audit_new_value = @v_new_case_source_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_race_cd <> @v_old_race_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'race_cd'
				, @v_audit_old_value = @v_old_race_cd
				, @v_audit_new_value = @v_new_race_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_household_cd <> @v_old_household_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'household_cd'
				, @v_audit_old_value = @v_old_household_cd
				, @v_audit_new_value = @v_new_household_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_never_bill_reason_cd <> @v_old_never_bill_reason_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'never_bill_reason_cd'
				, @v_audit_old_value = @v_old_never_bill_reason_cd
				, @v_audit_new_value = @v_new_never_bill_reason_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_never_pay_reason_cd <> @v_old_never_pay_reason_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'never_pay_reason_cd'
				, @v_audit_old_value = @v_old_never_pay_reason_cd
				, @v_audit_new_value = @v_new_never_pay_reason_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_dflt_reason_1st_cd <> @v_old_dflt_reason_1st_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'dflt_reason_1st_cd'
				, @v_audit_old_value = @v_old_dflt_reason_1st_cd
				, @v_audit_new_value = @v_new_dflt_reason_1st_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_dflt_reason_2nd_cd <> @v_old_dflt_reason_2nd_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'dflt_reason_2nd_cd'
				, @v_audit_old_value = @v_old_dflt_reason_2nd_cd
				, @v_audit_new_value = @v_new_dflt_reason_2nd_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_hud_termination_reason_cd <> @v_old_hud_termination_reason_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'hud_termination_reason_cd'
				, @v_audit_old_value = @v_old_hud_termination_reason_cd
				, @v_audit_new_value = @v_new_hud_termination_reason_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_hud_termination_dt <> @v_old_hud_termination_dt)
	BEGIN
		SELECT	@v_audit_col_name = 'hud_termination_dt'
				, @v_audit_old_value = cast(@v_old_hud_termination_dt as varchar(20))
				, @v_audit_new_value = cast(@v_new_hud_termination_dt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;	

	IF (@v_new_hud_outcome_cd <> @v_old_hud_outcome_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'hud_outcome_cd'
				, @v_audit_old_value = @v_old_hud_outcome_cd
				, @v_audit_new_value = @v_new_hud_outcome_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
--	IF (@v_new_AMI_percentage <> @v_old_AMI_percentage)
	IF (@v_new_counseling_duration_cd <> @v_old_counseling_duration_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'counseling_duration_cd'
				, @v_audit_old_value = @v_old_counseling_duration_cd
				, @v_audit_new_value = @v_new_counseling_duration_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_gender_cd <> @v_old_gender_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'gender_cd'
				, @v_audit_old_value = @v_old_gender_cd
				, @v_audit_new_value = @v_new_gender_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_fname <> @v_old_borrower_fname)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_fname'
				, @v_audit_old_value = @v_old_borrower_fname
				, @v_audit_new_value = @v_new_borrower_fname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_lname <> @v_old_borrower_lname)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_lname'
				, @v_audit_old_value = @v_old_borrower_lname
				, @v_audit_new_value = @v_new_borrower_lname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_mname <> @v_old_borrower_mname)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_mname'
				, @v_audit_old_value = @v_old_borrower_mname
				, @v_audit_new_value = @v_new_borrower_mname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_mother_maiden_lname <> @v_old_mother_maiden_lname)
	BEGIN
		SELECT	@v_audit_col_name = 'mother_maiden_lname'
				, @v_audit_old_value = @v_old_mother_maiden_lname
				, @v_audit_new_value = @v_new_mother_maiden_lname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
/*	IF (@v_new_borrower_ssn <> @v_old_borrower_ssn) ???*/
	IF (@v_new_borrower_last4_SSN <> @v_old_borrower_last4_SSN)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_last4_SSN'
				, @v_audit_old_value = @v_old_borrower_last4_SSN
				, @v_audit_new_value = @v_new_borrower_last4_SSN
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_DOB <> @v_old_borrower_DOB)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_DOB'
				, @v_audit_old_value = cast(@v_old_borrower_DOB as varchar(20))
				, @v_audit_new_value = cast(@v_new_borrower_DOB as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_co_borrower_fname <> @v_old_co_borrower_fname)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_fname'
				, @v_audit_old_value = @v_old_co_borrower_fname
				, @v_audit_new_value = @v_new_co_borrower_fname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_co_borrower_lname <> @v_old_co_borrower_lname)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_lname'
				, @v_audit_old_value = @v_old_co_borrower_lname
				, @v_audit_new_value = @v_new_co_borrower_lname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_co_borrower_mname <> @v_old_co_borrower_mname)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_mname'
				, @v_audit_old_value = @v_old_co_borrower_mname
				, @v_audit_new_value = @v_new_co_borrower_mname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
/*	IF (@v_new_co_borrower_ssn <> @v_old_co_borrower_ssn) ???*/
	IF (@v_new_co_borrower_last4_SSN <> @v_old_co_borrower_last4_SSN)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_last4_SSN'
				, @v_audit_old_value = @v_old_co_borrower_last4_SSN
				, @v_audit_new_value = @v_new_co_borrower_last4_SSN
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_co_borrower_DOB <> @v_old_co_borrower_DOB)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_DOB'
				, @v_audit_old_value = cast(@v_old_co_borrower_DOB as varchar(20))
				, @v_audit_new_value = cast(@v_new_co_borrower_DOB as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_primary_contact_no <> @v_old_primary_contact_no)
	BEGIN
		SELECT	@v_audit_col_name = 'primary_contact_no'
				, @v_audit_old_value = @v_old_primary_contact_no
				, @v_audit_new_value = @v_new_primary_contact_no
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_second_contact_no <> @v_old_second_contact_no)
	BEGIN
		SELECT	@v_audit_col_name = 'second_contact_no'
				, @v_audit_old_value = @v_old_second_contact_no
				, @v_audit_new_value = @v_new_second_contact_no
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_email_1 <> @v_old_email_1)
	BEGIN
		SELECT	@v_audit_col_name = 'email_1'
				, @v_audit_old_value = @v_old_email_1
				, @v_audit_new_value = @v_new_email_1
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contact_zip_plus4 <> @v_old_contact_zip_plus4)
	BEGIN
		SELECT	@v_audit_col_name = 'contact_zip_plus4'
				, @v_audit_old_value = @v_old_contact_zip_plus4
				, @v_audit_new_value = @v_new_contact_zip_plus4
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_email_2 <> @v_old_email_2)
	BEGIN
		SELECT	@v_audit_col_name = 'email_2'
				, @v_audit_old_value = @v_old_email_2
				, @v_audit_new_value = @v_new_email_2
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contact_addr1 <> @v_old_contact_addr1)
	BEGIN
		SELECT	@v_audit_col_name = 'contact_addr1'
				, @v_audit_old_value = @v_old_contact_addr1
				, @v_audit_new_value = @v_new_contact_addr1
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contact_addr2 <> @v_old_contact_addr2)
	BEGIN
		SELECT	@v_audit_col_name = 'contact_addr2'
				, @v_audit_old_value = @v_old_contact_addr2
				, @v_audit_new_value = @v_new_contact_addr2
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contact_city <> @v_old_contact_city)
	BEGIN
		SELECT	@v_audit_col_name = 'contact_city'
				, @v_audit_old_value = @v_old_contact_city
				, @v_audit_new_value = @v_new_contact_city
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contact_state_cd <> @v_old_contact_state_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'contact_state_cd'
				, @v_audit_old_value = @v_old_contact_state_cd
				, @v_audit_new_value = @v_new_contact_state_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contact_zip <> @v_old_contact_zip)
	BEGIN
		SELECT	@v_audit_col_name = 'contact_zip'
				, @v_audit_old_value = @v_old_contact_zip
				, @v_audit_new_value = @v_new_contact_zip
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_prop_addr1 <> @v_old_prop_addr1)
	BEGIN
		SELECT	@v_audit_col_name = 'prop_addr1'
				, @v_audit_old_value = @v_old_prop_addr1
				, @v_audit_new_value = @v_new_prop_addr1
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_prop_addr2 <> @v_old_prop_addr2)
	BEGIN
		SELECT	@v_audit_col_name = 'prop_addr2'
				, @v_audit_old_value = @v_old_prop_addr2
				, @v_audit_new_value = @v_new_prop_addr2
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

	IF (@v_new_prop_city <> @v_old_prop_city)
	BEGIN
		SELECT	@v_audit_col_name = 'prop_city'
				, @v_audit_old_value = @v_old_prop_city
				, @v_audit_new_value = @v_new_prop_city
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_prop_state_cd <> @v_old_prop_state_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'prop_state_cd'
				, @v_audit_old_value = @v_old_prop_state_cd
				, @v_audit_new_value = @v_new_prop_state_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_prop_zip <> @v_old_prop_zip)
	BEGIN
		SELECT	@v_audit_col_name = 'prop_zip'
				, @v_audit_old_value = @v_old_prop_zip
				, @v_audit_new_value = @v_new_prop_zip
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_prop_zip_plus_4 <> @v_old_prop_zip_plus_4)
	BEGIN
		SELECT	@v_audit_col_name = 'prop_zip_plus_4'
				, @v_audit_old_value = @v_old_prop_zip_plus_4
				, @v_audit_new_value = @v_new_prop_zip_plus_4
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_bankruptcy_ind <> @v_old_bankruptcy_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'bankruptcy_ind'
				, @v_audit_old_value = @v_old_bankruptcy_ind
				, @v_audit_new_value = @v_new_bankruptcy_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_bankruptcy_attorney <> @v_old_bankruptcy_attorney)
	BEGIN
		SELECT	@v_audit_col_name = 'bankruptcy_attorney'
				, @v_audit_old_value = @v_old_bankruptcy_attorney
				, @v_audit_new_value = @v_new_bankruptcy_attorney
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_bankruptcy_pmt_current_ind <> @v_old_bankruptcy_pmt_current_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'bankruptcy_pmt_current_ind'
				, @v_audit_old_value = @v_old_bankruptcy_pmt_current_ind
				, @v_audit_new_value = @v_new_bankruptcy_pmt_current_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_educ_level_completed_cd <> @v_old_borrower_educ_level_completed_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_educ_level_completed_cd'
				, @v_audit_old_value = @v_old_borrower_educ_level_completed_cd
				, @v_audit_new_value = @v_new_borrower_educ_level_completed_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_marital_status_cd <> @v_old_borrower_marital_status_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_marital_status_cd'
				, @v_audit_old_value = @v_old_borrower_marital_status_cd
				, @v_audit_new_value = @v_new_borrower_marital_status_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_preferred_lang_cd <> @v_old_borrower_preferred_lang_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_preferred_lang_cd'
				, @v_audit_old_value = @v_old_borrower_preferred_lang_cd
				, @v_audit_new_value = @v_new_borrower_preferred_lang_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_occupation <> @v_old_borrower_occupation)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_occupation'
				, @v_audit_old_value = @v_old_borrower_occupation
				, @v_audit_new_value = @v_new_borrower_occupation
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_co_borrower_occupation <> @v_old_co_borrower_occupation)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_occupation'
				, @v_audit_old_value = @v_old_co_borrower_occupation
				, @v_audit_new_value = @v_new_co_borrower_occupation
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_hispanic_ind <> @v_old_hispanic_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'hispanic_ind'
				, @v_audit_old_value = @v_old_hispanic_ind
				, @v_audit_new_value = @v_new_hispanic_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_duplicate_ind <> @v_old_duplicate_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'duplicate_ind'
				, @v_audit_old_value = @v_old_duplicate_ind
				, @v_audit_new_value = @v_new_duplicate_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_fc_notice_received_ind <> @v_old_fc_notice_received_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'fc_notice_received_ind'
				, @v_audit_old_value = @v_old_fc_notice_received_ind
				, @v_audit_new_value = @v_new_fc_notice_received_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	
	IF (@v_new_completed_dt <> @v_old_completed_dt)
	BEGIN
		SELECT	@v_audit_col_name = 'completed_dt'
				, @v_audit_old_value = cast(@v_old_completed_dt as varchar(20))
				, @v_audit_new_value = cast(@v_new_completed_dt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_funding_consent_ind <> @v_old_funding_consent_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'funding_consent_ind'
				, @v_audit_old_value = @v_old_funding_consent_ind
				, @v_audit_new_value = @v_new_funding_consent_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_servicer_consent_ind <> @v_old_servicer_consent_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'servicer_consent_ind'
				, @v_audit_old_value = @v_old_servicer_consent_ind
				, @v_audit_new_value = @v_new_servicer_consent_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_agency_media_interest_ind <> @v_old_agency_media_interest_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'agency_media_interest_ind'
				, @v_audit_old_value = @v_old_agency_media_interest_ind
				, @v_audit_new_value = @v_new_agency_media_interest_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_hpf_media_candidate_ind <> @v_old_hpf_media_candidate_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'hpf_media_candidate_ind'
				, @v_audit_old_value = @v_old_hpf_media_candidate_ind
				, @v_audit_new_value = @v_new_hpf_media_candidate_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_hpf_success_story_ind <> @v_old_hpf_success_story_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'hpf_success_story_ind'
				, @v_audit_old_value = @v_old_hpf_success_story_ind
				, @v_audit_new_value = @v_new_hpf_success_story_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_agency_success_story_ind <> @v_old_agency_success_story_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'agency_success_story_ind'
				, @v_audit_old_value = @v_old_agency_success_story_ind
				, @v_audit_new_value = @v_new_agency_success_story_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_borrower_disabled_ind <> @v_old_borrower_disabled_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'borrower_disabled_ind'
				, @v_audit_old_value = @v_old_borrower_disabled_ind
				, @v_audit_new_value = @v_new_borrower_disabled_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_co_borrower_disabled_ind <> @v_old_co_borrower_disabled_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'co_borrower_disabled_ind'
				, @v_audit_old_value = @v_old_co_borrower_disabled_ind
				, @v_audit_new_value = @v_new_co_borrower_disabled_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_summary_sent_other_cd <> @v_old_summary_sent_other_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'summary_sent_other_cd'
				, @v_audit_old_value = @v_old_summary_sent_other_cd
				, @v_audit_new_value = @v_new_summary_sent_other_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_summary_sent_other_dt <> @v_old_summary_sent_other_dt)
	BEGIN
		SELECT	@v_audit_col_name = 'summary_sent_other_dt'
				, @v_audit_old_value = cast(@v_old_summary_sent_other_dt as varchar(20))
				, @v_audit_new_value = cast(@v_new_summary_sent_other_dt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_summary_sent_dt <> @v_old_summary_sent_dt)
	BEGIN
		SELECT	@v_audit_col_name = 'summary_sent_dt'
				, @v_audit_old_value = cast(@v_old_summary_sent_dt as varchar(20))
				, @v_audit_new_value = cast(@v_new_summary_sent_dt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_occupant_num <> @v_old_occupant_num)
	BEGIN
		SELECT	@v_audit_col_name = 'occupant_num'
				, @v_audit_old_value = cast(@v_old_occupant_num as varchar(20))
				, @v_audit_new_value = cast(@v_new_occupant_num as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_loan_dflt_reason_notes <> @v_old_loan_dflt_reason_notes)
	BEGIN
		SELECT	@v_audit_col_name = 'loan_dflt_reason_notes'
				, @v_audit_old_value = @v_old_loan_dflt_reason_notes
				, @v_audit_new_value = @v_new_loan_dflt_reason_notes
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_action_items_notes <> @v_old_action_items_notes)
	BEGIN
		SELECT	@v_audit_col_name = 'action_items_notes'
				, @v_audit_old_value = @v_old_action_items_notes
				, @v_audit_new_value = @v_new_action_items_notes
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_followup_notes <> @v_old_followup_notes)
	BEGIN
		SELECT	@v_audit_col_name = 'followup_notes'
				, @v_audit_old_value = @v_old_followup_notes
				, @v_audit_new_value = @v_new_followup_notes
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_prim_res_est_mkt_value <> @v_old_prim_res_est_mkt_value)
	BEGIN
		SELECT	@v_audit_col_name = 'prim_res_est_mkt_value'
				, @v_audit_old_value = cast(@v_old_prim_res_est_mkt_value  as varchar(20))
				, @v_audit_new_value = cast(@v_new_prim_res_est_mkt_value as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_counselor_email <> @v_old_counselor_email)
	BEGIN
		SELECT	@v_audit_col_name = 'counselor_email'
				, @v_audit_old_value = @v_old_counselor_email
				, @v_audit_new_value = @v_new_counselor_email
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_counselor_phone <> @v_old_counselor_phone)
	BEGIN
		SELECT	@v_audit_col_name = 'counselor_phone'
				, @v_audit_old_value = @v_old_counselor_phone
				, @v_audit_new_value = @v_new_counselor_phone
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_counselor_ext <> @v_old_counselor_ext)
	BEGIN
		SELECT	@v_audit_col_name = 'counselor_ext'
				, @v_audit_old_value = @v_old_counselor_ext
				, @v_audit_new_value = @v_new_counselor_ext
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_discussed_solution_with_srvcr_ind <> @v_old_discussed_solution_with_srvcr_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'discussed_solution_with_srvcr_ind'
				, @v_audit_old_value = @v_old_discussed_solution_with_srvcr_ind
				, @v_audit_new_value = @v_new_discussed_solution_with_srvcr_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_worked_with_another_agency_ind <> @v_old_worked_with_another_agency_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'worked_with_another_agency_ind'
				, @v_audit_old_value = @v_old_worked_with_another_agency_ind
				, @v_audit_new_value = @v_new_worked_with_another_agency_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_contacted_srvcr_recently_ind <> @v_old_contacted_srvcr_recently_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'contacted_srvcr_recently_ind'
				, @v_audit_old_value = @v_old_contacted_srvcr_recently_ind
				, @v_audit_new_value = @v_new_contacted_srvcr_recently_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_has_workout_plan_ind <> @v_old_has_workout_plan_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'has_workout_plan_ind'
				, @v_audit_old_value = @v_old_has_workout_plan_ind
				, @v_audit_new_value = @v_new_has_workout_plan_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_srvcr_workout_plan_current_ind <> @v_old_srvcr_workout_plan_current_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'srvcr_workout_plan_current_ind'
				, @v_audit_old_value = @v_old_srvcr_workout_plan_current_ind
				, @v_audit_new_value = @v_new_srvcr_workout_plan_current_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_opt_out_newsletter_ind <> @v_old_opt_out_newsletter_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'opt_out_newsletter_ind'
				, @v_audit_old_value = @v_old_opt_out_newsletter_ind
				, @v_audit_new_value = @v_new_opt_out_newsletter_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_opt_out_survey_ind <> @v_old_opt_out_survey_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'opt_out_survey_ind'
				, @v_audit_old_value = @v_old_opt_out_survey_ind
				, @v_audit_new_value = @v_new_opt_out_survey_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_do_not_call_ind <> @v_old_do_not_call_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'do_not_call_ind'
				, @v_audit_old_value = @v_old_do_not_call_ind
				, @v_audit_new_value = @v_new_do_not_call_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_owner_occupied_ind <> @v_old_owner_occupied_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'owner_occupied_ind'
				, @v_audit_old_value = @v_old_owner_occupied_ind
				, @v_audit_new_value = @v_new_owner_occupied_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_primary_residence_ind <> @v_old_primary_residence_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'primary_residence_ind'
				, @v_audit_old_value = @v_old_primary_residence_ind
				, @v_audit_new_value = @v_new_primary_residence_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_realty_company <> @v_old_realty_company)
	BEGIN
		SELECT	@v_audit_col_name = 'realty_company'
				, @v_audit_old_value = @v_old_realty_company
				, @v_audit_new_value = @v_new_realty_company
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_property_cd <> @v_old_property_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'property_cd'
				, @v_audit_old_value = @v_old_property_cd
				, @v_audit_new_value = @v_new_property_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_for_sale_ind <> @v_old_for_sale_ind)
	BEGIN
		SELECT	@v_audit_col_name = 'for_sale_ind'
				, @v_audit_old_value = @v_old_for_sale_ind
				, @v_audit_new_value = @v_new_for_sale_ind
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_home_sale_price <> @v_old_home_sale_price)
	BEGIN
		SELECT	@v_audit_col_name = 'home_sale_price'
				, @v_audit_old_value = cast(@v_old_home_sale_price as varchar(20))
				, @v_audit_new_value = cast(@v_new_home_sale_price as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_home_purchase_year <> @v_old_home_purchase_year)
	BEGIN
		SELECT	@v_audit_col_name = 'home_purchase_year'
				, @v_audit_old_value = cast(@v_old_home_purchase_year as varchar(4))
				, @v_audit_new_value = cast(@v_new_home_purchase_year as varchar(4))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_home_purchase_price <> @v_old_home_purchase_price)
	BEGIN
		SELECT	@v_audit_col_name = 'home_purchase_price'
				, @v_audit_old_value = cast(@v_old_home_purchase_price as varchar(20))
				, @v_audit_new_value = cast(@v_new_home_purchase_price  as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_home_current_market_value <> @v_old_home_current_market_value)
	BEGIN
		SELECT	@v_audit_col_name = 'home_current_market_value'
				, @v_audit_old_value = cast(@v_old_home_current_market_value as varchar(20))
				, @v_audit_new_value = cast(@v_new_home_current_market_value as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_military_service_cd <> @v_old_military_service_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'military_service_cd'
				, @v_audit_old_value = @v_old_military_service_cd
				, @v_audit_new_value = @v_new_military_service_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_household_gross_annual_income_amt <> @v_old_household_gross_annual_income_amt)
	BEGIN
		SELECT	@v_audit_col_name = 'household_gross_annual_income_amt'
				, @v_audit_old_value = cast(@v_old_household_gross_annual_income_amt as varchar(20))
				, @v_audit_new_value = cast(@v_new_household_gross_annual_income_amt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_loan_list <> @v_old_loan_list)
	BEGIN
		SELECT	@v_audit_col_name = 'loan_list'
				, @v_audit_old_value = @v_old_loan_list
				, @v_audit_new_value = @v_new_loan_list
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_counselor_fname <> @v_old_counselor_fname)
	BEGIN
		SELECT	@v_audit_col_name = 'counselor_fname'
				, @v_audit_old_value = @v_old_counselor_fname
				, @v_audit_new_value = @v_new_counselor_fname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_counselor_lname <> @v_old_counselor_lname)
	BEGIN
		SELECT	@v_audit_col_name = 'counselor_lname'
				, @v_audit_old_value = @v_old_counselor_lname
				, @v_audit_new_value = @v_new_counselor_lname
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_intake_credit_score <> @v_old_intake_credit_score)
	BEGIN
		SELECT	@v_audit_col_name = 'intake_credit_score'
				, @v_audit_old_value = @v_old_intake_credit_score
				, @v_audit_new_value = @v_new_intake_credit_score
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_intake_credit_bureau_cd <> @v_old_intake_credit_bureau_cd)
	BEGIN
		SELECT	@v_audit_col_name = 'intake_credit_bureau_cd'
				, @v_audit_old_value = @v_old_intake_credit_bureau_cd
				, @v_audit_new_value = @v_new_intake_credit_bureau_cd
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_counselor_id_ref <> @v_old_counselor_id_ref)
	BEGIN
		SELECT	@v_audit_col_name = 'counselor_id_ref'
				, @v_audit_old_value = @v_old_counselor_id_ref
				, @v_audit_new_value = @v_new_counselor_id_ref
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;
	IF (@v_new_fc_sale_dt <> @v_old_fc_sale_dt)
	BEGIN
		SELECT	@v_audit_col_name = 'fc_sale_dt'
				, @v_audit_old_value = cast(@v_old_fc_sale_dt as varchar(20))
				, @v_audit_new_value = cast(@v_new_fc_sale_dt as varchar(20))
		INSERT INTO change_audit(audit_dt,audit_table,audit_record_key,audit_col_name,audit_old_value,audit_new_value,create_dt,create_user_id,create_app_name)
		VALUES (getdate(),@v_audit_table,@v_new_fc_id, @v_audit_col_name, @v_audit_old_value, @v_audit_new_value,@v_create_dt,@v_create_user_id,@v_create_app_name);
	END;

END
