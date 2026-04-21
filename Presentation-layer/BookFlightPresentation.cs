public class BookFlight
{
    public BookFlight(FlightModel chosenflight)
    {
        // TODO: Console.Clear in menuhandler moet verwijderd worden en moet hier lokaal gebeuren.
        
        Console.WriteLine($"You have selected flight {chosenflight.FlightId} from {chosenflight.Origin} to {chosenflight.Destination}.");
        Console.WriteLine($"The price for this flight is {chosenflight.DefaultPrice}."); //TODO: price laten zien + prijs van de extras
        Console.WriteLine("Do you want to book this flight?");

        string[] options = { "Yes", "No" };
        Menu menu = new Menu();
        string choice = menu.VerticalMenu(options, "Confirm Booking");

        switch (choice)
        {
            case "Yes":
                // TODO: assign seat (Zie BookFlightBusiness.cs)
                Console.WriteLine("Booking confirmed! Log in to or create your account to check your booking details.");

                var customerflight = new CustomerFlightModel
                {
                    UserID = 1, // TODO: ID halen van ingelogde user
                    FlightID = chosenflight.FlightId,
                    Seat = "18B", // dummy data
                    SeatChosen = false, // dummy data
                    ExtraLegroom = false, // dummy data
                    OnflightMeal = false, // dummy data
                    ExtraLuggage = false // dummy data
                };

                // Moet naar de access layer om naar de database gestuurd te worden
                BookFlightAccess bookFlightAccess = new BookFlightAccess();
                bookFlightAccess.Write(customerflight);

                break;

            case "No":
                Console.WriteLine("Booking cancelled. Returning to flight overview.");
                FlightOverviewSearchPresentation flightOverviewSearchPresentation = new FlightOverviewSearchPresentation();
                flightOverviewSearchPresentation.FlightSearchMenu();
                Console.ReadKey();
                break;
        }
    }
}
