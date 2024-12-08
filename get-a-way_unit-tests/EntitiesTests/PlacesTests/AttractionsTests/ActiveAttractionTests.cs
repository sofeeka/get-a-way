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
        Place.ResetExtent();
        _valid = new ActiveAttraction(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _validEntryFee, _validMinimalAge, _validDescription, _validActivityType);

        _validEvents = new List<string>();
        _validEvents.Add("Some new valid event");
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

        Assert.That(() => _valid.ActivityType = "short", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));

        string tooLong = """
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
        Assert.That(() => _valid.ActivityType = tooLong, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.ActivityType, Is.EqualTo(_validActivityType));
    }

}