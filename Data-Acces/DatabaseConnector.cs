using Microsoft.Data.Sqlite;

public static class DatabaseConnector
{
    public static SqliteConnection Open(string cs)
    {
        var connection = new SqliteConnection(cs);
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = "PRAGMA foreign_keys = ON;";
        cmd.ExecuteNonQuery();

        return connection;
    }
}