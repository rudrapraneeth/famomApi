CREATE TABLE [dbo].[Rating] (
    [RatingId]   INT            IDENTITY (1, 1) NOT NULL,
    [SubOrderId] INT            NOT NULL,
    [Rating]     SMALLINT       NOT NULL,
    [Review]     NVARCHAR(1000) NULL,
    [ChefId]     INT            NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED ([RatingId] ASC),
    CONSTRAINT [FK__Rating__ChefId__55BFB948] FOREIGN KEY ([ChefId]) REFERENCES [dbo].[Chef] ([ChefId]),
    CONSTRAINT [FK__Rating__SubOrder__54CB950F] FOREIGN KEY ([SubOrderId]) REFERENCES [dbo].[SubOrder] ([SubOrderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Rating_SubOrderId]
    ON [dbo].[Rating]([SubOrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Rating_ChefId]
    ON [dbo].[Rating]([ChefId] ASC);

