using System;
using System.Collections.Generic;
using System.Linq;

public class AccountAccess
{
    private static List<AccountModel> _accounts = new List<AccountModel>();
    private static int _nextId = 1;

    public void Write(AccountModel account)
    {
        account.UserID = _nextId++;
        account.CreatedAt = DateTime.Now;

        _accounts.Add(account);
    }

    public AccountModel GetByEmail(string email)
    {
        return _accounts.FirstOrDefault(a => a.Email == email);
    }

    public int GetIdByEmail(string email)
    {
        AccountModel account = _accounts.FirstOrDefault(a => a.Email == email);

        if (account != null)
        {
            return account.UserID;
        }

        return 0;
    }

    public List<AccountModel> GetAll()
    {
        return _accounts;
    }

    public void Update(AccountModel updatedAccount)
    {
        AccountModel existing = _accounts.FirstOrDefault(a => a.UserID == updatedAccount.UserID);

        if (existing != null)
        {
            existing.Email = updatedAccount.Email;
            existing.Password = updatedAccount.Password;
            existing.FirstName = updatedAccount.FirstName;
            existing.LastName = updatedAccount.LastName;
            existing.TelNum = updatedAccount.TelNum;
            existing.LoyaltyPoints = updatedAccount.LoyaltyPoints;
            // CreatedAt blijft onveranderd
        }
    }

    public void Delete(int userId)
    {
        AccountModel account = _accounts.FirstOrDefault(a => a.UserID == userId);

        if (account != null)
        {
            _accounts.Remove(account);
        }
    }
}