DECLARE @SwedishQuestionCollectionId AS UNIQUEIDENTIFIER
DECLARE @EnglishQuestionCollectionId AS UNIQUEIDENTIFIER

SET @SwedishQuestionCollectionId = 'f898db42-071a-4e5c-93ad-3e10f054f9a3'
SET @EnglishQuestionCollectionId = '73db4a81-43d7-4d8f-a43f-92504e4559d5'

if not exists (select 1 from dbo.QuestionCollection)
begin 
	insert into dbo.QuestionCollection ([Id], [Name], [Description]) 
	values 
	(@SwedishQuestionCollectionId, 'Svenska', 'Svenska Ord'),
	(@EnglishQuestionCollectionId, 'Engelska', 'Engelska Ord')
end

if not exists (select 1 from dbo.Question)
begin 
	insert into dbo.Question ([Id], [Text], [Answertext], [QuestionTypeId], [QuestionCollectionId]) 
	values 
	(NEWID(), 'Pogrom', 'Våldsam och blodig förföljelse av en folkgrupp', 1, @SwedishQuestionCollectionId),
	(NEWID(), 'Curmudgeon', 'Surpuppa', 1, @EnglishQuestionCollectionId)
end