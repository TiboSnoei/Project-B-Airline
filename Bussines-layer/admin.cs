public class Admin
{
    public void admin_menu()
    {
        FlightPresentation flight = new FlightPresentation();
        Menu menu = new Menu();

        string[] options = { "Create Flight", "List Flights", "Exit" };
        string header = "Flight Manager";

        switch (menu.VerticalMenu(options, header))
        {
            case "Create Flight":
                flight.CreateFlight();
                break;
            case "List Flights":
                flight._flightLogic.ListFlights();
                Console.ReadKey();
                break;
            case "Exit":
                break;
        }
    }
}