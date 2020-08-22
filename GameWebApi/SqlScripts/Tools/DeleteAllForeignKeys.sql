IF(EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tools].[DeleteAllForeignKeys]') and [TYPE] = N'P'))
DROP PROCEDURE [Tools].[DeleteAllForeignKeys]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Tools].[DeleteAllForeignKeys]
AS
BEGIN

	declare @sql nvarchar(max) = (
		select 
			'alter table ' + quotename(schema_name(schema_id)) + '.' +
			quotename(object_name(parent_object_id)) +
			' drop constraint '+quotename(name) + ';'
		from sys.foreign_keys
		for xml path('')
	);

exec sp_executesql @sql;

END
GO