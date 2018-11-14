CREATE TABLE [dbo].[Donates] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [userid]     INT           NOT NULL,
    [donatedate] NVARCHAR (50) NOT NULL,
    [howmany]    INT           NOT NULL,
    [cashtype]   NVARCHAR (50) NOT NULL,
    [petid]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Donates_ToPet] FOREIGN KEY ([petid]) REFERENCES [dbo].[Pet] ([id])
);

