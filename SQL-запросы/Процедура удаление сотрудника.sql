CREATE PROCEDURE dbo.DeleteEmployee
(
    @employeeId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Сначала удаляем все заказы, связанные с сотрудником
        DELETE FROM Заказ WHERE Master_id = @employeeId;

        -- 2. Получаем User_id сотрудника
        DECLARE @userId INT;
        SELECT @userId = Users_id FROM Сотрудник WHERE Id = @employeeId;

        -- 3. Удаляем сотрудника
        DELETE FROM Сотрудник WHERE Id = @employeeId;

        -- 4. Удаляем пользователя
        DELETE FROM Пользователь WHERE Id = @userId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO