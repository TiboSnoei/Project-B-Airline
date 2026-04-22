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

    public bool EditFlight(FlightModel flight)
    {
        // TODO: edit not only fees, but times aswell
        // TODO: add validation
        return _flightAccess.Update(flight);

    }

    public List<FlightModel> GetAll()
    {
        return _flightAccess.GetAll();
    }
}