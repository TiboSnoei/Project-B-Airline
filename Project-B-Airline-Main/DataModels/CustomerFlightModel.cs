public class CustomerFlightModel
{
    public int UserID { get; set; }
    public int FlightID { get; set;}
    public string Seat { get; set; }
    public bool SeatChosen { get; set; }
    public bool ExtraLegroom { get; set; }
    public bool OnflightMeal { get; set; }
    public bool ExtraLuggage { get; set; }
}