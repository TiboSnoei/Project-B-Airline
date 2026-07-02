using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

public class AccountPresentation
{
    private AccountLogic _accountLogic = new AccountLogic();

    private static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        while (true)
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }

    public void Register()
    {
        bool run = true;
        AccountLogic accountlogic = new AccountLogic();

        Console.Clear();
        Console.WriteLine("=== Register ===");

        Console.Write("First name: ");
        string? firstName = Console.ReadLine();

        // validate first name
        if (string.IsNullOrEmpty(firstName) || firstName.Length < 2 || !firstName.All(char.IsLetter))
        {
            Console.WriteLine("Invalid first name. It must be at least 2 characters long and contain only letters. Press [Enter] to try again.");
            Console.ReadKey();
            return;
        }

        Console.Write("Last name: ");
        string? lastName = Console.ReadLine();

        // validate last name
        if (string.IsNullOrEmpty(lastName) || lastName.Length < 2 || !lastName.All(char.IsLetter))
        {
            Console.WriteLine("Invalid last name. It must be at least 2 characters long and contain only letters. Press [Enter] to try again.");
            Console.ReadKey();
            return;
        }

        Console.Write("Email: ");
        string? email = Console.ReadLine();

        // validate email
        if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
        {
            Console.WriteLine("Invalid email address. Press [Enter] to try again.");
            Console.ReadKey();
            return;
        }

        Console.Write("Phone number: ");
        string? telNum = Console.ReadLine();

        // validate phone number
        if (string.IsNullOrEmpty(telNum) || telNum.Length < 10 || !telNum.All(char.IsDigit))
        {
            Console.WriteLine("Invalid phone number. It must be at least 10 digits long and contain only numbers. Press [Enter] to try again.");
            Console.ReadKey();
            return;
        }

        string password = "";
        bool success = false;
        while (run)
        {
            Console.Write("Password: ");
            password = ReadPassword();

            Console.Write("Confirm Password: ");
            string confirmationpassword = ReadPassword();

            bool passwordmatch = accountlogic.ConfirmPassword(password, confirmationpassword);
            if (!passwordmatch)
            {
                Console.Clear();
                Console.WriteLine("=== Register ===");
                Console.WriteLine($"First name: {firstName}");
                Console.WriteLine($"Last name: {lastName}");
                Console.WriteLine($"Email: {email}");
                Console.WriteLine($"Phone number: {telNum}");
                Console.WriteLine("Password doesnt match, please try again.");
            }

            else if (passwordmatch)
            {
                AccountModel newAccount = new AccountModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    TelNum = telNum,
                    RankName = "-"
                };

                success = _accountLogic.CreateAccount(newAccount);

                Console.Clear();
                if (success)
                {
                    Console.WriteLine("\nRegistration successful!");
                }

                else
                {
                    Console.WriteLine("\nRegistration failed (invalid data or user already exists).");
                }

                run = false;
            }
        }

        if (success)
        {
            AccountModel account = _accountLogic.CheckLogin(email, password);
            Session.SetUser(account);
            Console.WriteLine($"\nWelcome {account.FirstName} {account.LastName}!");
            Console.WriteLine($"Press 'Enter' to continue.");
            Console.ReadKey();
        }
    }

    public void Login()
    {
        Console.Clear();
        Console.WriteLine("=== Login ===\n");

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = ReadPassword();

        AccountModel account = _accountLogic.CheckLogin(email, password);

        if (account != null)
        {
            Console.WriteLine($"\nWelcome {account.FirstName} {account.LastName}!");
            if (account.RankName != "-")
            {
                Console.WriteLine($"Your current loyalty rank is: {account.RankName}");
            }
            Session.SetUser(account);
        }
        else
        {
            // TODO: add reason why invalid and requery 
            Console.WriteLine("\nInvalid email or password.");
        }
        
        Console.ReadKey();
    }
}