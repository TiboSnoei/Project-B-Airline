public class BookFlight
{
    public BookFlight(FlightModel chosenflight)
    {
        // vraag user of ze de gevonden booking vast willen zetten, als user confirmt word de boeking in de database gezet.
        // de user moet een stoel toegewezen krijgen en de prijs moet zichtbaar zijn.
        //als de user de booking niet confirmt worden ze teruggestuurd naar het overzicht.

        // method: show_price -- business layer? (BookFlightBusiness) TODO: Zie BookFlightBusiness.cs
        // method: assign_seat -- business layer (BookflightBusiness) TODO: Zie BookFlightBusiness.cs
        // method: save_booking -- data access layer (BookFlightAccess)
        // method: show_flight_details -- presentation layer (deze file)
        // maak customer flight aan, flight + user

        Console.WriteLine($"You have selected flight {chosenflight.FlightId} from {chosenflight.Origin} to {chosenflight.Destination}.");
        Console.WriteLine($"The price for this flight is {chosenflight.DefaultPrice}.");
        Console.WriteLine("Do you want to book this flight?");
        // gebruik menu handler
        string[] options = { "Yes", "No" };
        Menu menu = new Menu();
        string choice = menu.VerticalMenu(options, "Confirm Booking");

        switch (choice)
        {
            case "Yes":
                // assign seat
                // show price
                // save booking
                Console.WriteLine("Booking confirmed! Log in to or create your account to check your booking details.");
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