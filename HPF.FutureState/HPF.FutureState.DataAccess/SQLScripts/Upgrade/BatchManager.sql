--CREATE Table

create table mha_escalation
(
created_dt			datetime not null,
borrower_lname		varchar(30),
borrower_fname		varchar(30),
acct_num			varchar(30),
servicer			varchar(50),
servicer_id			int,
escalation			varchar(80),
escalation_cd		varchar(15),
escalation_team_notes	varchar(3000),
agency_case_num		varchar(30),
fc_id				int,
gse_lookup			varchar(30),
counselor_name		varchar(60),
counselor_email		varchar(50),
counselor_phone		varchar(40),
escalated_to_hpf		varchar(10),
current_owner_of_issue	varchar(30),
final_resolution		varchar(80),
final_resolution_cd	varchar(15),
final_resolution_notes	varchar(3000),
resolved_by			varchar(60),
escalated_to_fannie	varchar(10),
escalated_to_freddie	varchar(10),
hpf_notes			varchar(3000),
gse_notes			varchar(3000), 
create_dt			datetime not null,
create_user_id		varchar(30) not null,
create_app_name		varchar(20) not null,
chg_lst_dt			datetime not null,
chg_lst_user_id		varchar(30) not null,
chg_lst_app_name		varchar(20) not null
)


--INSERT MHA Escalation Codes

insert into ref_code_set (ref_code_set_name, code_set_comment, agency_usage_ind)
values ('mha escalation code', 'MHA escalation code', 'Y')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'UNK', 'Unknown', '', 1, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'FEESRVCR', 'Servicer is charging up front fees', '', 2, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'MUSTBEDELINQ', 'Servicer claims borrower must miss a payment', '', 3, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'MISAPPLYMHA', 'Servicer is improperly applying MHA guidelines', '', 4, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'DENIED', 'Wrongfully denied', '', 5, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'SCAM', '3rd party scam / fraud', '', 6, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha escalation code', 'VIP', 'VIP', '', 7, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

 
---INSERT MHA Final Resolution Codes

insert into ref_code_set (ref_code_set_name, code_set_comment, agency_usage_ind)
values ('mha final resolution code', 'MHA final resolution code', 'Y')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'HANGUP', 'Hang-up', '', 1, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'REPEATCALL', 'Repeat call - Transfer', '', 2, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'REFUSEDSERVICE', 'Refused service', '', 3, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'NOINFO', 'Did not collect enough information', '', 4, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'RESOLVEDCNSLR', 'Resolved by counselor', '', 5, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'RESOLVEDSRVCR', 'Resolved with servicer', '', 6, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'RESOLVEDFRED', 'Resolved with Freddie Mac', '', 7, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('mha final resolution code', 'RESOLVEDFAN', 'Resolved with Fannie Mae', '', 8, 'Y',
       getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')


----
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hpf_mha_escalation_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[hpf_mha_escalation_insert]
			@pi_created_dt datetime
           ,@pi_borrower_lname varchar(30)
           ,@pi_borrower_fname varchar(30)
           ,@pi_acct_num varchar(30)
           ,@pi_servicer varchar(50)
         --  ,@pi_servicer_id int
           ,@pi_escalation varchar(80)
         -- ,@pi_escalation_cd varchar(15)
           ,@pi_escalation_team_notes varchar(3000)
           ,@pi_agency_case_num varchar(30)
           ,@pi_fc_id int
           ,@pi_gse_lookup varchar(30)
           ,@pi_counselor_name varchar(60)
           ,@pi_counselor_email varchar(50)
           ,@pi_counselor_phone varchar(40)
           ,@pi_escalated_to_hpf varchar(10)
           ,@pi_current_owner_of_issue varchar(30)
           ,@pi_final_resolution varchar(80)
           --,@pi_final_resolution_cd varchar(15)
           ,@pi_final_resolution_notes varchar(3000)
           ,@pi_resolved_by varchar(60)
           ,@pi_escalated_to_fannie varchar(10)
           ,@pi_escalated_to_freddie varchar(10)
           ,@pi_hpf_notes varchar(3000)
           ,@pi_gse_notes varchar(3000)
--		   ,@pi_create_dt datetime
		   ,@pi_created_user_id varchar(30)
		   ,@pi_created_app_name varchar(20)
--		   ,@pi_chg_create_dt datetime
--		   ,@pi_chg_lst_user_id varchar(30)
--		   ,pi_chg_lst_app_name varchar(20)
	
AS
BEGIN
	declare @pv_servicer_id int;
	declare @pv_escalation_cd varchar(15);
	declare @pv_final_resolution_cd varchar(15);
	
	SELECT @pv_servicer_id = servicer_id FROM servicer WHERE servicer_name = @pi_servicer
	SELECT @pv_escalation_cd = ref_code_item_id FROM ref_code_item WHERE code_desc = @pi_escalation
	SELECT @pv_final_resolution_cd = ref_code_item_id FROM ref_code_item WHERE code_desc = @pi_final_resolution	

	INSERT INTO [hpf].[dbo].[mha_escalation]
           ([created_dt]
           ,[borrower_lname]
           ,[borrower_fname]
           ,[acct_num]
           ,[servicer]
           ,[servicer_id]
           ,[escalation]
           ,[escalation_cd]
           ,[escalation_team_notes]
           ,[agency_case_num]
           ,[fc_id]
           ,[gse_lookup]
           ,[counselor_name]
           ,[counselor_email]
           ,[counselor_phone]
           ,[escalated_to_hpf]
           ,[current_owner_of_issue]
           ,[final_resolution]
           ,[final_resolution_cd]
           ,[final_resolution_notes]
           ,[resolved_by]
           ,[escalated_to_fannie]
           ,[escalated_to_freddie]
           ,[hpf_notes]
           ,[gse_notes]
           ,[create_dt]
		   ,[create_user_id]
		   ,[create_app_name]		   
		   ,[chg_lst_dt]
		   ,[chg_lst_user_id]
		   ,[chg_lst_app_name]
	)
     VALUES
           (@pi_created_dt
           ,@pi_borrower_lname
           ,@pi_borrower_fname
           ,@pi_acct_num
           ,@pi_servicer
           ,@pv_servicer_id
           ,@pi_escalation
           ,@pv_escalation_cd
           ,@pi_escalation_team_notes
           ,@pi_agency_case_num
           ,@pi_fc_id
           ,@pi_gse_lookup
           ,@pi_counselor_name
           ,@pi_counselor_email
           ,@pi_counselor_phone
           ,@pi_escalated_to_hpf
           ,@pi_current_owner_of_issue
           ,@pi_final_resolution
           ,@pv_final_resolution_cd
           ,@pi_final_resolution_notes
           ,@pi_resolved_by
           ,@pi_escalated_to_fannie
           ,@pi_escalated_to_freddie
           ,@pi_hpf_notes
           ,@pi_gse_notes
           ,getdate()
		   ,@pi_created_user_id
		   ,@pi_created_app_name
		   ,getdate()
		   ,@pi_created_user_id
		   ,@pi_created_app_name
	)	

END

' 
END
