CREATE TABLE [dbo].[UsageData] (
    [UsageDataId]         INT           IDENTITY (1, 1) NOT NULL,
    [ApplicationUserId]   INT       NOT NULL,
    [LastActionDate]      DATETIME NOT  NULL,
    [ActionName]          VARCHAR (255) NOT NULL,
    [CreateDateTime]      DATETIME      NULL,
    [CreatedBy]           VARCHAR(100)  NULL,
    [UpdateDateTime]      DATETIME      NULL,
    [UpdateBy]            VARCHAR(100)  NULL
    CONSTRAINT [PK_UsageData] PRIMARY KEY CLUSTERED ([UsageDataId] ASC),
    CONSTRAINT [FK__UsageData_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserId])
);

