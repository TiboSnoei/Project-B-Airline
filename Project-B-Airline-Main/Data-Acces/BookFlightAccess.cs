using Microsoft.Data.Sqlite;

public class BookFlightAccess
{
    private readonly string _connectionString;

    public BookFlightAccess()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

    // Saves the new customerflight in the database.
    // This method is called in the business layer after a user has confirmed a booking.
    public void Write(CustomerFlightModel customerFlight)
    {
        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            INSERT INTO CustomerFlight 
            (UserID, FlightID, Seat, SeatChosen, ExtraLegroom, OnflightMeal, ExtraLuggage)
            VALUES 
            ($UserID, $FlightID, $Seat, $SeatChosen, $ExtraLegroom, $OnflightMeal, $ExtraLuggage)";

            cmd.Parameters.AddWithValue("$UserID", customerFlight.UserID);
            cmd.Parameters.AddWithValue("$FlightID", customerFlight.FlightID);
            cmd.Parameters.AddWithValue("$Seat", customerFlight.Seat);
            cmd.Parameters.AddWithValue("$SeatChosen", customerFlight.SeatChosen);
            cmd.Parameters.AddWithValue("$ExtraLegroom", customerFlight.ExtraLegroom);
            cmd.Parameters.AddWithValue("$OnflightMeal", customerFlight.OnflightMeal);
            cmd.Parameters.AddWithValue("$ExtraLuggage", customerFlight.ExtraLuggage);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                Console.WriteLine("Booking saved to database. Please check your account for details.");
            else
                Console.WriteLine("Failed to save booking to database. Please try again!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing booking to database: {ex.Message}");
        }
    }

// this method is called in the presentation layer after a user has confirmed a booking.
// The method creates a customerflightmodel and then calls the write method to save it in the database.
    public static void EnterIntoDatabase(FlightModel chosenflight)
    {
        try
        {            
            var customerflight = new CustomerFlightModel
            {
                UserID = Session.LoggedInUser.UserID,
                FlightID = chosenflight.FlightId,
                Seat = "18B", // dummy data
                SeatChosen = false, // dummy data
                ExtraLegroom = false, // dummy data
                OnflightMeal = false, // dummy data
                ExtraLuggage = false // dummy data
            };

            BookFlightAccess bookFlightAccess = new BookFlightAccess();
            bookFlightAccess.Write(customerflight);
        }
        catch (Exception Exception)
        {
            Console.WriteLine(Exception.Message);
            throw;
        }
    }
}
