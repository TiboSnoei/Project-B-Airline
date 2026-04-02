using System.Linq;

public class AccountLogic
{
    private AccountAccess _accountAccess = new AccountAccess();

    public bool CreateAccount(AccountModel newAccount)
    {
        // Check if account already exists
        AccountModel? existingAccount = _accountAccess.GetByEmail(newAccount.Email);
        if (existingAccount != null)
        {
            return false; // Voor net geval dat account al bestaat.
        }

        // valideerdt name fields
        if (!CheckName(newAccount.FirstName) || !CheckName(newAccount.LastName))
        {
            return false;
        }

        // valideert email and password
        if (!IsValidEmail(newAccount.Email) || !CheckValidPassword(newAccount.Password))
        {
            return false;
        }

        _accountAccess.Write(newAccount);
        return true;
    }

    public AccountModel? CheckLogin(string email, string password)
    {
        AccountModel account = _accountAccess.GetByEmail(email);

        if (account != null && account.Password == password)
        {
            return account;
        }

        return null;
    }

    public bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    public bool CheckValidPassword(string password)
    {
        return password.Length >= 6;
    }

    public bool CheckName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length > 1;
    }

    public int GetIdByEmail(string email)
    {
        int id = _accountAccess.GetIdByEmail(email);
        return id != 0 ? id : 0;
    }
}