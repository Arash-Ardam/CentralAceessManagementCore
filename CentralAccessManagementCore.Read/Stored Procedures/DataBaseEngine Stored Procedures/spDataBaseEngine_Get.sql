CREATE PROCEDURE [dbo].[spDataBaseEngine_Get]
	@dcName nvarchar(50),
	@name nvarchar(50)
AS
BEGIN
	select Name,Address from dbo.DataBaseEngine
	where
	DataCenterId = (select id from dbo.DataCenter where Name = @dcName)
	and 
	Name = @name
END