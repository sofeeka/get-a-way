using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests;

public class ShopTests
{
    private Shop _valid;

    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory priceCategory = PriceCategory.Free;
    private static bool petFriendly = true;
    private static ShopType shopType = ShopType.Mall;
    private static bool onlineOrderAvailability = true;

    
    [SetUp]
    public void SetUpEnvironment()
    {
        Shop.ResetExtent();
        _valid = new Shop(ValidName, ValidLocation,_validOpenTime, _validCloseTime, 
            priceCategory, petFriendly, shopType, onlineOrderAvailability);
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
}