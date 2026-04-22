using Microsoft.Data.Sqlite;

public class BookFlightAccess
{
    private readonly string _connectionString;

    public BookFlightAccess()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

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
                Console.WriteLine("Booking saved to database.");
            else
                Console.WriteLine("Failed to save booking to database.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing booking to database: {ex.Message}");
        }
    }
}