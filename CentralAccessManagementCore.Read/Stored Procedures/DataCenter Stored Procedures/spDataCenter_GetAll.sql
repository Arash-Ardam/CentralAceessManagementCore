CREATE PROCEDURE [dbo].[spDataCenter_GetAll]

AS
Begin
	select Id,Name from dbo.DataCenter
End
