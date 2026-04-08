using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

public class BookingAccess
{
    private readonly string _connectionString;

    public BookingAccess()
    {
        var dbPath = Path.Combine("data", "airline.db");
        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = dbPath,
            Mode = SqliteOpenMode.ReadWrite
        }.ToString();
    }

    public void InsertBooking(Booking booking)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string sql = @"
            INSERT INTO CustomerFlight 
            (UserID, FlightID, Seat, SeatChosen, ExtraLegroom, OnflightMeal, ExtraLuggage)
            VALUES (@uid, @fid, @seat, @seatChosen, @extraLegroom, @meal, @luggage);
        ";

        using var command = new SqliteCommand(sql, connection);

        command.Parameters.AddWithValue("@uid", booking.UserId);
        command.Parameters.AddWithValue("@fid", booking.FlightId);
        command.Parameters.AddWithValue("@seat", booking.Seat);
        command.Parameters.AddWithValue("@seatChosen", booking.SeatChosen);
        command.Parameters.AddWithValue("@extraLegroom", booking.ExtraLegroom);
        command.Parameters.AddWithValue("@meal", booking.OnflightMeal);
        command.Parameters.AddWithValue("@luggage", booking.ExtraLuggage);

        command.ExecuteNonQuery();
    }
}