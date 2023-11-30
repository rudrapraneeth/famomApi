CREATE TABLE [dbo].[Address] (
    [AddressId]      INT           IDENTITY (1, 1) NOT NULL,
    [AddressLine1]   VARCHAR (255) NOT NULL,
    [AddressLine2]   VARCHAR (255) NULL,
    [City]           VARCHAR (100) NOT NULL,
    [PostalCode]     VARCHAR (10)  NOT NULL,
    [State]          VARCHAR (100) NOT NULL,
    [Country]        VARCHAR (100) NOT NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);

