using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Trip;

[Serializable]
public class Trip : IExtent<Trip>
{
    public static List<Trip> Extent = new List<Trip>();

    private static long IdCounter = 0;
    public long ID { get; set; }
    public Account Account { get; set; }
    public DateTime Date { get; set; }
    public TripType TripType { get; set; }
    public List<String> Pictures { get; set; }
    public String Description { get; set; }

    public Trip()
    {
    }

    public Trip(Account account, DateTime date, TripType tripType, string description)
    {
        ID = ++IdCounter;
        Account = account;
        Date = date;
        TripType = tripType;
        Pictures = new List<string>();
        Description = description;
        
        AddInstanceToExtent(this);
    }

    public List<Trip> GetExtentCopy()
    {
        return new List<Trip>(Extent);
    }

    public void AddInstanceToExtent(Trip instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(Trip instance)
    {
        Extent.Remove(instance);
    }
}