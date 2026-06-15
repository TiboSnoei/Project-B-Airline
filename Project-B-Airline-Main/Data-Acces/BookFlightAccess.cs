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

    // this method updates the loyalty rank of the user after they have gained loyalty points.
    // if the user has gained enough loyalty points to increase their rank, this method will update their rank in the database.
    // from < 1000 to 1000 or above: '-' to 'Bronze'
    // from < 2500 to 2500 or above: 'Bronze' to 'Silver'
    // from < 5000 to 5000 or above: 'Silver' to 'Gold'
    // from < 10000 to 10000 or above: 'Gold' to 'Platinum'
    public void UpdateLoyaltyRank(AccountModel LoggedInUser, FlightModel chosenFlight)
    {
        var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenFlight.DefaultPrice);
        int pointsAfter = LoggedInUser.LoyaltyPoints + gainLoyaltyPointsBusiness.CalculateLoyaltyPoints();

        string newRank = BookFlightBusiness.GetRank(pointsAfter);

        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            UPDATE Users
            SET
                RankName = $RankName
            WHERE UserID = $UserID";

            cmd.Parameters.AddWithValue("$RankName", newRank);
            cmd.Parameters.AddWithValue("$UserID", LoggedInUser.UserID);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"Congratulations! Your loyalty rank has been updated to {newRank}.");
            else
                Console.WriteLine("Failed to update loyalty rank. Please try again!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating loyalty rank in database: {ex.Message}");
        }
    }
}
