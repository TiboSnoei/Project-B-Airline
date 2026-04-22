using Microsoft.Data.Sqlite;
public class SearchoverviewAcces
{
    private readonly string _connectionString;
    public SearchoverviewAcces()
    {
        string dbPath = "data/airline.db";
        _connectionString = $"Data Source={dbPath}";
    }


    internal List<FlightModel> SearchFlights(SearchOverviewModel search)
    {
        var outbound = ExecuteFlightSearch(
            destination: search.Destinationselected,
            origin: "Rotterdam",
            date: search.Departuredate);

        if (!search.Returnflightselected)
        {
            return outbound;
        }

        var inbound = ExecuteFlightSearch(
            destination: "Rotterdam",
            origin: search.Destinationselected,
            date: search.Returndate);

        outbound.AddRange(inbound);
        return outbound;
    }

    private List<FlightModel> ExecuteFlightSearch(string destination, string origin, DateTime date)
    {
        var results = new List<FlightModel>();

        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();

            DateTime start = date.Date;
            DateTime end = start.AddDays(1);

            // TODO: imploment dapper
            cmd.CommandText =
                "SELECT FlightID, TailNumber, Destination, Origin, ArrivalTime, DepartureTime, DefaultPrice " +
                "FROM Flight " +
                "WHERE Destination = @Destination " +
                "AND Origin = @Origin " +
                "AND DepartureTime >= @Start " +
                "AND DepartureTime < @End;";

            cmd.Parameters.AddWithValue("@Destination", destination);
            cmd.Parameters.AddWithValue("@Origin", origin);
            cmd.Parameters.AddWithValue("@Start", start.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@End", end.ToString("yyyy-MM-dd HH:mm:ss"));

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                results.Add(new FlightModel
                {
                    FlightId = reader.GetInt32(0),
                    TailNumber = reader.GetString(1),
                    Origin = reader.GetString(2),
                    Destination = reader.GetString(3),
                    TakeOffTime = reader.GetDateTime(4),
                    ArrivalTime = reader.GetDateTime(5),
                    DefaultPrice = reader.GetInt32(6)
                });
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error searching in flights {ex.Message}");
            return null!;
        }
        return results;
    }
}