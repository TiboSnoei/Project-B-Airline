// TODO: make static.
public class Menu
{
    // Displays a vertical selection menu where the user can navigate using arrow keys.
    // options = array of selectable menu options
    // header = title displayed at the top of the menu
    // returns selected option or "Exit" if Escape is pressed
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

    // Displays a vertical selection menu with a description block above the options.
    // options = array of selectable menu options
    // header = title displayed at the top of the menu
    // description = multi-line description text (use \n for line breaks)
    // returns selected option or "Exit" if Escape is pressed
    public string VerticalMenu(string[] options, string header, string description)
    {
        bool running = true;
        int index = 0;

        while (running)
        {
            Console.Clear();
            Console.WriteLine($"=== {header} ===\n");
            Console.WriteLine(description);
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

    // Displays a vertical menu with column-formatted options.
    // options = pre-formatted table rows
    // header = menu title
    // optionsHeader = column header row
    // returns selected index or options.Length if Escape is pressed
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
        return options.Length;
    }

    // placeholder
    public string PlaneMenu()
    {
        return "Not implemented";
    }
}