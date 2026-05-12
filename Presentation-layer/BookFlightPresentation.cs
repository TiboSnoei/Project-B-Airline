// TODO: assign seat (Zie BookFlightBusiness.cs)

public static class BookFlight
{
    public static void Bookflight(FlightModel chosenflight)
    {
        string[] options = { "Yes", "No" };
        Menu menu = new Menu();
        string context = $"You have selected flight {chosenflight.FlightId} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
        string choice = menu.VerticalMenu(options, "Confirm Booking", context);

        // Checkt of de user ingelogd is of niet, skipt dit blok als de user al ingelogd is.
        if (Session.LoggedInUser == null)
        {
            switch (choice)
            {
                case "Yes":
                    Console.Clear();

                    string[] options2 = { "Log In", "Create Account" };
                    Menu menu2 = new Menu();
                    string context2 = "To complete your booking, you need to have an account. Please choose one of the options below to proceed.\n";
                    string choice2 = menu2.VerticalMenu(options2, "Do you want to log in or register a new account?", context2);

                    switch (choice2)
                    {
                        case "Log In":
                            AccountPresentation login = new AccountPresentation();
                            login.Login();

                            Console.Clear();

                            EnterIntoDatabase(chosenflight);
                            break;

                        case "Create Account":
                            AccountPresentation register = new AccountPresentation();
                            register.Register();

                            Console.Clear();

                            EnterIntoDatabase(chosenflight);
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
        // Als de user al ingelogd is word dit blok uitgevoerd.
        else
        {
            switch (choice)
            {
                case "Yes":
                    Console.Clear();
                    Console.WriteLine("Booking confirmed! Returning to flight overview.");
                    EnterIntoDatabase(chosenflight);
                    Console.ReadKey();
                    break;

                case "No":
                    Console.WriteLine("Booking cancelled. Returning to flight overview.");
                    Console.ReadKey();
                    break;
            }
        }
    }

// EnterIntoDatabase maakt een nieuw CustomerFlightModel aan om de gegevens op te slaan en schrijft ze met de Write() method naar de database.
    public static void EnterIntoDatabase(FlightModel chosenflight)
    {
        try
        {            
            var customerflight = new CustomerFlightModel
            {
                UserID = Session.LoggedInUser.UserID,
                FlightID = chosenflight.FlightId,
                Seat = "18B", // dummy data
                SeatChosen = false, // dummy data
                ExtraLegroom = false, // dummy data
                OnflightMeal = false, // dummy data
                ExtraLuggage = false // dummy data
            };

            BookFlightAccess bookFlightAccess = new BookFlightAccess();
            bookFlightAccess.Write(customerflight);
        }
        catch (Exception Exception)
        {
            Console.WriteLine(Exception.Message);
            throw;
        }
    }
}
