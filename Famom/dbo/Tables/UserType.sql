CREATE TABLE [dbo].[UserType] (
    [UserTypeId]     INT          IDENTITY (1, 1) NOT NULL,
    [Code]           VARCHAR (6)  NOT NULL,
    [Description]    VARCHAR (20) NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED ([UserTypeId] ASC)
);

