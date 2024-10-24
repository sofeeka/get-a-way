namespace get_a_way.Entities;

[Serializable]
public class Trip
{
    public static List<Trip> Extent = new List<Trip>();

    public Trip()
    {
    }

    public Trip(Account account, DateTime date, TripType tripType, List<string> pictures, string description)
    {
        this.account = account;
        this.date = date;
        this.tripType = tripType;
        this.pictures = pictures;
        this.description = description;
    }

    public Account account { get; set; }
    public DateTime date { get; set; }
    public TripType tripType { get;   set; }
    public List<String> pictures { get;   set; }
    public String description { get;   set; }
}