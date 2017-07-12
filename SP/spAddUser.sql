USE [QuanLyUser]
GO
/****** Object:  StoredProcedure [dbo].[spAddUser]    Script Date: 12/07/2017 16:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAddUser] --'','','','','123',1
    @Name nvarchar(20),
    @Email nvarchar(50),
	@UserName nvarchar(32),
	@Password nvarchar(32),
	@Phone nvarchar(50),
	@Status bit,
	@ID int output
AS
BEGIN

	insert into Users
	(Name,Email,UserName,[Password],Phone,[Status])
	values
	(@Name,@Email,@UserName,@Password,@Phone,@Status)
	
	set @ID  = SCOPE_IDENTITY()

	--declare @result bigint
	--set @result  = SCOPE_IDENTITY()
	--select @result
	
END