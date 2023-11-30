CREATE TABLE [dbo].[Chef] (
    [ChefId]            INT      IDENTITY (1, 1) NOT NULL,
    [ApplicationUserId] INT         NOT NULL,
    [ProfileImageId]    INT         NULL,
    [DisplayName]       VARCHAR(50) NULL,
    [AboutMe]           VARCHAR(1000) NULL,
    [CreateDateTime]    DATETIME      NULL,
    [CreatedBy]         VARCHAR(100)  NULL,
    [UpdateDateTime]    DATETIME      NULL,
    [UpdateBy]          VARCHAR(100)  NULL
    CONSTRAINT [PK_Chef] PRIMARY KEY CLUSTERED ([ChefId] ASC),
    CONSTRAINT [FK__ChefImage__Image] FOREIGN KEY ([ProfileImageId]) REFERENCES [dbo].[Image] ([ImageId]),
    CONSTRAINT [FK__Chef__Applicatio__1AD3FDA4] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[ApplicationUser] ([ApplicationUserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Chef_ChefID_ApplicationUserId]
    ON [dbo].[Chef]([ChefId] ASC, [ApplicationUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ChefUnique]
    ON [dbo].[Chef]([ApplicationUserId] ASC);

