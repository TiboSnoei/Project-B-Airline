public class FlightOverviewSearchPresentation
{
    public void SearchFlight()
    {
        // TODO: imploment search menu

        string destinationselected = "";
        bool returnflightselected = false;
        DateTime departuredate = DateTime.Parse("2001-09-11 08:46:00");

        SearchoverviewBuisness searchoverviewbuisness = new SearchoverviewBuisness(destinationselected, returnflightselected, departuredate);
    }

    public List<string> CreateFlightOverview()
    {
        List<FlightModel> SelectedFlights = new List<FlightModel>[SearchoverviewAcces.SearchFlights()];
    }
}