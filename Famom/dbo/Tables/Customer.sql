CREATE TABLE [dbo].[Customer] (
    [CustomerId]        INT      IDENTITY (1, 1) NOT NULL,
    [ApplicationUserId] INT      NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [FK__Customer__Applic__17F790F9] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [CustomerUnique]
    ON [dbo].[Customer]([ApplicationUserId] ASC);

