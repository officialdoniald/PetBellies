CREATE TABLE [dbo].[Petpictures] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [petid]      INT           NOT NULL,
    [pictureurl] IMAGE         NOT NULL,
    [uploaddate] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Petpictures_ToPet] FOREIGN KEY ([petid]) REFERENCES [dbo].[Pet] ([id])
);

