CREATE PROCEDURE [dbo].[spAccess_Search]
	@sourceName nvarchar(50),
	@sourceAddress nvarchar(50),
	@destinationName nvarchar(50),
	@destinationAddress nvarchar(50),
	@sourceDCName nvarchar(50),
	@destinationDCName nvarchar(50),
	@port int,
	@direction int
AS
BEGIN
	Select Id,Source,Destination,Port,Direction 
	from dbo.Access
	where
	(
		(@sourceName = '' or SourceName = @sourceName)
		and 
		(@sourceAddress = '' or SourceAddress = @sourceAddress)
	)
	and
	(
		(@destinationName = '' or DestinationName = @destinationName)
		and 
		(@destinationAddress = '' or DestinationAddress = @destinationAddress)
	)
	and 
	(
		@sourceDCName = '' or SourceDCName = @sourceDCName
	)
	and
	(
		@destinationDCName = '' or DestinationDCName = @destinationDCName
	)
	and
	(
		@port = 0 
		or
		Port = @port
	)
	and
	(
		@direction = 0
		or
		Direction = @direction
	)
END