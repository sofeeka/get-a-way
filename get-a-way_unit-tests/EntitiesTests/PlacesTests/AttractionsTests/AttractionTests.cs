using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests.AttractionsTests;

public class AttractionTests
{
    private class TestAttraction(
        HashSet<OwnerAccount> owners,
        string name,
        string location,
        DateTime openTime,
        DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly,
        int entryFee,
        int minimalAge,
        string description)
        : Attraction(owners, name, location, openTime, closeTime, priceCategory, petFriendly, entryFee, minimalAge,
            description);

    private TestAttraction _valid;

    // place fields
    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory _priceCategory = PriceCategory.Free;
    private static bool _petFriendly = true;

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("AttractionOwner", "ValidPassword123", "validemail@pjwstk.edu.pl");

    // attraction fields
    private int _validEntryFee = 15;
    private int _validMinimalAge = 8;
    private List<string> _validEvents;
    private string _validDescription = "Some description";

    [SetUp]
    public void SetUpEnvironment()
    {
        Owners.Add(DummyOwner);
        _valid = new TestAttraction(Owners, ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _validEntryFee, _validMinimalAge, _validDescription);

        _validEvents = new List<string>();
        _validEvents.Add("Some new valid event");
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
        var attraction = new TestAttraction(Owners, ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _validEntryFee, _validMinimalAge, _validDescription);

        Assert.That(attraction.EntryFee, Is.EqualTo(_validEntryFee));
        Assert.That(attraction.MinimalAge, Is.EqualTo(_validMinimalAge));
        Assert.That(attraction.Events, Is.Empty);
        Assert.That(attraction.Description, Is.EqualTo(_validDescription));
    }

    [Test]
    public void Setter_ValidEntryFee_SetsEntryFee()
    {
        _valid.EntryFee = _validEntryFee;
        Assert.That(_valid.EntryFee, Is.EqualTo(_validEntryFee));

        _valid.EntryFee = 0;
        Assert.That(_valid.EntryFee, Is.EqualTo(0));
    }

    [Test]
    public void Setter_InvalidEntryFee_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.EntryFee = -1, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.EntryFee, Is.EqualTo(_validEntryFee));
    }

    [Test]
    public void Setter_ValidMinimalAge_SetsMinimalAge()
    {
        _valid.MinimalAge = _validMinimalAge;
        Assert.That(_valid.MinimalAge, Is.EqualTo(_validMinimalAge));

        _valid.MinimalAge = 0;
        Assert.That(_valid.MinimalAge, Is.EqualTo(0));
    }

    [Test]
    public void Setter_InvalidMinimalAge_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.MinimalAge = -1, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.MinimalAge, Is.EqualTo(_validMinimalAge));

        Assert.That(() => _valid.MinimalAge = 125, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.MinimalAge, Is.EqualTo(_validMinimalAge));
    }

    [Test]
    public void Setter_ValidEvents_SetsEvents()
    {
        _valid.Events = _validEvents;
        Assert.That(_valid.Events, Is.EqualTo(_validEvents));
    }

    [Test]
    public void Setter_InvalidEvents_ThrowsInvalidAttributeException()
    {
        _valid.Events = _validEvents;

        Assert.That(() => _valid.Events = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Events, Is.EqualTo(_validEvents));
    }

    [Test]
    public void Setter_InvalidEventsItem_ThrowsInvalidEventsItemException()
    {
        _valid.Events = _validEvents;

        List<string> events = new List<string>();
        events.Add(null);
        Assert.That(() => _valid.Events = events, Throws.TypeOf<InvalidEventException>());
        Assert.That(() => _valid.Events, Is.EqualTo(_validEvents));

        events.Clear();
        events.Add("");
        Assert.That(() => _valid.Events = events, Throws.TypeOf<InvalidEventException>());
        Assert.That(() => _valid.Events, Is.EqualTo(_validEvents));

        events.Clear();
        events.Add(" ");
        Assert.That(() => _valid.Events = events, Throws.TypeOf<InvalidEventException>());
        Assert.That(() => _valid.Events, Is.EqualTo(_validEvents));
    }

    [Test]
    public void Setter_ValidDescription_SetsDescription()
    {
        _valid.Description = _validDescription;
        Assert.That(_valid.Description, Is.EqualTo(_validDescription));
    }

    [Test]
    public void Setter_InvalidDescription_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Description = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(_validDescription));

        Assert.That(() => _valid.Description = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(_validDescription));

        Assert.That(() => _valid.Description = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(_validDescription));

        Assert.That(() => _valid.Description = "short", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(_validDescription));

        var tooLong = new string('a', 1001);
        Assert.That(() => _valid.Description = tooLong, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(_validDescription));
    }
}