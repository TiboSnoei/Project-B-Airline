public class AccountModel
{
    public int UserID { get; set; }

    public string Email { get; set; }
    public string Password { get; set; } 

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime CreatedAt { get; set; }

    public string TelNum { get; set; }

    public int LoyaltyPoints { get; set; }
}