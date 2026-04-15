
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/03/2026 23:07:13
-- Generated from EDMX file: D:\GitHub Project\Wedding_Planner\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Wedding_Planner];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AmentiesMaster_EventMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AmentiesMaster] DROP CONSTRAINT [FK_AmentiesMaster_EventMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_AreaMaster_CityMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AreaMaster] DROP CONSTRAINT [FK_AreaMaster_CityMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_BookingMaster_UserMaster1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingMaster] DROP CONSTRAINT [FK_BookingMaster_UserMaster1];
GO
IF OBJECT_ID(N'[dbo].[FK_ComplaintsMaster_UserMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ComplaintsMaster] DROP CONSTRAINT [FK_ComplaintsMaster_UserMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_FeedBackMaster_UserMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeedBackMaster] DROP CONSTRAINT [FK_FeedBackMaster_UserMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_LoginMaster_UserMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LoginMaster] DROP CONSTRAINT [FK_LoginMaster_UserMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMaster_AreaMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMaster] DROP CONSTRAINT [FK_UserMaster_AreaMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMaster_CityMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMaster] DROP CONSTRAINT [FK_UserMaster_CityMaster];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AmentiesMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AmentiesMaster];
GO
IF OBJECT_ID(N'[dbo].[AreaMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AreaMaster];
GO
IF OBJECT_ID(N'[dbo].[BookingMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookingMaster];
GO
IF OBJECT_ID(N'[dbo].[CityMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CityMaster];
GO
IF OBJECT_ID(N'[dbo].[ComplaintsMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComplaintsMaster];
GO
IF OBJECT_ID(N'[dbo].[ContactMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactMaster];
GO
IF OBJECT_ID(N'[dbo].[EnquiryMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnquiryMaster];
GO
IF OBJECT_ID(N'[dbo].[EventMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventMaster];
GO
IF OBJECT_ID(N'[dbo].[FeedBackMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedBackMaster];
GO
IF OBJECT_ID(N'[dbo].[LoginMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginMaster];
GO
IF OBJECT_ID(N'[dbo].[SendEmailMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SendEmailMaster];
GO
IF OBJECT_ID(N'[dbo].[UserMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMaster];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AmentiesMasters'
CREATE TABLE [dbo].[AmentiesMasters] (
    [EventName] varchar(70)  NOT NULL,
    [Laun] varchar(50)  NULL,
    [Hall] varchar(50)  NULL,
    [Music] varchar(50)  NULL,
    [Tent] varchar(50)  NULL,
    [Parlour] varchar(50)  NULL,
    [Catering] varchar(70)  NULL
);
GO

-- Creating table 'AreaMasters'
CREATE TABLE [dbo].[AreaMasters] (
    [Area_Id] int IDENTITY(1,1) NOT NULL,
    [Area_Name] varchar(50)  NULL,
    [Related_City_Id] int  NULL
);
GO

-- Creating table 'BookingMasters'
CREATE TABLE [dbo].[BookingMasters] (
    [BookingId] int IDENTITY(1,1) NOT NULL,
    [BookedBy] varchar(120)  NULL,
    [EventList] varchar(70)  NULL,
    [BookDate] datetime  NULL,
    [GuestCount] int  NULL,
    [Hall] varchar(50)  NULL,
    [Lawn] varchar(50)  NULL,
    [Music] varchar(50)  NULL,
    [Tent] varchar(50)  NULL,
    [Parlour] varchar(50)  NULL,
    [Catering] varchar(50)  NULL,
    [TotalAmount] float  NULL
);
GO

-- Creating table 'CityMasters'
CREATE TABLE [dbo].[CityMasters] (
    [City_Id] int IDENTITY(1,1) NOT NULL,
    [City_Name] varchar(50)  NULL
);
GO

-- Creating table 'ComplaintsMasters'
CREATE TABLE [dbo].[ComplaintsMasters] (
    [SubjectId] int IDENTITY(1,1) NOT NULL,
    [SubjectName] varchar(200)  NULL,
    [Message] varchar(1000)  NULL,
    [ComplainBy] varchar(120)  NULL,
    [Date_Time] varchar(100)  NULL
);
GO

-- Creating table 'ContactMasters'
CREATE TABLE [dbo].[ContactMasters] (
    [Contact_Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [FName] varchar(60)  NULL,
    [EmailId] varchar(120)  NULL,
    [MobNo] varchar(15)  NULL,
    [Message] varchar(500)  NULL,
    [Date_Time] varchar(100)  NULL
);
GO

-- Creating table 'EnquiryMasters'
CREATE TABLE [dbo].[EnquiryMasters] (
    [Enquiry_Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [EmailId] varchar(120)  NULL,
    [MobileNo] varchar(20)  NULL,
    [Enquiry_Message] varchar(max)  NULL,
    [Enquiry_DateTime] datetime  NULL
);
GO

-- Creating table 'EventMasters'
CREATE TABLE [dbo].[EventMasters] (
    [EventId] int IDENTITY(1,1) NOT NULL,
    [EventName] varchar(70)  NOT NULL
);
GO

-- Creating table 'FeedBackMasters'
CREATE TABLE [dbo].[FeedBackMasters] (
    [FeedBackId] int IDENTITY(1,1) NOT NULL,
    [Subject] varchar(150)  NULL,
    [Rating] int  NULL,
    [Message] varchar(500)  NULL,
    [FeedBackBy] varchar(120)  NULL,
    [Date_Time] varchar(100)  NULL
);
GO

-- Creating table 'LoginMasters'
CREATE TABLE [dbo].[LoginMasters] (
    [UserId] varchar(120)  NOT NULL,
    [User_Password] varchar(250)  NULL,
    [User_Type] varchar(20)  NULL,
    [User_Status] bit  NULL,
    [Login_Count] int  NULL,
    [Last_Login_Time] datetime  NULL
);
GO

-- Creating table 'UserMasters'
CREATE TABLE [dbo].[UserMasters] (
    [Name] varchar(70)  NULL,
    [Gender] varchar(10)  NULL,
    [EmailId] varchar(120)  NOT NULL,
    [MobileNo] varchar(20)  NULL,
    [Related_City_Id] int  NULL,
    [Related_Area_Id] int  NULL,
    [Address] varchar(max)  NULL,
    [Picture_File_Name] varchar(250)  NULL,
    [DateTime_Of_Reg] datetime  NULL,
    [Is_Del] bit  NULL
);
GO

-- Creating table 'SendEmailMasters'
CREATE TABLE [dbo].[SendEmailMasters] (
    [Send_Id] int IDENTITY(1,1) NOT NULL,
    [EmailId] varchar(120)  NULL,
    [Subject] varchar(150)  NULL,
    [Message] varchar(550)  NULL,
    [Send_On] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EventName] in table 'AmentiesMasters'
ALTER TABLE [dbo].[AmentiesMasters]
ADD CONSTRAINT [PK_AmentiesMasters]
    PRIMARY KEY CLUSTERED ([EventName] ASC);
GO

-- Creating primary key on [Area_Id] in table 'AreaMasters'
ALTER TABLE [dbo].[AreaMasters]
ADD CONSTRAINT [PK_AreaMasters]
    PRIMARY KEY CLUSTERED ([Area_Id] ASC);
GO

-- Creating primary key on [BookingId] in table 'BookingMasters'
ALTER TABLE [dbo].[BookingMasters]
ADD CONSTRAINT [PK_BookingMasters]
    PRIMARY KEY CLUSTERED ([BookingId] ASC);
GO

-- Creating primary key on [City_Id] in table 'CityMasters'
ALTER TABLE [dbo].[CityMasters]
ADD CONSTRAINT [PK_CityMasters]
    PRIMARY KEY CLUSTERED ([City_Id] ASC);
GO

-- Creating primary key on [SubjectId] in table 'ComplaintsMasters'
ALTER TABLE [dbo].[ComplaintsMasters]
ADD CONSTRAINT [PK_ComplaintsMasters]
    PRIMARY KEY CLUSTERED ([SubjectId] ASC);
GO

-- Creating primary key on [Contact_Id] in table 'ContactMasters'
ALTER TABLE [dbo].[ContactMasters]
ADD CONSTRAINT [PK_ContactMasters]
    PRIMARY KEY CLUSTERED ([Contact_Id] ASC);
GO

-- Creating primary key on [Enquiry_Id] in table 'EnquiryMasters'
ALTER TABLE [dbo].[EnquiryMasters]
ADD CONSTRAINT [PK_EnquiryMasters]
    PRIMARY KEY CLUSTERED ([Enquiry_Id] ASC);
GO

-- Creating primary key on [EventName] in table 'EventMasters'
ALTER TABLE [dbo].[EventMasters]
ADD CONSTRAINT [PK_EventMasters]
    PRIMARY KEY CLUSTERED ([EventName] ASC);
GO

-- Creating primary key on [FeedBackId] in table 'FeedBackMasters'
ALTER TABLE [dbo].[FeedBackMasters]
ADD CONSTRAINT [PK_FeedBackMasters]
    PRIMARY KEY CLUSTERED ([FeedBackId] ASC);
GO

-- Creating primary key on [UserId] in table 'LoginMasters'
ALTER TABLE [dbo].[LoginMasters]
ADD CONSTRAINT [PK_LoginMasters]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [EmailId] in table 'UserMasters'
ALTER TABLE [dbo].[UserMasters]
ADD CONSTRAINT [PK_UserMasters]
    PRIMARY KEY CLUSTERED ([EmailId] ASC);
GO

-- Creating primary key on [Send_Id] in table 'SendEmailMasters'
ALTER TABLE [dbo].[SendEmailMasters]
ADD CONSTRAINT [PK_SendEmailMasters]
    PRIMARY KEY CLUSTERED ([Send_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EventName] in table 'AmentiesMasters'
ALTER TABLE [dbo].[AmentiesMasters]
ADD CONSTRAINT [FK_AmentiesMaster_EventMaster]
    FOREIGN KEY ([EventName])
    REFERENCES [dbo].[EventMasters]
        ([EventName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Related_City_Id] in table 'AreaMasters'
ALTER TABLE [dbo].[AreaMasters]
ADD CONSTRAINT [FK_AreaMaster_CityMaster]
    FOREIGN KEY ([Related_City_Id])
    REFERENCES [dbo].[CityMasters]
        ([City_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AreaMaster_CityMaster'
CREATE INDEX [IX_FK_AreaMaster_CityMaster]
ON [dbo].[AreaMasters]
    ([Related_City_Id]);
GO

-- Creating foreign key on [Related_Area_Id] in table 'UserMasters'
ALTER TABLE [dbo].[UserMasters]
ADD CONSTRAINT [FK_UserMaster_AreaMaster]
    FOREIGN KEY ([Related_Area_Id])
    REFERENCES [dbo].[AreaMasters]
        ([Area_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMaster_AreaMaster'
CREATE INDEX [IX_FK_UserMaster_AreaMaster]
ON [dbo].[UserMasters]
    ([Related_Area_Id]);
GO

-- Creating foreign key on [BookedBy] in table 'BookingMasters'
ALTER TABLE [dbo].[BookingMasters]
ADD CONSTRAINT [FK_BookingMaster_UserMaster1]
    FOREIGN KEY ([BookedBy])
    REFERENCES [dbo].[UserMasters]
        ([EmailId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingMaster_UserMaster1'
CREATE INDEX [IX_FK_BookingMaster_UserMaster1]
ON [dbo].[BookingMasters]
    ([BookedBy]);
GO

-- Creating foreign key on [Related_City_Id] in table 'UserMasters'
ALTER TABLE [dbo].[UserMasters]
ADD CONSTRAINT [FK_UserMaster_CityMaster]
    FOREIGN KEY ([Related_City_Id])
    REFERENCES [dbo].[CityMasters]
        ([City_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMaster_CityMaster'
CREATE INDEX [IX_FK_UserMaster_CityMaster]
ON [dbo].[UserMasters]
    ([Related_City_Id]);
GO

-- Creating foreign key on [ComplainBy] in table 'ComplaintsMasters'
ALTER TABLE [dbo].[ComplaintsMasters]
ADD CONSTRAINT [FK_ComplaintsMaster_UserMaster]
    FOREIGN KEY ([ComplainBy])
    REFERENCES [dbo].[UserMasters]
        ([EmailId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComplaintsMaster_UserMaster'
CREATE INDEX [IX_FK_ComplaintsMaster_UserMaster]
ON [dbo].[ComplaintsMasters]
    ([ComplainBy]);
GO

-- Creating foreign key on [FeedBackBy] in table 'FeedBackMasters'
ALTER TABLE [dbo].[FeedBackMasters]
ADD CONSTRAINT [FK_FeedBackMaster_UserMaster]
    FOREIGN KEY ([FeedBackBy])
    REFERENCES [dbo].[UserMasters]
        ([EmailId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FeedBackMaster_UserMaster'
CREATE INDEX [IX_FK_FeedBackMaster_UserMaster]
ON [dbo].[FeedBackMasters]
    ([FeedBackBy]);
GO

-- Creating foreign key on [UserId] in table 'LoginMasters'
ALTER TABLE [dbo].[LoginMasters]
ADD CONSTRAINT [FK_LoginMaster_UserMaster]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserMasters]
        ([EmailId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

CREATE TABLE ChatMessages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserMessage NVARCHAR(MAX),
    BotReply NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------