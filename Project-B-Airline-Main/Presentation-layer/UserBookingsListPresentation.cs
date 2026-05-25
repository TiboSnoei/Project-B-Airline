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
        Menu menu = new Menu();
        UserBookingsListLogic userBookingsListLogic = new UserBookingsListLogic(LoggedInUser);
        List<CustomerFlightModel> BookingsList = userBookingsListLogic.GiveBookingsList();

        string spacingformat = "{0,-10}|{1,-20}|{2,-30}|{3,-40}|{4,-50}|{5,-60}|{6,-70}|{6,-80}";
        string header = "Your Bookings";
        string optionsheader = string.Format(spacingformat, "UserID", "FlightID", "FlightNumber", "Seat", "SeatChosen", "ExtraLegroom", "OnflightMeal", "ExtraLuggage");
        string[] optionsarray = userBookingsListLogic.GetBookings();
        
        int chosenbookingindex = menu.VerticalMenuWithColumns(optionsarray, header, optionsheader);
        if (chosenbookingindex == optionsarray.Length)
        {
            return;
        }
        
        else
        {
            CustomerFlightModel selectedbooking = BookingsList[chosenbookingindex];
            Console.WriteLine("Viewing and upgrading bookings not implomented yet"); // the menu to view and upgrade your booking as a customer is not implomented yet
        }
    }
}