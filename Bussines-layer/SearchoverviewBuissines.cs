class SearchoverviewBuisness
{
    // Fields and Attributes
        public required string Destinationselected { get; set; }
        public required bool Returnflightselected { get; set; }
        public required DateTime Departuredate { get; set; }
        public DateTime Returndate { get; set; }
    
    // Constructor
    public SearchoverviewBuisness(string destinationselected, bool returnflightselected, DateTime departuredate)
    {
        Destinationselected = destinationselected;
        Returnflightselected = returnflightselected;
        Departuredate = departuredate;
    }
}