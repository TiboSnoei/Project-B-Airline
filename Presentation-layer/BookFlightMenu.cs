public class BookFlightMenu
{
    private readonly Database _db;
    private readonly int _currentUserId;

    public BookFlightMenu(int currentUserId)
    {
        _currentUserId = currentUserId;
    }

    public void ShowFlightDetails(Flight flight, Plane plane)
    {
        Console.WriteLine("Flight Details:");
        Console.WriteLine($"Flight: {flight.FlightID}");
        Console.WriteLine($"Departure: {flight.Origin} at {flight.DepartureTime}");
        Console.WriteLine($"Arrival: {flight.Destination} at {flight.ArrivalTime}");
        Console.WriteLine($"Plane: {plane.Model}");
        Console.WriteLine($"Price: ${flight.Price}");
        Console.WriteLine();
    }

    public void OptionsMenu(Flight flight) //TODO: moet nog te navigeren zijn met arrows
    {
        Console.WriteLine("    [R]eturn to search results.");
        Console.WriteLine("    [C]onfirm booking.");
        Console.WriteLine("    [E]xit.");

        Console.Write("Please select an option: ");
        string option = Console.ReadLine().ToUpper();

        switch (option)
        {
            case "R":
                ReturnToSearch();
                break;
            case "C":
                ConfirmBooking(flight);
                break;
            case "E":
                ExitToMainMenu();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                OptionsMenu(flight);
                break;
        }
    }

    private void ReturnToSearch()
    {
        Console.WriteLine("Returning to search results.");
        // TODO: implementeren wanneer het klaar is.
    }

    private void ConfirmBooking(Flight flight)
    {
    var builder = new BookingBuilder();
    BookingModel booking = builder.Build(_currentUserId, flight);

    var access = new BookingAccess();
    access.InsertBooking(booking);

    Console.WriteLine("Booking saved!");
    }

    private void ExitToMainMenu()
    {
        Console.WriteLine("Returning to main menu.");
        // TODO: impldementeren wanneer dit klaar is.
    }
}
