CREATE TABLE [dbo].[Side] (
    [SideId]         INT          IDENTITY (1, 1) NOT NULL,
    [Title]          NVARCHAR(50) NOT NULL,
    [Price]          SMALLMONEY   NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Side] PRIMARY KEY CLUSTERED ([SideId] ASC)
);

