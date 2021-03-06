USE [QuanLyUser]
GO
/****** Object:  StoredProcedure [dbo].[SearchUser]    Script Date: 12/07/2017 17:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SearchUser]
    @keyword nvarchar(max),
    @fromDate datetime = null,
	@toDate datetime = null,
	@page int,
	@pageSize int
AS
BEGIN

declare @startRow int
declare @endRow int
set @startRow=(@pageSize * @page) + 1
set @endRow=@pageSize * (@page + 1)

;WITH RS AS(
	SELECT ROW_NUMBER() OVER (ORDER BY CreatedDate DESC) AS RowNumber,
		ID,
		Name,
		Email,
		Amount,
		Phone,
		UserName,
		[Password],
		CreatedDate,
		[Status]
    FROM Users AS U
    WHERE ((@fromDate IS NULL OR CreatedDate >= @fromDate)  AND (@toDate IS NULL OR  CreatedDate <= @toDate) )

    AND ((@keyword IS NULL OR Name LIKE '%' + @keyword + '%') 
	OR @keyword IS NULL OR Email LIKE '%' + @keyword + '%' 
	OR (@keyword IS NULL OR Amount LIKE '%' + @keyword + '%')
	OR (@keyword IS NULL OR Phone LIKE '%' + @keyword + '%')
    OR (@keyword IS NULL OR ID LIKE '%' + @keyword + '%')
    OR (@keyword IS NULL OR UserName LIKE '%' + @keyword + '%'))         
)
    SELECT * FROM RS
   WHERE RowNumber BETWEEN @startRow AND @endRow

   --SELECT @totalRow = Count(*) FROM Users
END