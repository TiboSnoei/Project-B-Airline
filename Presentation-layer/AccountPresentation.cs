using System;

public class AccountPresentation
{
    private AccountLogic _accountLogic = new AccountLogic();

    public void Run()
    {
        bool running = true;
        string[] options = { "Login", "Register", "Exit" };
        int index = 0;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Account System ===\n");
            Console.WriteLine("Use ↑/↓ and Enter to select:\n");

            for (int i = 0; i < options.Length; i++)
            {
                bool isSelected = i == index;

                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"  {options[i]}");

                if (isSelected)
                    Console.ResetColor();
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
                    switch (index)
                    {
                        case 0:
                            Login();
                            break;

                        case 1:
                            Register();
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

    private void Register()
    {
        Console.Clear();
        Console.WriteLine("=== Register ===\n");

        Console.Write("First name: ");
        string firstName = Console.ReadLine();

        Console.Write("Last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        AccountModel newAccount = new AccountModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        bool success = _accountLogic.CreateAccount(newAccount);

        if (success)
            Console.WriteLine("\nRegistration successful!");
        else
            Console.WriteLine("\nRegistration failed (invalid data or user already exists).");

        Console.ReadKey();
    }

    private void Login()
    {
        Console.Clear();
        Console.WriteLine("=== Login ===\n");

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        AccountModel account = _accountLogic.CheckLogin(email, password);

        if (account != null)
        {
            Console.WriteLine($"\nWelcome {account.FirstName} {account.LastName}!");
            
        }
        else
        {
            Console.WriteLine("\nInvalid email or password.");
        }

        Console.ReadKey();
    }
}