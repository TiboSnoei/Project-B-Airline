using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

[TestClass]
public class AccountAccessTests
{
    private AccountAccess _accountAccess;

    [TestInitialize]
    public void Setup()
    {
        _accountAccess = new AccountAccess();
        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();
    }

    [TestMethod]
    public void Write_Inserts_New_Account()
    {
        var account = new AccountModel
        {
            FirstName = "Geert",
            LastName = "Geertmans",
            Email = $"Geert{Guid.NewGuid()}@mail.com",
            Password = "password123",
            TelNum = "0612345678"
        };

        _accountAccess.Write(account);

        var result = _accountAccess.GetByEmail(account.Email);

        Assert.IsNotNull(result);
        Assert.AreEqual(account.Email, result.Email);
        Assert.AreEqual("Geert", result.FirstName);
        Assert.AreEqual("Customer", result.UserType);
        Assert.AreEqual(0, result.LoyaltyPoints);
    }

    [TestMethod]
    public void GetByEmail_Returns_Null_When_Email_Does_Not_Exist()
    {
        var result = _accountAccess.GetByEmail("IExistIPromise@gmail.com");

        Assert.IsNull(result);
    }

    // TODO: should this be 0? maby null is better instead of giving the first user ever created.....
    // this could get users logged into account 0 no?
    [TestMethod]
    public void GetIdByEmail_Returns_Zero_When_Email_Does_Not_Exist()
    {
        var result = _accountAccess.GetIdByEmail("IExistIPromise@gmail.com");

        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetAll_Returns_List_Of_Accounts()
    {
        var result = _accountAccess.GetAll();

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(List<AccountModel>));
    }

    // TODO: when implementing updating users, uncomment and adjust apropiately.
    // [TestMethod]
    // public void Update_Modifies_Existing_Account()
    // {
    //     var email = $"update{Guid.NewGuid()}@mail.com";

    //     var account = new AccountModel
    //     {
    //         FirstName = "Jane",
    //         LastName = "Smith",
    //         Email = email,
    //         Password = "oldPassword",
    //         TelNum = "0600000000"
    //     };

    //     _accountAccess.Write(account);

    //     var inserted = _accountAccess.GetByEmail(email);

    //     inserted.FirstName = "Updated";
    //     inserted.Password = "newPassword";

    //     _accountAccess.Update(inserted);

    //     var updated = _accountAccess.GetByEmail(email);

    //     Assert.AreEqual("Updated", updated.FirstName);
    //     Assert.AreEqual("newPassword", updated.Password);
    // }

    // TODO: when implementing deleting users, uncomment and adjust apropiately.
    // [TestMethod]
    // public void Delete_Removes_Account()
    // {
    //     var email = $"delete{Guid.NewGuid()}@mail.com";

    //     var account = new AccountModel
    //     {
    //         FirstName = "Delete",
    //         LastName = "Me",
    //         Email = email,
    //         Password = "password",
    //         TelNum = "0611111111"
    //     };

    //     _accountAccess.Write(account);

    //     var inserted = _accountAccess.GetByEmail(email);

    //     _accountAccess.Delete(inserted.UserID);

    //     var deleted = _accountAccess.GetByEmail(email);

    //     Assert.IsNull(deleted);
    // }
}