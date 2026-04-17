-- ========================================
-- CREATE DATABASE
-- ========================================
USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GymManagement')
    DROP DATABASE GymManagement;
GO
CREATE DATABASE GymManagement;
GO
USE GymManagement;
GO

-- ========================================
-- SYSTEM CONFIG (BASE LƯƠNG)
-- ========================================
CREATE TABLE SystemConfig (
    Id INT PRIMARY KEY,
    BaseSalaryPerShift DECIMAL(10,2)
);

INSERT INTO SystemConfig VALUES (1,100000);

-- ========================================
-- STAFF LEVEL (LƯƠNG THEO KINH NGHIỆM)
-- ========================================
CREATE TABLE StaffLevels (
    LevelId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50),
    MinExperience INT,
    MaxExperience INT,
    SalaryPercentIncrease INT
);

-- ========================================
-- USERS
-- ========================================
CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,

    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) UNIQUE,
    Email NVARCHAR(100),
    Avatar NVARCHAR(255),

    Role NVARCHAR(20) CHECK (Role IN ('Admin','Staff')),
    Status NVARCHAR(20) DEFAULT 'Active',

    Experience INT DEFAULT 0,
    LevelId INT,

    CreatedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT chk_phone CHECK (Phone IS NULL OR LEN(Phone) >= 9),
    CONSTRAINT chk_email CHECK (Email IS NULL OR Email LIKE '%@%'),

    FOREIGN KEY (LevelId) REFERENCES StaffLevels(LevelId)
);

CREATE TABLE PendingUsers (
    Id INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50),
    Password NVARCHAR(255),
    FullName NVARCHAR(100),
    Status NVARCHAR(20) DEFAULT 'Pending'
);

-- ========================================
-- WORK SHIFT + ATTENDANCE
-- ========================================
CREATE TABLE WorkShifts (
    ShiftId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50),
    StartTime TIME,
    EndTime TIME
);

CREATE TABLE UserShifts (
    Id INT IDENTITY PRIMARY KEY,
    UserId INT,
    ShiftId INT,
    WorkDate DATE,

    CONSTRAINT uq_user_shift UNIQUE (UserId, ShiftId, WorkDate),

    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (ShiftId) REFERENCES WorkShifts(ShiftId)
);

CREATE TABLE Attendances (
    AttendanceId INT IDENTITY PRIMARY KEY,
    UserId INT,
    WorkDate DATE,
    CheckIn DATETIME,
    CheckOut DATETIME,

    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- ========================================
-- MEMBERS
-- ========================================
CREATE TABLE Members (
    MemberId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    Phone NVARCHAR(20) UNIQUE,
    JoinDate DATE DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Active',

    Avatar NVARCHAR(255),
    IdentityNumber NVARCHAR(50),
    CardCode NVARCHAR(50) UNIQUE
);

CREATE TABLE Checkins (
    CheckinId INT IDENTITY PRIMARY KEY,
    MemberId INT,
    CheckinTime DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);

-- ========================================
-- TRAINER LEVEL
-- ========================================
CREATE TABLE TrainerLevels (
    LevelId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50),
    MinExperience INT,
    MaxExperience INT,
    PricePercentIncrease INT CHECK (PricePercentIncrease >= 0)
);

-- ========================================
-- TRAINERS
-- ========================================
CREATE TABLE Trainers (
    TrainerId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),

    Experience INT,
    SalaryPercent INT CHECK (SalaryPercent BETWEEN 0 AND 100),

    Status NVARCHAR(20) DEFAULT 'Active',
    Avatar NVARCHAR(255),
    Specialty NVARCHAR(100),

    LevelId INT,
    FOREIGN KEY (LevelId) REFERENCES TrainerLevels(LevelId)
);

-- ========================================
-- PACKAGES
-- ========================================
CREATE TABLE Packages (
    PackageId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    Price DECIMAL(10,2),

    DurationValue INT CHECK (DurationValue > 0),
    DurationType NVARCHAR(20) CHECK (DurationType IN ('Day','Month','Year')),

    Description NVARCHAR(255)
);

