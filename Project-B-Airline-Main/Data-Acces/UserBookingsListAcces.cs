using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;
public class UserBookingsListAcces
{
    AccountModel LoggedInUser = Session.LoggedInUser;
    private readonly string _connectionString;

    public UserBookingsListAcces(AccountModel loggedInUser)
    {
        LoggedInUser = loggedInUser;

        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }


    // SearchBookings opens a sql connection with the database
    // and excecutes a search for customerflight tables where the UserId match the UserID of LoggedInUser
    // Than it puts all the valid customerflight tables in a list with customerflightmodels and returns the list
    public List<CustomerFlightModel> SearchBookings()
    {
        var results = new List<CustomerFlightModel>();

        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "SELECT UserID, FlightID, Seat, SeatChosen, ExtraLegroom, OnflightMeal, ExtraLuggage FROM CustomerFlight WHERE UserID = @UserID ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                results.Add(new CustomerFlightModel
                {
                    UserID = reader.GetInt32(0),
                    FlightID = reader.GetInt32(1),
                    Seat = reader.GetString(2), // TOTO: needs to be cahnged to array at some point.
                    SeatChosen = reader.GetBoolean(3), // need to be removed at some point
                    ExtraLegroom = reader.GetBoolean(4), // need to be removed at some point
                    OnflightMeal = reader.GetBoolean(5), // need to be removed at some point
                    ExtraLuggage = reader.GetBoolean(6) // need to be removed at some point
                });
            }
            return results;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error searching in flights {ex.Message}");
            return new List<CustomerFlightModel>();
        }
    }
}