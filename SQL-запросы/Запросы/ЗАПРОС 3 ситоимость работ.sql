--������ 3: ������ �������� ��������� �����--

USE [��������������_2]
SELECT [dbo].[�����].Id, ����_���������, [dbo].[�����].��������� + [dbo].[������].��������� AS ��������_��������� 
FROM [dbo].[�����],[dbo].[������]
WHERE [dbo].[�����].Id = [dbo].[������].Id 