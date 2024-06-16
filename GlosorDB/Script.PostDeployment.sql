if not exists (select 1 from dbo.Question)
begin 
	insert into dbo.Question ([Id], [Text], [Answertext], [QuestionTypeId]) 
	values 
	(NEWID(), 'Curmudgeon', 'Surpuppa', 1)
end