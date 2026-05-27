
[TestClass]
public class UserBookingsListTests
{

    private UserBookingsListAcces _UserBookingsListAcces;
    private BookFlightAccess _BookFlightAccess;

    [TestInitialize]
    public void Setup()
    {
        var loggedinuser = new AccountModel
        {
            UserID = 1,
            Email = "UnitTestUser@DuckTeep.com",
            Password = "UnitTestUserPassword",
            FirstName = "Unit",
            LastName = "Test",
            UserType = "Customer",
            CreatedAt = DateTime.Parse("2000-01-01 00:00:00"),
            TelNum = "0611111111",
            LoyaltyPoints = 0
        };

        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();

        _UserBookingsListAcces = new UserBookingsListAcces(loggedinuser);
        _BookFlightAccess = new BookFlightAccess();

    }

    [TestMethod]
    public void SearchBookings_Returns_Correct_Booking()
    {
        List<CustomerFlightModel> bookingsList = new ();

        for (int i = 1; i < 4; i++)
        {
            var booking = new CustomerFlightModel
            {
                UserID = 10000,
                FlightID = i,
                Seat = "12A",
                SeatChosen = false,
                ExtraLegroom = false,
                OnflightMeal = false,
                ExtraLuggage = false
            };

            _BookFlightAccess.Write(booking);
            bookingsList.Add(booking);
        }


        var results = _UserBookingsListAcces.SearchBookings();
        var insertedbookings = results.TakeLast(3).ToList();

        // Enumerable.SequenceEqual(insertedbookings, bookingsList);

        for (int i = 0; i < 3; i++)
        {
            Assert.AreEqual(bookingsList[i].UserID, insertedbookings[i].UserID);
        }
    }
}