USE University;
GO

CREATE TABLE Users
(
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Boards
(
    BoardId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000),
    OwnerId INT FOREIGN KEY REFERENCES Users(UserId)
);

CREATE TABLE BoardRoles
(
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE UserBoardRoles
(
    UserBoardRoleId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    BoardId INT FOREIGN KEY REFERENCES Boards(BoardId),
    RoleId INT FOREIGN KEY REFERENCES BoardRoles(RoleId),
    UNIQUE (UserId, BoardId)
);

CREATE TABLE Tasks
(
    TaskId INT PRIMARY KEY IDENTITY(1,1),
    BoardId INT FOREIGN KEY REFERENCES Boards(BoardId),
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000),
    Status NVARCHAR(50) NOT NULL,
    Deadline DATETIME,
    AssignedUserId INT FOREIGN KEY REFERENCES Users(UserId)
);

INSERT INTO BoardRoles (RoleName) VALUES ('Owner');
INSERT INTO BoardRoles (RoleName) VALUES ('Basic');
INSERT INTO BoardRoles (RoleName) VALUES ('Reader');