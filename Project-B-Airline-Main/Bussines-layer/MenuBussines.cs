static class Menu
{
    // Displays a vertical selection menu where the user can navigate using arrow keys.
    // options = array of selectable menu options
    // header = title displayed at the top of the menu
    // returns selected option or "Exit" if Escape is pressed
    public static string VerticalMenu(string[] options, string header)
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
        return "Exit";
    }

    // Displays a vertical selection menu with a description block above the options.
    // options = array of selectable menu options
    // header = title displayed at the top of the menu
    // description = multi-line description text (use \n for line breaks)
    // returns selected option or "Exit" if Escape is pressed
    public static string VerticalMenu(string[] options, string header, string description)
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
        return "Exit";
    }

    // Displays a vertical menu with column-formatted options.
    // options = pre-formatted table rows
    // header = menu title
    // optionsHeader = column header row
    // returns selected index or options.Length if Escape is pressed
    public static int VerticalMenuWithColumns(string[] options, string header, string optionsHeader)
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
                    return options.Length;
            }
        }
        return options.Length;
    }

    public static string SeatMap(SeatModel[,] model, string header, string optionsHeader)
    {
        bool running = true;
        int columnIndex = 0;
        int rowIndex = 0;

        while (running)
        {
            Console.Clear();
            Console.WriteLine($"=== {header} ===\n");
            Console.WriteLine("Use ←/↑/→/↓ and Enter to select:\n");
            Console.WriteLine($"{optionsHeader}");

            // flight.planemodel
            // loop each seat of the plane model
            // Seat[,] model = { {1, 4, 2}, {3, 6, 8} }; // id's of seats
            // example:
            // null , 1, 2, 3, null 4, 5, 6, null
            // 7, 8, 9, 10, null, 11, 12, 13, 14
            // so null is no seat here, the numbers are replaced with seat objects. 
            // a seat has an Id, Class, ExtraLeggroom, SeatNumber
            while (model[columnIndex, rowIndex] == null)
            {
                rowIndex++;
            }
            for (int row = 0; row < model.GetLength(1); row++)
            {
                Console.Write("| ");
                for (int column = 0; column < model.GetLength(0); column++)
                {                    
                    if (column == columnIndex && row == rowIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (model[column, row] != null) 
                    {
                        if (model[column,row].UserID == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"[XXX] ");
                            Console.ForegroundColor = ConsoleColor.White;
                            
                        }
                        else if (model[column, row].SeatNumber.Length > 2)
                        {
                            Console.Write($"[{model[column, row].SeatNumber}] ");
                        }
                        else
                        {
                            Console.Write($"[ {model[column, row].SeatNumber}] ");
                        }
                    }
                    else
                    {
                        Console.Write("      ");
                    }

                    if (column == columnIndex && row == rowIndex) Console.ResetColor();
                }
                Console.Write("|");
                Console.WriteLine();
                
            }

            var key = Console.ReadKey(true).Key;
            do
            {                
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        rowIndex = (rowIndex - 1 + model.GetLength(1)) % model.GetLength(1);
                        break;

                    case ConsoleKey.DownArrow:
                        rowIndex = (rowIndex + 1) % model.GetLength(1);
                        break;

                    case ConsoleKey.LeftArrow:
                        columnIndex = (columnIndex - 1 + model.GetLength(0)) % model.GetLength(0);
                        break;

                    case ConsoleKey.RightArrow:
                        columnIndex = (columnIndex + 1) % model.GetLength(0);
                        break;

                    case ConsoleKey.Enter:
                        string choice = InternalSeatMenu(model[columnIndex, rowIndex]);
                        if (choice == "No")
                        {
                            break;
                        }
                        else
                        {
                            return choice;
                        }

                    case ConsoleKey.Escape:
                        return "Exit";
                    
                }
            }
            while (model[columnIndex, rowIndex] == null);
        }
        return "Exit";
    }

    public static string InternalSeatMenu(SeatModel seat)
    {
        if (seat.UserID == 0)
        {
            Console.WriteLine("This seat has been booked by someone else");
            Console.ReadKey();
            return "No";
        }
        else
        {
            Console.WriteLine($"Seat number: {seat.SeatNumber}");
            Console.WriteLine($"Class: {seat.Class}");
            if (seat.ExtraLegroom)
            {
                Console.WriteLine("This seat includes extra legroom!");
            }
            Console.WriteLine("Price calculation has not been implemented");
            Console.ReadKey();

            bool running = true;
            int index = 0;

            while (running)
            {
                Console.WriteLine("Would you like to book this seat?");
                Console.WriteLine("Use ↑/↓ and Enter to select:\n");

                string[] options = { "Yes", "No" };

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
                        return "No";
                }

                for (int i = 0; i < 5; i++)
                {

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth)); 
                }
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }
        return "Exit";
    }
}