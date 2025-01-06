using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests.AttractionsTests;

public class ActiveAttractionTests
{
    private ActiveAttraction _valid;

    // place fields
    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory _priceCategory = PriceCategory.Free;
    private static bool _petFriendly = true;

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("ActiveAttractionOwner", "ValidPassword123", "validemail@pjwstk.edu.pl");

    // attraction fields
    private int _validEntryFee = 15;
    private int _validMinimalAge = 8;
    private List<string> _validEvents;
    private string _validDescription = "Some description";

    // active attraction fields
    private string _validActivityType = "valid activity type";

    [SetUp]
    public void SetUpEnvironment()
    {
        Owners.Add(DummyOwner);
        _valid = new ActiveAttraction(Owners, ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _validEntryFee, _validMinimalAge, _validDescription, _validActivityType);

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
    public void Setter_ValidActivityType_SetsActivityType()
    {
        _valid.ActivityType = _validActivityType;
        Assert.That(_valid.ActivityType, Is.EqualTo(_validActivityType));
    }

    [Test]
    public void Setter_InvalidActivityType_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.ActivityType = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));

        Assert.That(() => _valid.ActivityType = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));

        Assert.That(() => _valid.ActivityType = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));

        string tooShort = "min";
        Assert.That(() => _valid.ActivityType = tooShort, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));

        var tooLong = new string('a', 1001);
        Assert.That(() => _valid.ActivityType = tooLong, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));
    }
}