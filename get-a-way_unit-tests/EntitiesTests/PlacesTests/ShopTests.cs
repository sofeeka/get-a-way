using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests;

public class ShopTests
{
    private Shop _valid;

    // place fields
    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory priceCategory = PriceCategory.Free;
    private static bool petFriendly = true;

    // shop fields
    private static ShopType shopType = ShopType.Mall;
    private static bool onlineOrderAvailability = true;
    private static List<string> _validHolidaySpecials;

    [SetUp]
    public void SetUpEnvironment()
    {
        Place.ResetExtent();
        _valid = new Shop(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            priceCategory, petFriendly, shopType, onlineOrderAvailability);
        _validHolidaySpecials = new List<string>();
        _validHolidaySpecials.Add("New Holiday Special");
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var shop = new Shop(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            priceCategory, petFriendly, shopType, onlineOrderAvailability);

        // ID == 2 because _valid.ID == 1
        Assert.That(shop.ID, Is.EqualTo(2));

        Assert.That(shop.Type, Is.EqualTo(shopType));
        Assert.That(shop.OnlineOrderAvailability, Is.EqualTo(onlineOrderAvailability));
        Assert.That(shop.HolidaySpecials, Is.Empty);
    }

    [Test]
    public void Setter_ValidShopType_SetsShopType()
    {
        _valid.Type = ShopType.Supermarket;
        Assert.That(_valid.Type, Is.EqualTo(ShopType.Supermarket));
    }

    [Test]
    public void Setter_ValidOnlineOrderAvailability_SetsOnlineOrderAvailability()
    {
        _valid.OnlineOrderAvailability = false;
        Assert.That(_valid.OnlineOrderAvailability, Is.EqualTo(false));
    }

    [Test]
    public void Setter_ValidHolidaySpecialsList_SetsHolidaySpecialsList()
    {
        List<string> newValidHolidaySpecials = new List<string>();
        newValidHolidaySpecials.Add("New Valid Holiday Special");

        _valid.HolidaySpecials = newValidHolidaySpecials;
        Assert.That(_valid.PictureUrls, Is.EqualTo(newValidHolidaySpecials));
    }

    [Test]
    public void Setter_InvalidHolidaySpecialsList_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.HolidaySpecials = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));

        List<string> holidaySpecials = new List<string>();

        for (int i = 0; i < 102; i++)
            holidaySpecials.Add("New holiday Special");

        Assert.That(() => _valid.HolidaySpecials = holidaySpecials, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));
    }

    [Test]
    public void Setter_InvalidHolidaySpecial_ThrowsInvalidHolidaySpecialException()
    {
        List<string> holidaySpecials = new List<string>();
        holidaySpecials.Add(null);
        Assert.That(() => _valid.HolidaySpecials = holidaySpecials, Throws.TypeOf<InvalidHolidaySpecialException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));

        holidaySpecials.Clear();
        holidaySpecials.Add("");
        Assert.That(() => _valid.HolidaySpecials = holidaySpecials, Throws.TypeOf<InvalidHolidaySpecialException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));

        holidaySpecials.Clear();
        holidaySpecials.Add(" ");
        Assert.That(() => _valid.HolidaySpecials = holidaySpecials, Throws.TypeOf<InvalidHolidaySpecialException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));

        holidaySpecials.Clear();
        holidaySpecials.Add("invalid path");
        Assert.That(() => _valid.HolidaySpecials = holidaySpecials, Throws.TypeOf<InvalidHolidaySpecialException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));

        holidaySpecials.Clear();
        holidaySpecials.Add("https://not/image/extention");
        Assert.That(() => _valid.HolidaySpecials = holidaySpecials, Throws.TypeOf<InvalidHolidaySpecialException>());
        Assert.That(() => _valid.HolidaySpecials, Is.EqualTo(_validHolidaySpecials));
    }
}