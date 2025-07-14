CREATE PROCEDURE [dbo].[spDataBaseEngine_Search]
	@dcName nvarchar(50),
	@name nvarchar(50),
	@address nvarchar(50)
AS
BEGIN
	SELECT Name, Address 
	FROM dbo.DataBaseEngine
	WHERE 
	DataCenterId = (SELECT Id FROM dbo.DataCenter WHERE Name = @dcName)
	AND 
	(@name = '' OR Name = @name)
	AND 
	(@address = '' OR Address = @address);
END
