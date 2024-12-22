using get_a_way.Entities.Places;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Accounts;

[Serializable]
public class TravelerAccount : Account
{
    private Dictionary<String, Place> _accessiblePlacesByLocation;

    public Dictionary<String, Place> AccessiblePlacesByLocation => new Dictionary<string, Place>(_accessiblePlacesByLocation);
    public TravelerAccount()
    {
    }

    public TravelerAccount(string username, string password, string email) : base(username,
        password, email)
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

    public override string ToString()
    {
        return base.ToString();
    }
}