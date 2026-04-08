public class Admin
{
    public void admin_menu()
    {
        bool running = true;
        string[] options = { "Create Flight", "List Flights", "Exit" };
        int index = 0;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Flight System ===\n");
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
                    FlightPresentation flight = new FlightPresentation();
                    switch (index)
                    {
                        case 0:
                            flight.CreateFlight();
                            break;
                        case 1:
                            flight._flightLogic.ListFlights();
                            Console.ReadKey();
                            break;
                        case 2:
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