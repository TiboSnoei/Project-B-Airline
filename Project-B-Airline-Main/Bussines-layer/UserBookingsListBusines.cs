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
    public string[] ConvertBookingsToArray(List<CustomerFlightModel> bookings)
    {
        FlightLogic flightlogic = new ();
        string spacingformat = "{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}|{5,-25}|{6,-25}|{7,-25}";
        List<string> optionslist = new List<string>();

        foreach (CustomerFlightModel booking in bookings)
        {
            FlightModel _Flight = flightlogic.GetFlightById(booking.FlightID);
            if (_Flight is null)
            {
                continue;
            }

            var _FlightNumber = _Flight.FlightNumber;
            var _TailNumber = _Flight.TailNumber;
            var _TakeOffTime = _Flight.TakeOffTime;
            var _ArrivalTime = _Flight.ArrivalTime;
            var _Price = _Flight.DefaultPrice; // TODO: Price needs to be chanced when upgrades/extras are booked or loyalty bonuses are givven.
            var _Destination = _Flight.Destination;
            var _Origin = _Flight.Origin;

            string option = string.Format(spacingformat, _FlightNumber, _TailNumber, booking.Seat, _TakeOffTime, _ArrivalTime, _Price, _Destination, _Origin);
            optionslist.Add(option);
        }

        string[] optionsarray = optionslist.ToArray();

        return optionsarray;
    }
}