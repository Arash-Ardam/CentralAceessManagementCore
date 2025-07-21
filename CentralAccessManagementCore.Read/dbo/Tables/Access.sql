CREATE TABLE [dbo].[Access]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Source] NVARCHAR(MAX) NOT NULL, 
    [Destination] NVARCHAR(MAX) NOT NULL, 
    [Port] INT NOT NULL, 
    [Direction] INT NOT NULL, 
    [SourceName] nvarchar(50) NOT NULL,
    [SourceAddress] nvarchar(50) NOT NULL,
    [DestinationName] nvarchar(50) NOT NULL,
    [DestinationAddress] nvarchar(50) NOT NULL,
    [SourceDCName] nvarchar(50) NOT NULL,
    [DestinationDCName] nvarchar(50) NOT NULL,
    [DataCenterId] INT NOT NULL, 
    CONSTRAINT [FK_Access_DataCenter] FOREIGN KEY ([DataCenterId]) REFERENCES [DataCenter]([Id]) on delete cascade
)
