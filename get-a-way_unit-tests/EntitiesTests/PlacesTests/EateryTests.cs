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
    private static EateryType _eateryType = EateryType.Cafe;
    private static Cuisine _cusine = Cuisine.Italian;
    private static List<string> _validMenu;
    private static HashSet<DietaryOptions> _validDietaryOptions;
    private bool _reservationRequired = true;

    [SetUp]
    public void SetUpEnvironment()
    {
        Place.ResetExtent();
        _valid = new Eatery(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            priceCategory, petFriendly, _eateryType, _cusine, _reservationRequired);

        _validMenu = new List<string>();
        _validMenu.Add("New Valid Menu Item");

        _validDietaryOptions = new HashSet<DietaryOptions>();
        _validDietaryOptions.Add(DietaryOptions.Vegan);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var eatery = new Eatery(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            priceCategory, petFriendly, _eateryType, _cusine, _reservationRequired);

        // ID == 2 because _valid.ID == 1
        Assert.That(eatery.ID, Is.EqualTo(2));

        Assert.That(eatery.Type, Is.EqualTo(_eateryType));
        Assert.That(eatery.Cuisine, Is.EqualTo(_cusine));
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

    [Test]
    public void Setter_ValidDietaryOptions_SetsDietaryOptions()
    {
        _valid.DietaryOptions = _validDietaryOptions;
        Assert.That(_valid.DietaryOptions, Is.EqualTo(_validDietaryOptions));
    }

    [Test]
    public void Setter_InvalidDietaryOptions_ThrowsInvalidAttributeException()
    {
        _valid.DietaryOptions = _validDietaryOptions;

        Assert.That(() => _valid.DietaryOptions = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.DietaryOptions, Is.EqualTo(_validDietaryOptions));
    }

    [Test]
    public void Setter_DuplicateDietaryOptionsItem_DoesNotChangeSizeOfDietaryOptionsSet()
    {
        _valid.DietaryOptions.Add(DietaryOptions.Keto);
        int count = _valid.DietaryOptions.Count;
        _valid.DietaryOptions.Add(DietaryOptions.Keto);
        Assert.That(() => _valid.DietaryOptions.Count, Is.EqualTo(count));
    }

    [Test]
    public void Setter_ValidReservationRequired_SetsReservationRequired()
    {
        _valid.ReservationRequired = false;
        Assert.That(_valid.ReservationRequired, Is.EqualTo(false));
    }
}