-- ========================================
-- MEMBERSHIPS
-- ========================================
CREATE TABLE Memberships (
    MembershipId INT IDENTITY PRIMARY KEY,
    MemberId INT,
    PackageId INT,
    TrainerId INT NULL,

    StartDate DATE,
    EndDate DATE,

    Status NVARCHAR(20) DEFAULT 'Active',
    Price DECIMAL(10,2),

    CONSTRAINT chk_date CHECK (EndDate > StartDate),

    FOREIGN KEY (MemberId) REFERENCES Members(MemberId),
    FOREIGN KEY (PackageId) REFERENCES Packages(PackageId),
    FOREIGN KEY (TrainerId) REFERENCES Trainers(TrainerId)
);

-- ========================================
-- PRODUCTS
-- ========================================
CREATE TABLE Brands (
    BrandId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100)
);

CREATE TABLE Units (
    UnitId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50)
);

CREATE TABLE Products (
    ProductId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),

    BrandId INT,
    UnitId INT,

    Price DECIMAL(10,2),
    Quantity INT DEFAULT 0 CHECK (Quantity >= 0),

    Barcode NVARCHAR(50) UNIQUE,
    Image NVARCHAR(255),
    Description NVARCHAR(255),

    FOREIGN KEY (BrandId) REFERENCES Brands(BrandId),
    FOREIGN KEY (UnitId) REFERENCES Units(UnitId)
);

-- ========================================
-- IMPORT
-- ========================================
CREATE TABLE ImportReceipts (
    ImportId INT IDENTITY PRIMARY KEY,
    Date DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2)
);

CREATE TABLE ImportDetails (
    Id INT IDENTITY PRIMARY KEY,
    ImportId INT,
    ProductId INT,
    Quantity INT,
    Price DECIMAL(10,2),

    FOREIGN KEY (ImportId) REFERENCES ImportReceipts(ImportId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- ========================================
-- SALES
-- ========================================
CREATE TABLE Invoices (
    InvoiceId INT IDENTITY PRIMARY KEY,
    MemberId INT NULL,
    Date DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2),

    FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);

CREATE TABLE InvoiceDetails (
    Id INT IDENTITY PRIMARY KEY,
    InvoiceId INT,
    ProductId INT,
    Quantity INT,
    Price DECIMAL(10,2),

    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- ========================================
-- PAYMENTS
-- ========================================
CREATE TABLE Payments (
    PaymentId INT IDENTITY PRIMARY KEY,
    Amount DECIMAL(10,2),
    PaymentDate DATETIME DEFAULT GETDATE(),

    Method NVARCHAR(20) CHECK (Method IN ('Cash','Online')),

    CashReceived DECIMAL(10,2) NULL,
	ChangeAmount DECIMAL(10,2) NULL,

    MembershipId INT NULL,
    InvoiceId INT NULL,

    CONSTRAINT chk_payment_target CHECK (
        (MembershipId IS NOT NULL AND InvoiceId IS NULL)
        OR
        (MembershipId IS NULL AND InvoiceId IS NOT NULL)
    ),

    FOREIGN KEY (MembershipId) REFERENCES Memberships(MembershipId),
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId)
);

-- ========================================
-- INDEX (TỐI ƯU)
-- ========================================
CREATE INDEX idx_member_phone ON Members(Phone);
CREATE INDEX idx_product_name ON Products(Name);
CREATE INDEX idx_invoice_date ON Invoices(Date);
-- ========================================
-- TRIGGERS
-- ========================================
go
CREATE TRIGGER trg_Import_AddStock
ON ImportDetails
AFTER INSERT
AS
BEGIN
    UPDATE p
    SET p.Quantity = p.Quantity + i.Quantity
    FROM Products p
    JOIN inserted i ON p.ProductId = i.ProductId
END;

go
CREATE TRIGGER trg_Sell_ReduceStock
ON InvoiceDetails
AFTER INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM Products p
        JOIN inserted i ON p.ProductId = i.ProductId
        WHERE p.Quantity < i.Quantity
    )
    BEGIN
        RAISERROR(N'Không đủ hàng',16,1)
        ROLLBACK
        RETURN
    END

    UPDATE p
    SET p.Quantity = p.Quantity - i.Quantity
    FROM Products p
    JOIN inserted i ON p.ProductId = i.ProductId
END;

-- ========================================
-- STORED PROCEDURES
-- ========================================

