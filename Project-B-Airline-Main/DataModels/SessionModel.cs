public static class Session
{
    public static AccountModel LoggedInUser { get; private set; }

    public static void SetUser(AccountModel user)
    {
        LoggedInUser = user;
    }

    public static void Logout()
    {
        LoggedInUser = null;
    }
}