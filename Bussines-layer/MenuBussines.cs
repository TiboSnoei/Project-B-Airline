public class Menu
{
    // options = { "option 1", "option 2", "option 3" }; -> an array of all options (as strings) that the users can choose in this menu
    // header = "header" -> The header of this menu
    // returns: The option that has been selected or "Exit" if the escape key is pressed.
    // This means that you should always account for an exit!
    // How do I acces the result?
    // You shall use a switch statement.
    public string VerticalMenu(string[] options, string header)
    {
        bool running = true;
        int index = 0;

        while (running)
        {
            Console.Clear();
            Console.WriteLine($"=== {header} ===\n");
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
                    return options[index];

                case ConsoleKey.Escape:
                    return "Exit";
            }
        }
        return "Exit"; // to prevent build error.
    }

    // see: https://web.archive.org/web/20120803184243/http://www.dylanbeattie.net/cheatsheets/dot_net_string_format_cheat_sheet.pdf
    // string optionsHeader = string.Format("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", "Tail Number", "Origin", "Destination", "Date");
    // string option = string.Format("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", flight.tailNumber, flight.origin, flight.destination, flight.TakeOffTime);
    // options = { option, option2, option3 }; -> gebruik gewoon een loopje om aan te maken, houd het netjes en makkelijk!
    // returns: int -> plaats in array of lengte van de array indien exit

    // voorbeeld om uit te proberen:
    // string optionsHeader = string.Format("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", "Tail Number", "Origin", "Destination", "Date");
    // string option = string.Format("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", flight.TailNumber, flight.Origin, flight.Destination, flight.TakeOffTime);
    // string option2 = string.Format("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", flight.TailNumber, flight.Origin, flight.Destination, flight.TakeOffTime);
    // string option3 = string.Format("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", flight.TailNumber, flight.Origin, flight.Destination, flight.TakeOffTime);
    // string header = "bazinga";
    // string[] options = { option, option2, option3 };
    // menu.VerticalMenuWithColumns(options, header, optionsHeader);
    public int VerticalMenuWithColumns(string[] options, string header, string optionsHeader)
    {
        bool running = true;
        int index = 0;

        while (running)
        {
            Console.Clear();
            Console.WriteLine($"=== {header} ===\n");
            Console.WriteLine("Use ↑/↓ and Enter to select:\n");
            Console.WriteLine($"{optionsHeader}");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"{options[i]}");

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
                    return index;

                case ConsoleKey.Escape:
                    running = false;
                    return options.Length;
            }
        }
        return options.Length; // to prevent build error.
    }

    public string PlaneMenu()
    {
        return "Not implemented";
    }
}