go
-- Đăng ký gói
CREATE OR ALTER PROCEDURE sp_RegisterMembership
    @MemberId INT,
    @PackageId INT,
    @TrainerId INT = NULL,
    @StartDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Kiểm tra chồng chéo gói tập
        IF EXISTS (SELECT 1 FROM Memberships WHERE MemberId = @MemberId AND EndDate > @StartDate AND Status = 'Active')
        BEGIN
            RAISERROR(N'Hội viên này vẫn còn gói tập đang có hiệu lực. Không thể đăng ký mới.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 2. Lấy thông tin giá và thời hạn
        DECLARE @DurationValue INT, @DurationType NVARCHAR(20), @BasePrice DECIMAL(10,2), @FinalPrice DECIMAL(10,2), @EndDate DATE;
        SELECT @DurationValue = DurationValue, @DurationType = DurationType, @BasePrice = Price 
        FROM Packages WHERE PackageId = @PackageId;

        -- 3. Tính ngày kết thúc
        SET @EndDate = CASE @DurationType 
            WHEN 'Day' THEN DATEADD(DAY, @DurationValue, @StartDate)
            WHEN 'Month' THEN DATEADD(MONTH, @DurationValue, @StartDate)
            WHEN 'Year' THEN DATEADD(YEAR, @DurationValue, @StartDate)
        END;

        -- 4. Tính giá theo Level của PT
        DECLARE @Increase INT = 0;
        IF @TrainerId IS NOT NULL
            SELECT @Increase = ISNULL(tl.PricePercentIncrease, 0)
            FROM Trainers t JOIN TrainerLevels tl ON t.LevelId = tl.LevelId
            WHERE t.TrainerId = @TrainerId;

        SET @FinalPrice = @BasePrice * (1 + @Increase / 100.0);

		-- Thêm vào trước bước Insert trong sp_RegisterMembership
		IF @TrainerId IS NOT NULL
		BEGIN
			DECLARE @ActiveClients INT;
			SELECT @ActiveClients = COUNT(*) FROM Memberships 
			WHERE TrainerId = @TrainerId AND Status = 'Active' AND EndDate > GETDATE();

			IF @ActiveClients >= 10 -- Giả sử giới hạn là 10
			BEGIN
				RAISERROR(N'Huấn luyện viên này đã đạt giới hạn số lượng học viên.', 16, 1);
				ROLLBACK TRANSACTION;
				RETURN;
			END
		END

        -- 5. Insert
        INSERT INTO Memberships (MemberId, PackageId, TrainerId, StartDate, EndDate, Price, Status)
        VALUES (@MemberId, @PackageId, @TrainerId, @StartDate, @EndDate, @FinalPrice, 'Active');

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO

go
CREATE TRIGGER trg_Membership_CreateInvoice
ON Memberships
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tự động chèn một hóa đơn mới dựa trên thông tin Membership vừa tạo
    INSERT INTO Invoices (MemberId, Date, Total)
    SELECT i.MemberId, GETDATE(), i.Price
    FROM inserted i;
    
    -- Lưu ý: Bạn có thể mở rộng để chèn vào InvoiceDetails nếu muốn chi tiết hơn
END;
GO

go
-- Trigger khi xóa chi tiết hóa đơn (Hoàn kho)
CREATE TRIGGER trg_InvoiceDetails_Delete
ON InvoiceDetails
AFTER DELETE
AS
BEGIN
    UPDATE p
    SET p.Quantity = p.Quantity + d.Quantity
    FROM Products p
    JOIN deleted d ON p.ProductId = d.ProductId;
END;
GO

-- Trigger khi cập nhật số lượng trong hóa đơn
CREATE TRIGGER trg_InvoiceDetails_Update
ON InvoiceDetails
AFTER UPDATE
AS
BEGIN
    UPDATE p
    SET p.Quantity = p.Quantity + (d.Quantity - i.Quantity)
    FROM Products p
    JOIN deleted d ON p.ProductId = d.ProductId
    JOIN inserted i ON p.ProductId = i.ProductId;
END;
GO

go
-- Thanh toán
CREATE PROCEDURE sp_Payment
    @Amount DECIMAL(10,2),
    @Method NVARCHAR(20),
    @CashReceived DECIMAL(10,2) = NULL,
    @InvoiceId INT = NULL,
    @MembershipId INT = NULL
AS
BEGIN
    DECLARE @Change DECIMAL(10,2)

    IF @Method = 'Cash'
        SET @Change = @CashReceived - @Amount

    INSERT INTO Payments(Amount, Method, CashReceived, ChangeAmount, InvoiceId, MembershipId)
    VALUES(@Amount, @Method, @CashReceived, @Change, @InvoiceId, @MembershipId)
END;
GO
INSERT INTO StaffLevels (Name, MinExperience, MaxExperience, SalaryPercentIncrease)
VALUES
(N'Junior',0,1,10),
(N'Mid',2,4,20),
(N'Senior',5,10,30);
SELECT 
    u.FullName,
    COUNT(a.AttendanceId) AS TotalShift,
    sc.BaseSalaryPerShift,
    sl.SalaryPercentIncrease,

    COUNT(a.AttendanceId) * 
    sc.BaseSalaryPerShift * 
    (1 + sl.SalaryPercentIncrease / 100.0) AS Salary

FROM Users u
JOIN StaffLevels sl ON u.LevelId = sl.LevelId
CROSS JOIN SystemConfig sc
LEFT JOIN Attendances a ON u.UserId = a.UserId

GROUP BY u.FullName, sc.BaseSalaryPerShift, sl.SalaryPercentIncrease;
--INSERT

INSERT INTO Users (Username, Password, FullName, Phone, Email, Role, Experience, LevelId)
VALUES
('admin','123',N'Nguyễn Minh Quân','0901111111','admin@gmail.com','Admin',5,3),

('staff1','123',N'Trần Văn Hùng','0902222222','hung@gmail.com','Staff',1,1),
('staff2','123',N'Lê Thị Lan','0903333333','lan@gmail.com','Staff',3,2),
('staff3','123',N'Phạm Quốc Bảo','0904444444','bao@gmail.com','Staff',6,3);

INSERT INTO WorkShifts (Name, StartTime, EndTime)
VALUES
(N'Sáng','06:00','12:00'),
(N'Chiều','12:00','18:00'),
(N'Tối','18:00','22:00');

INSERT INTO UserShifts (UserId, ShiftId, WorkDate)
VALUES
(2,1,'2026-07-01'),
(2,2,'2026-07-02'),
(3,2,'2026-07-01'),
(3,3,'2026-07-03'),
(4,1,'2026-07-01'),
(4,3,'2026-07-04');

INSERT INTO Attendances (UserId, WorkDate, CheckIn, CheckOut)
VALUES
(2,'2026-07-01','2026-07-01 06:00','2026-07-01 12:00'),
(2,'2026-07-02','2026-07-02 12:00','2026-07-02 18:00'),

(3,'2026-07-01','2026-07-01 12:00','2026-07-01 18:00'),
(3,'2026-07-03','2026-07-03 18:00','2026-07-03 22:00'),

(4,'2026-07-01','2026-07-01 06:00','2026-07-01 12:00'),
(4,'2026-07-04','2026-07-04 18:00','2026-07-04 22:00');

INSERT INTO Members (Name, Phone, Avatar, IdentityNumber, CardCode)
VALUES
(N'Nguyễn Văn A','0911111111','m1.jpg','123456789','CARD001'),
(N'Trần Thị B','0922222222','m2.jpg','223456789','CARD002'),
(N'Lê Văn C','0933333333','m3.jpg','323456789','CARD003'),
(N'Phạm Thị D','0944444444','m4.jpg','423456789','CARD004'),
(N'Hoàng Văn E','0955555555','m5.jpg','523456789','CARD005');

INSERT INTO TrainerLevels (Name, MinExperience, MaxExperience, PricePercentIncrease)
VALUES
(N'Basic',0,1,10),
(N'Pro',2,4,20),
(N'Elite',5,10,30);

INSERT INTO Trainers (Name, Phone, Email, Experience, SalaryPercent, Specialty, LevelId)
VALUES
(N'Nguyễn PT 1','0981111111','pt1@gmail.com',1,20,N'Giảm cân',1),
(N'Trần PT 2','0982222222','pt2@gmail.com',3,25,N'Tăng cơ',2),
(N'Lê PT 3','0983333333','pt3@gmail.com',6,30,N'Thể hình chuyên sâu',3);

INSERT INTO Packages (Name, Price, DurationValue, DurationType, Description)
VALUES
(N'Gói ngày',50000,1,'Day',N'Tập 1 ngày'),
(N'Gói tháng',500000,1,'Month',N'Tập 1 tháng'),
(N'Gói năm',5000000,1,'Year',N'Tập 1 năm');

INSERT INTO Memberships (MemberId, PackageId, TrainerId, StartDate, EndDate, Price)
VALUES
(1,2,NULL,'2026-07-01','2026-08-01',500000),
(2,2,1,'2026-07-01','2026-08-01',550000),
(3,3,2,'2026-01-01','2027-01-01',6000000),
(4,1,NULL,'2026-07-10','2026-07-11',50000);

INSERT INTO Brands (Name)
VALUES (N'ON'),(N'Muscletech'),(N'Rule1'),(N'Nutri');

INSERT INTO Units (Name)
VALUES (N'Hộp'),(N'Chai'),(N'Cái');

INSERT INTO Products (Name, BrandId, UnitId, Price, Quantity, Barcode, Image)
VALUES
(N'Whey Gold',1,1,850000,50,'P001','/whey.jpg'),
(N'Creatine',2,1,400000,40,'P002','/creatine.jpg'),
(N'BCAA',3,1,600000,30,'P003','/bcaa.jpg'),
(N'Pre Workout',2,1,700000,25,'P004','/pre.jpg'),
(N'Nước suối',1,2,10000,200,'P005','/water.jpg');

INSERT INTO ImportReceipts (Date, Total)
VALUES ('2026-06-01',2000000);

INSERT INTO ImportDetails (ImportId, ProductId, Quantity, Price)
VALUES
(1,1,20,700000),
(1,2,30,300000);

INSERT INTO Invoices (MemberId, Date, Total)
VALUES
(1,'2026-07-05',900000),
(2,'2026-07-06',400000);

INSERT INTO InvoiceDetails (InvoiceId, ProductId, Quantity, Price)
VALUES
(1,1,1,850000),
(1,5,5,10000),
(2,2,1,400000);

INSERT INTO Payments (Amount, Method, CashReceived, ChangeAmount, InvoiceId)
VALUES
(900000,'Cash',1000000,100000,1),
(400000,'Online',NULL,NULL,2);

-- Payment cho membership
INSERT INTO Payments (Amount, Method, CashReceived, ChangeAmount, MembershipId)
VALUES
(500000,'Cash',500000,0,1),
(550000,'Cash',600000,50000,2),
(6000000,'Online',NULL,NULL,3),
(50000,'Cash',100000,50000,4);
-- ========================================
-- DASHBOARD
-- ========================================

-- Doanh thu theo ngày
SELECT 
    CAST(Date AS DATE) AS Day,
    SUM(Total) AS Revenue
FROM Invoices
GROUP BY CAST(Date AS DATE)
ORDER BY Day;

-- Doanh thu theo tháng
SELECT 
    MONTH(Date) AS Month,
    SUM(Total) AS Revenue
FROM Invoices
GROUP BY MONTH(Date);

-- Top sản phẩm
SELECT p.Name, SUM(i.Quantity) AS Sold
FROM InvoiceDetails i
JOIN Products p ON p.ProductId = i.ProductId
GROUP BY p.Name
ORDER BY Sold DESC;

-- Khách VIP
SELECT TOP 5
    m.Name,
    SUM(i.Total) AS TotalSpent
FROM Members m
JOIN Invoices i ON m.MemberId = i.MemberId
GROUP BY m.Name
ORDER BY TotalSpent DESC;

-- Top PT
SELECT t.Name, SUM(m.Price) AS Revenue
FROM Memberships m
JOIN Trainers t ON m.TrainerId = t.TrainerId
GROUP BY t.Name;

SELECT 
    CAST(PaymentDate AS DATE) AS Day,
    SUM(Amount) AS Revenue
FROM Payments
GROUP BY CAST(PaymentDate AS DATE)
ORDER BY Day;

SELECT 
    YEAR(PaymentDate) AS Year,
    MONTH(PaymentDate) AS Month,
    SUM(Amount) AS Revenue
FROM Payments
GROUP BY YEAR(PaymentDate), MONTH(PaymentDate)
ORDER BY Year, Month;

SELECT 
    FORMAT(PaymentDate,'yyyy-MM') AS Month,
    SUM(Amount) AS Revenue
FROM Payments
WHERE PaymentDate >= DATEADD(MONTH,-1,GETDATE())
GROUP BY FORMAT(PaymentDate,'yyyy-MM');

SELECT TOP 5
    p.Name,
    SUM(i.Quantity) AS TotalSold,
    SUM(i.Quantity * i.Price) AS Revenue
FROM InvoiceDetails i
JOIN Products p ON p.ProductId = i.ProductId
GROUP BY p.Name
ORDER BY TotalSold DESC;

SELECT TOP 5
    m.Name,
    SUM(ISNULL(p.Amount,0)) AS TotalSpent
FROM Members m
LEFT JOIN Payments p 
    ON p.MembershipId IN (
        SELECT MembershipId FROM Memberships WHERE MemberId = m.MemberId
    )
    OR p.InvoiceId IN (
        SELECT InvoiceId FROM Invoices WHERE MemberId = m.MemberId
    )
GROUP BY m.Name
ORDER BY TotalSpent DESC;

SELECT TOP 5
    t.Name,
    COUNT(m.MembershipId) AS TotalClients,
    SUM(m.Price) AS Revenue
FROM Trainers t
JOIN Memberships m ON t.TrainerId = m.TrainerId
GROUP BY t.Name
ORDER BY Revenue DESC;

SELECT 
    u.FullName,
    COUNT(a.AttendanceId) AS TotalShift,
    sc.BaseSalaryPerShift,
    sl.SalaryPercentIncrease,

    COUNT(a.AttendanceId) * 
    sc.BaseSalaryPerShift * 
    (1 + sl.SalaryPercentIncrease / 100.0) AS Salary

FROM Users u
JOIN StaffLevels sl ON u.LevelId = sl.LevelId
CROSS JOIN SystemConfig sc
LEFT JOIN Attendances a ON u.UserId = a.UserId

GROUP BY u.FullName, sc.BaseSalaryPerShift, sl.SalaryPercentIncrease;

DECLARE @TargetMonth INT = 7, @TargetYear INT = 2026;

SELECT 
    u.FullName,
    COUNT(a.AttendanceId) AS TotalShifts,
    sc.BaseSalaryPerShift,
    sl.SalaryPercentIncrease,
    (COUNT(a.AttendanceId) * sc.BaseSalaryPerShift * (1 + sl.SalaryPercentIncrease / 100.0)) AS MonthlySalary
FROM Users u
JOIN StaffLevels sl ON u.LevelId = sl.LevelId
CROSS JOIN SystemConfig sc
LEFT JOIN Attendances a ON u.UserId = a.UserId 
    AND MONTH(a.WorkDate) = @TargetMonth 
    AND YEAR(a.WorkDate) = @TargetYear
GROUP BY u.FullName, sc.BaseSalaryPerShift, sl.SalaryPercentIncrease;

SELECT 
    FORMAT(PaymentDate, 'MM-yyyy') AS MonthYear,
    SUM(Amount) AS TotalRevenue,
    COUNT(PaymentId) AS TotalTransactions
FROM Payments
WHERE YEAR(PaymentDate) = 2026 -- Hoặc lọc theo biến
GROUP BY FORMAT(PaymentDate, 'MM-yyyy');

CREATE TABLE MembershipLogs (
    LogId INT IDENTITY PRIMARY KEY,
    MembershipId INT,
    Action NVARCHAR(50), -- 'CREATED', 'UPDATED', 'EXPIRED'
    ChangeDate DATETIME DEFAULT GETDATE(),
    OldEndDate DATE,
    NewEndDate DATE,
    Note NVARCHAR(255)
);
go

CREATE TRIGGER trg_LogMembership
ON Memberships
AFTER INSERT, UPDATE
AS
BEGIN
    INSERT INTO MembershipLogs (MembershipId, Action, OldEndDate, NewEndDate, Note)
    SELECT 
        i.MembershipId, 
        CASE WHEN d.MembershipId IS NULL THEN 'CREATED' ELSE 'UPDATED' END,
        d.EndDate,
        i.EndDate,
        N'Cập nhật bởi hệ thống'
    FROM inserted i
    LEFT JOIN deleted d ON i.MembershipId = d.MembershipId;
END;
GO

UPDATE Users SET Password = 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3';