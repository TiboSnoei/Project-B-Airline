using System;
using System.IO;
using Microsoft.Data.Sqlite;
internal static class Creatdatabase
{
    static int Main(string[] args)
    {
        var dbPath = Path.Combine("data", "airline.db");
        var schemaPath = Path.Combine("db", "schema.sqlite.sql");

        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"ERROR: Could not find schema file at '{schemaPath}'.");
            return 1;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = dbPath,
            Mode = SqliteOpenMode.ReadWriteCreate
        }.ToString();

        try
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            using (var pragma = conn.CreateCommand())
            {
                pragma.CommandText = "PRAGMA foreign_keys = ON;";
                pragma.ExecuteNonQuery();
            }

            var sql = File.ReadAllText(schemaPath);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine($"OK: SQLite database initialized at '{dbPath}'.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("ERROR: Failed to initialize SQLite database.");
            Console.Error.WriteLine(ex);
            return 1;
        }
    }
}