CREATE PROCEDURE [dbo].[spDataBaseEngine_Add]
	@dcName nvarchar(50),
	@name nvarchar(50),
	@address nvarchar(50)
AS
BEGIN
	insert into dbo.DataBaseEngine (Name,Address,DataCenterId)
	VALUES (
	@name,
	@address,
	(select id from dbo.DataCenter where Name = @dcName)
	);
END
