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
            List<FlightModel> outboundflights = new List<FlightModel>();
            List<FlightModel> inboundflights = new List<FlightModel>();
            List<string> stringoutboundflights = new List<string>();
            List<string> stringinboundflights = new List<string>();

            foreach(FlightModel flight in filteredflights)
            {
                if (flight.Origin == "Rotterdam") {outboundflights.Add(flight);}
                if (flight.Destination == "Rotterdam") {inboundflights.Add(flight);}
            }

            string spacingformat = "{0,-12}|{1,-15}|{2,-20}|{3,-20}|{4,-25}|{5,-25}|{6,-10}";
            string header = "Available Flights";
            string optionsHeader = string.Format(spacingformat, "FlightId", "TailNumber", "Destination", "Departure", "TakeOffTime", "ArrivalTime", "Price");
            

            foreach(FlightModel flight in outboundflights)
            {
                string option = string.Format(spacingformat, flight.FlightId, flight.TailNumber, flight.Destination, flight.Origin, flight.TakeOffTime, flight.ArrivalTime, flight.DefaultPrice);
                stringoutboundflights.Add(option);
            }

            foreach(FlightModel flight in inboundflights)
            {
                string option = string.Format(spacingformat, flight.FlightId, flight.TailNumber, flight.Destination, flight.Origin, flight.TakeOffTime, flight.ArrivalTime, flight.DefaultPrice);
                stringinboundflights.Add(option);
            }

            if (!Returnflightselected)
            {
                FlightModel chosenflight;
                string[] arrayoutboundflight = stringoutboundflights.ToArray();
                Menu menu = new Menu();
                int chosenflightindex = menu.VerticalMenuWithColumns(arrayoutboundflight, header, optionsHeader);
                if (chosenflightindex == arrayoutboundflight.Length) {return;}
                else {chosenflight = outboundflights[chosenflightindex];}
                // BookFlight bookflight = new BookFlight(chosenoutboundflight); //????????
            }

            else if (Returnflightselected)
            {
                FlightModel chosenoutboundflight;
                string[] arrayoutboundflight = stringoutboundflights.ToArray();
                Menu outboundmenu = new Menu();
                int chosenoutboundflightindex = outboundmenu.VerticalMenuWithColumns(arrayoutboundflight, header, optionsHeader);
                if (chosenoutboundflightindex == arrayoutboundflight.Length) {return;}
                else {chosenoutboundflight = outboundflights[chosenoutboundflightindex];}

                FlightModel choseninboundflight;
                string[] arrayinboundflight = stringinboundflights.ToArray();
                Menu inboundmenu = new Menu();
                int choseninboundflightindex = inboundmenu.VerticalMenuWithColumns(arrayinboundflight, header, optionsHeader);
                if (choseninboundflightindex == arrayinboundflight.Length) {return;}
                else {choseninboundflight = inboundflights[choseninboundflightindex];}

                BookFlight bookflight = new BookFlight(chosenoutboundflight);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error printing flight overview: {ex.Message}");
        }
    }
}