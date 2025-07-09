CREATE TABLE [dbo].[DataBaseEngine]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [DataCenterId] INT NOT NULL, 
    CONSTRAINT [FK_DataBaseEngine_DataCenter_DataCenterName] FOREIGN KEY ([DataCenterId]) REFERENCES [DataCenter]([Id]) 
    on delete cascade
)
