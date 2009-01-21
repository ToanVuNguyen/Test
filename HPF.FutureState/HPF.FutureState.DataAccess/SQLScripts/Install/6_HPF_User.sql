-- =============================================
-- Create date: 21 Jan 2009
-- Project : HPF 
-- Build 
-- Description: Create deployed users for WS, Web App Admin, Report, DBO
-- =============================================
USE [master]
GO
CREATE LOGIN [hpf] WITH PASSWORD=N'hpf', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[British], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE LOGIN [hpf_app_user] WITH PASSWORD=N'hpf_app_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[British], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE LOGIN [hpf_ws_user] WITH PASSWORD=N'hpf_ws_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE LOGIN [hpf_report_user] WITH PASSWORD=N'hpf_report_user', DEFAULT_DATABASE=[hpf], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE HPF
GO
CREATE USER [hpf_ws_user] FOR LOGIN [hpf_ws_user]
GO
EXEC sp_addrolemember N'db_datareader', N'hpf_ws_user'
GO
EXEC sp_addrolemember N'db_datawriter', N'hpf_ws_user'
GO

CREATE USER [hpf_app_user] FOR LOGIN [hpf_app_user]
GO
EXEC sp_addrolemember N'db_datareader', N'hpf_app_user'
GO
EXEC sp_addrolemember N'db_datawriter', N'hpf_app_user'
GO

CREATE USER [hpf_report_user] FOR LOGIN [hpf_report_user]
GO
EXEC sp_addrolemember N'db_datareader', N'hpf_report_user'
GO
EXEC sp_addrolemember N'db_datawriter', N'hpf_report_user'
GO

CREATE USER [hpf] FOR LOGIN [hpf]
GO
EXEC sp_addrolemember N'db_owner', N'hpf'
GO
