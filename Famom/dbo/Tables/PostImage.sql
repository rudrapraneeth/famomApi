CREATE TABLE [dbo].[PostImage] (
    [PostImageId]    INT      IDENTITY (1, 1) NOT NULL,
    [ImageId]        INT      NOT NULL,
    [PostId]         INT      NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_PostImage] PRIMARY KEY CLUSTERED ([PostImageId] ASC),
    CONSTRAINT [FK__PostImage__Image__24285DB4] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Image] ([ImageId]),
    CONSTRAINT [FK__PostImage__PostI__251C81ED] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([PostId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PostImage_PostId]
    ON [dbo].[PostImage]([PostId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PostImage_ImageId]
    ON [dbo].[PostImage]([ImageId] ASC);

