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
        int defaultPrice = GetValidInt("Default ticket price: ");
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

    public void EditFlight(FlightModel flight)
    {
        Console.WriteLine("=== Edit Flight ===\n");

        Console.WriteLine($"Current default ticket price: {flight.DefaultPrice}");
        int defaultPrice = GetValidInt("Default ticket price: ");
        Console.WriteLine($"Current legroom fee: {flight.LegroomFee}");
        int legroomFee = GetValidInt("Legroom fee: ");
        Console.WriteLine($"Current meal price: {flight.MealPrice}");
        int mealPrice = GetValidInt("Meal price: ");
        Console.WriteLine($"Current chosen seat fee: {flight.ChosenSeatFee}");
        int chosenSeatFee = GetValidInt("Chosen seat fee: ");
        Console.WriteLine($"Current extra luggage fee: {flight.ExtraLuggageFee}");
        int extraLuggageFee = GetValidInt("Extra luggage fee: ");

        flight.DefaultPrice = defaultPrice;
        flight.LegroomFee = legroomFee;
        flight.MealPrice = mealPrice;
        flight.ChosenSeatFee = chosenSeatFee;
        flight.ExtraLuggageFee = extraLuggageFee;

        bool success = _flightLogic.EditFlight(flight);

        if (success)
            Console.WriteLine("\nFlight updated successfully!");
        else
            Console.WriteLine("\nFailed to update flight.");

        Console.ReadKey();
    }

    public void ListFlights()
    {
        //This shouldnt be used. Tibo should make the indexed and filtered flight list.
        var flights = _flightLogic.GetAll();

        Menu menu = new Menu();
        List<string> optionsList = new List<string>();

        string formatting = "|{0,-14}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|{5,-5}|{6,-10}|{7,-10}|{8,-10}|{9,-10}|";
        string optionsHeader = string.Format(formatting, "Tail Number", "Origin", "Destination", "Take off", "Touch down", "Price", "Legroom fee", "Meal price", "Chosen seat fee", "Extra luggage fee");
        string header = "All Flights";

        foreach (var flight in flights)
        {
            string option = string.Format(formatting, flight.TailNumber, flight.Origin, flight.Destination, flight.TakeOffTime, flight.ArrivalTime, flight.DefaultPrice, flight.LegroomFee, flight.MealPrice, flight.ChosenSeatFee, flight.ExtraLuggageFee);
            optionsList.Add(option);
        }

        string[] options = optionsList.ToArray();

        int index = menu.VerticalMenuWithColumns(options, header, optionsHeader);

        if (index != flights.Count)
        {
            EditFlight(flights[index]);
        }
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