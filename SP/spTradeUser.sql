USE [QuanLyUser]
GO
/****** Object:  StoredProcedure [dbo].[spTradeUser]    Script Date: 12/07/2017 16:08:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spTradeUser] 
	 @User_Id_A integer,

	 @User_Id_B integer,

	 @Amount int
AS
BEGIN
 
-- Ghi ra số Transaction hiện thời.
-- Thực tế lúc này chưa có giao dịch nào.
PRINT '@@TranCount = ' + CAST(@@Trancount AS varchar(5));
 
PRINT 'Begin transaction';
 
-- Bắt đầu giao dịch
BEGIN TRAN;
 
 -- Bẫy lỗi.
 BEGIN TRY
   --
   -- Trừ tiền trong tài khoản người A đi 10$ (Account_ID = 1)
   UPDATE Users
   SET Amount = Amount - @Amount
   WHERE ID = @User_Id_A;
   --
   -- Ghi thông tin thời điểm giao dịch vào bảng Acc_Transaction.
   INSERT INTO TradeInfomations 
   ( UserID, AMOUNT)
     VALUES 
	 (@User_Id_A,-@Amount);

	     -- RAISERROR with severity 11-19 will cause execution to   
    -- jump to the CATCH block.  
    --RAISERROR ('Error raised in TRY block.', -- Message text.  
    --           16, -- Severity.  
    --           1 -- State.  
    --           );  

   --
   -- Cộng tiền vào tài khoản người B thêm 10$
   UPDATE Users
   SET Amount = Amount + @Amount
   WHERE ID = @User_Id_B;
   --
   -- Ghi thông tin thời điểm giao dịch vào bảng Acc_Transaction.
   INSERT INTO TradeInfomations 
   (UserID, AMOUNT)
     VALUES 
	 ( @User_Id_B, @Amount);
   -- Hoàn thành giao dịch
   IF @@Trancount > 0
     PRINT 'Commit Transaction';
 COMMIT TRAN;
 
END TRY
-- Nếu có lỗi khối Catch sẽ được chạy.
BEGIN CATCH
 PRINT 'Error: ' + ERROR_MESSAGE();
 PRINT 'Error --> Rollback Transaction';
 IF @@Trancount > 0
   ROLLBACK TRAN;
END CATCH;
 
 
 
END;