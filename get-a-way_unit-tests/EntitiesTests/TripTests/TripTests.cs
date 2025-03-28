using get_a_way;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Entities.Trip;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.TripTests;

public class TripTests
{
    private static Trip _validTrip;

    private static readonly TravelerAccount ValidTraveler =
        new TravelerAccount("ValidTraveler", "Password123", "valid.email@pjwstk.edu.pl");

    private static readonly DateTime Now = DateTime.Now;
    private static readonly DateTime ValidDate = new DateTime(Now.Year - 1, Now.Month, Now.Day);
    private const string ValidDescription = "Valid Description";
    private const string ValidPictureUrl = "https://valid/image.png";
    private List<string> _validPictureUrls = new List<string>();

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("ValidOwnerName", "ValidPassword123", "validemail@pjwstk.edu.pl");

    private readonly HashSet<Place> _validPlaces = new HashSet<Place>();

    private static readonly Place DummyValidPlace = new Shop(Owners, "ValidDummyName", "ValidLocation",
        DateTime.Today.AddHours(8), DateTime.Today.AddHours(21), PriceCategory.Moderate, true,
        ShopType.Supermarket, true);

    private static readonly Place ValidPlace = new Shop(Owners, "ValidName", "ValidLocation",
        DateTime.Today.AddHours(7), DateTime.Today.AddHours(20), PriceCategory.Expensive, true,
        ShopType.Mall, false);


    [SetUp]
    public void SetUpEnvironment()
    {
        Trip.ResetExtent();
        Place.ResetExtent();

        Owners.Add(DummyOwner);
        _validPlaces.Add(DummyValidPlace);
        _validTrip = new Trip(ValidTraveler, _validPlaces, ValidDate, TripType.Friends, ValidDescription);

        _validPictureUrls = new List<string>();
        _validPictureUrls.Add(ValidPictureUrl);
        _validPictureUrls.Add(ValidPictureUrl);

        _validTrip.PictureUrls = _validPictureUrls;
    }

    [TearDown]
    public void TeardownEnvironment()
    {
        foreach(Place place in _validTrip.Places)
            _validTrip.RemovePlace(place);
    }
    
    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var trip = new Trip(ValidTraveler, _validPlaces, ValidDate, TripType.Friends, ValidDescription);

        // ID == 2 because _valid.ID == 1
        Assert.That(trip.ID, Is.EqualTo(2));

        // associations
        Assert.That(trip.Traveler, Is.EqualTo(ValidTraveler));
        Assert.That(trip.Places, Is.EqualTo(_validPlaces));

