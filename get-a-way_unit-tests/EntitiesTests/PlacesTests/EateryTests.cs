using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Eatery;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests;

public class EateryTests
{
    private Eatery _valid;

    // place fields
    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory priceCategory = PriceCategory.Free;
    private static bool petFriendly = true;

    // eatery fields
    private static EateryType eateryType = EateryType.Cafe;
    private static Cuisine cusine = Cuisine.Italian;
    private static List<string> _validMenu;
    private static HashSet<DietaryOptions> _dietaryOptions;
    private bool _reservationRequired = true;

    [SetUp]
    public void SetUpEnvironment()
    {
        Place.ResetExtent();
        _valid = new Eatery(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            priceCategory, petFriendly, eateryType, cusine, _reservationRequired);
        _validMenu = new List<string>();
        _validMenu.Add("New Valid Menu Item");
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var eatery = new Eatery(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            priceCategory, petFriendly, eateryType, cusine, _reservationRequired);

        // ID == 2 because _valid.ID == 1
        Assert.That(eatery.ID, Is.EqualTo(2));

        Assert.That(eatery.Type, Is.EqualTo(eateryType));
        Assert.That(eatery.Cuisine, Is.EqualTo(cusine));
        Assert.That(eatery.Menu, Is.Empty);
        Assert.That(eatery.DietaryOptions, Is.Empty);
        Assert.That(eatery.ReservationRequired, Is.EqualTo(_reservationRequired));
    }

    [Test]
    public void Setter_ValidEateryType_SetsEateryType()
    {
        _valid.Type = EateryType.Bar;
        Assert.That(_valid.Type, Is.EqualTo(EateryType.Bar));
    }

    [Test]
    public void Setter_ValidCuisine_SetsCuisine()
    {
        _valid.Cuisine = Cuisine.Ukrainian;
        Assert.That(_valid.Cuisine, Is.EqualTo(Cuisine.Ukrainian));
    }


    [Test]
    public void Setter_ValidMenu_SetsMenu()
    {
        _valid.Menu = _validMenu;
        Assert.That(_valid.Menu, Is.EqualTo(_validMenu));
    }

    [Test]
    public void Setter_InvalidMenu_ThrowsInvalidAttributeException()
    {
        _valid.Menu = _validMenu;

        Assert.That(() => _valid.Menu = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Menu, Is.EqualTo(_validMenu));
    }

    [Test]
    public void Setter_InvalidMenuItem_ThrowsInvalidMenuItemException()
    {
        _valid.Menu = _validMenu;

        List<string> menu = new List<string>();
        menu.Add(null);
        Assert.That(() => _valid.Menu = menu, Throws.TypeOf<InvalidMenuItemException>());
        Assert.That(() => _valid.Menu, Is.EqualTo(_validMenu));

        menu.Clear();
        menu.Add("");
        Assert.That(() => _valid.Menu = menu, Throws.TypeOf<InvalidMenuItemException>());
        Assert.That(() => _valid.Menu, Is.EqualTo(_validMenu));

        menu.Clear();
        menu.Add(" ");
        Assert.That(() => _valid.Menu = menu, Throws.TypeOf<InvalidMenuItemException>());
        Assert.That(() => _valid.Menu, Is.EqualTo(_validMenu));
    }
}