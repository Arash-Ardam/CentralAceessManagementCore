CREATE PROCEDURE [dbo].[spDataCenter_GetWithDataBaseEngines]
	@name nvarchar(50)
AS

Begin
	select DataBaseEngine.Name,Address from dbo.DataBaseEngine
	
	where DatabaseEngine.DataCenterId = (select id from dbo.DataCenter where Name = @name)
End

