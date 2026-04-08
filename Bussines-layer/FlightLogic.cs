using System;

public class FlightLogic
{
    private readonly FlightAccess _flightAccess = new FlightAccess();

    public bool CreateFlight(FlightModel flight)
    {
        // Validation of arival, take off, destination, origin and plane ID
        if (flight.ArrivalTime <= flight.TakeOffTime)
        {
            Console.WriteLine("Arrival time must be after takeofflight.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(flight.Destination))
        {
            Console.WriteLine("Destination cannot be empty.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(flight.Origin))
        {
            Console.WriteLine("Origin cannot be empty.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(flight.TailNumber))
        {
            // TODO: dit moet nog veranderd worden naar een zoekopdracht naar het juiste vliegtuig
            Console.WriteLine("Invalid tail number.");
            return false;
        }

        //TODO: add validation for fees (no negatives, not null)

        return _flightAccess.Write(flight);
    }

    public void ListFlights()
    {
        //This shouldnt be used. Tibo should make the indexed and filtered flight list.
        var flights = _flightAccess.GetAll();

        Console.WriteLine("=== All Flights ===");
        foreach (var flight in flights)
        {
            Console.WriteLine($"FlightID: {flight.FlightId}, TailNumber: {flight.TailNumber}, Origin: {flight.Origin}, Destination: {flight.Destination}, TakeOff: {flight.TakeOffTime}, Arrival: {flight.ArrivalTime}");
        }
        Console.WriteLine("===================");
    }
}