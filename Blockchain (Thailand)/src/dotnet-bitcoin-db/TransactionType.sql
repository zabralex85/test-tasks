CREATE TABLE [dbo].[TransactionType]
(
	[Id] TINYINT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED ([Id])
)