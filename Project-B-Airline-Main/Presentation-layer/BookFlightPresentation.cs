// TODO: assign seat: Moet gebeuren wanneer er een map gemaakt is van de vliegtuigen.

public static class BookFlight
{
    public static void Bookflight(FlightModel chosenflight)
    {
        string[] options = { "Yes", "No" };
        Menu menu = new Menu();
        string context = $"You have selected flight {chosenflight.FlightNumber} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
        string choice = menu.VerticalMenu(options, "Confirm Booking", context);

        // Checks if the user is already logged in, if not it gives the user the option to log in or create an account to complete the booking.
        // If the user chooses to cancel, they will be redirected back to the flight overview.
        // If the user chooses to log in or create an account, they will be redirected to the respective menu and after completing the process, 
        // the booking will be confirmed and they will be redirected back to their account page.
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

                            BookFlightBusiness.EnterIntoDatabase(chosenflight);
                            var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                            gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                            break;

                        case "Create Account":
                            AccountPresentation register = new AccountPresentation();
                            register.Register();

                            Console.Clear();

                            BookFlightBusiness.EnterIntoDatabase(chosenflight);
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

        // If the user is already logged in the code jumps to this part, where the user gets the option to
        // confirm or cancel the booking and then gets redirected to the flight overview.
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
                        if (rankIncrease is true)
                        {
                            Console.WriteLine("Booking this flight will increase your loyalty rank! Press [enter] to continue.");
                            Console.ReadKey();
                        }
                    }
                    Console.WriteLine("Booking confirmed! Returning to flight overview.");
                    BookFlightBusiness.EnterIntoDatabase(chosenflight);
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
