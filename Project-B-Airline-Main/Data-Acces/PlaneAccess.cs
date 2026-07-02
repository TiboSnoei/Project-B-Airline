using Microsoft.Data.Sqlite;

public class PlaneAccess
{
    private readonly string _connectionString;

    public PlaneAccess()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }

    public PlaneModel GetPlaneByTailNumber(string TailNumber)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT
            TailNumber, SeatCount, Model
            FROM Plane
            WHERE TailNumber = @TailNumber";

        cmd.Parameters.AddWithValue("@TailNumber", TailNumber);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new PlaneModel
            {
                TailNumber = reader.GetString(0),
                SeatCount = reader.GetInt32(1),
                Model = reader.GetString(2)
            };
        }

        return null;
    }
}