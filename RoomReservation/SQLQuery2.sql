USE Reservation
GO

CREATE TABLE [Hotels] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [Rating] INT NOT NULL,
    [is_available] BIT
)

CREATE TABLE [Rooms] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
	[Hotel_id] INT NOT NULL,
    [Category] VARCHAR(10) NOT NULL,
    [capacity] INT NOT NULL,
    [is_available] BIT,
    FOREIGN KEY (Hotel_id) REFERENCES Hotels(Id)
)

CREATE TABLE [Bookings] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
	[Room_id] INT NOT NULL,
    [Start_date] DATE NOT NULL,
    [End_date] DATE NOT NULL,
    [Guest_count] INT NOT NULL,
    [Guest_name] VARCHAR(50) NOT NULL,
    [is_cancelled] BIT
)