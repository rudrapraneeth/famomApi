CREATE TABLE [dbo].[AvailabilityType] (
    [AvailabilityTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]               VARCHAR(50) NULL,
    [Description]        VARCHAR(500) NULL,
    [CreateDateTime]     DATETIME      NULL,
    [CreatedBy]          VARCHAR(100)  NULL,
    [UpdateDateTime]     DATETIME      NULL,
    [UpdateBy]           VARCHAR(100)  NULL
    CONSTRAINT [PK_AvailabilityType] PRIMARY KEY CLUSTERED ([AvailabilityTypeId] ASC)
);

