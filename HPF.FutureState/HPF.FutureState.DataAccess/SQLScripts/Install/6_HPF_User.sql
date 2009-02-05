-- =============================================
-- Create date: 22 Jan 2009
-- Project : HPF 
-- Build 
-- Description: Create deployed users for WS, Web App Admin, Report, DBO
-- =============================================
USE hpf
GO
IF EXISTS (SELECT name from sysusers WHERE name = N'hpf_user')
    exec sp_dropuser hpf_user;
GO
IF EXISTS (SELECT name from sysusers WHERE name = N'hpf_app_user')
    exec sp_dropuser hpf_app_user;
GO
IF EXISTS (SELECT name from sysusers WHERE name = N'hpf_report_user')
    exec sp_dropuser hpf_report_user;
GO
IF EXISTS (SELECT name from sysusers WHERE name = N'hpf_ws_user')
    exec sp_dropuser hpf_ws_user;
GO

USE [master]
GO

IF EXISTS (SELECT name from syslogins WHERE name = N'hpf_user')
    exec sp_droplogin hpf_user;
GO
IF EXISTS (SELECT name from syslogins WHERE name = N'hpf_app_user')
    exec sp_droplogin hpf_app_user;
GO
IF EXISTS (SELECT name from syslogins WHERE name = N'hpf_report_user')
    exec sp_droplogin hpf_report_user;
GO
IF EXISTS (SELECT name from syslogins WHERE name = N'hpf_ws_user')
    exec sp_droplogin hpf_ws_user;
GO

CREATE LOGIN [hpf_user] WITH PASSWORD=N'hpf_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE LOGIN [hpf_app_user] WITH PASSWORD=N'hpf_app_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE LOGIN [hpf_ws_user] WITH PASSWORD=N'hpf_ws_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE LOGIN [hpf_report_user] WITH PASSWORD=N'hpf_report_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE hpf
GO
CREATE USER [hpf_ws_user] FOR LOGIN [hpf_ws_user]
GO

CREATE USER [hpf_app_user] FOR LOGIN [hpf_app_user]
GO

CREATE USER [hpf_report_user] FOR LOGIN [hpf_report_user]
GO

CREATE USER [hpf_user] FOR LOGIN [hpf_user]
GO
EXEC sp_addrolemember N'db_owner', N'hpf_user'
GO
