CREATE TABLE [dbo].[Post] (
    [PostId]           INT        IDENTITY (1, 1) NOT NULL,
    [ChefId]           INT        NOT NULL,
    [MenuId]           INT        NOT NULL,
    [Quantity]         INT        NULL,
    [QuantityTypeId]   INT        NOT NULL,
    [DeliveryTypeId]   INT        NOT NULL,
    [AvailabilityTypeId]      INT        NOT NULL default(2),
    [Price]            SMALLMONEY NOT NULL,
    [IsActive]         BIT        NOT NULL,
    [AvailableFrom]    DATETIME   NULL,
    [AvailableTo]      DATETIME   NULL,
    [NoticeHours]      INT        NULL,
    [MinimumOrder]     INT        NULL,
    [InactiveDateTime] DATETIME   NULL,
    [DeliveryCharge]   SMALLMONEY NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([PostId] ASC),
    CONSTRAINT [FK__Post__ChefId__236943A5] FOREIGN KEY ([ChefId]) REFERENCES [dbo].[Chef] ([ChefId]),
    CONSTRAINT [FK__Post__DeliveryTy__2645B050] FOREIGN KEY ([DeliveryTypeId]) REFERENCES [dbo].[DeliveryType] ([DeliveryTypeId]),
    CONSTRAINT [FK__Post__MenuId__245D67DE] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([MenuId]),
    CONSTRAINT [FK__Post__QuantityTy__25518C17] FOREIGN KEY ([QuantityTypeId]) REFERENCES [dbo].[QuantityType] ([QuantityTypeId]),
    CONSTRAINT [FK__Post__AvailabilityTypeId] FOREIGN KEY ([AvailabilityTypeId]) REFERENCES [dbo].[AvailabilityType] ([AvailabilityTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Post_PostID_IsActive_AvailableFrom_AvailableTo]
    ON [dbo].[Post]([PostId] ASC)
    INCLUDE([IsActive], [Quantity], [AvailableTo]);


GO
CREATE NONCLUSTERED INDEX [IX_Post_MenuId]
    ON [dbo].[Post]([MenuId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Post_DeliveryTypeId]
    ON [dbo].[Post]([DeliveryTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Post_ChefId]
    ON [dbo].[Post]([ChefId] ASC);

