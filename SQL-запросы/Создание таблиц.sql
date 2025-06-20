USE [��������������_2];

--���_������������
CREATE TABLE [��� ������������] (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ���� VARCHAR(100) NOT NULL UNIQUE
);

--������������
CREATE TABLE ������������ (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ��� VARCHAR(100) NOT NULL,
    ������� VARCHAR(50) NOT NULL,
	�������� VARCHAR(50),
    Email VARCHAR(100),
    ������ VARCHAR(255) NOT NULL,
    ������� VARCHAR(20) NOT NULL,
    User_Type_id INT NOT NULL,
    FOREIGN KEY (User_Type_id) REFERENCES [��� ������������](Id)
);

-- �������
CREATE TABLE ������ (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ������� VARCHAR(50) NOT NULL,
    ��� VARCHAR(50) NOT NULL,
    �������� VARCHAR(50),
    ������� VARCHAR(20),
    Users_id INT NOT NULL,
    FOREIGN KEY (Users_id) REFERENCES ������������(Id),
);

-- ���������������
CREATE TABLE ������������� (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    �������� VARCHAR(100) NOT NULL,
    ����� VARCHAR(255) NOT NULL,
    ������� VARCHAR(20),
    ������� DECIMAL(3, 1) CHECK (������� >= 0 AND ������� <= 5),
    [������ ������] TIME NOT NULL,
	[����� ������] TIME NOT NULL
);

-- �������������
CREATE TABLE ������������� (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    �������� TEXT,
);

-- ����������
CREATE TABLE ���������� (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ����� VARCHAR(50) NOT NULL,
    ������ VARCHAR(50) NOT NULL,
    [��� �������] INT NOT NULL,
    Client_id INT NOT NULL,
	Malfunction_id int NOT NULL,
    FOREIGN KEY (Client_id) REFERENCES ������(Id),
	FOREIGN KEY (Malfunction_id) REFERENCES �������������(Id)
);

-- ������
CREATE TABLE ������ (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ������������ VARCHAR(100) NOT NULL,
    �������� TEXT NOT NULL,
    ��������� DECIMAL(10, 2) NOT NULL,
	���������� VARBINARY(MAX),
    Auto_Service_id INT NOT NULL,
    FOREIGN KEY (Auto_Service_id) REFERENCES �������������(Id)
);

-- ����������
CREATE TABLE ��������� (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ������� VARCHAR(50) NOT NULL,
    ��� VARCHAR(50) NOT NULL,
    �������� VARCHAR(50),
    ������� VARCHAR(20),
    ��������� VARCHAR(50) NOT NULL,
    ��������� VARCHAR(50),
    Auto_Service_id INT NOT NULL,
    Users_id INT NOT NULL,
    FOREIGN KEY (Users_id) REFERENCES ������������(Id),
    FOREIGN KEY (Auto_Service_id) REFERENCES �������������(Id)
);

-- ������� �������
CREATE TABLE [������ ������] (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ������ VARCHAR(20) NOT NULL
);

--�����
CREATE TABLE ����� (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Cars_id int NOT NULL,
    Clients_id INT,
    Master_id INT NOT NULL,
    Auto_Service_id INT NOT NULL,
    Statuses_id INT NOT NULL,
    Services_id INT NOT NULL,
    FOREIGN KEY (Cars_id) REFERENCES ����������(Id),
    FOREIGN KEY (Clients_id) REFERENCES ������(Id),
    FOREIGN KEY (Master_id) REFERENCES ���������(Id),
    FOREIGN KEY (Auto_Service_id) REFERENCES �������������(Id),
    FOREIGN KEY (Statuses_id) REFERENCES [������ ������](Id),
    FOREIGN KEY (Services_id) REFERENCES ������(Id)
);

