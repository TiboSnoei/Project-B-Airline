using Microsoft.Data.Sqlite;
public class SearchoverviewAcces
{
    private const string DatabaseLoc = "data/airline.db";
    private readonly string cs = $"Data Source={DatabaseLoc}";

    internal List<FlightModel> SearchFlights(SearchoverviewBuisness search)
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

        using var connection = DatabaseConnector.Open(cs);
        using var cmd = connection.CreateCommand();

        DateTime start = date.Date;
        DateTime end = start.AddDays(1);

        cmd.CommandText =
            "SELECT FlightID, TailNumber, Destination, Origin, ArrivalTime, DepartureTime, " +
            "LegroomFee, DefaultPrice, MealFee, ChosenSeatFee, ExtraLuggageFee " +
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
            results.Add(new FlightModel());
        }

        return results;
    }
}