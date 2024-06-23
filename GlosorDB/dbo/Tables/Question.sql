﻿CREATE TABLE [dbo].[Question]
(
	[Id] UNIQUEIDENTIFIER CONSTRAINT [PK_Question_Id] PRIMARY KEY default NEWID(),
	[Text] NVARCHAR(255) NOT NULL,
	[AnswerText] NVARCHAR(255) NOT NULL,
	[QuestionTypeId] INT NOT NULL,
	[Description] NVARCHAR(255) NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[Occurrences] INT NOT NULL DEFAULT (0),
	[IncorrectAnswers] INT NOT NULL DEFAULT (0),
)

