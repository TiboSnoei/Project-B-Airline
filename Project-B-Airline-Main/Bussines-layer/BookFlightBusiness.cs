public class BookFlightBusiness
{
        public static string GetRank(int LoyaltyPoints)
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
                return null;
        }

        // Checks the amount of loyalty points the user has, and the price of the flight they booked.
        // Every euro is worth 1 loyalty point.
        // The method calculates the new amount of loyalty points the user will have after confirming the flight
        // and returns 'true' if the rank will increase and 'false' if the rank will stay the same.
        public static bool CheckRankIncrease(AccountModel account, FlightModel chosenFlight)
        {
                int pointsBefore = account.LoyaltyPoints;
                int pointsAfter = account.LoyaltyPoints + (int)chosenFlight.DefaultPrice;
        
                string newRank = GetRank(pointsAfter);
                string? oldRank = account.RankName;
        
                return newRank != oldRank;
        }
}