CREATE TABLE [dbo].[Orders] (
    [OrderId]        INT        IDENTITY (1, 1) NOT NULL,
    [CustomerId]     INT        NOT NULL,
    [OrderDateTime]  DATETIME   NOT NULL,
    [TotalCost]      SMALLMONEY NOT NULL,
    [StatusId]       INT        NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_OrderId] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK__Orders__Customer__3CF40B7E] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK__Orders__StatusId__3DE82FB7] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[OrderStatus] ([OrderStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Orders_StatusId]
    ON [dbo].[Orders]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Orders_CustomerId_OrderID]
    ON [dbo].[Orders]([CustomerId] ASC, [OrderId] ASC);

