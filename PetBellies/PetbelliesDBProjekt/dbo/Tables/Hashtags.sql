CREATE TABLE [dbo].[Hashtags] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [petpicturesid] INT            NOT NULL,
    [hashtag]       NVARCHAR (150) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Hashtags_ToPetpictures] FOREIGN KEY ([petpicturesid]) REFERENCES [dbo].[Petpictures] ([id])
);

