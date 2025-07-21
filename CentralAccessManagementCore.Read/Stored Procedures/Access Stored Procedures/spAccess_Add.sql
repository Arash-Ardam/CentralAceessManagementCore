CREATE PROCEDURE [dbo].[spAccess_Add]
	@dcName nvarchar(50),

	@source nvarchar(MAX),
	@sourceName nvarchar(50),
	@sourceAddress nvarchar(50),

	@destination nvarchar(MAX),
	@destinationName nvarchar(50),
	@destinationAddress nvarchar(50),

	@sourceDCName nvarchar(50),
	@destinationDCName nvarchar(50),

	@port int,
	@direction int
AS

BEGIN
	insert into dbo.Access (
	DataCenterId,

	Source,
	SourceName,
	SourceAddress,

	Destination,
	DestinationName,
	DestinationAddress,

	SourceDCName,
	DestinationDCName,

	Port,
	Direction)
	values 
	(
	(select id from dbo.DataCenter where Name = @dcName),

	@source,
	@sourceName,
	@sourceAddress,

	@destination,
	@destinationName,
	@destinationAddress,

	@sourceDCName,
	@destinationDCName,

	@port,
	@direction
	)

END

