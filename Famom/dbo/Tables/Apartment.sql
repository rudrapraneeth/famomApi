CREATE TABLE [dbo].[Apartment] (
    [ApartmentId]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [AddressId]      INT           NOT NULL,
    [IsActive]       BIT           NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Apartment] PRIMARY KEY CLUSTERED ([ApartmentId] ASC),
    CONSTRAINT [FK__Apartment__Addre__17C286CF] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Apartment_AddressId]
    ON [dbo].[Apartment]([AddressId] ASC);

