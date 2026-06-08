using Microsoft.Data.Sqlite;

public class GainLoyaltyPointsAcces
{
    public AccountModel LoggedInUser = Session.LoggedInUser;
    public int LoyaltyPointsToAdd { get; set; }
    private readonly string _connectionString;

    public GainLoyaltyPointsAcces(int loyaltyPointsToAdd)
    {
        LoyaltyPointsToAdd = loyaltyPointsToAdd;
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

    public bool WriteLoyaltyPoints()
    {
        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            UPDATE Users
            SET
                LoyaltyPoints = $LoyaltyPoints
            WHERE UserID = $UserID";

            cmd.Parameters.AddWithValue("$LoyaltyPoints", LoggedInUser.LoyaltyPoints + LoyaltyPointsToAdd);
            cmd.Parameters.AddWithValue("$UserID", LoggedInUser.UserID);
            int rowsAffected = cmd.ExecuteNonQuery();

            LoggedInUser.LoyaltyPoints = LoggedInUser.LoyaltyPoints + LoyaltyPointsToAdd;

            return rowsAffected > 0;
        }

        catch(Exception ex)
        {
            Console.WriteLine($"Error While Adding/Writing Loyalty Points To Account (GainLoyaltyPointsAcces): {ex}");
            Console.ReadKey();
            return false;
        }
    }
}