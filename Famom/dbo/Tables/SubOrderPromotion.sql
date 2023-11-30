CREATE TABLE [dbo].[SubOrderPromotion] (
    [SubOrderPromotionId]       INT           IDENTITY (1, 1) NOT NULL,
    [SubOrderId]                INT           NOT NULL,
    [PromotionId]               INT           NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_SubOrderPromotion] PRIMARY KEY CLUSTERED ([SubOrderPromotionId] ASC),
    CONSTRAINT [FK__SubOrderPromotion__SubOrderId] FOREIGN KEY ([SubOrderId]) REFERENCES [dbo].[SubOrder] ([SubOrderId]),
    CONSTRAINT [FK__SubOrderPromotion__PromotionId] FOREIGN KEY ([PromotionId]) REFERENCES [dbo].[Promotion] ([PromotionId]),
);


GO
CREATE NONCLUSTERED INDEX [IX_SubOrderPromotion_SubOrderId]
    ON [dbo].[SubOrderPromotion]([SubOrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubOrderPromotion_PromotionId]
    ON [dbo].[SubOrderPromotion]([PromotionId] ASC);