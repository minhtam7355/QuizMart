IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'QuizMartDB')
BEGIN
    CREATE DATABASE QuizMartDB;
END
GO

USE QuizMartDB;
GO

-- Table: Roles
CREATE TABLE Roles (
  RoleID UNIQUEIDENTIFIER PRIMARY KEY,
  RoleName VARCHAR(50) NOT NULL
);

-- Inserting role data
INSERT INTO Roles (RoleID, RoleName) VALUES
(NEWID(), 'FreeUser'),
(NEWID(), 'PremiumUser'),
(NEWID(), 'Moderator'),
(NEWID(), 'Admin');
GO

-- Table: Users
CREATE TABLE Users (
  UserID UNIQUEIDENTIFIER PRIMARY KEY,
  Username VARCHAR(255),
  PasswordHash NVARCHAR(255),
  Email VARCHAR(255),
  FullName VARCHAR(255),
  PhoneNumber VARCHAR(20),
  HomeAddress VARCHAR(255),
  DateOfBirth DATE,
  Gender VARCHAR(50),
  ProfilePicture NVARCHAR(MAX),
  RoleID UNIQUEIDENTIFIER,
  FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

-- Table: Decks
CREATE TABLE Decks (
  DeckID UNIQUEIDENTIFIER PRIMARY KEY,
  HostID UNIQUEIDENTIFIER NOT NULL,
  Title VARCHAR(100),
  Description TEXT,
  PublishedAt DATETIME,
  Status VARCHAR(20),
  ModeratorID UNIQUEIDENTIFIER,
  FOREIGN KEY (HostID) REFERENCES Users(UserID),
  FOREIGN KEY (ModeratorID) REFERENCES Users(UserID)
);
GO

-- Table: Quizzes
CREATE TABLE Quizzes (
  QuizID UNIQUEIDENTIFIER PRIMARY KEY,
  DeckID UNIQUEIDENTIFIER NOT NULL,
  Type VARCHAR(50),
  QuestionText TEXT,
  Favorite BIT,
  FOREIGN KEY (DeckID) REFERENCES Decks(DeckID) ON DELETE CASCADE
);
GO

-- Table: Choices
CREATE TABLE Choices (
  ChoiceID UNIQUEIDENTIFIER PRIMARY KEY,
  QuizID UNIQUEIDENTIFIER NOT NULL,
  Content TEXT,
  IsCorrect BIT,
  FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID) ON DELETE CASCADE
);
GO

CREATE TABLE Requests (
    RequestID UNIQUEIDENTIFIER PRIMARY KEY,
    RequestType VARCHAR(255), -- 'Add' or 'Update'
    RequestStatus BIT, -- Status of the request 'Pending', 'Approved', 'Rejected'
    RequestDate DATETIME, -- Store both date and time
    DeckID UNIQUEIDENTIFIER,
	HostID UNIQUEIDENTIFIER NOT NULL,
    ModeratorID UNIQUEIDENTIFIER,
    FOREIGN KEY (DeckID) REFERENCES Decks(DeckID) ON DELETE CASCADE,
    FOREIGN KEY (HostID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ModeratorID) REFERENCES Users(UserID)
);


-- Inserting sample user data for each role
INSERT INTO Users (UserID, Username, PasswordHash, Email, FullName, PhoneNumber, HomeAddress, DateOfBirth, Gender, ProfilePicture, RoleID)
VALUES
    -- FreeUser
    (NEWID(), 'freeuser123', '$2a$13$gksxMKJtj/XTIEh.Pclv0u0UxzBPuhIHhtbx9DrHuJ.s8HyuOE6ri', 'freeuser@example.com', 'John Doe', '1234567890', '123 Free St, FreeCity', '1990-01-01', 'Male', NULL, (SELECT RoleID FROM Roles WHERE RoleName = 'FreeUser')),
    
    -- PremiumUser
    (NEWID(), 'premiumuser456', '$2a$13$gksxMKJtj/XTIEh.Pclv0u0UxzBPuhIHhtbx9DrHuJ.s8HyuOE6ri', 'premiumuser@example.com', 'Jane Smith', '9876543210', '456 Premium Ave, PremiumCity', '1985-05-15', 'Female', NULL, (SELECT RoleID FROM Roles WHERE RoleName = 'PremiumUser')),
    
    -- Moderator
    (NEWID(), 'moderator789', '$2a$13$gksxMKJtj/XTIEh.Pclv0u0UxzBPuhIHhtbx9DrHuJ.s8HyuOE6ri', 'moderator@example.com', 'Moderator Jones', '5551234567', '789 Moderator Blvd, ModCity', '1975-08-20', 'Male', NULL, (SELECT RoleID FROM Roles WHERE RoleName = 'Moderator')),
    
    -- Admin
    (NEWID(), 'admin101', '$2a$13$gksxMKJtj/XTIEh.Pclv0u0UxzBPuhIHhtbx9DrHuJ.s8HyuOE6ri', 'admin@example.com', 'Admin Smith', '4449876543', '101 Admin St, AdminCity', '1980-12-10', 'Female', NULL, (SELECT RoleID FROM Roles WHERE RoleName = 'Admin'));
