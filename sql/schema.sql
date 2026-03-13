CREATE TABLE Users (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  FullName NVARCHAR(120) NOT NULL,
  Email NVARCHAR(150) NOT NULL UNIQUE,
  PhoneNumber NVARCHAR(20) NOT NULL,
  PasswordHash NVARCHAR(256) NOT NULL,
  Role INT NOT NULL,
  IsActive BIT NOT NULL DEFAULT 1,
  CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE ServicePlans (
  Id INT IDENTITY PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL,
  Description NVARCHAR(300) NOT NULL,
  VehicleType INT NOT NULL,
  Frequency INT NOT NULL,
  Price DECIMAL(10,2) NOT NULL,
  VisitsPerCycle INT NOT NULL,
  IncludesInteriorCleaning BIT NOT NULL,
  IncludesPolishing BIT NOT NULL,
  IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Vehicles (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Users(Id),
  RegistrationNumber NVARCHAR(30) NOT NULL UNIQUE,
  Brand NVARCHAR(80) NOT NULL,
  Model NVARCHAR(80) NOT NULL,
  Color NVARCHAR(40) NOT NULL,
  VehicleType INT NOT NULL,
  ParkingSpot NVARCHAR(50) NOT NULL
);

CREATE TABLE Subscriptions (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Users(Id),
  VehicleId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
  ServicePlanId INT NOT NULL FOREIGN KEY REFERENCES ServicePlans(Id),
  StartDateUtc DATETIME2 NOT NULL,
  EndDateUtc DATETIME2 NULL,
  Status INT NOT NULL,
  RemainingVisits INT NOT NULL,
  CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE WorkOrders (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  SubscriptionId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Subscriptions(Id),
  ServiceDate DATE NOT NULL,
  Status INT NOT NULL,
  AssignedStaffId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES Users(Id),
  AssignedById UNIQUEIDENTIFIER NULL,
  Notes NVARCHAR(500) NOT NULL DEFAULT '',
  BeforeImageUrl NVARCHAR(500) NULL,
  AfterImageUrl NVARCHAR(500) NULL,
  CompletedAtUtc DATETIME2 NULL
);

CREATE TABLE Payments (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  SubscriptionId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Subscriptions(Id),
  Amount DECIMAL(10,2) NOT NULL,
  PaidOnUtc DATETIME2 NOT NULL,
  ReferenceNumber NVARCHAR(80) NOT NULL,
  PaymentMode NVARCHAR(30) NOT NULL
);

INSERT INTO ServicePlans (Name, Description, VehicleType, Frequency, Price, VisitsPerCycle, IncludesInteriorCleaning, IncludesPolishing)
VALUES
('Daily Exterior Wash', 'Daily exterior cleaning for cars', 1, 1, 1499, 30, 0, 0),
('Weekly Full Wash', 'Weekly exterior + interior cleaning for cars', 1, 2, 1199, 4, 1, 0),
('Monthly Polish', 'One-time premium deep cleaning and polish', 1, 3, 999, 1, 1, 1),
('Bike Daily Wash', 'Daily cleaning for bikes', 2, 1, 699, 30, 0, 0);
