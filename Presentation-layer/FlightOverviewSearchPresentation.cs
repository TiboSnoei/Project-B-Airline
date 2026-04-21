public class FlightOverviewSearchPresentation
{
    public void FlightSearchMenu()
    {
        string destinationselected;
        DateTime departuredate;
        bool returnflightselected;
        DateTime returndate;

        // TODO: imploment search menu
        Console.Clear();
        Console.WriteLine("===Search Flights===");
        destinationselected = GetValidString("Destination: ");
        departuredate = GetValidDate("Flight Date (yyyy-mm-dd): ");
        string[] options = {"Yes", "No"};
        Menu menu = new Menu();
        string returnflightawnser = menu.VerticalMenu(options, "Do you want a return flight");
        // string returnflightawnser = GetValidString("Do you want a return flight (yes/no)");
        if (returnflightawnser == "Yes") 
        {
            returnflightselected = true;
            returndate = GetValidDate("Return Flight Date (yyyy-mm-dd)");

            FlightOverviewCreator flightOverviewCreator = new FlightOverviewCreator(destinationselected, returnflightselected, departuredate, returndate);
            flightOverviewCreator.GenerateFlightOverview();
        }

        else if (returnflightawnser == "No") 
        {
            returnflightselected = false;

            FlightOverviewCreator flightOverviewCreator = new FlightOverviewCreator(destinationselected, returnflightselected, departuredate);
            flightOverviewCreator.GenerateFlightOverview();
        }

        else;
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

    private DateTime GetValidDate(string message)
    {
        DateTime value;
        while (true)
        {
            Console.Write(message);
            string date = Console.ReadLine();
            date = $"{date} 00:00:00";
            if (DateTime.TryParse(date, out value))
                return value;

            Console.WriteLine("Invalid date.");
        }
    }
}