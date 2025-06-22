USE [Автомастерская_2];

-- Тип пользователя
CREATE TABLE [Тип Пользователя] (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Роль VARCHAR(100) NOT NULL UNIQUE
);

-- Пользователи
CREATE TABLE Пользователь (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Имя VARCHAR(100) NOT NULL,
    Фамилия VARCHAR(50) NOT NULL,
    Отчество VARCHAR(50),
    Email VARCHAR(100),
    Пароль VARCHAR(255) NOT NULL,
    Телефон VARCHAR(20),
    User_Type_id INT NOT NULL,
    FOREIGN KEY (User_Type_id) REFERENCES [Тип Пользователя](Id)
);

-- Клиенты
CREATE TABLE Клиент (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Фамилия VARCHAR(50) NOT NULL,
    Имя VARCHAR(50) NOT NULL,
    Отчество VARCHAR(50),
    Телефон VARCHAR(20),
    Users_id INT NOT NULL,
    FOREIGN KEY (Users_id) REFERENCES Пользователь(Id) 
);

-- Автопредприятие
CREATE TABLE Автосервисная (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Название VARCHAR(100) NOT NULL,
    Адрес VARCHAR(255) NOT NULL,
    Телефон VARCHAR(20),
    Рейтинг DECIMAL(3, 1) CHECK (Рейтинг >= 0 AND Рейтинг <= 5),
    [Начало работы] TIME NOT NULL,
    [Конец работы] TIME NOT NULL
);

-- Неисправность
CREATE TABLE Неисправность (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Описание TEXT
);

-- Автомобили
CREATE TABLE Автомобиль (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Марка VARCHAR(50) NOT NULL,
    Модель VARCHAR(50) NOT NULL,
    [Год выпуска] INT NOT NULL,
    Client_id INT,
    Malfunction_id INT,
    FOREIGN KEY (Client_id) REFERENCES Клиент(Id), -- Без каскадного удаления
    FOREIGN KEY (Malfunction_id) REFERENCES Неисправность(Id)
);

-- Услуги
CREATE TABLE Услуги (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Наименование VARCHAR(100) NOT NULL,
    Описание TEXT NOT NULL,
    Стоимость DECIMAL(10, 2) NOT NULL,
    Фотография VARBINARY(MAX) NOT NULL,
    Auto_Service_id INT NOT NULL,
    FOREIGN KEY (Auto_Service_id) REFERENCES Автосервисная(Id)
);

-- Сотрудники
CREATE TABLE Сотрудник (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Фамилия VARCHAR(50) NOT NULL,
    Имя VARCHAR(50) NOT NULL,
    Отчество VARCHAR(50),
    Телефон VARCHAR(20),
    Должность VARCHAR(50) NOT NULL,
    Состояние VARCHAR(50),
    Auto_Service_id INT NOT NULL,
    Users_id INT NOT NULL,
    FOREIGN KEY (Users_id) REFERENCES Пользователь(Id),
    FOREIGN KEY (Auto_Service_id) REFERENCES Автосервисная(Id)
);

-- Статусы заказов
CREATE TABLE [Статус заказа] (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Статус VARCHAR(20) NOT NULL
);

-- Заказ
CREATE TABLE Заказ (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Дата_начала DATETIME NOT NULL,
    Дата_окончания DATETIME NOT NULL,
    Стоимость DECIMAL(10, 2) NOT NULL,
    Cars_id INT,
    Clients_id INT,
    Master_id INT,
    Auto_Service_id INT NOT NULL,
    Statuses_id INT NOT NULL,
    Services_id INT NOT NULL,
    FOREIGN KEY (Cars_id) REFERENCES Автомобиль(Id),
    FOREIGN KEY (Clients_id) REFERENCES Клиент(Id), -- Без каскадного удаления
    FOREIGN KEY (Master_id) REFERENCES Сотрудник(Id),
    FOREIGN KEY (Auto_Service_id) REFERENCES Автосервисная(Id),
    FOREIGN KEY (Statuses_id) REFERENCES [Статус заказа](Id),
    FOREIGN KEY (Services_id) REFERENCES Услуги(Id)
);