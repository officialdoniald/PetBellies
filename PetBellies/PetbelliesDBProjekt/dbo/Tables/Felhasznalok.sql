CREATE TABLE [dbo].[Felhasznalok] (
    [ID]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Email] NVARCHAR (50) NOT NULL,
    [Age]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