        Assert.That(trip.Date, Is.EqualTo(ValidDate));
        Assert.That(trip.TripType, Is.EqualTo(TripType.Friends));
        Assert.That(trip.PictureUrls, Is.Empty);
        Assert.That(trip.Description, Is.EqualTo(ValidDescription));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Trip(ValidTraveler, _validPlaces, ValidDate, TripType.Friends, ValidDescription);
        var test2 = new Trip(ValidTraveler, _validPlaces, ValidDate, TripType.Friends, ValidDescription);

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_NullPLaces_ThrowsArgumentNullException()
    {
        Assert.That(() => { new Trip(ValidTraveler, null, ValidDate, TripType.Friends, ValidDescription); },
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Constructor_EmptyPLaces_ThrowsInvalidAttributeException()
    {
        Assert.That(
            () => { new Trip(ValidTraveler, new HashSet<Place>(), ValidDate, TripType.Friends, ValidDescription); },
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_NullPLace_ThrowsArgumentNullException()
    {
        HashSet<Place> placesWithNullPlace = new HashSet<Place>();
        placesWithNullPlace.Add(null);

        Assert.That(
            () => { new Trip(ValidTraveler, placesWithNullPlace, ValidDate, TripType.Friends, ValidDescription); },
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Setter_ValidDate_SetsDate()
    {
        var anotherValidDate = new DateTime(Now.Year - 2, Now.Month, Now.Day);
        _validTrip.Date = anotherValidDate;
        Assert.That(_validTrip.Date, Is.EqualTo(anotherValidDate));
    }

    [Test]
    public void Setter_InvalidDate_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validTrip.Date = new DateTime(Now.Year + 1, Now.Month, Now.Day),
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.Date, Is.EqualTo(ValidDate));
    }

    [Test]
    public void Setter_ValidPictureUrls_SetsPictureUrls()
    {
        _validTrip.PictureUrls = _validPictureUrls;
        Assert.That(_validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));
    }

    [Test]
    public void Setter_InvalidPictureUrlsList_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validTrip.PictureUrls = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));

        List<string> urls = new List<string>();

        for (int i = 0; i < 12; i++)
            urls.Add(ValidPictureUrl);

        Assert.That(() => _validTrip.PictureUrls = urls, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));
    }

    [Test]
    public void Setter_InvalidPictureUrl_ThrowsInvalidAttributeException()
    {
        List<string> urls = new List<string>();

        urls.Add(null);
        Assert.That(() => _validTrip.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add("");
        Assert.That(() => _validTrip.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add(" ");
        Assert.That(() => _validTrip.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add("invalid path");
        Assert.That(() => _validTrip.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add("https://not/image/extention");
        Assert.That(() => _validTrip.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _validTrip.PictureUrls, Is.EqualTo(_validPictureUrls));
    }

    [Test]
    public void Setter_ValidDescription_SetsDescription()
    {
        _validTrip.Description = "Some proper new description.";
        Assert.That(_validTrip.Description, Is.EqualTo("Some proper new description."));
    }

    [Test]
    public void Setter_InvalidDescription_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validTrip.Description = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.Description, Is.EqualTo(ValidDescription));

        Assert.That(() => _validTrip.Description = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.Description, Is.EqualTo(ValidDescription));

        Assert.That(() => _validTrip.Description = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.Description, Is.EqualTo(ValidDescription));

        var hugeDescription = new string('a', 1001);

        Assert.That(() => _validTrip.Description = hugeDescription, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.Description, Is.EqualTo(ValidDescription));
    }

    [Test]
    public void AddPlaces_ValidPlaces_AddsPlaces()
    {
        HashSet<Place> places = new HashSet<Place>();
        places.Add(ValidPlace);

        _validTrip.AddPlaces(places);

        Assert.That(_validTrip.Places, Does.Contain(ValidPlace));
        Assert.That(ValidPlace.Trips, Does.Contain(_validTrip));
    }

    [Test]
    public void AddPlaces_NullPlaces_ThrowsArgumentNullException()
    {
        Assert.That(() => _validTrip.AddPlaces(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddPlaces_PlacesWithNullPlace_ThrowsArgumentNullException()
    {
        HashSet<Place> places = new HashSet<Place>();
        places.Add(null);

        Assert.That(() => _validTrip.AddPlaces(places), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddPlace_ValidPlace_AddsPlace()
    {
        _validTrip.AddPlace(ValidPlace);
        Assert.That(_validTrip.Places, Does.Contain(ValidPlace));
        Assert.That(ValidPlace.Trips, Does.Contain(_validTrip));
    }

    [Test]
    public void AddPlace_DuplicatePlace_DoesNotAddPlaceAgain()
    {
        _validTrip.AddPlace(ValidPlace);
        var count = _validTrip.Places.Count;

        _validTrip.AddPlace(ValidPlace);
        Assert.True(_validTrip.Places.Count == count);
    }

    [Test]
    public void AddPlace_NullPlace_ThrowsArgumentNullException()
    {
        Assert.That(() => _validTrip.AddPlace(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void RemovePlace_AddedPlace_RemovesPlace()
    {
        _validTrip.AddPlace(ValidPlace);
        _validTrip.RemovePlace(ValidPlace);

        Assert.False(_validTrip.Places.Contains(ValidPlace));
        Assert.False(ValidPlace.Trips.Contains(_validTrip));
    }

    [Test]
    public void RemovePlace_NullPlace_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validTrip.RemovePlace(null), Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void RemovePlace_NotAddedPlace_DoesNothing()
    {
        Assert.That(_validTrip.Places, Does.Contain(DummyValidPlace));
        Assert.That(_validTrip.Places, Does.Not.Contain(ValidPlace));

        _validTrip.RemovePlace(ValidPlace);

        Assert.That(_validTrip.Places, Does.Contain(DummyValidPlace));
        Assert.That(DummyValidPlace.Trips, Does.Contain(_validTrip));

        Assert.That(_validTrip.Places, Does.Not.Contain(ValidPlace));
        Assert.That(ValidPlace.Trips, Does.Not.Contain(_validTrip));
    }

    [Test]
    public void GetPlace_ReturnsCopy()
    {
        HashSet<Place> places = _validTrip.Places;
        int count = places.Count;

        places.Clear();
        Assert.That(_validTrip.Places.Count == count);
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Trip.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new Trip(ValidTraveler, _validPlaces, ValidDate, TripType.Friends, ValidDescription);
        Assert.That(Trip.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Trip.GetExtentCopy().Count;
        Trip.RemoveInstanceFromExtent(_validTrip);
        Assert.That(Trip.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(Trip.GetExtentCopy() != Trip.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Trip.ResetExtent();
        Assert.That(Trip.GetExtent().Count, Is.EqualTo(0));
    }
}