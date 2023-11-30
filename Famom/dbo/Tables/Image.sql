CREATE TABLE [dbo].[Image] (
    [ImageId]        INT            IDENTITY (1, 1) NOT NULL,
    [Url]            VARCHAR (500)  NOT NULL,
    [Metadata]       VARCHAR (1000) NULL,
    [FileName]       VARCHAR(255)  NULL,
    [CreateDateTime] DATETIME      NULL,
    [CreatedBy]      VARCHAR(100)  NULL,
    [UpdateDateTime] DATETIME      NULL,
    [UpdateBy]       VARCHAR(100)  NULL
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([ImageId] ASC)
);

