CREATE TABLE [dbo].[Wallet]
(
    [Address] VARCHAR(35),
	[AmountBalance] DECIMAL(18,8) NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED ([Address])
)
