using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class AccountLogicTests
{
    private AccountLogic _accountLogic;

    [TestInitialize]
    public void Setup()
    {
        _accountLogic = new AccountLogic();
        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();
    }

    [TestMethod]
    public void IsValidEmail_Returns_True_For_Valid_Email()
    {
        bool result = _accountLogic.IsValidEmail("test@mail.com");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidEmail_Returns_False_For_Invalid_Email()
    {
        bool result = _accountLogic.IsValidEmail("invalidemail");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CheckValidPassword_Returns_True_For_Valid_Password()
    {
        bool result = _accountLogic.CheckValidPassword("password123");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CheckValidPassword_Returns_False_For_Short_Password()
    {
        bool result = _accountLogic.CheckValidPassword("123");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CheckName_Returns_True_For_Valid_Name()
    {
        bool result = _accountLogic.CheckName("John");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CheckName_Returns_False_For_Empty_Name()
    {
        bool result = _accountLogic.CheckName("");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ConfirmPassword_Returns_True_When_Passwords_Match()
    {
        bool result = _accountLogic.ConfirmPassword("password123", "password123");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ConfirmPassword_Returns_False_When_Passwords_Do_Not_Match()
    {
        bool result = _accountLogic.ConfirmPassword("password123", "differentpassword");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateAccount_Returns_True_For_Valid_Account()
    {
        var account = new AccountModel
        {
            FirstName = "John",
            LastName = "Helldiver",
            Email = $"logic{Guid.NewGuid()}@mail.com",
            Password = "password123",
            TelNum = "0612345678"
        };

        bool result = _accountLogic.CreateAccount(account);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CreateAccount_Returns_False_When_Email_Already_Exists()
    {
        string email = $"duplicate{Guid.NewGuid()}@mail.com";

        var account1 = new AccountModel
        {
            FirstName = "John",
            LastName = "Helldiver",
            Email = email,
            Password = "password123",
            TelNum = "0612345678"
        };

        var account2 = new AccountModel
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = email,
            Password = "password456",
            TelNum = "0687654321"
        };

        _accountLogic.CreateAccount(account1);

        bool result = _accountLogic.CreateAccount(account2);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateAccount_Returns_False_For_Invalid_Email()
    {
        var account = new AccountModel
        {
            FirstName = "John",
            LastName = "Snow",
            Email = "invalidemail",
            Password = "password123",
            TelNum = "0612345678"
        };

        bool result = _accountLogic.CreateAccount(account);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateAccount_Returns_False_For_Invalid_Password()
    {
        var account = new AccountModel
        {
            FirstName = "John",
            LastName = "Doe",
            Email = $"shortpass{Guid.NewGuid()}@mail.com",
            Password = "123",
            TelNum = "0612345678"
        };

        bool result = _accountLogic.CreateAccount(account);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CheckLogin_Returns_Account_For_Correct_Credentials()
    {
        string email = $"login{Guid.NewGuid()}@mail.com";
        string password = "password123";

        var account = new AccountModel
        {
            FirstName = "Login",
            LastName = "Test",
            Email = email,
            Password = password,
            TelNum = "0612345678"
        };

        _accountLogic.CreateAccount(account);

        AccountModel result = _accountLogic.CheckLogin(email, password);

        Assert.IsNotNull(result);
        Assert.AreEqual(email, result.Email);
    }

    [TestMethod]
    public void CheckLogin_Returns_Null_For_Wrong_Password()
    {
        string email = $"wrongpass{Guid.NewGuid()}@mail.com";

        var account = new AccountModel
        {
            FirstName = "Wrong",
            LastName = "Password",
            Email = email,
            Password = "correctpassword",
            TelNum = "0612345678"
        };

        _accountLogic.CreateAccount(account);

        AccountModel result = _accountLogic.CheckLogin(email, "wrongpassword");

        Assert.IsNull(result);
    }
}