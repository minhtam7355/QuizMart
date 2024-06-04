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
  StartsAt DATETIME,
  EndsAt DATETIME,
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
  FOREIGN KEY (DeckID) REFERENCES Decks(DeckID)
);
GO

-- Table: Choices
CREATE TABLE Choices (
  ChoiceID UNIQUEIDENTIFIER PRIMARY KEY,
  QuizID UNIQUEIDENTIFIER NOT NULL,
  Content TEXT,
  IsCorrect BIT,
  FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
GO

