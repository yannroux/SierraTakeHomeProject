SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

--exec CreateOrder 1245, 1, 10

CREATE OR ALTER PROCEDURE dbo.CreateOrder 
	@customerId int,
	@productId int,
	@quantity int
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @productPrice numeric(9,2);
	select @productPrice = Price from dbo.Products p where p.Id = @productId

	insert into dbo.Orders (CustomerId,ProductId,Quantity,TotalCost) VALUES (@customerId,@productId,@quantity,@productPrice * @quantity);
	
	select * from dbo.Orders WHERE Id = @@identity
END
GO
