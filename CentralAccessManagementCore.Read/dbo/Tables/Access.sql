CREATE TABLE [dbo].[Access]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Source] NVARCHAR(50) NOT NULL, 
    [Destination] NVARCHAR(50) NOT NULL, 
    [Port] INT NOT NULL, 
    [Direction] INT NOT NULL, 
    [SourceName] nvarchar(50) NOT NULL,
    [SourceAddress] nvarchar(50) NOT NULL,
    [DestinationName] nvarchar(50) NOT NULL,
    [DestinationAddress] nvarchar(50) NOT NULL,
    [DataCenterId] INT NOT NULL, 
    CONSTRAINT [FK_Access_DataCenter] FOREIGN KEY ([DataCenterId]) REFERENCES [DataCenter]([Id]) on delete cascade
)
