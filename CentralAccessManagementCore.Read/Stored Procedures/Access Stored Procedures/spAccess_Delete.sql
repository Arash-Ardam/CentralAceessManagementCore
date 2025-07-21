CREATE PROCEDURE [dbo].[spAccess_Delete]
	@source nvarchar(MAX),
	@destination nvarchar(MAX),
	@port int
AS
BEGIN
	delete from dbo.Access
	where 
	Source = @source 
	and 
	Destination = @destination
	and 
	Port = @port
END