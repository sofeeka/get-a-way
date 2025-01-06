using get_a_way;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class OwnerAccountTests
{
    private readonly OwnerAccount _validOwnerAccount = new OwnerAccount(ValidUserName, ValidPassword, ValidEmail);
    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static Place _validPlace;
    private static Place _otherValidPlace;

    private const string ValidUserName = "ValidUserName";
    private const string ValidPassword = "ValidPassword123";
    private const string ValidEmail = "validemail@pjwstk.edu.pl";

    [SetUp]
    public void SetUpEnvironment()
    {
        Owners.Add(_validOwnerAccount);
        _validPlace = new Shop(Owners, "ValidName", "ValidLocation",
            DateTime.Today.AddHours(8), DateTime.Today.AddHours(21), PriceCategory.Moderate, true,
            ShopType.Supermarket, true);
    }

    [TearDown]
    public void TearDownEnvironment()
    {
        Database.Reset();
    }

    [Test]
    public void SetTax_ValidValue_SetsTax()
    {
        _validOwnerAccount.Tax = 20;
        Assert.That(_validOwnerAccount.Tax, Is.EqualTo(20));
    }

    [Test]
    public void SetTax_InvalidValue_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validOwnerAccount.Tax = -5, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validOwnerAccount.Tax = 105, Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void AddPlace_ValidPlace_AddsPlaceAndReverseReference()
    {
        // owner is added in constructor of place
        _otherValidPlace = new Shop(Owners, "OtherValidName", "ValidLocation", DateTime.Today.AddHours(8),
            DateTime.Today.AddHours(21), PriceCategory.Moderate, true, ShopType.Supermarket, true);
        Assert.That(() => _validOwnerAccount.Places.Contains(_otherValidPlace)); // place added to places owned by owner
        Assert.That(() => _otherValidPlace.Owners.Contains(_validOwnerAccount)); // owner added to owners of place
    }

    [Test]
    public void AddPlace_NullPlace_ThrowsArgumentNullException()
    {
        Assert.That(() => _validOwnerAccount.AddPlace(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddPlace_DuplicatePlace_DoesNothing()
    {
        int count = _validOwnerAccount.Places.Count;
        _validOwnerAccount.AddPlace(_validPlace);
        Assert.That(() => count == _validOwnerAccount.Places.Count);
    }

    [Test]
    public void RemovePlace_ValidPlace_RemovesPlaceAndReverseReference()
    {
        Place otherValidPlace = new Shop(Owners, "ValidName", "ValidLocation", DateTime.Today.AddHours(8),
            DateTime.Today.AddHours(21), PriceCategory.Moderate, true, ShopType.Supermarket, true);
        Assert.That(_validOwnerAccount.Places, Does.Contain(otherValidPlace)); // was added after creation
        Assert.That(otherValidPlace.Owners, Does.Contain(_validOwnerAccount)); // reverse connection too

        _validOwnerAccount.RemovePlace(otherValidPlace);

        Assert.That(_validOwnerAccount.Places, Does.Not.Contain(otherValidPlace)); // removed from places owned by owner
        Assert.That(otherValidPlace.Owners, Does.Not.Contain(_validOwnerAccount)); // removed from owners of place
    }

    [Test]
    public void RemovePlace_NullPlace_AddsPlaceAndReverseReference()
    {
        Assert.That(() => _validOwnerAccount.RemovePlace(null), Throws.TypeOf<ArgumentNullException>());
    }
}