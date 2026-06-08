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

    public int GetCurrentLoyaltyPoints()
    {
        try
        {
            
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            SELECT
            LoyaltyPoints
            FROM Users
            WHERE UserID = $UserID";

            cmd.Parameters.AddWithValue("$UserID", LoggedInUser.UserID);

            using var reader = cmd.ExecuteReader();

            return reader.GetInt32(0);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error Reading Current Loyalty Points From Database (GainLoyaltyPointsAcces): {ex} ");
            return 0;
        }
    }

    public bool WriteLoyaltyPoints()
    {
        try
        {
            int CurrentLoyaltyPoints = GetCurrentLoyaltyPoints();

            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            UPDATE Users
            SET
                LoyaltyPoints = $LoyaltyPoints
            WHERE UserID = $UserID";

            cmd.Parameters.AddWithValue("LoyaltyPoints", GetCurrentLoyaltyPoints() + LoyaltyPointsToAdd);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        catch(Exception ex)
        {
            Console.WriteLine($"Error While Adding/Writing Loyalty Points To Account (GainLoyaltyPointsAcces): {ex}");
            return false;
        }
    }
}