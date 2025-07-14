CREATE PROCEDURE [dbo].[spAccess_Search]
	@dcName nvarchar(50),
	@sourceName nvarchar(50),
	@sourceAddress nvarchar(50),
	@destinationName nvarchar(50),
	@destinationAddress nvarchar(50),
	@port int
AS
BEGIN
	Select Source,Destination,Port,Direction 
	from dbo.Access
	where
	DataCenterId = (select Id from dbo.DataCenter where Name = @dcName)
	and
	(
		(SourceName = '' or SourceName = @sourceName)
		or 
		(SourceAddress = '' or SourceAddress = @sourceAddress)
	)
	and
	(
		(DestinationName = '' or DestinationName = @destinationName)
		or 
		(DestinationAddress = '' or DestinationAddress = @destinationAddress)
	)
	and 
	(
		Port is null 
		or
		Port = @port
	)
END