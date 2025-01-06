using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests;

public class PlaceTests
{
    private class TestPlace(
        HashSet<OwnerAccount> owners,
        string name,
        string location,
        DateTime openTime,
        DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly) : Place(owners, name, location, openTime, closeTime, priceCategory, petFriendly);

    private TestPlace _valid;

    private const string ValidName = "ValidName";
    private const string ValidLocation = "ValidLocation";
    private static readonly DateTime ValidOpenTime = DateTime.Today.AddHours(8);
    private static readonly DateTime ValidCloseTime = DateTime.Today.AddHours(21);
    private const PriceCategory ValidPriceCategory = PriceCategory.Moderate;
    private const bool ValidPetFriendly = true;

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("PlaceOwner", "ValidPassword123", "validemail@pjwstk.edu.pl");

    [SetUp]
    public void SetUpEnvironment()
    {
        Owners.Add(DummyOwner);
        _valid = new TestPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime, ValidPriceCategory,
            ValidPetFriendly);
    }

    [TearDown]
    public void TearDownEnvironment()
    {
        Place.ResetExtent();
        Account.ResetExtent();
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var place = new TestPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime, PriceCategory.Budget,
            false);

        Assert.That(place.Name, Is.EqualTo(ValidName));
        Assert.That(place.Location, Is.EqualTo(ValidLocation));
        Assert.That(place.OpenTime, Is.EqualTo(ValidOpenTime));
        Assert.That(place.CloseTime, Is.EqualTo(ValidCloseTime));
        Assert.That(place.PriceCategory, Is.EqualTo(PriceCategory.Budget));
        Assert.That(place.PetFriendly, Is.False);
        Assert.That(place.OpenedAtNight, Is.False);
    }

    [Test]
    public void Constructor_NewInstance_IncrementsId()
    {
        var place1 = new TestPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime,
            PriceCategory.Moderate, true);
        var place2 = new TestPlace(Owners, ValidName, ValidLocation, ValidOpenTime.AddHours(1),
            ValidCloseTime.AddHours(2), PriceCategory.Budget, false);

        Assert.That(place2.ID - place1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Setter_ValidName_SetsName()
    {
        _valid.Name = "NewValidName";
        Assert.That(_valid.Name, Is.EqualTo("NewValidName"));
    }

    [Test]
    public void Setter_InvalidName_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Name = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = "iv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = "WayTooLongOfPlaceNameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));
    }

    [Test]
    public void Setter_ValidLocation_SetsLocation()
    {
        _valid.Location = "NewLocation";
        Assert.That(_valid.Location, Is.EqualTo("NewLocation"));
    }

    [Test]
    public void Setter_InvalidLocation_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Location = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Location, Is.EqualTo("ValidLocation"));

        Assert.That(() => _valid.Location = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Location, Is.EqualTo("ValidLocation"));

        Assert.That(() => _valid.Location = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Location, Is.EqualTo("ValidLocation"));
    }

    [Test]
    public void Setter_ValidPictureUrls_ValidUrls_SetsUrls()
    {
        var validUrls = new List<string> { "https://example.com/image1.jpg", "https://example.com/image2.png" };
        _valid.PictureUrls = validUrls;
        Assert.That(_valid.PictureUrls, Is.EqualTo(validUrls));
    }

    [Test]
    public void Setter_InvalidPictureUrls_ThrowsInvalidPictureUrlException()
    {
        Assert.That(() => _valid.PictureUrls = null, Throws.TypeOf<InvalidAttributeException>());

        var tooManyUrls = Enumerable.Repeat("https://example.com/image.jpg", 11).ToList(); // List of 11 URLs
        Assert.That(() => _valid.PictureUrls = tooManyUrls, Throws.TypeOf<InvalidAttributeException>());

        var emptyUrlList = new List<string> { "https://example.com/image.jpg", "" };
        Assert.That(() => _valid.PictureUrls = emptyUrlList, Throws.TypeOf<InvalidPictureUrlException>());

        var whitespaceUrlList = new List<string> { "https://example.com/image.jpg", " " };
        Assert.That(() => _valid.PictureUrls = whitespaceUrlList, Throws.TypeOf<InvalidPictureUrlException>());

        var invalidUrlFormatList = new List<string> { "https://example.com/image.jpg", "invalid_url" };
        Assert.That(() => _valid.PictureUrls = invalidUrlFormatList, Throws.TypeOf<InvalidPictureUrlException>());
    }

    [Test]
    public void Setter_ValidOpenTime_SetsCorrectValue()
    {
        var newOpenTime = DateTime.Today.AddHours(5);
        _valid.OpenTime = newOpenTime;
        Assert.That(_valid.OpenTime, Is.EqualTo(newOpenTime));
        Assert.That(_valid.OpenedAtNight, Is.True);
    }

    [Test]
    public void Setter_ValidCloseTime_SetsCorrectValue()
    {
        var newCloseTime = DateTime.Today.AddHours(20);
        _valid.CloseTime = newCloseTime;
        Assert.That(_valid.CloseTime, Is.EqualTo(newCloseTime));
        Assert.That(_valid.OpenedAtNight, Is.False);
    }

    [Test]
    public void Setter_PriceCategory_SetsCorrectValue()
    {
        var newPriceCategory = PriceCategory.Free;
        _valid.PriceCategory = newPriceCategory;
        Assert.That(_valid.PriceCategory, Is.EqualTo(newPriceCategory));
    }

    [Test]
    public void Setter_PetFriendly_SetsCorrectValue()
    {
        var newPetFriendlyValue = false;
        _valid.PetFriendly = newPetFriendlyValue;
        Assert.That(_valid.PetFriendly, Is.EqualTo(newPetFriendlyValue));
    }

    [Test]
    public void Constructor_NightOpenCloseTimes_SetOpenedAtNightCorrectly()
    {
        var place = new TestPlace(Owners, "Night Cafe", ValidLocation, DateTime.Today.AddHours(21),
            DateTime.Today.AddHours(4), ValidPriceCategory, ValidPetFriendly);
        Assert.That(place.OpenedAtNight, Is.True);
    }

    [Test]
    public void Constructor_DayOpenCloseTimes_SetOpenedAtNightCorrectly()
    {
        var place = new TestPlace(Owners, "Day Cafe", ValidLocation, DateTime.Today.AddHours(8),
            DateTime.Today.AddHours(18), ValidPriceCategory, ValidPetFriendly);
        Assert.That(place.OpenedAtNight, Is.False);
    }

    [Test]
    public void AddInstanceToExtent_OnCreation_IncreasesExtentCount()
    {
        int initialCount = Place.GetExtentCopy().Count;
        var newPlace = new TestPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime,
            ValidPriceCategory, ValidPetFriendly);

        Assert.That(Place.GetExtentCopy().Count, Is.EqualTo(initialCount + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemoval_DecreasesExtentCount()
    {
        int initialCount = Place.GetExtentCopy().Count;
        Place.RemoveInstanceFromExtent(_valid);
        Assert.That(Place.GetExtentCopy().Count, Is.EqualTo(initialCount - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtentReference()
    {
        Assert.That(Place.GetExtentCopy(), Is.Not.SameAs(Place.GetExtent()));
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Place.ResetExtent();
        Assert.That(Place.GetExtent().Count, Is.EqualTo(0));
    }
}