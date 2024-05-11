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
(NEWID(), 'Guest'),
(NEWID(), 'Free User'),
(NEWID(), 'Premium User'),
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

-- Table: Quizzes
CREATE TABLE Quizzes (
  QuizID UNIQUEIDENTIFIER PRIMARY KEY,
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

-- Table: Questions
CREATE TABLE Questions (
  QuestionID UNIQUEIDENTIFIER PRIMARY KEY,
  QuizID UNIQUEIDENTIFIER NOT NULL,
  Type VARCHAR(50),
  QuestionText TEXT,
  FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
GO

-- Table: Answers
CREATE TABLE Answers (
  AnswerID UNIQUEIDENTIFIER PRIMARY KEY,
  QuestionID UNIQUEIDENTIFIER NOT NULL,
  Content TEXT,
  IsCorrect BIT,
  FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID)
);
GO

-- Table: QuizResults
CREATE TABLE QuizResults (
  ResultID UNIQUEIDENTIFIER PRIMARY KEY,
  UserID UNIQUEIDENTIFIER NOT NULL,
  QuizID UNIQUEIDENTIFIER NOT NULL,
  Score INT,
  StartedAt DATETIME,
  FinishedAt DATETIME,
  FOREIGN KEY (UserID) REFERENCES Users(UserID),
  FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
GO

-- Table: UserAnswers
CREATE TABLE UserAnswers (
  UserAnswerID UNIQUEIDENTIFIER PRIMARY KEY,
  ResultID UNIQUEIDENTIFIER NOT NULL,
  QuestionID UNIQUEIDENTIFIER NOT NULL,
  AnswerID UNIQUEIDENTIFIER NOT NULL,
  IsCorrect BIT,
  FOREIGN KEY (ResultID) REFERENCES QuizResults(ResultID),
  FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID),
  FOREIGN KEY (AnswerID) REFERENCES Answers(AnswerID)
);
GO
