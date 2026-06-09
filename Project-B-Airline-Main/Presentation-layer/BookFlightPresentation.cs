// TODO: assign seat: Moet gebeuren wanneer er een map gemaakt is van de vliegtuigen.

public static class BookFlight
{
    public static void Bookflight(FlightModel chosenflight)
    {
        string[] options = { "Yes", "No" };
        Menu menu = new Menu();
        string context = $"You have selected flight {chosenflight.FlightNumber} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
        string choice = menu.VerticalMenu(options, "Confirm Booking", context);

        // Checkt of de user ingelogd is of niet, skipt dit blok als de user al ingelogd is.
        // Als de niet ingelogde user inlogt of een account maakt word daarna de boeking in de database gezet.
        // Als de user ervoor kiest om de boeking te annuleren word deze terug gestuurd naar de flight overview.
        if (Session.LoggedInUser == null)
        {
            switch (choice)
            {
                case "Yes":
                    Console.Clear();

                    string[] options2 = { "Log In", "Create Account", "Cancel Booking" };
                    Menu menu2 = new Menu();
                    string context2 = "\nTo complete your booking, you need to have an account. Please choose one of the options below to proceed.\n";
                    string choice2 = menu2.VerticalMenu(options2, "Do you want to log in or register a new account?", context2);

                    switch (choice2)
                    {
                        case "Log In":
                            AccountPresentation login = new AccountPresentation();
                            login.Login();

                            Console.Clear();

                            BookFlightAccess.EnterIntoDatabase(chosenflight);
                            var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                            gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                            break;

                        case "Create Account":
                            AccountPresentation register = new AccountPresentation();
                            register.Register();

                            Console.Clear();

                            BookFlightAccess.EnterIntoDatabase(chosenflight);
                            gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                            gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                            break;
                        
                        case "Cancel Booking":
                            Console.WriteLine("Booking cancelled. Returning to menu.");
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

        // Als de user al ingelogd is word dit blok uitgevoerd.
        // De user krijgt de optie om de boeking te bevestigen of annuleren en word daarna terug gestuurd naar de flight overview.
        else
        {
            switch (choice)
            {
                case "Yes":
                    Console.Clear();
                    // checking the user's loyalty rank and if it will increase after booking the flight
                    if (Session.LoggedInUser != null)
                    {
                        bool rankIncrease = BookFlightBusiness.CheckRankIncrease(Session.LoggedInUser, chosenflight);
                        if (rankIncrease)
                        {
                            Console.WriteLine("Booking this flight will increase your loyalty rank! Press [enter] to continue.");
                            Console.ReadKey();
                        }
                    }
                    Console.WriteLine("Booking confirmed! Returning to flight overview.");
                    BookFlightAccess.EnterIntoDatabase(chosenflight);
                    var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                    gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                    Console.ReadKey();
                    break;

                case "No":
                    Console.WriteLine("Booking cancelled. Returning to flight overview.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
