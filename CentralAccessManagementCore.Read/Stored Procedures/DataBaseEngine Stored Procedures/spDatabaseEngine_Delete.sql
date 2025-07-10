CREATE PROCEDURE [dbo].[spDatabaseEngine_Delete]
	@dcName nvarchar(50),
	@name nvarchar(50)
AS
BEGIN
	delete from dbo.DataBaseEngine 
	where 
	Name = @name  
	and
	DataCenterId = (select id from dbo.DataCenter where Name = @dcName)
END
