CREATE OR ALTER PROCEDURE [dbo].[GetUserAuditById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[CreatedOn],
		[CreatedBy],
		[ModifiedOn],
		[ModifiedBy],
		[Status],
		[Id],
		[Username],
		[EmailAddress],
		[MobileNo],
		[Skills],
		[Hobby]
	FROM
		UsersView
	WHERE
		Id=@Id
END
