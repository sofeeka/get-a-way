using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;

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
    private static List<string> holidaySpecials;
    
    [SetUp]
    public void SetUpEnvironment()
    {
        Shop.ResetExtent();
        _valid = new Shop(ValidName, ValidLocation,_validOpenTime, _validCloseTime, 
            priceCategory, petFriendly, shopType, onlineOrderAvailability);
        holidaySpecials = new List<string>();
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var shop = new Shop(ValidName, ValidLocation,_validOpenTime, _validCloseTime, 
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
}