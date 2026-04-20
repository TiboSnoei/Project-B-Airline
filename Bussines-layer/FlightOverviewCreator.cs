using System.ComponentModel.Design;

public class FlightOverviewCreator
{
    // Fields --------------------------------------------------------------------------------------------------------
    public string Destinationselected { get; set; }
    public bool Returnflightselected { get; set; }
    public DateTime Departuredate { get; set; }
    public DateTime Returndate { get; set; }
    // Constructor ---------------------------------------------------------------------------------------------------
    public FlightOverviewCreator(string destinationselected, bool returnflightselected, DateTime departuredate, DateTime returndate)
    {
        Destinationselected = destinationselected;
        Returnflightselected = returnflightselected;
        Departuredate = departuredate;
        Returndate = returndate;
    }

    public FlightOverviewCreator(string destinationselected, bool returnflightselected, DateTime departuredate)
    {
        Destinationselected = destinationselected;
        Returnflightselected = returnflightselected;
        Departuredate = departuredate;
    }

    public List<FlightModel> GetFlightOverview(SearchOverviewModel search)
    {
        SearchoverviewAcces searchoverviewacces = new SearchoverviewAcces();
        List<FlightModel> filteredflights = new List<FlightModel>(searchoverviewacces.SearchFlights(search));
        return filteredflights;
    }

    public void GenerateFlightOverview()
    {
        List<FlightModel> filteredflights = new List<FlightModel>();

        if (!Returnflightselected)
        {
            SearchOverviewModel search = new SearchOverviewModel
            {
                Departuredate = Departuredate, Destinationselected = Destinationselected, Returnflightselected = Returnflightselected
            };
            filteredflights = GetFlightOverview(search);
        }

        else if (Returnflightselected)
        {
            SearchOverviewModel search = new SearchOverviewModel
            {
                Departuredate = Departuredate, Destinationselected = Destinationselected, Returnflightselected = Returnflightselected, Returndate = Returndate
            };
            filteredflights = GetFlightOverview(search);
        }

        try
        {
            // List<string> stringfilteredflights = new List<string>();
            string spacingformat = "{0,-12} {1,-15} {2,-20} {3,-20} {4,-25} {5,-25} {6,-10}";
            Console.WriteLine("========== Available Flights ==========\n");
            Console.WriteLine("Use ↑/↓ and Enter to select:\n");
            Console.WriteLine(spacingformat, "FlightId", "TailNumber", "Destination", "Departure", "TakeOffTime", "ArrivalTime", "Price");
            Console.WriteLine("");
            foreach(FlightModel filght in filteredflights)
            {
                // string stringflight = $"{filght.FlightId}, {filght.TailNumber}, {filght.Destination}, {filght.Origin}, {filght.TakeOffTime}, {filght.ArrivalTime}, {filght.DefaultPrice}";
                // stringfilteredflights.Add(stringflight);
                Console.WriteLine(spacingformat, filght.FlightId, filght.TailNumber, filght.Destination, filght.Origin, filght.TakeOffTime, filght.ArrivalTime, filght.DefaultPrice);
            }

                // string[] arrayfilteredflight = stringfilteredflights.ToArray();
                // Menu menu = new Menu();
                // menu.verticalmenuwithdata(filteredflights);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error printing flight overview: {ex.Message}");
        }
    }
}