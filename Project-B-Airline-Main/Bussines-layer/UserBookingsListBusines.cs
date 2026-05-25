using System.Security.Cryptography.X509Certificates;

public class UserBookingsListLogic
{
    AccountModel LoggedInUser = Session.LoggedInUser;

    public UserBookingsListLogic(AccountModel loggedInUser)
    {
        LoggedInUser = loggedInUser;
    }


    // gets bookings from the database throught the acces layer
    // returns the bookings as a list of customerflightmodels
    public List<CustomerFlightModel> GiveBookingsList()
    {
        UserBookingsListAcces userBookingsListAcces = new UserBookingsListAcces(LoggedInUser);
        List<CustomerFlightModel> Bookings = userBookingsListAcces.SearchBookings();
        return Bookings;
    }


    // gets bookings from the database throught the acces layer and calls ConvertBookingsToString.
    // returns a list of bookings as strings
    public string[] GetBookings()
    {
        UserBookingsListAcces userBookingsListAcces = new UserBookingsListAcces(LoggedInUser);
        List<CustomerFlightModel> Bookings = userBookingsListAcces.SearchBookings();
        string[] optionsarray = ConvertBookingsToArray(Bookings);
        return optionsarray;
    }


    // recieves a list of CustomerFlightModels and converts the list into a array of strings
    // first converts the bookings into strings and adds them to a list
    // then converts the list into an array
    // returns an array of bookings as strings
    public string[] ConvertBookingsToArray(List<CustomerFlightModel> bookings) // TODO: convert all bookings from flightmodel to string.
    {
        string spacingformat = "{0,-10}|{1,-20}|{2,-30}|{3,-40}|{4,-50}|{5,-60}|{6,-70}|{6,-80}";
        List<string> optionslist = new List<string>();

        foreach (CustomerFlightModel booking in bookings)
        {
            string option = string.Format(spacingformat, booking.UserID, booking.FlightID, booking.FlightNumber, booking.Seat, booking.SeatChosen, booking.ExtraLegroom, booking.OnflightMeal, booking.ExtraLuggage);
            optionslist.Add(option);
        }

        string[] optionsarray = optionslist.ToArray();

        return optionsarray;
    }
}