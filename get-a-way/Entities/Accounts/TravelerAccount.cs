using System.Xml.Serialization;

namespace get_a_way.Entities.Accounts;

[Serializable]
public class TravelerAccount : Account
{
    private HashSet<Trip.Trip> _trips = new HashSet<Trip.Trip>();

    [XmlArray("Trips")]
    [XmlArrayItem("Trip")]
    public HashSet<Trip.Trip> Trips => new HashSet<Trip.Trip>(_trips);

    public TravelerAccount()
    {
    }

    public TravelerAccount(TravelerAccount traveler)
        : this(traveler.Username, traveler.Password, traveler.Email)
    {
    }

    public TravelerAccount(string username, string password, string email)
        : base(username, password, email)
    {
    }

    ~TravelerAccount()
    {
        // composition (when account gets deleted, all their trips are deleted too)
        foreach (Trip.Trip trip in _trips)
            RemoveTrip(trip);
    }

    public void AddTrip(Trip.Trip trip)
    {
        if (trip == null)
            throw new ArgumentNullException(nameof(trip), "Null trip cannot be added to traveller account.");
        _trips.Add(trip);
    }

    public void RemoveTrip(Trip.Trip trip)
    {
        if (trip == null)
            throw new ArgumentNullException(nameof(trip), "Null trip cannot be removed from traveller account.");
        _trips.Remove(trip);
        trip.RemoveTraveler(this); // reverse connection
    }

    public override string ToString()
    {
        return base.ToString();
    }
}