CREATE TABLE [dbo].[Menu] (
    [MenuId]         INT           IDENTITY (1, 1) NOT NULL,
    [Title]          NVARCHAR(100) NOT NULL,
    [Description]    NVARCHAR(500) NULL,
    [Categories]     VARCHAR(50)   NULL,
    [ChefId]         INT           NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([MenuId] ASC)
);

