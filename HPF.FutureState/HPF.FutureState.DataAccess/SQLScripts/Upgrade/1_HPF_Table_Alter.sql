-- =============================================
-- Create date: 21 Sep 2009
-- Project : HPF 
-- Build 
-- =============================================
USE HPF
GO

SET IDENTITY_INSERT dbo.program ON
GO

insert into program (program_id, program_name, start_dt, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values (2001, 'Escalation', '20090922', getdate(), 'ctomczik', 'SQL', getdate(), 'ctomczik', 'SQL')

SET IDENTITY_INSERT dbo.program OFF
GO

insert into agency_rate (program_id, agency_id, pmt_rate, eff_dt, nfmc_upcharge_pmt_rate, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values (2001, 17081, 120, '20090922', 10, getdate(), 'ctomczik', 'SQL', getdate(), 'ctomczik', 'SQL')


-- Create never bill reason codes
insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('never bill reason code', 'DUPEESCP', 'Duplicate escalation of partial', '', 9, 'Y', getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('never bill reason code', 'DUPEESCC', 'Duplicate escalation of completed', '', 10, 'Y', getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

-- Create never pay reason codes
insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('never pay reason code', 'DUPEESCP', 'Duplicate escalation of partial', '', 9, 'Y', getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')

insert into ref_code_item (ref_code_set_name, code, code_desc, code_comment, sort_order, active_ind,
       create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)
values ('never pay reason code', 'DUPEESCC', 'Duplicate escalation of completed', '', 10, 'Y', getdate(), 'ctomczik', 'SQL Studio', getdate(), 'ctomczik', 'SQL Studio')
