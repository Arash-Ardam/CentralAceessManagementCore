CREATE PROCEDURE [dbo].[spAccess_Add]
	@dcName nvarchar(50),

	@source nvarchar(50),
	@sourceName nvarchar(50),
	@sourceAddress nvarchar(50),

	@destination nvarchar(50),
	@destinationName nvarchar(50),
	@destinationAddress nvarchar(50),

	@port int,
	@direction int
AS

BEGIN
	insert into dbo.Access (DataCenterId,Source,SourceName,SourceAddress,Destination,DestinationName,DestinationAddress,Port,Direction)
	values 
	(
	(select id from dbo.DataCenter where Name = @dcName),

	@source,
	@sourceName,
	@sourceAddress,

	@destination,
	@destinationName,
	@destinationAddress,

	@port,
	@direction
	)

END

