Create Database AdonetDb

CREATE TABLE dbo.Categories 
(
	CategoryId int IDENTITY(1,1) NOT NULL,  
	Name varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,  
	CONSTRAINT PK_Categories PRIMARY KEY (CategoryId) 
);  



GO 
SET IDENTITY_INSERT [dbo].[Categories] ON  
GO 
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (1, N'Books') 
GO 
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (2, N'Courses') 
GO 
SET IDENTITY_INSERT [dbo].[Categories] OFF 



CREATE TABLE dbo.Products 
( 
	ProductId int IDENTITY(1,1) NOT NULL,  
	Name varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,  
	Description varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,  
	UnitPrice decimal(18,2) NOT NULL,  
	CategoryId int NOT NULL,  
	CreatedDate datetime2 DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL,  
	CONSTRAINT PK_Products PRIMARY KEY (ProductId),  
	CONSTRAINT FK_Products_Categories_CategoryId FOREIGN KEY (CategoryId) REFERENCES ADONetDb.dbo.Categories(CategoryId) ON DELETE CASCADE 
); 


SET IDENTITY_INSERT [dbo].[Products] ON  
GO 
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [CategoryId], [CreatedDate]) 
VALUES (1, N'ASP.NET Core Book', N'ASP.NET Core Book', CAST(1000.00 AS Decimal(18, 2)), 1, CAST(N'2022-10-12T23:24:36.7366667' AS DateTime2)) 
GO 
SET IDENTITY_INSERT [dbo].[Products] OFF 
GO 


-- ================================================

CREATE PROCEDURE [dbo].[AddNewProductDetails]
 (
	@Name varchar(50),
	@Description varchar(50),
	@UnitPrice decimal,
	@CategoryId int
 )
AS
BEGIN
	INSERT INTO Products(Name,Description,UnitPrice,CategoryId,CreatedDate)
	VALUES(@Name,@Description,@UnitPrice,@CategoryId,GETDATE())
END
GO


CREATE PROCEDURE [dbo].[UpdateProductDetails]
(
	@ProductId int,
	@Name varchar(50),
	@Description varchar(50),
	@UnitPrice decimal,
	@CategoryId int
)
AS
BEGIN

	Update Products SET Name=@Name,Description=@Description,UnitPrice=@UnitPrice,CategoryId=@CategoryId
	Where ProductId=@ProductId
END
GO

CREATE PROCEDURE [dbo].[DeleteProductById]
	@ProductId int
AS
BEGIN
	Delete from Products Where ProductId=@ProductId
END
GO

CREATE PROCEDURE [dbo].[usp_getproduct]
	@ProductId int
AS
BEGIN
	SELECT * From Products Where ProductId=@ProductId
END
GO