CREATE TABLE [dbo].[Promotion] (
    [PromotionId]      INT           IDENTITY (1, 1) NOT NULL,
    [StartDate]        DATETIME      NOT NULL,
    [EndDate]          DATETIME      NOT NULL,
    [PromoCode]        VARCHAR (10)  NULL,
    [DiscountAmount]   SMALLMONEY    NULL,
    [DiscountPercent]  INT           NOT NULL,
    [PromoType]        VARCHAR (20)  NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED ([PromotionId] ASC)
);

