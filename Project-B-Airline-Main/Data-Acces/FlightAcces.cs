using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;

public class FlightAccess
{
    private readonly string _connectionString;

    public FlightAccess()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

    public bool Write(FlightModel flight)
    {
        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            INSERT INTO Flight 
            (TailNumber, Origin, Destination, DepartureTime, ArrivalTime, LegroomFee, DefaultPrice, MealFee, ChosenSeatFee, ExtraLuggageFee)
            VALUES 
            ($TailNumber, $Origin, $Destination, $DepartureTime, $ArrivalTime, $LegroomFee, $DefaultPrice, $MealFee, $ChosenSeatFee, $ExtraLuggageFee)";

            cmd.Parameters.AddWithValue("$TailNumber", flight.TailNumber);
            cmd.Parameters.AddWithValue("$Origin", flight.Origin);
            cmd.Parameters.AddWithValue("$Destination", flight.Destination);
            cmd.Parameters.AddWithValue("$DepartureTime", flight.TakeOffTime);
            cmd.Parameters.AddWithValue("$ArrivalTime", flight.ArrivalTime);
            cmd.Parameters.AddWithValue("$LegroomFee", flight.LegroomFee);
            cmd.Parameters.AddWithValue("$DefaultPrice", flight.DefaultPrice);
            cmd.Parameters.AddWithValue("$MealFee", flight.MealPrice);
            cmd.Parameters.AddWithValue("$ChosenSeatFee", flight.ChosenSeatFee);
            cmd.Parameters.AddWithValue("$ExtraLuggageFee", flight.ExtraLuggageFee);
            
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing flight to database: {ex.Message}");
            return false;
        }
    }

    public bool Update(FlightModel flight)
    {
        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE Flight
                SET 
                    TailNumber = $TailNumber,
                    Origin = $Origin,
                    Destination = $Destination,
                    DepartureTime = $DepartureTime,
                    ArrivalTime = $ArrivalTime,
                    LegroomFee = $LegroomFee,
                    DefaultPrice = $DefaultPrice,
                    MealFee = $MealFee,
                    ChosenSeatFee = $ChosenSeatFee,
                    ExtraLuggageFee = $ExtraLuggageFee
                WHERE FlightId = $FlightId";

            cmd.Parameters.AddWithValue("$FlightId", flight.FlightId);
            cmd.Parameters.AddWithValue("$TailNumber", flight.TailNumber);
            cmd.Parameters.AddWithValue("$Origin", flight.Origin);
            cmd.Parameters.AddWithValue("$Destination", flight.Destination);
            cmd.Parameters.AddWithValue("$DepartureTime", flight.TakeOffTime);
            cmd.Parameters.AddWithValue("$ArrivalTime", flight.ArrivalTime);
            cmd.Parameters.AddWithValue("$LegroomFee", flight.LegroomFee);
            cmd.Parameters.AddWithValue("$DefaultPrice", flight.DefaultPrice);
            cmd.Parameters.AddWithValue("$MealFee", flight.MealPrice);
            cmd.Parameters.AddWithValue("$ChosenSeatFee", flight.ChosenSeatFee);
            cmd.Parameters.AddWithValue("$ExtraLuggageFee", flight.ExtraLuggageFee);
            
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating flight in database: {ex.Message}");
            return false;
        }
    }

    public List<FlightModel> GetAll()
    {
        var flights = new List<FlightModel>();

        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT 
            FlightId, TailNumber, Origin, Destination, 
            Departuretime, ArrivalTime, 
            LegroomFee, DefaultPrice, MealFee, ChosenSeatFee, ExtraLuggageFee 
        FROM Flight";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            flights.Add(new FlightModel
            {
                FlightId = reader.GetInt32(0),
                TailNumber = reader.GetString(1),
                Origin = reader.GetString(2),
                Destination = reader.GetString(3),
                TakeOffTime = reader.GetDateTime(4),
                ArrivalTime = reader.GetDateTime(5),
                LegroomFee = reader.GetInt32(6),
                DefaultPrice = reader.GetInt32(7),
                MealPrice = reader.GetInt32(8),
                ChosenSeatFee = reader.GetInt32(9),
                ExtraLuggageFee = reader.GetInt32(10)
            });
        }

        return flights;
    }
}