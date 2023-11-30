CREATE TABLE [dbo].[ApartmentPromotion] (
    [ApartmentPromotionId]      INT      IDENTITY (1, 1) NOT NULL,
    [PromotionId]               INT      NOT NULL,
    [ApartmentId]               INT      NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_ApartmentPromotion] PRIMARY KEY CLUSTERED ([ApartmentPromotionId] ASC),
    CONSTRAINT [FK__ApartmentPromotion__Promotion] FOREIGN KEY ([PromotionId]) REFERENCES [dbo].[Promotion] ([PromotionId]),
    CONSTRAINT [FK__ApartmentPromotion__Apartment] FOREIGN KEY ([ApartmentId]) REFERENCES [dbo].[Apartment] ([ApartmentId])
);

GO
CREATE NONCLUSTERED INDEX [IX_ApartmentPromotion_PromotionId]
    ON [dbo].[ApartmentPromotion]([PromotionId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_ApartmentPromotion_ApartmentId]
    ON [dbo].[ApartmentPromotion]([ApartmentId] ASC);
