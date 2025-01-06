using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests.AttractionsTests;

public class HistoricalAttractionTests
{
    private HistoricalAttraction _valid;

    // place fields
    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory _priceCategory = PriceCategory.Free;
    private static bool _petFriendly = true;

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("HistoricalAttractionOwner", "ValidPassword123", "validemail@pjwstk.edu.pl");

    // attraction fields
    private int _validEntryFee = 15;
    private int _validMinimalAge = 8;
    private List<string> _validEvents;
    private string _validDescription = "Some description";

    // historical attraction fields
    private string _validCulturalPeriod = "valid historical period";

    [SetUp]
    public void SetUpEnvironment()
    {
        Owners.Add(DummyOwner);
        _valid = new HistoricalAttraction(Owners, ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _validEntryFee, _validMinimalAge, _validDescription, _validCulturalPeriod);

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
    public void Setter_ValidCulturalPeriod_SetsCulturalPeriod()
    {
        _valid.CulturalPeriod = _validCulturalPeriod;
        Assert.That(_valid.CulturalPeriod, Is.EqualTo(_validCulturalPeriod));
    }

    [Test]
    public void Setter_InvalidCulturalPeriod_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.CulturalPeriod = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.CulturalPeriod, Is.EqualTo(_validCulturalPeriod));

        Assert.That(() => _valid.CulturalPeriod = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.CulturalPeriod, Is.EqualTo(_validCulturalPeriod));

        Assert.That(() => _valid.CulturalPeriod = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.CulturalPeriod, Is.EqualTo(_validCulturalPeriod));

        string tooShort = "min";
        Assert.That(() => _valid.CulturalPeriod = tooShort, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.CulturalPeriod, Is.EqualTo(_validCulturalPeriod));

        var tooLong = new string('a', 1001);
        Assert.That(() => _valid.CulturalPeriod = tooLong, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.CulturalPeriod, Is.EqualTo(_validCulturalPeriod));
    }
}