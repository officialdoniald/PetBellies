CREATE TABLE [dbo].[User] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [firstname]      NVARCHAR (50) NOT NULL,
    [lastname]       NVARCHAR (50) NOT NULL,
    [facebookid]     NVARCHAR (50) NULL,
    [profilepicture] IMAGE         NULL,
    [email]          NVARCHAR (50) NOT NULL,
    [password]       VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

