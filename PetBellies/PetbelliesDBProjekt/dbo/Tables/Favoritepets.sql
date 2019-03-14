CREATE TABLE [dbo].[Favoritepets] (
    [id]     INT IDENTITY (1, 1) NOT NULL,
    [userid] INT NOT NULL,
    [petid]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Favoritepets_ToPet] FOREIGN KEY ([petid]) REFERENCES [dbo].[Pet] ([id]) ON DELETE CASCADE
);

