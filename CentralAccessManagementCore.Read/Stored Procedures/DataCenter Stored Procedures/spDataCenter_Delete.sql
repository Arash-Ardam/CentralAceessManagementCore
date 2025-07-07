CREATE PROCEDURE [dbo].[spDataCenter_Delete]
	@name nvarchar(50)
AS
BEGIN
	Delete from dbo.DataCenter
	Where Name = @name
END

