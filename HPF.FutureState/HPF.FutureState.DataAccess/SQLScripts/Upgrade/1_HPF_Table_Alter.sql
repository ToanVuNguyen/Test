-- =============================================
-- Create date: 14 Jul 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 14 Jul 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

INSERT INTO ref_code_item(ref_code_set_name,code,code_desc,code_comment,sort_order,active_ind,create_dt,create_user_id,create_app_name,chg_lst_dt,chg_lst_user_id,chg_lst_app_name) VALUES('final disposition code', 'ESCALATION', 'Escalation','', 16,'Y',getdate(),'Installation','SQL Studio',getdate(),'Installation','SQL Studio');