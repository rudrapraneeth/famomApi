CREATE TABLE [dbo].[PostSide] (
    [SideId]         INT      IDENTITY (1, 1) NOT NULL,
    [PostId]         INT      NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_PostSide] PRIMARY KEY CLUSTERED ([SideId] ASC),
    CONSTRAINT [FK__PostSide__PostId__2BFE89A6] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([PostId]),
    CONSTRAINT [FK__PostSide__SideId__2B0A656D] FOREIGN KEY ([SideId]) REFERENCES [dbo].[Side] ([SideId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PostSide_PostId]
    ON [dbo].[PostSide]([PostId] ASC);

