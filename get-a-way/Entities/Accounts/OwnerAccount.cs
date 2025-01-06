using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Accounts;

[Serializable]
public class OwnerAccount : Account
{
    private double _tax = 15;
    private HashSet<Place> _places = new HashSet<Place>();

    public double Tax
    {
        get => _tax;
        set => _tax = ValidateTax(value);
    }

    public HashSet<Place> Places => new HashSet<Place>(_places);

    private static double ValidateTax(double value)
    {
        if (value is < 0.0 or > 100.0)
            throw new InvalidAttributeException($"Invalid tax '{value}'. Tax has to be in between 0.0 % and 100.0 %");
        return value;
    }

    public OwnerAccount()
    {
    }

    public OwnerAccount(string username, string password, string email) :
        base(username, password, email)
    {
        HashSet<OwnerAccount> owners = new HashSet<OwnerAccount>();
        owners.Add(this);

        Place dummyPLace = new Shop(owners, "ValidName", "ValidLocation", DateTime.Today.AddHours(8),
            DateTime.Today.AddHours(21), PriceCategory.Moderate, true, ShopType.Supermarket, true, isDummy: true);
        
        _places = new HashSet<Place>();
        _places.Add(dummyPLace);
    }

    public OwnerAccount(HashSet<Place> places, string username, string password, string email) :
        base(username, password, email)
    {
        _places = ValidatePlaces(places);
    }

    private HashSet<Place> ValidatePlaces(HashSet<Place> places)
    {
        if (places == null)
            throw new ArgumentNullException(nameof(places), "Places list cannot be null.");

        if (places.Count == 0)
            throw new InvalidAttributeException("At least 1 place must be added to an owner Account.");

        foreach (Place place in places)
            if (place == null)
                throw new ArgumentNullException(nameof(place), "Place cannot be null.");

        return places;
    }

    public void AddPlace(Place place)
    {
        if(OwnerHasDummyPlace())
            _places.Clear();
        
        if (place == null)
            throw new ArgumentNullException(nameof(place),
                "Place cannot be null when trying to add place to owned places.");

        if (_places.Add(place))
            place.AddOwner(this);
    }

    private bool OwnerHasDummyPlace()
    {
        return _places.Count == 1 && _places.Single().IsDummy;
    }

    public void RemovePlace(Place place)
    {
        if (place == null)
            throw new ArgumentNullException(nameof(place),
                "Place cannot be null when trying to remove place from owned places.");

        if (place.Owners.Contains(this) && _places.Remove(place))
            place.RemoveOwner(this);
    }

    public override string ToString()
    {
        return base.ToString() + $"Tax: {Tax}\n";
    }
}