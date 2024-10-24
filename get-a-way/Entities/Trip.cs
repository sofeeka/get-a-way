namespace get_a_way.Entities;

public class Trip : Account
{
    public Trip(long id, string username, string password, string email, DateTime date, TripType tripType, List<string> pictures, string description) : base(id, username, password, email)
    {
        this.date = date;
        this.tripType = tripType;
        this.pictures = pictures;
        this.description = description;
    }

    public DateTime date { get; private set; }
    public TripType tripType { get; private set; }
    public List<String> pictures { get; private set; }
    public String description { get; private set; }
}