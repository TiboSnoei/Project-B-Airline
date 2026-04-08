using System;

public class AccountPresentation
{
    private AccountLogic _accountLogic = new AccountLogic();

    public void Register()
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

        Console.Write("Phone number: ");
        string telNum = Console.ReadLine();

        AccountModel newAccount = new AccountModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            TelNum = telNum
        };

        bool success = _accountLogic.CreateAccount(newAccount);

        if (success)
            Console.WriteLine("\nRegistration successful!");
            // Ook hier zou een verwijzing naar de menu-clas moeten komen als die is gemaakt.
        else
            Console.WriteLine("\nRegistration failed (invalid data or user already exists).");

        Console.ReadKey();
    }

public AccountModel Login()
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
        return account; // now valid
    }
    else
    {
        Console.WriteLine("\nInvalid email or password.");
    }

    Console.ReadKey();
    return null; // return null if login failed
}
}