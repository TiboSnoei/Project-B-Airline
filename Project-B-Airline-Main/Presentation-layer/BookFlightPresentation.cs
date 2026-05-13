// TODO: Validation, laat een error message zien als het niet lukt en laat de console.writeline dan niet zien.
// TODO: assign seat (Zie BookFlightBusiness.cs)

public static class BookFlight
{
    public static void Bookflight(FlightModel chosenflight)
    {
        string[] options = { "Yes", "No" };
        Menu menu = new Menu();
        string context = $"You have selected flight {chosenflight.FlightId} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
        string choice = menu.VerticalMenu(options, "Confirm Booking", context);

        // TODO: checken of de user al ingelogd is, zo ja, skip
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
                            Console.WriteLine("login");

                            afterLogin(chosenflight);
                            break;

                        case "Create Account":
                            AccountPresentation register = new AccountPresentation();
                            register.Register();

                            Console.Clear();
                            Console.WriteLine("register");

                            afterRegister(chosenflight);
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
        else
        {
            switch (choice)
            {
                case "Yes":
                    Console.Clear();
                    Console.WriteLine("Booking confirmed! Returning to flight overview.");
                    afterLogin(chosenflight);
                    Console.ReadKey();
                    break;

                case "No":
                    Console.WriteLine("Booking cancelled. Returning to flight overview.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public static void afterRegister(FlightModel chosenflight)
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

    public static void afterLogin(FlightModel chosenflight)
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
}
