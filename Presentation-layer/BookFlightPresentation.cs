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
                // TODO: Validation, laat een error message zien als het niet lukt en laat de console.writeline dan niet zien.
                // TODO: assign seat (Zie BookFlightBusiness.cs)

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

                Console.Clear();
                Console.WriteLine("You're almost done! Please log in or create an account to complete your booking.");

                string[] options2 = { "Log In", "Create Account" };
                Menu menu2 = new Menu();
                string choice2 = menu2.VerticalMenu(options2, "Do you want to log in or register a new account?");

                switch (choice2)
                {
                    case "Log In":
                        AccountPresentation login = new AccountPresentation();
                        login.Login();
                        Console.ReadKey();
                        break;

                    case "Create Account":
                        AccountPresentation register = new AccountPresentation();
                        register.Register();
                        Console.ReadKey();
                        break;
                }
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