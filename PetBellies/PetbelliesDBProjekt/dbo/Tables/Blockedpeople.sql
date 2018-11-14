CREATE TABLE [dbo].[Blockedpeople] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [UserID]        INT NOT NULL,
    [BlockedUserID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

