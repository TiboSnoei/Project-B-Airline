public class UserBookingsListPresentation
{
    public void ShowBookingsAsMenu()
    {
        Menu menu = new Menu();
        AccountModel LoggedInUser = Session.LoggedInUser;

        UserBookingsListLogic userBookingsListLogic = new UserBookingsListLogic(LoggedInUser);
        List<CustomerFlightModel> BookingsList = userBookingsListLogic.GiveBookingsList();

        string spacingformat = "{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}|{5,-25}|{6,-25}|{7,-25}";
        string header = "Your Bookings";
        string optionsheader = string.Format(spacingformat, "FlightNumber", "Tailnumber", "Seat", "TakeOffTime", "ArrivalTime", "Price", "Departure", "Destination");
        string[] optionsarray = userBookingsListLogic.GetBookings();

        if (optionsarray.Length > 0)
        {
            int chosenbookingindex = menu.VerticalMenuWithColumns(optionsarray, header, optionsheader);
            if (chosenbookingindex == optionsarray.Length)
            {
                return;
            }
            
            else
            {
                CustomerFlightModel selectedbooking = BookingsList[chosenbookingindex];
                Console.WriteLine("Viewing and upgrading bookings not implomented yet"); // TODO: the menu to view and upgrade your booking as a customer is not implomented yet. impoment it.
            }
        }

        else
        {
            Console.Clear();
            Console.WriteLine("\nThere are no bookings for this account.\n");
            Console.WriteLine("Press ESC to return.");
        }
    }
}