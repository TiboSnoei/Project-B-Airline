public class GainLoyaltyPointsBusiness
{
    public AccountModel LoggedInUser = Session.LoggedInUser;
    public double AmountSpend { get; set; }
    public GainLoyaltyPointsBusiness(double amountSpend)
    {
        AmountSpend = amountSpend;
    }

    public int CalculateLoyaltyPoints() // INFO: Added this method to simplify adding more complicated calculations for loyalty points in the future.
    {
        return Convert.ToInt32(Math.Truncate(AmountSpend));
    }

    public void GiveLoyaltyPoints() // INFO: This is the method that is supposed to be called when the customer spends money on bookings, upgrading bookings or extras. You should not have to use anything else.
    {
        try
        {
            var gainLoyaltyPointsAcces = new GainLoyaltyPointsAcces(CalculateLoyaltyPoints());
            gainLoyaltyPointsAcces.WriteLoyaltyPoints();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error While Adding/Writing Loyalty Points To Account (GainLoyaltyPointsBuisiness): {ex}");
        }
    }
}