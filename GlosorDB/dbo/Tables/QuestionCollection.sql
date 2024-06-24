CREATE TABLE [dbo].[QuestionCollection]
(
	[Id] UNIQUEIDENTIFIER CONSTRAINT [PK_QuestionCollection_Id] PRIMARY KEY default NEWID(),
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(255) NULL,
)
