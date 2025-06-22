CREATE PROCEDURE dbo.DeleteClient
(
    @clientId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. ��������� ������, ����� ��� �� ��������� �� ���������� �������
        UPDATE �����
        SET Cars_id = NULL
        WHERE Cars_id IN (
            SELECT Id FROM ���������� WHERE Client_id = @clientId
        );

        -- 2. ��������� ������, ����� ��� �� ��������� �� ���������� �������
        UPDATE �����
        SET Clients_id = NULL
        WHERE Clients_id = @clientId;

        -- 3. �������� User_id �������
        DECLARE @userId INT;
        SELECT @userId = Users_id FROM ������ WHERE Id = @clientId;

        -- 4. ������� ����������, ������������� �������
        DELETE FROM ���������� WHERE Client_id = @clientId;

        -- 5. ������� �������
        DELETE FROM ������ WHERE Id = @clientId;

        -- 6. ������� ������������
        DELETE FROM ������������ WHERE Id = @userId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO