using System;

public class FlightLogic
{
    private readonly FlightAccess _flightAccess = new FlightAccess();

    // Creates a new flight after validating input fields
    // returns true if flight was successfully stored in database
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
            // TODO: Check if tailnumber is an existing one.
            Console.WriteLine("Invalid tail number.");
            return false;
        }

        //TODO: add validation for fees (no negatives, not null)

        return _flightAccess.Write(flight);
    }

    // Updates an existing flight in the database
    public bool EditFlight(FlightModel flight)
    {
        // TODO: edit not only fees, but times as well
        // TODO: add validation
        return _flightAccess.Update(flight);
    }

    // Retrieves all flights from the database
    public List<FlightModel> GetAll()
    {
        return _flightAccess.GetAll();
    }
}