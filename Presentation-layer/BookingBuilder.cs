public class BookingBuilder
{
    public BookingModel BuildBooking(int userId, Flight flight) //TODO: moet navigeerbaar zijn met arrows
    {
        var booking = new BookingModel();
        booking.UserId = userId;
        booking.FlightId = flight.FlightID;

        Console.Write("Choose a seat (e.g. 12A): ");
        booking.Seat = Console.ReadLine();
        booking.SeatChosen = true;

        Console.Write("Extra legroom? (y/n): ");
        booking.ExtraLegroom = Console.ReadLine().ToLower() == "y";

        Console.Write("On-flight meal? (y/n): ");
        booking.OnflightMeal = Console.ReadLine().ToLower() == "y";

        Console.Write("Extra luggage? (y/n): ");
        booking.ExtraLuggage = Console.ReadLine().ToLower() == "y";

        return booking;
    }
}