using System.Text.RegularExpressions;
using System.Xml.Serialization;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Trip;

[Serializable]
public class Trip : IExtent<Trip>
{
    private static List<Trip> _extent = new List<Trip>();

    private static long IdCounter = 0;
    private long _id;
    private DateTime _date;
    private TripType _tripType;
    private List<String> _pictureUrls; // todo addPicture() and validate
    private String _description;

    private TravelerAccount _traveler; 
    private HashSet<Place> _places;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    [XmlIgnore] public TravelerAccount Traveler => _traveler;

    public DateTime Date
    {
        get => _date;
        set => _date = ValidateDate(value);
    }

    public TripType TripType
    {
        get => _tripType;
        set => _tripType = value;
    }

    public List<String> PictureUrls
    {
        get => _pictureUrls;
        set => _pictureUrls = ValidatePictureUrls(value);
    }

    public String Description
    {
        get => _description;
        set => _description = ValidateDescription(value);
    }

    [XmlArray("Places")]
    [XmlArrayItem("Place")]
    public HashSet<Place> Places => new HashSet<Place>(_places); // todo test that returns a copy

    public Trip()
    {
        _places = new HashSet<Place>();
    }

    public Trip(TravelerAccount traveler, HashSet<Place> places, DateTime date, TripType tripType, string description) : this()
    {
        Date = date;
        TripType = tripType;
        PictureUrls = new List<string>();
        Description = description;

        AddPlaces(places);
        
        _traveler = traveler ?? throw new ArgumentNullException(nameof(traveler));
        _traveler.AddTrip(this); // composition (reverse connection)

        ID = ++IdCounter;
        AddInstanceToExtent(this);
    }

    private DateTime ValidateDate(DateTime date)
    {
        if (date > DateTime.Now)
            throw new InvalidAttributeException("Date cannot be in the future.");
        return date;
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

    private bool IsValidImageUrl(string url)
    {
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
    }

    private string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidAttributeException("Description cannot be empty.");
        if (description.Length > 1000)
            throw new InvalidAttributeException("Description cannot exceed 1000 characters.");
        return description;
    }

    private HashSet<Place> ValidatePlaces(HashSet<Place> places)
    {
        if (places == null)
            throw new ArgumentNullException(nameof(places), "Places list cannot be null.");

        if (places.Count == 0)
            throw new InvalidAttributeException("At least 1 place must be added to a trip.");
        
        foreach (Place place in places)
            if(place == null)
                throw new ArgumentNullException(nameof(place), "Place cannot be null.");

        return places;
    }

    public void AddPlaces(HashSet<Place> places)
    {
        if (places == null)
            throw new ArgumentNullException(nameof(places), "Places list added to a trip cannot be null.");

        if (places.Count < 1)
            throw new InvalidAttributeException("Trip must consist of at least one place.");
        
        foreach(Place place in places)
            AddPlace(place);
    }

    public void AddPlace(Place place)
    {
        if (place == null)
            throw new ArgumentNullException(nameof(place), "Place added to a trip cannot be null.");

        if (_places.Add(place)) // check if not already present
            place.AddTrip(this); // reverse connection
    }

    public void RemovePlace(Place place)
    {
        if (place == null)
            throw new InvalidAttributeException("Place removed from a trip cannot be null");

        if (_places.Remove(place)) // check if present
            place.RemoveTrip(this); // reverse connection
    }

    public void RemoveTraveler(TravelerAccount account)
    {
        _traveler = null;
    }

    public static List<Trip> GetExtentCopy()
    {
        return new List<Trip>(_extent);
    }

    public static void AddInstanceToExtent(Trip instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(Trip instance)
    {
        _extent.Remove(instance);
    }

    public static List<Trip> GetExtent()
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
        return $"Trip ID: {ID}\n" +
               // $"Account: {Account.Username}\n" +
               $"Date: {Date:yyyy-MM-dd}\n" +
               $"Trip Type: {TripType}\n" +
               $"Pictures: {GetPictureUrls()}\n" +
               $"Description: {(string.IsNullOrWhiteSpace(Description) ? "No description provided" : Description)}\n";
    }

    private string GetPictureUrls()
    {
        if (PictureUrls == null || PictureUrls.Count == 0)
            return "No pictures available";
        return string.Join(", ", PictureUrls);
    }
}