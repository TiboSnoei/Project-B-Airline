using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Project_B_Airline.Tests;

[TestClass]
public class PasswordConfirmationTests
{
    [TestMethod]
    public void ConfirmPassword_AssertsIfPasswordsAreEqual_AssertsAndRetrunsTrue()
    {
        // Arrange
        var accountLogic = new AccountLogic();
        string password = "TestPassword";
        string confirmationpassword = "TestPassword";

        // Act
        var result = accountLogic.ConfirmPassword(password, confirmationpassword);

        // Assert
        Assert.AreEqual(true, result);
    }
}