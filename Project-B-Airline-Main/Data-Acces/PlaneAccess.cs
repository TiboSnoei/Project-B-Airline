using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;

public class PlaneAccess
{
    private readonly string _connectionString;

    public PlaneAccess()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

    public PlaneModel GetPlaneByTailNumber(string tailNumber)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT 
            TailNumber, SeatCount, Model, SeatLayout
        FROM Plane WHERE TailNumber = @TailNumber";

        cmd.Parameters.AddWithValue("@TailNumber", tailNumber);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new PlaneModel
            {
                TailNumber = reader.GetString(0),
                SeatCount = reader.GetInt32(1),
                Model = reader.GetString(2),
                SeatLayout = reader.GetString(3),
            };
        }

        return null;
    }
}