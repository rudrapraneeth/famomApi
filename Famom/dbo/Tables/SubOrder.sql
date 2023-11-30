CREATE TABLE [dbo].[SubOrder] (
    [SubOrderId]       INT           IDENTITY (1, 1) NOT NULL,
    [OrderId]          INT           NOT NULL,
    [PostId]           INT           NOT NULL,
    [Quantity]         INT           NOT NULL,
    [TotalPrice]       SMALLMONEY    NOT NULL,
    [TotalDiscountPrice]       SMALLMONEY    NULL,
    [StatusId]         INT           NOT NULL,
    [DeliveryTypeId]   INT           NOT NULL,
    [DeliveryDateTime] DATETIME      NOT NULL,
    [Instructions]     NVARCHAR(255) NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_SubOrder] PRIMARY KEY CLUSTERED ([SubOrderId] ASC),
    CONSTRAINT [FK__SubOrder__OrderI__40C49C62] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [FK__SubOrder__PostId__41B8C09B] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([PostId]),
    CONSTRAINT [FK__SubOrder__Status__42ACE4D4] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[OrderStatus] ([OrderStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SubOrder_StatusId]
    ON [dbo].[SubOrder]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubOrder_PostId]
    ON [dbo].[SubOrder]([PostId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubOrder_OrderId]
    ON [dbo].[SubOrder]([OrderId] ASC);

