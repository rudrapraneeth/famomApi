CREATE TABLE [dbo].[NotificationType] (
    [NotificationTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [Name]                 VARCHAR(50)  NULL,
    [NotificationTypeCode] VARCHAR (6)   NULL,
    [UserTypeId]           INT           NULL,
    [Title]                VARCHAR (50)  NULL,
    [Body]                 VARCHAR (200) NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_NotificationType] PRIMARY KEY CLUSTERED ([NotificationTypeId] ASC)
);

