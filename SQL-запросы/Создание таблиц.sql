USE [Автомастерская_2];

--Тип_Пользователя
CREATE TABLE [Тип Пользователя] (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Роль VARCHAR(100) NOT NULL UNIQUE
);

--Пользователи
CREATE TABLE Пользователь (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Имя VARCHAR(100) NOT NULL,
    Фамилия VARCHAR(50) NOT NULL,
	Отчество VARCHAR(50),
    Email VARCHAR(100) NOT NULL UNIQUE,
    Пароль VARCHAR(255) NOT NULL,
    Телефон VARCHAR(20),
    User_Type_id INT NOT NULL,
    FOREIGN KEY (User_Type_id) REFERENCES [Тип Пользователя](Id)
);

-- Статусы карты
--CREATE TABLE [Статус карты] (
  --  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
   -- Status VARCHAR(20)
--);

-- Состояния карты
--CREATE TABLE [Состояние карты] (
 --   Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  --  State VARCHAR(20)
--);

-- Карта лояльности
--CREATE TABLE [Карта лояльности] (
  --  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
   -- Баллы INT NOT NULL,
    --Номер_Карты BIGINT NOT NULL,
    --Card_Status INT NOT NULL,
    --Card_State INT NOT NULL,
    --Phone VARCHAR(20) NOT NULL,
    --FOREIGN KEY (Card_Status) REFERENCES [Статус карты](Id),
    --FOREIGN KEY (Card_State) REFERENCES [Состояние карты](Id)
--);

-- Программа лояльности
--CREATE TABLE [Программа лояльности] (
  --  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
   -- Название VARCHAR(100) NOT NULL,
   -- [Условия участия] VARCHAR(100) NOT NULL,
   -- Описание TEXT NOT NULL,
   -- Скидка DECIMAL(5, 2)
--);

-- Клиенты
CREATE TABLE Клиент (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Фамилия VARCHAR(50) NOT NULL,
    Имя VARCHAR(50) NOT NULL,
    Отчество VARCHAR(50),
    Телефон VARCHAR(20),
    Users_id INT NOT NULL,
   -- Program_id INT,
  --  Card_id INT,
    FOREIGN KEY (Users_id) REFERENCES Пользователь(Id),
  --  FOREIGN KEY (Program_id) REFERENCES [Программа лояльности](Id),
  --  FOREIGN KEY (Card_id) REFERENCES [Карта лояльности](Id)
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
    Описание TEXT,
   -- Cars_id INT NOT NULL,
    --FOREIGN KEY (Cars_id) REFERENCES Автомобиль(Id), --ON DELETE CASCADE

);

-- Автомобили
CREATE TABLE Автомобиль (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Марка VARCHAR(50) NOT NULL,
    Модель VARCHAR(50) NOT NULL,
    [Год выпуска] INT NOT NULL,
    Client_id INT NOT NULL,
	Malfunction_id int NOT NULL,
    FOREIGN KEY (Client_id) REFERENCES Клиент(Id),
	FOREIGN KEY (Malfunction_id) REFERENCES Автосервисная(Id)
);

-- Услуги
CREATE TABLE Услуги (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Наименование VARCHAR(100) NOT NULL,
    Описание TEXT NOT NULL,
    Стоимость DECIMAL(10, 2) NOT NULL,
	Фотография VARBINARY(MAX),
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

CREATE TABLE Заказ (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Дата_начала DATETime NOT NULL,
	Дата_окончания DateTime NOT NULL,
    Стоимость DECIMAL(10, 2) NOT NULL,
    Cars_id int NOT NULL,
    Clients_id INT,
    Master_id INT NOT NULL,
    Auto_Service_id INT NOT NULL,
    Statuses_id INT NOT NULL,
    Services_id INT NOT NULL,
    FOREIGN KEY (Cars_id) REFERENCES Автомобиль(Id), --ON DELETE CASCADE,
    FOREIGN KEY (Clients_id) REFERENCES Клиент(Id),
    FOREIGN KEY (Master_id) REFERENCES Сотрудник(Id),
    FOREIGN KEY (Auto_Service_id) REFERENCES Автосервисная(Id),
    FOREIGN KEY (Statuses_id) REFERENCES [Статус заказа](Id),
    FOREIGN KEY (Services_id) REFERENCES Услуги(Id)
);





-- Удаляю прежнюю связь между заказом и услугами
--ALTER TABLE Заказ DROP COLUMN Services_id;

--CREATE TABLE Заказ_услуга (
  --  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    --Order_id INT NOT NULL,
   -- Services_id INT NOT NULL,
    --FOREIGN KEY (Order_id) REFERENCES Заказ(Id),
   -- FOREIGN KEY (Services_id) REFERENCES Услуги(Id)
--);