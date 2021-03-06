USE [QuanLyUser]
GO
/****** Object:  StoredProcedure [dbo].[SearchUserCount]    Script Date: 12/07/2017 17:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SearchUserCount]
    @keyword nvarchar(max),
    @fromDate datetime = null,
	@toDate datetime = null
AS
BEGIN

SELECT COUNT(*)
    FROM Users AS U
    WHERE ((@fromDate IS NULL OR CreatedDate >= @fromDate)  AND (@toDate IS NULL OR  CreatedDate <= @toDate) )

    AND ((@keyword IS NULL OR Name LIKE '%' + @keyword + '%') 
	OR @keyword IS NULL OR Email LIKE '%' + @keyword + '%' 
	OR (@keyword IS NULL OR Phone LIKE '%' + @keyword + '%')
    OR (@keyword IS NULL OR ID LIKE '%' + @keyword + '%')
    OR (@keyword IS NULL OR UserName LIKE '%' + @keyword + '%'))

   --SELECT @totalRow = Count(*) FROM Users
END