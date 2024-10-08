IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [WeatherForecasts] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(500) NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [WeatherForecastType] int NOT NULL,
    [TemperatureC] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_WeatherForecasts] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240921193524_InitialMigration', N'8.0.8');
GO

COMMIT;
GO


