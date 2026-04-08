using System;
using System.Security.Cryptography.X509Certificates;

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

        //TODO: Dit menu moet natuurlijk een menu class worden. Dit is te bulky nog :)
        bool running = true;
        int index = 0;

        while (running)
        {
            string[] options;

            if (loggedInUser == null)
            {
                options = new string[] { "View Flights", "Login", "Register", "Exit" };
            }
            else if (loggedInUser.UserType == "Admin")
            {
                options = new string[] { "Flights", "Users", "Booked Flights", "Exit" };
            }
            else
            {
                options = new string[] { "View Flights", "My flights", "My account", "Exit" };
            }

            Console.Clear();
            Console.WriteLine("=== Flight Booking System ===\n");
            Console.WriteLine("Use ↑/↓ and Enter to select:\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"  {options[i]}");

                if (i == index) Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    index = (index - 1 + options.Length) % options.Length;
                    break;

                case ConsoleKey.DownArrow:
                    index = (index + 1) % options.Length;
                    break;

                case ConsoleKey.Enter:
                    string choice = options[index];

                    switch (choice)
                    {
                        case "View Flights":
                            Console.WriteLine("Not implemented.");
                            Console.ReadKey();
                            break;

                        case "Login":
                            loggedInUser = presentation.Login();
                            Console.WriteLine($"{loggedInUser.UserType}");
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
                    }
                    break;
                    

                case ConsoleKey.Escape:
                    running = false;
                    break;
            }
        }
    }
}