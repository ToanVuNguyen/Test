-- =============================================
-- Create date: 02 Apr 2009
-- Project : HPF 
-- Build 
-- Description:	Apply database changes on: 02 Apr 2009
--		Refer to file "DB Track changes.xls"
-- =============================================
USE HPF
GO

alter table area_median_income alter column state varchar(2);
alter table area_median_income alter column county varchar(3);
alter table area_median_income alter column msa varchar(4);

update area_median_income set state = replicate('0', 2 - len(state)) + state;
update area_median_income set county = replicate('0', 3 - len(county)) + county;
update area_median_income set msa = replicate('0', 4 - len(msa)) + msa;