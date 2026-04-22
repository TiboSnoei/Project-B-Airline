using System;
using System.Security.Cryptography.X509Certificates; //useless?

class Program
{
    static void Main(string[] args)
    {
        //For testing purposes, this will check if there is a database. 
        // If there isn't any in the expected location, it will generate and seed one instead.
        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();

        AccountPresentation presentation = new AccountPresentation();
        AccountModel loggedInUser = null;
        Menu menu = new Menu();

        bool running = true;

        while (running)
        {
            string[] options;
            string header = "Flight Booking System";

            // TODO: refactor to user.mainMenu
            // dit is clunky en kan gewoon in hen eigen class.
            // de enige logica hier zou van een uitgelogde gebruiker moeten zijn imo?
            if (loggedInUser == null)
            {
                options = new string[] { "Search Flights", "Login", "Register", "Exit" };
            }
            else if (loggedInUser.UserType == "Admin")
            {
                options = new string[] { "Flights", "Users", "Booked Flights", "Exit" };
            }
            else
            {
                options = new string[] { "Search Flights", "My Flights", "My Account", "Exit" };
            }

            switch (menu.VerticalMenu(options, header))
            {
                case "Search Flights":
                    FlightOverviewSearchPresentation flightOverviewSearchPresentation = new FlightOverviewSearchPresentation();
                    flightOverviewSearchPresentation.FlightSearchMenu();
                    Console.ReadKey();
                    break;

                case "Login":
                        loggedInUser = presentation.Login();
                    if (loggedInUser != null)
                    {                                
                        Console.WriteLine($"{loggedInUser.UserType}");
                    }
                    else
                    {
                        Console.WriteLine("Login failed.");
                        Console.ReadKey();
                    }
                    break;

                case "Register":
                    presentation.Register();
                    break;

                case "My Flights":
                    Console.WriteLine("Not implemented.");
                    Console.ReadKey();
                    break;

                case "My Account":
                    Console.WriteLine("Not implemented.");
                    Console.ReadKey();
                    break;

                case "Flights":
                    Admin admin = new Admin();
                    admin.admin_menu();
                    break;

                case "Users":
                    Console.WriteLine("Not implemented.");
                    Console.ReadKey();
                    break;

                case "Booked Flights":
                    Console.WriteLine("Not implemented.");
                    Console.ReadKey();
                    break;

                case "Exit":
                    running = false;
                    break;
                
                default:
                    Console.WriteLine("Somehow you got an non-existing input!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}