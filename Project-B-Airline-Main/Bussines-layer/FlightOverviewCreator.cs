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
            List<string> stringfilteredflights = new List<string>();

            foreach(FlightModel flight in filteredflights)
            {
                if (flight.Origin == "Rotterdam") {outboundflights.Add(flight);}
                if (flight.Destination == "Rotterdam") {inboundflights.Add(flight);}
            }

            string spacingformat = "{0,-12}|{1,-15}|{2,-20}|{3,-20}|{4,-25}|{5,-25}|{6,-10}";
            string header = "Available Flights";
            string optionsHeader = string.Format(spacingformat, "FlightId", "TailNumber", "Destination", "Departure", "TakeOffTime", "ArrivalTime", "Price");
            
            foreach(FlightModel flight in filteredflights)
            {
                string option = string.Format(spacingformat, flight.FlightId, flight.TailNumber, flight.Destination, flight.Origin, flight.TakeOffTime, flight.ArrivalTime, flight.DefaultPrice);
                stringfilteredflights.Add(option);
            }

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
                string[] arrayfliteredflights = stringfilteredflights.ToArray();

                int chosenflightindex = Menu.VerticalMenuWithColumns(arrayfliteredflights, header, optionsHeader);
                if (chosenflightindex == arrayfliteredflights.Length) {return;}
                else {chosenflight = filteredflights[chosenflightindex];}

                BookFlight.Bookflight(chosenflight, AskForSeat(chosenflight));
            }

            else if (Returnflightselected)
            {
                FlightModel chosenoutboundflight;
                string[] arrayoutboundflight = stringoutboundflights.ToArray();

                int chosenoutboundflightindex = Menu.VerticalMenuWithColumns(arrayoutboundflight, header, optionsHeader);
                if (chosenoutboundflightindex == arrayoutboundflight.Length) {return;}
                else {chosenoutboundflight = outboundflights[chosenoutboundflightindex];}

                FlightModel choseninboundflight;
                string[] arrayinboundflight = stringinboundflights.ToArray();

                int choseninboundflightindex = Menu.VerticalMenuWithColumns(arrayinboundflight, header, optionsHeader);
                if (choseninboundflightindex == arrayinboundflight.Length) {return;}
                else {choseninboundflight = inboundflights[choseninboundflightindex];}

                BookFlight.Bookflight(chosenoutboundflight, AskForSeat(chosenoutboundflight));
                BookFlight.Bookflight(choseninboundflight, AskForSeat(choseninboundflight));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error printing flight overview: {ex.Message}");
        }
    }

    public string AskForSeat(FlightModel flight)
    {
        string[] options = { "Yes", "No" };

        string context = "Reserving a seat will cost a small fee.\n";
        string choice = Menu.VerticalMenu(
            options,
            "Would you like to reserve a seat?",
            context
        );

        string selectedSeat = "Exit";
        try
        {            
            switch (choice)
            {
                case "Yes":

                    // Get seat map from business layer
                    SeatLogic seatLogic = new SeatLogic();
                    SeatModel[,] model = seatLogic.GetSeatMapByFlight(flight);

                    string header = "Select Your Seat";
                    string optionsHeader = "Available seats";

                    selectedSeat = Menu.SeatMap(model, header, optionsHeader);
                    break;

                case "No":
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in seat overview: {ex.Message}");
        }
        return selectedSeat;
    }
}