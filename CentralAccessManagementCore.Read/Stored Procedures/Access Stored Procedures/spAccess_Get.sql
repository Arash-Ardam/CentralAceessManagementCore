CREATE PROCEDURE [dbo].[spAccess_Get]
	@id int
AS
BEGIN 
	select Id,Source,Destination,Port,Direction from dbo.Access
	where Id = @id
END