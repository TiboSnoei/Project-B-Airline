using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

[TestClass]
public class FlightAccessTests
{
    private FlightAccess _flightAccess;

    [TestInitialize]
    public void Setup()
    {
        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();

        _flightAccess = new FlightAccess();
    }

    [TestMethod]
    public void Write_Inserts_New_Flight()
    {
        var flight = new FlightModel
        {
            TailNumber = $"HR101",
            Origin = "Amsterdam",
            Destination = "London",
            TakeOffTime = DateTime.Now.AddHours(2),
            ArrivalTime = DateTime.Now.AddHours(4),
            LegroomFee = 25,
            DefaultPrice = 150,
            MealPrice = 15,
            ChosenSeatFee = 10,
            ExtraLuggageFee = 30
        };

        bool result = _flightAccess.Write(flight);

        var flights = _flightAccess.GetAll();

        var insertedFlight = flights.LastOrDefault(x =>
            x.TailNumber == flight.TailNumber &&
            x.Origin == "Amsterdam" &&
            x.Destination == "London");

        Assert.IsTrue(result);
        Assert.IsNotNull(insertedFlight);
    }

    [TestMethod]
    public void GetAll_Returns_List_Of_Flights()
    {
        var result = _flightAccess.GetAll();

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(List<FlightModel>));
    }

    [TestMethod]
    public void Update_Modifies_Existing_Flight()
    {
        var flight = new FlightModel
        {
            TailNumber = $"HR102",
            Origin = "Paris",
            Destination = "Berlin",
            TakeOffTime = DateTime.Now.AddHours(1),
            ArrivalTime = DateTime.Now.AddHours(3),
            LegroomFee = 20,
            DefaultPrice = 100,
            MealPrice = 10,
            ChosenSeatFee = 5,
            ExtraLuggageFee = 25
        };

        _flightAccess.Write(flight);

        var insertedFlight = _flightAccess.GetAll()
            .Last(x => x.TailNumber == flight.TailNumber);

        insertedFlight.Origin = "Madrid";
        insertedFlight.DefaultPrice = 200;

        bool result = _flightAccess.Update(insertedFlight);

        var updatedFlight = _flightAccess.GetAll()
            .Last(x => x.FlightId == insertedFlight.FlightId);

        Assert.IsTrue(result);
        Assert.AreEqual("Madrid", updatedFlight.Origin);
        Assert.AreEqual(200, updatedFlight.DefaultPrice);
    }

    [TestMethod]
    public void Update_Returns_False_For_Nonexistent_Flight()
    {
        var flight = new FlightModel
        {
            FlightId = -999,
            TailNumber = "NONEXISTENT",
            Origin = "There",
            Destination = "Here",
            TakeOffTime = DateTime.Now,
            ArrivalTime = DateTime.Now.AddHours(2),
            LegroomFee = 10,
            DefaultPrice = 50,
            MealPrice = 5,
            ChosenSeatFee = 5,
            ExtraLuggageFee = 10
        };

        bool result = _flightAccess.Update(flight);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Write_Returns_False_When_Database_Insert_Fails()
    {
        var flight = new FlightModel
        {
            TailNumber = "Papieren vliegtuig 1",
            Origin = "Amsterdam",
            Destination = "London",
            TakeOffTime = DateTime.Now.AddHours(2),
            ArrivalTime = DateTime.Now,
            LegroomFee = 20,
            DefaultPrice = 100,
            MealPrice = 10,
            ChosenSeatFee = 5,
            ExtraLuggageFee = 15
        };

        bool result = _flightAccess.Write(flight);

        Assert.IsFalse(result);
    }
}