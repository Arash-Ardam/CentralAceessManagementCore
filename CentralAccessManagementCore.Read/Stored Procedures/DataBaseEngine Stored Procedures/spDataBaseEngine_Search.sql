CREATE PROCEDURE [dbo].[spDataBaseEngine_Search]
	@dcName nvarchar(50),
	@name nvarchar(50),
	@address nvarchar(50)
AS
BEGIN
	IF (@name = '' and @address = '')
		select DataBaseEngine.Name,Address from dbo.DataBaseEngine
		where 
		DatabaseEngine.DataCenterId = (select id from dbo.DataCenter where Name = @dcName) ;
	
	ELSE
		select DataBaseEngine.Name,Address from dbo.DataBaseEngine
		where 
		DatabaseEngine.DataCenterId = (select id from dbo.DataCenter where Name = @dcName) 
		and
		(DataBaseEngine.Name = @name or DataBaseEngine.Address = @address);
END
