CREATE TABLE [dbo].[Following] (
    [id]      INT IDENTITY (1, 1) NOT NULL,
    [userid]  INT NOT NULL,
    [fuserid] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Following_ToPet] FOREIGN KEY ([fuserid]) REFERENCES [dbo].[Pet] ([id])
);

