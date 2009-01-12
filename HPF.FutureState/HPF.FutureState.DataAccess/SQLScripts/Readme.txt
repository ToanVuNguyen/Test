Guilde to install or upgrade HPF database
A. Prequisite
	- Install IIS 
	- Install SQL server 2005 Standard, Service Pack 2

B. HPF database scripts
1 - To create a new HPF database, run the scripts as step below: 
	1_HPF_Schema_create.sql
	2_HPF_Table_create.sql
	3_HPF_SP_WS_and_Admin.sql
	4_HPF_SP_Report.sql
	5_HPF_Trigger.sql
	6_HPF_User.sql
	7_HPF_Priviledge.sql
	8_HPF_InitData.sql
2 - To upgrade a HPF database from previous version (30 Dec 2008), run the scripts as step below
	1_HPF_Table_Alter.sql
	3_HPF_SP_WS_and_Admin.sql
	4_HPF_SP_Report.sql
	5_HPF_Trigger.sql
	7_HPF_Priviledge.sql


Example: Upgrade the HPF database version 16 Dec 2008 to version 30 Dec 2008. 
You have to run the scripts HPF_DB_Script_Upgrade.sql to upgrade to version 22 Dec and then to version 30 Dec 2008.


