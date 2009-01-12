-- =============================================
-- Create date: 12 Jan 2009
-- Project : HPF 
-- Build 
-- Description:	Create triggers are being used in HPF Reports
-- =============================================
USE HPF 
GO

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
--		2) Get *_last4_SSN
--		3) Encryption *_SSN
-- =============================================
CREATE TRIGGER [trg_foreclosure_case_insert]
   ON  [dbo].[foreclosure_case]
   AFTER INSERT
AS 
DECLARE	@v_servicer_consent_ind varchar(1),
		@v_funding_consent_ind	varchar(1),
		@v_case_complete_ind	varchar(1),
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
			,@v_case_complete_ind = case_complete_ind
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
	IF (@v_case_complete_ind = 'Y')
	BEGIN
		SELECT @v_activity_cd = 'SUMM';
		SELECT @v_activity_note = 'Summary sent via ' + @v_summary_sent_other_cd;
		INSERT INTO activity_log (fc_id, activity_cd, activity_dt, activity_note
							, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
				SELECT	i.fc_id, @v_activity_cd, getdate(), @v_activity_note
						, i.create_dt, i.create_user_id, i.create_app_name, i.chg_lst_dt, i.chg_lst_user_id, i.chg_lst_app_name	
				FROM	inserted i;				
	END
-- Task 2:  get *_last4_SSN
	SELECT	borrower_last4_SSN = @v_borrower_last4_SSN
			, co_borrower_last4_SSN = @v_co_borrower_last4_SSN
	FROM	inserted;
-- Task 3: Encryption *_SSN
	SELECT borrower_SSN = dbo.hpf_encryption (@v_borrower_SSN,'xyz123') FROM inserted;
	SELECT co_borrower_SSN = dbo.hpf_encryption (@v_co_borrower_SSN,'xyz123')  FROM inserted;
END
GO

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
		@v_new_case_complete_ind		varchar(1),
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
			, @v_new_case_complete_ind = case_complete_ind
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
	IF (@v_new_case_complete_ind = 'Y')
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





GO

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
	SELECT @v_Loan_list = loan_list + ', ' + inserted.acct_num + ' - ' + s.servicer_name
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




GO

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
GO