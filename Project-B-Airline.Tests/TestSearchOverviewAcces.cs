using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class SearchoverviewAccesTests
{
    private SearchoverviewAcces _searchAccess;
    private FlightAccess _flightAccess;

    [TestInitialize]
    public void Setup()
    {
        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();

        _searchAccess = new SearchoverviewAcces();
        _flightAccess = new FlightAccess();
    }

    [TestMethod]
    public void SearchFlights_Returns_Outbound_Flights()
    {
        var departureDate = DateTime.Today.AddDays(7);

        var flight = new FlightModel
        {
            TailNumber = $"HR101",
            Origin = "Rotterdam",
            Destination = "London",
            TakeOffTime = departureDate.AddHours(10),
            ArrivalTime = departureDate.AddHours(12),
            LegroomFee = 20,
            DefaultPrice = 150,
            MealPrice = 15,
            ChosenSeatFee = 10,
            ExtraLuggageFee = 30
        };

        _flightAccess.Write(flight);

        var search = new SearchOverviewModel
        {
            Destinationselected = "London",
            Departuredate = departureDate,
            Returnflightselected = false
        };

        List<FlightModel> results = _searchAccess.SearchFlights(search);

        Assert.IsNotNull(results);
        Assert.IsTrue(results.Any(x =>
            x.Destination == "London" &&
            x.Origin == "Rotterdam"));
    }

    [TestMethod]
    public void SearchFlights_Returns_Outbound_And_Return_Flights()
    {
        var departureDate = DateTime.Today.AddDays(10);
        var returnDate = DateTime.Today.AddDays(15);

        var outboundFlight = new FlightModel
        {
            TailNumber = $"HR101",
            Origin = "Rotterdam",
            Destination = "Paris",
            TakeOffTime = departureDate.AddHours(9),
            ArrivalTime = departureDate.AddHours(11),
            LegroomFee = 20,
            DefaultPrice = 120,
            MealPrice = 10,
            ChosenSeatFee = 5,
            ExtraLuggageFee = 20
        };

        var returnFlight = new FlightModel
        {
            TailNumber = $"HR101",
            Origin = "Paris",
            Destination = "Rotterdam",
            TakeOffTime = returnDate.AddHours(14),
            ArrivalTime = returnDate.AddHours(16),
            LegroomFee = 20,
            DefaultPrice = 120,
            MealPrice = 10,
            ChosenSeatFee = 5,
            ExtraLuggageFee = 20
        };

        _flightAccess.Write(outboundFlight);
        _flightAccess.Write(returnFlight);

        var search = new SearchOverviewModel
        {
            Destinationselected = "Paris",
            Departuredate = departureDate,
            Returndate = returnDate,
            Returnflightselected = true
        };

        List<FlightModel> results = _searchAccess.SearchFlights(search);

        Assert.IsNotNull(results);

        Assert.IsTrue(results.Any(x =>
            x.Origin == "Rotterdam" &&
            x.Destination == "Paris"));

        Assert.IsTrue(results.Any(x =>
            x.Origin == "Paris" &&
            x.Destination == "Rotterdam"));
    }

    [TestMethod]
    public void SearchFlights_Returns_Empty_List_When_No_Flights_Exist()
    {
        var search = new SearchOverviewModel
        {
            Destinationselected = "NonExistingDestination",
            Departuredate = DateTime.Today.AddYears(1),
            Returnflightselected = false
        };

        List<FlightModel> results = _searchAccess.SearchFlights(search);

        Assert.IsNotNull(results);
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    public void SearchFlights_Returns_Flights_Only_On_Selected_Date()
    {
        var selectedDate = DateTime.Today.AddDays(20);

        var matchingFlight = new FlightModel
        {
            TailNumber = $"HR101",
            Origin = "Rotterdam",
            Destination = "Rome",
            TakeOffTime = selectedDate.AddHours(8),
            ArrivalTime = selectedDate.AddHours(10),
            LegroomFee = 20,
            DefaultPrice = 200,
            MealPrice = 15,
            ChosenSeatFee = 10,
            ExtraLuggageFee = 30
        };

        var nonMatchingFlight = new FlightModel
        {
            TailNumber = $"HR102",
            Origin = "Rotterdam",
            Destination = "Rome",
            TakeOffTime = selectedDate.AddDays(1).AddHours(8),
            ArrivalTime = selectedDate.AddDays(1).AddHours(10),
            LegroomFee = 20,
            DefaultPrice = 200,
            MealPrice = 15,
            ChosenSeatFee = 10,
            ExtraLuggageFee = 30
        };

        _flightAccess.Write(matchingFlight);
        _flightAccess.Write(nonMatchingFlight);

        var search = new SearchOverviewModel
        {
            Destinationselected = "Rome",
            Departuredate = selectedDate,
            Returnflightselected = false
        };

        List<FlightModel> results = _searchAccess.SearchFlights(search);

        Assert.IsTrue(results.Any(x =>
            x.TailNumber == matchingFlight.TailNumber));

        Assert.IsFalse(results.Any(x =>
            x.TailNumber == nonMatchingFlight.TailNumber));
    }
}