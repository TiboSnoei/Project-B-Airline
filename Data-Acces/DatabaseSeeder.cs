using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

public class DatabaseSeeder
{
    private readonly string _dbPath;
    private readonly string _connectionString;

    public DatabaseSeeder()
    {
        _dbPath = "data/airline.db";
        _connectionString = $"Data Source={_dbPath}";
    }

    public void EnsureDatabase()
    {
        string folder = Path.GetDirectoryName(_dbPath);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        // If the database file does not exist, create it and seed tables
        if (!File.Exists(_dbPath))
        {
            Console.WriteLine("No Database found. Creating database for testing.");
            CreateTables();
            Console.WriteLine("Seeding testing database");
            SeedData();
        }
        else
        {
            Console.WriteLine("Database already exists.");
        }
    }

    private void CreateTables()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var commands = new List<string>
        {
            @"CREATE TABLE Plane (
                TailNumber VARCHAR(100) PRIMARY KEY,
                SeatCount INTEGER NOT NULL,
                Model VARCHAR(100) NOT NULL
            );",

            @"CREATE TABLE Users (
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                UserType VARCHAR(100) NOT NULL,
                FirstName VARCHAR(100) NOT NULL,
                LastName VARCHAR(100) NOT NULL,
                Password VARCHAR(100) NOT NULL,
                Email VARCHAR(100) NOT NULL,
                created_at DATETIME NOT NULL,
                TelNum VARCHAR(100) NOT NULL,
                LoyaltyPoints INTEGER NOT NULL
            );",

            @"CREATE TABLE Flight (
                FlightID INTEGER PRIMARY KEY AUTOINCREMENT,
                TailNumber VARCHAR(100) NOT NULL,
                Destination VARCHAR(100) NOT NULL,
                Origin VARCHAR(100) NOT NULL,
                ArrivalTime DATETIME NOT NULL,
                DepartureTime DATETIME NOT NULL,
                LegroomFee DECIMAL(10,2) NOT NULL,
                DefaultPrice DECIMAL(10,2) NOT NULL,
                MealFee DECIMAL(10,2) NOT NULL,
                ChosenSeatFee DECIMAL(10,2) NOT NULL,
                ExtraLuggageFee DECIMAL(10,2) NOT NULL,
                FOREIGN KEY (TailNumber) REFERENCES Plane(TailNumber)
            );",

            @"CREATE TABLE CustomerFlight (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                UserID INTEGER NOT NULL,
                FlightID INTEGER NOT NULL,
                Seat VARCHAR(100) NOT NULL,
                SeatChosen BOOLEAN NOT NULL,
                ExtraLegroom BOOLEAN NOT NULL,
                OnflightMeal BOOLEAN NOT NULL,
                ExtraLuggage BOOLEAN NOT NULL,
                FOREIGN KEY(UserID) REFERENCES Users(UserID),
                FOREIGN KEY(FlightID) REFERENCES Flight(FlightID)
            );"
        };

        foreach (var cmdText in commands)
        {
            using var cmd = new SqliteCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine("Table creation succesfull.");
    }

    private void SeedData()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var commands = new List<string>
        {
            "INSERT INTO Plane (TailNumber, SeatCount, Model) VALUES " +
            "('HR101', 250, 'Boeing-737')," +
            "('HR102', 250, 'Boeing-737')," +
            "('HR103', 345, 'Airbus 330')," +
            "('HR104', 345, 'Airbus 330');",

            "INSERT INTO Users (UserType, FirstName, LastName, Password, Email, created_at, TelNum, LoyaltyPoints) VALUES " +
            "('Customer', 'Bob', 'Marten', 'BobMarten_120805', 'BobMarten@gmail.com', '2026-05-01 00:00:00', '0676543566', 0)," +
            "('Admin', 'Ad', 'Min', 'Admin_1', 'Admin@duckteep.com', '2026-05-01 00:00:00', '0676543566', 0);",

            "INSERT INTO Flight (TailNumber, Origin, Destination, DepartureTime, ArrivalTime, LegroomFee, DefaultPrice, MealFee, ChosenSeatFee, ExtraLuggageFee) VALUES " +
            "('HR101', 'Rotterdam', 'Berlin', '2026-05-01 12:45:00', '2026-05-01 14:15:00', 100, 100, 100, 100, 100)," +
            "('HR101', 'Rotterdam', 'Berlin', '2025-05-01 11:45:00', '2026-05-01 13:15:00', 100, 100, 100, 100, 100)," +
            "('HR102', 'Rotterdam', 'Berlin', '2026-05-01 10:45:00', '2026-05-01 12:15:00', 100, 100, 100, 100, 100)," +
            "('HR102', 'Rotterdam', 'Berlin', '2026-05-01 09:45:00', '2026-05-01 11:15:00', 100, 100, 100, 100, 100)," +
            "('HR102', 'Rotterdam', 'Berlin', '2026-05-01 08:45:00', '2026-05-01 10:15:00', 100, 100, 100, 100, 100)," +
            "('HR103', 'Berlin', 'Rotterdam', '2026-06-01 12:45:00', '2026-05-01 14:15:00', 100, 100, 100, 100, 100)," +
            "('HR103', 'Berlin', 'Rotterdam', '2026-06-01 11:45:00', '2026-05-01 13:15:00', 100, 100, 100, 100, 100)," +
            "('HR103', 'Berlin', 'Rotterdam', '2026-06-01 10:45:00', '2026-05-01 12:15:00', 100, 100, 100, 100, 100)," +
            "('HR104', 'Berlin', 'Rotterdam', '2026-06-01 09:45:00', '2026-05-01 11:15:00', 100, 100, 100, 100, 100)," +
            "('HR104', 'Rotterdam', 'Madrid', '2026-05-01 12:45:00', '2026-05-01 14:15:00', 100, 100, 100, 100, 100);",

            "INSERT INTO CustomerFlight (UserID, FlightID, Seat, SeatChosen, ExtraLegroom, OnflightMeal, ExtraLuggage) VALUES " +
            "(1, 1, '12C', 0, 0, 0, 0);"
        };

        foreach (var cmdText in commands)
        {
            using var cmd = new SqliteCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine("Seed succesfull.");
    }
}