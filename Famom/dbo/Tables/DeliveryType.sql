CREATE TABLE [dbo].[DeliveryType] (
    [DeliveryTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (50) NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_DeliveryType] PRIMARY KEY CLUSTERED ([DeliveryTypeId] ASC)
);

