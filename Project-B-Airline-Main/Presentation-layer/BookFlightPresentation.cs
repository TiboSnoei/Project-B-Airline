// TODO: assign seat: Moet gebeuren wanneer er een map gemaakt is van de vliegtuigen.

using System.Security.Cryptography.X509Certificates;

public static class BookFlight
{
    public static FlightModel Chosenflight { get; set; }

    public static void Bookflight(FlightModel chosenflight)
    {
        Chosenflight = chosenflight;

        // Checks if the user is already logged in, if not it gives the user the option to log in or create an account to complete the booking.
        // If the user chooses to cancel, they will be redirected back to the flight overview.
        // If the user chooses to log in or create an account, they will be redirected to the respective menu and after completing the process, 
        // the booking will be confirmed and they will be redirected back to their account page.
        if (Session.LoggedInUser == null)
        {
            string[] options = { "Log In", "Create Account", "Cancel Booking" };
            Menu menu = new Menu();
            string context = "\nTo complete your booking, you need to have an account. Please choose one of the options below to proceed.\n";
            string choice = menu.VerticalMenu(options, "Do you want to log in or register a new account?", context);

            switch (choice)
            {
                case "Log In":
                    AccountPresentation login = new AccountPresentation();
                    while (Session.LoggedInUser == null)
                    {
                        login.Login();
                        Console.Clear();
                    }

                    string[] options2 = { "Yes", "No" };
                    Menu menu2 = new Menu();
                    string context2 = $"You have selected flight {chosenflight.FlightNumber} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
                    string choice2 = menu2.VerticalMenu(options2, "Confirm Booking", context2);

                    // Console.WriteLine("Press ENTER to continue with the booking process.");
                    // Console.ReadKey();
                    // Console.Clear();

                    switch (choice2)
                    {
                        case "Yes":
                            Console.Clear();
                            // checking the user's loyalty rank and if it will increase after booking the flight
                            bool rankIncrease = BookFlightBusiness.CheckRankIncrease(Session.LoggedInUser, chosenflight);
                            if (rankIncrease is true)
                            {
                                Console.WriteLine("Booking this flight has increased your loyalty rank!\n");
                                BookFlightBusiness.UpdateLoyaltyRank(Session.LoggedInUser, chosenflight);
                            }
                            Console.WriteLine("Booking confirmed! Press ENTER to return to menu.");
                            BookFlightBusiness.EnterIntoDatabase(chosenflight);
                            var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                            gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                            break;

                        case "No":
                            Console.WriteLine("Booking cancelled. Press ENTER to return to menu.");
                            FlightOverviewSearchPresentation flightOverviewSearchPresentation = new FlightOverviewSearchPresentation();
                            flightOverviewSearchPresentation.FlightSearchMenu();
                            break;
                    }

                    break;

                case "Create Account":
                    AccountPresentation register = new AccountPresentation();
                    while (Session.LoggedInUser == null)
                    {
                        register.Register();
                        Console.Clear();
                    }

                    string[] options3 = { "Yes", "No" };
                    Menu menu3 = new Menu();
                    string context3 = $"You have selected flight {chosenflight.FlightNumber} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
                    string choice3 = menu3.VerticalMenu(options3, "Confirm Booking", context3);

                    // Console.WriteLine("Press ENTER to continue with the booking process.");
                    // Console.ReadKey();
                    // Console.Clear();

                    switch (choice3)
                    {
                        case "Yes":
                            Console.Clear();
                            // checking the user's loyalty rank and if it will increase after booking the flight
                            bool rankIncrease = BookFlightBusiness.CheckRankIncrease(Session.LoggedInUser, chosenflight);
                            if (rankIncrease is true)
                            {
                                Console.WriteLine("Booking this flight has increased your loyalty rank!\n");
                                BookFlightBusiness.UpdateLoyaltyRank(Session.LoggedInUser, chosenflight);
                            }
                            Console.WriteLine("Booking confirmed! Press ENTER to return to menu.");
                            BookFlightBusiness.EnterIntoDatabase(chosenflight);
                            var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                            gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                            break;

                        case "No":
                            Console.WriteLine("Booking cancelled. Press ENTER to return to menu.");
                            FlightOverviewSearchPresentation flightOverviewSearchPresentation = new FlightOverviewSearchPresentation();
                            flightOverviewSearchPresentation.FlightSearchMenu();
                            break;
                    }
                    break;
                
                case "Cancel Booking":
                    Console.WriteLine("Booking cancelled. Returning to menu.");
                    break;
            }
            Console.ReadKey();
        }

        // If the user is already logged in the code jumps to this part, where the user gets the option to
        // confirm or cancel the booking and then gets redirected to the flight overview.
        else
        {
            string[] options2 = { "Yes", "No" };
            Menu menu2 = new Menu();
            string context2 = $"You have selected flight {chosenflight.FlightNumber} from {chosenflight.Origin} to {chosenflight.Destination}.\nThe price for this flight is {chosenflight.DefaultPrice}. Do you want to book this flight?\n";
            string choice2 = menu2.VerticalMenu(options2, "Confirm Booking", context2);

            switch (choice2)
            {
                case "Yes":
                    Console.Clear();
                    // checking the user's loyalty rank and if it will increase after booking the flight
                    if (Session.LoggedInUser != null)
                    {
                        bool rankIncrease = BookFlightBusiness.CheckRankIncrease(Session.LoggedInUser, chosenflight);
                        if (rankIncrease is true)
                        {
                            Console.WriteLine("Booking this flight has increased your loyalty rank!\n");
                            BookFlightBusiness.UpdateLoyaltyRank(Session.LoggedInUser, chosenflight);
                        }
                    }
                    Console.WriteLine("Booking confirmed! Press ENTER to return to menu.");
                    BookFlightBusiness.EnterIntoDatabase(chosenflight);
                    var gainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenflight.DefaultPrice); // TODO: Add Correct Total Price
                    gainLoyaltyPointsBusiness.GiveLoyaltyPoints();
                    break;

                case "No":
                    Console.WriteLine("Booking cancelled. Press ENTER to return to menu.");
                    FlightOverviewSearchPresentation flightOverviewSearchPresentation = new FlightOverviewSearchPresentation();
                    flightOverviewSearchPresentation.FlightSearchMenu();
                    break;
            }
            Console.ReadKey();
        }
    }
}
