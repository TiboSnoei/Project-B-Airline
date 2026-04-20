public class FlightOverviewSearchPresentation
{
    public void FlightSearchMenu()
    {
        // TODO: imploment search menu

        // INFO: 4 lines below are dummy data
        string destinationselected = "Berlin";
        bool returnflightselected = false;
        DateTime departuredate = DateTime.Parse("2026-05-01 12:45:00");
        DateTime returndate = DateTime.Parse("2026-05-01 14:15:00");

        if (!returnflightselected)
        {
            FlightOverviewCreator flightoverviewcreator = new FlightOverviewCreator(destinationselected, returnflightselected, departuredate);
            flightoverviewcreator.GenerateFlightOverview();
        }

        else if (returnflightselected)
        {
            FlightOverviewCreator flightoverviewcreator = new FlightOverviewCreator(destinationselected, returnflightselected, departuredate, returndate);
            flightoverviewcreator.GenerateFlightOverview();
        }
    }
}