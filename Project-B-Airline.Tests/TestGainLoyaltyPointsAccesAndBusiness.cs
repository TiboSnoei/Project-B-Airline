using System.Reflection.Metadata;

[TestClass]
public class GainLoyaltyPointsAccesAndBusinessTest
{
    private GainLoyaltyPointsAcces _GainLoyaltyPointsAcces;
    private GainLoyaltyPointsBusiness _GainLoyaltyPointsBusiness1;
    private GainLoyaltyPointsBusiness _GainLoyaltyPointsBusiness2;
    private GainLoyaltyPointsBusiness _GainLoyaltyPointsBusiness3;
    private AccountLogic _AccountLogic;

    [TestInitialize]
    public void Setup()
    {
        _AccountLogic = new AccountLogic();
    }

    [TestMethod]
    public void WriteLoyaltyPoints_Inserts_Points_Correctly()
    {

        AccountModel newAccount = new AccountModel
        {
            FirstName = "Unit",
            LastName = "Test",
            Email = "UnitTest@duckteep.com",
            Password = "UnitTestPassword",
            TelNum = "0600110011"
        };

        var success = _AccountLogic.CreateAccount(newAccount);

        if (success)
        {
            AccountModel account = _AccountLogic.CheckLogin("UnitTest@duckteep.com", "UnitTestPassword");
            Session.SetUser(account);
        }

        var PreviousLoyaltyPoints = Session.LoggedInUser.LoyaltyPoints;

        int loyaltyPointsToAdd = 500;
        _GainLoyaltyPointsAcces = new GainLoyaltyPointsAcces(loyaltyPointsToAdd);

        _GainLoyaltyPointsAcces.WriteLoyaltyPoints();

        Assert.AreEqual(PreviousLoyaltyPoints, Session.LoggedInUser.LoyaltyPoints - 500);
    }

    [TestMethod]
    public void CalculateLoyaltyPoints_Returns_Correct_Amount()
    {
        double amountSpend1 = 532.52;
        double amountSpend2 = 439.34;
        double amountSpend3 = 200;

        _GainLoyaltyPointsBusiness1 = new GainLoyaltyPointsBusiness(amountSpend1);
        _GainLoyaltyPointsBusiness2 = new GainLoyaltyPointsBusiness(amountSpend2);
        _GainLoyaltyPointsBusiness3 = new GainLoyaltyPointsBusiness(amountSpend3);

        var CalculatedLoyaltyPoints1 = _GainLoyaltyPointsBusiness1.CalculateLoyaltyPoints();
        var CalculatedLoyaltyPoints2 = _GainLoyaltyPointsBusiness2.CalculateLoyaltyPoints();
        var CalculatedLoyaltyPoints3 = _GainLoyaltyPointsBusiness3.CalculateLoyaltyPoints();

        Assert.AreEqual(532, CalculatedLoyaltyPoints1);
        Assert.AreEqual(439, CalculatedLoyaltyPoints2);
        Assert.AreEqual(200, CalculatedLoyaltyPoints3);
    }
}