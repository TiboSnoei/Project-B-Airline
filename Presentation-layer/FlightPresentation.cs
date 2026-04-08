using System;

public class FlightPresentation
{
    public readonly FlightLogic _flightLogic = new FlightLogic();

    public void CreateFlight()
    {
        Console.Clear();
        Console.WriteLine("=== Create Flight ===\n");

        string tailNumber = GetValidString("Plane tail number: ");
        string destination = GetValidString("Destination: ");
        string origin = GetValidString("Origin: ");
        DateTime takeOff = GetValidDateTime("Takeoff (yyyy-MM-dd HH:mm): ");
        DateTime arrival = GetValidDateTime("Arrival (yyyy-MM-dd HH:mm): ");
        int defaultPrice = GetValidInt("default ticket price: ");
        int legroomFee = GetValidInt("Legroom fee: ");
        int mealPrice = GetValidInt("Meal price: ");
        int chosenSeatFee = GetValidInt("Chosen seat fee: ");
        int extraLuggageFee = GetValidInt("Extra luggage fee: ");



        var flight = new FlightModel
        {
            TailNumber = tailNumber,
            Destination = destination,
            Origin = origin,
            TakeOffTime = takeOff,
            ArrivalTime = arrival,
            DefaultPrice = defaultPrice,
            LegroomFee = legroomFee,
            MealPrice = mealPrice,
            ChosenSeatFee = chosenSeatFee,
            ExtraLuggageFee = extraLuggageFee
        };

        bool success = _flightLogic.CreateFlight(flight);

        if (success)
            Console.WriteLine("\nFlight created successfully!");
        else
            Console.WriteLine("\nFailed to create flight.");

        Console.ReadKey();
    }

    private int GetValidInt(string message)
    {
        //TODO: all validators should be in validation file
        int value;
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Invalid number.");
        }
    }

    private string GetValidString(string message)
    {
        string input;
        while (true)
        {
            Console.Write(message);
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input;

            Console.WriteLine("Cannot be empty.");
        }
    }

    private DateTime GetValidDateTime(string message)
    {
        DateTime value;
        while (true)
        {
            Console.Write(message);
            if (DateTime.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Invalid date.");
        }
    }
}