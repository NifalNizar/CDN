CREATE SCHEMA [AUDIT];
GO

CREATE TABLE [Audit].[Users]
(
	[Id] [int] NULL,
	[Username] [nvarchar](max) NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[MobileNo] [nvarchar](max) NULL,
	[Skills] [nvarchar](max) NULL,
	[Hobby] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedBy] [int] NULL,
 )
GO

CREATE OR ALTER TRIGGER [dbo].[TriggerUsers] ON [dbo].[Users]
AFTER UPDATE
AS
BEGIN
    INSERT INTO [AUDIT].[Users]
	(
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
	)
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
    FROM deleted;
END;


