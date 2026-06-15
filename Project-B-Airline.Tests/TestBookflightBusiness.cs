[TestClass]

public class BookflightBusinessTests
{
    // Test for the GetRank method in the bookflight business class.
    [TestMethod]
    public void GetRank_Returns_Correct_Rank_Based_On_Loyalty_Points()
    {
        Assert.AreEqual("Platinum", BookFlightBusiness.GetRank(10000));
        Assert.AreEqual("Gold", BookFlightBusiness.GetRank(9999));
        Assert.AreEqual("Gold", BookFlightBusiness.GetRank(5000));
        Assert.AreEqual("Silver", BookFlightBusiness.GetRank(4999));
        Assert.AreEqual("Silver", BookFlightBusiness.GetRank(2500));
        Assert.AreEqual("Bronze", BookFlightBusiness.GetRank(2499));
        Assert.AreEqual("Bronze", BookFlightBusiness.GetRank(1000));
        Assert.AreEqual("-", BookFlightBusiness.GetRank(999));
    }

    [TestMethod]
    public void CheckRankIncrease_Returns_True_When_Rank_Increases()
    {
        var account = new AccountModel { LoyaltyPoints = 9000, RankName = "Gold" };
        var flight = new FlightModel { DefaultPrice = 2000 };

        bool result = BookFlightBusiness.CheckRankIncrease(account, flight);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CheckRankIncrease_Returns_False_When_Rank_Does_Not_Increase()
    {
        var account = new AccountModel { LoyaltyPoints = 9000, RankName = "Gold" };
        var flight = new FlightModel { DefaultPrice = 500 };

        bool result = BookFlightBusiness.CheckRankIncrease(account, flight);

        Assert.IsFalse(result);
    }
}