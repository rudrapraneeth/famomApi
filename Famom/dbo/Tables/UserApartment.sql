CREATE TABLE [dbo].[UserApartment] (
    [UserApartmentId]   INT          IDENTITY (1, 1) NOT NULL,
    [ApartmentId]       INT          NOT NULL,
    [ApplicationUserId] INT          NOT NULL,
    [Block]             VARCHAR (50) NULL,
    [ApartmentNumber]   VARCHAR (20) NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_UserApartment] PRIMARY KEY CLUSTERED ([UserApartmentId] ASC),
    CONSTRAINT [FK__UserApart__Apart__1F63A897] FOREIGN KEY ([ApartmentId]) REFERENCES [dbo].[Apartment] ([ApartmentId]),
    CONSTRAINT [FK__UserApart__Appli__1E6F845E] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserApartment_ApplicationUserId]
    ON [dbo].[UserApartment]([ApplicationUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserApartment_ApplicationUserId_ApartmentID_UserApartmentID]
    ON [dbo].[UserApartment]([ApplicationUserId] ASC, [ApartmentId] ASC, [UserApartmentId] ASC);

