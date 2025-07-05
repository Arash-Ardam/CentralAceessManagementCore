CREATE PROCEDURE [dbo].[spDataCenter_Get]
	@name nvarchar(50)

AS
Begin
	select Id,Name from dbo.DataCenter
	Where Name = @name
End
