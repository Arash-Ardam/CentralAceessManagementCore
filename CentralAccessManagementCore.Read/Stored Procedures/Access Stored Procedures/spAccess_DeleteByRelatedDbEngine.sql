CREATE PROCEDURE [dbo].[spAccess_DeleteByRelatedDbEngine]
	@dcName nvarchar(50),
	@name nvarchar(50)
AS
BEGIN
	delete from dbo.Access
	where
	DataCenterId = (select Id from dbo.DataCenter where Name = @dcName)
	and
	(SourceName = @name or DestinationName = @name)
END

