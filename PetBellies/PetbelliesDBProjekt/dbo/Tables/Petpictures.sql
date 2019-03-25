CREATE TABLE [dbo].[Petpictures] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [petid]      INT           NOT NULL,
    [pictureurl] IMAGE         NOT NULL,
    [uploaddate] NVARCHAR (50) NOT NULL,
    [reported] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Petpictures_ToPet] FOREIGN KEY ([petid]) REFERENCES [dbo].[Pet] ([id]) ON DELETE CASCADE
);

