CREATE TABLE [dbo].[Purchase] (
    [purchase_id]   INT      NOT NULL,
    [product_id]    INT      NOT NULL,
    [user_id]       INT      NOT NULL,
    [purchase_date] NVARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([purchase_id] ASC),
    CONSTRAINT [FK_Purchase_ToProduct_product_id] FOREIGN KEY ([product_id]) REFERENCES [dbo].[Product] ([product_id]),
    CONSTRAINT [FK_Purchase_ToUserdbo_user_id] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Userdbo] ([user_id])
);

