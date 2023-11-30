CREATE TABLE [dbo].[ApplicationUser] (
    [ApplicationUserId] INT           IDENTITY (1, 1) NOT NULL,
    [LastName]          VARCHAR (255) NULL,
    [FirstName]         VARCHAR (255) NULL,
    [UserName]          VARCHAR (50)  NOT NULL,
    [UserTypeId]        INT           NOT NULL,
    [Email]             VARCHAR (650) NULL,
    [MobileNumber]      VARCHAR (20)  NOT NULL,
    [IsActive]          BIT           NOT NULL,
    [IsVerified]        BIT           NOT NULL,
    [ExpoPushToken]     VARCHAR (255) NULL,
    [PasswordHash]      CHAR (255)    NOT NULL,
    [CreateDateTime]    DATETIME      NULL,
    [CreatedBy]         VARCHAR(100)  NULL,
    [UpdateDateTime]    DATETIME      NULL,
    [UpdateBy]          VARCHAR(100)  NULL
    CONSTRAINT [PK_ApplicationUser] PRIMARY KEY CLUSTERED ([ApplicationUserId] ASC),
    CONSTRAINT [FK__Applicati__UserT__151B244E] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserType] ([UserTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_ApplicationUserID_MobileNumber_ApplicationUserType]
    ON [dbo].[ApplicationUser]([ApplicationUserId] ASC, [MobileNumber] ASC, [UserTypeId] ASC);

