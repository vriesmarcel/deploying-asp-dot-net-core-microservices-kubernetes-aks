IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Baskets] (
    [BasketId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Baskets] PRIMARY KEY ([BasketId])
);

GO

CREATE TABLE [BasketLines] (
    [BasketLineId] uniqueidentifier NOT NULL,
    [BasketId] uniqueidentifier NOT NULL,
    [EventId] uniqueidentifier NOT NULL,
    [TicketAmount] int NOT NULL,
    CONSTRAINT [PK_BasketLines] PRIMARY KEY ([BasketLineId]),
    CONSTRAINT [FK_BasketLines_Baskets_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [Baskets] ([BasketId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_BasketLines_BasketId] ON [BasketLines] ([BasketId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200630142717_InitialMigration', N'3.1.5');

GO

CREATE TABLE [Events] (
    [EventId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([EventId])
);

GO

CREATE INDEX [IX_BasketLines_EventId] ON [BasketLines] ([EventId]);

GO

ALTER TABLE [BasketLines] ADD CONSTRAINT [FK_BasketLines_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200711083614_Events', N'3.1.5');

GO

ALTER TABLE [BasketLines] ADD [Price] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200711091215_BasketLinePrice', N'3.1.5');

GO

ALTER TABLE [Baskets] ADD [CouponId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200815084323_CouponIdAddedToBasket', N'3.1.5');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Baskets]') AND [c].[name] = N'CouponId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Baskets] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Baskets] ALTER COLUMN [CouponId] uniqueidentifier NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200815102156_CouponIdNullable', N'3.1.5');

GO

CREATE TABLE [BasketChangeEvents] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [EventId] uniqueidentifier NOT NULL,
    [InsertedAt] datetimeoffset NOT NULL,
    [BasketChangeType] int NOT NULL,
    CONSTRAINT [PK_BasketChangeEvents] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200815140846_BasketChangeEventAdded', N'3.1.5');

GO

