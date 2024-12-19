using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Entities.Trip;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.TripTests;

public class TripTests
{
    private static Trip _validTrip;
    private static Place _validPlace;

    private static readonly Account ValidAccount =
        new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl");

    private static readonly DateTime Now = DateTime.Now;
    private static readonly DateTime ValidDate = new DateTime(Now.Year - 1, Now.Month, Now.Day);
    private const string ValidDescription = "Valid Description";
    private const string ValidPictureUrl = "https://valid/image.png";
    private List<string> _validPictureUrls = new List<string>();

    [SetUp]
    public void SetUpEnvironment()
    {
        Trip.ResetExtent();
        Account.ResetExtent();
        _validTrip = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);

        _validPictureUrls = new List<string>();
        _validPictureUrls.Add(ValidPictureUrl);
        _validPictureUrls.Add(ValidPictureUrl);

        _validTrip.PictureUrls = _validPictureUrls;

        _validPlace = new Shop("ValidName", "ValidLocation", DateTime.Today.AddHours(8), DateTime.Today.AddHours(21),
            PriceCategory.Moderate, true, ShopType.Supermarket, true);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var trip = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);

        // ID == 2 because _valid.ID == 1
        Assert.That(trip.ID, Is.EqualTo(2));

        Assert.That(trip.Date, Is.EqualTo(ValidDate));
        Assert.That(trip.TripType, Is.EqualTo(TripType.Friends));
        Assert.That(trip.PictureUrls, Is.Empty);
        Assert.That(trip.Description, Is.EqualTo(ValidDescription));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);
        var test2 = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
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
        Assert.That(() => _validTrip.Description, Is.EqualTo(ValidDescription));

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

        string hugeDescription = """
                                 Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula
                                 eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient
                                 montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu,
                                 pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel,
                                 aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis
                                 vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras
                                 dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo
                                 ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in,
                                 viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet.
                                 Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper
                                 ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum
                                 rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc,
                                 blandit vel, luctus pulvinar, hendre
                                 """;

        Assert.That(() => _validTrip.Description = hugeDescription, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validTrip.Description, Is.EqualTo(ValidDescription));
    }

    [Test]
    public void AddPlace_ValidPlace_AddsPlace()
    {
        _validTrip.AddPlace(_validPlace);
        Assert.That(_validTrip.Places.Contains(_validPlace));
        Assert.That(_validPlace.Trips.Contains(_validTrip));
    }

    [Test]
    public void AddPlace_DuplicatePlace_DoesNotAddPlaceAgain()
    {
        _validTrip.AddPlace(_validPlace);
        var count = _validTrip.Places.Count;

        _validTrip.AddPlace(_validPlace);
        Assert.True(_validTrip.Places.Count == count);
    }

    [Test]
    public void AddPlace_NullPlace_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validTrip.AddPlace(null), Throws.TypeOf<InvalidAttributeException>());
    }
    
    [Test]
    public void RemovePlace_AddedPlace_RemovesPlace()
    {
        _validTrip.AddPlace(_validPlace);

        _validTrip.RemovePlace(_validPlace);
        Assert.That(_validTrip.Places.Contains(_validPlace), Is.False);
        Assert.That(_validPlace.Trips.Contains(_validTrip), Is.False);
    }
    
    [Test]
    public void RemovePlace_NullPlace_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validTrip.RemovePlace(null), Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void RemovePlace_NotAddedPlace_DoesNothing()
    {
        Assert.That(_validTrip.Places.Contains(_validPlace), Is.False);

        _validTrip.RemovePlace(_validPlace);
        Assert.That(_validTrip.Places.Contains(_validPlace), Is.False);
        Assert.That(_validPlace.Trips.Contains(_validTrip), Is.False);
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Trip.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new Trip(ValidAccount, ValidDate, TripType.Family, ValidDescription);
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