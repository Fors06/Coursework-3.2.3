CREATE PROCEDURE dbo.DeleteEmployee
(
    @employeeId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

		-- 1. Обновляем заказы, чтобы они не ссылались на удаленного клиента
        UPDATE Заказ
        SET Master_id = NULL
        WHERE Master_id = @employeeId;

        -- 1. Получаем User_id сотрудника
        DECLARE @userId INT;
        SELECT @userId = Users_id FROM Сотрудник WHERE Id = @employeeId;

        -- 2. Удаляем сотрудника
        DELETE FROM Сотрудник WHERE Id = @employeeId;

        -- 3. Удаляем пользователя
        DELETE FROM Пользователь WHERE Id = @userId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO