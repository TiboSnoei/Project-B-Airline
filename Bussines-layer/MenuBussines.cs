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

    public string VerticalMenuWithColumns(string[] options, string header, string optionsHeader, string spacingFormat)
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
                    running = false;
                    return "Exit";
            }
        }
        return "Exit"; // to prevent build error.
    }

    public string PlaneMenu()
    {
        return "Not implemented";
    }
}