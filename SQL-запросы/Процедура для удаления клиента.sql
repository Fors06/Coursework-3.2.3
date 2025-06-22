CREATE PROCEDURE dbo.DeleteClient
(
    @clientId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Обновляем заказы, чтобы они не ссылались на автомобили клиента
        UPDATE Заказ
        SET Cars_id = NULL
        WHERE Cars_id IN (
            SELECT Id FROM Автомобиль WHERE Client_id = @clientId
        );

        -- 2. Обновляем заказы, чтобы они не ссылались на удаленного клиента
        UPDATE Заказ
        SET Clients_id = NULL
        WHERE Clients_id = @clientId;

        -- 3. Получаем User_id клиента
        DECLARE @userId INT;
        SELECT @userId = Users_id FROM Клиент WHERE Id = @clientId;

        -- 4. Удаляем автомобили, принадлежащие клиенту
        DELETE FROM Автомобиль WHERE Client_id = @clientId;

        -- 5. Удаляем клиента
        DELETE FROM Клиент WHERE Id = @clientId;

        -- 6. Удаляем пользователя
        DELETE FROM Пользователь WHERE Id = @userId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO