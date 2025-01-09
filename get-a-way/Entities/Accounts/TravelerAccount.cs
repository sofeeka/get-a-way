using System.Runtime.InteropServices.JavaScript;
using get_a_way.Entities.Places;
using get_a_way.Exceptions;
using System.Xml.Serialization;

namespace get_a_way.Entities.Accounts;

[Serializable]
public class TravelerAccount : Account
{
    private Dictionary<String, Place> _accessiblePlacesByLocation;

    private HashSet<Trip.Trip> _trips = new HashSet<Trip.Trip>();
    private HashSet<Review.Review> _reviews = new HashSet<Review.Review>();

    public Dictionary<String, Place> AccessiblePlacesByLocation =>
        new Dictionary<string, Place>(_accessiblePlacesByLocation);

    [XmlArray("Trips")]
    [XmlArrayItem("Trip")]
    public HashSet<Trip.Trip> Trips => new HashSet<Trip.Trip>(_trips);

    [XmlArray("Reviews")]
    [XmlArrayItem("Review")]
    public HashSet<Review.Review> Reviews => new HashSet<Review.Review>(_reviews);

    public TravelerAccount()
    {
    }

    public TravelerAccount(string username, string password, string email)
        : base(username, password, email)
    {
        _accessiblePlacesByLocation = new Dictionary<string, Place>();
    }

    public void AddPlace(string location, Place place)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new InvalidAttributeException("Location cannot be null or empty");

        if (place == null)
            throw new ArgumentNullException(nameof(place), "Place cannot be null");

        _accessiblePlacesByLocation[location] = place; //add or replace
    }

    public void RemovePlace(string location)
    {
        if (!_accessiblePlacesByLocation.ContainsKey(location))
            throw new KeyNotFoundException($"No place found for location: {location}");

        _accessiblePlacesByLocation.Remove(location);
    }

    public Place GetPlace(string location)
    {
        if (!_accessiblePlacesByLocation.TryGetValue(location, out var place))
            throw new KeyNotFoundException($"No place found for location: {location}");

        return place;
    }

    public List<Place> GetAllPlaces()
    {
        return _accessiblePlacesByLocation.Values.ToList();
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

    public void AddReview(Review.Review review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review), "Cannot add null review.");
        _reviews.Add(review);
    }

    public void RemoveReview(Review.Review review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review), "Cannot remove null review.");
        _reviews.Remove(review);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}