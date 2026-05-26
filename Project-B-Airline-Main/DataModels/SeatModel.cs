public class SeatModel
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int FlightID { get; set;}
    public string SeatNumber { get; set; }
    public string Class { get; set; }
    public bool ExtraLegroom { get; set; }
    public bool OnflightMeal { get; set; }
    public bool ExtraLuggage { get; set; }
}