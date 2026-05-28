using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;

public class SeatAccess
{
    private readonly string _connectionString;

    public SeatAccess()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

    public List<SeatModel> GetSeatByFlight(FlightModel flight)
    {
        var results = new List<SeatModel>();

        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT 
                s.ID,
                s.FlightID,
                s.SeatNumber,
                s.Class,
                s.ExtraLegroom,
                s.OnflightMeal,
                s.ExtraLuggage,
                cf.UserID
            FROM Seats s
            LEFT JOIN CustomerFlightSeat cfs ON cfs.SeatID = s.ID
            LEFT JOIN CustomerFlight cf ON cf.ID = cfs.CustomerFlightID
            WHERE s.FlightID = @FlightId
        ";

        cmd.Parameters.AddWithValue("@FlightId", flight.FlightId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            results.Add(new SeatModel
            {
                ID = reader.GetInt32(0),
                FlightID = reader.GetInt32(1),
                SeatNumber = reader.GetString(2),
                Class = reader.GetString(3),
                ExtraLegroom = reader.GetBoolean(4),
                OnflightMeal = reader.GetBoolean(5),
                ExtraLuggage = reader.GetBoolean(6),

                // NEW FIELD
                UserID = reader.IsDBNull(7) ? null : reader.GetInt32(7)
            });
        }

        return results;
    }
}