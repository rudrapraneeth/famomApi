CREATE TABLE [dbo].[PushNotification] (
    [PushNotificationId] INT          IDENTITY (1, 1) NOT NULL,
    [ApplicationUserId]  INT          NOT NULL,
    [ScheduleDateTime]   DATETIME     NOT NULL,
    [NotificationTypeId] INT          NOT NULL,
    [SequenceNumber]     INT          NOT NULL,
    [ReferenceId]        INT          NOT NULL,
    [ReferenceValue]     VARCHAR(20) NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_PushNotification] PRIMARY KEY CLUSTERED ([PushNotificationId] ASC),
    CONSTRAINT [FK__PushNotif__Notif__4E1E9780] FOREIGN KEY ([NotificationTypeId]) REFERENCES [dbo].[NotificationType] ([NotificationTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PushNotification_NotificationTypeId]
    ON [dbo].[PushNotification]([NotificationTypeId] ASC);

