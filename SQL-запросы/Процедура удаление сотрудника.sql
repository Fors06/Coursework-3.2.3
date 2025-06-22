CREATE PROCEDURE dbo.DeleteEmployee
(
    @employeeId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

		-- 1. ��������� ������, ����� ��� �� ��������� �� ���������� �������
        UPDATE �����
        SET Master_id = NULL
        WHERE Master_id = @employeeId;

        -- 1. �������� User_id ����������
        DECLARE @userId INT;
        SELECT @userId = Users_id FROM ��������� WHERE Id = @employeeId;

        -- 2. ������� ����������
        DELETE FROM ��������� WHERE Id = @employeeId;

        -- 3. ������� ������������
        DELETE FROM ������������ WHERE Id = @userId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO