IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_referral]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_referral]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_referral_budget]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[hpf_referral_budget]
GO

/****** Object:  StoredProcedure [dbo].[hpf_referral]    Script Date: 02/25/2009 17:54:06 ******/
CREATE procedure [dbo].[hpf_referral]
	 @logicalDate	datetime
	,@debug			bit = 0		--Debug mode?
with execute as caller
as
/*
	This procedure is a wrapper for the relevant UDF because the RTM version of
	SQL2005 does not parse paramaters in UDFs correctly. Once this issue is fixed
	the packages can call the UDFs directly.

	exec hpf_referral '2008-03-01', 1
*/
begin
	set nocount on
	
	declare @referral_id table (REFERRAL_SEQ_ID int)
	declare @ref_result_type_hist table (REFERRAL_SEQ_ID int, REFERRAL_RESULT_DATE datetime, ACCT_PRD varchar(6))

	if @debug = 1 begin
		insert into @referral_id
		select top (1000) REFERRAL_SEQ_ID
		from REFERRAL
		where CHG_LST_DATE >= @logicalDate
		order by REFERRAL_SEQ_ID
	end else begin
		insert into @referral_id
		select REFERRAL_SEQ_ID
		from REFERRAL
		where CHG_LST_DATE >= @logicalDate
		order by REFERRAL_SEQ_ID
	end

	insert into @ref_result_type_hist
	select h2.REFERRAL_SEQ_ID, max(h1.REFERRAL_RESULT_DATE), h2.ACCT_PRD
	from REF_RESULT_TYPE_HIST h1
		inner join
		(
			select h.REFERRAL_SEQ_ID, max(h.ACCT_PRD) ACCT_PRD
			from REF_RESULT_TYPE_HIST h
				join @referral_id t on t.REFERRAL_SEQ_ID = h.REFERRAL_SEQ_ID
					and h.ACCT_PRD is not null
			group by h.REFERRAL_SEQ_ID
		) h2 on h2.REFERRAL_SEQ_ID = h1.REFERRAL_SEQ_ID and h2.ACCT_PRD = h1.ACCT_PRD
	group by h2.REFERRAL_SEQ_ID, h2.ACCT_PRD

	if @debug = 1 begin
		select top (1000)
			r.REFERRAL_SEQ_ID,
			isnull(r.PROGRAM_SEQ_ID,101) PROGRAM_SEQ_ID, 
			r.AGENCY_SEQ_ID, 
			case when ltrim(r.AGENCY_REFERRAL_ID)= '' then NULL ELSE r.AGENCY_REFERRAL_ID END AGENCY_REFERRAL_ID,
			case when ltrim(r.LAST_NAME )= '' then NULL ELSE r.LAST_NAME END LAST_NAME, 
			case when ltrim(r.FIRST_NAME)= '' then NULL ELSE r.FIRST_NAME END FIRST_NAME, 
			case when ltrim(r.MI_NAME)= '' then NULL ELSE r.MI_NAME END MI_NAME,
			case when ltrim(r.ADDR_LINE_1)= '' then NULL ELSE r.ADDR_LINE_1 END ADDR_LINE_1, 
			case when ltrim(r.CITY)= '' then NULL ELSE r.CITY END CITY, 
			case when ltrim(r.STATE)= '' then NULL ELSE r.STATE END STATE, 
			case when ltrim(r.ZIP)= '' then NULL ELSE r.ZIP END ZIP, 
			r.REFERRAL_DATE, 
			case when ltrim(r.CREATE_PROG_NAME)= '' then NULL ELSE r.CREATE_PROG_NAME END CREATE_PROG_NAME, 
			r.CREATE_DATE, 
			case when ltrim(r.CREATE_USER_ID)= '' then NULL ELSE r.CREATE_USER_ID END CREATE_USER_ID,
			case when ltrim(r.CHG_LST_PROG_NAME)= '' then NULL ELSE r.CHG_LST_PROG_NAME END CHG_LST_PROG_NAME, 
			r.CHG_LST_DATE, 
			case when ltrim(r.CHG_LST_USER_ID)= '' then NULL ELSE r.CHG_LST_USER_ID END CHG_LST_USER_ID,
			r.SUMMARY_RPT_SENT_DATE, 
			case when ltrim(r.PHN)= '' then NULL ELSE r.PHN END PHN, 
			case when ltrim(r.LOAN_DEFAULT_RSN)= '' then NULL ELSE r.LOAN_DEFAULT_RSN END LOAN_DEFAULT_RSN, 
			case when ltrim(r.ACTION_ITEMS)= '' then NULL ELSE r.ACTION_ITEMS END ACTION_ITEMS,
			case when ltrim(r.FOLLOWUP_NOTES)= '' then NULL ELSE r.FOLLOWUP_NOTES END FOLLOWUP_NOTES, 
			case when ltrim(r.SUCCESS_STORY_IND)= '' then NULL ELSE r.SUCCESS_STORY_IND END SUCCESS_STORY_IND,
			case when ltrim(r.FIRST_CONTACT_STATUS_TYPE_CODE)= '' then NULL ELSE r.FIRST_CONTACT_STATUS_TYPE_CODE END FIRST_CONTACT_STATUS_TYPE_CODE,
			case when ltrim(r.SECOND_LOAN_STATUS_TYPE_CODE)= '' then NULL ELSE r.SECOND_LOAN_STATUS_TYPE_CODE END SECOND_LOAN_STATUS_TYPE_CODE,
			case when ltrim(r.REFERRAL_SOURCE_TYPE_CODE)= '' then NULL ELSE r.REFERRAL_SOURCE_TYPE_CODE END REFERRAL_SOURCE_TYPE_CODE, 
			case when ltrim(r.CONTACTED_SERVICER_IND)= '' then NULL ELSE r.CONTACTED_SERVICER_IND END CONTACTED_SERVICER_IND, 
			case when ltrim(r.COUNSELING_DURATION_TYPE_CODE)= '' then NULL ELSE r.COUNSELING_DURATION_TYPE_CODE END COUNSELING_DURATION_TYPE_CODE,
			r.OCCUPANTS, 
			case when ltrim(r.MOTHERS_MAIDEN_NAME)= '' then NULL ELSE r.MOTHERS_MAIDEN_NAME END MOTHERS_MAIDEN_NAME, 
			case when ltrim(r.PRIVACY_CONSENT_IND)= '' then NULL ELSE r.PRIVACY_CONSENT_IND END PRIVACY_CONSENT_IND, 
			case when ltrim(r.SECONDARY_CONTACT_NUMBER)= '' then NULL ELSE r.SECONDARY_CONTACT_NUMBER END SECONDARY_CONTACT_NUMBER, 
			case when ltrim(r.EMAIL_ADDR)= '' then NULL ELSE r.EMAIL_ADDR END EMAIL_ADDR, 
			case when ltrim(r.BANKRUPTCY_IND)= '' then NULL ELSE r.BANKRUPTCY_IND END BANKRUPTCY_IND, 
			case when ltrim(r.BANKRUPTCY_ATTORNEY_NAME)= '' then NULL ELSE r.BANKRUPTCY_ATTORNEY_NAME END BANKRUPTCY_ATTORNEY_NAME, 
			case when ltrim(r.OWNER_OCCUPIED_IND)= '' then NULL ELSE r.OWNER_OCCUPIED_IND END OWNER_OCCUPIED_IND, 
			case when ltrim(r.CREDIT_SCORE)= '' then NULL ELSE r.CREDIT_SCORE END CREDIT_SCORE, 
			case when ltrim(r.PRIMARY_DFLT_RSN_TYPE_CODE)= '' then NULL ELSE r.PRIMARY_DFLT_RSN_TYPE_CODE END PRIMARY_DFLT_RSN_TYPE_CODE, 
			case when ltrim(r.SECONDARY_DFLT_RSN_TYPE_CODE)= '' then NULL ELSE r.SECONDARY_DFLT_RSN_TYPE_CODE END SECONDARY_DFLT_RSN_TYPE_CODE, 
			case when ltrim(r.HISPANIC_IND)= '' then NULL ELSE r.HISPANIC_IND END HISPANIC_IND,
			case when ltrim(r.RACE_TYPE_CODE)= '' then NULL ELSE r.RACE_TYPE_CODE END RACE_TYPE_CODE, 
			'N' as DUPE_IND, 
			r.AGE,
			case when ltrim(r.GENDER_TYPE_CODE)= '' then NULL ELSE r.GENDER_TYPE_CODE END GENDER_TYPE_CODE, 
			case when ltrim(r.HOUSEHOLD_TYPE_CODE)= '' then NULL ELSE r.HOUSEHOLD_TYPE_CODE END HOUSEHOLD_TYPE_CODE,
			r.HOUSEHOLD_INCOME_AMT, 
			case when ltrim(r.INTAKE_SCORE_TYPE_CODE)= '' then NULL ELSE r.INTAKE_SCORE_TYPE_CODE END INTAKE_SCORE_TYPE_CODE, 
			case when ltrim(r.PROPERTY_FOR_SALE_IND)= '' then NULL ELSE r.PROPERTY_FOR_SALE_IND END PROPERTY_FOR_SALE_IND,
			r.LIST_PRICE_AMT, 
			case when ltrim(r.REALTY_COMPANY_NAME)= '' then NULL ELSE r.REALTY_COMPANY_NAME END REALTY_COMPANY_NAME, 
			case when ltrim(r.FC_NOTICE_RECD_IND)= '' then NULL ELSE r.FC_NOTICE_RECD_IND END FC_NOTICE_RECD_IND, 
			case when ltrim(r.INCOME_EARNERS_TYPE_CODE)= '' then NULL ELSE r.INCOME_EARNERS_TYPE_CODE END INCOME_EARNERS_TYPE_CODE,
			case when ltrim(r.SUMMARY_SENT_OTHER_TYPE_CODE)= '' then NULL ELSE r.SUMMARY_SENT_OTHER_TYPE_CODE END SUMMARY_SENT_OTHER_TYPE_CODE, 
			r.SUMMARY_SENT_OTHER_DATE, 
			case when ltrim(r.DISCUSSED_SOLUTION_WITH_SRVCR_IND)= '' then NULL ELSE r.DISCUSSED_SOLUTION_WITH_SRVCR_IND END DISCUSSED_SOLUTION_WITH_SRVCR_IND,
			case when ltrim(r.HUD_OUTCOME_TYPE_CODE)= '' then NULL ELSE r.HUD_OUTCOME_TYPE_CODE END HUD_OUTCOME_TYPE_CODE, 
			case when ltrim(r.HUD_TERM_REASON_TYPE_CODE)= '' then NULL ELSE r.HUD_TERM_REASON_TYPE_CODE END HUD_TERM_REASON_TYPE_CODE, 
			r.HUD_TERM_DATE, 
			case when ltrim(r.WORKED_WITH_ANOTHER_AGENCY_IND)= '' then NULL ELSE r.WORKED_WITH_ANOTHER_AGENCY_IND END WORKED_WITH_ANOTHER_AGENCY_IND,
			case when ltrim(r.LOAN_ID) = '' then NULL ELSE ltrim(r.LOAN_ID) END LOAN_ID,
			case when ltrim(r.SECOND_LOAN_ID)= '' then NULL ELSE ltrim(r.SECOND_LOAN_ID) END SECOND_LOAN_ID,
			r.SERVICER_SEQ_ID,
			r.SECOND_SERVICER_SEQ_ID,
			case when ltrim(r.SECOND_MORTGAGE_TYPE_CODE)= '' then NULL ELSE r.SECOND_MORTGAGE_TYPE_CODE END SECOND_MORTGAGE_TYPE_CODE,
			case when ltrim(r.FIRST_MORTGAGE_TYPE_CODE)= '' then NULL ELSE r.FIRST_MORTGAGE_TYPE_CODE END FIRST_MORTGAGE_TYPE_CODE,
			case when ltrim(r.ARM_RESET_IND)= '' then NULL ELSE r.ARM_RESET_IND END ARM_RESET_IND,
			substring(isnull(u.agency_user_id, u.nt_userid),1,30) counselor_id_ref, 
			isnull(u.first_name, 'unknown at convert') counselor_fname, 
			isnull(u.last_name, 'unknown at convert') counselor_lname, 
			substring(isnull(u.primary_email_addr, 'unknown at convert'),1,50) counselor_email, 
			isnull(u.primary_phn, 'unknown at convert') counselor_phone,
			t.REFERRAL_RESULT_DATE,
			case when ltrim(t.ACCT_PRD)= '' then NULL ELSE t.ACCT_PRD END ACCT_PRD,
			case when ltrim(r.MTHLY_NET_INCOME_TYPE_CODE)= '' then NULL ELSE r.MTHLY_NET_INCOME_TYPE_CODE END MTHLY_NET_INCOME_TYPE_CODE,
			case ltrim(r.MTHLY_NET_INCOME_TYPE_CODE) 
				when '1' then 1000 
				when '2' then 2000 
				when '3' then 3000 
				when '4' then 4000 
				when '5' then 5000 
				when ''  then NULL
			end MTHLY_NET_INCOME,
			case when ltrim(r.MTHLY_EXPENSE_TYPE_CODE)= '' then NULL ELSE r.MTHLY_EXPENSE_TYPE_CODE END MTHLY_EXPENSE_TYPE_CODE,
			case ltrim(r.MTHLY_EXPENSE_TYPE_CODE )
				when '01' then 1375
				when '02' then 1625 
				when '03' then 1875 
				when '04' then 2125 
				when '05' then 2375 
				when '06' then 2625 
				when '07' then 2875 
				when '08' then 3125 
				when '09' then 3375 
				when '10' then 3625 
				when '11' then 3875 
				when '12' then 4125 
				when '13' then 4375 
				when '14' then 4625 
				when '15' then 4875 
				when '16' then 5125
				when ''	  then NULL
			end MTHLY_EXPENSE,
			r.PITI_AMT
		from REFERRAL r
			join CCRC_USER u on u.CCRC_USER_SEQ_ID = r.COUNSELOR_SEQ_ID
				and r.CHG_LST_DATE >= @logicalDate
			left join @ref_result_type_hist t on t.REFERRAL_SEQ_ID = r.REFERRAL_SEQ_ID
		WHERE r.AGENCY_SEQ_ID IN (2, 3, 4, 2821, 15881, 17081, 18361, 18382, 18383, 18381)
		order by REFERRAL_SEQ_ID

	end else begin
		select
			r.REFERRAL_SEQ_ID,
			isnull(r.PROGRAM_SEQ_ID,101) PROGRAM_SEQ_ID, 
			r.AGENCY_SEQ_ID, 
			case when ltrim(r.AGENCY_REFERRAL_ID)= '' then NULL ELSE r.AGENCY_REFERRAL_ID END AGENCY_REFERRAL_ID,
			case when ltrim(r.LAST_NAME )= '' then NULL ELSE r.LAST_NAME END LAST_NAME, 
			case when ltrim(r.FIRST_NAME)= '' then NULL ELSE r.FIRST_NAME END FIRST_NAME, 
			case when ltrim(r.MI_NAME)= '' then NULL ELSE r.MI_NAME END MI_NAME,
			case when ltrim(r.ADDR_LINE_1)= '' then NULL ELSE r.ADDR_LINE_1 END ADDR_LINE_1, 
			case when ltrim(r.CITY)= '' then NULL ELSE r.CITY END CITY, 
			case when ltrim(r.STATE)= '' then NULL ELSE r.STATE END STATE, 
			case when ltrim(r.ZIP)= '' then NULL ELSE r.ZIP END ZIP, 
			r.REFERRAL_DATE, 
			case when ltrim(r.CREATE_PROG_NAME)= '' then NULL ELSE r.CREATE_PROG_NAME END CREATE_PROG_NAME, 
			r.CREATE_DATE, 
			case when ltrim(r.CREATE_USER_ID)= '' then NULL ELSE r.CREATE_USER_ID END CREATE_USER_ID,
			case when ltrim(r.CHG_LST_PROG_NAME)= '' then NULL ELSE r.CHG_LST_PROG_NAME END CHG_LST_PROG_NAME, 
			r.CHG_LST_DATE, 
			case when ltrim(r.CHG_LST_USER_ID)= '' then NULL ELSE r.CHG_LST_USER_ID END CHG_LST_USER_ID,
			r.SUMMARY_RPT_SENT_DATE, 
			case when ltrim(r.PHN)= '' then NULL ELSE r.PHN END PHN, 
			case when ltrim(r.LOAN_DEFAULT_RSN)= '' then NULL ELSE r.LOAN_DEFAULT_RSN END LOAN_DEFAULT_RSN, 
			case when ltrim(r.ACTION_ITEMS)= '' then NULL ELSE r.ACTION_ITEMS END ACTION_ITEMS,
			case when ltrim(r.FOLLOWUP_NOTES)= '' then NULL ELSE r.FOLLOWUP_NOTES END FOLLOWUP_NOTES, 
			case when ltrim(r.SUCCESS_STORY_IND)= '' then NULL ELSE r.SUCCESS_STORY_IND END SUCCESS_STORY_IND,
			case when ltrim(r.FIRST_CONTACT_STATUS_TYPE_CODE)= '' then NULL ELSE r.FIRST_CONTACT_STATUS_TYPE_CODE END FIRST_CONTACT_STATUS_TYPE_CODE,
			case when ltrim(r.SECOND_LOAN_STATUS_TYPE_CODE)= '' then NULL ELSE r.SECOND_LOAN_STATUS_TYPE_CODE END SECOND_LOAN_STATUS_TYPE_CODE,
			case when ltrim(r.REFERRAL_SOURCE_TYPE_CODE)= '' then NULL ELSE r.REFERRAL_SOURCE_TYPE_CODE END REFERRAL_SOURCE_TYPE_CODE, 
			case when ltrim(r.CONTACTED_SERVICER_IND)= '' then NULL ELSE r.CONTACTED_SERVICER_IND END CONTACTED_SERVICER_IND, 
			case when ltrim(r.COUNSELING_DURATION_TYPE_CODE)= '' then NULL ELSE r.COUNSELING_DURATION_TYPE_CODE END COUNSELING_DURATION_TYPE_CODE,
			r.OCCUPANTS, 
			case when ltrim(r.MOTHERS_MAIDEN_NAME)= '' then NULL ELSE r.MOTHERS_MAIDEN_NAME END MOTHERS_MAIDEN_NAME, 
			case when ltrim(r.PRIVACY_CONSENT_IND)= '' then NULL ELSE r.PRIVACY_CONSENT_IND END PRIVACY_CONSENT_IND, 
			case when ltrim(r.SECONDARY_CONTACT_NUMBER)= '' then NULL ELSE r.SECONDARY_CONTACT_NUMBER END SECONDARY_CONTACT_NUMBER, 
			case when ltrim(r.EMAIL_ADDR)= '' then NULL ELSE r.EMAIL_ADDR END EMAIL_ADDR, 
			case when ltrim(r.BANKRUPTCY_IND)= '' then NULL ELSE r.BANKRUPTCY_IND END BANKRUPTCY_IND, 
			case when ltrim(r.BANKRUPTCY_ATTORNEY_NAME)= '' then NULL ELSE r.BANKRUPTCY_ATTORNEY_NAME END BANKRUPTCY_ATTORNEY_NAME, 
			case when ltrim(r.OWNER_OCCUPIED_IND)= '' then NULL ELSE r.OWNER_OCCUPIED_IND END OWNER_OCCUPIED_IND, 
			case when ltrim(r.CREDIT_SCORE)= '' then NULL ELSE r.CREDIT_SCORE END CREDIT_SCORE, 
			case when ltrim(r.PRIMARY_DFLT_RSN_TYPE_CODE)= '' then NULL ELSE r.PRIMARY_DFLT_RSN_TYPE_CODE END PRIMARY_DFLT_RSN_TYPE_CODE, 
			case when ltrim(r.SECONDARY_DFLT_RSN_TYPE_CODE)= '' then NULL ELSE r.SECONDARY_DFLT_RSN_TYPE_CODE END SECONDARY_DFLT_RSN_TYPE_CODE, 
			case when ltrim(r.HISPANIC_IND)= '' then NULL ELSE r.HISPANIC_IND END HISPANIC_IND,
			case when ltrim(r.RACE_TYPE_CODE)= '' then NULL ELSE r.RACE_TYPE_CODE END RACE_TYPE_CODE, 
			'N' as DUPE_IND, 
			r.AGE,
			case when ltrim(r.GENDER_TYPE_CODE)= '' then NULL ELSE r.GENDER_TYPE_CODE END GENDER_TYPE_CODE, 
			case when ltrim(r.HOUSEHOLD_TYPE_CODE)= '' then NULL ELSE r.HOUSEHOLD_TYPE_CODE END HOUSEHOLD_TYPE_CODE,
			r.HOUSEHOLD_INCOME_AMT, 
			case when ltrim(r.INTAKE_SCORE_TYPE_CODE)= '' then NULL ELSE r.INTAKE_SCORE_TYPE_CODE END INTAKE_SCORE_TYPE_CODE, 
			case when ltrim(r.PROPERTY_FOR_SALE_IND)= '' then NULL ELSE r.PROPERTY_FOR_SALE_IND END PROPERTY_FOR_SALE_IND,
			r.LIST_PRICE_AMT, 
			case when ltrim(r.REALTY_COMPANY_NAME)= '' then NULL ELSE r.REALTY_COMPANY_NAME END REALTY_COMPANY_NAME, 
			case when ltrim(r.FC_NOTICE_RECD_IND)= '' then NULL ELSE r.FC_NOTICE_RECD_IND END FC_NOTICE_RECD_IND, 
			case when ltrim(r.INCOME_EARNERS_TYPE_CODE)= '' then NULL ELSE r.INCOME_EARNERS_TYPE_CODE END INCOME_EARNERS_TYPE_CODE,
			case when ltrim(r.SUMMARY_SENT_OTHER_TYPE_CODE)= '' then NULL ELSE r.SUMMARY_SENT_OTHER_TYPE_CODE END SUMMARY_SENT_OTHER_TYPE_CODE, 
			r.SUMMARY_SENT_OTHER_DATE, 
			case when ltrim(r.DISCUSSED_SOLUTION_WITH_SRVCR_IND)= '' then NULL ELSE r.DISCUSSED_SOLUTION_WITH_SRVCR_IND END DISCUSSED_SOLUTION_WITH_SRVCR_IND,
			case when ltrim(r.HUD_OUTCOME_TYPE_CODE)= '' then NULL ELSE r.HUD_OUTCOME_TYPE_CODE END HUD_OUTCOME_TYPE_CODE, 
			case when ltrim(r.HUD_TERM_REASON_TYPE_CODE)= '' then NULL ELSE r.HUD_TERM_REASON_TYPE_CODE END HUD_TERM_REASON_TYPE_CODE, 
			r.HUD_TERM_DATE, 
			case when ltrim(r.WORKED_WITH_ANOTHER_AGENCY_IND)= '' then NULL ELSE r.WORKED_WITH_ANOTHER_AGENCY_IND END WORKED_WITH_ANOTHER_AGENCY_IND,
			case when ltrim(r.LOAN_ID) = '' then NULL ELSE ltrim(r.LOAN_ID) END LOAN_ID,
			case when ltrim(r.SECOND_LOAN_ID)= '' then NULL ELSE ltrim(r.SECOND_LOAN_ID) END SECOND_LOAN_ID,
			r.SERVICER_SEQ_ID,
			r.SECOND_SERVICER_SEQ_ID,
			case when ltrim(r.SECOND_MORTGAGE_TYPE_CODE)= '' then NULL ELSE r.SECOND_MORTGAGE_TYPE_CODE END SECOND_MORTGAGE_TYPE_CODE,
			case when ltrim(r.FIRST_MORTGAGE_TYPE_CODE)= '' then NULL ELSE r.FIRST_MORTGAGE_TYPE_CODE END FIRST_MORTGAGE_TYPE_CODE,
			case when ltrim(r.ARM_RESET_IND)= '' then NULL ELSE r.ARM_RESET_IND END ARM_RESET_IND,
			substring(isnull(u.agency_user_id, u.nt_userid),1,30) counselor_id_ref, 
			isnull(u.first_name, 'unknown at convert') counselor_fname, 
			isnull(u.last_name, 'unknown at convert') counselor_lname, 
			substring(isnull(u.primary_email_addr, 'unknown at convert'),1,50) counselor_email, 
			isnull(u.primary_phn, 'unknown at convert') counselor_phone,
			t.REFERRAL_RESULT_DATE,
			case when ltrim(t.ACCT_PRD)= '' then NULL ELSE t.ACCT_PRD END ACCT_PRD,
			case when ltrim(r.MTHLY_NET_INCOME_TYPE_CODE)= '' then NULL ELSE r.MTHLY_NET_INCOME_TYPE_CODE END MTHLY_NET_INCOME_TYPE_CODE,
			case ltrim(r.MTHLY_NET_INCOME_TYPE_CODE) 
				when '1' then 1000 
				when '2' then 2000 
				when '3' then 3000 
				when '4' then 4000 
				when '5' then 5000 
				when ''  then NULL
			end MTHLY_NET_INCOME,
			case when ltrim(r.MTHLY_EXPENSE_TYPE_CODE)= '' then NULL ELSE r.MTHLY_EXPENSE_TYPE_CODE END MTHLY_EXPENSE_TYPE_CODE,
			case ltrim(r.MTHLY_EXPENSE_TYPE_CODE )
				when '01' then 1375
				when '02' then 1625 
				when '03' then 1875 
				when '04' then 2125 
				when '05' then 2375 
				when '06' then 2625 
				when '07' then 2875 
				when '08' then 3125 
				when '09' then 3375 
				when '10' then 3625 
				when '11' then 3875 
				when '12' then 4125 
				when '13' then 4375 
				when '14' then 4625 
				when '15' then 4875 
				when '16' then 5125
				when ''	  then NULL
			end MTHLY_EXPENSE,
			r.PITI_AMT
		from REFERRAL r
			join CCRC_USER u on u.CCRC_USER_SEQ_ID = r.COUNSELOR_SEQ_ID
				and r.CHG_LST_DATE >= @logicalDate
			left join @ref_result_type_hist t on t.REFERRAL_SEQ_ID = r.REFERRAL_SEQ_ID
		WHERE r.AGENCY_SEQ_ID IN (2, 3, 4, 2821, 15881, 17081, 18361, 18382, 18383, 18381)
		order by REFERRAL_SEQ_ID
	end

	set nocount off
end --proc
GO

CREATE procedure [dbo].[hpf_referral_budget]
	 @logicalDate	datetime
	,@debug			bit = 0		--Debug mode?
with execute as caller
as
/*
	This procedure is a wrapper for the relevant UDF because the RTM version of
	SQL2005 does not parse paramaters in UDFs correctly. Once this issue is fixed
	the packages can call the UDFs directly.

	exec hpf_referral_budget '2008-03-01', 1
*/
begin
	set nocount on
	declare @referral_budget_id table(REFERRAL_SEQ_ID int)

	--get REFERRAL_SEQ_IDs which have at least one budget items change
	if @debug = 1 begin
		insert into @referral_budget_id
		select top 1000  rb.REFERRAL_SEQ_ID
		from REFERRAL_BUDGET rb, REFERRAL r
		where rb.CHG_LST_DATE >= @logicalDate
			AND rb.REFERRAL_SEQ_ID = r.REFERRAL_SEQ_ID
			AND r.AGENCY_SEQ_ID IN (2, 3, 4, 2821, 15881, 17081, 18361, 18382, 18383, 18381)			
		group by rb.REFERRAL_SEQ_ID
	end else begin
		insert into @referral_budget_id
		select rb.REFERRAL_SEQ_ID
		from REFERRAL_BUDGET rb, REFERRAL r
		where rb.CHG_LST_DATE >= @logicalDate
			AND rb.REFERRAL_SEQ_ID = r.REFERRAL_SEQ_ID
			AND r.AGENCY_SEQ_ID IN (2, 3, 4, 2821, 15881, 17081, 18361, 18382, 18383, 18381)			
		group by rb.REFERRAL_SEQ_ID
	end

	select r.REFERRAL_SEQ_ID, r.BUDGET_SUBCATEGORY_SEQ_ID, bs.BUDGET_CATEGORY_SEQ_ID
		, r.AMT
		, case when ltrim(r.BUDGET_NOTE) = '' then NULL ELSE ltrim(r.BUDGET_NOTE) END BUDGET_NOTE
		, case when ltrim(r.CREATE_PROG_NAME) = '' then NULL ELSE ltrim(r.CREATE_PROG_NAME) END CREATE_PROG_NAME
		, r.CREATE_DATE
		, case when ltrim(r.CREATE_USER_ID) = '' then NULL ELSE ltrim(r.CREATE_USER_ID) END CREATE_USER_ID
		, case when ltrim(r.CHG_LST_PROG_NAME) = '' then NULL ELSE ltrim(r.CHG_LST_PROG_NAME) END CHG_LST_PROG_NAME
		, r.CHG_LST_DATE
		, case when ltrim(r.CHG_LST_USER_ID) = '' then NULL ELSE ltrim(r.CHG_LST_USER_ID) END CHG_LST_USER_ID
	from REFERRAL_BUDGET r
		inner join @referral_budget_id i on i.REFERRAL_SEQ_ID = r.REFERRAL_SEQ_ID
		inner join BUDGET_SUBCATEGORY bs on r.BUDGET_SUBCATEGORY_SEQ_ID = bs.BUDGET_SUBCATEGORY_SEQ_ID
	order by r.REFERRAL_SEQ_ID, r.CHG_LST_DATE

	set nocount off
end --proc
GO
