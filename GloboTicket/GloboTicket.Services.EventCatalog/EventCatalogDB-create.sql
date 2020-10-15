IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Categories] (
    [CategoryId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);

GO

CREATE TABLE [Events] (
    [EventId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Price] int NOT NULL,
    [Artist] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([EventId]),
    CONSTRAINT [FK_Events_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Events_CategoryId] ON [Events] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200630132753_initial', N'3.1.5');

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('cfb88e29-4744-48c0-94fa-b25b92dea314', N'Concerts');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('cfb88e29-4744-48c0-94fa-b25b92dea315', N'Musicals');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('cfb88e29-4744-48c0-94fa-b25b92dea316', N'Plays');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('cfb88e29-4744-48c0-94fa-b25b92dea317', N'John Egbert', 'cfb88e29-4744-48c0-94fa-b25b92dea314', '2021-01-11T16:22:42.2504932+01:00', N'Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.', N'/img/banjo.jpg', N'John Egbert Live', 65);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('cfb88e29-4744-48c0-94fa-b25b92dea319', N'Michael Johnson', 'cfb88e29-4744-48c0-94fa-b25b92dea314', '2021-04-11T16:22:42.2525753+02:00', N'Michael Johnson doesn''t need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?', N'/img/michael.jpg', N'The State of Affairs: Michael Live!', 85);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('cfb88e29-4744-48c0-94fa-b25b92dea318', N'Nick Sailor', 'cfb88e29-4744-48c0-94fa-b25b92dea315', '2021-03-11T16:22:42.2525838+01:00', N'The critics are over the moon and so will you after you''ve watched this sing and dance extravaganza written by Nick Sailor, the man from ''My dad and sister''.', N'/img/musical.jpg', N'To the Moon and Back', 135);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200711142242_InitialData', N'3.1.5');

GO

DELETE FROM [Categories]
WHERE [CategoryId] = 'cfb88e29-4744-48c0-94fa-b25b92dea316';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Events]
WHERE [EventId] = 'cfb88e29-4744-48c0-94fa-b25b92dea317';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Events]
WHERE [EventId] = 'cfb88e29-4744-48c0-94fa-b25b92dea318';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Events]
WHERE [EventId] = 'cfb88e29-4744-48c0-94fa-b25b92dea319';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Categories]
WHERE [CategoryId] = 'cfb88e29-4744-48c0-94fa-b25b92dea314';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Categories]
WHERE [CategoryId] = 'cfb88e29-4744-48c0-94fa-b25b92dea315';
SELECT @@ROWCOUNT;


GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('b0788d2f-8003-43c1-92a4-edc76a7c5dde', N'Concerts');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('6313179f-7837-473a-a4d5-a5571b43e6a6', N'Musicals');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('bf3f3002-7e53-441e-8b76-f6280be284aa', N'Plays');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('ee272f8b-6096-4cb6-8625-bb4bb2d89e8b', N'John Egbert', 'b0788d2f-8003-43c1-92a4-edc76a7c5dde', '2021-02-15T17:50:11.6260730+01:00', N'Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.', N'/img/banjo.jpg', N'John Egbert Live', 65);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('3448d5a4-0f72-4dd7-bf15-c14a46b26c00', N'Michael Johnson', 'b0788d2f-8003-43c1-92a4-edc76a7c5dde', '2021-05-15T17:50:11.6285304+02:00', N'Michael Johnson doesn''t need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?', N'/img/michael.jpg', N'The State of Affairs: Michael Live!', 85);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('62787623-4c52-43fe-b0c9-b7044fb5929b', N'Nick Sailor', '6313179f-7837-473a-a4d5-a5571b43e6a6', '2021-04-15T17:50:11.6285633+02:00', N'The critics are over the moon and so will you after you''ve watched this sing and dance extravaganza written by Nick Sailor, the man from ''My dad and sister''.', N'/img/musical.jpg', N'To the Moon and Back', 135);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200815155011_DifferentGuids', N'3.1.5');

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES ('fe98f549-e790-4e9f-aa16-18c2292a2ee9', N'Conferences');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

UPDATE [Events] SET [Date] = '2021-05-23T12:01:42.3689706+02:00', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg'
WHERE [EventId] = '3448d5a4-0f72-4dd7-bf15-c14a46b26c00';
SELECT @@ROWCOUNT;


GO

UPDATE [Events] SET [Artist] = N'Manuel Santinonisi', [CategoryId] = 'b0788d2f-8003-43c1-92a4-edc76a7c5dde', [Date] = '2020-12-23T12:01:42.3689867+01:00', [Description] = N'Get on the hype of Spanish Guitar concerts with Manuel.', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/guitar.jpg', [Name] = N'Spanish guitar hits with Manuel', [Price] = 25
WHERE [EventId] = '62787623-4c52-43fe-b0c9-b7044fb5929b';
SELECT @@ROWCOUNT;


GO

UPDATE [Events] SET [Date] = '2021-02-23T12:01:42.3670510+01:00', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg'
WHERE [EventId] = 'ee272f8b-6096-4cb6-8625-bb4bb2d89e8b';
SELECT @@ROWCOUNT;


GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('b419a7ca-3321-4f38-be8e-4d7b6a529319', N'DJ ''The Mike''', 'b0788d2f-8003-43c1-92a4-edc76a7c5dde', '2020-12-23T12:01:42.3689837+01:00', N'DJs from all over the world will compete in this epic battle for eternal fame.', N'https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg', N'Clash of the DJs', 85),
('adc42c09-08c1-4d2c-9f96-2d15bb1af299', N'Nick Sailor', '6313179f-7837-473a-a4d5-a5571b43e6a6', '2021-04-23T12:01:42.3689915+02:00', N'The critics are over the moon and so will you after you''ve watched this sing and dance extravaganza written by Nick Sailor, the man from ''My dad and sister''.', N'/img/musical.jpg', N'To the Moon and Back', 135);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] ON;
INSERT INTO [Events] ([EventId], [Artist], [CategoryId], [Date], [Description], [ImageUrl], [Name], [Price])
VALUES ('1babd057-e980-4cb3-9cd2-7fdd9e525668', N'Many', 'fe98f549-e790-4e9f-aa16-18c2292a2ee9', '2021-06-23T12:01:42.3689889+02:00', N'The best tech conference in the world', N'https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/conf.jpg', N'Techorama 2021', 400);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EventId', N'Artist', N'CategoryId', N'Date', N'Description', N'ImageUrl', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Events]'))
    SET IDENTITY_INSERT [Events] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200823100142_moreData', N'3.1.5');

GO

