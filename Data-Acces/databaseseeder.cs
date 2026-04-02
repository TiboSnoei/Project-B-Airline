using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;


public class DatabaseSeeder
{
    //fields || unnececery
    public const string DatabaseLoc = "data/airline.db";
    public string cs = $"Data Source={DatabaseLoc}";
    //constructor
    public void databaseSeeder()
    {

        string sql1 = "INSERT INTO Plane VALUES ('HR101', 250, 'Boeing-737'), ('HR102', 250, 'Boeing-737'), ('HR103', 345, 'Airbus 330'), ('HR104', 345, 'Airbus 330')";

        string sql2 = "INSERT INTO Users VALUES ('1a', 'Customer', 'Bob', 'Marten', 'BobMarten_120805', 'BobMarten@gmail.com', '2026-05-01 00:00:00', 0676543566, 0)";

        string sql3 = "INSERT INTO Flight VALUES ('1a', 'HR101', 'Berlin', 'Rotterdam', '2026-05-01 12:45:00', '2026/05/01 11:15:00', 100.00, 100.00, 100.00, 100.00, 100.00)";

        string sql4 = "INSERT INTO CustomerFlight VALUES('1a', '1a', '12C', False, False, False, False)";

        using var connection = DatabaseConnector.Open(cs);

        List<string> sqllist = new List<string> {sql1, sql2, sql3, sql4};

        foreach (string sql in sqllist)
        {
            using SqliteCommand command = new(sql, connection);
        }
    }
}