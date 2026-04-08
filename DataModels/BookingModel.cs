public class BookingModel
{
    public int UserId { get; set; }
    public int FlightId { get; set; }
    public string Seat { get; set; }
    public bool SeatChosen { get; set; }
    public bool ExtraLegroom { get; set; }
    public bool OnflightMeal { get; set; }
    public bool ExtraLuggage { get; set; }
}