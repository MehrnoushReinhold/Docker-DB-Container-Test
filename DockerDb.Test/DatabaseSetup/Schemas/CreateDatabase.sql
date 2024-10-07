IF EXISTS(SELECT *
          FROM sys.databases
          WHERE name = 'WeatherTestDb')
    BEGIN
        DROP DATABASE WeatherTestDb
    END

CREATE DATABASE WeatherTestDb
