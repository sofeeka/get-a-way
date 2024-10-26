using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities;

[Serializable]
public class Trip : IExtent<Trip>
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

    public Trip(Account account, DateTime date, TripType tripType, string description)
    {
        Account = account;
        Date = date;
        TripType = tripType;
        Pictures = new List<string>();
        Description = description;
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