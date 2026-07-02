using System.Linq;
using System.ComponentModel.DataAnnotations;

public class AccountLogic
{
    private AccountAccess _accountAccess = new AccountAccess();

    public bool CreateAccount(AccountModel newAccount)
    {
        AccountModel? existingAccount = _accountAccess.GetByEmail(newAccount.Email);
        if (existingAccount != null)
        {
            return false; // Voor net geval dat account al bestaat.
        }

        // valideerdt name fields
        if (string.IsNullOrEmpty(newAccount.FirstName) || newAccount.FirstName.Length < 2 || !newAccount.FirstName.All(char.IsLetter))
        {
            return false;
        }

        if (string.IsNullOrEmpty(newAccount.LastName) || newAccount.LastName.Length < 2 || !newAccount.LastName.All(char.IsLetter))
        {
            return false;
        }

        // valideert email, phone number and password
        if (string.IsNullOrEmpty(newAccount.Email) || !new EmailAddressAttribute().IsValid(newAccount.Email))
        {
            return false;
        }

        if (string.IsNullOrEmpty(newAccount.TelNum) || newAccount.TelNum.Length < 10 || !newAccount.TelNum.All(char.IsDigit))
        {
            return false;
        }

        if (string.IsNullOrEmpty(newAccount.Password) || newAccount.Password.Length < 6)
        {
            return false;
        }

        // Hash het wachtwoord voordat het wordt opgeslagen
        newAccount.Password = BCrypt.Net.BCrypt.HashPassword(newAccount.Password);

        _accountAccess.Write(newAccount);
        return true;
    }

    public AccountModel? CheckLogin(string email, string password)
    {
        AccountModel account = _accountAccess.GetByEmail(email);

        if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
        {
            return account;
        }

        return null;
    }

    public bool ConfirmPassword(string password, string confirmationpassword)
    {
        return confirmationpassword == password;
    }

    // Irrevant methods for validation, commented out for now. They can be used if needed in the future.

    // public bool IsValidEmail(string email)
    // {
    //     bool check = email.Contains("@") && email.Contains(".");
    //     if(!check)
    //     {
    //         Console.WriteLine("Not a valid Email.");
    //     }
    //     return check;
    // }

    // public bool CheckValidPassword(string password)
    // {
    //     bool check = password.Length >= 6;
    //     if(!check)
    //     {
    //         Console.WriteLine("Password must be 6 or more characters long.");
    //     }
    //     return check;
    // }

    // public bool CheckName(string name)
    // {
    //     bool check = !string.IsNullOrWhiteSpace(name) && name.Length > 1;
    //     if(!check)
    //     {
    //         Console.WriteLine("Name can't be empty.");
    //     }
    //     return check;
    // }

    // public int GetIdByEmail(string email)
    // {
    //     int id = _accountAccess.GetIdByEmail(email);
    //     if(id != 0)
    //     {
    //         Console.WriteLine("An account with this email already excists.");
    //     }
    //     return id != 0 ? id : 0;
    // }
}