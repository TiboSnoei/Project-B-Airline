public static class BookFlightBusiness
{
    public static string GetRank(int LoyaltyPoints)
    // This method checks the amount of loyalty points a user has and returns their loyalty rank as a string.
    {
        if (LoyaltyPoints >= 10000)
            return "Platinum";
        else if (LoyaltyPoints >= 5000)
            return "Gold";
        else if (LoyaltyPoints >= 2500)
            return "Silver";
        else if (LoyaltyPoints >= 1000)
            return "Bronze";
        else
            return "-";
    }

    // Checks the amount of loyalty points the user has, and the price of the flight they booked.
    // Every euro is worth 1 loyalty point.
    // The method calculates the new amount of loyalty points the user will have after confirming the flight
    // and returns 'true' if the rank will increase and 'false' if the rank will stay the same.
    public static bool CheckRankIncrease(AccountModel account, FlightModel chosenFlight)
    {
        var _GainLoyaltyPointsBusiness = new GainLoyaltyPointsBusiness(chosenFlight.DefaultPrice);
        int pointsBefore = account.LoyaltyPoints;
        int pointsAfter = account.LoyaltyPoints + _GainLoyaltyPointsBusiness.CalculateLoyaltyPoints();

        string newRank = GetRank(pointsAfter);
        string oldRank = account.RankName;

        if (newRank.StartsWith("-")) newRank = oldRank;

        return newRank != oldRank;
    }

    // This method is called in the presentation layer to confirm a booking.
    // It calls the method from the data access layer to save the booking in the database.
    public static void EnterIntoDatabase(FlightModel chosenflight)
    {
        BookFlightAccess.EnterIntoDatabase(chosenflight);
    }

    // This method is called in the presentation layer after a user has confirmed a booking and gained loyalty points.
    // It calls the method in the data access layer to update the loyalty rank of the user in the database if they have gained enough loyalty points to increase their rank.
    public static void UpdateLoyaltyRank(AccountModel LoggedInUser, FlightModel chosenFlight)
    {
        var bookFlightAccess = new BookFlightAccess();
        bookFlightAccess.UpdateLoyaltyRank(LoggedInUser, chosenFlight);
    }
}