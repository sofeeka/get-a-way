namespace get_a_way.Entities;

[Serializable]
public class Trip
{
    public static List<Trip> Extent = new List<Trip>();

    public Account Account { get; set; }
    public DateTime Date { get; set; }
    public TripType TripType { get; set; }
    public List<String> Pictures { get; set; }
    public String Description { get; set; }

    public Trip()
    {
    }

    public Trip(Account account, DateTime date, TripType tripType, List<string> pictures, string description)
    {
        Account = account;
        Date = date;
        TripType = tripType;
        Pictures = pictures;
        Description = description;
    }
}