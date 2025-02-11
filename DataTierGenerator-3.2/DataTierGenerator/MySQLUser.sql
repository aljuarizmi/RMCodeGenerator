/*USE #DatabaseName#*/

/******************************************************************************************
Create the #UserName# login.
******************************************************************************************/
/*IF NOT EXISTS(SELECT * FROM master..syslogins WHERE name = '#UserName#')
	EXEC sp_addlogin '#UserName#', '', '#DatabaseName#'
GO*/
GRANT SELECT,INSERT,UPDATE,DELETE,CREATE,DROP
        ON #DatabaseName#.*
        TO '#UserName#'@'localhost'
        IDENTIFIED BY '';

/******************************************************************************************
Grant the #UserName# login access to the #DatabaseName# database.
******************************************************************************************/
/*IF NOT EXISTS (SELECT * FROM [dbo].sysusers WHERE NAME = N'#UserName#' AND uid < 16382)
	EXEC sp_grantdbaccess N'#UserName#', N'#UserName#'
GO*/
