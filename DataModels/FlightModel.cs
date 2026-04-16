using System;

public class FlightModel
{
    public int FlightId { get;} //pk
    public string TailNumber { get; set; } = string.Empty; //fk
    public string Destination { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public DateTime TakeOffTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int LegroomFee { get; set; }
    public int DefaultPrice { get; set; }
    public int MealPrice { get; set; }
    public int ChosenSeatFee { get; set; }
    public int ExtraLuggageFee { get; set; }
}