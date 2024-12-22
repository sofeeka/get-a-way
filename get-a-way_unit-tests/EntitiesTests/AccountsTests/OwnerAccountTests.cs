using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class OwnerAccountTests
{
    private OwnerAccount _validOwnerAccount = new OwnerAccount(ValidUserName, ValidPassword, ValidEmail);

    private static readonly Place ValidPlace = new Shop("ValidName", "ValidLocation", DateTime.Today.AddHours(8),
        DateTime.Today.AddHours(21), PriceCategory.Moderate, true, ShopType.Supermarket, true);

    private const string ValidUserName = "ValidUserName";
    private const string ValidPassword = "ValidPassword123";
    private const string ValidEmail = "validemail@pjwstk.edu.pl";

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
        _validOwnerAccount.AddPlace(ValidPlace);

        Assert.That(() => _validOwnerAccount.Places.Contains(ValidPlace)); // place added to places owned by owner
        Assert.That(() => ValidPlace.Owners.Contains(_validOwnerAccount)); // owner added to owners of place
    }

    [Test]
    public void AddPlace_NullPlace_AddsPlaceAndReverseReference()
    {
        Assert.That(() => _validOwnerAccount.AddPlace(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddPlace_DuplicatePlace_DoesNothing()
    {
        _validOwnerAccount.AddPlace(ValidPlace);
        int count = _validOwnerAccount.Places.Count;

        _validOwnerAccount.AddPlace(ValidPlace);
        Assert.That(() => count == _validOwnerAccount.Places.Count);
    }

    [Test]
    public void RemovePlace_ValidPlace_RemovesPlaceAndReverseReference()
    {
        _validOwnerAccount.AddPlace(ValidPlace);

        Place otherValidPlace = new Shop("ValidName", "ValidLocation", DateTime.Today.AddHours(8),
            DateTime.Today.AddHours(21), PriceCategory.Moderate, true, ShopType.Supermarket, true);
        _validOwnerAccount.AddPlace(otherValidPlace);

        _validOwnerAccount.RemovePlace(otherValidPlace);

        Assert.True(_validOwnerAccount.Places.Count == 1); // only ValidPlace
        Assert.False(_validOwnerAccount.Places.Contains(otherValidPlace)); // place removed from places owned by owner
        Assert.False(otherValidPlace.Owners.Contains(_validOwnerAccount)); // owner removed from owners of place
    }

    [Test]
    public void RemovePlace_NullPlace_AddsPlaceAndReverseReference()
    {
        Assert.That(() => _validOwnerAccount.RemovePlace(null), Throws.TypeOf<ArgumentNullException>());
    }
}