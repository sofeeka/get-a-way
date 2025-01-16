using System.Text.RegularExpressions;
using System.Xml.Serialization;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Places;

[Serializable]
[XmlInclude(typeof(Accommodation.Accommodation))]
[XmlInclude(typeof(Eatery.Eatery))]
[XmlInclude(typeof(Attractions.Attraction))]
[XmlInclude(typeof(Shop.Shop))]
public abstract class Place : IExtent<Place>
{
    private static List<Place> _extent = new List<Place>();

    private static long IdCounter = 0;

    private long _id;
    private string _name;
    private string _location;
    private List<String> _pictureUrls; // todo addPicture()
    private DateTime _openTime;
    private DateTime _closeTime;
    private PriceCategory _priceCategory;
    private bool _petFriendly;
    private bool _openedAtNight;
    private bool _isDummy;

    private HashSet<OwnerAccount> _owners;
    private HashSet<Trip.Trip> _trips;
    private HashSet<Review.Review> _reviews;

    // role-specific attributes
    private MultiAspectType _multiAspectType;
    private List<Currency>? _currencies;
    private List<Language>? _languages;
    private Country? _country;
    private bool _localLanguageOnly;
    private static Currency? _currency = Places.Currency.LOCAL;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = ValidateName(value);
    }

    public string Location
    {
        get => _location;
        set => _location = ValidateLocation(value);
    }

    public List<String> PictureUrls
    {
        get => _pictureUrls;
        set => _pictureUrls = ValidatePictureUrls(value);
    }

    public DateTime OpenTime
    {
        get => _openTime;
        set
        {
            _openTime = value;
            SetOpenedAtNight(); // recalculate when OpenTime is updated
        }
    }

    public DateTime CloseTime
    {
        get => _closeTime;
        set
        {
            _closeTime = value;
            SetOpenedAtNight(); // recalculate when CloseTime is updated
        }
    }

    public PriceCategory PriceCategory
    {
        get => _priceCategory;
        set => _priceCategory = value;
    }

    public bool PetFriendly
    {
        get => _petFriendly;
        set => _petFriendly = value;
    }

    public bool OpenedAtNight
    {
        get => _openedAtNight;
        set => _openedAtNight = value;
    }

    public bool IsDummy
    {
        get => _isDummy;
        set => _isDummy = value;
    }

    public bool LocalLanguageOnly
    {
        get => _localLanguageOnly;
        set => _localLanguageOnly = value;
    }

    public List<Currency>? Currencies
    {
        get => _currencies;
        set => _currencies = value;
    }

    public List<Language>? Languages
    {
        get => _languages;
        set => _languages = value;
    }

    public Country? Country
    {
        get => _country;
        set => _country = value ?? Places.Country.None;
    }

    public bool Active => _owners.Count > 0;

    public HashSet<OwnerAccount> Owners => new HashSet<OwnerAccount>(_owners);

    [XmlArray("Reviews")]
    [XmlArrayItem("Review")]
    public List<Review.Review> Reviews => new List<Review.Review>(_reviews);

    [XmlArray("Trips")]
    [XmlArrayItem("Trip")]
    public List<Trip.Trip> Trips => new List<Trip.Trip>(_trips);

    public Place()
    {
        _trips = new HashSet<Trip.Trip>();
        _owners = new HashSet<OwnerAccount>();
        _reviews = new HashSet<Review.Review>();
    }

    protected Place(HashSet<OwnerAccount> owners, string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, bool isDummy = false, List<Currency>? currencies = null,
        List<Language>? languages = null, Country country = Places.Country.None) : this(owners, name, location,
        openTime, closeTime,
        priceCategory, petFriendly, isDummy)
    {
        _multiAspectType = MultiAspectType.International;
        Currencies = ValidateCurrencies(currencies);
        Languages = ValidateLanguages(languages);
        Country = ValidateCountry(country);
    }

    protected Place(HashSet<OwnerAccount> owners, string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, bool isDummy = false, bool localLanguageOnly = true) : this(
        owners, name, location, openTime, closeTime,
        priceCategory, petFriendly, isDummy)
    {
        _multiAspectType = MultiAspectType.Local;
        _localLanguageOnly = localLanguageOnly;
    }

    protected Place(HashSet<OwnerAccount> owners, string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, bool isDummy = false) : this()
    {
        Name = name;
        Location = location;
        OpenTime = openTime;
        CloseTime = closeTime;
        PriceCategory = priceCategory;
        PetFriendly = petFriendly;
        IsDummy = isDummy;
        SetOpenedAtNight();

        AddOwners(owners);

        ID = ++IdCounter;
        AddInstanceToExtent(this);
    }

    private void SetOpenedAtNight()
    {
        TimeSpan nightStart = new TimeSpan(22, 0, 0);
        TimeSpan nightEnd = new TimeSpan(6, 0, 0);

        TimeSpan openTime = OpenTime.TimeOfDay;
        TimeSpan closeTime = CloseTime.TimeOfDay;

        bool opensDuringNight = (openTime < nightEnd || openTime > nightStart);
        bool closesDuringNight = (closeTime > nightStart || closeTime < nightEnd);

        OpenedAtNight = opensDuringNight || closesDuringNight;
    }

    private string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 40)
            throw new InvalidAttributeException("Name must be at least 3 characters long.");
        return name;
    }

    //todo validate through API
    private string ValidateLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new InvalidAttributeException("Location must not be null or white space");
        return location;
    }

    private List<string> ValidatePictureUrls(List<string> urls)
    {
        if (urls == null)
            throw new InvalidAttributeException("Pictures list cannot be null.");

        if (urls.Count > 10)
            throw new InvalidAttributeException("Pictures list cannot contain more than 10 images.");

        if (urls.Any(url => string.IsNullOrWhiteSpace(url) || !IsValidImageUrl(url)))
            throw new InvalidPictureUrlException();

        return urls;
    }

    private List<Language>? ValidateLanguages(List<Language>? languages)
    {
        if (languages == null)
            throw new NullReferenceException(nameof(languages));

        if (languages.Count == 0)
            throw new InvalidOperationException("Languages count must be greater than 0.");

        return languages;
    }

    private List<Currency> ValidateCurrencies(List<Currency>? currencies)
    {
        if (currencies == null)
            throw new NullReferenceException(nameof(currencies));

        if (currencies.Count == 0)
            throw new InvalidOperationException("Countries count must be greater than 0.");

        return currencies;
    }

    private Country ValidateCountry(Country country)
    {
        if (country == Places.Country.None)
            throw new NullReferenceException("Country cannot be null");
        return country;
    }

    private bool IsValidImageUrl(string url)
    {
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
    }

    private void AddOwners(HashSet<OwnerAccount> owners)
    {
        if (owners == null)
            throw new ArgumentNullException(nameof(owners), "Owners list cannot be null.");

        foreach (OwnerAccount owner in owners)
            if (owner == null)
                throw new ArgumentNullException(nameof(owner), "Owner cannot be null.");

        foreach (OwnerAccount owner in owners)
            AddOwner(owner);
    }

    public void AddOwner(OwnerAccount owner)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner), "Cannot add null owner to place.");

        if (_owners.Add(owner))
            owner.AddPlace(this);
    }

    public void RemoveOwner(OwnerAccount owner)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner), "Cannot remove null owner from place.");

        if (_owners.Remove(owner))
            owner.RemovePlace(this);
    }

    private void RemoveOwners()
    {
        foreach (OwnerAccount owner in Owners)
            RemoveOwner(owner);
    }

    public void Archive()
    {
        RemoveOwners();
    }

    public void AddTrip(Trip.Trip trip)
    {
        if (trip == null)
            throw new InvalidAttributeException("Place cannot be added to a null trip. Trip cannot be null");
        _trips.Add(trip);
    }

    public void RemoveTrip(Trip.Trip trip)
    {
        if (trip == null)
            throw new InvalidAttributeException("Place cannot be removed from a null trip. Trip cannot be null");
        _trips.Remove(trip);
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

    public static List<Place> GetExtentCopy()
    {
        return new List<Place>(_extent);
    }

    public static void AddInstanceToExtent(Place instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(Place instance)
    {
        _extent.Remove(instance);
    }

    public static List<Place> GetExtent()
    {
        return _extent;
    }

    public static void ResetExtent()
    {
        _extent.Clear();
        IdCounter = 0;
    }

    public override string ToString()
    {
        return $"Place Details:\n" +
               $"ID: {ID}\n" +
               $"Name: {Name}\n" +
               $"Location: {Location}\n" +
               $"Pictures: {GetPictureUrls()}\n" +
               $"Open Time: {OpenTime:HH:mm}\n" +
               $"Close Time: {CloseTime:HH:mm}\n" +
               $"Price Category: {PriceCategory}\n" +
               $"Pet Friendly: {(PetFriendly ? "Yes" : "No")}\n" +
               $"Opened At Night: {(OpenedAtNight ? "Yes" : "No")}\n" +
               $"Number of Reviews: {Reviews?.Count ?? 0}\n";
    }

    private string GetPictureUrls()
    {
        if (PictureUrls == null || PictureUrls.Count == 0)
            return "No pictures available";
        return string.Join(", ", PictureUrls);
    }
}