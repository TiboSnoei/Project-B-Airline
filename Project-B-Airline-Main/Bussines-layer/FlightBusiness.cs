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

        if (string.IsNullOrWhiteSpace(flight.FlightNumber))
        {
            Console.WriteLine("Flight number cannot be empty.");
            return false;
        }

        // Check if flightnumber matches the format of two letters followed by 4 digits: LL-NNNN (RO-1234)
        if (flight.FlightNumber.Length != 7 || flight.FlightNumber[2] != '-' ||
            !char.IsLetter(flight.FlightNumber[0]) || !char.IsLetter(flight.FlightNumber[1]) ||
            !char.IsDigit(flight.FlightNumber[3]) || !char.IsDigit(flight.FlightNumber[4]) ||
            !char.IsDigit(flight.FlightNumber[5]) || !char.IsDigit(flight.FlightNumber[6]))
        {
            Console.WriteLine("Flight number must be in the format LL-NNNN (e.g., AB-1234).");
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

    public FlightModel GetFlightById(int FlightID)
    {
        return _flightAccess.GetFlightById(FlightID);
    }
}