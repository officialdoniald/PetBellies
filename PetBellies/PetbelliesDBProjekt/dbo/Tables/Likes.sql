CREATE TABLE [dbo].[Likes] (
    [id]            INT IDENTITY (1, 1) NOT NULL,
    [petpicturesid] INT NOT NULL,
    [userid]        INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

