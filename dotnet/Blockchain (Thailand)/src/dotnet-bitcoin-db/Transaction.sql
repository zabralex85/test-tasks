CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL, 
    [TypeId] TINYINT NOT NULL, 
    [TxId] VARBINARY(32) NOT NULL, 
	[TimeStampUtc] DATETIMEOFFSET NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TypeId]) REFERENCES [TransactionType]([Id])
)

GO

CREATE NONCLUSTERED INDEX IX_Transaction_TxId ON [Transaction]([TxId])