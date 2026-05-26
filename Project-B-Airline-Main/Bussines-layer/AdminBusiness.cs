public class Admin
{
    //needs a refactor and should extend the account class
    public void admin_menu()
    {
        FlightPresentation flight = new FlightPresentation();

        string[] options = { "Create Flight", "List Flights", "Exit" };
        string header = "Flight Manager";

        switch (Menu.VerticalMenu(options, header))
        {
            case "Create Flight":
                flight.CreateFlight();
                break;
            case "List Flights":
                flight.ListFlights();
                break;
            case "Exit":
                break;
        }
    }
}