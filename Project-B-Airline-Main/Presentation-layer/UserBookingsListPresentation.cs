public class UserBookingsListPresentation
{
    AccountModel LoggedInUser = Session.LoggedInUser;
    // constructor
    public void userbookingslist(AccountModel loggedInUser)
    {
        LoggedInUser = loggedInUser;
    }

    public void ShowBookingsAsMenu() // TODO: user menu class to creato a menu with all bookings in it.
    {
        UserBookingsListLogic userBookingsListLogic = new UserBookingsListLogic(LoggedInUser);

        string[] optionsarray = userBookingsListLogic.GetBookings();
        string spacingformat = "{0,-10}|{1,-20}|{2,-30}|{3,-40}|{4,-50}|{5,-60}|{6,-70}";
        string header = "Your Bookings";
        string optionsheader = string.Format(spacingformat, "UserID", "FlightID", "Seat", "SeatChosen", "ExtraLegroom", "OnflightMeal", "ExtraLuggage");
        Menu menu = new Menu();

        int chosenbookingindex = menu.VerticalMenuWithColumns(optionsarray, header, optionsheader);
        
    }
}