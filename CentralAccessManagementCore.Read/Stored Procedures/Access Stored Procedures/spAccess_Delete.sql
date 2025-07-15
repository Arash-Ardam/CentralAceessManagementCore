CREATE PROCEDURE [dbo].[spAccess_Delete]
	@source nvarchar(50),
	@destination nvarchar(50)
AS
BEGIN
	delete from dbo.Access
	where Source = @source and Destination = @destination
END