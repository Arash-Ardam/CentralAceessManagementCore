CREATE PROCEDURE [dbo].[spDataCenter_Add]
	@name nvarchar(50)
AS
BEGIN
	Insert into dbo.DataCenter (Name)
	Values (@name)
END