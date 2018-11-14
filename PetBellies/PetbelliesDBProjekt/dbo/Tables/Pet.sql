CREATE TABLE [dbo].[Pet] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [name]           NVARCHAR (50) NOT NULL,
    [age]            DATETIME           NOT NULL,
    [pettype]        NVARCHAR (50) NOT NULL,
    [haveanowner]    INT           NOT NULL,
    [profilepicture] IMAGE         NULL,
    [uploader]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

