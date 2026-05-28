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
        string? folder = Path.GetDirectoryName(_dbPath);

        if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

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

        using (var pragma = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))
            pragma.ExecuteNonQuery();

        var commands = new List<string>
        {
            @"CREATE TABLE Plane (
                TailNumber VARCHAR(100) PRIMARY KEY,
                SeatCount INTEGER NOT NULL,
                Model VARCHAR(100) NOT NULL,
                SeatLayout TEXT NOT NULL
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
                FlightNumber VARCHAR(100) NOT NULL,
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
                SeatChosen BOOLEAN NOT NULL,
                FOREIGN KEY(UserID) REFERENCES Users(UserID),
                FOREIGN KEY(FlightID) REFERENCES Flight(FlightID)
            );",

            @"CREATE TABLE CustomerFlightSeat (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                CustomerFlightID INTEGER NOT NULL,
                SeatID INTEGER NOT NULL,
                FOREIGN KEY(CustomerFlightID) REFERENCES CustomerFlight(ID),
                FOREIGN KEY(SeatID) REFERENCES Seats(ID)
            );",

            @"CREATE TABLE Seats (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                FlightID INTEGER NOT NULL,
                SeatNumber VARCHAR(100) NOT NULL,
                Class VARCHAR(100) NOT NULL,
                ExtraLegroom BOOLEAN NOT NULL,
                OnflightMeal BOOLEAN NOT NULL,
                ExtraLuggage BOOLEAN NOT NULL,
                FOREIGN KEY(FlightID) REFERENCES Flight(FlightID)
            );"
        };

        foreach (var cmdText in commands)
        {
            using var cmd = new SqliteCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine("Table creation successful.");
    }

    private void SeedData()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using (var pragma = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))
            pragma.ExecuteNonQuery();

        string hashedBobPassword = BCrypt.Net.BCrypt.HashPassword("BobMarten_120805");
        string hashedAdminPassword = BCrypt.Net.BCrypt.HashPassword("Admin_1");

        var commands = new List<string>
        {
            @"INSERT INTO Plane (TailNumber, SeatCount, Model, SeatLayout) VALUES
            ('HR101', 30, 'Boeing-737', 'layout'),
            ('HR102', 30, 'Boeing-737', 'layout'),
            ('HR103', 40, 'Airbus A330', 'layout'),
            ('HR104', 40, 'Airbus A330', 'layout');",

            "INSERT INTO Users (UserType, FirstName, LastName, Password, Email, created_at, TelNum, LoyaltyPoints) VALUES " +
            $"('Customer', 'Bob', 'Marten', '{hashedBobPassword}', 'BobMarten@gmail.com', '2026-05-01 00:00:00', '0676543566', 0)," +
            $"('Admin', 'Ad', 'Min', '{hashedAdminPassword}', 'Admin@duckteep.com', '2026-05-01 00:00:00', '0676543566', 0);",

            "INSERT INTO Flight (TailNumber, FlightNumber, Origin, Destination, DepartureTime, ArrivalTime, LegroomFee, DefaultPrice, MealFee, ChosenSeatFee, ExtraLuggageFee) VALUES " +
            "('HR101', 'RO 1122', 'Rotterdam', 'Berlin', '2026-05-01 12:45:00', '2026-05-01 14:15:00', 100, 100, 100, 100, 100);",

            @"INSERT INTO Seats (FlightID, SeatNumber, Class, ExtraLegroom, OnflightMeal, ExtraLuggage) VALUES
            (1, '1A', 'First', 1, 1, 1),
            (1, '1B', 'Business', 1, 1, 0),
            (1, '1C', 'Business', 0, 1, 0),
            (1, '1D', 'Business', 0, 0, 0),
            (1, '1E', 'Business', 0, 0, 0),
            (1, '1F', 'Business', 0, 0, 0);",

            @"INSERT INTO CustomerFlight (UserID, FlightID, SeatChosen) VALUES
            (1, 1, 1);",

            @"INSERT INTO CustomerFlightSeat (CustomerFlightID, SeatID) VALUES
            (1, 1),
            (1, 2),
            (1, 3);"
        };

        foreach (var cmdText in commands)
        {
            using var cmd = new SqliteCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine("Seed successful.");
    }
}