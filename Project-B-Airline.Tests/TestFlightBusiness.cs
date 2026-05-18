using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class FlightLogicTests
{
    private FlightLogic _flightLogic;

    [TestInitialize]
    public void Setup()
    {
        var seeder = new DatabaseSeeder();
        seeder.EnsureDatabase();

        _flightLogic = new FlightLogic();
    }

    [TestMethod]
    public void CreateFlight_Returns_False_When_Arrival_Before_Takeoff()
    {
        var flight = new FlightModel
        {
            TailNumber = "HR101",
            Origin = "Amsterdam",
            Destination = "London",
            TakeOffTime = DateTime.Now.AddHours(5),
            ArrivalTime = DateTime.Now.AddHours(2)
        };

        var result = _flightLogic.CreateFlight(flight);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateFlight_Returns_False_When_Destination_Is_Empty()
    {
        var flight = new FlightModel
        {
            TailNumber = "HR101",
            Origin = "Amsterdam",
            Destination = "",
            TakeOffTime = DateTime.Now.AddHours(1),
            ArrivalTime = DateTime.Now.AddHours(3)
        };

        var result = _flightLogic.CreateFlight(flight);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateFlight_Returns_False_When_Origin_Is_Empty()
    {
        var flight = new FlightModel
        {
            TailNumber = "HR101",
            Origin = "",
            Destination = "London",
            TakeOffTime = DateTime.Now.AddHours(1),
            ArrivalTime = DateTime.Now.AddHours(3)
        };

        var result = _flightLogic.CreateFlight(flight);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateFlight_Returns_False_For_Invalid_TailNumber()
    {
        var flight = new FlightModel
        {
            TailNumber = "HR999",
            Origin = "Amsterdam",
            Destination = "London",
            TakeOffTime = DateTime.Now.AddHours(2),
            ArrivalTime = DateTime.Now.AddHours(4)
        };

        var result = _flightLogic.CreateFlight(flight);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateFlight_Returns_True_For_Valid_Flight()
    {
        var flight = new FlightModel
        {
            TailNumber = "HR101",
            Origin = "Amsterdam",
            Destination = "London",
            TakeOffTime = DateTime.Now.AddHours(2),
            ArrivalTime = DateTime.Now.AddHours(5),
            DefaultPrice = 150,
            MealPrice = 10,
            ChosenSeatFee = 5,
            ExtraLuggageFee = 20
        };

        var result = _flightLogic.CreateFlight(flight);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void EditFlight_Returns_True_When_Valid_Flight()
    {
        var flight = _flightLogic.GetAll().FirstOrDefault();

        Assert.IsNotNull(flight);

        flight.DefaultPrice += 10;

        var result = _flightLogic.EditFlight(flight);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GetAll_Returns_List_Of_Flights()
    {
        var result = _flightLogic.GetAll();

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(List<FlightModel>));
    }
}